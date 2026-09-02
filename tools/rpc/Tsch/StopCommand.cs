using System.ComponentModel;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mstsch;

namespace Titanis.Cli.TschTool;

/// <task category="TSCH">Stop a running scheduled task</task>
[Command]
[Description("Stops all running instances of a scheduled task")]
[Example("Stop a task", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd -TaskPath \\\\MyTask")]
public class StopCommand : TschCommand
{
	[Parameter]
	[Mandatory]
	[Description("Full path of the task to stop")]
	public string TaskPath { get; set; }

	protected sealed override async Task<int> RunAsync(TschClient client, CancellationToken cancellationToken)
	{
		await client.StopTask(TaskPath, cancellationToken);
		this.WriteMessage($"Stopped task '{TaskPath}'");
		return 0;
	}
}
