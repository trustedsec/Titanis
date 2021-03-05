using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.29 SMB2 ECHO Response
	sealed class Smb2EchoResponse : Smb2Pdu<Smb2EchoResponseBody>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Echo;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 4;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadEchoRespHdr();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2EchoResponseBody body)
		{
			writer.WriteEchoRespHdr(body);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2EchoResponseBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2EchoResponseBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal ushort reserved;
	}
}
