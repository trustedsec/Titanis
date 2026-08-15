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
[Description("Gets up-to-date vector info")]
[OutputRecordType(typeof(DsrepVector))]
public class UptodateVectorsCommand : DsbindCommand
{
	protected override DsbindScenario Scenario => DsbindScenario.Repnc;

	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Domain DN")]
	public LdapDistinguishedName Domain { get; set; }

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		var infos = await dsbind.GetUptodateVectors(this.Domain, cancellationToken);
		this.WriteRecords(infos);
		return 0;
	}
}
