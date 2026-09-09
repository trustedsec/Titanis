using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Cli.Tsch;
using Titanis.Msrpc.Mstsch;

namespace Tsch;

[Description("Gets a task definition")]
[OutputRecordType(typeof(TaskDefinition), DefaultOutputStyle = OutputStyle.List)]
public class GetTaskCommand : TschCommand
{
	[Parameter(After = nameof(ServerName))]
	[Description("Path of task")]
	public string[]? TaskPath { get; set; }

	protected override async Task<int> RunAsync(TaskSchedulerClient client, CancellationToken cancellationToken)
	{
		foreach (var taskPath_ in this.TaskPath)
		{
			var taskPath = taskPath_;
			taskPath = taskPath.Replace('/', '\\');
			var task = await client.RetrieveTask(taskPath, cancellationToken);
			this.WriteRecord(task);
		}
		return 0;
	}
}
