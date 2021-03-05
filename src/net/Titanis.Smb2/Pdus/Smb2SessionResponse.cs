using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.6 SMB2 SESSION_SETUP Response
	sealed class Smb2SessionResponse : Smb2Pdu<Smb2SessionResponseBody>
	{
		internal byte[] secToken;
		internal Memory<byte> pduBytes;

		internal override Smb2Command Command => Smb2Command.SessionSetup;
		internal override Smb2Priority Priority => Smb2Priority.SessionSetup;

		internal override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader hdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;

			// TODO: Validate header values

			ref readonly Smb2SessionResponseBody body = ref reader.ReadSessionRespBody();
			this.body = body;
			if (body.secBufferLength > 0)
			{
				reader.Position = offPdu + body.secBufferOffset;
				this.secToken = reader.ReadBytes(body.secBufferLength);
			}
		}

		protected override ushort ValidBodySize => 9;
		internal override void WriteTo(ByteWriter writer, ref Smb2SessionResponseBody body)
		{
			if (this.secToken != null)
			{
				body.secBufferOffset = (ushort)(Smb2PduSyncHeader.StructSize + Smb2SessionRequestBody.StructSize);
				body.secBufferLength = (ushort)this.secToken.Length;
			}
			else
			{
				body.secBufferOffset = 0;
				body.secBufferLength = 0;
			}

			writer.WriteSessionRespHdr(body);
			if (this.secToken != null)
				writer.WriteBytes(this.secToken);
		}
	}

	[Flags]
	enum Smb2SessionFlags : ushort
	{
		None = 0,

		IsGuest = 1,
		IsNull = 2,
		EncryptData = 4
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2SessionResponseBody:ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2SessionResponseBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal Smb2SessionFlags flags;
		internal ushort secBufferOffset;
		internal ushort secBufferLength;
	}
}
