using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;
using Titanis.Msrpc.Msdrsr;
using Titanis.Net;

namespace Titanis.Cli.Dsrep;

[Command]
[Description("Replicates a naming context")]
public sealed class ReplicateNcCommand : ReplicateCommand
{
	[Parameter(After = nameof(RpcCommand.ServerName))]
	[Description("DN of naming contexts (partitions) to replicate")]
	public LdapDistinguishedName[]? NamingContext { get; set; }

	protected sealed override ExtendedOpRequest GetExop() => 0;

	protected override async IAsyncEnumerable<DsName> GetObjectNames(CancellationToken cancellationToken)
	{
		if (this.NamingContext is null)
		{
			var ldapClient = await LdapClient.Connect(new DnsEndPoint(this.ServerName, 389), null, this.RequireService<ISocketService>(), this.RequireService<IClientCredentialService>(), cancellationToken);

			yield return new DsName(Guid.Empty, null, ldapClient.DomainRoot);
		}
		else
		{
			foreach (var nc in this.NamingContext)
			{
				yield return new DsName(Guid.Empty, null, nc);
			}
		}
	}
}
