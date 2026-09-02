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
[Example("Run in a specific RDP session", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd -TaskPath \\\\MyTask -SessionId 2")]
public class RunCommand : TschCommand
{
	[Parameter]
	[Mandatory]
	[Description("Full path of the task to run")]
	public string TaskPath { get; set; }

	[Parameter]
	[Description("Run task in this session ID (e.g. an active RDP session)")]
	public uint SessionId { get; set; }

	[Parameter]
	[Description("Run task as this user")]
	public string? RunAsUser { get; set; }

	[Parameter(20)]
	[Description("Optional arguments to pass to the task")]
	public string[]? TaskArgs { get; set; }

	protected sealed override async Task<int> RunAsync(TschClient client, CancellationToken cancellationToken)
	{
		uint flags = 0;
		if (SessionId > 0)
			flags |= 0x2; // TASK_RUN_USE_SESSION_ID

		var instanceId = await client.RunTask(TaskPath, TaskArgs, flags, SessionId, RunAsUser, cancellationToken);
		this.WriteMessage($"Task '{TaskPath}' started, instance: {instanceId}");
		return 0;
	}
}
