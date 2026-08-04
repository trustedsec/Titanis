using System;
using System.Buffers;
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
using Titanis.Winterop.Security;

namespace Titanis.Smb2.Cli
{
	/// <task category="SMB">Get a file from an SMB server</task>
	[Description("Gets the contents of a file.")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_get_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	[Example("Print a file to the terminal", @"{0} \\SERVER\Share\File.txt -u DOMAIN\User -p Password", "Copies File.txt from SERVER and prints to the terminal")]
	[Example("Copies a file", @"{0} \\SERVER\Share\File.txt LocalFile.txt -u DOMAIN\User -p Password", "Copies File.txt from SERVER and saves it to a local file named LocalFile.txt")]
	[Example("Copies a directory tree of *.txt files", @"{0} \\SERVER\Share\*.txt LocalTextFiles -depth 20 -u DOMAIN\User -p Password", "Copies all files matching *.txt from a directory tree, up to a depth of 20, into a local directory named LocalTextFiles")]
	[Example("Copies a directory tree structure (no files)", @"{0} \\SERVER\Share\Departments Departments -depth -1 -u DOMAIN\User -p Password", "Copies the directory structure with an unlimited depth without copying any files")]
	sealed class Smb2GetCommand : Smb2TreeCommand
	{
		[Parameter(UncParamPos + 1)]
		[Description("Name of local file to write")]
		public FileSpec? DestinationFileName { get; set; }

		[Parameter]
		[Advanced]
		[Description("Size of chunks to copy")]
		[Category(ParameterCategories.ClientBehavior)]
		public int ChunkSize { get; set; }

		[Parameter]
		[Advanced]
		[Description("Reads the data directly from storage")]
		[Category(ParameterCategories.ClientBehavior)]
		public SwitchParam Unbuffered { get; set; }

		[Parameter]
		[Advanced]
		[Description("Requests the server compress the data")]
		[Category(ParameterCategories.ClientBehavior)]
		public SwitchParam Compress { get; set; }

		[Parameter]
		[Description("Depth of directory tree to traverse (default = 0 [no recursion], -1 = no limit)")]
		[Category(ParameterCategories.ClientBehavior)]
		public int Depth { get; set; }

		[Parameter]
		[Advanced]
		[Description("Specifies the buffer size for querying the directory listing (for recursive operations).")]
		[Category(ParameterCategories.ClientBehavior)]
		public int QueryBufferSize { get; set; }

		[Parameter]
		[Advanced]
		[Description("Only copies the directory structure, but not the files.")]
		[Category(ParameterCategories.ClientBehavior)]
		public SwitchParam TreeOnly { get; set; }

		[Parameter]
		[Description("Overwrites existing local files")]
		[Category(ParameterCategories.ClientBehavior)]
		public SwitchParam Overwrite { get; set; }

		[Parameter]
		[Description("Continues copying after an error occurs")]
		[Category(ParameterCategories.ClientBehavior)]
		public SwitchParam ContinueOnError { get; set; }

		[Parameter]
		[Description("Snapshot version, either as a date/time or a @GMT token")]
		[Category(ParameterCategories.ClientBehavior)]
		public TimeWarpToken? TimeWarpToken { get; set; }

		// Observed from TYPE command
		public const int DefaultChunkSize = 32768;

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (this.ChunkSize == 0)
				this.ChunkSize = DefaultChunkSize;
			if (this.ChunkSize < 0)
				context.LogError(nameof(ChunkSize), "ChunkSize must be > 0");

			if (this.Depth < 0)
				this.Depth = int.MaxValue;

			if (this.QueryBufferSize == 0)
				this.QueryBufferSize = Smb2Directory.DefaultQueryBufferSize;

		}

		protected sealed override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			this.SetOutputFormat(OutputStyle.Raw);

			// Determine the read options
			Smb2ReadOptions readOptions = Smb2ReadOptions.None;
			if (this.Unbuffered.IsSet)
				readOptions |= Smb2ReadOptions.Unbuffered;
			if (this.Compress.IsSet)
				readOptions |= Smb2ReadOptions.Compressed;

			var destPath = this.DestinationFileName;

			if (destPath != null && (destPath.FileName.StartsWith(@"\\") || destPath.FileName.StartsWith(@"//")))
				this.WriteWarning("The destination part appears to be a UNC path.  Note that the destination is resolved using the OS functions and not using configured parameters.");

			// Determine whether the source is a directory
			var rootObjectPath = this.UncPath;
			WildcardPattern? pattern = null;
			if (this.UncPath.HasShareRelativePath)
			{
				string fileNamePart = this.UncPath.GetFileName();
				bool hasWildcard = !string.IsNullOrEmpty(fileNamePart) && WildcardPattern.ContainsWildcardCharacter(fileNamePart);
				if (hasWildcard)
				{
					rootObjectPath = this.UncPath.GetDirectoryPath();
					pattern = new WildcardPattern(fileNamePart);
				}
			}

			// Open the remote source file
			await using (var rootObject = await client.CreateFileAsync(
				rootObjectPath,
				new Smb2CreateInfo
				{
					CreateDisposition = Smb2CreateDisposition.OpenExisting,
					Priority = Smb2Priority.OpenDir,
					DesiredAccess = (uint)(Smb2FileAccessRights.ReadData | Smb2FileAccessRights.ReadEa | Smb2FileAccessRights.ReadAttributes | Smb2FileAccessRights.Synchronize),
					ShareAccess = Smb2ShareAccess.DefaultDirShare,
					FileAttributes = Winterop.FileAttributes.None,
					CreateOptions = Smb2FileCreateOptions.SynchronousIoNonalert | this.GetExtraCreateOptions(),
					ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
					RequestMaximalAccess = true,
					QueryOnDiskId = true,
					TimeWarpToken = this.TimeWarpToken?.Timestamp
				}, FileAccess.Read, cancellationToken))
			{
				var conn = rootObject.Tree.Session.Connection;
				if (this.ChunkSize > conn.MaxReadSize)
				{
					this.WriteWarning($"ChunkSize reduced to {conn.MaxReadSize:N0} because it exceeded the connection's MaxReadSize");
					this.ChunkSize = conn.MaxReadSize;
				}

				if (rootObject.IsDirectory)
				{
					var dir = (Smb2Directory)rootObject;

					if (destPath is null)
						throw new InvalidOperationException($"The source path is a directory, but no destination path was specified.");
					if (this.FileAccessService.FileExists(destPath))
						throw new InvalidOperationException($"The source path is a directory but the destination path identifies a file.  The destination for a directory copy operation must be a directory.");

					Directory.CreateDirectory(this.FileAccessService.ResolveFsPath(destPath));

					await CopyDirectory(
						client,
						dir,
						rootObjectPath,
						pattern,
						destPath,
						readOptions,
						this.Depth,
						cancellationToken);
				}
				else
				{
					var file = (Smb2OpenFile)rootObject;

					if (this.TreeOnly.IsSpecified)
						this.WriteVerbose("-TreeOnly ignored since the source is a file");

					await CopyFile(file, destPath, readOptions, cancellationToken);
				}
			}

			return 0;
		}

