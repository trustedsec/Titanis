using System;

namespace Titanis.Msrpc.Mslsar
{
	// [MS-LSAD] § 2.2.1.1.5 ACCESS_MASK for Trusted Domain Objects
	[Flags]
	public enum PolicySystemAccessMode : uint
	{
		NoAccess = 0,

		Interactive = 1,
		Network = 2,
		Batch = 4,
		Service = 0x10,
		DenyInteractive = 0x40,
		DenyNetwork = 0x80,
		DenyBatch = 0x100,
		DenyService = 0x200,
		RemoteInteractive = 0x400,
		DenyRemoteInteractive = 0x800,
		All = 0xFF7,
		AllNT4 = 0x37,
	}


}