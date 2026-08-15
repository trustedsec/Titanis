using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msdrsr;

namespace Titanis.Cli.Dsrep;

[Command]
[Description("List domains in a forest")]
public class ListDomainsCommand : DsbindCommand
{
	protected override DsbindScenario Scenario => DsbindScenario.Unspecified;

	[Parameter]
	[Description("Format of name to print")]
	[DefaultValue(DsCrackNameResultFormat.Fqdn1779)]
	public DsCrackNameResultFormat NameFormat { get; set; }

	[Parameter]
	[Description("Site to limit search to")]
	public string? Site { get; set; }

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		if (!string.IsNullOrEmpty(this.Site))
		{
			var names = await dsbind.GetDomains(this.NameFormat, cancellationToken);
			this.WriteRecords(names);
		}
		else
		{
			var names = await dsbind.GetDomains(this.NameFormat, cancellationToken);
			this.WriteRecords(names);
		}
		return 0;
	}
}
