using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Linterop.Fuse;

namespace Titanis.Smb2.Fusion
{
	internal class SmbOpenFile : IFuseOpenFile
	{
		internal SmbOpenFile(SharedFileNode node, Smb2FileStream fileStream, FileAccess access)
		{
			this._node = node;
			this._fileStream = fileStream;
			this._access = access;
		}

		private readonly SharedFileNode _node;
		private readonly Smb2FileStream _fileStream;
		private readonly FileAccess _access;


		IFuseNode IFuseOpenObject.Node => this._node;

		public void Dispose()
		{
			this._node.UpdateAttrs(this._fileStream.File.CloseAsync(Smb2CloseOptions.QueryAttributes, CancellationToken.None).Result);
		}

		public Task FlushAsync(CancellationToken cancellationToken)
		{
			// libfuse calls this when closing a read-only file
			if (0 != (this._access & FileAccess.Write))
				return this._fileStream.FlushAsync(cancellationToken);
			else
				return Task.CompletedTask;
		}

		public Task FsyncAsync(CancellationToken cancellationToken)
		{
			if (0 != (this._access & FileAccess.Write))
				return this._fileStream.FlushAsync(cancellationToken);
			else
				return Task.CompletedTask;
		}

		public Task<int> ReadAsync(long offset, byte[] buf, CancellationToken cancellationToken)
		{
			return this._fileStream.File.ReadAsync(offset, buf, 1, Smb2ReadOptions.None, cancellationToken);
		}

		public async Task<int> WriteAsync(long startOffset, FuseBufferList bufferList, CancellationToken cancellationToken)
		{
			int cbWritten = 0;
			for (int i = 0; i < bufferList.BufferCount; i++)
			{
				var bytes = bufferList.GetBytes(i).ToArray();
				int cbChunk = await _fileStream.File.WriteAsync(startOffset, bytes, Smb2WriteOptions.None, cancellationToken).ConfigureAwait(false);

				startOffset += cbChunk;
				cbWritten += cbChunk;
			}

			return cbWritten;
		}
	}
}
