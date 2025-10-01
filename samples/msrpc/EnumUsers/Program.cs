using ms_samr;
using System.Net;
using Titanis.Cli;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.Epm;
using Titanis.Msrpc.Mssamr;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Ntlm;

namespace EnumUsers
{
	[Command(HelpText = "Enumerates users on a remote system")]
	internal class Program : Command
	{
		static void Main(string[] args)
			=> RunProgramAsync<Program>(args);

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			// Set parameters
			string target = "10.66.0.13";
			string myWorkstationName = "TEST-WKS";

			// Create credentials
			var credService = new ClientCredentialDictionary();
			credService.DefaultCredentialFactory = (s, c) =>
			{
				var ntlmContext = new NtlmClientContext(new NtlmPasswordCredential("milchick", "LUMON", "Br3@kr00m!"), true)
				{
					Workstation = myWorkstationName,
					ClientChannelBindingsUnhashed = new byte[16],
					TargetSpn = new ServicePrincipalName(ServiceClassNames.HostU, target)
				};
				ntlmContext.RequiredCapabilities |= SecurityCapabilities.Integrity | SecurityCapabilities.Confidentiality;
				return ntlmContext;
			};
			this.Services.AddService(typeof(IClientCredentialService), credService);

			// Create RPC client
			RpcClient rpcClient = this.CreateRpcClient();
			rpcClient.DefaultAuthLevel = RpcAuthLevel.PacketPrivacy;

			var entry = Dns.GetHostEntry(target);

			// Query endpoint
			var hostAddress = entry.AddressList[0];
			var epm = await rpcClient.ConnectTcp<EpmClient>(new IPEndPoint(hostAddress, EpmClient.EPMapperPort), null, cancellationToken);
			var remoteEP = await epm.TryMapTcp(RpcInterfaceId.GetForType(typeof(samr)), cancellationToken);

			SamClient sam = new SamClient();
			await rpcClient.ConnectTcp(sam, remoteEP, null, cancellationToken);
			using var db = await sam.Connect(SamServerAccess.Connect | SamServerAccess.EnumerateDomains | SamServerAccess.LookupDomain, target, cancellationToken);
			var doms = await db.GetDomains(cancellationToken);
			foreach (var domInfo in doms)
			{
				using var dom = await db.OpenDomainAsync(domInfo.Name, SamDomainAccess.ListAccounts, cancellationToken);
				var users = await dom.EnumUsers(cancellationToken);
				foreach (var user in users)
				{
					this.WriteRecord(user.Name);
				}
			}

			return 0;
		}
	}
}
