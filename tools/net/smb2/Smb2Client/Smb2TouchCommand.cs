using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titanis.Cli;
using Titanis.Smb2.Pdus;

// @freefirex Authored original Smb2Client touch command

namespace Titanis.Smb2.Cli
{
	/// <task category="SMB">Update directory entry attributes and timestamps for a new or existing file on an SMB share</task>
	[OutputRecordType(typeof(Smb2DirEntry), DefaultFields = new string[] {
		nameof(Smb2DirEntry.FileName),
		nameof(Smb2DirEntry.CreationTime),
		nameof(Smb2DirEntry.LastAccessTime),
		nameof(Smb2DirEntry.LastWriteTime),
		nameof(Smb2DirEntry.LastChangeTime),
		nameof(Smb2DirEntry.FileAttributes),
	})]
	[Description("Updates the timestamps or attributes of a file or directory on an SMB share.")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_touch_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	[Example("Set hidden and Read-Only attributes on a file, overriding any previous state", @"{0} \\SERVER\Share\file.txt -u User -ud DOMAIN -p password -SetAttributes RH")]
	[Example("Add Hidden and remove Archive and System attributes from a file", @"{0} \\SERVER\Share\file.txt -u User -ud DOMAIN -p password -UpdateAttributes +H-AS")]
	[Example("Set the last access time on a file", @"{0} \\SERVER\Share\file.txt -u User -ud DOMAIN -p password -LastAccessTimestamp ""09/30/2023 13:23:55""")]
	[Example("Copy timestamps and attributes from another file", @"{0} \\SERVER\Share\file.txt -u User -ud DOMAIN -p password  -TimestampsFrom \\SERVER\Share\otherfile.txt")]
	sealed class Smb2TouchCommand : Smb2TreeCommand
	{

		[Parameter]
		[Description("Create time to set on the file (UTC).")]
		public DateTime? CreateTimestamp { get; set; }
		[Parameter]
		[Description("Last access time to set on the file (UTC).")]
		public DateTime? LastAccessTimestamp { get; set; }
		[Parameter]
		[Description("Last write time to set on the file (UTC).")]
		public DateTime? LastWriteTimestamp { get; set; }
		[Parameter]
		[Description("Change time to set on the file (UTC).")]
		public DateTime? ChangeTimestamp { get; set; }
		[Parameter]
		[Description("File attributes to set on the file or directory. Accepts Formats: RHSATFMCOIEVX (string), 28312 (int), 0x80 (hex). See Detailed help for meaning")]
		public FileAttributeSpec? SetAttributes { get; set; }

		[Parameter]
		[Description("File attributes to modify existing attributes from. Accepts Format: (+-)RHSATFMCOIEVX. See Detailed help for more info.")]
		public string? UpdateAttributes { get; set; }

		[Parameter]
		[Description("UNC Path of remote file to copy Creation, LastAccess, LastWrite and Change Time from.")]
		public UncPath? TimestampsFrom { get; set; }

		[Parameter]
		[Description($"If specified, also copy file attributes from {nameof(TimestampsFrom)}")]
		public SwitchParam CopyFileAttributes { get; set; }

		private const string NotUpdated = "<not updated>";
		private Winterop.FileAttributes userRemoveMask = Winterop.FileAttributes.None;
		private Winterop.FileAttributes userAddMask = Winterop.FileAttributes.None;

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if(SetAttributes != null && (!string.IsNullOrEmpty(UpdateAttributes) || CopyFileAttributes.IsSet))
			{
				context.LogError($"-{nameof(this.SetAttributes)} cannot be used with -{nameof(this.UpdateAttributes)} or -{nameof(this.CopyFileAttributes)}");
			}
			if (this.UpdateAttributes is not null)
			{
				if(string.IsNullOrEmpty(UpdateAttributes) || !(UpdateAttributes[0] is '+' or '-'))
				{
					context.LogError($"UpdateAttributes must begin with a '-' or '+'");
				}
			}

			if (UpdateAttributes != null)
			{
				GetMasks(UpdateAttributes, out userAddMask, out userRemoveMask);
				var check = userAddMask & userRemoveMask;
				if( check != 0)
				{
					context.LogError($"The following attributes are found in both add (+) and remove (-) : {check}");
				}
			}

			if (this.TimestampsFrom is not null)
			{
				if (!TimestampsFrom.MatchesServerAndShare(this.UncPath))
				{
					context.LogError($"When specifying -{nameof(this.TimestampsFrom)}, the file specified must be on the same server and share used by -{nameof(this.UncPath)}");
				}
			}

		}

		private void GetMasks(string updateAttributes, out Winterop.FileAttributes addMask, out Winterop.FileAttributes subMask)
		{
			bool adding = true;
			var invalidChars = new StringBuilder();
			addMask = Winterop.FileAttributes.None;
			subMask = Winterop.FileAttributes.None;
			Winterop.FileAttributes currentValue = Winterop.FileAttributes.None;
			for (int i = 0; i < updateAttributes.Length; i++)
			{
				switch (char.ToUpperInvariant(updateAttributes[i]))
				{
					case '+':
						adding = true;
						continue;
					case '-':
						adding = false;
						continue;
					case 'R':
						currentValue = Winterop.FileAttributes.ReadOnly;
						break;
					case 'H':
						currentValue = Winterop.FileAttributes.Hidden;
						break;
					case 'S':
						currentValue = Winterop.FileAttributes.System;
						break;
					case 'A':
						currentValue = Winterop.FileAttributes.Archive;
						break;
					case 'T':
						currentValue = Winterop.FileAttributes.Temporary;
						break;
					case 'F':
						currentValue = Winterop.FileAttributes.SparseFile;
						break;
					case 'M':
						currentValue = Winterop.FileAttributes.ReparsePoint;
						break;
					case 'C':
						currentValue = Winterop.FileAttributes.Compressed;
						break;
					case 'O':
						currentValue = Winterop.FileAttributes.Offline;
						break;
					case 'I':
						currentValue = Winterop.FileAttributes.NotContentIndexed;
						break;
					case 'E':
						currentValue = Winterop.FileAttributes.Encrypted;
						break;
					case 'V':
						currentValue = Winterop.FileAttributes.IntegrityStream;
						break;
					case 'X':
						currentValue = Winterop.FileAttributes.NoScrubData;
						break;
					default:
						invalidChars.Append(updateAttributes[i]);
						continue;
				}
				if (adding)
				{
					addMask |= currentValue;
				}
				else
				{
					subMask |= currentValue;
				}
			}
			if(invalidChars.Length > 0)
			{
				throw new ArgumentException($"Invalid attribute character(s) '{invalidChars}' in attribute string '{updateAttributes}'");
			}
		}

		protected sealed override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			Winterop.FileAttributes? attributes = null;
			
			if (this.TimestampsFrom != null)
			{
				this.WriteDiagnostic($"Opening {TimestampsFrom}");
				var openOptions = GetOpenFileCreateInfo();
				openOptions.DesiredAccess = (uint)(Smb2FileAccessRights.ReadAttributes | Smb2FileAccessRights.Synchronize);
				await using (var file = await client.CreateFileAsync(this.TimestampsFrom, openOptions, FileAccess.Read, cancellationToken))
				{
					this.WriteDiagnostic($"Getting {TimestampsFrom} file info");
					var fileBasicInfo = await file.GetBasicInfoAsync(cancellationToken);
					CreateTimestamp ??= fileBasicInfo.CreationTime;
					LastAccessTimestamp ??= fileBasicInfo.LastAccessTime;
					LastWriteTimestamp ??= fileBasicInfo.LastWriteTime;
					ChangeTimestamp ??= fileBasicInfo.ChangeTime;
					if (CopyFileAttributes.IsSet)
					{
						attributes = fileBasicInfo.Attributes;
					}
				}
			}

			var createOptions = GetCreateFileCreateInfo(Winterop.FileAttributes.Normal);
			createOptions.CreateDisposition = Smb2CreateDisposition.OpenIf;
			createOptions.DesiredAccess = (uint)(Smb2FileAccessRights.ReadAttributes | Smb2FileAccessRights.WriteAttributes | Smb2FileAccessRights.Synchronize);
			this.WriteDiagnostic($"Opening or creating {UncPath}");
			await using (var file = await client.CreateFileAsync(UncPath, createOptions, FileAccess.ReadWrite, cancellationToken))
			{
				if(file.CreateAction == Smb2CreateAction.Created)
				{
					this.WriteVerbose($"Created new file {this.UncPath}");
				}
				else
				{
					this.WriteVerbose($"Modifying directory entry for existing file {this.UncPath}");
				}
				this.WriteDiagnostic($"Getting original file info for {this.UncPath}");
				FileBasicInfo originalFileInfo = await file.GetBasicInfoAsync(cancellationToken);
				
				if (SetAttributes != null)
				{
					attributes = SetAttributes.Attributes;
				}
				else
				{
					attributes ??= originalFileInfo.Attributes; //If we didn't already grab these from specified remote file, use original file's values
					attributes |= userAddMask;
					attributes &= ~userRemoveMask;
				}
				this.WriteVerbose(string.Format(@"Updating info:
Create:	{0} -> {1}
Access:	{2} -> {3}
Write:	{4} -> {5}
Change:	{6} -> {7}
Attributes [{8}] (0x{9}) -> [{10}] (0x{11})",
originalFileInfo.CreationTime, (CreateTimestamp != null) ? CreateTimestamp : Smb2TouchCommand.NotUpdated,
(originalFileInfo.LastAccessTime), (LastAccessTimestamp != null) ? LastAccessTimestamp : Smb2TouchCommand.NotUpdated,
(originalFileInfo.LastWriteTime), (LastWriteTimestamp != null) ? LastWriteTimestamp : Smb2TouchCommand.NotUpdated,
(originalFileInfo.ChangeTime), (ChangeTimestamp != null) ? ChangeTimestamp : Smb2TouchCommand.NotUpdated,
originalFileInfo.Attributes, ((int)originalFileInfo.Attributes).ToString("x8"), (attributes != originalFileInfo.Attributes) ? attributes : Smb2TouchCommand.NotUpdated, ((int)attributes!.Value).ToString("x8")));
					await file.SetBasicInfoAsync(
						CreateTimestamp,
						LastAccessTimestamp,
						LastWriteTimestamp,
						ChangeTimestamp,
						attributes!.Value,
						cancellationToken).ConfigureAwait(false);
				this.WriteRecord(new Smb2DirEntry
				{
					FileName = UncPath.GetFileName(),
					RelativePath = UncPath.ShareRelativePath,
					CreationTime = CreateTimestamp ?? originalFileInfo.CreationTime,
					LastAccessTime = LastAccessTimestamp ?? originalFileInfo.LastAccessTime,
					LastWriteTime = LastWriteTimestamp ?? originalFileInfo.LastWriteTime,
					LastChangeTime = ChangeTimestamp ?? originalFileInfo.ChangeTime,
					Size = (ulong)file.Length,
					SizeOnDisk = (ulong)file.AllocationSize,
					FileAttributes = attributes.Value,
					FileId = file.FileId,
					MaxAccess = file.MaximalAccessAllowed,
				});
			}

			return 0;
		}

	}
}
