namespace Titanis.Msrpc.Msdrsr
{
    public class DomainControllerInfo
	{
		internal DomainControllerInfo(
			string netbiosName,
			string dnsHostName,
			string siteName,
			string siteObjectName,
			string computerObjectName,
			string serverObjectName,
			string ntdsDsaObjectName,
			bool isPdc,
			bool isDsEnabled,
			bool isGc,
			Guid siteObjectGuid,
			Guid computerObjectGuid,
			Guid serverObjectGuid,
			Guid ntdsDsaObjectGuid
			)
		{
			NetbiosName = netbiosName;
			DnsHostName = dnsHostName;
			SiteName = siteName;
			SiteObjectName = siteObjectName;
			ComputerObjectName = computerObjectName;
			ServerObjectName = serverObjectName;
			NtdsDsaObjectName = ntdsDsaObjectName;
			IsPdc = isPdc;
			IsDsEnabled = isDsEnabled;
			IsGc = isGc;
			SiteObjectGuid = siteObjectGuid;
			ComputerObjectGuid = computerObjectGuid;
			ServerObjectGuid = serverObjectGuid;
			NtdsDsaObjectGuid = ntdsDsaObjectGuid;
		}

		public string NetbiosName { get; }
		public string DnsHostName { get; }
		public string SiteName { get; }
		public string SiteObjectName { get; }
		public string ComputerObjectName { get; }
		public string ServerObjectName { get; }
		public string NtdsDsaObjectName { get; }
		public bool IsPdc { get; }
		public bool IsDsEnabled { get; }
		public bool IsGc { get; }
		public Guid SiteObjectGuid { get; }
		public Guid ComputerObjectGuid { get; }
		public Guid ServerObjectGuid { get; }
		public Guid NtdsDsaObjectGuid { get; }
	}
}
