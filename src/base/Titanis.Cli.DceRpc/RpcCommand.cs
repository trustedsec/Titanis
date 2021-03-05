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
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Kerberos;
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
		/// Gets the name of the target service class.
		/// </summary>
		protected virtual string? TargetServiceClass => ServiceClassNames.Host;
		/// <summary>
		/// Gets the name of the pipe hosting the endpoint, or <see langword="null"/> if
		/// there is not a well-known pipe endpoint.
		/// </summary>
		protected virtual string? WellKnownPipeName => null;
		/// <summary>
		/// Gets the well-known TCP port, or <c>0</c> if there is no well-known port.
		/// </summary>
		/// <remarks>
		/// If this value is non-zero, the implementation will use the specified port
		/// rather than discovering it with the endpoint mapper.
		/// </remarks>
		protected virtual int WellKnownPort => 0;
		/// <summary>
		/// Gets a value indicating whether the protocol supports binding over dynamic TCP.
		/// </summary>
		/// <remarks>
		/// If this value is <see langword="true"/>, the implementation may opt to
		/// use the endpoint mapper to discover the TCP port to connect to.
		/// </remarks>
		protected virtual bool SupportsDynamicTcp => false;
		/// <summary>
		/// Gets the type of the RPC interface.
		/// </summary>
		protected abstract Type InterfaceType { get; }

		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public AuthenticationParameters? Authentication { get; set; }

		private NetworkParameters? _netParameters;
		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public NetworkParameters? NetParameters
		{
			get => _netParameters;
			set
			{
				_netParameters = value;
				if (value != null) value.Log = Log;
			}
		}

		[ParameterGroup]
		public SmbParameters? SmbParameters { get; set; }

		[Parameter(0)]
		[Mandatory]
		[Description("RPC server to interact with")]
		public string ServerName { get; set; }

		[Parameter]
		[Description("Uses SP-NEGO for authentication")]
		public SwitchParam Spnego { get; set; }

		[Parameter]
		[Description("Encrypts RPC messages")]
		public SwitchParam EncryptRpc { get; set; }

		[Parameter]
		[Description("If the interface supports named pipes, attempt to connect over the named pipe instead of TCP")]
		public SwitchParam PreferSmb { get; set; }

		/// <summary>
		/// Determines whether to negotiate another authentication context over named pipes.
		/// </summary>
		protected virtual bool ReauthOverNamedPipes => this.EncryptRpc.IsSet;

		/// <summary>
		/// Gets a value indicating whether encryption is required.
		/// </summary>
		protected virtual bool RequiresEncryption => false;

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			Authentication?.Validate(false, context, Log);

			if (NetParameters.HostAddress.IsNullOrEmpty())
				NetParameters.HostAddress = new string[] { ServerName };

			if (ServerName.StartsWith(@"\\"))
				ServerName = ServerName.Substring(2);
		}

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			RpcAuthLevel authLevel = (this.RequiresEncryption || EncryptRpc.IsSet) ? RpcAuthLevel.PacketPrivacy : RpcAuthLevel.PacketIntegrity;
			RpcClient rpcClient = new RpcClient()
			{
				DefaultAuthLevel = authLevel
			};
			var remoteAddrs = await NetParameters.ResolveAsync(ServerName, cancellationToken).ConfigureAwait(false);

			if (remoteAddrs.IsNullOrEmpty())
			{
				WriteError("No remote addresses to connect to.");
				return -1;
			}

			var remoteAddr = remoteAddrs[0];

			SecurityCapabilities rpcRequiredCaps = 0;
			AuthOptions rpcAuthOptions = AuthOptions.None;
			if (Spnego.IsSet)
				rpcAuthOptions |= AuthOptions.PreferSpnego;
			if (this.RequiresEncryption || EncryptRpc.IsSet)
				rpcRequiredCaps |= SecurityCapabilities.Confidentiality;
			AuthClientContext? rpcAuthContext = Authentication?.CreateAuthContext(
				new ServicePrincipalName(TargetServiceClass.ToUpper(), ServerName),
				rpcAuthOptions,
				rpcRequiredCaps | SecurityCapabilities.DceStyle,
				Log
				);

			if (rpcAuthContext != null)
				rpcClient.DefaultAuthLevel = authLevel;

			var port = WellKnownPort;
			if (!PreferSmb.IsSet && (port != 0 || SupportsDynamicTcp))
			{
				// If the endpoint doesn't have a well-known port, use the EP mapper
				IPEndPoint remoteEP;
				if (port == 0)
				{
					var epm = await rpcClient.ConnectTcp<EpmClient>(new IPEndPoint(remoteAddr, EpmClient.EPMapperPort), cancellationToken).ConfigureAwait(false);
					remoteEP = await epm.TryMapTcp(RpcInterfaceId.GetForType(InterfaceType), remoteAddr, cancellationToken).ConfigureAwait(false);
				}
				else
					remoteEP = new IPEndPoint(remoteAddr, port);

				var svc = this.CreateServiceClient();
				await rpcClient.ConnectTcp(svc, remoteEP, cancellationToken, rpcAuthContext).ConfigureAwait(false);
				return await RunAsync(svc, cancellationToken).ConfigureAwait(false);
			}

			var pipeName = WellKnownPipeName;
			if (!string.IsNullOrEmpty(pipeName))
			{
				Smb2Client client = CreateSmbClient();
				await using (client)
				{
					var unc = new UncPath(ServerName, Smb2Client.TcpPort, Smb2Client.IpcName, null);
					// Connect to IPC$
					var share = await client.GetShare(unc, cancellationToken).ConfigureAwait(false);

					// Open the RPC pipe
					var pipe = await share.OpenPipeAsync(WellKnownPipeName, cancellationToken).ConfigureAwait(false);
					await using (pipe)
					{
						var stream = pipe.GetStream(false);
						await using (stream.ConfigureAwait(false))
						{
							// Bind to the pipe steram
							var binding = rpcClient.BindTo(stream);

							// Bind the service object
							var svcClient = this.CreateServiceClient();
							if (!this.ReauthOverNamedPipes)
							{
								rpcAuthContext = null;
								authLevel = RpcAuthLevel.Default;
							}

							await svcClient.BindToAsync(binding, false, rpcAuthContext, authLevel, cancellationToken).ConfigureAwait(false);

							await RunAsync(svcClient, cancellationToken).ConfigureAwait(false);
						}
					}

					return 0;
				}
			}

			throw new NotImplementedException();
		}

		private Smb2Client CreateSmbClient()
		{
			SecurityCapabilities requiredCaps = SecurityCapabilities.Integrity;
			var client = new Smb2Client(Authentication.GetCredentialServiceFor(new ServicePrincipalName(ServiceClassNames.Cifs, ServerName), requiredCaps, Log), nameResolver: NetParameters, traceCallback: new Smb2Logger(Log));
			this.SmbParameters?.ConfigureClient(client);
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

			Authentication.Validate(false, context, Log);
			SmbParameters.Validate(context, Authentication);
		}

		protected abstract Task<int> RunAsync(TClient client, CancellationToken cancellationToken);
		protected sealed override Task<int> RunAsync(RpcServiceClient client, CancellationToken cancellationToken)
			=> this.RunAsync((TClient)client, cancellationToken);

		protected sealed override RpcServiceClient CreateServiceClient() => new TClient();
	}
}
