using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.28 SMB2 ECHO Request
	sealed class Smb2EchoRequest : Smb2Pdu<Smb2EchoRequestBody>
	{
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 4;

		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Echo;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadEchoReqHdr();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2EchoRequestBody body)
		{
			writer.WriteEchoReqHdr(body);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2EchoRequestBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2EchoRequestBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal ushort reserved;
	}
}
