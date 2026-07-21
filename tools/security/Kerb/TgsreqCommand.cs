using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Titanis.Security;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb
{
	/// <task category="Kerberos;Expanding Access">Request a ticket for a service</task>
	/// <task category="Kerberos;Expanding Access">Get ticket hash for hash cracking</task>
	[Command]
	[Description("Requests a ticket from the KDC.")]
	[DetailedHelpText(@"This command sends a TGS-REQ to the KDC to request a ticket.

The target may either be specified as a service principal name of the form <class>/<instance> or as the name of the account itself.  For machine accounts, the $ is optional.  For instance, instead of host/LUMON-FS1, you may simply use LUMON-FS1$ or LUMON-FS1

The command line must include either a password or a hex-encoded key that is used both for pre-authentication as well as to decrypt the response.  When specifying the NTLM hash, specify just the NTLM portion with no colon.

By default, all supported encryption types are sent in the request.  To limit this, use the -EncTypes parameter to specify which encryption types to request from the server.")]
	[Example("Requesting a ticket for SMB", "{0} -Kdc 10.66.0.11 -Tgt milchick-tgt.kirbi cifs/LUMON-FS1 -OutputFile milchick-LUMON-FS1.kirbi")]
	[Example("Requesting a ticket for LUMON-FS1", "{0} -Kdc 10.66.0.11 -Tgt milchick-tgt.kirbi LUMON-FS1 -OutputFile milchick-LUMON-FS1.kirbi")]
	[Example("Requesting a ticket for SMB and Host", "{0} -Kdc 10.66.0.11 -Tgt milchick-tgt.kirbi cifs/LUMON-FS1, HOST/LUMON-FS1 -OutputFile milchick-LUMON-FS1.kirbi")]
	[Example("Requesting a U2U ticket", "{0} -Kdc 10.66.0.11 -v -Tgt allentown-tgt.kirbi -Overwrite -U2u allentown-tgt.kirbi -OutputFileName allentown-u2u.kirbi host/allentown")]
	[Example("Requesting a U2U ticket and extracting NTLM hash", "{0} -Kdc 10.66.0.11 -v -Tgt allentown-tgt.kirbi -Overwrite -U2u allentown-tgt.kirbi -OutputFileName allentown-u2u.kirbi host/allentown -AsrepKey 82d4ab5873cbfda126e00c28edb5bd97b6451aa06a291d85173e6fc4ed4aacee")]
	public class RequestTicketCommand : KdcRequestCommand
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
		[Parameter(After = nameof(Kdc))]
		[Mandatory]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("SPN(s) to request ticket(s) for")]
		public SecurityPrincipalName[] Target { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Name of file containing a ticket-granting ticket (.kirbi or ccache)")]
		[KerberosTicketFileSpec(true)]
		public FileSpec? Tgt { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Encryption types to request in response")]
		public EType[]? EncTypes { get; set; }

		[ParameterGroup]
		public TicketParameterGroup? TicketParamGroup { get; set; }

		[Parameter]
		[Description("Requests a forwarded ticket")]
		public SwitchParam Forwarded { get; set; }

		[Parameter]
		[Description("Realm of the KDC")]
		public string? Realm { get; set; }

		[Parameter]
		[Description("Name of user to impersonate with S4U")]
		public UserPrincipalName? S4UserName { get; set; }

		[Parameter]
		[Description("Name of file containing a certificate of a user to impersonate with S4U")]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[CertificateFileSpec(true)]
		public FileSpec? S4UserCert { get; set; }

		[Parameter]
		[Description("Name of service account with S4U2proxy")]
		public SecurityPrincipalName? S4ProxyService { get; set; }

		[Parameter]
		[Description("Name of file containing U2U ticket")]
		[KerberosTicketFileSpec(true)]
		public FileSpec? U2uTicket { get; set; }

		[Parameter]
		[Description("Password for service account (for decrypting authorization data)")]
		public string? ServicePassword { get; set; }

		[Parameter]
		[Category(ParameterCategories.TicketAuthorizationData)]
		[Description("Salt for service account (for decrypting authorization data)")]
		public string? ServiceSalt { get; set; }

		[Parameter]
		[Category(ParameterCategories.TicketAuthorizationData)]
		[Description("Encryption key from AS-REP (for decryption NTLM hash)")]
		public HexString? AsrepKey { get; set; }

		private X509Certificate2? _s4uCert;
		private KerberosKeyCredentialBase? _serviceCredential;
		private byte[]? _serviceSalt;
		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);
			if ((this.Tgt == null) && (this.TicketCache == null))
				context.LogError($"Either -{nameof(Tgt)} or -{nameof(TicketCache)} must be specified.");
			if (this.S4UserCert != null)
			{
				try
				{
					this.Log?.WriteMessage(LogMessage.Verbose(null, $"Loading certificate file {this.S4UserCert}"));
					var certBytes = this.FileAccessService.ReadAllBytesFrom(this.S4UserCert);
					this._s4uCert = new X509Certificate2(certBytes);
				}
				catch (Exception ex)
				{
					this.Log?.WriteError($"Error occurred loading certificate file {this.S4UserCert}: {ex.Message}");
					throw;
				}
			}

			if (this.ServicePassword != null)
			{
				if (this.ServiceSalt == null)
					context.LogError(nameof(ServiceSalt), $"-{nameof(ServiceSalt)} is required with -{nameof(ServicePassword)}");

				this._serviceSalt = Encoding.UTF8.GetBytes(this.ServiceSalt);

				if (this.U2uTicket != null)
					context.LogError(nameof(ServicePassword), $"-{nameof(ServicePassword)} cannot be used with -{nameof(U2uTicket)}");

				this._serviceCredential = new KerberosPasswordCredential(new UserPrincipalName("<service>", "<realm>"), this.ServicePassword);
			}
			else
			{
				if (this.ServiceSalt != null)
					context.LogError(nameof(ServiceSalt), $"-{nameof(ServiceSalt)} may only be used with -{nameof(ServicePassword)}");
			}
		}

		protected sealed override async Task<IList<TicketInfo>?> RequestTickets(KerberosClient krb, CancellationToken cancellationToken)
		{
			FileSpec ticketStoreFile;
			if (this.Tgt != null) ticketStoreFile = this.Tgt;
			else if (this.TicketCache != null) ticketStoreFile = this.TicketCache;
			else throw new InvalidOperationException($"The command is not configured with -{nameof(Tgt)} -{nameof(TicketCache)}.");

			TicketInfo? sourceTicket = LoadTgtFromStore(krb, ticketStoreFile);
			if (sourceTicket is null)
				return null;

			TicketInfo? u2uTicket;
			if (this.U2uTicket != null)
			{
				u2uTicket = LoadTgtFromStore(krb, this.U2uTicket);
				if (u2uTicket is null)
					return null;
			}
			else
			{
				u2uTicket = null;
			}

			this.WriteVerbose($"Using ticket for {sourceTicket.ClientName}@{sourceTicket.ClientRealm} => {sourceTicket.TargetSpn} expiring {sourceTicket.EndTime}");

			TicketParameters ticketParams = this.TicketParamGroup?.GetTicketParameters(this.Log, KerberosClient.DefaultTicketOptions) ?? krb.GetDefaultTicketOptions(sourceTicket);
			if (this.Forwarded.IsSet)
				ticketParams.Options |= KdcOptions.Forwarded;
			ticketParams.S4UserName = this.S4UserName;
			ticketParams.S4UserCertificate = this._s4uCert;
			ticketParams.S4ProxyService = this.S4ProxyService;
			TicketInfo? armorTicket = this.ArmorTicket != null ? LoadTgtFromStore(krb, this.ArmorTicket) : null;
			ticketParams.ArmorTicket = armorTicket;

			SessionKey? serviceKey;
			if (u2uTicket != null)
			{
				ticketParams.AdditionalTicket = u2uTicket;
				ticketParams.Options |= KdcOptions.EncTicketInSKey;

				serviceKey = u2uTicket.SessionKey;
			}
			else
				serviceKey = null;

			List<TicketInfo> newTickets = new List<TicketInfo>(this.Target.Length);
			foreach (var spn in this.Target)
			{
				var ticket = await krb.RequestTicket(sourceTicket, spn, this.Realm ?? sourceTicket.TicketRealm, this.EncTypes, ticketParams, cancellationToken).ConfigureAwait(false);

				SessionKey? ticketKey;
				if (serviceKey != null)
					ticketKey = serviceKey;
				else if (this._serviceCredential != null && this._serviceCredential.SupportsProfile(ticket.TicketEType))
				{
					var encProf = krb.TryGetEncProfile(ticket.TicketEType);
					if (encProf != null)
						ticketKey = this._serviceCredential.DeriveProtocolKeyFor(encProf, this._serviceSalt);
					else
						ticketKey = null;
				}
				else
				{
					ticketKey = null;
				}

				if (ticketKey != null)
				{
					// Verify the key
					try
					{
						var asrepKey = ticket.AsrepKey;
						//var asrepKey = krb.CreateSessionKeyFor(this.AsrepKey);
						var authzData = ticket.DecryptAuthorizationData(ticketKey, asrepKey);
						ticket.TicketKey = ticketKey;
					}
					catch (Exception ex)
					{
						this.WriteError($"Faild to extract authorization data: {ex.Message}");
					}

					if (ticket.TicketKey != null)
						Program.TryPrintAuthorizationData(ticket, "Ticket authorization data:", this.Log);
				}

				newTickets.Add(ticket);
			}

			return newTickets;
		}
	}
}