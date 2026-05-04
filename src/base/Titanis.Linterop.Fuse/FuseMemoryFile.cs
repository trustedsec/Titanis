using Titanis.Linterop.Fuse;

namespace Titanis.Linterop.Fuse
{
	public class FuseMemoryFile : IFuseOpenFile
	{
		public FuseMemoryFile(IFuseOpenFileOwner owner)
		{
			ArgumentNullException.ThrowIfNull(owner);

			this.Owner = owner;
		}

		public IFuseOpenFileOwner Owner { get; }
		public IFuseNode Node => this.Owner;
		public byte[] ContentsBuffer => this.Owner.Contents;


		public bool IsModified { get; set; }

		public virtual void Dispose() => this.CommitIfDirty(CancellationToken.None);
		public virtual Task FlushAsync(CancellationToken cancellationToken) => Task.CompletedTask;
		public virtual Task FsyncAsync(CancellationToken cancellationToken) => CommitIfDirty(cancellationToken);

		private async Task CommitIfDirty(CancellationToken cancellationToken)
		{
			if (this.IsModified)
			{
				await Owner.Commit(cancellationToken).ConfigureAwait(false);
				this.IsModified = false;
			}
		}

		public async Task<int> ReadAsync(long startOffset, byte[] buf, CancellationToken cancellationToken)
		{
			var contents = this.ContentsBuffer;
			int length = (int)Math.Min(contents.Length, this.Owner.FileSize);
			int count = (int)Math.Min(length - startOffset, buf.Length);

			contents.Slice(checked((int)startOffset), count).CopyTo(buf);

			return count;
		}

		public Task<int> WriteAsync(long startOffset, FuseBufferList bufferList, CancellationToken cancellationToken)
		{
			int cbWritten = 0;
			for (int i = 0; i < bufferList.BufferCount; i++)
			{
				cbWritten += (int)bufferList.GetBufferSize(i);
			}

			// TODO: Lock buffer, handle multithreading

			this.Owner.GrowFileTo(checked((int)startOffset) + cbWritten);
			var contents = this.ContentsBuffer;

			this.IsModified = true;

			cbWritten = 0;
			for (int i = 0; i < bufferList.BufferCount; i++)
			{
				var chunk = bufferList.GetBytes(i);
				chunk.CopyTo(contents.Slice(checked((int)startOffset + cbWritten), chunk.Length));

				cbWritten += chunk.Length;
			}

			return Task.FromResult(cbWritten);
		}
	}
}
