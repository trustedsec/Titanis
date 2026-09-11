using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Msrpc.Msdcom;
using Titanis.Net;

namespace Titanis.Cli.DceRpc;

public abstract class DcomCommand : RpcCommandBase
{

	protected sealed override async Task<int> RunOverServer(string serverName, bool wantsSmb, CancellationToken cancellationToken)
	{
		var credService = this.RequireService<IClientCredentialService>();

		var rpcClient = this.CreateRpcClient();
		this.RpcParameters.ApplyTo(rpcClient, RpcAuthLevel.PacketIntegrity);

		DcomClient dcom = await DcomClient.ConnectTo(serverName, rpcClient, cancellationToken, callback: new DcomLogger(Log)).ConfigureAwait(false);
		return await this.RunAsync(dcom, cancellationToken).ConfigureAwait(false);
	}

	protected abstract Task<int> RunAsync(DcomClient dcom, CancellationToken cancellationToken);
}
