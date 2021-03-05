using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	sealed class Smb2TreeDisconnectResponse : Smb2Pdu<Smb2LogoffRequestBody>
	{
		internal override Smb2Command Command => Smb2Command.TreeDisconnect;
		internal override Smb2Priority Priority => Smb2Priority.TreeDisconnect;

		internal override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadLogoffReqHdr();
		}

		protected override ushort ValidBodySize => 4;
		internal override void WriteTo(ByteWriter writer, ref Smb2LogoffRequestBody body)
		{
			writer.WriteLogoffReqHdr(body);
		}
	}
}
