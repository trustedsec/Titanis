using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Smb2.Cli;
using Titanis.Smb2.Pdus;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Smb2.Cli
{
	/// <task category="SMB">List the contents of directory on an SMB share</task>
	[OutputRecordType(typeof(Smb2DirEntry), DefaultFields = new string[] {
		nameof(Smb2DirEntry.RelativePath),
		nameof(Smb2DirEntry.Size),
		nameof(Smb2DirEntry.LastWriteTime),
		nameof(Smb2DirEntry.FileAttributes),
		nameof(Smb2DirEntry.LinkTarget)})]
	[OutputFieldFormat(nameof(Smb2DirEntry.FileAttributes), null, typeof(FileAttributeFormatter))]
	[Description("Lists the contents of a directory (including named pipes).")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_ls_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	[Example("Listing the contents of a share (NTLM)", @"{0} \\LUMON-FS1\MDR -u milchick -p Br3@kr00m!", Tag = "MilchickNtlm_Mdr")]
	[Example("Listing named pipes as anonymous", @"{0} \\LUMON-FS1\IPC$ -anon", Tag = "Anon_Pipes")]
	[Example("Listing named pipes", @"{0} \\LUMON-FS1\IPC$ -u milchick -p Br3@kr00m!", Tag = "Milchick_ListPipes")]
	[Example("Using Kerberos with a password", @"{0} \\LUMON-DC1\sysvol -u milchick@LUMON -p Br3@kr00m! -Kdc LUMON-DC1", "This command line specifies credentials along with the -Kdc option specifying the KDC to request a ticket from.", Tag = "MilchickKerberos")]
	[Example("Listing the contents of a share with an alternate host name", @"{0} \\SERVER\MDR -ha 10.66.0.13 -u milchick -p Br3@kr00m!", "In this example, the command line specifies a host name differing from the server name to resolve for connecting to the server.  When connecting to the target, the specified host address (10.66.0.13) is used.  Once the TCP connection is established, the name 'SERVER' is used in the application protocol.  In addition, the name SERVER is used as the SPN, both with NTLM and Kerberos; if strict SPN checking is enabled, this results in STATUS_ACCESS_DENIED.", Tag = "Milchick_AltHostAddress")]
	[Example("Passing the hash", @"{0} \\LUMON-FS1\MDR -u milchick -NtlmHash B406A01772D0AD225D7B1C67DD81496F", "This command line provides the credentials as an NTLM hash.", Tag = "MilchickNtlmHash")]
	[Example("Listing all columns", @"{0} \\LUMON-FS1\C$\Windows -u milchick -Password Br3@kr00m! -OutputFields *", "This command prints all file properties.", Tag = "MilchickNtlm_AllFields")]
	[Example("Customizing NTLM", @"{0} \\LUMON-FS1\MDR -u milchick -p Br3@kr00m! -ntlmver 10.0.0.0 -w MILCHICK-WKS", "This command line specifies a different NTLM version and workstation name to send during authentication.", Tag = "MilchickNtlm_WorkstationVersion")]
	public sealed class Smb2LsCommand : Smb2TreeCommand, ISupportTreeOutput
	{
		[Parameter]
		[Description("Specifies the buffer size for querying the directory listing.")]
		public int QueryBufferSize { get; set; }

		[Parameter]
		[Description("Sets the depth limit for a recursive listing (default = 0 [no recursion], -1 = no limit)")]
		public int Depth { get; set; }

		[Parameter]
		[Description("Snapshot version, either as a date/time or a @GMT token")]
		public TimeWarpToken? TimeWarpToken { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (this.QueryBufferSize == 0)
				this.QueryBufferSize = Smb2Directory.DefaultQueryBufferSize;
		}

		class TraversalStats
		{
			public int Directories { get; set; }
			public int Files { get; set; }
		}

		protected sealed override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			// If the last part of the path contains * or ?, treat it as a seacrh pattern
			var dirPath = this.UncPath;
			string searchPattern = "*";
			bool isDir;
			if (dirPath.ShareRelativePath.Length > 0)
			{
				var fileSpec = dirPath.GetFileName();
				if (string.IsNullOrEmpty(fileSpec))
				{
					// The path ends with a backslash, asserting it as a directory
					isDir = true;
				}
				else
				{
					bool hasWildcards = fileSpec.IndexOfAny(WildcardChars) >= 0;
					if (hasWildcards)
					{
						searchPattern = fileSpec;
						dirPath = dirPath.GetDirectoryPath();
						isDir = true;
					}
					else
					{
						// Check whether it is a directory
						isDir = await client.DirectoryExists(dirPath, cancellationToken, this.GetCreateOptions(0));

						if (!isDir)
						{
							searchPattern = fileSpec;
							dirPath = dirPath.GetDirectoryPath();
						}
					}
				}
			}
			else
			{
				isDir = true;
			}

			// Compute which parts (if any) of the security descriptor to retrieve
			SecurityInfo secInfo = SecurityInfo.None;
			if (this.IsFieldInOutput(nameof(Smb2DirEntry.SecurityDescriptorSddl)))
				secInfo |= SecurityInfo.Owner | SecurityInfo.Group | SecurityInfo.Dacl;
			if (this.IsFieldInOutput(nameof(Smb2DirEntry.Owner)))
				secInfo |= SecurityInfo.Owner;
			if (this.IsFieldInOutput(nameof(Smb2DirEntry.Group)))
				secInfo |= SecurityInfo.Group;
			if (this.IsFieldInOutput(nameof(Smb2DirEntry.Dacl)))
				secInfo |= SecurityInfo.Dacl;
			if (this.IsFieldInOutput(nameof(Smb2DirEntry.Sacl)))
				secInfo |= SecurityInfo.Sacl;

			// Determine other options
			Smb2Directory.Smb2DirQueryOptions options = 0;
			if (this.IsFieldInOutput(nameof(Smb2DirEntry.LinkTarget)))
				options |= Smb2Directory.Smb2DirQueryOptions.QueryReparseInfo;
			if (this.IsFieldInOutput(nameof(Smb2DirEntry.MaxAccess)))
				options |= Smb2Directory.Smb2DirQueryOptions.QueryMaxAccessAllowed;

			// This is known to be a directory
			var stats = new TraversalStats();
			if (this.Depth == 0)
			{
				var items = await GetDirectoryListingAsync(client, dirPath, searchPattern, secInfo, options, null, stats, cancellationToken);
				this.WriteRecords(items);

				ulong size = items.Aggregate(0UL, (acc, r) => acc + r.Size);
				this.WriteVerbose($"{items.Count} files, {size:N0} bytes");
			}
			else
			{
				int depth = this.Depth;
				if (depth < 0)
					depth = int.MaxValue;

				await ListSubtreeAsync(
					client,
					dirPath,
					searchPattern,
					secInfo,
					options,
					null,
					depth,
					stats,
					cancellationToken);
			}

			return 0;
		}

		private async Task ListSubtreeAsync(
			Smb2Client client,
			UncPath dirPath,
			string searchPattern,
			SecurityInfo secInfo,
			Smb2Directory.Smb2DirQueryOptions options,
			string? prefix,
			int depth,
			TraversalStats stats,
			CancellationToken cancellationToken)
		{
			var items = await GetDirectoryListingAsync(client, dirPath, searchPattern, secInfo, options, prefix, stats, cancellationToken);
			foreach (var item in items)
			{
				this.WriteRecord(item);
				if (item.IsDirectory && !item.IsReparsePoint && depth > 0)
				{
					try
					{
						await ListSubtreeAsync(
							client,
							dirPath.Append(item.FileName),
							searchPattern,
							secInfo,
							options,
							item.RelativePath + '\\',
							depth - 1,
							stats,
							cancellationToken);
					}
					catch { }
				}
			}
		}

		private async Task<List<Smb2DirEntry>> GetDirectoryListingAsync(
			Smb2Client client,
			UncPath dirPath,
			string searchPattern,
			SecurityInfo secInfo,
			Smb2Directory.Smb2DirQueryOptions options,
			string? prefix,
			TraversalStats stats,
			CancellationToken cancellationToken)
		{
			stats.Directories++;

			this.WriteVerbose($"Traversing {dirPath}; found {stats.Files} files in {stats.Directories} directories");

			// Open directory
			await using (var dir = (Smb2Directory)await client.CreateFileAsync(dirPath, new Smb2CreateInfo()
			{
				CreateDisposition = Smb2CreateDisposition.Open,
				Priority = Smb2Priority.OpenDir,
				DesiredAccess = (uint)Smb2FileAccessRights.DefaultOpenDirAccess,
				ShareAccess = Smb2ShareAccess.DefaultDirShare,
				FileAttributes = Winterop.FileAttributes.None,
				CreateOptions = GetCreateOptions(Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.SynchronousIoNonalert),
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				RequestMaximalAccess = true,
				QueryOnDiskId = true,
				TimeWarpToken = this.TimeWarpToken?.Timestamp
				//OplockLevel = lease ? Smb2OplockLevel.Lease : Smb2OplockLevel.None,
				//LeaseInfo = lease
				//	? new Smb2LeaseInfo()
				//	{
				//		LeaseState = Smb2LeaseState.ReadCaching | Smb2LeaseState.HandleCaching,
				//		UseV2Struct = this.Session.Connection.Dialect >= Smb2Dialect.Smb3_0
				//	}
				//	: null
			}, FileAccess.Read, cancellationToken))
			{
				// Enumerate files in directory
				var listing = await dir.QueryDirAsync(
					searchPattern,
					options,
					secInfo,
					this.QueryBufferSize,
					cancellationToken);
				var items = listing.FindAll(r => r.FileName != "." && r.FileName != "..");
				if (prefix != null)
				{
					foreach (var item in items)
					{
						item.RelativePath = prefix + item.FileName;
					}
				}
				stats.Files += items.Count;

				return items;
			}
		}

		TreeHandler ISupportTreeOutput.CreateTreeHandler()
		{
			return new TreeHandler<string, Smb2DirEntry>(
				r => r.RelativePath,
				r => Path.GetDirectoryName(r.RelativePath),
				null,
				StringComparer.OrdinalIgnoreCase
				)
			{
				KeyField = nameof(Smb2DirEntry.RelativePath),
				KeyDisplayField = OutputField.CreateForProperty(typeof(Smb2DirEntry), nameof(Smb2DirEntry.FileName), this.VerifyContext().MetadataContext)
			};
		}
	}
}
