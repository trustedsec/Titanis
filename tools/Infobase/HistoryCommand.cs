using System.ComponentModel;
using Titanis.Cli;
using Titanis.Info;

namespace Infobase;

[Command]
[Description("Prints command history")]
[OutputRecordType(typeof(CommandHistory))]
public class HistoryCommand : InfobaseCommand
{
	protected override async Task<int> RunAsync(InfoBase infobase, CancellationToken cancellationToken)
	{
		var history = await infobase.QueryCommandHistory(this.LogPartition, cancellationToken);
		this.WriteRecords(history);

		return 0;
	}
}