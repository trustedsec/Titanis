using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Msscmr
{
	/// <summary>
	/// Represents an object managed by <see cref="ScmClient"/>.
	/// </summary>
	public abstract class ServiceRpcObjectBase : RpcContextObjecBase<ScmClient>
	{
		private protected ServiceRpcObjectBase(RpcContextHandle handle, ScmClient client)
			: base(handle, client)
		{
		}

	}
}
