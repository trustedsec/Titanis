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
	/// <task category="SMB">Delete a file in SMB share</task>
	[Description("Deletes a file.")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_rm_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	sealed class Smb2RmCommand : Smb2TreeCommand
	{
		protected sealed override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			await client.CreateFileAsync(this.UncPath, GetDeleteFileCreateInfo(), FileAccess.Read, cancellationToken);
			return 0;
		}
	}
}
