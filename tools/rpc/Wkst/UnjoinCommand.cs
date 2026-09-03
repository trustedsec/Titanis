using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Mswkst;

namespace Titanis.Cli.Wkst;

[Command]
[Description("Joins a workstation to a domain")]
public class UnjoinCommand : WorkstationCommand
{
	protected override async Task<int> RunAsync(WorkstationClient client, CancellationToken cancellationToken)
	{
		await client.Unjoin(this.CurrentServerName, null, null, cancellationToken);
		return 0;
	}
}
