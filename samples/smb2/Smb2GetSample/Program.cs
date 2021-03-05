using System.Net;
using Titanis;
using Titanis.Cli;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Ntlm;
using Titanis.Security.Spnego;
using Titanis.Smb2;

namespace Smb2GetSample
{
	[Command(HelpText = "Gets a file from a remote computer via SMB")]
	internal class Program : Command
	{
		static async Task Main(string[] args)
			=> RunProgramAsync<Program>(args);

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			// Parameters
			string serverName = "LUMON-FS1";
			IPAddress hostAddress = IPAddress.Parse($"10.66.0.13");
			string shareName = "ADMIN$";
			string shareRelativePath = $"explorer.exe";
			string userName = "milchick";
			string domain = "LUMON";
			string password = "Br3@kr00m!";

			string outputPath = @"fetchedFile";

			ClientCredentialService credService = new ClientCredentialService();
			credService.AddCredential(new ServicePrincipalName(Smb2Client.ServiceClass, serverName), t =>
			{
				// Configure the credentials
				NtlmPasswordCredential cred = new NtlmPasswordCredential(userName, domain, password);
				NtlmClientContext ntlmContext = new NtlmClientContext(cred, true);
				ntlmContext.RequiredCapabilities |= SecurityCapabilities.Integrity;

				SpnegoClientContext negoContext = new SpnegoClientContext();
				negoContext.Contexts.Add(ntlmContext);

				return negoContext;
			});

			// Configure resolver
			var resolver = new DictionaryNameResolver();
			resolver.SetAddress(serverName, new IPAddress[] { hostAddress });

			// Configure the client
			Smb2Client client = new Smb2Client(
				credService,
				nameResolver: resolver
				);

			// Open the file
			using var file = await client.OpenFileReadAsync($@"\\{serverName}\{shareName}\{shareRelativePath}", cancellationToken);
			using var stream = file.GetStream(false);

			// Open the target file
			using var outStream = File.Create(outputPath);

			// Copy the file
			await stream.CopyToAsync(outStream, cancellationToken);

			return 0;
		}
	}
}
