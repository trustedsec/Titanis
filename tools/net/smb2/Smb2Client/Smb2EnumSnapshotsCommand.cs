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
	/// <task category="Enumeration">Enumerate the volume snapshots on an SMB server</task>
	[Description("Lists the available snapshots for a file or directory.")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_enumsnapshots_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	[OutputRecordType(typeof(FileSnapshotInfo))]
	sealed class Smb2EnumSnapshotsCommand : Smb2TreeCommand
	{
		protected sealed override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			// Create or open the file/directory
			await using (var file = await client.CreateFileAsync(this.UncPathInfo, new Smb2CreateInfo
			{
				OplockLevel = Smb2OplockLevel.None,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				DesiredAccess = (uint)(Smb2FileAccessRights.ReadAttributes | Smb2FileAccessRights.ReadData | Smb2FileAccessRights.Synchronize),
				FileAttributes = Winterop.FileAttributes.ReparsePoint,
				ShareAccess = Smb2ShareAccess.ReadWrite,
				CreateDisposition = Smb2CreateDisposition.Open,
				CreateOptions = Smb2FileCreateOptions.SynchronousIoNonalert,
				RequestMaximalAccess = true,
				QueryOnDiskId = false
			}, FileAccess.Read, cancellationToken))
			{
				var snapshots = await file.GetSnapshotInfoAsync(cancellationToken);
				this.WriteRecords(snapshots.Snapshots);
				this.WriteVerbose("Total snapshots: " + snapshots.TotalSnapshots);
			}

			return 0;
		}
	}
}
