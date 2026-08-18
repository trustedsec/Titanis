using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.Epm;
using Titanis.Msrpc;
using Titanis.Net;
using Titanis.Security;
using Titanis.Smb2;

namespace Titanis.Cli
{

	public class RpcParameterGroup : ParameterGroupBase, IRpcBinder
	{

		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public AuthenticationParameters? Authentication { get; set; }

		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public NetworkParameters NetParameters { get; set; }

		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public SmbParameters SmbParameters { get; set; }

		[Parameter]
		[Advanced]
		[Description("Time to wait for RPC connections")]
		[Category(ParameterCategories.Rpc)]
		public Duration? RpcConnectTimeout { get; set; }

		[Parameter]
		[Advanced]
		[Description("Time to wait for RPC calls")]
		[Category(ParameterCategories.Rpc)]
		public Duration? RpcCallTimeout { get; set; }

		[Parameter]
		[Advanced]
		[Description("Uses SP-NEGO for authentication")]
		[Category(ParameterCategories.Rpc)]
		public SwitchParam Spnego { get; set; }

		[Parameter]
		[Advanced]
		[Description("Authenticates EP mapper requests")]
		[Category(ParameterCategories.Rpc)]
		public SwitchParam AuthEpm { get; set; }

		[Parameter]
		[Advanced]
		[Description("Encrypts EP mappend requests")]
		[Category(ParameterCategories.Rpc)]
		public SwitchParam EncryptEpm { get; set; }

		[Parameter]
		[Description("Encrypts RPC messages")]
		[Category(ParameterCategories.Rpc)]
		public SwitchParam EncryptRpc { get; set; }

		[Parameter]
		[Description("Offers the NDR transfer syntax")]
		[DefaultValue(true)]
		[Advanced]
		[Category(ParameterCategories.Rpc)]
		public SwitchParam OfferNdr { get; set; }

		[Parameter]
		[Description("Offers the NDR64 transfer syntax")]
		[DefaultValue(true)]
		[Advanced]
		[Category(ParameterCategories.Rpc)]
		public SwitchParam OfferNdr64 { get; set; }

		[Parameter]
		[Description("If the interface supports named pipes, attempt to connect over the named pipe instead of TCP")]
		[Category(ParameterCategories.Rpc)]
		public SwitchParam PreferSmb { get; set; }

		protected override void Initialize(IServiceContainer services)
		{
			base.Initialize(services);
			services.AddService(typeof(IRpcBinder), this);
		}

		public void ValidateParameters(ParameterValidationContext context, RpcServiceClient svcClient, ref string? serverName)
		{
			this.NetParameters?.ValidateParameters(context);
			this.Authentication?.Validate(false, context);
			var hasAuth = this.Authentication.HasAuthInfo;
			if (!hasAuth)
			{
				if (this.AuthEpm.IsSet) context.LogError(nameof(AuthEpm), $"-{nameof(AuthEpm)} requires authentication, but no authentication information is provided.");
				if (this.EncryptEpm.IsSet) context.LogError(nameof(EncryptEpm), $"-{nameof(EncryptEpm)} requires authentication, but no authentication information is provided.");
				if (this.EncryptRpc.IsSet) context.LogError(nameof(EncryptRpc), $"-{nameof(EncryptRpc)} requires authentication, but no authentication information is provided.");
			}

			if (NetParameters.HostAddress.IsNullOrEmpty())
				NetParameters.HostAddress = new string[] { serverName };

			if (serverName.StartsWith(@"\\"))
				serverName = serverName.Substring(2);

			if (this.PreferSmb.IsSet)
			{
				if (svcClient.WellKnownPipeName is null)
					context.LogError(nameof(PreferSmb), $"-{nameof(PreferSmb)} specified, but the RPC service doesn't support named pipes.");

				if (this.EncryptRpc.IsSet && !svcClient.SupportsReauthOverNamedPipes)
				{
					this.Log?.WriteWarning($"-{nameof(EncryptRpc)} specified, but the RPC service doesn't support encryption over named pipes.  The command will likely fail.");
				}
			}

			this.SmbParameters.Validate(context, Authentication);
		}

