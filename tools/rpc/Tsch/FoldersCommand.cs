using System.ComponentModel;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mstsch;

namespace Titanis.Cli.TschTool;

/// <task category="TSCH;Enumeration">Enumerate task scheduler folders</task>
[Command]
[Description("Enumerates task scheduler folders")]
[Example("Enumerate folders", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd")]
public class FoldersCommand : TschCommand
{
	[Parameter]
	[Description("Folder path to enumerate (default: root \\)")]
	public string Path { get; set; } = "\\";

	[Parameter]
	[Description("Recurse into subfolders")]
	public SwitchParam Recurse { get; set; }

	protected sealed override async Task<int> RunAsync(TschClient client, CancellationToken cancellationToken)
	{
		await EnumFolders(client, Path, 0, cancellationToken);
		return 0;
	}

	private async Task EnumFolders(TschClient client, string folderPath, int depth, CancellationToken cancellationToken)
	{
		var folders = await client.EnumFolders(folderPath, 0, cancellationToken);
		foreach (var folder in folders)
		{
			string fullPath = folderPath.TrimEnd('\\') + "\\" + folder;
			string indent = new string(' ', depth * 2);
			this.WriteMessage($"{indent}{fullPath}");

			if (Recurse.IsSet)
			{
				await EnumFolders(client, fullPath, depth + 1, cancellationToken);
			}
		}
	}
}
