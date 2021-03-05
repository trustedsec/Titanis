using System;
using System.Runtime.InteropServices;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.23.2 Lease Break Notification
	sealed class Smb2LeaseBreak : Smb2OplockBreakBase<Smb2LeaseBreakBody>
	{
		public const ushort LeaseBreakSize = 44;

		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => LeaseBreakSize;

		/// <inheritdoc/>
		public sealed override bool IsAckRequired => 0 != (this.body.flags & Smb2LeaseBreakFlags.AckRequired);
		/// <inheritdoc/>
		internal sealed override Smb2Pdu CreateAck()
		{
			return new Smb2LeaseBreakAck()
			{
				body = new Smb2LeaseBreakAckBody
				{
					flags = 0,
					leaseKey = this.body.leaseKey,
					leaseState = this.body.newLeaseState,
				}
			};
		}


		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.OplockBreak;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadLeaseBreakHeader();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2LeaseBreakBody body)
		{
			writer.Write(this.body);
		}
	}

	// [MS-SMB2] § 2.2.13.2.8
	[Flags]
	public enum Smb2LeaseState : int
	{
		None = 0,
		ReadCaching = 1,
		HandleCaching = 2,
		WriteCaching = 4,
	}

	// [MS-SMB2] § 2.2.23.2
	[Flags]
	enum Smb2LeaseBreakFlags : int
	{
		None = 0,

		AckRequired = 1,
	}

	// [MS-SMB2] § 2.2.23.2
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2LeaseBreakBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2LeaseBreakBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal ushort newEpoch;
		internal Smb2LeaseBreakFlags flags;
		internal Guid leaseKey;
		internal Smb2LeaseState currentLeaseState;
		internal Smb2LeaseState newLeaseState;
		internal int breakReason;
		internal int accessMaskHint;
		internal int shareMaskHint;
	}
}
