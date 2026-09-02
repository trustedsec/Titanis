using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mstsch;

namespace Titanis.Cli.TschTool;

/// <task category="TSCH;Lateral Movement">Run a scheduled task immediately</task>
[Command]
[Description("Runs a scheduled task immediately")]
[Example("Run a task", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd -TaskPath \\\\MyTask")]
public class RunCommand : TschCommand
{
	[Parameter]
	[Mandatory]
	[Description("Full path of the task to run")]
	public string TaskPath { get; set; }

	[Parameter(20)]
	[Description("Optional arguments to pass to the task")]
	public string[]? TaskArgs { get; set; }

	protected sealed override async Task<int> RunAsync(TschClient client, CancellationToken cancellationToken)
	{
		var instanceId = await client.RunTask(TaskPath, TaskArgs, 0, cancellationToken);
		this.WriteMessage($"Task '{TaskPath}' started, instance: {instanceId}");
		return 0;
	}
}
