using System.ComponentModel;
using System.Net;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb;

/// <summary>
/// Base implementation for commands 
/// </summary>
public abstract class KdcCommand : Command, IHaveServerName
{
	internal const int KdcPosition = 0;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	[Parameter(KdcPosition)]
	[Mandatory]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("Host name or address of KDC")]
	[DefaultPort(KerberosClient.KdcTcpPort)]
	public EndPoint Kdc { get; set; }
    string? IHaveServerName.ServerName => (this.Kdc as DnsEndPoint)?.Host;

	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public NetworkParameters NetworkParameters { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

	[Parameter]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("Name of client workstation")]
	public string? Workstation { get; set; }

	protected KerberosClient CreateKerberosClient()
	{
		KerberosClient krb = this.CreateKerberosClient(new SimpleKdcLocator(this.Kdc));
		if (!string.IsNullOrEmpty(this.Workstation))
			krb.Workstation = HostAddress.FromNetbiosName(this.Workstation);
		return krb;
	}
}
