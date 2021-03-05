using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	sealed class Smb2TreeConnectResponse : Smb2Pdu<Smb2TreeConnectResponseBody>
	{
		internal string path;

		internal override Smb2Command Command => Smb2Command.TreeConnect;
		internal override Smb2Priority Priority => Smb2Priority.TreeConnect;

		internal override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;

			// TODO: Validate header values

			this.body = reader.ReadTreeConnectRespHdr();
		}

		protected override ushort ValidBodySize => 16;
		internal override void WriteTo(ByteWriter writer, ref Smb2TreeConnectResponseBody body)
		{
			writer.WriteTreeConnectRespHdr(body);
			if (this.path != null)
				writer.WriteStringUni(this.path);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2ShareInfo
	{
		internal Smb2ShareType shareType;
		internal byte reserved;
		internal Smb2ShareFlags flags;
		internal Smb2ShareCaps caps;
		internal uint maximalAccess;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2TreeConnectResponseBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2TreeConnectResponseBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal Smb2ShareInfo shareInfo;
	}
}
