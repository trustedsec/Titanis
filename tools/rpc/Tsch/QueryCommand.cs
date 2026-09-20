using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mstsch;

namespace Titanis.Cli.TschTool;

/// <task category="TSCH;Enumeration">Enumerate scheduled tasks</task>
[Command]
[Description("Enumerates scheduled tasks in a folder")]
[Example("Enumerate root tasks", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd")]
[Example("Enumerate tasks in a subfolder", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd -Path \\\\Microsoft\\\\Windows\\\\Defrag")]
public class QueryCommand : TschCommand
{
	[Parameter]
	[Description("Task folder path to enumerate (default: root \\)")]
	public string Path { get; set; } = "\\";

	[Parameter]
	[Description("Include hidden tasks")]
	public SwitchParam IncludeHidden { get; set; }

	[Parameter]
	[Description("Recurse into subfolders")]
	public SwitchParam Recurse { get; set; }

	protected sealed override async Task<int> RunAsync(TschClient client, CancellationToken cancellationToken)
	{
		uint flags = IncludeHidden.IsSet ? 1U : 0U;
		await EnumFolder(client, Path, flags, cancellationToken);
		return 0;
	}

	private async Task EnumFolder(TschClient client, string folderPath, uint flags, CancellationToken cancellationToken)
	{
		var tasks = await client.EnumTasks(folderPath, flags, cancellationToken);
		foreach (var task in tasks)
		{
			string fullPath = folderPath.TrimEnd('\\') + "\\" + task;

			TaskInfo? info = null;
			LastRunInfo? lastRun = null;
			try
			{
				info = await client.GetTaskInfo(fullPath, cancellationToken);
				lastRun = await client.GetLastRunInfo(fullPath, cancellationToken);
			}
			catch { }

			string state = info?.State.ToString() ?? "Unknown";
			string enabled = info?.Enabled == true ? "Enabled" : "Disabled";
			string lastRunStr = lastRun?.LastRunTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "Never";
			string lastResult = lastRun != null ? $"0x{lastRun.LastReturnCode:X}" : "-";

			this.WriteMessage($"{fullPath,-60} {state,-10} {enabled,-10} Last: {lastRunStr} ({lastResult})");
		}

		if (Recurse.IsSet)
		{
			var folders = await client.EnumFolders(folderPath, 0, cancellationToken);
			foreach (var folder in folders)
			{
				string subPath = folderPath.TrimEnd('\\') + "\\" + folder;
				await EnumFolder(client, subPath, flags, cancellationToken);
			}
		}
	}
}
