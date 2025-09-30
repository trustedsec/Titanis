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
	/// <task category="Enumeration">Enumerate the data streams of a file on an SMB server</task>
	[Description("Lists the data streams of a file or directory.")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_enumstreams_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	[OutputRecordType(typeof(FileStreamInfo))]
	sealed class Smb2EnumStreamsCommand : Smb2TreeCommand
	{
		protected sealed override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			// Create or open the file/directory
			await using (var file = await client.CreateFileAsync(this.UncPath, new Smb2CreateInfo
			{
				OplockLevel = Smb2OplockLevel.None,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				DesiredAccess = (uint)Smb2FileAccessRights.ReadAttributes,
				FileAttributes = Winterop.FileAttributes.ReparsePoint,
				ShareAccess = Smb2ShareAccess.ReadWriteDelete,
				CreateDisposition = Smb2CreateDisposition.Open,
				CreateOptions = GetCreateOptions(Smb2FileCreateOptions.None),
				RequestMaximalAccess = true,
				QueryOnDiskId = true
			}, FileAccess.Read, cancellationToken))
			{
				var streams = await file.GetStreamsInfoAsync(cancellationToken);
				this.WriteRecords(streams);
			}

			return 0;
		}
	}
}
