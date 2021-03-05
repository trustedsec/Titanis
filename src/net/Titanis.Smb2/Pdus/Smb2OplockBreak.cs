using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;
using Titanis.Winterop;

namespace Titanis.Smb2.Pdus
{
	interface ISmb2OplockBreak
	{
		bool IsAckRequired { get; }

	}

	abstract class Smb2OplockBreakBase<TBody> : Smb2Pdu<TBody>, ISmb2OplockBreak
		where TBody : struct, ISmb2PduStruct
	{
		public abstract bool IsAckRequired { get; }
		internal abstract Smb2Pdu CreateAck();
	}


	// [MS-SMB2] § 2.2.23.1 Oplock Break Notification
	sealed class Smb2OplockBreak : Smb2OplockBreakBase<Smb2OplockBreakBody>
	{
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 24;

		/// <inheritdoc/>
		public sealed override bool IsAckRequired => this.body.level > Smb2OplockLevel.None;
		/// <inheritdoc/>
		internal sealed override Smb2Pdu CreateAck()
		{
			return new Smb2OplockBreakAck()
			{
				hdr = new Smb2OplockBreakAckHeader
				{
					level = this.body.level,
					fileId = this.body.fileId,
				}
			};
		}

		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.OplockBreak;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;

			this.body = reader.ReadOplockBreakHeader();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2OplockBreakBody body)
		{
			writer.Write(body);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2OplockBreakBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2OplockBreakBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal Smb2OplockLevel level;
		internal byte reserved;
		internal int reserved2;
		internal Guid fileId;
	}
}
