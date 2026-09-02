using System.ComponentModel;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mstsch;

namespace Titanis.Cli.TschTool;

/// <task category="TSCH">Enable or disable a scheduled task</task>
[Command]
[Description("Enables or disables a scheduled task")]
[Example("Disable a task", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd -TaskPath \\\\MyTask -Disable")]
public class EnableCommand : TschCommand
{
	[Parameter]
	[Mandatory]
	[Description("Full path of the task")]
	public string TaskPath { get; set; }

	[Parameter]
	[Description("Disable the task instead of enabling it")]
	public SwitchParam Disable { get; set; }

	protected sealed override async Task<int> RunAsync(TschClient client, CancellationToken cancellationToken)
	{
		bool enable = !Disable.IsSet;
		await client.EnableTask(TaskPath, enable, cancellationToken);
		this.WriteMessage($"{(enable ? "Enabled" : "Disabled")} task '{TaskPath}'");
		return 0;
	}
}
