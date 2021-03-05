using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Msrpc.Mssamr
{
	[Flags]
	public enum SamServerAccess
	{
		None = 0,

		Connect = 1,
		Shutdown = 2,
		Initialize = 4,
		CreateDomain = 8,
		EnumerateDomains = 0x10,
		LookupDomain = 0x20,
		AllAccess = 0xF003F,
		Read = 0x20010,
		Write = 0x2000E,
		Execute = 0x20021,
	}

	[Flags]
	public enum SamDomainAccess : uint
	{
		None = 0,

		ReadPasswordParams = 0x01,
		WritePasswordParams = 0x02,
		ReadOtherParams = 4,
		WriteOtherParams = 8,
		CreateUser = 0x10,
		CreateGroup = 0x20,
		CreateAlias = 0x40,
		GetAliasMembership = 0x80,
		ListAccounts = 0x100,
		Lookup = 0x200,
		AdministerServer = 0x400,
		AllAccess = 0xF07FF,
		Read = 0x20084,
		Write = 0x2047A,
		Execute = 0x20301,
	}

	[Flags]
	public enum SamGroupAccess : uint
	{
		None = 0,
		ReadInfo = 1,
		WriteAccount = 2,
		AddMember = 4,
		RemoveMember = 8,
		ListMembers = 0x10,
		AllAccess = 0xF001F,
		Read = 0x20010,
		Write = 0x2000E,
		Execute = 0x20001,
	}

	[Flags]
	public enum SamAliasAccess : uint
	{
		None = 0,
		AddMember = 1,
		RemoveMember = 2,
		ListMembers = 4,
		ReadInfo = 8,
		WriteAccount = 0x10,
		AllAccess = 0xF001F,
		Read = 0x20004,
		Write = 0x20013,
		Execute = 0x20008,
	}

	[Flags]
	public enum SamUserAccess : uint
	{
		None = 1,
		ReadGeneral = 1,
		ReadPreferences = 2,
		WritePreferences = 4,
		ReadLogon = 8,
		ReadAccount = 0x10,
		WriteAccount = 0x20,
		ChangePassword = 0x40,
		ForcePasswordChange = 0x80,
		ListGroups = 0x100,
		ReadGroupInfo = 0x200,
		WriteGroupInfo = 0x400,
		AllAccess = 0xF07FF,
		Read = 0x2031A,
		Write = 0x20044,
		Execute = 0x20041,

		MaxAllowed = 0x02000000
	}

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

	public enum SamAccountType : uint
	{
		None = 0,

		DomainObject = 0,
		GroupObject = 0x10000000,
		NonSecurityGroupObject = 0x10000001,
		AliasObject = 0x20000000,
		NonSecurityAliasObject = 0x20000001,
		UserObject = 0x30000000,
		MachineAccount = 0x30000001,
		TrustAccount = 0x30000002,
		AppBasicGroup = 0x40000000,
		AppQueryGroup = 0x40000001,
	}

	[Flags]
	public enum SamGroupAttributes : uint
	{
		None = 0,

		Mandatory = 1,
		EnabledByDefault = 2,
		Enabled = 4,
	}

	public enum GroupType : uint
	{
		None = 0,
		AccountGroup = 2,
		ResourceGroup = 4,
		UniversalGroup = 8,
		SecurityEnabled = 0x80000000,
		SecurityAccount = 0x80000002,
		SecurityResource = 0x80000004,
		SecurityUniversal = 0x80000008,
	}

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

	public enum PredefinedRid : uint
	{
		Admin = 0x1F4,
		Guest = 0x1F5,
		Krbtgt = 0x1F6,
		Users = 0x201,
		Computers = 0x203,
		Controllers = 0x204,
		Admins = 0x220,
		ReadOnlyControllers = 0x209,
	}

	[Flags]
	public enum ADAccess : uint
	{
		List = 4,
		ReadProp = 0x10,
		WriteProp = 0x20,
		DeleteTree = 0x40,
		ControlAccess = 0x100,
	}

	public class SamStuff
	{
		public const string InvalidAccountChars = @"""/\[]:|<>+=;?,*";
	}
}
