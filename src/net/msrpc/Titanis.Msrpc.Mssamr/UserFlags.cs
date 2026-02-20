using System;

namespace Titanis.Msrpc.Mssamr
{
    [Flags]
	public enum UserFlags : uint
	{
		None = 0,

		Script = 1,
		AccountDisable = 2,
		HomedirRequired = 8,
		Lockout = 0x10,
		PasswordNotRequired = 0x20,
		PasswordCantChange = 0x40,
		EncryptedTextPasswordAllowed = 0x80,
		TempDuplicateAccount = 0x100,
		NormalAccount = 0x200,
		InterdomainTrustAccount = 0x800,
		WorkstationTrustAccount = 0x1000,
		ServerTrustAccount = 0x2000,
		DontExpirePassword = 0x10000,
		MnsLogonAccount = 0x200000,
		SmartCardRequired = 0x40000,
		TrustedForDelegation = 0x80000,
		NotDelegated = 0x100000,
		UseDesKeyOnly = 0x200000,
		DontRequiredPreauth = 0x400000,
		PasswordExpired = 0x800000,
		TrustedToAuthenticateForDelegation = 0x1000000,
		NoAuthDataRequired = 0x2000000,
		PartialSecretsAccount = 0x4000000,
		UseAesKeys = 0x8000000,
	}
}
