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
	/// <task category="SMB">Remove a directory junction or mount point within an SMB share</task>
	/// <summary>
	/// Implements the UMOUNT command.
	/// </summary>
	[Description("Unmounts a mount point.")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_umount_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	sealed class Smb2UmountCommand : Smb2TreeCommand
	{
		[Parameter]
		[Description("Deletes the directory after unmounting")]
		public SwitchParam RemoveDirectory { get; set; }

		protected sealed override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			// Determine create options depending on whether to delete the object
			Smb2FileCreateOptions createOptions =
				this.RemoveDirectory.IsSet ? (Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.DeleteOnClose)
				: Smb2FileCreateOptions.DeleteOnClose;

			// Determine access rights
			var access =
				this.RemoveDirectory.IsSet ? (Smb2FileAccessRights.WriteAttributes | Smb2FileAccessRights.Delete)
				: Smb2FileAccessRights.WriteAttributes;

			// Open the object
			await using (var file = await client.CreateFileAsync(this.UncPath, new Smb2CreateInfo
			{
				OplockLevel = Smb2OplockLevel.None,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				DesiredAccess = (uint)access,
				FileAttributes = Winterop.FileAttributes.Normal,
				ShareAccess = Smb2ShareAccess.ReadWriteDelete,
				CreateDisposition = Smb2CreateDisposition.Open,
				CreateOptions = createOptions,
				RequestMaximalAccess = true,
				QueryOnDiskId = true
			}, FileAccess.Read, cancellationToken))
			{
				// Delete the mount point info
				await file.DeleteMountPointInfoAsync(cancellationToken);
			}

			return 0;
		}
	}
}
