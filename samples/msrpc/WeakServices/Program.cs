using ms_lsar;
using ms_scmr;
using System.Net;
using Titanis;
using Titanis.Cli;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.Epm;
using Titanis.Msrpc;
using Titanis.Msrpc.Mslsar;
using Titanis.Msrpc.Msscmr;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Ntlm;
using Titanis.Smb2;
using Titanis.Winterop.Security;

namespace WeakServices
{
	[Command(HelpText = "Checks a remote computer for services with a weak DACL")]
	internal class Program : Command
	{
		static void Main(string[] args)
			=> RunProgramAsync<Program>(args);

		const string UserName = "milchick";
		const string Domain = "LUMON";
		const string Password = "Br3@kr00m!";
		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			// Set parameters
			string targetServer = "10.66.0.13";
			string myWorkstationName = "TEST-WKS";

			// Here are the access rights that are interesting
			const ServiceAccess InterestingAccessRights =
				ServiceAccess.ChangeConfig
				| (ServiceAccess)StandardAccessRights.WriteDac;

			// Maintain a list of what looks boring so we can ignore it later
			WellKnownSid[] uninterestingSids = new WellKnownSid[]
			{
				WellKnownSid.BuiltinAdministrators,
				WellKnownSid.LocalSystem,
				WellKnownSid.LocalAdministrator,
			};
			string[] uninterestingAccountNames = new string[]
			{
				"TrustedInstaller",
			};

			// Create credential dictionary
			ClientCredentialDictionary creds = new ClientCredentialDictionary();
			creds.DefaultCredentialFactory = (spn, caps) =>
			{
				// Set up credentials
				var ntlmContext = new NtlmClientContext(new NtlmPasswordCredential(UserName, Domain, Password), true);
				ntlmContext.RequiredCapabilities |= SecurityCapabilities.Integrity | SecurityCapabilities.Confidentiality;
				ntlmContext.Workstation = myWorkstationName;
				ntlmContext.ClientChannelBindingsUnhashed = new byte[16];
				ntlmContext.TargetSpn = spn;
				ntlmContext.ClientChannelBindingsUnhashed = new byte[16];
				return ntlmContext;
			};
			this.Services.AddService(typeof(IClientCredentialService), creds);

			// Create RPC client
			RpcClient rpcClient = this.CreateRpcClient();

			// Query endpoint for SCM
			var epm = await rpcClient.ConnectTcp<EpmClient>(new DnsEndPoint(targetServer, EpmClient.EPMapperPort), null, cancellationToken);
			var scmEP = await epm.TryMapTcp(RpcInterfaceId.GetForType(typeof(svcctl)), cancellationToken);

			rpcClient.DefaultAuthLevel = RpcAuthLevel.PacketPrivacy;

			// Set up SMB for LSA
			var smbClient = new Smb2Client(creds);

			// Connect to LSA
			LsaClient lsaClient = new LsaClient();
			await rpcClient.ConnectPipe(lsaClient, smbClient, new UncPath(targetServer, Smb2Client.IpcName, "lsarpc"), cancellationToken);
			using var lsap = await lsaClient.OpenPolicy(LsaPolicyAccess.LookupNames, cancellationToken);

			// Connect to SCM
			ScmClient scmClient = new ScmClient();
			await rpcClient.ConnectTcp(scmClient, scmEP, null, cancellationToken);
			using var scm = await scmClient.OpenScm(ScmAccess.Connect | ScmAccess.EnumerateService, cancellationToken);

			// Start looping through services
			var services = await scm.GetServicesAsync(cancellationToken);
			foreach (var serviceInfo in services)
			{
				try
				{
					// Open the service
					using var service = await scm.OpenServiceAsync(serviceInfo.ServiceName, (ServiceAccess)StandardAccessRights.ReadControl, cancellationToken);

					// Query the service DACL
					var sdBytes = await service.QuerySecurityAsync(Titanis.Winterop.Security.SecurityInfo.Dacl, cancellationToken);
					var sd = new SecurityDescriptor(sdBytes);

					// Check each ACE
					foreach (var ace in sd.Dacl.Entries)
					{
						// Only concern ourselves with AccessAllowed entries
						if (ace.AceType == AccessControlEntryType.AccessAllowed)
						{
							SimpleAce simple = (SimpleAce)ace;

							// Does it have any interesting access rights?
							if (0 != ((ServiceAccess)simple.AccessMask & InterestingAccessRights))
							{
								// Convert to a well-known SID and check our list
								var wks = simple.Trustee.AsWellKnownSid();
								if (!uninterestingSids.Contains(wks))
								{
									// Nope, now check the account name
									// This requires a call to the LSA to translate the SID to an account name
									string? accountName = null;
									try
									{
										var result = await lsap.ResolveSidAsync(simple.Trustee, cancellationToken);
										accountName = result.AccountName;
									}
									catch
									{
										accountName = simple.Trustee.ToString();
									}

									// Is the name uninteresting?
									bool isUninterestingName =
										accountName != null
										&& uninterestingAccountNames.Contains(accountName);

									if (!isUninterestingName)
									{
										// Looks like it may be interesting, print it
										this.WriteMessage($"{serviceInfo.ServiceName} can be modified by {accountName}");
									}
								}
							}
						}
					}
				}
				catch
				{

				}
			}

			return 0;
		}
	}
}
