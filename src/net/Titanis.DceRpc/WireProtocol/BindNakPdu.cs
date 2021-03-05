using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc.WireProtocol
{
	class BindNakPdu
	{
		internal BindNakPduHeader header;

		public BindNakPdu(BindNakPduHeader header)
		{
			this.header = header;
		}

		// TODO: Add "supported versions" if desired
	}
}
