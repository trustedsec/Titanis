using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Smb2;
using Titanis.Smb2.Cli;
using Titanis.Winterop;

namespace Titanis.Smb2.Cli
{
	/// <task category="SMB">Watch a file or directory on an SMB server for changes</task>
	/// <summary>
	/// Implements the WATCH command.
	/// </summary>
	[Description("Watches for modifications to a directory or subtree.")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_watch_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	[OutputRecordType(typeof(FileChangeNotification))]
	sealed class Smb2WatchCommand : Smb2TreeCommand
	{
		[Parameter]
		[Description("Watches the entire subtree")]
		public SwitchParam Recursive { get; set; }

		[Parameter]
		[Description("Buffer size (default = 2048)")]
		public int BufferSize { get; set; }

		[Parameter]
		[Description("Continue watching for changes if an error occurs")]
		public SwitchParam ContinueOnErrors { get; set; }

		protected sealed override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			// If the last part of the path contains * or ?, treat it as a seacrh pattern
			var dirPath = this.ShareRelativePath;

			if (this.BufferSize <= 0)
				this.BufferSize = 2048;

			// Open directory
			await using (var dir = await client.OpenDirectoryAsync(this.UncPath, cancellationToken))
			{
				WatchOptions options = WatchOptions.None;
				if (this.ContinueOnErrors.IsSet)
					options |= WatchOptions.ContinueOnError;
				if (this.Recursive.IsSet)
					options |= WatchOptions.WatchSubtree;

				// Watch for notifications
				var changeEnum = dir.ReadChangesAsync(Smb2ChangeFilter.All, options, this.BufferSize);

				this.WriteVerbose($"Watching for changes to {dirPath}{(this.Recursive.IsSet ? " and subdirectories" : null)}.  Press CTRL+C to stop.");

				// Loop through changes
				await foreach (var change in changeEnum.WithCancellation(cancellationToken))
				{
					this.WriteRecord(change);
				}
			}

			return 0;
		}
	}
}
