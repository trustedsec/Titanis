using System.ComponentModel;
using System.Net;
using Titanis.Cli;
using Titanis.Security;
using Titanis.Security.Kerberos;

namespace Kerb
{
	/// <task category="Kerberos;Expanding Access">Request a ticket for a service</task>
	/// <task category="Kerberos;Expanding Access">Get ticket hash for hash cracking</task>
	[Command]
	[Description("Requests a ticket from the KDC.")]
	[OutputRecordType(typeof(TicketInfo), DefaultOutputStyle = OutputStyle.List)]
	[DetailedHelpText(@"This command sends a TGS-REQ to the KDC to request a ticket.

The command line must include either a password or a hex-encoded key that is used both for pre-authentication as well as to decrypt the response.  When specifying the NTLM hash, specify just the NTLM portion with no colon.

By default, all supported encryption types are sent in the request.  To limit this, use the -EncTypes parameter to specify which encryption types to request from the server.")]
	[Example("Requesting a ticket for SMB", "{0} -Kdc 10.66.0.11 -Tgt milchick-tgt.kirbi cifs/LUMON-FS1 -OutputFile milchick-LUMON-FS1.kirbi")]
	[Example("Requesting a ticket for SMB and Host", "{0} -Kdc 10.66.0.11 -Tgt milchick-tgt.kirbi cifs/LUMON-FS1, HOST/LUMON-FS1 -OutputFile milchick-LUMON-FS1.kirbi")]
	internal class RequestTicketCommand : TicketRequestCommand
	{
		[Parameter(0)]
		[Mandatory]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("SPNs to request tickets for")]
		public SecurityPrincipalName[] Targets { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Name of file containing a ticket-granting ticket (.kirbi or ccache)")]
		public string? Tgt { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Encryption types to request in response")]
		public EType[]? EncTypes { get; set; }

		[ParameterGroup]
		public TicketParameterGroup TicketParamGroup { get; set; }

		[Parameter]
		[Description("Realm of the KDC")]
		public string? Realm { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);
			if (string.IsNullOrEmpty(this.Tgt) && string.IsNullOrEmpty(this.TicketCache))
				context.LogError($"Either -{nameof(Tgt)} or -{nameof(TicketCache)} must be specified.");
		}

		protected sealed override async Task<IList<TicketInfo>?> RequestTickets(KerberosClient krb, CancellationToken cancellationToken)
		{
			string ticketStoreFile;
			if (!string.IsNullOrEmpty(this.Tgt)) ticketStoreFile = this.ResolveFsPath(this.Tgt);
			else if (!string.IsNullOrEmpty(this.TicketCache)) ticketStoreFile = this.ResolveFsPath(this.TicketCache);
			else throw new InvalidOperationException($"The command is not configured with -{nameof(Tgt)} -{nameof(TicketCache)}.");

			this.WriteVerbose($"Reading TGT from {ticketStoreFile}");
			var tgtStore = krb.LoadTicketsFromFile(File.ReadAllBytes(ticketStoreFile), out _);

			TicketInfo sourceTicket;
			{
				if (ticketStoreFile.Length == 0)
				{
					this.WriteError($"The file {ticketStoreFile} does not contain any tickets.");
					return null;
				}

				var tgtCandidates = tgtStore.Where(r => r.IsCurrent && r.IsTgt).ToList();
				if (tgtCandidates.Count == 0)
				{
					this.WriteError($"The file {ticketStoreFile} does not contain any ticket-granting-tickets.");
					return null;
				}

				sourceTicket = tgtCandidates[0];
			}

			this.WriteVerbose($"Using ticket for {sourceTicket.UserName}@{sourceTicket.UserRealm} => {sourceTicket.TargetSpn} expiring {sourceTicket.EndTime}");

			TicketParameters ticketParameters = this.TicketParamGroup?.GetTicketParameters(this.Log) ?? krb.GetDefaultTicketOptions(sourceTicket);

			bool hasSuccess = false;
			List<TicketInfo> newTickets = new List<TicketInfo>(this.Targets.Length);
			foreach (var spn in this.Targets)
			{
				var ticket = await krb.RequestTicket(sourceTicket, spn, this.Realm ?? sourceTicket.TicketRealm, this.EncTypes, ticketParameters, cancellationToken).ConfigureAwait(false);
				hasSuccess = true;
				newTickets.Add(ticket);

				this.WriteRecord(ticket);
			}

			return newTickets;
		}
	}
}