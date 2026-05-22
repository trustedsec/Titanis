using ms_dcom;
using System.ComponentModel;
using System.Net;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Msrpc.Msdcom;
using Titanis.Net;
using Titanis.Security;

namespace Titanis.Cli.DcomTool;

/// <task category="DCOM;Lateral Movement">Activate a COM object on a remote computer</task>
[Command]
[Description("Activates an object over DCOM")]
public class ActivateCommand : Command, IHaveServerName
{
	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public RpcParameterGroup? RpcParameters { get; set; }

	private string _serverName;
	[Parameter(0)]
	[Mandatory]
	[Description("Name of the server to connect to")]
	public string ServerName { get => _serverName; set => _serverName = value; }

	[ParameterGroup(ParameterGroupOptions.Required)]
	public ActivationParameterGroup ActivationParameters { get; set; }

	public const int WellKnownTcpPort = 135;

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);
		this.RpcParameters.ValidateParameters(context, null, ref this._serverName);
	}

	protected override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		var rpcClient = this.CreateRpcClient();
		this.RpcParameters?.ApplyTo(rpcClient, RpcAuthLevel.PacketIntegrity);

		DcomClient dcom = await DcomClient.ConnectTo(this.ServerName, rpcClient, cancellationToken, callback: new DcomLogger(this.Log));

		var obj = await dcom.Activate<IUnknown>(this.ActivationParameters.Clsid, cancellationToken, fileName: this.ActivationParameters.FileName);

		this.WriteMessage($"Object activated.");

		return 0;
	}
}
