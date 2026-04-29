using Titanis.Linterop.Fuse;
using Titanis.Smb2.Fusion;

namespace Titanis.Smb2.Cli;

internal partial class SmbOpenDir : OpenDirBase
{
	internal SmbOpenDir(SharedDirNodeBase node, Smb2Directory dir)
	{
		this._node = node;
		this._dir = dir;
	}

	private readonly SharedDirNodeBase _node;
	private readonly Smb2Directory _dir;

	private List<Smb2DirEntry> _listing;

	public override IFuseNode Node => this._node;


	protected override async Task<IFuseNode?> ReadNextAsync(int index, CancellationToken cancellationToken)
	{
		if (index == 0 || this._listing == null)
		{
			var listing = await _dir.QueryDirAsync(cancellationToken).ConfigureAwait(false);
			this._listing = listing;
		}

		if (index < this._listing.Count)
		{
			var entry = this._listing[index];
			return this._node.GetFileNode(entry);
		}
		else
			return null;
	}
}

partial class SmbOpenDir : IDisposable
{
	private bool disposedValue;

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				// TODO: dispose managed state (managed objects)
			}

			disposedValue = true;
		}
	}

	public override void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
