using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using Titanis.Cli;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.Epm;
using Titanis.Msrpc.Msdcom;
using Titanis.Msrpc.Mswmi;
using Titanis.Security;
using Titanis.Smb2;

namespace Wmi;
internal abstract class WmiCommand : Command
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


	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		Authentication?.Validate(false, context, Log);

		if (this.NetworkParameters.HostAddress.IsNullOrEmpty())
			this.NetworkParameters.HostAddress = new string[] { ServerName };

		if (ServerName.StartsWith(@"\\"))
			ServerName = ServerName.Substring(2);
	}


	protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		const string TargetServiceClass = ServiceClassNames.RestrictedKrbHost;

		RpcAuthLevel authLevel = EncryptRpc.IsSet ? RpcAuthLevel.PacketPrivacy : RpcAuthLevel.PacketIntegrity;
		RpcClient rpcClient = new RpcClient()
		{
			DefaultAuthLevel = authLevel
		};
		var remoteAddrs = await this.NetworkParameters.ResolveAsync(ServerName, cancellationToken).ConfigureAwait(false);

		if (remoteAddrs.IsNullOrEmpty())
		{
			WriteError("No remote addresses to connect to.");
			return -1;
		}

		var remoteAddr = remoteAddrs[0];

		SecurityCapabilities rpcRequiredCaps = 0;
		AuthOptions rpcAuthOptions = AuthOptions.None;
		if (EncryptRpc.IsSet)
			rpcRequiredCaps |= SecurityCapabilities.Confidentiality;
		ServicePrincipalName targetSpn = new(TargetServiceClass.ToUpper(), ServerName);
		AuthClientContext? rpcAuthContext = Authentication?.CreateAuthContext(
			targetSpn,
			rpcAuthOptions,
			rpcRequiredCaps | SecurityCapabilities.DceStyle,
			Log
			);

		if (rpcAuthContext != null)
			rpcClient.DefaultAuthLevel =
				EncryptRpc.IsSet ? RpcAuthLevel.PacketPrivacy
				: RpcAuthLevel.PacketIntegrity;

		const int port = 135;

		// If the endpoint doesn't have a well-known port, use the EP mapper
		IPEndPoint remoteEP = new IPEndPoint(remoteAddr, port);

		var authContext = this.Authentication.CreateAuthContext(targetSpn, AuthOptions.None, SecurityCapabilities.Integrity | SecurityCapabilities.Confidentiality, this.Log);
		DcomClient dcom = await DcomClient.ConnectTo(this.ServerName, rpcClient, authContext, cancellationToken, null, callback: new DcomLogger(this.Log));
		WmiClient wmi = await WmiClient.ConnectTo(this.Authentication.Workstation, Random.Shared.Next(1024, 65536) & ~0x03, dcom, cancellationToken);

		return await RunAsync(wmi, cancellationToken).ConfigureAwait(false);
	}

	protected abstract Task<int> RunAsync(WmiClient wmi, CancellationToken cancellationToken);
}
