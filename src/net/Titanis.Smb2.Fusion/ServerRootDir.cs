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
internal class ServerRootDir : OpenDirBase
{
	internal ServerRootDir(ServerRootNode node)
	{
		this._serverNode = node;
	}

	private readonly ServerRootNode _serverNode;
	private IList<ShareInfo>? _shares;

	/// <inheritdoc/>
	public override IFuseNode Node => this._serverNode;

	/// <inheritdoc/>
	protected override async Task<IFuseNode?> ReadNextAsync(int index, CancellationToken cancellationToken)
	{
		if (index == 0 || this._shares == null)
		{
			this._shares = await _serverNode.GetShares(cancellationToken).ConfigureAwait(false);
		}

		Debug.Assert(this._shares != null);
		if (index < this._shares.Count)
		{
			var shareInfo = this._shares[index];

			var node = this._serverNode.GetShareNode(shareInfo.ShareName, shareInfo);
			return node;
		}
		else
			return null;
	}
}
