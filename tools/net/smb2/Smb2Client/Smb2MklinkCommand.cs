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
	/// <task category="SMB">Create a filesystem link on an SMB share</task>
	[Description("Creates a symbolic link.")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_mklink_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	[Example("Creates a symlink to a file", @"{0} \\SERVER\Share\Symlink -Relative ActualFile.txt", @"Creates a symlink at \\SERVER\Share\Symlink that points to ActualFile.txt within the same directory")]
	[Example("Creates a symlink to a directory", @"{0} \\SERVER\Share\SymlinkDir -Directory -Relative ActualDir", @"Creates a symlink directory at \\SERVER\Share\SymlinkDir that points to ActualFir within the same directory")]
	[Example("Creates a symlink to a directory with a different label", @"{0} \\SERVER\Share\SymlinkDir -Directory -Relative ActualDir -PrintName ""Not ActualDir""", @"Creates a symlink directory at \\SERVER\Share\SymlinkDir that points to ActualFir within the same directory, but a directory listing prints ""Not ActualDir""")]
	sealed class Smb2MklinkCommand : Smb2TreeCommand
	{
		[Parameter(UncParamPos + 1)]
		[Mandatory]
		[Description("Path of the symbolic link target")]
		public string? TargetPath { get; set; }

		[Parameter]
		[Description("The path to display to the user in directory listings (defaults to <TargetPath>)")]
		public string? PrintPath { get; set; }

		[Parameter]
		[Description("Creates the symlink as a directory")]
		public SwitchParam Directory { get; set; }

		[Parameter]
		[Description("Create the link as a relative path")]
		public SwitchParam Relative { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (this.PrintPath == null)
				this.PrintPath = this.TargetPath;
		}

		protected sealed override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			var flags = this.Relative.IsSet ? SymbolicLinkFlags.RelativePath : SymbolicLinkFlags.FullPathName;
			var createOptions =
				GetCreateOptions(this.Directory.IsSet ? (Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.Directory)
				: (Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.NonDirectory));

			// Create or open the file/directory
			await using (var file = await client.CreateFileAsync(this.UncPath, new Smb2CreateInfo
			{
				OplockLevel = Smb2OplockLevel.None,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				DesiredAccess = (uint)Smb2FileAccessRights.WriteAttributes,
				FileAttributes = Winterop.FileAttributes.ReparsePoint,
				ShareAccess = Smb2ShareAccess.ReadWriteDelete,
				CreateDisposition = Smb2CreateDisposition.OpenIf,
				CreateOptions = createOptions,
				RequestMaximalAccess = true,
				QueryOnDiskId = true
			}, FileAccess.Read, cancellationToken))
			{
				var substPath = this.TargetPath;
				if (!this.Relative.IsSet && !substPath.StartsWith(@"\??\"))
					substPath = @"\??\" + substPath;

				// Set the symlink
				await file.SetSymlinkInfoAsync(flags, substPath, this.PrintPath, cancellationToken);
			}

			return 0;
		}
	}
}
