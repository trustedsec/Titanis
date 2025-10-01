using Titanis.Cli;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Msrpc.Msdcom;
using Titanis.Msrpc.Mswmi;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Ntlm;

namespace WmiRegistry
{
	[Command(HelpText = "Manipulates the registry of a remote computer via WMI")]
	internal class Program : Command
	{
		static int Main(string[] args)
			=> RunProgramAsync<Program>(args);

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			// Set parameters
			string target = "10.66.0.13";
			string myWorkstationName = "TEST-WKS";
			int myPid = 5151;
			string commandLine = @"C:\Windows\system32\notepad.exe";

			// Create credential dictionary
			ClientCredentialDictionary creds = new ClientCredentialDictionary();
			creds.DefaultCredentialFactory = (spn, caps) =>
			{
				// Set up credentials
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
			rpcClient.DefaultAuthLevel = RpcAuthLevel.PacketPrivacy;

			// Connect to DCOM service
			DcomClient dcom = await DcomClient.ConnectTo(
				target,
				rpcClient,
				cancellationToken);

			// Connect to WMI service
			var wmi = await WmiClient.ConnectTo(myWorkstationName, myPid, dcom, cancellationToken);

			// Open root\default namespace
			var cimv2 = await wmi.OpenNamespace(WmiClient.RootCimV2Namespace, "en-US", cancellationToken);

			// Get the StdRegProv
			var regProv = await cimv2.GetObjectAsync("StdRegProv", cancellationToken);
			dynamic dynRegistry = regProv;

			// Get HKLM\Software\Microsoft\Windows NT\CurrentBuild
			uint hklm = 0x80000002U;
			var currentBuild = (await dynRegistry.GetStringValue(hklm, @"Software\Microsoft\Windows NT\CurrentVersion", "CurrentBuild")).sValue;
			this.WriteMessage($"The Windows build is {currentBuild}.");

			// Create a key
			await dynRegistry.CreateKey(hklm, @"Software\WmiRegistryTest");

			// Set a value
			await dynRegistry.SetStringValue(hklm, @"Software\WmiRegistryTest", "TestValue", "Test value data");

			return 0;
		}
	}
}
