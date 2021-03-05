using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mslsar
{
	public enum Privilege : long
	{
		SeAssignPrimaryTokenPrivilege = 3,
		SeAuditPrivilege = 21,
		SeBackupPrivilege = 17,
		SeChangeNotifyPrivilege = 23,
		SeCreateGlobalPrivilege = 30,
		SeCreatePagefilePrivilege = 15,
		SeCreatePermanentPrivilege = 16,
		SeCreateTokenPrivilege = 2,
		SeDebugPrivilege = 20,
		SeEnableDelegationPrivilege = 27,
		SeImpersonatePrivilege = 29,
		SeIncreaseBasePriorityPrivilege = 14,
		SeIncreaseQuotaPrivilege = 5,
		SeLoadDriverPrivilege = 10,
		SeLockMemoryPrivilege = 4,
		SeMachineAccountPrivilege = 6,
		SeManageVolumePrivilege = 28,
		SeProfileSingleProcessPrivilege = 13,
		SeRemoteShutdownPrivilege = 24,
		SeRestorePrivilege = 18,
		SeSecurityPrivilege = 8,
		SeShutdownPrivilege = 19,
		SeSyncAgentPrivilege = 26,
		SeSystemEnvironment = 22,
		SeSystemProfilePrivilege = 11,
		SeSystemtimePrivilege = 12,
		SeTakeOwnershipPrivilege = 9,
		SeTcbPrivilege = 7,
		SeUndockPrivilege = 25,
		SeCreateSymbolicLinkPrivilege = 35,
		SeIncreaseWorkingSetPrivilege = 33,
		SeRelabelPrivilege = 32,
		SeTimeZonePrivilege = 34,
		SeTrustedCredManAccessPrivilege = 31,
	}
}
