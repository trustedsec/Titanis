using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.Epm;
using Titanis.Msrpc;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Kerberos;
using Titanis.Security.Spnego;
using Titanis.Smb2;

namespace Titanis.Cli
{
	/// <summary>
	/// Base class for commands that use RPC.
	/// </summary>
	/// <remarks>
	/// Implementors should use <see cref="RpcCommand{TClient}"/>.
	/// </remarks>
	public abstract class RpcCommand : Command
	{
		/// <summary>
		/// Gets the type of the RPC interface.
		/// </summary>
		protected abstract Type InterfaceType { get; }

		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public AuthenticationParameters? Authentication { get; set; }

		private NetworkParameters? _netParameters;
		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public NetworkParameters NetParameters
		{
			get => _netParameters;
			set
			{
				_netParameters = value;
				if (value != null) value.Log = Log;
			}
		}

		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public SmbParameters SmbParameters { get; set; }

		[Parameter(0)]
		[Mandatory]
		[Description("RPC server to interact with")]
		public string ServerName { get; set; }

		[Parameter]
		[Description("Uses SP-NEGO for authentication")]
		public SwitchParam Spnego { get; set; }

		[Parameter]
		[Description("Authenticates EP mapper requests")]
		public SwitchParam AuthEpm { get; set; }

		[Parameter]
		[Description("Encrypts EP mappend requests")]
		public SwitchParam EncryptEpm { get; set; }

		[Parameter]
		[Description("Encrypts RPC messages")]
		public SwitchParam EncryptRpc { get; set; }

		[Parameter]
		[Description("If the interface supports named pipes, attempt to connect over the named pipe instead of TCP")]
		public SwitchParam PreferSmb { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			Authentication?.Validate(false, context);
			var hasAuth = this.Authentication.HasAuthInfo;
			if (!hasAuth)
			{
				if (this.AuthEpm.IsSet) context.LogError(nameof(AuthEpm), $"-{nameof(AuthEpm)} requires authentication, but no authentication information is provided.");
				if (this.EncryptRpc.IsSet) context.LogError(nameof(EncryptRpc), $"-{nameof(EncryptEpm)} requires authentication, but no authentication information is provided.");
				if (this.EncryptRpc.IsSet) context.LogError(nameof(EncryptRpc), $"-{nameof(EncryptRpc)} requires authentication, but no authentication information is provided.");
			}

			if (NetParameters.HostAddress.IsNullOrEmpty())
				NetParameters.HostAddress = new string[] { ServerName };

			if (ServerName.StartsWith(@"\\"))
				ServerName = ServerName.Substring(2);

			if (this.PreferSmb.IsSet)
			{
				var svcClient = this.CreateServiceClient();
				if (svcClient.WellKnownPipeName is null)
					context.LogError(nameof(PreferSmb), $"-{nameof(PreferSmb)} specified, but the RPC service doesn't support named pipes.");

				if (this.EncryptRpc.IsSet && !svcClient.SupportsReauthOverNamedPipes)
				{
					this.WriteWarning($"-{nameof(EncryptRpc)} specified, but the RPC service doesn't support encryption over named pipes.  The command will likely fail.");
				}
			}
		}

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			RpcClient rpcClient = this.CreateRpcClient();

			var remoteAddrs = await NetParameters.ResolveAsync(ServerName, cancellationToken).ConfigureAwait(false);

			if (remoteAddrs.IsNullOrEmpty())
			{
				WriteError("No remote addresses to connect to.");
				return -1;
			}

			var remoteAddr = remoteAddrs[0];


			var svcClient = this.CreateServiceClient();
			var credService = this.RequireService<IClientCredentialService>();
			ServicePrincipalName spn = svcClient.GetSpnFor(this.ServerName);

			var port = svcClient.WellKnownTcpPort;
			if (!PreferSmb.IsSet && (port != 0 || svcClient.SupportsDynamicTcp))
			{
				// If the endpoint doesn't have a well-known port, use the EP mapper
				IPEndPoint remoteEP;
				if (port == 0)
				{
					var epm = await rpcClient.ConnectTcp<EpmClient>(
						new IPEndPoint(remoteAddr, EpmClient.EPMapperPort),
						new ServicePrincipalName(ServiceClassNames.Rpc, this.ServerName),
						this.EncryptEpm.IsSet ? RpcAuthLevel.PacketPrivacy : this.AuthEpm.IsSet ? RpcAuthLevel.PacketIntegrity : RpcAuthLevel.None, cancellationToken).ConfigureAwait(false);
					remoteEP = await epm.TryMapTcp(RpcInterfaceId.GetForType(InterfaceType), remoteAddr, cancellationToken).ConfigureAwait(false);
				}
				else
					remoteEP = new IPEndPoint(remoteAddr, port);

				if (remoteEP != null)
				{
					SecurityCapabilities rpcRequiredCaps = SecurityCapabilities.DceStyle;
					if (svcClient.RequiresEncryptionOverTcp || this.EncryptRpc.IsSet)
						rpcRequiredCaps |= SecurityCapabilities.Confidentiality;

					AuthOptions rpcAuthOptions = AuthOptions.None;
					if (Spnego.IsSet)
						rpcAuthOptions |= AuthOptions.PreferSpnego;

					RpcAuthLevel authLevel = (svcClient.RequiresEncryptionOverTcp || EncryptRpc.IsSet) ? RpcAuthLevel.PacketPrivacy : this.Authentication.HasAuthInfo ? RpcAuthLevel.PacketIntegrity : RpcAuthLevel.None;
					rpcClient.DefaultAuthLevel = authLevel;

					await rpcClient.ConnectTcp(svcClient.Proxy, remoteEP, spn, cancellationToken).ConfigureAwait(false);
					return await RunAsync(svcClient, cancellationToken).ConfigureAwait(false);
				}
			}

			var pipeName = svcClient.WellKnownPipeName;
			if (!string.IsNullOrEmpty(pipeName))
			{
				Smb2Client client = CreateSmbClient();
				await using (client)
				{
					RpcAuthLevel authLevel = (this.EncryptRpc.IsSet) ? RpcAuthLevel.PacketPrivacy : RpcAuthLevel.None;
					rpcClient.DefaultAuthLevel = authLevel;

					var pipePath = new UncPath(ServerName, Smb2Client.IpcName, pipeName);
					await rpcClient.ConnectPipe(svcClient, client, pipePath, cancellationToken).ConfigureAwait(false);

					await RunAsync(svcClient, cancellationToken).ConfigureAwait(false);
				}

				return 0;
			}

			throw new NotImplementedException();
		}

		private Smb2Client CreateSmbClient()
		{
			SecurityCapabilities requiredCaps = SecurityCapabilities.Integrity;
			var client = this.SmbParameters.CreateClient();
			return client;
		}

		protected abstract RpcServiceClient CreateServiceClient();
		protected abstract Task<int> RunAsync(RpcServiceClient client, CancellationToken cancellationToken);
	}

	public abstract class RpcCommand<TClient> : RpcCommand
		where TClient : RpcServiceClient, new()
	{
		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			SmbParameters ??= new SmbParameters();
			Authentication ??= new AuthenticationParameters();

			Authentication.Validate(false, context);
			SmbParameters.Validate(context, Authentication);
		}

		protected abstract Task<int> RunAsync(TClient client, CancellationToken cancellationToken);
		protected sealed override Task<int> RunAsync(RpcServiceClient client, CancellationToken cancellationToken)
			=> this.RunAsync((TClient)client, cancellationToken);

		protected sealed override RpcServiceClient CreateServiceClient() => new TClient();
	}
}
