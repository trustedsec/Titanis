using System;
using System.ComponentModel;
using System.Linq;
using Titanis;
using Titanis.Cli;
using Titanis.Cli.Tsch;
using Titanis.Msrpc.Mslsar;
using Titanis.Msrpc.Mstsch;
using Titanis.Winterop.Security;

namespace Tsch;

[Description("Stops a running task instance")]
public class StopInstanceTaskCommand : TschCommand
{
	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Instance(s) to stop")]
	public Guid[] InstanceGuid { get; set; }

	protected override async Task<int> RunAsync(TaskSchedulerClient client, CancellationToken cancellationToken)
	{
		foreach (var guid in this.InstanceGuid)
		{
			await client.StopTaskInstance(
				guid,
				cancellationToken);
		}
		return 0;
	}
}
