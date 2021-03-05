using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Security;

namespace Titanis.Net
{
	/// <summary>
	/// Implements <see cref="IClientCredentialService"/> as a simple dictionary.
	/// </summary>
	/// <remarks>
	/// Add credentials by calling <see cref="AddCredential"/>.
	/// </remarks>
	public class ClientCredentialService : IClientCredentialService
	{
		/// <summary>
		/// Initializes a new <see cref="ClientCredentialService"/>.
		/// </summary>
		/// <param name="fallbackService">Service to call when a requested credential is not available.</param>
		public ClientCredentialService(IClientCredentialService? fallbackService = null)
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

		public Func<ServicePrincipalName, AuthClientContext>? DefaultCredential { get; set; }

		private Dictionary<CredKey, Func<AuthClientContext>> _credentials = new();
		private readonly IClientCredentialService? _fallbackService;

		public void AddCredential(UncPath sharePath, Func<UncPath, AuthClientContext> authContextFactory)
		{
			if (sharePath is null) throw new ArgumentNullException(nameof(sharePath));
			if (authContextFactory is null) throw new ArgumentNullException(nameof(authContextFactory));
			this._credentials.Add(new CredKey(ResourceTypes.SmbShare, sharePath), () => authContextFactory(sharePath));
		}
		public void AddCredential(ServicePrincipalName service, Func<ServicePrincipalName, AuthClientContext> authContextFactory)
		{
			if (service is null) throw new ArgumentNullException(nameof(service));
			if (authContextFactory is null) throw new ArgumentNullException(nameof(authContextFactory));
			this._credentials.Add(new CredKey(ResourceTypes.Service, service), () => authContextFactory(service));
		}

		/// <summary>
		/// Adds a client credential.
		/// </summary>
		/// <param name="resourceType">Resource type</param>
		/// <param name="resourceKey">Resource key</param>
		/// <param name="authContextFactory">Function accepting <paramref name="resourceKey"/> and <paramref name="resourceKey"/> and returning a <see cref="AuthClientContext"/> for the resource</param>
		/// <exception cref="ArgumentException"><paramref name="resourceType"/> is <see langword="null"/> or empty.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="authContextFactory"/> is <see langword="null"/>.</exception>
		public void AddOtherCredential(string resourceType, object resourceKey, Func<string, object, AuthClientContext> authContextFactory)
		{
			if (string.IsNullOrEmpty(resourceType)) throw new ArgumentException($"'{nameof(resourceType)}' cannot be null or empty.", nameof(resourceType));
			if (authContextFactory is null) throw new ArgumentNullException(nameof(authContextFactory));

			this._credentials.Add(new CredKey(resourceType, resourceKey), () => authContextFactory(resourceType, resourceKey));
		}

		/// <inheritdoc/>
		public AuthClientContext GetAuthContextForResource(string resourceType, object resourceKey)
		{
			if (this._credentials.TryGetValue(new CredKey(resourceType, resourceKey), out var cred))
			{
				var authContext = cred();
				if (authContext == null)
					throw new InvalidOperationException($"The provided factory did not return an AuthContext for {resourceKey}.");

				return authContext;
			}
			else if (resourceKey is ServicePrincipalName spn)
			{
				if (this.DefaultCredential != null)
					return this.DefaultCredential(spn);
			}

			return this._fallbackService?.GetAuthContextForResource(resourceType, resourceKey);
		}
	}
}
