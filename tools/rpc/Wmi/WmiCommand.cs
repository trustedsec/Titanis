using System.ComponentModel;
using System.Net;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Msrpc.Msdcom;
using Titanis.Msrpc.Mswmi;
using Titanis.Net;
using Titanis.Security;

namespace Titanis.Cli.WmiTool;

internal abstract class WmiCommand : Command, IHaveServerName
{
	//[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	//public AuthenticationParameters Authentication { get; set; }

	//[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	//public NetworkParameters NetworkParameters { get; set; }

	//[Parameter]
	//[Description("Encrypts RPC messages")]
	//public SwitchParam EncryptRpc { get; set; }

	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public RpcParameterGroup RpcParameters { get; set; }

	[Parameter(0)]
	[Mandatory]
	[Description("Name of the server to connect to")]
	public string ServerName { get; set; }


	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		var rpcParams = this.RpcParameters;
		rpcParams.Authentication?.Validate(false, context);

		var netParams = rpcParams.NetParameters;

		if (netParams.HostAddress.IsNullOrEmpty())
			netParams.HostAddress = new string[] { ServerName };

		if (ServerName.StartsWith(@"\\"))
			ServerName = ServerName.Substring(2);
	}


	protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		var rpcParams = this.RpcParameters;

		var remoteAddrs = await rpcParams.NetParameters.ResolveAsync(ServerName, cancellationToken).ConfigureAwait(false);

		if (remoteAddrs.IsNullOrEmpty())
		{
			WriteError("No remote addresses to connect to.");
			return -1;
		}

		var remoteAddr = remoteAddrs[0];

		var credService = this.RequireService<IClientCredentialService>();

		var rpcClient = this.CreateRpcClient();
		this.RpcParameters.ApplyTo(rpcClient, RpcAuthLevel.PacketIntegrity);
		//rpcClient.DefaultCallTimeout = TimeSpan.FromMinutes(1);

		// If the endpoint doesn't have a well-known port, use the EP mapper
		IPEndPoint remoteEP = new IPEndPoint(remoteAddr, WmiClient.WellKnownTcpPort);

		DcomClient dcom = await DcomClient.ConnectTo(this.ServerName, rpcClient, cancellationToken, callback: new DcomLogger(this.Log));
		WmiClient wmi = await WmiClient.ConnectTo(rpcParams.Authentication?.Workstation ?? string.Empty, Random.Shared.Next(1024, 65536) & ~0x03, dcom, cancellationToken);

		return await RunAsync(wmi, cancellationToken).ConfigureAwait(false);
	}

	protected abstract Task<int> RunAsync(WmiClient wmi, CancellationToken cancellationToken);
}
