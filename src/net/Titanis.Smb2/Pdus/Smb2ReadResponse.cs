using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	sealed class Smb2ReadResponse : Smb2Pdu<Smb2ReadResponseBody>
	{
		internal Memory<byte> buffer;

		internal int BytesRead => this.body.dataLength;

		internal override Smb2Command Command => Smb2Command.Read;

		internal override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;
			ref readonly Smb2ReadResponseBody body = ref reader.ReadReadRespHdr();
			this.body = body;

			var req = this.request as Smb2ReadRequest;
			if (body.dataLength > 0)
			{
				reader.Position = offPdu + body.dataOffset;
				var buffer =
					(req != null) ? req.receiveBuffer.Slice(0, Math.Min(body.dataLength, req.receiveBuffer.Length))
					: new byte[body.dataLength];
				reader.Consume(buffer.Length).CopyTo(buffer.Span);
				this.buffer = buffer;
			}
		}

		protected override ushort ValidBodySize => 17;
		internal override void WriteTo(ByteWriter writer, ref Smb2ReadResponseBody body)
		{
			if (this.buffer.Length > 0)
			{
				body.dataOffset = (byte)(Smb2PduAsyncHeader.StructSize + Smb2ReadResponseBody.StructSize);
				body.dataLength = this.buffer.Length;
			}
			writer.WriteReadRespHdr(this.body);

			if (this.buffer.Length > 0)
				writer.WriteBytes(this.buffer.Span);
		}
	}

	[Flags]
	enum Smb2ReadResponseFlags : uint
	{
		None = 0,
		RdmaTransform = 1
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2ReadResponseBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2ReadResponseBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;

		internal byte dataOffset;
		internal byte reserved;
		internal int dataLength;
		internal uint dataRemaining;
		internal Smb2ReadResponseFlags flags;
	}
}
