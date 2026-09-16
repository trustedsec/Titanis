using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.DceRpc.Server
{
	public abstract class RpcEndpoint : Runnable
	{

		internal RpcServer? owner;
		public bool IsBound => this.owner != null;

		internal void OnBinding(RpcServer owner)
		{
			if (this.owner != null)
				throw new InvalidOperationException($"The endpoint is already bound and cannot be bound again.");

			this.owner = owner;
		}
	}
}
