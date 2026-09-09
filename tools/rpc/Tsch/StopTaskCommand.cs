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

[Description("Stops a task")]
public class StopTaskCommand : TschCommand
{
	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Path of task")]
	public string[] TaskPath { get; set; }

	protected override async Task<int> RunAsync(TaskSchedulerClient client, CancellationToken cancellationToken)
	{
		foreach (var taskPath_ in this.TaskPath)
		{
			var taskPath = taskPath_;
			taskPath = taskPath_.Replace('/', '\\');
			await client.StopTask(
				taskPath,
				cancellationToken);
		}
		return 0;
	}
}
