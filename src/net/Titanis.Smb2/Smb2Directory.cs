using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Smb2.Pdus;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Smb2
{
	/// <summary>
	/// Specifies options for <see cref="Smb2Directory.ReadChangesAsync(Smb2ChangeFilter, WatchOptions, int)"/>
	/// </summary>
	[Flags]
	public enum WatchOptions
	{
		None = 0,

		WatchSubtree = 1,
		ContinueOnError = 2,
	}

	/// <summary>
	/// Represents a directory open over an SMB2 share.
	/// </summary>
	/// <seealso cref="Smb2TreeConnect.OpenDirectoryAsync(string, CancellationToken)"/>
	public class Smb2Directory : Smb2OpenFileObjectBase
	{
		internal Smb2Directory(Smb2TreeConnect tree, string shareRelativePath, in Smb2FileOpenInfo fileInfo)
			: base(tree, shareRelativePath, fileInfo)
		{
		}

		public Task<List<Smb2DirEntry>> QueryDirAsync(CancellationToken cancellationToken)
			=> this.QueryDirAsync("*", Smb2DirQueryOptions.None, SecurityInfo.None, DefaultQueryBufferSize, cancellationToken);

		public class ChangeNotifyEnumerator : IAsyncEnumerator<FileChangeNotification>
		{
			private readonly Smb2Directory dir;
			private readonly Smb2ChangeNotifyRequest req;
			private readonly CancellationToken cancellationToken;
			private readonly bool continueOnError;
			private bool done;

			internal ChangeNotifyEnumerator(
				Smb2Directory dir,
				Smb2ChangeNotifyRequest req,
				bool continueOnError,
				CancellationToken cancellationToken
				)
			{
				this.dir = dir;
				this.req = req;
				this.cancellationToken = cancellationToken;
				this.continueOnError = continueOnError;
			}

			private Queue<FileChangeNotification> _notifs = new Queue<FileChangeNotification>();

			public FileChangeNotification? Current { get; private set; }

			public ValueTask DisposeAsync()
			{
				// TODO: Cancel the PDU but for now do nothing
				return ValueTask.CompletedTask;
			}

			public ValueTask<bool> MoveNextAsync()
			{
				if (this.cancellationToken.IsCancellationRequested)
					this.done = true;

				if (this.done)
					return ValueTask.FromResult(false);

				if (this._notifs.Count > 0)
				{
					this.Current = this._notifs.Dequeue();
					return ValueTask.FromResult(true);
				}
				else
					return this.SendRequest();
			}

			private async ValueTask<bool> SendRequest()
			{
				while (true)
				{
					Smb2ChangeNotifyResponse resp;
					try
					{
						resp = (Smb2ChangeNotifyResponse)await this.dir.Tree.SendSyncPduAsync(req, this.cancellationToken).ConfigureAwait(false);
					}
					catch (OperationCanceledException)
					{
						this.done = true;
						return false;
					}

					if (resp.pduhdr.status == Ntstatus.STATUS_NOTIFY_ENUM_DIR)
					{
						if (this.continueOnError)
							continue;
						else
							// TODO: Check for this error specifically
							throw new NtstatusException(resp.pduhdr.status, "The server returned an error when watching for directory notifications.  This is most likely due to the buffer size being too small.  Try specifing a larger buffer size.");
					}
					else if (resp.infos != null && resp.infos.Count > 0)
					{
						this.Current = resp.infos[0];
						for (int i = 1; i < resp.infos.Count; i++)
						{
							FileChangeNotification? item = resp.infos[i];
							this._notifs.Enqueue(item);
						}

						return true;
					}
					else
					{
						return false;
					}
				}
			}
		}

		public class ChangeNotifyEnumerable : IAsyncEnumerable<FileChangeNotification>
		{
			private readonly Smb2Directory dir;
			private Smb2ChangeNotifyRequest req;
			private readonly bool continueOnError;

			internal ChangeNotifyEnumerable(
				Smb2Directory dir,
				Smb2ChangeNotifyRequest req,
				bool continueOnError
				)
			{
				this.dir = dir;
				this.req = req;
				this.continueOnError = continueOnError;
			}

			public ChangeNotifyEnumerator GetAsyncEnumerator(CancellationToken cancellationToken = default)
			{
				return new ChangeNotifyEnumerator(this.dir, this.req, this.continueOnError, cancellationToken);
			}
			IAsyncEnumerator<FileChangeNotification> IAsyncEnumerable<FileChangeNotification>.GetAsyncEnumerator(CancellationToken cancellationToken = default)
				=> this.GetAsyncEnumerator(cancellationToken);
		}

		public ChangeNotifyEnumerable ReadChangesAsync(
			Smb2ChangeFilter filter,
			WatchOptions options,
			int bufferSize = 2048)
		{
			Smb2ChangeNotifyRequest req = new Smb2ChangeNotifyRequest
			{
				body = new Smb2ChangeNotifyRequestHeader
				{
					flags = (0 != (options & WatchOptions.WatchSubtree)) ? Smb2ChangeNotifyOptions.WatchTree : Smb2ChangeNotifyOptions.None,
					outputBufferLength = bufferSize,
					handle = this.Handle,
					filter = filter,
				}
			};

			return new ChangeNotifyEnumerable(this, req, 0 != (options & WatchOptions.ContinueOnError));
		}

		[Flags]
		public enum Smb2DirQueryOptions
		{
			None = 0,
			Recursive = 1,
			QueryReparseInfo = 2,
			QueryMaxAccessAllowed = 4,
		}

		public const int DefaultQueryBufferSize = 1024;

		public async Task<List<Smb2DirEntry>> QueryDirAsync(
			string searchPattern,
			Smb2DirQueryOptions options,
			SecurityInfo securityInfo,
			int bufferSize,
			CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(searchPattern))
				throw new ArgumentNullException(nameof(searchPattern));

			List<Smb2DirEntry> entries = new List<Smb2DirEntry>();

			var infoClass = this.Tree.ShareType == Smb2ShareType.Pipe
				? Smb2DirEntryInfoClass.BothDirInfo
				: Smb2DirEntryInfoClass.BothDirInfoId;

			do
			{
				Smb2QueryDirRequest req = new Smb2QueryDirRequest
				{
					body = new Smb2QueryDirRequestBody
					{
						infoClass = infoClass,
						flags = 0,
						// Windows default
						dirHandle = this.Handle,
						outputBufferLength = bufferSize,
					},
					searchPattern = searchPattern
				};

				Smb2QueryDirResponse resp;
				try
				{
					resp = (Smb2QueryDirResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);
				}
				catch (NtstatusException ex) when (ex.StatusCode == Ntstatus.STATUS_INVALID_INFO_CLASS
					&& infoClass == Smb2DirEntryInfoClass.BothDirInfoId)
				{
					// Fall back to basic info class
					infoClass = Smb2DirEntryInfoClass.DirInfo;
					continue;
				}
				catch (NtstatusException ex) when (ex.StatusCode == Ntstatus.STATUS_NO_MORE_FILES)
				{
					break;
				}

				this.ReadDirEntriesInto(resp.buf, entries, infoClass);
			} while (true);


			if (
				(0 != (options & Smb2DirQueryOptions.QueryReparseInfo))
				|| (0 != securityInfo)
				)
			{
				await QueryAdditionalFileInfoAsync(entries, options, securityInfo, cancellationToken).ConfigureAwait(false);
			}

			return entries;
		}

		private async Task QueryAdditionalFileInfoAsync(
			List<Smb2DirEntry> entries,
			Smb2DirQueryOptions options,
			SecurityInfo secInfo,
			CancellationToken cancellationToken)
		{
			var access = (Smb2FileAccessRights.ReadEa | Smb2FileAccessRights.ReadAttributes | Smb2FileAccessRights.Synchronize);
			if (secInfo != SecurityInfo.None)
			{
				access |= Smb2FileAccessRights.ReadControl;
				if (0 != (secInfo & SecurityInfo.Sacl))
					access |= Smb2FileAccessRights.AccessSystemSecurity;
			}

			foreach (var entry in entries)
			{
				bool reparse = (
						(0 != (entry.FileAttributes & Winterop.FileAttributes.ReparsePoint))
						&& (0 != (options & Smb2DirQueryOptions.QueryReparseInfo))
					);
				bool otherInfo = 0 != (options & ~Smb2DirQueryOptions.QueryReparseInfo);
				bool shouldOpen = reparse || otherInfo || (secInfo != 0);

				if (shouldOpen)
				{
					try
					{
						using (var file = await this.Tree.CreateFileAsync(Path.Combine(this.ShareRelativePath, entry.FileName), new Smb2CreateInfo
						{
							CreateDisposition = Smb2CreateDisposition.Open,
							DesiredAccess = (uint)access,
							ShareAccess = Smb2ShareAccess.ReadWriteDelete,
							ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
							CreateOptions = Smb2FileCreateOptions.SynchronousIoNonalert | Smb2FileCreateOptions.OpenReparsePoint,
							FileAttributes = 0,
							RequestMaximalAccess = (0 != (options & Smb2DirQueryOptions.QueryMaxAccessAllowed))
						}, FileAccess.Read, cancellationToken).ConfigureAwait(false))
						{
							if (reparse)
							{
								var reparseInfo = await file.GetReparseInfoAsync(cancellationToken).ConfigureAwait(false);

								entry.ReparseTag = reparseInfo.Tag;
								if (reparseInfo is SymbolicLinkInfo symlink)
								{
									entry.LinkTarget = symlink.PrintName;
								}
								else if (reparseInfo is MountPointInfo mount)
								{
									entry.LinkTarget = mount.PrintName;
								}
							}

							if (secInfo != 0)
							{
								entry.SecurityDescriptor = await file.GetSecurityAsync(secInfo, 4096, cancellationToken).ConfigureAwait(false);
							}

							entry.MaxAccess = file.MaximalAccessAllowed;
						}
					}
					catch { }
				}
			}
		}

		private List<Smb2DirEntry> ReadDirEntriesInto(
			Memory<byte> buf,
			List<Smb2DirEntry> entries,
			Smb2DirEntryInfoClass infoClass
			)
		{
			ByteMemoryReader reader = new ByteMemoryReader(buf);

			int offEntry = reader.Position;
			int next = 0;
			do
			{
				offEntry += next;
				reader.Position = offEntry;

				Smb2DirEntry entry;
				switch (infoClass)
				{
					case Smb2DirEntryInfoClass.DirInfo:
						entry = ReadDirInfo(reader, out next);
						break;
					case Smb2DirEntryInfoClass.FullDirInfo:
						entry = ReadFullDirInfo(reader, out next);
						break;
					case Smb2DirEntryInfoClass.FullDirInfoId:
						entry = ReadFullDirInfoId(reader, out next);
						break;
					case Smb2DirEntryInfoClass.BothDirInfo:
						entry = ReadBothDirInfo(reader, out next);
						break;
					case Smb2DirEntryInfoClass.BothDirInfoId:
						entry = ReadBothDirInfoId(reader, out next);
						break;
					case Smb2DirEntryInfoClass.NamesInfo:
						entry = ReadNamesInfo(reader, out next);
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(infoClass));
				}

				entries.Add(entry);

			} while (next > 0);

			return entries;
		}

		private static Smb2DirEntry ReadFullDirInfo(ByteMemoryReader reader, out int next)
		{
			ref readonly var fileInfoStruc = ref reader.ReadFileFullDirInfo();
			Smb2DirEntry entry = new Smb2DirEntry
			{
				FileIndex = fileInfoStruc.fileIndex,
				CreationTime = DateTime.FromFileTimeUtc(fileInfoStruc.creationTime),
				LastAccessTime = DateTime.FromFileTimeUtc(fileInfoStruc.lastAccessTime),
				LastWriteTime = DateTime.FromFileTimeUtc(fileInfoStruc.lastWriteTime),
				LastChangeTime = DateTime.FromFileTimeUtc(fileInfoStruc.changeTime),
				Size = fileInfoStruc.endOfFile,
				SizeOnDisk = fileInfoStruc.allocationSize,
				FileAttributes = fileInfoStruc.fileAttributes,
				EaSize = fileInfoStruc.eaSize,

				FileName = Encoding.Unicode.GetString(reader.ReadBytes(fileInfoStruc.fileNameLength)),
			};
			next = fileInfoStruc.nextEntryOffset;
			return entry;
		}

		private static Smb2DirEntry ReadFullDirInfoId(ByteMemoryReader reader, out int next)
		{
			ref readonly var fileInfoStruc = ref reader.ReadFileIdFullDirInfo();
			Smb2DirEntry entry = new Smb2DirEntry
			{
				FileIndex = fileInfoStruc.fileIndex,
				CreationTime = DateTime.FromFileTimeUtc(fileInfoStruc.creationTime),
				LastAccessTime = DateTime.FromFileTimeUtc(fileInfoStruc.lastAccessTime),
				LastWriteTime = DateTime.FromFileTimeUtc(fileInfoStruc.lastWriteTime),
				LastChangeTime = DateTime.FromFileTimeUtc(fileInfoStruc.changeTime),
				Size = fileInfoStruc.endOfFile,
				SizeOnDisk = fileInfoStruc.allocationSize,
				FileAttributes = fileInfoStruc.fileAttributes,
				EaSize = fileInfoStruc.eaSize,
				FileId = fileInfoStruc.fileId,

				FileName = Encoding.Unicode.GetString(reader.ReadBytes(fileInfoStruc.fileNameLength)),
			};
			next = fileInfoStruc.nextEntryOffset;
			return entry;
		}

		private static Smb2DirEntry ReadBothDirInfo(ByteMemoryReader reader, out int next)
		{
			ref readonly var fileInfoStruc = ref reader.ReadFileBothDirInfo();
			Smb2DirEntry entry = new Smb2DirEntry
			{
				FileIndex = fileInfoStruc.fileIndex,
				CreationTime = DateTime.FromFileTimeUtc(fileInfoStruc.creationTime),
				LastAccessTime = DateTime.FromFileTimeUtc(fileInfoStruc.lastAccessTime),
				LastWriteTime = DateTime.FromFileTimeUtc(fileInfoStruc.lastWriteTime),
				LastChangeTime = DateTime.FromFileTimeUtc(fileInfoStruc.changeTime),
				Size = fileInfoStruc.endOfFile,
				SizeOnDisk = fileInfoStruc.allocationSize,
				FileAttributes = fileInfoStruc.fileAttributes,
				EaSize = fileInfoStruc.eaSize,

				FileName = Encoding.Unicode.GetString(reader.ReadBytes(fileInfoStruc.fileNameLength)),
				ShortName = ShortNameHelper.GetShortName(in fileInfoStruc)
			};
			next = fileInfoStruc.nextEntryOffset;
			return entry;
		}

		private static Smb2DirEntry ReadBothDirInfoId(ByteMemoryReader reader, out int next)
		{
			ref readonly var fileInfoStruc = ref reader.ReadFileIdBothDirInfo();
			Smb2DirEntry entry = new Smb2DirEntry
			{
				FileIndex = fileInfoStruc.fileIndex,
				CreationTime = DateTime.FromFileTimeUtc(fileInfoStruc.creationTime),
				LastAccessTime = DateTime.FromFileTimeUtc(fileInfoStruc.lastAccessTime),
				LastWriteTime = DateTime.FromFileTimeUtc(fileInfoStruc.lastWriteTime),
				LastChangeTime = DateTime.FromFileTimeUtc(fileInfoStruc.changeTime),
				Size = fileInfoStruc.endOfFile,
				SizeOnDisk = fileInfoStruc.allocationSize,
				FileAttributes = fileInfoStruc.fileAttributes,
				EaSize = fileInfoStruc.eaSize,
				FileId = fileInfoStruc.fileId,

				FileName = Encoding.Unicode.GetString(reader.ReadBytes(fileInfoStruc.fileNameLength)),
				ShortName = ShortNameHelper.GetShortName(in fileInfoStruc)
			};
			next = fileInfoStruc.nextEntryOffset;
			return entry;
		}

		private static Smb2DirEntry ReadNamesInfo(ByteMemoryReader reader, out int next)
		{
			ref readonly var fileInfoStruc = ref reader.ReadFileNamesInfo();
			Smb2DirEntry entry = new Smb2DirEntry
			{
				FileIndex = fileInfoStruc.fileIndex,
				FileName = Encoding.Unicode.GetString(reader.ReadBytes(fileInfoStruc.fileNameLength)),
			};
			next = fileInfoStruc.nextEntryOffset;
			return entry;
		}

		private static Smb2DirEntry ReadDirInfo(ByteMemoryReader reader, out int next)
		{
			ref readonly var fileInfoStruc = ref reader.ReadFileDirInfo();
			Smb2DirEntry entry = new Smb2DirEntry
			{
				FileIndex = fileInfoStruc.fileIndex,
				CreationTime = DateTime.FromFileTimeUtc(fileInfoStruc.creationTime),
				LastAccessTime = DateTime.FromFileTimeUtc(fileInfoStruc.lastAccessTime),
				LastWriteTime = DateTime.FromFileTimeUtc(fileInfoStruc.lastWriteTime),
				LastChangeTime = DateTime.FromFileTimeUtc(fileInfoStruc.changeTime),
				Size = fileInfoStruc.endOfFile,
				SizeOnDisk = fileInfoStruc.allocationSize,
				FileAttributes = fileInfoStruc.fileAttributes,

				FileName = Encoding.Unicode.GetString(reader.ReadBytes(fileInfoStruc.fileNameLength)),
			};
			next = fileInfoStruc.nextEntryOffset;
			return entry;
		}
	}
}