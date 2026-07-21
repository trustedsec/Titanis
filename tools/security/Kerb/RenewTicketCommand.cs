using System.ComponentModel;
using Titanis.Security;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb;

/// <task category="Kerberos">Renew a ticket</task>
[Command]
[Description("Renews a ticket")]
[DetailedHelpText(@"This command sends a request to the TGS to renew the source ticket.  You may provide the source ticket to renew either with -Ticket or -TicketCache.  For -TicketCache, -TargetSpn is required; for -Ticket, -TargetSpn is optional.  If you specify both -Ticket and -TicketCache, {0} only loads source tickets from -Ticket and only uses -TicketCache for output.

If you specify -TargetSpn with one or more SPNs, {0} only renews tickets matching one of the specified SPNs.
")]
[Example("Renewing all tickets in a file", "{0} -Ticket milchick-lumon-fs1.kirbi 10.66.0.11 -OutputFileName milchick-lumon-fs1.kirbi -Overwrite")]
[Example("Renewing tickets from cache", "{0} -TicketCache milchick.ccache 10.66.0.11 -TargetSpn host/lumon-fs1, cifs/lumon-fs1")]
public class RenewTicketCommand : KdcRequestCommand
{

	[Parameter]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("Name of file containing a ticket-granting ticket (.kirbi or ccache)")]
	[KerberosTicketFileSpec(true)]
	public FileSpec? Ticket { get; set; }

	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public TicketParameterGroup TicketParameters { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	[Parameter(After = nameof(Kdc))]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("SPNs to renew tickets for")]
	public SecurityPrincipalName[] TargetSpn { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		bool hasSourceTicket =
			(this.Ticket != null)
			|| (this.TicketCache != null && this.TargetSpn != null)
			;
		if (!hasSourceTicket)
			context.LogError("You must specify the source ticket to renew by specifying -Ticket <ticket file> or by providing both -TicketCache and -TargetSpn.");
	}

	protected override async Task<IList<TicketInfo>?> RequestTickets(KerberosClient krb, CancellationToken cancellationToken)
	{
		List<TicketInfo> sourceTickets = new List<TicketInfo>();
		if (this.Ticket != null)
		{
			var ticketFile = this.Ticket;
			this.WriteDiagnostic($"Loading tickets from {ticketFile}");
			var tickets = krb.LoadTicketsFromFile(this.FileAccessService.ReadAllBytesFrom(ticketFile), ticketFile.FileName, out var format);

			if (this.TargetSpn != null)
			{
				tickets = Array.FindAll(tickets, t => this.TargetSpn.Any(r => r.Equals(t.TargetSpn)));
			}
			sourceTickets.AddRange(tickets);
		}
		else
		{
			foreach (var spn in this.TargetSpn)
			{
				var ticket = krb.TicketCache.GetTicketFromCache(spn, null);
				if (ticket is not null)
					sourceTickets.Add(ticket);
				else
					this.WriteWarning($"No ticket for {spn}");
			}
		}

		if (sourceTickets.Count == 0)
			this.WriteError($"No source tickets found to renew.");

		List<TicketInfo> renewedTickets = new List<TicketInfo>();
		bool flexEndTime = !this.TicketParameters.EndTime.HasValue;
		var ticketParams = this.TicketParameters.GetTicketParameters(this.Log, KerberosClient.DefaultTicketOptions);

		// TODO: What ticket options does Windows use for renewal?

		ticketParams.Options |= KdcOptions.Renew;

		foreach (var ticket in sourceTickets)
		{
			if (flexEndTime && ticket.RenewTill.HasValue)
				ticketParams.EndTime = ticket.RenewTill;

			this.WriteDiagnostic($"Renewing ticket to {ticket.TargetSpn}");
			var renewed = await krb.RequestTicket(ticket, ticket.TargetSpn, ticket.TicketRealm, null, ticketParams, cancellationToken);
			renewedTickets.Add(renewed);
		}

		return renewedTickets;
	}
}
