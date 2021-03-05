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
	internal class RequestTicketCommand : Command
	{
		[Parameter]
		[Mandatory]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Host name or address of KDC")]
		public string Kdc { get; set; }

		[Parameter(0)]
		[Mandatory]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("SPNs to request tickets for")]
		public ServicePrincipalName[] Targets { get; set; }

		[Parameter]
		[Mandatory]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Name of file containing a ticket-granting ticket (.kirbi or ccache)")]
		public string Tgt { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Encryption types to request in response")]
		public EType[]? EncTypes { get; set; }

		[ParameterGroup]
		public TicketParameterGroup TicketParamGroup { get; set; }

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

			string tgtFileName = this.ResolveFsPath(this.Tgt);
			this.WriteVerbose($"Reading TGT from {tgtFileName}");
			var tgtStore = krb.LoadTicketsFromFile(File.ReadAllBytes(tgtFileName), out _);
			TicketInfo sourceTicket;
			{
				if (tgtStore.Length == 0)
				{
					this.WriteError($"The file {tgtFileName} does not contain any tickets.");
					return 1;
				}

				var tgtCandidates = tgtStore.Where(r => r.ServiceClass.Equals(KerberosClient.TgsServiceClass, StringComparison.OrdinalIgnoreCase)).ToList();
				if (tgtCandidates.Count == 0)
				{
					this.WriteError($"The file {tgtFileName} does not contain any ticket-granting-tickets.");
					return 1;
				}

				sourceTicket = tgtCandidates[0];
			}

			this.WriteVerbose($"Using ticket for {sourceTicket.UserName}@{sourceTicket.UserRealm} => {sourceTicket.ServiceClass}/{sourceTicket.Host}");


			// Load tickets from file, if it exists
			List<TicketInfo> tickets = new List<TicketInfo>();
			if (File.Exists(outFileName) && this.Append.IsSet)
			{
				TicketInfo[] existingTickets = krb.LoadTicketsFromFile(File.ReadAllBytes(outFileName), out _);
				this.WriteVerbose($"Loaded {existingTickets.Length} ticket(s) from {outFileName}.");
				tickets.AddRange(existingTickets);
			}

			TicketParameters ticketParameters = this.TicketParamGroup?.GetTicketParameters(this.Log) ?? krb.GetDefaultTicketOptions(sourceTicket);

			bool hasSuccess = false;
			foreach (var spn in this.Targets)
			{
				var ticket = await krb.RequestTicket(sourceTicket, spn, sourceTicket.TicketRealm, this.EncTypes, ticketParameters, cancellationToken).ConfigureAwait(false);
				hasSuccess = true;
				tickets.Add(ticket);

				this.WriteRecord(ticket);
			}

			if (hasSuccess)
			{
				var ticketBytes = krb.ExportTickets(tickets, AsreqCommand.FormatFromFileName(outFileName));
				File.WriteAllBytes(outFileName, ticketBytes);

				this.WriteVerbose($"Exported {tickets.Count} ticket(s) to {outFileName}");
			}

			return 0;
		}
	}
}