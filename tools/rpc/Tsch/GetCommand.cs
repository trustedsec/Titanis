using System.ComponentModel;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mstsch;

namespace Titanis.Cli.TschTool;

/// <task category="TSCH;Enumeration">Retrieve a task's XML definition</task>
[Command]
[Description("Retrieves the XML definition of a scheduled task")]
[Example("Get task XML", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd -TaskPath \\\\MyTask")]
public class GetCommand : TschCommand
{
	[Parameter]
	[Mandatory]
	[Description("Full path of the task to retrieve")]
	public string TaskPath { get; set; }

	protected sealed override async Task<int> RunAsync(TschClient client, CancellationToken cancellationToken)
	{
		var xml = await client.RetrieveTask(TaskPath, cancellationToken);
		this.WriteMessage(xml);
		return 0;
	}
}
