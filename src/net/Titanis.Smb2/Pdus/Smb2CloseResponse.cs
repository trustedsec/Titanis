using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.16 SMB2 CLOSE Response
	sealed class Smb2CloseResponse : Smb2Pdu<Smb2CloseResponseBody>
	{
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 60;

		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Close;
		/// <inheritdoc/>
		internal sealed override Smb2Priority Priority => Smb2Priority.Close;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadCloseRespHdr();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2CloseResponseBody body)
		{
			writer.WriteCloseRespHdr(body);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2CloseResponseBody: ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2CloseResponseBody);
		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }

		internal ushort structureSize;
		internal Smb2CloseOptions flags;
		internal uint reserved;

		internal Smb2OpenFileAttributes attrs;
	}
}
