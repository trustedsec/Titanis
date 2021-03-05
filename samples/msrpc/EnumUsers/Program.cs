using ms_samr;
using System.Net;
using Titanis.Cli;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.Epm;
using Titanis.Msrpc.Mssamr;
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
			var ntlmContext = new NtlmClientContext(new NtlmPasswordCredential("milchick", "LUMON", "Br3@kr00m!"), true)
			{
				Workstation = myWorkstationName,
				ClientChannelBindingsUnhashed = new byte[16],
				TargetSpn = new ServicePrincipalName(ServiceClassNames.Host, target)
			};
			ntlmContext.RequiredCapabilities |= SecurityCapabilities.Integrity | SecurityCapabilities.Confidentiality;

			// Create RPC client
			RpcClient rpcClient = new RpcClient()
			{
				DefaultAuthLevel = RpcAuthLevel.PacketPrivacy,
			};

			var entry = Dns.GetHostEntry(target);

			// Query endpoint
			var hostAddress = entry.AddressList[0];
			var epm = await rpcClient.ConnectTcp<EpmClient>(new IPEndPoint(hostAddress, EpmClient.EPMapperPort), cancellationToken);
			var remoteEP = await epm.TryMapTcp(RpcInterfaceId.GetForType(typeof(samr)), hostAddress, cancellationToken);

			SamClient sam = new SamClient();
			await rpcClient.ConnectTcp(sam, remoteEP, cancellationToken, ntlmContext);
			using var db = await sam.Connect(SamServerAccess.Connect | SamServerAccess.EnumerateDomains, target, cancellationToken);
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

			throw new NotImplementedException();
		}
	}
}
