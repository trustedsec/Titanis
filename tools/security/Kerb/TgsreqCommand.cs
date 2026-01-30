using System.ComponentModel;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Titanis;
using Titanis.Cli;
using Titanis.Security;
using Titanis.Security.Kerberos;

namespace Kerb
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
	internal class RequestTicketCommand : TicketRequestCommand
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
		[Parameter(KdcPosition + 1)]
		[Mandatory]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("SPNs to request tickets for")]
		public SecurityPrincipalName[] Targets { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Name of file containing a ticket-granting ticket (.kirbi or ccache)")]
		public string? Tgt { get; set; }

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
		public string? S4UserCert { get; set; }

		[Parameter]
		[Description("Name of service account with S4U2proxy")]
		public SecurityPrincipalName? S4ProxyService { get; set; }

		[Parameter]
		[Description("Name of file containing U2U ticket")]
		public string? U2uTicket { get; set; }

		private X509Certificate2? _s4uCert;
		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);
			if (string.IsNullOrEmpty(this.Tgt) && string.IsNullOrEmpty(this.TicketCache))
				context.LogError($"Either -{nameof(Tgt)} or -{nameof(TicketCache)} must be specified.");
			if (!string.IsNullOrEmpty(this.S4UserCert))
			{
				try
				{
					this.Log?.WriteMessage(LogMessage.Verbose(null, $"Loading certificate file {this.S4UserCert}"));
					var certBytes = File.ReadAllBytes(this.S4UserCert);
					this._s4uCert = new X509Certificate2(certBytes);
				}
				catch (Exception ex)
				{
					this.Log?.WriteError($"Error occurred loading certificate file {this.S4UserCert}: {ex.Message}");
					throw;
				}


			}
		}

		protected sealed override async Task<IList<TicketInfo>?> RequestTickets(KerberosClient krb, CancellationToken cancellationToken)
		{
			string ticketStoreFile;
			if (!string.IsNullOrEmpty(this.Tgt)) ticketStoreFile = this.ResolveFsPath(this.Tgt);
			else if (!string.IsNullOrEmpty(this.TicketCache)) ticketStoreFile = this.ResolveFsPath(this.TicketCache);
			else throw new InvalidOperationException($"The command is not configured with -{nameof(Tgt)} -{nameof(TicketCache)}.");

			TicketInfo? sourceTicket = LoadTgtFromStore(krb, ticketStoreFile);
			if (sourceTicket is null)
				return null;


			TicketInfo? u2uTicket;
			if (!string.IsNullOrEmpty(this.U2uTicket))
			{
				u2uTicket = LoadTgtFromStore(krb, this.U2uTicket);
				if (u2uTicket is null)
					return null;
			}
			else
			{
				u2uTicket = null;
			}

			this.WriteVerbose($"Using ticket for {sourceTicket.UserName}@{sourceTicket.UserRealm} => {sourceTicket.TargetSpn} expiring {sourceTicket.EndTime}");

			TicketParameters ticketParams = this.TicketParamGroup?.GetTicketParameters(this.Log) ?? krb.GetDefaultTicketOptions(sourceTicket);
			if (this.Forwarded.IsSet)
				ticketParams.Options |= KdcOptions.Forwarded;
			ticketParams.S4UserName = this.S4UserName;
			ticketParams.S4UserCertificate = this._s4uCert;
			ticketParams.S4ProxyService = this.S4ProxyService;

			if (u2uTicket != null)
			{
				ticketParams.AdditionalTicket = u2uTicket;
				ticketParams.Options |= KdcOptions.EncTicketInSKey;
			}

			List<TicketInfo> newTickets = new List<TicketInfo>(this.Targets.Length);
			foreach (var spn in this.Targets)
			{
				var ticket = await krb.RequestTicket(sourceTicket, spn, this.Realm ?? sourceTicket.TicketRealm, this.EncTypes, ticketParams, cancellationToken).ConfigureAwait(false);

				if (u2uTicket != null)
					ticket.DecryptAuthorizationData(u2uTicket.SessionKey);

				newTickets.Add(ticket);
			}

			return newTickets;
		}

		private TicketInfo? LoadTgtFromStore(KerberosClient krb, string ticketStoreFile)
		{
			this.WriteVerbose($"Reading TGT from {ticketStoreFile}");
			var tgtStore = krb.LoadTicketsFromFile(File.ReadAllBytes(ticketStoreFile), out _);

			TicketInfo? sourceTicket;
			if (ticketStoreFile.Length == 0)
			{
				this.WriteError($"The file {ticketStoreFile} does not contain any tickets.");
				sourceTicket = null;
			}
			else
			{
				var tgtCandidates = tgtStore.Where(r => r.IsCurrent && r.IsTgt).ToList();
				if (tgtCandidates.Count == 0)
				{
					this.WriteError($"The file {ticketStoreFile} does not contain any valid ticket-granting tickets.");
					sourceTicket = null;
				}
				else
				{
					sourceTicket = tgtCandidates[0];
				}
			}

			return sourceTicket;
		}
	}
}