using System.Threading;
using Titanis.Linterop.Fuse;
using Titanis.Smb2.Cli;

namespace Titanis.Smb2.Fusion;

/// <summary>
/// Base class for directory nodes based on a share.
/// </summary>
abstract class SharedDirNodeBase : SmbFileNodeBase, IFuseNodeSource<SmbFileNodeBase, Smb2DirEntry>
{
	internal SharedDirNodeBase(SmbMountInfo mountInfo, UncPath path)
		: base(mountInfo, path)
	{
		this._nodeCache = new FuseNodeCache<SmbFileNodeBase, Smb2DirEntry>(this, StringComparer.OrdinalIgnoreCase);
	}

	private Smb2Directory? _openDir;

	/// <inheritdoc/>
	public sealed override LinuxFileType FileType => LinuxFileType.Directory;

	/// <inheritdoc/>
	public sealed override long FileSize => 0x1000;
	/// <inheritdoc/>
	public sealed override PosixFileMode Mode => this._mountInfo.defaultDirAccess;

	protected virtual LinuxFileType ChildFileType => LinuxFileType.RegularFile;

	private async ValueTask<Smb2Directory> OpenSmbDir(CancellationToken cancellationToken)
	{
		if (this._openDir == null)
		{
			var dir = await this.Client.OpenDirectoryAsync(this.SharedPath, cancellationToken, extraOptions: this._mountInfo.extraCreateOptions).ConfigureAwait(false);
			this._openDir = dir;
		}

		return this._openDir;
	}

	/// <inheritdoc/>
	public sealed override async Task<IFuseOpenDirectory> OpenDirectory(CancellationToken cancellationToken)
	{
		var dir = await this.Client.OpenDirectoryAsync(this.SharedPath, cancellationToken, extraOptions: this._mountInfo.extraCreateOptions).ConfigureAwait(false);
		return new SmbOpenDir(this, dir);
	}

	private readonly FuseNodeCache<SmbFileNodeBase, Smb2DirEntry> _nodeCache;

	public sealed override async Task<IFuseNode?> Lookup(string name, CancellationToken cancellationToken)
	{
		var cached = this._nodeCache.TryGetNode(name);
		if (cached != null)
			return cached;

		var dir = await OpenSmbDir(cancellationToken).ConfigureAwait(false);
		var info = await dir.QueryDirAsync(name, Smb2Directory.Smb2DirQueryOptions.QueryMaxAccessAllowed, Winterop.Security.SecurityInfo.None, Smb2Directory.DefaultQueryBufferSize, cancellationToken).ConfigureAwait(false);

		var info0 = info.FirstOrDefault();
		if (info0 is null || !info0.FileName.Equals(name, StringComparison.OrdinalIgnoreCase))
			throw new LinuxException(LinuxErrorCode.ENOENT);

		return this._nodeCache.GetNode(name, info0);
	}

	public override async Task<IFuseOpenFile> CreateFile(string name, FuseOpenFlags flags, CancellationToken cancellationToken)
	{
		var path = this.SharedPath.Append(name);

		var file = await this.Client.CreateFileAsync(path, cancellationToken, this._mountInfo.extraCreateOptions).ConfigureAwait(false);
		var node = (SharedFileNode)this._nodeCache.GetNode(name, file.File.GetDirectoryEntry());
		return new SmbOpenFile(node, file, FileAccess.ReadWrite);
	}

	public override async Task<IFuseNode> CreateDirectory(string name, CancellationToken cancellationToken)
	{
		var path = this.SharedPath.Append(name);
		var dir = await this.Client.CreateDirectoryAsync(path, cancellationToken, this._mountInfo.extraCreateOptions).ConfigureAwait(false);
		var node = new SharedDirNode(this._mountInfo, path, dir.GetDirectoryEntry());
		return node;
	}

	/// <inheritdoc/>
	SmbFileNodeBase IFuseNodeSource<SmbFileNodeBase, Smb2DirEntry>.GetNode(string name, Smb2DirEntry arg)
	{
		if (arg.IsDirectory)
			return new SharedDirNode(this._mountInfo, this.SharedPath.Append(arg.FileName), arg);
		else
			return new SharedFileNode(this._mountInfo, this.SharedPath.Append(arg.FileName), arg, this.ChildFileType);
	}

	internal IFuseNode? GetFileNode(Smb2DirEntry entry)
	{
		return this._nodeCache.GetNode(entry.FileName, entry);
	}


	public override async Task DeleteDirectory(string name, CancellationToken cancellationToken)
	{
		var path = this.SharedPath.Append(name);
		await this.Client.RemoveDirectoryAsync(path, cancellationToken).ConfigureAwait(false);
	}

	public override async Task DeleteFile(string name, CancellationToken cancellationToken)
	{
		var path = this.SharedPath.Append(name);
		await this.Client.DeleteFileAsync(path, cancellationToken).ConfigureAwait(false);
	}
}
