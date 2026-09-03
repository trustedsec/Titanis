using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Mswkst;

namespace Titanis.Cli.Wkst;

[Command]
[Description("Adds an alternate computer name")]
public class AddAltNameCommand : WorkstationCommand
{
	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Name(s) to add")]
	public string[] Name { get; set; }

	protected override async Task<int> RunAsync(WorkstationClient client, CancellationToken cancellationToken)
	{
		foreach (var name in this.Name)
		{
			await client.AddAlternateNames(this.CurrentServerName, name, cancellationToken);
		}
		return 0;
	}
}
