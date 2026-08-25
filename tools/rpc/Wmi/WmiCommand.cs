using System.ComponentModel;
using System.Net;
using Titanis.Cli.DceRpc;
using Titanis.Msrpc.Msdcom;
using Titanis.Msrpc.Mswmi;
using Titanis.Security;

namespace Titanis.Cli.WmiTool;

internal abstract class WmiCommand : DcomCommand
{
	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		var rpcParams = this.RpcParameters;
		rpcParams.Authentication?.Validate(false, context);
	}


	protected sealed override async Task<int> RunAsync(DcomClient dcom, CancellationToken cancellationToken)
	{
		WmiClient wmi = await WmiClient.ConnectTo(this.RpcParameters.Authentication?.Workstation ?? string.Empty, Random.Shared.Next(1024, 65536) & ~0x03, dcom, cancellationToken);

		return await RunAsync(wmi, cancellationToken).ConfigureAwait(false);
	}

	protected abstract Task<int> RunAsync(WmiClient wmi, CancellationToken cancellationToken);
}
