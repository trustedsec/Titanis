using ms_wkst;

namespace Titanis.Msrpc.Mswkst
{
	public enum JoinStatus : int
	{
		Unknown = NETSETUP_JOIN_STATUS.NetSetupUnknownStatus,
		Unjoined = NETSETUP_JOIN_STATUS.NetSetupUnjoined,
		Workgroup = NETSETUP_JOIN_STATUS.NetSetupWorkgroupName,
		Domain = NETSETUP_JOIN_STATUS.NetSetupDomainName,
	}
}
