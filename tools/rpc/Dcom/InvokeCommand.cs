using System.ComponentModel;
using System.Net;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Msrpc.Msdcom;
using Titanis.Net;
using Titanis.Security;

namespace Titanis.Cli.DcomTool;

[Command]
[Description("Invokes a method on an OLE automation object over DCOM")]
internal class InvokeCommand : Command
{

	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public AuthenticationParameters Authentication { get; set; }

	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public NetworkParameters NetworkParameters { get; set; }

	[Parameter]
	[Description("Encrypts RPC messages")]
	public SwitchParam EncryptRpc { get; set; }

	[Parameter(0)]
	[Mandatory]
	[Description("Name of the server to connect to")]
	public string ServerName { get; set; }


	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("CLSID of object to activate")]
	public Guid Clsid { get; set; }

	[Parameter(After = nameof(Clsid))]
	[Mandatory]
	[Description("Name of method to invoke")]
	public string MethodName { get; set; }

	[Parameter(After = nameof(MethodName))]
	[Description("Arguments to pass to the method")]
	public string[]? Arguments { get; set; }

	//[Parameter(After = nameof(Clsid))]
	//[Description("IID of automation interface")]
	//public Guid Iid { get; set; }

	public const int WellKnownTcpPort = 135;

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);
		this.Authentication.Validate(true, context, false);
	}

	protected override async Task<int> RunAsync(CancellationToken cancellationToken)
	{

		var rpcClient = this.CreateRpcClient();
		rpcClient.DefaultAuthLevel = EncryptRpc.IsSet ? RpcAuthLevel.PacketPrivacy : RpcAuthLevel.PacketIntegrity;

		DcomClient dcom = await DcomClient.ConnectTo(this.ServerName, rpcClient, cancellationToken, callback: new DcomLogger(this.Log));

		var obj = await dcom.Activate(this.Clsid, cancellationToken);
		var result = await obj.InvokeMethod(this.MethodName, this.Arguments ?? Array.Empty<string>(), cancellationToken);

		if (result != null)
			this.WriteRecord(result);
		else
			this.WriteMessage($"Method invoked and returned <null>.");

		return 0;
	}
}
