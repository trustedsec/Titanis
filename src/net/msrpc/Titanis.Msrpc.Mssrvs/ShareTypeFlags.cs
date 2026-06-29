using System;

namespace Titanis.Msrpc.Mswkst
{
	[Flags]
	public enum ShareType : uint
	{
		Disk = 0,
		PrintQueue = 1,
		Device = 2,
		Ipc = 3,
	}
	[Flags]
	public enum ShareTypeFlags : uint
	{
		Disk = 0,
		PrintQueue = 1,
		Device = 2,
		Ipc = 3,

		TypeMask = 0x03,

		Hidden = 0x80000000,

		Cluster = 0x02000000,
		ScaleOutCluster = 0x04000000,
		DfsCluster = 0x08000000
	}
}