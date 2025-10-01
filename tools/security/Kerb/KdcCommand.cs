using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Security.Kerberos;

namespace Kerb;

/// <summary>
/// Base implementation for commands 
/// </summary>
internal abstract class KdcCommand : Command
{
	[Parameter]
	[Mandatory]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("Host name or address of KDC")]
	[TypeConverter(typeof(EndPointConverter))]
	[DefaultPort(KerberosClient.KdcTcpPort)]
	public EndPoint Kdc { get; set; }

	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public NetworkParameters NetworkParameters { get; set; }

	protected KerberosClient CreateKerberosClient()
	{
		KerberosClient krb = this.CreateKerberosClient(new SimpleKdcLocator(this.Kdc));
		return krb;
	}
}
