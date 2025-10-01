using System;
using Titanis.Security;

namespace Titanis.Net
{
	[Flags]
	public enum AuthOptions
	{
		None = 0,

		PreferSpnego = 1,
	}

	/// <summary>
	/// Provides functionality to retrieve credentials for a resource.
	/// </summary>
	public interface IClientCredentialService
	{
		/// <summary>
		/// Gets a client authentication context for a server.
		/// </summary>
		/// <param name="resourceType">Type of resource</param>
		/// <param name="resourceKey">Resource key</param>
		/// <param name="requiredCaps"><see cref="SecurityCapabilities"/> required by the caller</param>
		/// <param name="options">Options affecting the creation of the authentication context</param>
		/// <returns>The <see cref="AuthClientContext"/> to use to authenticate.</returns>
		/// <remarks>
		/// If the requested credential is not available, the implementation should return
		/// <see langword="null"/>.
		/// <para>
		/// The type of <paramref name="resourceKey"/> depends on the <paramref name="resourceType"/>.
		/// </para>
		/// <para>
		/// See <see cref="ResourceTypes"/> for a list of predefined resource types.
		/// </para>
		/// </remarks>
		AuthClientContext? GetAuthContextForResource(string resourceType, object resourceKey, SecurityCapabilities requiredCaps, AuthOptions options = AuthOptions.None);
	}

	public static class ClientCredentialServiceExtensions
	{
		public static AuthClientContext? GetAuthContextForSmbShare(
			this IClientCredentialService credentialService,
			UncPath sharePath,
			SecurityCapabilities requiredCaps,
			AuthOptions options = AuthOptions.None)
			=> credentialService.GetAuthContextForResource(ResourceTypes.SmbShare, sharePath, requiredCaps, options);
		public static AuthClientContext? GetAuthContextForService(
			this IClientCredentialService credentialService,
			ServicePrincipalName service,
			SecurityCapabilities requiredCaps,
			AuthOptions options = AuthOptions.None)
			=> credentialService.GetAuthContextForResource(ResourceTypes.Service, service, requiredCaps, options);
	}

	public static class ResourceTypes
	{
		public const string Server = "Server";
		public const string Service = "Service";
		public const string SmbShare = "SmbShare";
	}
}