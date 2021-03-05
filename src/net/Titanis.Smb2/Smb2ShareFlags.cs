using System;

namespace Titanis.Smb2
{
	[Flags]
	public enum Smb2ShareFlags : uint
	{
		None = 0,

		ManualCaching = 0,
		AutoCaching = 0x10,
		VdoCaching = 0x20,
		NoCaching = 0x30,
		Dfs = 1,
		DfsRoot = 2,
		RestrictExclusiveOpens = 0x100,
		ForceSharedDelete = 0x200,
		AllowNamespaceCaching = 0x400,
		AccessBasedDirectoryEnum = 0x800,
		ForceLevel2Oplock = 0x1000,
		EnableHashV1 = 0x2000,
		EnableHashV2 = 0x4000,
		EncryptData = 0x8000,
		IdentityRemoting = 0x00040000
	}
}
