using Titanis.Linterop.Fuse;
using Titanis.Msrpc.Mswkst;

namespace Titanis.Smb2.Fusion;

/// <summary>
/// Represents an SMB2 share.
/// </summary>
sealed class ShareNode : SharedDirNodeBase
{
	internal ShareNode(SmbMountInfo mountInfo, UncPath path, ShareTypeFlags shareType, ShareInfo? shareInfo)
		: base(mountInfo, path)
	{
		this.Name = path.ShareName;
		this.shareType = shareType;
		this._shareInfo = shareInfo;
		this._mountTime = DateTime.UtcNow;
	}

	private readonly ShareTypeFlags shareType;
	private readonly ShareInfo? _shareInfo;
	private readonly DateTime _mountTime;

	protected override LinuxFileType ChildFileType => ((this.shareType & ShareTypeFlags.TypeMask) == ShareTypeFlags.Ipc) ? LinuxFileType.Socket : LinuxFileType.RegularFile;

	/// <inheritdoc/>
	public sealed override string Name { get; }

	/// <inheritdoc/>
	public sealed override DateTime? LastAccessTime => this._mountTime;
	/// <inheritdoc/>
	public sealed override DateTime? LastWriteTime => this._mountTime;
	/// <inheritdoc/>
	public sealed override DateTime? LastChangeTime => this._mountTime;
}
