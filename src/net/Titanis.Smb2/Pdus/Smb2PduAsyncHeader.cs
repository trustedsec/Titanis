using System;
using System.Runtime.InteropServices;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.1.1 - SMB2 Packet Header - ASYNC
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2PduAsyncHeader
	{
		public unsafe static short StructSize => (short)sizeof(Smb2PduAsyncHeader);
		public const uint ValidSignature = 0x424d53fe;

		internal uint protocolId;
		internal short structSize;
		internal ushort creditCharge;
		internal uint status;
		internal Smb2Command command;
		internal ushort creditResp;
		internal Smb2PduFlags flags;
		internal uint nextCommand;
		internal ulong messageId;
		internal ulong asyncId;
		internal ulong sessionId;
		internal Guid signature;
	}
}
