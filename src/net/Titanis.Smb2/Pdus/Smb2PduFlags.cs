using System;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.1.1 - SMB2 Packet Header - ASYNC
	[Flags]
	enum Smb2PduFlags : uint
	{
		None = 0,

		ServerToRedir = 1,
		AsyncCommand = 2,
		RelatedOperations = 4,
		Signed = 8,
		PriorityMask = 0x70,
		DfsOperations = 0x10000000,
		ReplayOperation = 0x20000000,
	}
}
