namespace Titanis.Msrpc.Msdrsr
{
	// [MS-DRSR] § 4.1.4.1.2 DRS_MSG_CRACKREQ_V1 
	public enum DsCrackNameResultFormat : uint
	{
		StringSidName = 0xFFFFFFF4,
		UpnForLogon = 0xFFFFFFF2,


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
