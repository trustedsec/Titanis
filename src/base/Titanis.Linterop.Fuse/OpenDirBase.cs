using Titanis.Linterop.Fuse;

namespace Titanis.Linterop.Fuse;

public abstract class OpenDirBase : IFuseOpenDirectory
{
	public abstract IFuseNode Node { get; }

	async Task<IFuseNode?> IFuseOpenDirectory.ReadNextAsync(CancellationToken cancellationToken)
	{
		if (this._nextOffset < 1)
		{
			var node = this.Node;
			this._nextOffset++;
			return node;
		}
		else
		{
			var node = await ReadNextAsync(this._nextOffset - 1, cancellationToken).ConfigureAwait(false);
			if (node != null)
				this._nextOffset++;
			return node;
		}
	}
	protected abstract Task<IFuseNode?> ReadNextAsync(int index, CancellationToken cancellationToken);

	private int _nextOffset;
	/// <inheritdoc/>
	long IFuseOpenDirectory.NextOffset => this._nextOffset;

	/// <inheritdoc/>
	public void Seek(long offset)
	{
		this._nextOffset = checked((int)offset);
	}

	public virtual void Dispose()
	{
	}
}
