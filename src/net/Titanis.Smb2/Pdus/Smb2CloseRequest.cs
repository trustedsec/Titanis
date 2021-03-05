using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.15 SMB2 CLOSE Request
	sealed class Smb2CloseRequest : Smb2Pdu<Smb2CloseRequestBody>
	{
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 24;

		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Close;
		/// <inheritdoc/>
		internal sealed override Smb2Priority Priority => Smb2Priority.Close;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadCloseReqHdr();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2CloseRequestBody body)
		{
			writer.WriteCloseReqHdr(body);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2CloseRequestBody: ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2CloseRequestBody);
		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }

		internal ushort structureSize;
		internal Smb2CloseOptions flags;
		internal uint reserved;
		internal Smb2FileHandle handle;
	}
}
