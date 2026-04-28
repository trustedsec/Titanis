using System.Diagnostics;
using Titanis.Linterop.Fuse;
using Titanis.Msrpc.Mswkst;

namespace Titanis.Smb2.Fusion;

/// <summary>
/// Represents the root directory of a mounted SMB2 server.
/// </summary>
/// <remarks>
/// This node presents the shares as directories, including IPC$.
/// </remarks>
internal class ServerRootDir : IFuseOpenDirectory
{
	internal ServerRootDir(ServerRootNode node)
	{
		this._serverNode = node;
	}

	private readonly ServerRootNode _serverNode;
	private IList<ShareInfo>? _shares;

	IFuseNode IFuseOpenObject.Node => this._serverNode;

	/// <inheritdoc/>
	public long NextOffset { get; private set; }

	/// <inheritdoc/>
	public async Task<IFuseNode?> ReadNextAsync(CancellationToken cancellationToken)
	{
		if (this.NextOffset == 0 || this._shares == null)
		{
			this._shares = await _serverNode.GetShares(cancellationToken).ConfigureAwait(false);
		}

		Debug.Assert(this._shares != null);
		if (this.NextOffset < this._shares.Count)
		{
			var shareInfo = this._shares[(int)this.NextOffset];
			this.NextOffset++;

			var node = this._serverNode.GetShareNode(shareInfo.ShareName, shareInfo);
			return node;
		}
		else
			return null;
	}

	/// <inheritdoc/>
	public void Seek(long offset)
	{
		this.NextOffset = offset;
	}

	public void Dispose()
	{
	}
}
