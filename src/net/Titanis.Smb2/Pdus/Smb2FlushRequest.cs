using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.17 SMB2 FLUSH Request
	sealed class Smb2FlushRequest : Smb2Pdu<Smb2FlushRequestBody>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Flush;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 24;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadFlushReqHdr();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2FlushRequestBody body)
		{
			writer.WriteFlushReqHdr(body);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2FlushRequestBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2FlushRequestBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal ushort reserved;
		internal uint reserved2;
		internal Smb2FileHandle handle;
	}
}
