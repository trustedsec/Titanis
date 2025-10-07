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
	/// <task category="SMB">Delete a directory on an SMB share</task>
	[Description("Deletes a directory.")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_rmdir_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	sealed class Smb2RmdirCommand : Smb2TreeCommand
	{
		protected sealed override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			await client.RemoveDirectoryAsync(this.UncPath, cancellationToken);
			return 0;
		}
	}
}
