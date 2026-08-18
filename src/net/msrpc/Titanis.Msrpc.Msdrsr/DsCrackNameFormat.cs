namespace Titanis.Msrpc.Msdrsr
{
	// [MS-DRSR] § 4.1.4.1.2 DRS_MSG_CRACKREQ_V1 
	public enum DsCrackNameFormat : uint
	{
		ListSites = 0xFFFFFFFF,
		ListServersInSite = 0xFFFFFFFE,
		ListDomainsInSite = 0xFFFFFFFD,
		ListServersForDomainInSite = 0xFFFFFFFC,
		ListInfoForServer = 0xFFFFFFFB,
		ListRoles = 0xFFFFFFFA,
		SamAccountNameSansDomain = 0xFFFFFFF9,
		MapSchemaGuid = 0xFFFFFFF8,
		ListDomains = 0xFFFFFFF7,
		ListPartitions = 0xFFFFFFF6,
		AltSecurityIdentitiesName = 0xFFFFFFF5,
		StringSidName = 0xFFFFFFF4,
		ListServersWithDcsInSite = 0xFFFFFFF3,
		ListGlobalCatalogServers = 0xFFFFFFF1,
		SamAccountNameEx = 0xFFFFFFF0,
		UpnAndAltSecId = 0xFFFFFFEF,

		Unknown = 0,
		Fqdn1779 = 1,
		SamAccountName = 2,
		DisplayName = 3,
		UniqueIdName = 6,
		CanonicalName = 7,
		UserPrincipalName = 8,
		CanonicalNameEx = 9,
		ServicePrincipalName = 10,
		SidOrSidHistory = 11,
		DnsDomainName = 12
	}
}
