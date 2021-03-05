using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	sealed class Smb2WriteResponse : Smb2Pdu<Smb2WriteResponseBody>
	{
		internal override Smb2Command Command => Smb2Command.Write;

		internal override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadWriteRespBody();
		}

		protected override ushort ValidBodySize => 17;
		internal override void WriteTo(ByteWriter writer, ref Smb2WriteResponseBody body)
		{
			writer.WriteWriteRespHdr(body);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2WriteResponseBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2WriteResponseBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;

		internal ushort reserved;
		internal uint count;
		internal uint remaining;
		internal ushort channelInfoOffset;
		internal ushort channelInfoLength;
	}
}
