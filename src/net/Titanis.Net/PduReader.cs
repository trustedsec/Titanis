using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Net
{
	public interface IPduReadHandler
	{
		int HeaderSize { get; }
		int ExtractPacketSize(ReadOnlySpan<byte> header);
	}

	public static class PduReader
	{
		public static async Task<int> ReadPduAsync<THandler>(this Stream stream,
			Memory<byte> buffer,
			int cbRecv,
			THandler handler,
			CancellationToken cancellationToken)
			where THandler : IPduReadHandler
		{
			int cbChunk;
			while (cbRecv < handler.HeaderSize)
			{
				cbChunk = await stream.ReadAsync(buffer.Slice(cbRecv, buffer.Length - cbRecv), cancellationToken).ConfigureAwait(false);
				if (cbChunk == 0)
					return 0;

				cbRecv += cbChunk;
			}

			if (cbRecv >= handler.HeaderSize)
			{
				var cbPacket = handler.ExtractPacketSize(buffer.Span);
				if (cbRecv < cbPacket)
				{
					do
					{
						cbChunk = await stream.ReadAsync(buffer.Slice(cbRecv, buffer.Length - cbRecv), cancellationToken).ConfigureAwait(false);
						cbRecv += cbChunk;
					} while (cbChunk > 0 && cbRecv < cbPacket);

					if (cbChunk == 0)
						throw new EndOfStreamException("Partial PDU received");
				}

				return cbRecv;
			}
			else
				throw new EndOfStreamException("Partial PDU received");
		}
	}
}
