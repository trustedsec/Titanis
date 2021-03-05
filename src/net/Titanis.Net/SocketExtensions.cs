using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Net
{
	/// <summary>
	/// Provides extension methods for <see cref="Socket"/> objects.
	/// </summary>
	public static class SocketExtensions
	{
		/// <summary>
		/// Receives bytes from a socket into a buffer, and does not return until
		/// all bytes are read.
		/// </summary>
		/// <param name="socket">Stream <see cref="Socket"/> to read from.</param>
		/// <param name="buffer">Buffer to receive bytes into</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <exception cref="EndOfStreamException"><paramref name="socket"/> is closed before <paramref name="buffer"/> is filled.</exception>
		public static async Task ReceiveAllAsync(
			this Socket socket,
			Memory<byte> buffer,
			CancellationToken cancellationToken
			)
		{
			int cbTotalRecv = 0;
			while (cbTotalRecv < buffer.Length)
			{
				int cbRecv = await socket.ReceiveAsync(
					buffer.Slice(cbTotalRecv),
					SocketFlags.None,
					cancellationToken
					).ConfigureAwait(false);
				if (cbRecv == 0)
					throw new EndOfStreamException();

				cbTotalRecv += cbRecv;
			}
		}

		/// <summary>
		/// Receives bytes from a socket into a buffer, and does not return until
		/// a minimum number of bytes is read.
		/// </summary>
		/// <param name="socket">Stream <see cref="Socket"/> to read from.</param>
		/// <param name="buffer">Buffer to receive bytes into</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <exception cref="EndOfStreamException"><paramref name="socket"/> is closed before <paramref name="minCount"/> is read.</exception>
		public static async Task<int> ReceiveAtLeastAsync(
			this Socket socket,
			Memory<byte> buffer,
			int minCount,
			CancellationToken cancellationToken
			)
		{
			if (buffer.Length < minCount)
				throw new ArgumentException(Messages.Error_BufferLessThanMinCount, nameof(buffer));

			int cbTotalRecv = 0;
			while (cbTotalRecv < minCount)
			{
				int cbRecv = await socket.ReceiveAsync(
					buffer.Slice(cbTotalRecv),
					SocketFlags.None,
					cancellationToken
					).ConfigureAwait(false);
				if (cbRecv == 0)
					throw new EndOfStreamException();

				cbTotalRecv += cbRecv;
			}

			return cbTotalRecv;
		}

		public static AddressFamily AddressFamilyOrDefault(this EndPoint address, AddressFamily defaultValue)
		{
			var family = address.AddressFamily;
			if (family == AddressFamily.Unspecified)
				family = defaultValue;
			return family;
		}
	}
}
