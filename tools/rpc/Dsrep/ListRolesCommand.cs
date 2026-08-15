using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msdrsr;

namespace Titanis.Cli.Dsrep;

[Command]
[Description("List roles in a forest")]
public class ListRolesCommand : DsbindCommand
{
	protected override DsbindScenario Scenario => DsbindScenario.Unspecified;

	[Parameter]
	[Description("Format of name to print")]
	[DefaultValue(DsCrackNameResultFormat.Fqdn1779)]
	public DsCrackNameResultFormat NameFormat { get; set; }

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		var names = await dsbind.GetRoles(this.NameFormat, cancellationToken);
		this.WriteRecords(names);
		return 0;
	}
}
