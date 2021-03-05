using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.30 SMB2 CANCEL Request
	sealed class Smb2CancelRequest : Smb2Pdu<Smb2CancelRequestBody>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Cancel;

		/// <inheritdoc/>
		// TODO: What is the actual priority?
		internal sealed override Smb2Priority Priority => Smb2Priority.Unknown;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 4;

		public ulong AsyncId
		{
			get => this.pduhdrbuf.async.asyncId;
			internal set => this.pduhdrbuf.async.asyncId = value;
		}
		public ulong OriginalMessageId
		{
			get => this.pduhdr.messageId;
			internal set => this.pduhdr.messageId = value;
		}

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadCancelReqHdr();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2CancelRequestBody body)
		{
			writer.Write(this.body);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2CancelRequestBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2CancelRequestBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }

		internal ushort structureSize;
		internal ushort reserved;
	}
}
