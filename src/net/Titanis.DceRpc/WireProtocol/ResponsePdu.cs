using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc.WireProtocol
{
	class ResponsePdu
	{
		internal ResponsePduHeader hdr;

		public ResponsePdu(ResponsePduHeader hdr)
		{
			this.hdr = hdr;
		}

		public ReadOnlyMemory<byte> StubData { get; internal set; }
	}
}
