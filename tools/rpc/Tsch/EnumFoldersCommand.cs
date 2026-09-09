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

[Description("Gets a list of task folders")]
public class EnumFoldersCommand : FolderEnumCommand
{
	protected override ValueTask OnFolder(TaskSchedulerClient client, string fullPath, string relativePath, string name, CancellationToken cancellationToken)
	{
		this.WriteRecord(relativePath);
		return ValueTask.CompletedTask;
	}

}