		public void ApplyTo(RpcClient rpcClient, RpcAuthLevel minAuthLevel = RpcAuthLevel.Connect)
		{
			ArgumentNullException.ThrowIfNull(rpcClient);

			if (this.RpcConnectTimeout != null)
				rpcClient.ConnectTimeout = this.RpcConnectTimeout.TimeSpan;
			if (this.RpcCallTimeout != null)
				rpcClient.DefaultCallTimeout = this.RpcCallTimeout.TimeSpan;

			if (this.OfferNdr.IsSpecified)
				rpcClient.OfferNdr = this.OfferNdr.IsSet;
			if (this.OfferNdr64.IsSpecified)
				rpcClient.OfferNdr64 = this.OfferNdr64.IsSet;

			RpcAuthLevel authLevel;
			if (this.EncryptRpc.IsSet)
				authLevel = RpcAuthLevel.PacketPrivacy;
			else
				authLevel = minAuthLevel;

			rpcClient.DefaultAuthLevel = authLevel;
		}

		private Smb2Client CreateSmbClient()
		{
			SecurityCapabilities requiredCaps = SecurityCapabilities.Integrity;
			var client = this.SmbParameters.CreateClient();
			return client;
		}

		public async Task<RpcBindInfo> BindServiceClient(
			RpcServiceClient svcClient,
			string serverName,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(svcClient);

			RpcClient rpcClient = this.Services.CreateRpcClient();
			this.ApplyTo(rpcClient);
			var net = this.NetParameters;

			var credService = this.Services.RequireService<IClientCredentialService>();
			ServicePrincipalName spn = svcClient.GetSpnFor(serverName);

			IPAddress.TryParse(serverName, out var remoteAddr);

			var port = svcClient.WellKnownTcpPort;
			if (!this.PreferSmb.IsSet && (port != 0 || svcClient.SupportsDynamicTcp))
			{
				// If the endpoint doesn't have a well-known port, use the EP mapper
				EndPoint? remoteEP;
				if (port == 0)
				{
					var epm = await rpcClient.ConnectTcp<EpmClient>(
						new DnsEndPoint(serverName, EpmClient.EPMapperPort),
						new ServicePrincipalName(PrincipalNameType.ServiceInstance, ServiceClassNames.Rpc, serverName),
						this.EncryptEpm.IsSet ? RpcAuthLevel.PacketPrivacy : this.AuthEpm.IsSet ? RpcAuthLevel.PacketIntegrity : RpcAuthLevel.None, cancellationToken).ConfigureAwait(false);
					remoteEP = await epm.TryMapTcp(RpcInterfaceId.GetForType(svcClient.Proxy.InterfaceType), remoteAddr, cancellationToken).ConfigureAwait(false);
				}
				else
					remoteEP = new DnsEndPoint(serverName, port);

				if (remoteEP != null)
				{
					SecurityCapabilities rpcRequiredCaps = SecurityCapabilities.DceStyle | SecurityCapabilities.MutualAuthentication | SecurityCapabilities.SequenceDetection | SecurityCapabilities.ReplayDetection;
					if (svcClient.RequiresEncryptionOverTcp || this.EncryptRpc.IsSet)
						rpcRequiredCaps |= SecurityCapabilities.Confidentiality;

					AuthOptions rpcAuthOptions = AuthOptions.None;
					if (Spnego.IsSet)
						rpcAuthOptions |= AuthOptions.PreferSpnego;

					RpcAuthLevel authLevel = (svcClient.RequiresEncryptionOverTcp || EncryptRpc.IsSet) ? RpcAuthLevel.PacketPrivacy : this.Authentication.HasAuthInfo ? RpcAuthLevel.PacketIntegrity : RpcAuthLevel.None;
					rpcClient.DefaultAuthLevel = (RpcAuthLevel)Math.Max((int)rpcClient.DefaultAuthLevel, (int)authLevel);

					await rpcClient.ConnectTcp(svcClient.Proxy, remoteEP, spn, cancellationToken).ConfigureAwait(false);
					return new RpcBindInfo();
				}
			}

			var pipeName = svcClient.WellKnownPipeName;
			if (!string.IsNullOrEmpty(pipeName))
			{
				var smbClient = CreateSmbClient();

				RpcAuthLevel authLevel = (this.EncryptRpc.IsSet) ? RpcAuthLevel.PacketPrivacy : RpcAuthLevel.None;
				rpcClient.DefaultAuthLevel = authLevel;

				var pipePath = new UncPath(serverName, Smb2Client.IpcName, pipeName);
				await rpcClient.ConnectPipe(svcClient, smbClient, pipePath, cancellationToken).ConfigureAwait(false);

				return new RpcBindInfo(smbClient);
			}

			throw new NotImplementedException();
		}
	}
}
