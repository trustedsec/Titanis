using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Mswkst;

namespace Titanis.Cli.Wkst;

[Description("Gets workstation info")]
[OutputRecordType(typeof(WorkstationInfo), DefaultOutputStyle = OutputStyle.List)]
public class GetInfoCommand : WorkstationCommand
{
	protected override async Task<int> RunAsync(WorkstationClient client, CancellationToken cancellationToken)
	{
		var info = await client.GetInfo(this.CurrentServerName, [
			WorkstationInfoLevel.Level102,  WorkstationInfoLevel.Level502
			], cancellationToken);
		this.WriteRecord(info);
		return 0;
	}
}
