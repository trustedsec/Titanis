using System;

namespace Titanis.Smb2
{
	// [MS-SMB2] § 2.2.13 - SMB2 CREATE Request
	[Flags]
	public enum Smb2ShareAccess : uint
	{
		None = 0,

		Read = 1,
		Write = 2,
		Delete = 4,

		ReadWrite = Read | Write,
		ReadWriteDelete = Read | Write | Delete,

		DefaultDirShare = ReadWriteDelete,
		DefaultRootDirShare = ReadWrite
	}


}
