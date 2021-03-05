using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.34 SMB2 QUERY_DIRECTORY Response
	sealed class Smb2QueryDirResponse : Smb2Pdu<Smb2QueryDirResponseBody>
	{
		internal byte[] buf;

		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.QueryDirectory;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 9;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader hdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;

			// TODO: Validate header values

			ref readonly Smb2QueryDirResponseBody body = ref reader.ReadQueryDirRespHdr();
			this.body = body;
			if (body.outputBufferLength > 0)
			{
				reader.Position = offPdu + body.outputBufferOffset;
				this.buf = reader.ReadBytes((int)body.outputBufferLength);
			}
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2QueryDirResponseBody body)
		{
			if (this.buf != null)
			{
				body.outputBufferOffset = (ushort)(Smb2PduSyncHeader.StructSize + Smb2QueryDirResponseBody.StructSize);
				body.outputBufferLength = (ushort)this.buf.Length;
			}
			else
			{
				body.outputBufferOffset = 0;
				body.outputBufferLength = 0;
			}

			writer.WriteQueryDirRespHdr(body);
			if (this.buf != null)
				writer.WriteBytes(this.buf);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2QueryDirResponseBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2QueryDirResponseBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal ushort outputBufferOffset;
		internal uint outputBufferLength;
	}


}
