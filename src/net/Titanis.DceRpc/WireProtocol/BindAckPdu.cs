using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc.WireProtocol
{
	class BindAckPdu
	{
		internal BindAckPduHeader header;
		internal PortAny secondaryAddress;
		internal PresContextResult[] contextResults;

		public BindAckPdu()
		{

		}

		public BindAckPdu(BindAckPduHeader header, PortAny secondaryAddress)
		{
			this.header = header;
			this.secondaryAddress = secondaryAddress;
		}
	}
}
