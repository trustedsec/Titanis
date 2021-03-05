using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Mslsar
{
	public class LsaSecret : LsaObject
	{
		internal LsaSecret(LsaClient lsaClient, RpcContextHandle handle)
			: base(lsaClient, handle)
		{
		}

		public Task<SecretInfo> QueryInfo(CancellationToken cancellationToken)
			=> this._lsaClient.GetSecret(this._handle, cancellationToken);
	}
}