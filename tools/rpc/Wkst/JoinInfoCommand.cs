using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Mswkst;

namespace Titanis.Cli.Wkst;

[Description("Gets workstation join info")]
[OutputRecordType(typeof(JoinInfoCommand), DefaultOutputStyle = OutputStyle.List)]
public class JoinInfoCommand : WorkstationCommand
{
	protected override async Task<int> RunAsync(WorkstationClient client, CancellationToken cancellationToken)
	{
		var info = await client.GetJoinInfo(this.CurrentServerName, cancellationToken);
		this.WriteRecord(info);
		return 0;
	}
}
