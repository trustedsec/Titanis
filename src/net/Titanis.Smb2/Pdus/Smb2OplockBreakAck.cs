using System;
using System.Runtime.InteropServices;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.24.1
	sealed class Smb2OplockBreakAck : Smb2Pdu
	{
		internal Smb2OplockBreakAckHeader hdr;

		internal override Smb2Command Command => Smb2Command.OplockBreak;

		internal override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.hdr = reader.ReadOplockBreakAckHeader();
		}

		internal override void WriteTo(ByteWriter writer)
		{
			this.hdr.structSize = 24;
			writer.Write(this.hdr);
		}
	}

	// [MS-SMB2] § 2.2.24.1
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2OplockBreakAckHeader
	{
		public unsafe static int StructSize => sizeof(Smb2LeaseBreakAckBody);

		internal ushort structSize;
		internal Smb2OplockLevel level;
		internal byte reserved;
		internal int reserved2;
		internal Guid fileId;
	}
}
