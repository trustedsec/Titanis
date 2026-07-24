using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb;

public abstract class KdcRequestCommand : TicketRequestCommand, IHaveServerName
{
	[Parameter(0)]
	[Mandatory]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("Host name or address of KDC")]
	[DefaultPort(KerberosClient.KdcTcpPort)]
	public EndPoint Kdc { get; set; }
	string? IHaveServerName.ServerName => (this.Kdc as DnsEndPoint)?.Host;

	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public NetworkParameters NetworkParameters { get; set; }

	[Parameter]
	[Advanced]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("Name of file containing armor ticket")]
	[KerberosTicketFileSpec(true)]
	public FileSpec? ArmorTicket { get; set; }

	protected TicketInfo? LoadTgtFromStore(KerberosClient krb, FileSpec ticketStoreFileName)
	{
		this.WriteVerbose($"Reading TGT from {ticketStoreFileName}");
		var tgtStore = krb.LoadTicketsFromFile(this.FileAccessService.ReadAllBytesFrom(ticketStoreFileName), ticketStoreFileName.FileName, out _);

		TicketInfo? sourceTicket;
		if (tgtStore.Length == 0)
		{
			this.WriteError($"The file {ticketStoreFileName} does not contain any tickets.");
			sourceTicket = null;
		}
		else
		{
			var tgtCandidates = tgtStore.Where(r => r.IsCurrent && r.IsTgt).ToList();
			if (tgtCandidates.Count == 0)
			{
				this.WriteError($"The file {ticketStoreFileName} does not contain any valid ticket-granting tickets.");
				sourceTicket = null;
			}
			else
			{
				sourceTicket = tgtCandidates[0];
			}
		}

		return sourceTicket;
	}

	protected override KerberosClient CreateKerberosClient()
	{
		KerberosClient krb = this.CreateKerberosClient(new SimpleKdcLocator(this.Kdc));
		if (!string.IsNullOrEmpty(this.Workstation))
			krb.Workstation = HostAddress.FromNetbiosName(this.Workstation);
		return krb;
	}
}
