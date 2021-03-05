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
	/// <task category="SMB">Create a directory on an SMB share</task>
	[Description("Creates a directory.")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_mkdir_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	sealed class Smb2MkdirCommand : Smb2TreeCommand
	{
		[Parameter]
		[Description("Create parent directories")]
		public SwitchParam Parents { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);
			if (string.IsNullOrEmpty(this.UncPathInfo.ShareRelativePath))
				context.LogError(nameof(this.UncPath), "The path must include at least one level after the share name.");
		}

		protected sealed override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			bool exists = false;
			try
			{
				await using var file = await client.CreateDirectoryAsync(this.UncPathInfo, cancellationToken);
				exists = true;
			}
			catch (NtstatusException ex) when (this.Parents.IsSet && ex.StatusCode == Ntstatus.STATUS_OBJECT_PATH_NOT_FOUND)
			{
				exists = false;

				// This only matters if -Parents is set
			}

			if (this.Parents.IsSet && !exists)
			{
				Stack<string> dirNames = new Stack<string>();
				var path = this.UncPathInfo.ShareRelativePath;
				do
				{
					var name = Path.GetFileName(path);
					dirNames.Push(name);
					path = Path.GetDirectoryName(path);
				} while (!string.IsNullOrEmpty(path));

				var dirPath = this.UncPathInfo.ShareUncPath;
				while (dirNames.Count > 0)
				{
					dirPath = dirPath.Append(dirNames.Pop());
					try
					{
						await using var file = await client.CreateFileAsync(dirPath, new Smb2CreateInfo
						{
							Priority = Smb2Priority.CreateDir,
							// OpenIf makes more sense here, but this reflects what MKDIR does
							CreateDisposition = Smb2CreateDisposition.Create,
							DesiredAccess = (uint)Smb2FileAccessRights.DefaultCreateDirAccess,
							ShareAccess = Smb2ShareAccess.ReadWrite,
							FileAttributes = Winterop.FileAttributes.Normal,
							CreateOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.SynchronousIoNonalert,
							ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
						}, FileAccess.Read, cancellationToken);
					}
					catch (NtstatusException ex) when (ex.StatusCode == Ntstatus.STATUS_OBJECT_NAME_COLLISION)
					{
						// Continue
					}
				}
			}
			else
			{
			}

			return 0;
		}
	}
}
