using System;

namespace Titanis.Winterop.Security
{
	// [MS-SAMR] § 2.2.1.12 - USER_ACCOUNT Codes
	[Flags]
	public enum SamUserAccountFlags : uint
	{
		None = 0,

		Disabled = 1,
		DirectoryRequired = 2,
		PasswordNotRequired = 4,
		TempDuplicateAccount = 8,
		NormalAccount = 0x10,
		MnsLogonAccount = 0x20,
		InterdomainTrustAccount = 0x40,
		WorkstationTrustAccount = 0x80,
		ServerTrustAccount = 0x100,
		DontExpirePassword = 0x200,
		AutoLocked = 0x400,
		EncryptedTextPasswordAllowed = 0x800,
		SmartCardRequired = 0x1000,
		TrustedForDelegation = 0x2000,
		NotDelegated = 0x4000,
		UseDesKeyOnly = 0x8000,
		DontRequirePreauth = 0x10000,
		PasswordExpired = 0x20000,
		TrustedToAuthenticateForDelegation = 0x40000,
		NoAuthDataRequired = 0x80000,
		PartialSecretsAccount = 0x100000,
		UseAesKeys = 0x200000,
	}
}
