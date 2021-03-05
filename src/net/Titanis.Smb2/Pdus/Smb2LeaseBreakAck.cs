using System;
using System.Runtime.InteropServices;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.24.2 Lease Break Acknowledgment
	sealed class Smb2LeaseBreakAck : Smb2Pdu<Smb2LeaseBreakAckBody>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.OplockBreak;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 36;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadLeaseBreakAckHeader();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2LeaseBreakAckBody body)
		{
			writer.Write(body);
		}
	}

	// [MS-SMB2] § 2.2.23.2
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2LeaseBreakAckBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2LeaseBreakAckBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal ushort reserved;
		internal int flags;
		internal Guid leaseKey;
		internal Smb2LeaseState leaseState;
		internal long leaseDuration;
	}
}
