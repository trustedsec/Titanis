using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Net
{
	public interface IStreamTransportHandler
	{
		int ExtractMessageFrameSize(ReadOnlySpan<byte> buffer);
		Task HandleFrame(Memory<byte> frame);
	}

	public class StreamTransport
	{
		private readonly Stream _stream;

		public StreamTransport(Stream stream)
		{
			ArgumentNullException.ThrowIfNull(stream);

			this._stream = stream;
		}

		public bool IsEndOfStream { get; set; }

		public async Task Run(
			IStreamTransportHandler handler,
			int bufferSize,
			int headerSize,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(handler);

			if (bufferSize <= 0)
				throw new InvalidOperationException($"The buffer size must be > 0.  The implementation returned {bufferSize}.");
			if (headerSize > bufferSize)
				throw new InvalidOperationException($"The buffer size must be >= headerSize.");

			try
			{
				byte[] buf = new byte[bufferSize];
				int cbRecv = 0;
				while (!cancellationToken.IsCancellationRequested)
				{
					{
						int cbChunk = await this._stream.ReadAsync(buf.AsMemory(cbRecv, buf.Length - cbRecv)).ConfigureAwait(false);
						if (cbChunk == 0)
							// TODO: Communicate incomplete message
							break;
						cbRecv += cbChunk;
					}

					while (cbRecv >= headerSize)
					{
						var frameSize = handler.ExtractMessageFrameSize(buf.Slice(0, cbRecv));
						if (cbRecv >= frameSize)
						{
							await handler.HandleFrame(buf.AsMemory(0, frameSize)).ConfigureAwait(false);

							int cbRem = cbRecv - frameSize;
							if (cbRem > 0)
								buf.AsSpan(frameSize, cbRem).CopyTo(buf);

							cbRecv = cbRem;
						}
						else
							break;
					}
				}
			}
			finally
			{
				this.IsEndOfStream = true;
			}
		}
	}
}
