using System;

namespace Titanis.Smb2
{
	// [MS-SMB2] § 2.2.13 - SMB2 CREATE Request
	[Flags]
	public enum Smb2FileCreateOptions : uint
	{
		None = 0,

		Directory = 1,
		WriteThrough = 2,
		Sequential = 4,
		NoIntermediateBuffering = 8,
		SynchronousIoAlert = 0x10,
		SynchronousIoNonalert = 0x20,
		NonDirectory = 0x40,
		CompleteIfOplocked = 0x100,
		NoEaKnowledge = 0x200,
		RandomAccess = 0x800,
		DeleteOnClose = 0x1000,
		OpenByFileId = 0x2000,
		OpenForBackupIntent = 0x4000,
		NoCompression = 0x8000,
		OpenRemoteInstance = 0x400,
		OpenRequiringOplock = 0x10000,
		DisallowExclusive = 0x00020000,
		ReserveOpfilter = 0x00100000,
		OpenReparsePoint = 0x00200000,
		OpenNoRecall = 0x00400000,
		OpenForFreeSpaceQuery = 0x00800000,
	}


}
