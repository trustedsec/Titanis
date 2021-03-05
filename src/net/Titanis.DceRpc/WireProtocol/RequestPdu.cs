using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc.WireProtocol
{
	class RequestPdu
	{
		internal readonly RequestPduHeader header;

		public RequestPdu(RequestPduHeader header)
		{
			this.header = header;
		}

		public Guid ObjectId { get; internal set; }
		public ReadOnlyMemory<byte> StubData { get; internal set; }
	}
}
