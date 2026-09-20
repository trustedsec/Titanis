using System.ComponentModel;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mstsch;

namespace Titanis.Cli.TschTool;

/// <task category="TSCH">Delete a scheduled task</task>
[Command]
[Description("Deletes a scheduled task")]
[Example("Delete a task", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd -TaskPath \\\\MyTask")]
public class DeleteCommand : TschCommand
{
	[Parameter]
	[Mandatory]
	[Description("Full path of the task to delete")]
	public string TaskPath { get; set; }

	protected sealed override async Task<int> RunAsync(TschClient client, CancellationToken cancellationToken)
	{
		await client.DeleteTask(TaskPath, cancellationToken);
		this.WriteMessage($"Deleted task '{TaskPath}'");
		return 0;
	}
}
