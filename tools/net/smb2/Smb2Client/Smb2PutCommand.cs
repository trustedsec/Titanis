using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.IO;
using Titanis.Smb2.Cli;
using Titanis.Smb2.Pdus;

namespace Titanis.Smb2.Cli
{
	/// <task category="SMB">Upload a file to an SMB share</task>
	[Description("Sends a file to the server.")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_put_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	sealed class Smb2PutCommand : Smb2TreeCommand
	{
		[Parameter(UncParamPos - 1)]
		[Description("Name of local file to send")]
		public string? SourceFileName { get; set; }

		[Parameter]
		[Description("Size of chunks to copy")]
		[DefaultValue(Smb2Client.DefaultChunkSize)]
		public int ChunkSize { get; set; }

		[Parameter]
		[Description("UNC Path of remote file to copy Creation, LastAccess, LastWrite and Change Time from.")]
		public UncPath? TimestampsFrom { get; set; }

		[Parameter]
		[Description("Create time to set on the file (UTC).  If specified, overrides any timestamps copied from source or remote file.")]
		public DateTime? CreateTimestamp { get; set; }

		[Parameter]
		[Description("Last access time to set on the file (UTC).  If specified, overrides any timestamps copied from source or remote file.")]
		public DateTime? LastAccessTimestamp { get; set; }

		[Parameter]
		[Description("Last write time to set on the file (UTC).  If specified, overrides any timestamps copied from source or remote file.")]
		public DateTime? LastWriteTimestamp { get; set; }

		[Parameter]
		[Description("Change time to set on the file (UTC).  If specified, overrides any timestamps copied from source or remote file.")]
		public DateTime? ChangeTimestamp { get; set; }



		/// <inheritdoc/>
		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (this.ChunkSize < 0)
				context.LogError(nameof(ChunkSize), "ChunkSize must be > 0");

			// RemoteTimeFileName when Specified MUST reside on the same share as where we will be putting the target file
			if (this.TimestampsFrom is not null)
			{
				if (!TimestampsFrom.MatchesServerAndShare(this.UncPath))
				{
					context.LogError($"When specifying {nameof(this.TimestampsFrom)}, the file specified must be on the same server and share used by {nameof(this.UncPath)}");
				}
			}
		}

		/// <inheritdoc/>
		protected sealed override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			// Open the source file
			await using (var sourceStream = this.GetSourceStream())
			{
				var sourceFileName = this.SourceFileName;
				Winterop.FileAttributes fileAttributes = Winterop.FileAttributes.Normal;
				// Get source file attributes
				Winterop.FileAttributes attrs = string.IsNullOrEmpty(sourceFileName)
					? Winterop.FileAttributes.Normal
					: (Winterop.FileAttributes)File.GetAttributes(sourceFileName);

				UncPath destPath = this.UncPath;

				// Check whether the remote target is a directory
				bool isDestDir = false;
				try
				{
					await using (var file = await client.CreateFileAsync(destPath, new Smb2CreateInfo
					{
						OplockLevel = Smb2OplockLevel.None,
						ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
						DesiredAccess = (uint)Smb2FileAccessRights.ReadAttributes,
						FileAttributes = 0,
						ShareAccess = Smb2ShareAccess.ReadWriteDelete,
						CreateDisposition = Smb2CreateDisposition.Open,
						CreateOptions = GetCreateOptions(Smb2FileCreateOptions.OpenReparsePoint),
						RequestMaximalAccess = true,
						QueryOnDiskId = true
					}, FileAccess.Read, cancellationToken))
					{
						isDestDir = file.IsDirectory;
						if (isDestDir)
						{
							string? sourceFilePart = string.IsNullOrEmpty(SourceFileName) ? null : Path.GetFileName(sourceFileName);
							if (sourceFilePart == null)
								throw new InvalidOperationException("The destination is a directory, but no source file name was provided.  When copying from console input, you must provide a destination file name.");

							destPath = destPath.Append(Path.GetFileName(sourceFilePart));
						}
					}
				}
				catch { }

				// Check whether remote target exists
				// If the user-provided name is a directory, the previous step appended the path name
				bool exists = false;
				try
				{
					await using (var file = await client.CreateFileAsync(destPath, new Smb2CreateInfo
					{
						OplockLevel = Smb2OplockLevel.None,
						ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
						DesiredAccess = (uint)Smb2FileAccessRights.ReadAttributes,
						FileAttributes = 0,
						ShareAccess = Smb2ShareAccess.ReadWriteDelete,
						CreateDisposition = Smb2CreateDisposition.Open,
						CreateOptions = GetCreateOptions(Smb2FileCreateOptions.OpenReparsePoint),
						RequestMaximalAccess = true,
						QueryOnDiskId = true
					}, FileAccess.Read, cancellationToken))
					{
						isDestDir = file.IsDirectory;
						if (isDestDir)
						{
							string? sourceFilePart = string.IsNullOrEmpty(SourceFileName) ? null : Path.GetFileName(sourceFileName);
							if (sourceFilePart == null)
								throw new InvalidOperationException("The destination is a directory, but no source file name was provided.  When copying from console input, you must provide a destination file name.");

							destPath = destPath.Append(Path.GetFileName(sourceFilePart));
						}
					}
				}
				catch { }

				FileBasicInfo? fileBasicInfo = null;

				//If we're copying timestamps from a remote target file, grab them.  This also validates the target file exists so we don't have a failure condition later.
				if (this.TimestampsFrom != null)
				{
					var options = GetOpenFileCreateInfo();
					options.DesiredAccess = (uint)(Smb2FileAccessRights.ReadAttributes | Smb2FileAccessRights.Synchronize);
					await using (var file = (Smb2OpenFile)await client.CreateFileAsync(this.TimestampsFrom, options, FileAccess.Read, cancellationToken))
					{
						fileBasicInfo = await file.GetBasicInfoAsync(cancellationToken);
					}
				}

				await using (var file = (Smb2OpenFile)await client.CreateFileAsync(this.UncPath, GetCreateFileCreateInfo(attrs), FileAccess.ReadWrite, cancellationToken))
				{
					if (sourceStream.CanSeek)
					{
						await file.SetLengthAsync(sourceStream.Length, cancellationToken);
					}

					await using (var destStream = file.GetStream(false))
					{
						await sourceStream.CopyToAsync2(destStream, this.ChunkSize, cancellationToken);
					}

					if (fileBasicInfo != null)
					{
						CreateTimestamp ??= fileBasicInfo.CreationTime;
						LastAccessTimestamp ??= fileBasicInfo.LastAccessTime;
						LastWriteTimestamp ??= fileBasicInfo.LastWriteTime;
						ChangeTimestamp ??= fileBasicInfo.ChangeTime;
					}
					else if (!string.IsNullOrEmpty(SourceFileName))
					{
						DateTime dateTime = File.GetLastWriteTimeUtc(sourceFileName);
						LastWriteTimestamp ??= dateTime;
						ChangeTimestamp ??= dateTime;
						fileAttributes = (Winterop.FileAttributes)File.GetAttributes(sourceFileName);
					}

					if (CreateTimestamp != null || LastAccessTimestamp != null || LastWriteTimestamp != null || ChangeTimestamp != null)
					{
						await file.SetBasicInfoAsync(
							CreateTimestamp,
							LastAccessTimestamp,
							LastWriteTimestamp,
							ChangeTimestamp,
							fileAttributes,
							cancellationToken
							);
					}

				}
			}

			return 0;
		}

		private Stream GetSourceStream()
		{
			if (!string.IsNullOrEmpty(this.SourceFileName))
				return File.OpenRead(this.ResolveFsPath(this.SourceFileName));
			else
				return this.OpenRawInputStream();
		}
	}
}
