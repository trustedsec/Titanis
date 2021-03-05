using System;
using System.Runtime.InteropServices;
using Titanis.Winterop;

namespace Titanis.Smb2.Pdus
{
	/// <summary>
	/// Union of <see cref="Smb2PduSyncHeader"/> and <see cref="Smb2PduAsyncHeader"/>.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	struct Smb2PduHeaderBuffer
	{
		[FieldOffset(0)]
		internal Smb2PduSyncHeader sync;
		[FieldOffset(0)]
		internal Smb2PduAsyncHeader async;
	}

	[StructLayout(LayoutKind.Explicit)]
	struct Smb2Signature
	{
		[FieldOffset(0)]
		internal Guid value;
		[FieldOffset(0)]
		internal ulong lo;
		[FieldOffset(8)]
		internal ulong hi;
	}

	// [MS-SMB2] § 2.2.1.2 - SMB2 Packet Header - SYNC
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2PduSyncHeader
	{
		public unsafe static short StructSize => (short)sizeof(Smb2PduSyncHeader);
		public const uint ProcessId = 0xFEFF;
		public const uint ValidSignature = 0x424d53fe;
		public const int SigSize = 16;

		internal uint protocolId;
		internal short structSize;
		internal ushort creditCharge;
		internal Ntstatus status;
		internal Smb2Command command;
		internal ushort creditReqResp;
		internal Smb2PduFlags flags;
		internal uint nextCommand;
		internal ulong messageId;
		internal uint processId;
		internal uint treeId;
		internal ulong sessionId;
		internal Smb2Signature signature;

		internal ushort ChannelSequence
		{
			get => (ushort)this.status;
			set => this.status = (Ntstatus)value;
		}
	}
}
