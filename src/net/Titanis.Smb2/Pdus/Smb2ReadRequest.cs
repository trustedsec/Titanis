using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.19 SMB2 READ Request
	sealed class Smb2ReadRequest : Smb2Pdu<Smb2ReadRequestBody>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Read;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 49;
		/// <inheritdoc/>
		internal sealed override Smb2Priority Priority => Smb2Priority.Read;
		/// <inheritdoc/>
		internal sealed override int ResponsePayloadSize => this.body.length;

		internal Memory<byte> receiveBuffer;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadReadReqHdr();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2ReadRequestBody body)
		{
			writer.WriteReadReqHdr(body);
			// HACK: Sent by Windows, without it STATUS_INVALID_PARAMETER
			writer.Consume(1);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2ReadRequestBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2ReadRequestBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;

		internal byte padding;
		internal Smb2ReadOptions options;
		internal int length;
		internal long offset;
		internal Smb2FileHandle handle;
		internal int minCount;
		internal uint channel;
		internal int remainingBytes;
		internal ushort readChannelInfoOffset;
		internal ushort readChannelInfoLength;
	}
}
