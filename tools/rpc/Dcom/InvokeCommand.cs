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
[DetailedHelpText(@"{0} activates the object with the specified CLSID and attempts to invoke the specified method.  The arguments are not parsed locally and are all passed as strings.  It is up to the server to coerce them to the correct type.  Most implementations rely on OLE automation to do this.

The -MethodName may specify either a method or a property.  If it is a property, the value of the property is retrieved and printed.

If the method is specified as a dot-separated multi-part name, this is interpreted as a property path.  The properties are retrieved one by one.  The last part is interpreted as the actual name of the method to invoke on the resulting object.")]
[Example("Invoke MMC20 ExecuteShellCommand", "{0} LUMON-FS1 -UserName milchick@LUMON -Password Br3@kr00m! 49B2791A-B1AE-4C90-9B8E-E860BA07F889 Document.ActiveView.ExecuteShellCommand \"cmd.exe\" C:\\ \" /c whoami\" \"\"", "The CLSID corresponds to MMC20.Application.  This object is activated, then the properties Document and retrieved ActiveView, and finally ExecuteShellCommand is executed on the ActiveView object.", Tag = "milchickNtlm_Mmc20Exec")]
public class InvokeCommand : Command, IHaveServerName
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
		string methodName = this.MethodName;
		int isep = methodName.LastIndexOf('.');
		if (isep != -1)
		{
			var pathParts = methodName.Substring(0, isep).Split('.', StringSplitOptions.RemoveEmptyEntries);

			foreach (var prop in pathParts)
			{
				this.WriteVerbose($"Getting property '{prop}'");
				var propValue = await obj.InvokeMethod(prop, Array.Empty<string>(), cancellationToken);

				if (propValue is OleAutomationObject oleauto)
					obj = oleauto;
				else
				{
					this.WriteError($"The property '{prop}' did not return an automation object.  It returned a {(prop is null ? "<null>" : prop.GetType().FullName)}' with value '{prop}'.");
				}
			}
			methodName = this.MethodName.Substring(isep + 1);
		}

		var result = await obj.InvokeMethod(methodName, this.Arguments ?? Array.Empty<string>(), cancellationToken);

		if (result != null)
			this.WriteRecord(result);
		else
			this.WriteMessage($"Method invoked and returned <null>.");

		return 0;
	}
}
