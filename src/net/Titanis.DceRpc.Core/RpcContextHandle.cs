using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc
{
	public sealed class RpcContextHandle
	{
		public RpcContextHandle()
		{

		}
		public RpcContextHandle(NdrContextHandle h)
		{
			this.contextId = h;
		}

		internal NdrContextHandle contextId;
		public bool IsEmpty => this.contextId.IsEmpty;
	}
}
