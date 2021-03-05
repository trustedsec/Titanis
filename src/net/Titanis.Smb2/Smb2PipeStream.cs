using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Smb2
{
	/// <summary>
	/// Provides a <see cref="Stream"/> for accessing data within an <see cref="Smb2Pipe"/>
	/// </summary>
	/// <seealso cref="Smb2Pipe.GetStream(bool)"/>
	public sealed class Smb2PipeStream : Smb2FileStreamBase, IAsyncPipeStream
	{
		internal Smb2PipeStream(Smb2Pipe pipe, FileAccess access, bool ownsFile) : base(pipe, access, ownsFile)
		{
			this._pipe = pipe;
		}

		private Smb2Pipe _pipe;

		/// <inheritdoc/>
		public sealed override bool CanSeek => false;
		/// <inheritdoc/>
		public sealed override long Length => throw new NotSupportedException();
		/// <inheritdoc/>
		public sealed override long Position
		{
			get => throw new NotSupportedException();
			set => throw new NotSupportedException();
		}

		/// <inheritdoc/>
		public sealed override long Seek(long offset, SeekOrigin origin)
			=> throw new NotSupportedException();

		/// <inheritdoc/>
		public sealed override void SetLength(long value)
			=> throw new System.NotImplementedException();

		public Task<int> Transceive(
			ReadOnlyMemory<byte> bytesToSend,
			Memory<byte> responseBuffer,
			CancellationToken cancellationToken)
			=> this._pipe.TransceiveAsync(bytesToSend, responseBuffer, cancellationToken);
	}
}