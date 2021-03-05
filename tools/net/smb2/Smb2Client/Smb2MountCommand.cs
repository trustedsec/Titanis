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
	/// <task category="SMB">Create a directory junction or mount point on an SMB share</task>
	[Description("Creates a mount point or junction.")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_mount_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	[Example("Creates a volume mount point", @"{0} \\SERVER\Share\MountPoint \??\Volume{12345678-1234-1234-1234-123456789ABC}\", @"Creates a mount point at \\SERVER\Share\MountPoint that points to the volume {12345678-1234-1234-1234-123456789ABC}")]
	[Example("Creates a junction", @"{0} \\SERVER\Share\Junction \??\C:\WINDOWS", @"Creates a junction at \\SERVER\Share\Junction that points to C:\WINDOWS on the remote system")]
	[Example("Creates a junction with a different label", @"{0} \\SERVER\Share\Junction \??\C:\WINDOWS -PrintName ""Not Windows""", @"Creates a junction at \\SERVER\Share\Junction that points to C:\WINDOWS on the remote system, but prints the link as Not Windows in a directory listing.")]
	sealed class Smb2MountCommand : Smb2TreeCommand
	{
		[Parameter(UncParamPos + 1)]
		[Mandatory]
		[Description("Path of the target volume or directory")]
		public string? TargetPath { get; set; }

		[Parameter]
		[Description("The path to display to the user in directory listings (defaults to <TargetPath>)")]
		public string? PrintPath { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (this.PrintPath == null)
				this.PrintPath = this.TargetPath;

			var targetPath = this.TargetPath;

			// Check the target path
			if (!targetPath.StartsWith(@"\??\"))
				this.WriteWarning("Target path should start with \\??\\");
			if (targetPath.StartsWith(@"\??\Volume") && !targetPath.EndsWith(@"\"))
				this.WriteWarning("Volume path should end with \\");
		}

		protected sealed override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			// Create or open the directory (fail if it's actually a file)
			await using (var file = await client.CreateFileAsync(this.UncPathInfo, new Smb2CreateInfo
			{
				OplockLevel = Smb2OplockLevel.None,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				DesiredAccess = (uint)Smb2FileAccessRights.WriteAttributes,
				FileAttributes = Winterop.FileAttributes.Normal,
				ShareAccess = Smb2ShareAccess.ReadWriteDelete,
				CreateDisposition = Smb2CreateDisposition.OpenIf,
				CreateOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.OpenReparsePoint,
				RequestMaximalAccess = true,
				QueryOnDiskId = true
			}, FileAccess.Read, cancellationToken))
			{
				// Indicate whether the directory was created or already existed.
				if (file.CreateAction != Pdus.Smb2CreateAction.Created)
					this.WriteVerbose($"Directory {this.UncPath} already existed");
				else
					this.WriteVerbose($"Directory {this.UncPath} created");

				// Set the mount point info
				await file.SetVolumeMountPointInfoAsync(this.TargetPath, this.PrintPath, cancellationToken);
			}

			return 0;
		}
	}
}
