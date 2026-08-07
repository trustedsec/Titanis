using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Info;

namespace Infobase;

[Command]
[Description("Queries item data")]
[OutputRecordType(typeof(Item))]
public class QueryCommand : InfobaseCommand
{
	[Parameter(After = nameof(InfoBase))]
	[Description("Object class to query for")]
	public string? ObjectClass { get; set; }

	[Parameter]
	[Description("Command to query results for")]
	public int? CommandId { get; set; }

	protected override async Task<int> RunAsync(InfoBase infobase, CancellationToken cancellationToken)
	{
		var items = await infobase.GetItems(this.ObjectClass, this.CommandId ?? 0, cancellationToken);
		this.WriteRecords(items);

		return 0;
	}
}
