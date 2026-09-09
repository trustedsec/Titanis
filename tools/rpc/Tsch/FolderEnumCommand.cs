using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mstsch;

namespace Titanis.Cli.Tsch;

public abstract class FolderEnumCommand : TschCommand
{
	[Parameter(After = nameof(ServerName))]
	[Description("Folder path to enumerate")]
	public string[]? FolderPath { get; set; }

	[Parameter]
	[Description("Recurse into subfolders")]
	public SwitchParam Recursive { get; set; }

	[Parameter]
	[DefaultValue(100)]
	[Description("Number of tasks to retrieve per call")]
	public int PageSize { get; set; }

	public bool IsSingleFolder { get; set; }

	protected static string CombineTaskPath(string container, string item)
	{
		if (container.EndsWith(@"\"))
			return container + item;
		else
			return $"{container}\\{item}";
	}

	protected override async Task<int> RunAsync(TaskSchedulerClient client, CancellationToken cancellationToken)
	{
		Queue<string> names = new Queue<string>();
		if (this.FolderPath == null)
			this.FolderPath = [string.Empty];

		this.IsSingleFolder = (this.FolderPath.Length == 1) && !this.Recursive.IsSet;

		foreach (var rootPath in this.FolderPath)
		{
			var userRootPath = rootPath ?? string.Empty;
			userRootPath = userRootPath.Replace('/', '\\');
			if (!userRootPath.StartsWith(@"\\"))
				userRootPath = @"\" + userRootPath;
			names.Enqueue(userRootPath);

			while (names.TryDequeue(out var folderPath))
			{
				await foreach (var name in client.GetFolders(folderPath, 100, FolderEnumOptions.IncludeHidden, cancellationToken).WithCancellation(cancellationToken))
				{
					var itemPath = CombineTaskPath(folderPath, name);
					this.WriteVerbose($"Enumerating folder {itemPath}");
					string relativePath = itemPath.Substring(userRootPath.Length);
					try
					{
						this.OnFolder(client, itemPath, relativePath, itemPath, cancellationToken);
						if (this.Recursive.IsSet)
						{
							names.Enqueue(itemPath);
						}
					}
					catch (Exception ex)
					{
						if (this.ContinueOnError.IsSet)
						{
							this.WriteError($"Error occurred on folder {itemPath}: {ex.Message}");
						}
						else
							throw;
					}
				}
			}
		}

		return 0;
	}


	protected abstract ValueTask OnFolder(TaskSchedulerClient client, string fullPath, string relativePath, string name, CancellationToken cancellationToken);
}
