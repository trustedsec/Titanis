using System.Text;
using Titanis.DceRpc.Client;
using Titanis.Linterop.Fuse;
using Titanis.Msrpc;
using Titanis.Msrpc.Mswkst;

namespace Titanis.Smb2.Fusion;

/// <summary>
/// Represents an SMB server as a node within a filesystem.
/// </summary>
class ServerRootNode : IFuseNode, IFuseNodeSource<ShareNode, ShareInfo>
{
	internal ServerRootNode(SmbMountInfo mountInfo, RpcClient rpcClient, UncPath serverPath)
	{
		this._mountInfo = mountInfo;
		this._rpcClient = rpcClient;
		this._serverPath = serverPath;

		this._nodeCache = new FuseNodeCache<ShareNode, ShareInfo>(this);
		this._shares = new CacheElement<IList<ShareInfo>>(this.RefreshShares);

		this._mountTime = DateTime.UtcNow;
	}

	private static readonly TimeSpan DefaultTimeoutMilliseconds = new TimeSpan(30 * 1000);
	private readonly SmbMountInfo _mountInfo;
	private readonly RpcClient _rpcClient;
	private readonly UncPath _serverPath;

	private ServerServiceClient? _srvs;

	/// <inheritdoc/>
	public string Name => _serverPath.ServerName;

	/// <inheritdoc/>
	public PosixFileMode Mode => this._mountInfo.defaultDirAccess;

	/// <inheritdoc/>
	public LinuxFileType FileType => LinuxFileType.Directory;

	public uint Uid => this._mountInfo.uid;

	public uint Gid => this._mountInfo.gid;

	public long FileSize { get => 0x1000; set => throw new NotSupportedException(); }

	public long BlockSize => 0x1000;

	public long BlockCount => 8;

	public DateTime? LastAccessTime { get => this._mountTime; set => throw new NotSupportedException(); }

	public DateTime? LastWriteTime { get => this._mountTime; set => throw new NotSupportedException(); }

	public DateTime? LastChangeTime { get => this._mountTime; set => throw new NotSupportedException(); }

	private async ValueTask<ServerServiceClient> GetSrvs(CancellationToken cancellationToken)
	{
		return (this._srvs ??= await ConnectService(cancellationToken).ConfigureAwait(false));
	}
	private async Task<ServerServiceClient> ConnectService(CancellationToken cancellationToken)
	{
		this._rpcClient.DefaultAuthLevel = DceRpc.RpcAuthLevel.None;
		ServerServiceClient srvs = new ServerServiceClient();

		await _rpcClient.ConnectPipe(
			srvs,
			this._mountInfo.smbClient,
			_serverPath.Append(Smb2Client.IpcName).Append(srvs.WellKnownPipeName),
			cancellationToken
			).ConfigureAwait(false);

		return srvs;
	}

	/// <inheritdoc/>
	public Task<IFuseOpenDirectory> OpenDirectory(CancellationToken cancellationToken)
	{
		var dir = new ServerRootDir(this);
		return Task.FromResult<IFuseOpenDirectory>(dir);
	}

	public Task<IFuseOpenFile> OpenFile(FuseOpenFlags openFlags, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	#region Shares
	private CacheElement<IList<ShareInfo>> _shares;
	private readonly DateTime _mountTime;
	private Dictionary<string, ShareInfo> _sharesByName;
	private async Task<IList<ShareInfo>> RefreshShares(CancellationToken cancellationToken)
	{
		var srvs = await GetSrvs(cancellationToken).ConfigureAwait(false);
		var shares = await srvs.GetShares(cancellationToken).ConfigureAwait(false);
		this._sharesByName = shares.ToDictionary(r => r.ShareName);
		return shares;
	}
	internal ValueTask<IList<ShareInfo>> GetShares(CancellationToken cancellationToken) => this._shares.GetValue(DefaultTimeoutMilliseconds, cancellationToken);
	#endregion

	#region Nodes
	private readonly FuseNodeCache<ShareNode, ShareInfo> _nodeCache;

	/// <inheritdoc/>
	ShareNode IFuseNodeSource<ShareNode, ShareInfo>.GetNode(string name, ShareInfo arg)
	{
		if (!(arg is ShareInfo shareInfo))
			throw new ArgumentException(nameof(arg));

		return new ShareNode(this._mountInfo, this._serverPath.Append(name), shareInfo.ShareType, shareInfo);
	}

	internal ShareNode GetShareNode(string name, ShareInfo shareInfo)
	{
		return this._nodeCache.GetNode(name, shareInfo);
	}

	public string[]? GetXAttributeNames() => null;

	public ValueTask<XAttrData> GetXAttribute(string name, int bufferSize, CancellationToken cancellationToken)
	{
		return ValueTask.FromResult(XAttrData.NotPresent);
	}

	/// <inheritdoc/>
	public async Task<IFuseNode?> Lookup(string name, CancellationToken cancellationToken)
	{
		var cached = this._nodeCache.TryGetNode(name);
		if (cached != null)
			return cached;

		var shares = await GetShares(cancellationToken).ConfigureAwait(false);
		if (this._sharesByName.TryGetValue(name, out var share))
		{
			return this._nodeCache.GetNode(name, share);
		}

		throw new FileNotFoundException();
	}

	public Task<IFuseOpenFile> CreateFile(string name, FuseOpenFlags flags, CancellationToken cancellationToken)
	{
		throw new NotSupportedException();
	}

	Task<IFuseNode> IFuseNode.CreateDirectory(string name, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	Task IFuseNode.DeleteDirectory(string name, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	Task IFuseNode.DeleteFile(string name, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
	#endregion
}
