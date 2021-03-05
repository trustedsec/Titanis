using System.ComponentModel;
using System.Net;
using Titanis.Cli;
using Titanis.Security;
using Titanis.Security.Kerberos;

namespace Kerb
{
	/// <task category="Kerberos;Expanding Access">Request a ticket-granting-ticket</task>
	/// <task category="Kerberos">Check whether a user account requires pre-authentication</task>
	/// <task category="Kerberos">Check the encryption types supported for a user account</task>
	/// <task category="Enumeration">Check whether a user name is valid</task>
	[Command]
	[Description("Requests a TGT from the KDC.")]
	[OutputRecordType(typeof(TicketInfo), DefaultOutputStyle = OutputStyle.List)]
	[DetailedHelpText(@"This command sends an AS-REQ to the KDC to request a ticket-granting ticket.

The command line must include either a password or a hex-encoded key that is used both for preauthentication as well as to decrypt the response.  When specifying the NTLM hash, specify just the NTLM portion with no colon.

The provided credential determines the encryption type of the response.  If you provide a password then all encryption types supported by {0} are presented.  To override this, use -EncTypes to specify a list of encryption types to accept.  Note that this only effects the encryption used in the response and not the preauthorization data.

Dates/times are interpreted as local time unless otherwise specified.  If only a time is supplied, the assumed date is today.

Note that the ticket flags and time parameters affect the request sent to the KDC.  However, the KDC is free to ignore them; specifying an option doesn't guarantee that the ticket will have the requested option.

If you don't specify any options for the ticket, {0} uses default values, requesting a ticket that expires 10 hours from now with the options Canonicalize, RenewableOk, Renewable, and Forwardable.  If any options are specified, then no default values are applied and only the options specified are used.
")]
	[Example("Requesting a TGT with a password", "{0} -UserName milchick -Realm LUMON -Password Br3@kr00m! -Kdc 10.66.0.11 -v -OutputFileName milchick-tgt.kirbi -Overwrite")]
	[Example("Requesting a TGT with a password request Rc4Hmac", "{0} -UserName milchick -Realm LUMON -Password Br3@kr00m! -EncTypes Rc4Hmac -Kdc 10.66.0.11 -v -OutputFileName milchick-tgt.kirbi -Overwrite")]
	[Example("Requesting a TGT with a password request AES 128 or AES 256", "{0} -UserName milchick -Realm LUMON -Password Br3@kr00m! -EncTypes Aes128CtsHmacSha1_96, Aes256CtsHmacSha1_96 -Kdc 10.66.0.11 -v -OutputFileName milchick-tgt.kirbi -Overwrite")]
	[Example("Requesting a TGT with an NTLM Hash", "{0} -UserName milchick -NtlmHash B406A01772D0AD225D7B1C67DD81496F -Kdc 10.66.0.11 -Realm LUMON -v -OutputFileName milchick-tgt.kirbi -Overwrite")]
	[Example("Requesting a TGT with an AES 128 key", "{0} -UserName milchick -Aes c5673764957bc2839e367ba7b82f32e1 -Kdc 10.66.0.11 -Realm LUMON -v -OutputFileName milchick-tgt.kirbi -Overwrite")]
	[Example("Requesting a TGT with an AES 256 key", "{0} -UserName milchick -Aes 76332deee4296dcb20200888630755268e605c8576e50ff38db2d8b92351f4e4 -Kdc 10.66.0.11 -Realm LUMON -v -OutputFileName milchick-tgt.kirbi -Overwrite")]
	internal class AsreqCommand : Command
	{
		[Parameter]
		[Mandatory]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Name of user (no domain)")]
		public string UserName { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Password")]
		public string? Password { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("NTLM hash (hex-encoded, no colons)")]
		public HexString? NtlmHash { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("AES 128 key")]
		public HexString? Aes128Key { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("AES 256 key")]
		public HexString? Aes256Key { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Encryption types to request in response")]
		public EType[]? EncTypes { get; set; }

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

		[ParameterGroup]
		public TicketParameterGroup? TicketParamGroup { get; set; }

		[Parameter]
		[Category(ParameterCategories.Output)]
		[Description("Service principal name to request ticket for")]
		public ServicePrincipalName? Spn { get; set; }

		[Parameter]
		[Mandatory]
		[Category(ParameterCategories.Output)]
		[Description("Name of file to write ticket to")]
		public string OutputFileName { get; set; }

		[Parameter]
		[Category(ParameterCategories.Output)]
		[Description("Overwrites the output file, if it exists")]
		public SwitchParam Overwrite { get; set; }

		[Parameter]
		[Category(ParameterCategories.Output)]
		[Description("Appends to the output file, if it exists")]
		public SwitchParam Append { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			int credCount = 0;
			if (this.Password != null) credCount++;
			if (this.NtlmHash != null) credCount++;
			if (this.Aes128Key != null) credCount++;
			if (this.Aes256Key != null) credCount++;

			if (this.EncTypes != null && this.Password == null)
				context.LogError(nameof(EncTypes), "EncTypes may only be specified along with -Password");

			if (credCount != 1)
				context.LogError(new ParameterValidationError(null, "The command line must specify exactly one (1) credential."));

			string outFileName = this.ResolveFsPath(this.OutputFileName);
			if (File.Exists(outFileName) && !(this.Overwrite.IsSet || this.Append.IsSet))
			{
				context.LogError($"Output file '{outFileName}' already exists.  Specify a different file name or use -Overwrite to overwrite it or -Append to append to it.");
			}
		}

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			string outFileName = this.ResolveFsPath(this.OutputFileName);

			KerberosClient krb = new KerberosClient(new SimpleKdcLocator(new DnsEndPoint(this.Kdc, KerberosClient.KdcTcpPort)), callback: new KerberosDiagnosticLogger(this.Log));

			// Load tickets from file, if it exists
			List<TicketInfo> tickets = new List<TicketInfo>();
			if (File.Exists(outFileName) && this.Append.IsSet)
			{
				TicketInfo[] existingTickets = krb.LoadTicketsFromFile(File.ReadAllBytes(outFileName), out _);
				this.WriteVerbose($"Loaded {existingTickets.Length} ticket(s) from {outFileName}.");
				tickets.AddRange(existingTickets);
			}

			KerberosCredential cred =
				(this.Password != null) ? new KerberosPasswordCredential(this.UserName, this.Realm, this.Password)
				: (this.NtlmHash != null) ? new KerberosKeyCredential(this.UserName, this.Realm, EType.Rc4Hmac, this.NtlmHash.Bytes)
				: (this.Aes128Key != null) ? new KerberosKeyCredential(this.UserName, this.Realm, EType.Aes128CtsHmacSha1_96, this.Aes128Key.Bytes)
				: (this.Aes256Key != null) ? new KerberosKeyCredential(this.UserName, this.Realm, EType.Aes256CtsHmacSha1_96, this.Aes256Key.Bytes)
				: throw new SyntaxException("No credential provided");

			TicketParameters ticketParameters = this.TicketParamGroup?.GetTicketParameters(this.Log) ?? krb.GetDefaultTgtOptions();

			var tgt = await krb.RequestTgt(this.Realm, cred, this.Spn, ticketParameters, this.EncTypes, cancellationToken).ConfigureAwait(false);
			this.WriteRecord(tgt);

			tickets.Add(tgt);
			var tgtBytes = krb.ExportTickets(tickets, FormatFromFileName(this.OutputFileName));
			File.WriteAllBytes(outFileName, tgtBytes);


			this.WriteVerbose($"Exported {tickets.Count} ticket(s) to {outFileName}");

			return 0;
		}

		public static KerberosFileFormat FormatFromFileName(string outputFileName)
		{
			var ext = Path.GetExtension(outputFileName);
			if (ext.Equals(".ccache", StringComparison.OrdinalIgnoreCase))
				return KerberosFileFormat.Ccache;
			else //if (ext.Equals(".kirbi", StringComparison.OrdinalIgnoreCase))
				return KerberosFileFormat.Kirbi;
		}
	}
}