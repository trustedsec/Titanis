using System.ComponentModel;
using System.Net;
using Titanis.Cli;
using Titanis.Security.Kerberos;

namespace Kerb
{
	/// <task category="Kerberos">Check whether a user account requires pre-authentication</task>
	/// <task category="Kerberos">Check the encryption types supported for a user account</task>
	/// <task category="Enumeration">Check whether a user name is valid</task>
	[Command]
	[OutputRecordType(typeof(KdcEncryptionTypeInfo))]
	[Description("Gets server time and encryption types (with salts) for a user account.")]
	[DetailedHelpText(@"This command sends an AS-REQ to the KDC for a user and checks the response.  Typically, the KDC response with an error indicating that preauthentication is required along with its time and valid encryption wypes for the specified account.  This command analyzes that error response and prints the information.

If the account does not exist or the realm name is wrong, the KDC returns an error indicating this and does not provide preauthentication info.

If the user exists but does not require preauthentication, the KDC will instead reply with a TGT without providing encryption types.  In that case, use the requesttgt command to analyze the ticket.")]
	internal class GetASInfoCommand : Command
	{
		[Parameter]
		[Mandatory]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Name of user (no domain)")]
		public string UserName { get; set; }

		[Parameter]
		[Mandatory]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Name of realm (domain)")]
		public string Realm { get; set; }

		[Parameter]
		[Mandatory]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Host name or address of KDC")]
		public string Kdc { get; set; }

		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public NetworkParameters NetParameters { get; set; }

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			KerberosClient krb = this.CreateKerberosClient(new SimpleKdcLocator(new DnsEndPoint(this.Kdc, KerberosClient.KdcTcpPort)));

			var asInfo = await krb.GetASInfo(Realm, this.UserName, cancellationToken).ConfigureAwait(false);

			this.WriteMessage($"KDC time: {asInfo.KdcTime:O}");

			this.WriteRecords(asInfo.SupportedEncryptionTypes);

			return 0;
		}
	}
}