using System;

namespace Titanis.Winterop
{
	// [MS-FSCC] § 2.6 - File Attributes
	[Flags]
	public enum FileAttributes : uint
	{
		None = 0,

		Archive = 0x20,
		Compressed = 0x800,
		Directory = 0x10,
		Encrypted = 0x4000,
		Hidden = 2,
		Normal = 0x80,
		NotContentIndexed = 0x2000,
		Offline = 0x1000,
		ReadOnly = 0x01,
		ReparsePoint = 0x400,
		SparseFile = 0x200,
		System = 4,
		Temporary = 0x100,
		IntegrityStream = 0x8000,
		NoScrubData = 0x00020000
	}


}
