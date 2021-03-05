using System;

namespace Titanis.DceRpc.WireProtocol
{
	[Flags]
	public enum PfcFlags : byte
	{
		None = 0,
		FirstFrag = 1,
		LastFrag = 2,
		PendingCancel = 4,
		Reserved = 8,
		SupportsConcurrent = 0x10,
		DidNotExecute = 0x20,
		Maybe = 0x40,
		ObjectUuid = 0x80,

		SupportHeaderSigning = PendingCancel,
	}
}
