using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msdrsr;

namespace Titanis.Cli.Dsrep;

[Command]
[Description("List FSMO roles in a forest")]
public class ListSitesCommand : DsbindCommand
{
	protected override DsbindScenario Scenario => DsbindScenario.Unspecified;

	[Parameter]
	[Description("Format of name to print")]
	[DefaultValue(DsCrackNameResultFormat.Fqdn1779)]
	public DsCrackNameResultFormat NameFormat { get; set; }

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		var names = await dsbind.GetSites(this.NameFormat, cancellationToken);
		this.WriteRecords(names);
		return 0;
	}
}