		private async Task CopyFile(
			Smb2OpenFile file,
			FileSpec? destPath,
			Smb2ReadOptions readOptions,
			CancellationToken cancellationToken)
		{
			this.WriteVerbose($"{file.ShareRelativePath} => {destPath}");

			// Get a stream to read the file
			await using (var sourceStream = file.GetStream(false))
			{
				// Specify read options
				sourceStream.ReadOptions = readOptions;

				// Compute file attributes
				const FileAttributes FileAttrMask =
					FileAttributes.Archive
					| FileAttributes.Hidden
					| FileAttributes.System
					| FileAttributes.ReadOnly
					| FileAttributes.Normal
					| FileAttributes.NotContentIndexed
					| FileAttributes.SparseFile
					| FileAttributes.Compressed;
				FileAttributes attrs = FileAttrMask & (FileAttributes)file.FileAttributes;

				// Open the source stream
				await using (var destStream = this.GetDestStream(destPath, attrs, file.Length))
				{
					await sourceStream.CopyToAsync2(destStream, this.ChunkSize, cancellationToken);
				}

				// Apply the timestamps from the remote file to the local one
				if (destPath != null)
				{
					var resolved = this.FileAccessService.ResolveFsPath(destPath);
					File.SetCreationTimeUtc(resolved, file.CreationTime);
					File.SetLastWriteTimeUtc(resolved, file.LastWriteTime);
				}
			}
		}

