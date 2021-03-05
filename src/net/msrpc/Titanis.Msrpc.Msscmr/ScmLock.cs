using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Msscmr
{
	public sealed class ScmLock : ServiceRpcObjectBase
	{
		public ScmLock(RpcContextHandle handle, ScmClient client) : base(handle, client)
		{
		}

		protected sealed override Task CloseAsync(CancellationToken cancellationToken)
			=> this.client.UnlockScm(this.handle, cancellationToken);
	}
}