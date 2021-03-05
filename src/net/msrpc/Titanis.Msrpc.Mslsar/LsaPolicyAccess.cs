using System;

namespace Titanis.Msrpc.Mslsar
{
	// [MS-LSAD] § 2.2.1.1.2 ACCESS_MASK for Policy Objects
	[Flags]
	public enum LsaPolicyAccess : uint
	{
		None = 0,

		ViewLocalInfo = 1,
		ViewAuditInfo = 2,
		GetPrivateInfo = 4,
		TrustAdmin = 8,
		CreateAccount = 0x10,
		CreateSecret = 0x20,
		CreatePrivilege = 0x40,
		SetDefaultQuotaLimits = 0x80,
		SetAuditRequirements = 0x100,
		AuditLogAdmin = 0x200,
		ServerAdmin = 0x400,
		LookupNames = 0x800,
		Notification = 0x1000,

		MaxAllowed = 0x02000000,
	}

	// [MS-LSAD] § 2.2.1.1.3 ACCESS_MASK for Account Objects
	[Flags]
	public enum LsaAccountAccess : uint
	{
		None = 0,

		View = 1,
		AdjustPrivileges = 0x02,
		AdjustQuotas = 4,
		AdjustSystemAccess = 8,
	}

	// [MS-LSAD] § 2.2.1.1.4 ACCESS_MASK for Secret Objects
	[Flags]
	public enum LsaSecretAccess : uint
	{
		None = 0,

		SetValue = 1,
		QueryValue = 2
	}

	// [MS-LSAD] § 2.2.1.1.5 ACCESS_MASK for Trusted Domain Objects
	[Flags]
	public enum LsaTrustedDomainAccess : uint
	{
		None = 0,

		QueryDomainName = 1,
		QueryControllers = 2,
		SetControllers = 4,
		QueryPosix = 8,
		SetPosix = 0x10,
		SetAuth = 0x20,
		QueryAuth = 0x40,
	}


}