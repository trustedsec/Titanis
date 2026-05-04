using Titanis.Linterop.Fuse;
using Titanis.Smb2.Cli;

namespace Titanis.Smb2.Fusion;

internal sealed class SharedFileNode : SmbFileNodeBase
{
	internal SharedFileNode(SmbMountInfo mountInfo, UncPath path, Smb2DirEntry dirEntry, LinuxFileType fileType) : base(mountInfo, path)
	{
		this._dirEntry = dirEntry;
		this.FileType = fileType;
	}

	private readonly Smb2DirEntry _dirEntry;

	/// <inheritdoc/>
	public sealed override string Name => this._dirEntry.FileName;

	/// <inheritdoc/>
	public sealed override PosixFileMode Mode => this._mountInfo.defaultFileAccess;

	/// <inheritdoc/>
	public sealed override LinuxFileType FileType { get; }

	/// <inheritdoc/>
	public sealed override long FileSize => (long)this._dirEntry.Size;

	public sealed override FileAttributes NtfsAttributes => (FileAttributes)this._dirEntry.FileAttributes;

	/// <inheritdoc/>
	public sealed override Task<IFuseNode?> Lookup(string name, CancellationToken cancellationToken) => throw new NotSupportedException();

	public override async Task<IFuseOpenFile> OpenFile(FuseOpenFlags openFlags, CancellationToken cancellationToken)
	{
		(FileAccess fileAccess, Smb2FileAccessRights accessRights) =
			(0 != (openFlags & FuseOpenFlags.Append)) ? (FileAccess.Write, Smb2FileAccessRights.DefaultOpenAppendAccess)
			: (openFlags & FuseOpenFlags.AccessMask) switch
			{
				FuseOpenFlags.ReadOnly => (FileAccess.Read, Smb2FileAccessRights.DefaultOpenReadAccess),
				FuseOpenFlags.WriteOnly => (FileAccess.Write, Smb2FileAccessRights.DefaultOpenWriteAccess),
				FuseOpenFlags.ReadWrite => (FileAccess.ReadWrite, Smb2FileAccessRights.DefaultOpenReadWriteAccess),
			};

		var modeFlags = (openFlags & (FuseOpenFlags.Create | FuseOpenFlags.Exclusive | FuseOpenFlags.Truncate | FuseOpenFlags.Exclusive));
		FileMode mode = modeFlags switch
		{
			0 => FileMode.Open,
			FuseOpenFlags.Create => FileMode.OpenOrCreate,
			(FuseOpenFlags.Create | FuseOpenFlags.Exclusive) => FileMode.Create,
			(FuseOpenFlags.Create | FuseOpenFlags.Truncate) => FileMode.CreateNew,
			FuseOpenFlags.Truncate => FileMode.Truncate,
		};

		var file = await this.Client.CreateFileAsync(this.SharedPath.ToString(), mode, fileAccess, FileShare.Read, cancellationToken, this._mountInfo.extraCreateOptions).ConfigureAwait(false);
		return new SmbOpenFile(this, file, fileAccess);
	}


	internal void UpdateAttrs(Smb2OpenFileAttributes attrs)
	{
		this._dirEntry.CreationTime = DateTime.FromFileTimeUtc(attrs.creationTime);
		this._dirEntry.LastAccessTime = DateTime.FromFileTimeUtc(attrs.lastAccessTime);
		this._dirEntry.LastWriteTime = DateTime.FromFileTimeUtc(attrs.lastWriteTime);
		this._dirEntry.LastChangeTime = DateTime.FromFileTimeUtc(attrs.changeTime);
		this._dirEntry.SizeOnDisk = (ulong)attrs.allocationSize;
		this._dirEntry.Size = (ulong)attrs.endOfFile;
		this._dirEntry.FileAttributes = attrs.fileAttributes;
	}

	/// <inheritdoc/>
	public sealed override DateTime? LastAccessTime => this._dirEntry.LastAccessTime;
	/// <inheritdoc/>
	public sealed override DateTime? LastWriteTime => this._dirEntry.LastWriteTime;
	/// <inheritdoc/>
	public sealed override DateTime? LastChangeTime => this._dirEntry.LastChangeTime;
}