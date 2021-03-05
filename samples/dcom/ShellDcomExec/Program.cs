using System.Runtime.CompilerServices;
using Titanis.Cli;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Msrpc.Msdcom;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Ntlm;

namespace ShellExec
{
	[Command(HelpText = "Executes a remote command via the Windows Shell interface")]
	internal class Program : Command
	{
		static async Task Main(string[] args)
			=> RunProgramAsync<Program>(args);

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			//Guid clsidShell = new Guid("{9BA05972-F6A8-11CF-A442-00A0C90A8F39}");
			Guid clsidShell = new Guid("c08afd90-f2a1-11d1-8455-00a0c91f3880");

			// Set parameters
			string target = "10.73.0.101";
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
			RpcClient rpcClient = new RpcClient()
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
			dynamic shell = await dcom.Activate(clsidShell, cancellationToken);
			shell.Document.Application.ShellExecute("netsh.exe");

			return 0;
		}
	}

	class CustomSyncContext : SynchronizationContext
	{
	}
}
