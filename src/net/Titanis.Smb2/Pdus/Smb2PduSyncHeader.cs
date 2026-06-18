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

	[PduStruct]
	[StructLayout(LayoutKind.Explicit)]
	partial struct Smb2Signature
	{
		[PduIgnore]
		[FieldOffset(0)]
		internal Guid value;
		[FieldOffset(0)]
		internal ulong lo;
		[FieldOffset(8)]
		internal ulong hi;
	}


	public enum Smb2ProtocolId : uint
	{
		Smb2 = 0x424d53fe,
		// [MS-SMB2] § 2.2.42.1 SMB2_COMPRESSION_TRANSFORM_HEADER_UNCHAINED
		Compression = 0x424D53FC,
		// [MS-SMB2] § 2.2.41 SMB2 TRANSFORM_HEADER
		Transform = 0x424D53FD,
	}

	// [MS-SMB2] § 2.2.1.2 - SMB2 Packet Header - SYNC
	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	partial struct Smb2PduSyncHeader
	{
		public unsafe static short StructSize => (short)sizeof(Smb2PduSyncHeader);
		public const uint ProcessId = 0xFEFF;
		public const int SigSize = 16;

		internal Smb2ProtocolId protocolId;
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

	enum CompressionFlags : ushort
	{
		None = 0,
		Chained = 1,
	}

	// [MS-SMB2] § 2.2.42.1 SMB2_COMPRESSION_TRANSFORM_HEADER_UNCHAINED
	[PduStruct]
	partial struct Smb2CompressHeaderUnchained
	{
		internal Smb2ProtocolId protocolId;
		internal uint originalCompressedSegmentSize;
		internal CompressionAlgorithm compressionAlgorithm;
		internal CompressionFlags flags;
		internal int offset;
	}

	// [MS-SMB2] § 2.2.42.2 SMB2_COMPRESSION_TRANSFORM_HEADER_CHAINED
	[PduStruct]
	partial struct Smb2CompressHeaderChained
	{
		internal Smb2ProtocolId protocolId;
		internal uint originalCompressedSegmentSize;
		internal Smb2CompressChainedPayloadHeader payloadHeader;
	}

	[PduStruct]
	partial struct Smb2CompressChainedPayloadHeader
	{
		internal CompressionAlgorithm compressionAlgorithm;
		internal CompressionFlags flags;
		internal uint length;
		// internal uint originalPayloadSize;
	}
}
