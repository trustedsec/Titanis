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

[Description("Gets a list of tasks within folder")]
[OutputRecordType(typeof(TaskDefinition), DefaultOutputStyle = OutputStyle.List)]
public class EnumTasksCommand : FolderEnumCommand
{
	protected override async ValueTask OnFolder(TaskSchedulerClient client, string fullPath, string relativePath, string name, CancellationToken cancellationToken)
	{
		var tasks = client.GetTasks(fullPath, this.PageSize, FolderEnumOptions.IncludeHidden, cancellationToken);
		await foreach (var taskName in tasks.WithCancellation(cancellationToken))
		{
			var taskPath = $"{fullPath}\\{taskName}";
			var task = await client.RetrieveTask(taskPath, cancellationToken);
			this.WriteRecord(task);
		}
	}

}