		private Stream GetDestStream(FileSpec fileName, FileAttributes fileAttributes, long fileSize)
		{
			if (fileName != null)
			{
				var resolved = this.FileAccessService.ResolveFsPath(fileName);
				if (this.FileAccessService.FileExists(fileName))
				{
					if (this.Overwrite.IsSet)
					{
						var attrs = File.GetAttributes(resolved);
						if (0 != (attrs & (FileAttributes.System | FileAttributes.ReadOnly | FileAttributes.Hidden)))
						{
							this.WriteWarning($"{fileName} marked as read-only, hidden, or system; clearing attributes and overwriting.");
							// Clear the read-only bit
							File.SetAttributes(resolved, FileAttributes.Normal);
						}
					}
					else
					{
						throw new InvalidOperationException($"{fileName} already exists; use -Overwrite to overwrite");
					}
				}

				var stream = new FileStream(this.ResolveFsPath(fileName), new FileStreamOptions
				{
					Mode = FileMode.Create,
					Access = FileAccess.Write,
					Share = FileShare.Read,
					Options = FileOptions.None,
				});
				try
				{
					stream.SetLength(fileSize);
					File.SetAttributes(resolved, fileAttributes);
				}
				catch (Exception ex) { }
				return stream;
			}
			else
				return this.OpenRawOutputStream();
		}


		private async Task CopyDirectory(
			Smb2Client smb,
			Smb2Directory dir,
			UncPath dirPath,
			WildcardPattern? pattern,
			FileSpec destPath,
			Smb2ReadOptions readOptions,
			int depth,
			CancellationToken cancellationToken)
		{
			var entries = await dir.QueryDirAsync(
				"*",
				Smb2Directory.Smb2DirQueryOptions.QueryReparseInfo,
				SecurityInfo.None,
				this.QueryBufferSize,
				cancellationToken);

			foreach (var entry in entries)
			{
				// Skip dot directories
				if (entry.FileName == "." || entry.FileName == "..")
					continue;

				// Determine paths of item
				UncPath itemPath = dirPath.Append(entry.FileName);
				var destItemPath = new FileSpec(Path.Combine(this.FileAccessService.ResolveFsPath(destPath), entry.FileName),true);

				try
				{
					if (entry.IsDirectory)
					{
						if (depth < +0)
							continue;

						// Create local directory
						Directory.CreateDirectory(destItemPath.FileName);
						// Open the remote directory
						await using (var subdir = (Smb2Directory)await smb.CreateFileAsync(itemPath, this.GetOpenDirectoryCreateInfo(), FileAccess.Read, cancellationToken))
						{
							// Copy its contents
							await this.CopyDirectory(
								smb,
								subdir,
								itemPath,
								pattern,
								destItemPath,
								readOptions,
								depth - 1,
								cancellationToken
								);
						}
					}
					else
					{
						// Check whether the file name matches the pattern (if any)
						bool matches = pattern?.Matches(entry.FileName) ?? true;
						if (!matches)
							continue;

						// Open the file
						await using (var file = (Smb2OpenFile)await smb.CreateFileAsync(itemPath, this.GetOpenFileCreateInfo(), FileAccess.Read, cancellationToken))
						{
							// Copy the file
							await CopyFile(file, destItemPath, readOptions, cancellationToken);
						}
					}
				}
				catch (Exception ex)
				{
					this.WriteError($"An error occurred while copying {itemPath} to {destItemPath}: {ex.Message}");
					if (!this.ContinueOnError.IsSet)
						throw;
				}
			}
		}
	}
}
