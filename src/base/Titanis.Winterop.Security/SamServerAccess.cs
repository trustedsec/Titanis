using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Winterop.Security
{
	[Flags]
	public enum SamServerAccessRights
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
	public enum SamDomainAccessRights : uint
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
	public enum SamGroupAccessRights : uint
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

		MaxAllowed = 0x02000000
	}

	[Flags]
	public enum SamAliasAccessRights : uint
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

		MaxAllowed = 0x02000000
	}

	[Flags]
	public enum SamUserAccessRights : uint
	{
		None = 0,
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
}
