using System.ComponentModel;
using System.Net;
using Titanis.Security;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb
{
	/// <task category="Kerberos;Enumeration">Check whether a user account requires pre-authentication</task>
	/// <task category="Kerberos;Enumeration">Check the encryption types supported for a user account</task>
	/// <task category="Enumeration">Check whether a user name is valid</task>
	[Command]
	[OutputRecordType(typeof(KdcEncryptionTypeInfo))]
	[Description("Gets server time and encryption types (with salts) for a user account.")]
	[DetailedHelpText(@"This command sends an AS-REQ to the KDC for a user and checks the response.  Typically, the KDC response with an error indicating that preauthentication is required along with its time and valid encryption wypes for the specified account.  This command analyzes that error response and prints the information.

If the account does not exist or the realm name is wrong, the KDC returns an error indicating this and does not provide preauthentication info.

If the user exists but does not require preauthentication, the KDC will instead reply with a TGT without providing encryption types.  In that case, use the requesttgt command to analyze the ticket.")]
	[Example("Get AS info for milchick", "{0} milchick@LUMON 10.66.0.11")]
	public class GetASInfoCommand : Command, IHaveServerName
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
		[Parameter(0)]
		[Mandatory]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Name of user (no domain)")]
		public UserPrincipalName UserName { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Name of realm (domain)")]
		public string? Realm { get; set; }

		[Parameter]
		[Description("ETypes to request")]
		public EType[]? EncTypes { get; set; }

		[Parameter(10)]
		[Mandatory]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Host name or address of KDC")]
		public string Kdc { get; set; }
		string? IHaveServerName.ServerName => this.Kdc;

		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public NetworkParameters NetParameters { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			var realm = this.Realm ?? this.UserName.Realm;
			if (realm is null)
				context.LogError(new ParameterValidationError(nameof(Realm), $"Realm must be specified either as -{nameof(Realm)} or along with the user name."));
		}

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			KerberosClient krb = this.CreateKerberosClient(new SimpleKdcLocator(new DnsEndPoint(this.Kdc, KerberosClient.KdcTcpPort)));

			var asInfo = await krb.GetASInfo(Realm ?? this.UserName.Realm, this.UserName.UserName, this.EncTypes, cancellationToken).ConfigureAwait(false);

			this.WriteMessage($"KDC time: {asInfo.KdcTime:O}");
			this.WriteRecords(asInfo.SupportedEncryptionTypes);

			return 0;
		}
	}
}