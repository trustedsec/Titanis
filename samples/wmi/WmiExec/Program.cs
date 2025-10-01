using System.ComponentModel;
using Titanis.Cli;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Msrpc.Msdcom;
using Titanis.Msrpc.Mswmi;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Ntlm;

namespace WmiExec
{
	[Description("Executes a remote command via WMI")]
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
			rpcClient.DefaultAuthLevel = RpcAuthLevel.PacketPrivacy;

			// Connect to DCOM service
			DcomClient dcom = await DcomClient.ConnectTo(
				target,
				rpcClient,
				cancellationToken);

			// Connect to WMI service
			var wmi = await WmiClient.ConnectTo(myWorkstationName, myPid, dcom, cancellationToken);

			// Open root\cimv2 namespace
			var cimv2 = await wmi.OpenNamespace(WmiClient.RootCimV2Namespace, "en-US", cancellationToken);

			// Get the Win32_Process and related classes
			var processClass = await cimv2.GetObjectAsync("Win32_Process", cancellationToken);

			// Set up parameters
			var processStartupInfoClass = (WmiClassObject)await cimv2.GetObjectAsync("Win32_ProcessStartup", cancellationToken);
			dynamic startupInfo = processStartupInfoClass.Instantiate();
			startupInfo.EnvironmentVariables = new string[] { "MyEnvVar=Value" };

			// Start the process
			dynamic dynProcess = processClass;
			var result = await dynProcess.Create(CommandLine: commandLine, CurrentDirectory: @"C:\", ProcessStartupInformation: startupInfo);

			// Print result
			Console.WriteLine($"Return value={result.ReturnValue}, PID={result.ProcessId}");

			return 0;
		}
	}
}
