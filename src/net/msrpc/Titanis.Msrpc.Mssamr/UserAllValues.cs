using System;

namespace Titanis.Msrpc.Mssamr
{
    [Flags]
	public enum UserAllValues : uint
	{
		None = 0,
		Username = 1,
		Fullname = 2,
		UserId = 4,
		PrimaryGroupId = 8,
		AdminComment = 0x10,
		UserComment = 0x20,
		HomeDirectory = 0x40,
		HomeDirectoryDrive = 0x80,
		ScriptPath = 0x100,
		ProfilePath = 0x200,
		Workstations = 0x400,
		LastLogon = 0x800,
		LastLogoff = 0x1000,
		LogonHours = 0x2000,
		BadPasswordConut = 0x4000,
		LogonConut = 0x8000,
		PasswordCanChange = 0x10000,
		PasswordMustChange = 0x20000,
		PasswordLastSet = 0x40000,
		AccountExpires = 0x80000,
		UserAccountControl = 0x100000,
		Parameters = 0x200000,
		CountryCode = 0x400000,
		CodePage = 0x800000,
		NTPasswordPresent = 0x1000000,
		LMPasswordPresent = 0x2000000,
		PrivateData = 0x4000000,
		PasswordExpired = 0x8000000,
		SecurityDescriptor = 0x10000000,
		UndefinedMask = 0xC0000000,
	}
}
