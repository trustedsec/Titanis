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
public class JoinCommand : WorkstationCommand
{
	protected override async Task<int> RunAsync(WorkstationClient client, CancellationToken cancellationToken)
	{
		await client.Join(this.CurrentServerName, "corp.lumon.ind", null, "milchick", "Br3@kr00m!", cancellationToken);
		//await client.Join(this.CurrentServerName, "corp.lumon.ind", null, null, null, cancellationToken);
		return 0;
	}
}
