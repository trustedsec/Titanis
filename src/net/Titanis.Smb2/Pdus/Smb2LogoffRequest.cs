using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.7 SMB2 LOGOFF Request
	sealed class Smb2LogoffRequest : Smb2Pdu<Smb2LogoffRequestBody>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Logoff;
		/// <inheritdoc/>
		internal sealed override Smb2Priority Priority => Smb2Priority.Logoff;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 4;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadLogoffReqHdr();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2LogoffRequestBody body)
		{
			writer.WriteLogoffReqHdr(body);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2LogoffRequestBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2LogoffRequestBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal ushort reserved;
	}
}
