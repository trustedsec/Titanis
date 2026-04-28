using Titanis.Linterop.Fuse;
using Titanis.Smb2.Fusion;

namespace Titanis.Smb2.Cli;

internal partial class SmbOpenDir : IFuseOpenDirectory
{
	internal SmbOpenDir(SharedDirNodeBase node, Smb2Directory dir)
	{
		this._node = node;
		this._dir = dir;
	}

	private readonly SharedDirNodeBase _node;
	private readonly Smb2Directory _dir;

	private int _readIndex;
	private List<Smb2DirEntry> _listing;

	IFuseNode IFuseOpenObject.Node => this._node;

	public long NextOffset => this._readIndex;

	public async Task<IFuseNode?> ReadNextAsync(CancellationToken cancellationToken)
	{
		if (this._readIndex == 0 || this._listing == null)
		{
			var listing = await _dir.QueryDirAsync(cancellationToken).ConfigureAwait(false);
			this._listing = listing;
		}

		if (this._readIndex < this._listing.Count)
		{
			var entry = this._listing[this._readIndex];
			this._readIndex++;

			return this._node.GetFileNode(entry);
		}
		else
			return null;
	}

	public void Seek(long offset)
	{
		this._readIndex = (int)offset;
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

			// TODO: free unmanaged resources (unmanaged objects) and override finalizer
			// TODO: set large fields to null
			disposedValue = true;
		}
	}

	// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
	// ~SmbOpenDir()
	// {
	//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
	//     Dispose(disposing: false);
	// }

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
