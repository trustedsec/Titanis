using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	sealed class Smb2TreeConnectRequest : Smb2Pdu<Smb2TreeConnectRequestBody>
	{
		internal string path;

		internal override Smb2Command Command => Smb2Command.TreeConnect;
		internal override Smb2Priority Priority => Smb2Priority.TreeConnect;

		internal override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;

			// TODO: Validate header values

			ref readonly Smb2TreeConnectRequestBody body = ref reader.ReadTreeConnectReqHdr();
			this.body = base.body;
			if (body.pathLength > 0)
			{
				reader.Position = offPdu + body.pathOffset;
				this.path = Encoding.Unicode.GetString(reader.ReadBytes(body.pathLength));
			}
		}

		protected override ushort ValidBodySize => 9;
		internal override void WriteTo(ByteWriter writer, ref Smb2TreeConnectRequestBody body)
		{
			if (this.path != null)
			{
				body.pathOffset = (ushort)(Smb2PduSyncHeader.StructSize + Smb2TreeConnectRequestBody.StructSize);
				body.pathLength = (ushort)Encoding.Unicode.GetByteCount(this.path);
			}
			else
			{
				body.pathOffset = 0;
				body.pathLength = 0;
			}

			writer.WriteTreeConnectReqBody(body);
			if (this.path != null)
				writer.WriteStringUni(this.path);
		}
	}

	[Flags]
	enum Smb2TreeConnectReqFlags : ushort
	{
		None = 0,

		ClusterReconnect = 1,
		RedirectToOwner = 2,
		ExtensionsPresent = 4,
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2TreeConnectRequestBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2TreeConnectRequestBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal Smb2TreeConnectReqFlags flags;
		internal ushort pathOffset;
		internal ushort pathLength;
	}
}
