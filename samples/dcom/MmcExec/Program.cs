using System.Runtime.CompilerServices;
using Titanis;
using Titanis.Cli;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Msrpc.Msdcom;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Ntlm;

namespace MmcExec
{
	[Command(HelpText = "Executes a remote command using the MMC interface")]
	internal class Program : Command
	{
		static async Task Main(string[] args)
			=> RunProgramAsync<Program>(args);

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			Guid clsidMmc = new Guid("49B2791A-B1AE-4C90-9B8E-E860BA07F889");

			// Set parameters
			string target = "10.66.0.13";
			string myWorkstationName = "TEST-WKS";
			int myPid = 5151;
			string commandLine = @"C:\Windows\system32\notepad.exe";

			// Create credentials
			var ntlmContext = new NtlmClientContext(new NtlmPasswordCredential("milchick", "LUMON", "Br3@kr00m!"), true);
			ntlmContext.RequiredCapabilities |= SecurityCapabilities.Integrity | SecurityCapabilities.Confidentiality;
			ntlmContext.Workstation = myWorkstationName;
			ntlmContext.ClientChannelBindingsUnhashed = new byte[16];
			ntlmContext.TargetSpn = new ServicePrincipalName(ServiceClassNames.RestrictedKrbHost, target);

			// Create RPC client
			RpcClient rpcClient = new RpcClient(Singleton.SingleInstance<PlatformSocketService>())
			{
				DefaultAuthLevel = RpcAuthLevel.PacketPrivacy
			};

			// Connect to DCOM service
			DcomClient dcom = await DcomClient.ConnectTo(
				target,
				rpcClient,
				ntlmContext,
				cancellationToken);

			// Start MMC
			dynamic mmc = await dcom.Activate(clsidMmc, cancellationToken);
			var doc = mmc.Document.ActiveView.ExecuteShellCommand(@"C:\Windows\system32\netsh.exe", @"C:\", "", "");

			return 0;
		}
	}
}
