using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	sealed class Smb2SetInfoResponse : Smb2Pdu<Smb2SetInfoResponseBody>
	{
		internal override Smb2Command Command => Smb2Command.SetInfo;

		internal override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadSmb2SetInfoResponseHeader();
		}

		protected override ushort ValidBodySize => 2;
		internal override void WriteTo(ByteWriter writer, ref Smb2SetInfoResponseBody body)
		{
			writer.Write(body);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2SetInfoResponseBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2SetInfoResponseBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
	}
}
