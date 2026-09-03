using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Mswkst;

namespace Titanis.Cli.Wkst;

[Description("Gets a list of names used by a computer")]
[OutputRecordType(typeof(string), DefaultOutputStyle = OutputStyle.Freeform)]
public class EnumNamesCommand : WorkstationCommand
{
	protected override async Task<int> RunAsync(WorkstationClient client, CancellationToken cancellationToken)
	{
		var names = await client.GetComputerNames(this.CurrentServerName, cancellationToken);
		this.WriteRecords(names);
		return 0;
	}
}
