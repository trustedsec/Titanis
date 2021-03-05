using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	sealed class Smb2WriteRequest : Smb2Pdu<Smb2WriteRequestBody>
	{
		internal ReadOnlyMemory<byte> buffer;

		internal override Smb2Command Command => Smb2Command.Write;
		/// <inheritdoc/>
		internal sealed override int SendPayloadSize => this.buffer.Length;

		internal override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;
			ref readonly Smb2WriteRequestBody body = ref reader.ReadWriteReqHdr();
			this.body = body;

			if (body.dataLength > 0)
			{
				reader.Position = offPdu + body.dataOffset;
				this.buffer = reader.ReadBytes((int)body.dataLength);
			}
		}

		protected override ushort ValidBodySize => 49;
		internal override void WriteTo(ByteWriter writer, ref Smb2WriteRequestBody body)
		{
			if (this.buffer.Length > 0)
			{
				body.dataOffset = (ushort)(Smb2PduAsyncHeader.StructSize + Smb2WriteRequestBody.StructSize);
				body.dataLength = (uint)this.buffer.Length;
			}
			writer.WriteWriteReqHdr(body);

			if (this.buffer.Length > 0)
				writer.WriteBytes(this.buffer.Span);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2WriteRequestBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2WriteRequestBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal ushort dataOffset;

		internal uint dataLength;
		internal ulong writeOffset;
		internal Smb2FileHandle fileHandle;
		internal uint channel;
		internal uint remainingBytes;
		internal ushort channelInfoOffset;
		internal ushort channelInfoLength;
		internal Smb2WriteOptions flags;
	}
}
