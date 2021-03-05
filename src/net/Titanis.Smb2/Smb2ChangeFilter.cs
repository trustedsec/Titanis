using System;

namespace Titanis.Smb2
{
	[Flags]
	public enum Smb2ChangeFilter : uint
	{
		None = 0,

		FileName = 1,
		DirName = 2,
		Attributes = 4,
		Size = 8,
		LastWrite = 0x10,
		LastAccess = 0x20,
		Creation = 0x40,
		ExtendedAttributes = 0x80,
		Security = 0x100,
		StreamName = 0x200,
		StreamSize = 0x400,
		StreamWrite = 0x800,

		All = 0x0FFF,
	}
}
