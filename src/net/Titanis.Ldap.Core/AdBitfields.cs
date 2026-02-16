using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	// [MS-ADTS] § 2.2.9
	/// <summary>
	/// Specifies the short names of <c>SearchFlags</c>.
	/// </summary>
	[Flags]
	public enum SearchShortFlags
	{
		IX = 1,
		PI = 2,
		AR = 4,
		PR = 8,
		CP = 0x10,
		TP = 0x20,
		ST = 0x40,
		CF = 0x80,
		NV = 0x100,
		RO = 0x200,
		XL = 0x400,
		BO = 0x800,
		SE = 0x1000
	}
	// [MS-ADTS] § 2.2.9
	/// <summary>
	/// Specifies the long names of <c>SearchFlags</c>.
	/// </summary>
	[Flags]
	public enum SearchFlags
	{
		None = 0,
		AttributeIndex = 1,
		PdntAttributeIndex = 2,
		Anr = 4,
		PreserveOnDelete = 8,
		Copy = 0x10,
		TupleIndex = 0x20,
		SubtreeAttributeIndex = 0x40,
		Confidential = 0x80,
		NeverValueAudit = 0x100,
		RodcFilteredAttribute = 0x200,
		ExtendedLinkTracking = 0x400,
		BaseOnly = 0x800,
		PartitionSecret = 0x1000
	}

	// [MS-ADTS] § 2.2.10
	/// <summary>
	/// Specifies the short names of <c>SystemFlags</c>.
	/// </summary>
	[Flags]
	public enum SystemShortFlags : uint
	{
		NR = 1,
		PS = 2,
		CS = 4,
		OP = 8,
		BS = 0x10,
		RD = 0x20,
		DE = 0x02000000,
		DM = 0x04000000,
		DR = 0x08000000,
		AL = 0x10000000,
		AM = 0x20000000,
		AR = 0x40000000,
		DD = 0x80000000,
	}

	// [MS-ADTS] § 2.2.10
	/// <summary>
	/// Specifies the long names of <c>SearchFlags</c>.
	/// </summary>
	[Flags]
	public enum SystemFlags : uint
	{
		None = 0,

		NotReplicated = 1,
		PartialAttributeSet = 2,
		Constructed = 4,
		Operational = 8,
		SchemaBaseObject = 0x10,
		Rdn = 0x20,
		DisallowMoveOnDelete = 0x02000000,
		DomainDisallowMove = 0x04000000,
		DomainDisallowRename = 0x08000000,
		ConfigAllowLimitedMove = 0x10000000,
		ConfigAllowMove = 0x20000000,
		ConfigAllowRename = 0x40000000,
		DisallowDelete = 0x80000000,
	}

	// [MS-ADTS] § 2.2.11
	[Flags]
	public enum SchemaFlags : uint
	{
		None = 0,
		Critical = 1
	}
	// [MS-ADTS] § 2.2.11
	[Flags]
	public enum SchemaShortFlags : uint
	{
		CR = 1
	}

	// [MS-ADTS] § 2.2.12
	[Flags]
	public enum GroupTypeFlags : uint
	{
		None = 0,

		Builtin = 1,
		Global = 2,
		DomainLocal = 4,
		Universal = 8,
		AppBasic = 0x10,
		Query = 0x20,
		SecurityEnabled = 0x80000000
	}

	// [MS-ADTS] § 2.2.13
	public enum GroupSecurityFlags
	{
		None = 0,
		GroupOwner = 8,
		UseForDenyOnly = 0x10,
		SecurityPrivilege = 8,
		TakeOwnershipPrivilege = 9,
		RestorePrivilege = 0x12,
		DebugPrivilege = 0x14,
		EnableDelegationPrivilege = 0x1B
	}

	// [MS-ADTS] § 2.2.16
	[Flags]
	public enum UserAccountControlShortFlags
	{
		None = 0,

		D = 2,
		HR = 8,
		L = 0x10,
		NR = 0x20,
		CC = 0x40,
		ET = 0x80,
		N = 0x200,
		ID = 0x800,
		WT = 0x1000,
		ST = 0x2000,
		DP = 0x1_0000,
		SR = 0x4_0000,
		TD = 0x8_0000,
		ND = 0x10_0000,
		DK = 0x20_0000,
		DR = 0x40_0000,
		PE = 0x80_0000,
		TA = 0x100_0000,
		NA = 0x200_0000,
		PS = 0x400_0000,
	}

	// [MS-ADTS] § 2.2.16
	[Flags]
	public enum UserAccountControlFlags
	{
		None = 0,

		Disabled = 2,
		HomeDirRequired = 8,
		LockedOut = 0x10,
		PasswordNotRequired = 0x20,
		CantChangePassword = 0x40,
		EncryptedCleartextPassword = 0x80,
		NormalAccount = 0x200,
		InterdomainTrustAccount = 0x800,
		WorkstationTrustAccount = 0x1000,
		ServerTrustAccount = 0x2000,
		PasswordNeverExpires = 0x1_0000,
		SmartcardRequired = 0x4_0000,
		TrustedForDelegation = 0x8_0000,
		NotDelegated = 0x10_0000,
		UseDesKeyOnly = 0x20_0000,
		NoPreauthRequired = 0x40_0000,
		PasswordExpired = 0x80_0000,
		TrustedForS4U = 0x100_0000,
		NoAuthDataRequired = 0x200_0000,
		PartialSecretsAccount = 0x400_0000,
	}

	// [MS-ADTS]  6.1.1.1.1
	[Flags]
	public enum InstanceTypeFlags
	{
		None = 0,
		NcHead = 1,
		Uninstantiated = 2,
		Writable = 4,
		Above = 8,
		Coming = 0x10,
		Going = 0x20,
	}
	// [MS-ADTS]  6.1.1.1.1
	[Flags]
	public enum InstanceTypeShortFlags
	{
		H = 1,
		U = 2,
		W = 4,
		A = 8,
		C = 0x10,
		G = 0x20,
	}

	// [MS-ADTS] § 6.1.1.2.2.1.2.1.1 nTDSDSA Object
	[Flags]
	public enum NtdsaOptionsFlags
	{
		None = 0,

		IsGc = 1,
		DisableInboundReplication = 2,
		DisableOutboundReplication = 4,
		DisableTranslation = 8,
		DisableSpnRegistration = 0x10,
	}
	// [MS-ADTS] § 6.1.1.2.2.1.2.1.1 nTDSDSA Object
	[Flags]
	public enum NtdsaOptionsShortFlags
	{
		GC = 1,
		DI = 2,
		DO = 4,
		DNX = 8,
		DS = 0x10,
	}
}
