using ms_drsr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;
using Titanis.Msrpc.Msdrsr;

namespace Titanis.Cli.Dsrep;

[Command]
[Description("Gets replication (repsTo) neighbors")]
[OutputRecordType(typeof(DsrepNeighbor))]
public class RepsToCommand : DsbindCommand
{
	protected override DsbindScenario Scenario => DsbindScenario.Repnc;

	[Parameter(After = nameof(ServerName))]
	[Description("Domain DN")]
	public LdapDistinguishedName? Domain { get; set; }

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		var names = await dsbind.GetRepsToNeighbors(this.Domain, cancellationToken);
		this.WriteRecords(names);
		return 0;
	}
}
