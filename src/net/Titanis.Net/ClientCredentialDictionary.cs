using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Security;

namespace Titanis.Net
{
	/// <summary>
	/// Base for a for a <see cref="IClientCredentialService"/> implementation.
	/// </summary>
	public class ClientCredentialServiceBase : IClientCredentialService
	{
		/// <inheritdoc/>
		public virtual AuthClientContext? GetAuthContextForResource(string resourceType, object resourceKey, SecurityCapabilities requiredCaps, AuthOptions options)
		{
			if (resourceType is ResourceTypes.Service)
			{
				if (resourceKey is ServicePrincipalName spn)
					return this.GetAuthContextForService(spn, requiredCaps, options);
				else
					throw new ArgumentException($"resourceKey must be a {nameof(ServicePrincipalName)} for resource type '{ResourceTypes.Service}', but an argument of type '{resourceKey?.GetType()?.FullName ?? "<null>"}' was provided.", nameof(resourceKey));
			}
			else if (resourceType is ResourceTypes.SmbShare)
			{
				if (resourceKey is UncPath uncPath)
					return this.GetAuthContextForSmbShare(uncPath, requiredCaps, options);
				else
					throw new ArgumentException($"resourceKey must be a {nameof(UncPath)} for resource type '{ResourceTypes.Service}', but an argument of type '{resourceKey?.GetType()?.FullName ?? "<null>"}' was provided.", nameof(resourceKey));
			}
			else
				return null;
		}
		/// <summary>
		/// Gets a credential for a service.
		/// </summary>
		/// <param name="spn"><see cref="ServicePrincipalName"/> of the service</param>
		/// <param name="requiredCaps"><see cref="SecurityCapabilities"/> required by the caller</param>
		/// <param name="options">Options affecting the creation of the authentication context</param>
		/// <returns>An <see cref="AuthClientContext"/> for the credentials, if found; otherwise, <see langword="null"/></returns>
		public virtual AuthClientContext? GetAuthContextForService(ServicePrincipalName spn, SecurityCapabilities requiredCaps, AuthOptions options)
		{
			ArgumentNullException.ThrowIfNull(spn);
			return null;
		}

		/// <summary>
		/// Gets a credential for an SMB share.
		/// </summary>
		/// <param name="uncPath">Path of SMB share</param>
		/// <param name="requiredCaps"><see cref="SecurityCapabilities"/> required by the caller</param>
		/// <param name="options">Options affecting the creation of the authentication context</param>
		/// <returns>An <see cref="AuthClientContext"/> for the credentials, if found; otherwise, <see langword="null"/></returns>
		public virtual AuthClientContext? GetAuthContextForSmbShare(UncPath uncPath, SecurityCapabilities requiredCaps, AuthOptions options)
		{
			ArgumentNullException.ThrowIfNull(uncPath);
			return null;
		}
	}
	/// <summary>
	/// Implements <see cref="IClientCredentialService"/> as a simple dictionary.
	/// </summary>
	/// <remarks>
	/// Add credentials by calling <see cref="AddCredential(ServicePrincipalName, Func{ServicePrincipalName, SecurityCapabilities, AuthClientContext})"/> or related methods.
	/// </remarks>
	public class ClientCredentialDictionary : IClientCredentialService
	{
		/// <summary>
		/// Initializes a new <see cref="ClientCredentialDictionary"/>.
		/// </summary>
		/// <param name="fallbackService">Service to call when a requested credential is not available.</param>
		public ClientCredentialDictionary(IClientCredentialService? fallbackService = null)
		{
			this._fallbackService = fallbackService;
		}

		struct CredKey : IEquatable<CredKey>
		{
			public CredKey(string resourceType, object key)
			{
				ResourceType = resourceType;
				Key = key;
			}

			public string ResourceType { get; }
			public object Key { get; }

			public override bool Equals(object? obj)
			{
				return obj is CredKey key && Equals(key);
			}

			public bool Equals(CredKey other)
			{
				return ResourceType == other.ResourceType &&
					   EqualityComparer<object>.Default.Equals(Key, other.Key);
			}

			public override int GetHashCode()
			{
				return HashCode.Combine(ResourceType, Key);
			}

			public static bool operator ==(CredKey left, CredKey right)
			{
				return left.Equals(right);
			}

			public static bool operator !=(CredKey left, CredKey right)
			{
				return !(left == right);
			}
		}

		/// <summary>
		/// Gets or sets a function that creates a <see cref="AuthClientContext"/> when no more specific credentials are available.
		/// </summary>
		public Func<ServicePrincipalName, SecurityCapabilities, AuthClientContext>? DefaultCredentialFactory { get; set; }

		private Dictionary<CredKey, Func<SecurityCapabilities, AuthClientContext>> _credentials = new();
		private readonly IClientCredentialService? _fallbackService;

		public void AddCredential(UncPath sharePath, Func<UncPath, SecurityCapabilities, AuthClientContext> authContextFactory)
		{
			if (sharePath is null) throw new ArgumentNullException(nameof(sharePath));
			if (authContextFactory is null) throw new ArgumentNullException(nameof(authContextFactory));
			this._credentials.Add(new CredKey(ResourceTypes.SmbShare, sharePath), caps => authContextFactory(sharePath, caps));
		}
		public void AddCredential(ServicePrincipalName service, Func<ServicePrincipalName, SecurityCapabilities, AuthClientContext> authContextFactory)
		{
			if (service is null) throw new ArgumentNullException(nameof(service));
			if (authContextFactory is null) throw new ArgumentNullException(nameof(authContextFactory));
			this._credentials.Add(new CredKey(ResourceTypes.Service, service), caps => authContextFactory(service, caps));
		}

		/// <summary>
		/// Adds a client credential.
		/// </summary>
		/// <param name="resourceType">Resource type</param>
		/// <param name="resourceKey">Resource key</param>
		/// <param name="authContextFactory">Function accepting <paramref name="resourceKey"/> and <paramref name="resourceKey"/> and returning a <see cref="AuthClientContext"/> for the resource</param>
		/// <exception cref="ArgumentException"><paramref name="resourceType"/> is <see langword="null"/> or empty.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="authContextFactory"/> is <see langword="null"/>.</exception>
		public void AddOtherCredential(string resourceType, object resourceKey, Func<string, object, SecurityCapabilities, AuthClientContext> authContextFactory)
		{
			if (string.IsNullOrEmpty(resourceType)) throw new ArgumentException($"'{nameof(resourceType)}' cannot be null or empty.", nameof(resourceType));
			if (authContextFactory is null) throw new ArgumentNullException(nameof(authContextFactory));

			this._credentials.Add(new CredKey(resourceType, resourceKey), caps => authContextFactory(resourceType, resourceKey, caps));
		}

		/// <inheritdoc/>
		public AuthClientContext GetAuthContextForResource(string resourceType, object resourceKey, SecurityCapabilities requiredCaps, AuthOptions options)
		{
			if (this._credentials.TryGetValue(new CredKey(resourceType, resourceKey), out var cred))
			{
				var authContext = cred(requiredCaps);
				if (authContext == null)
					throw new InvalidOperationException($"The provided factory did not return an AuthContext for {resourceKey}.");

				return authContext;
			}
			else if (resourceKey is ServicePrincipalName spn)
			{
				if (this.DefaultCredentialFactory != null)
					return this.DefaultCredentialFactory(spn, requiredCaps);
			}

			return this._fallbackService?.GetAuthContextForResource(resourceType, resourceKey, requiredCaps, options);
		}
	}
}
