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
[Description("Gets KCC failure information")]
[OutputRecordType(typeof(DsrepKccFailure))]
public class KccFailuresCommand : DsbindCommand
{
	protected override DsbindScenario Scenario => DsbindScenario.Repnc;

	[Parameter]
	[Mandatory]
	[Description("Failure kind")]
	public DsrepKccFailureKind Kind { get; set; }

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		var infos = await dsbind.GetKccFailures(null, this.Kind, cancellationToken);
		this.WriteRecords(infos);
		return 0;
	}
}
