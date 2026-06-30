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

	protected override KerberosClient CreateKerberosClient()
	{
		KerberosClient krb = this.CreateKerberosClient(new SimpleKdcLocator(this.Kdc));
		if (!string.IsNullOrEmpty(this.Workstation))
			krb.Workstation = HostAddress.FromNetbiosName(this.Workstation);
		return krb;
	}
}
