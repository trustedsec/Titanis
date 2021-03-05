using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.18 SMB2 FLUSH Response
	sealed class Smb2FlushResponse : Smb2Pdu<Smb2FlushResponseBody>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Flush;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 4;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadFlushRespHdr();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2FlushResponseBody body)
		{
			writer.WriteFlushRespHdr(body);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2FlushResponseBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2FlushResponseBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal ushort reserved;
	}
}
