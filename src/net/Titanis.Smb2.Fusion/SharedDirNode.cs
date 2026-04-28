using Titanis.Linterop.Fuse;

namespace Titanis.Smb2.Fusion;

sealed class SharedDirNode : SharedDirNodeBase
{
	private readonly Smb2DirEntry _dirEntry;

	internal SharedDirNode(SmbMountInfo mountInfo, UncPath path, Smb2DirEntry dirEntry) : base(mountInfo, path)
	{
		this._dirEntry = dirEntry;
	}

	/// <inheritdoc/>
	public sealed override string Name => this._dirEntry.FileName;

	/// <inheritdoc/>
	public sealed override DateTime? LastAccessTime => this._dirEntry.LastAccessTime;
	/// <inheritdoc/>
	public sealed override DateTime? LastWriteTime => this._dirEntry.LastWriteTime;
	/// <inheritdoc/>
	public sealed override DateTime? LastChangeTime => this._dirEntry.LastChangeTime;
}
