using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc.WireProtocol
{
	class FaultPdu
	{
		internal FaultPduHeader hdr;

		public FaultPdu(FaultPduHeader hdr)
		{
			this.hdr = hdr;
		}

		public ReadOnlyMemory<byte> StubData { get; internal set; }
	}
}
