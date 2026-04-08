using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Client;
using Titanis.Smb2;

namespace Titanis.Msrpc
{
	public record struct RpcBindInfo(Smb2Client? SmbClient);
	public interface IRpcBinder
	{
		Task<RpcBindInfo> BindServiceClient(
			RpcServiceClient svcClient,
			string serverName,
			CancellationToken cancellationToken);
	}
}
