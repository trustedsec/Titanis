using System;
using System.Buffers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.IO
{
	/// <summary>
	/// Provides useful extension methods for <see cref="Stream"/> objects.
	/// </summary>
	public static class StreamExtensions
	{
		/// <summary>
		/// Reads a minimum number of bytes from a stream.
		/// </summary>
		/// <param name="stream">The stream to read data from.</param>
		/// <param name="buffer">An array to read the data into.</param>
		/// <param name="startIndex">The starting index of <paramref name="buffer"/> to receive bytes read from <paramref name="stream"/>.</param>
		/// <param name="minBytes">The minimum number of bytes to read.</param>
		/// <param name="maxBytes">The maximum number of bytes.</param>
		/// <returns>The number of bytes read into <paramref name="buffer"/>.</returns>
		/// <exception cref="StreamUnderflowException">Thrown when the stream does not contain at least <paramref name="minBytes"/>.</exception>
		/// <remarks>
		/// If this method encounters the end of stream before reading at least <paramref name="minBytes"/> bytes,
		/// it throws <see cref="StreamUnderflowException"/>.  The caller can extract the actual number of bytes
		/// read from the stream.
		/// </remarks>
		public static int ReadAtLeast(this Stream stream, byte[] buffer, int startIndex, int minBytes, int maxBytes)
		{
			if (stream is null)
				throw new ArgumentNullException(nameof(stream));
			if (buffer is null)
				throw new ArgumentNullException(nameof(buffer));
			ArgValidation.IsNonnegative(startIndex, nameof(startIndex));
			ArgValidation.IsNonnegative(minBytes, nameof(minBytes));
			ArgValidation.IsNonnegative(maxBytes, nameof(maxBytes));
			ArgValidation.IsMinMax(minBytes, maxBytes, nameof(minBytes));
			ArgValidation.IsValidRange(buffer, startIndex, maxBytes);

			int totalBytesRead = 0;
			do
			{
				int bytesRead = stream.Read(buffer, startIndex + totalBytesRead, maxBytes - totalBytesRead);
				if (bytesRead == 0)
					throw new StreamUnderflowException(totalBytesRead);

				totalBytesRead += bytesRead;
			} while (totalBytesRead < minBytes);

			return totalBytesRead;
		}
		/// <summary>
		/// Reads a minimum number of bytes from a stream.
		/// </summary>
		/// <param name="stream">The stream to read data from.</param>
		/// <param name="buffer">An array to read the data into.</param>
		/// <param name="minBytes">The minimum number of bytes to read.</param>
		/// <returns>The number of bytes read into <paramref name="buffer"/>.</returns>
		/// <exception cref="StreamUnderflowException">Thrown when the stream does not contain at least <paramref name="minBytes"/>.</exception>
		/// <remarks>
		/// If this method encounters the end of stream before reading at least <paramref name="minBytes"/> bytes,
		/// it throws <see cref="StreamUnderflowException"/>.  The caller can extract the actual number of bytes
		/// read from the stream.
		/// </remarks>
		public static int ReadAtLeast(this Stream stream, Span<byte> buffer, int minBytes)
		{
			if (stream is null)
				throw new ArgumentNullException(nameof(stream));
			ArgValidation.IsNonnegative(minBytes, nameof(minBytes));
			ArgValidation.IsNonnegative(buffer.Length, nameof(buffer));
			ArgValidation.IsMinMax(minBytes, buffer.Length, nameof(minBytes));

			int totalBytesRead = 0;
			do
			{
				int bytesRead = stream.Read(buffer.Slice(totalBytesRead));
				if (bytesRead == 0)
					throw new StreamUnderflowException(totalBytesRead);

				totalBytesRead += bytesRead;
			} while (totalBytesRead < minBytes);

			return totalBytesRead;
		}

		/// <summary>
		/// Reads data from a <see cref="Stream"/> until either the buffer is full or the end of the stream is encountered.
		/// </summary>
		/// <param name="stream"><see cref="Stream"/> to read from.</param>
		/// <param name="buffer">Buffer to read data into</param>
		/// <param name="startIndex">Start index into buffer</param>
		/// <param name="count">Number of bytes to read</param>
		/// <exception cref="EndOfStreamException">The end of stream was encountered before reading <paramref name="count"/> bytes.</exception>
		public static void ReadAll(this Stream stream, byte[] buffer, int startIndex, int count)
		{
			int cbTotalRead = 0;
			int cbChunkRead;
			while ((cbTotalRead < count) && (0 != (cbChunkRead = stream.Read(buffer, startIndex + cbTotalRead, count - cbTotalRead))))
			{
				cbTotalRead += cbChunkRead;
			}
			if (cbTotalRead < count)
				throw new EndOfStreamException();
		}

		/// <summary>
		/// Reads data from a <see cref="Stream"/> until either the buffer is full or the end of the stream is encountered.
		/// </summary>
		/// <param name="stream"><see cref="Stream"/> to read from.</param>
		/// <param name="buffer">Buffer to read data into</param>
		/// <param name="startIndex">Start index into buffer</param>
		/// <param name="count">Number of bytes to read</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <exception cref="EndOfStreamException">The end of stream was encountered before reading <paramref name="count"/> bytes.</exception>
		public static async ValueTask ReadAllAsync(this Stream stream, byte[] buffer, int startIndex, int count, CancellationToken cancellationToken)
		{
			int cbTotalRead = 0;
			int cbChunkRead;
			while (!cancellationToken.IsCancellationRequested && (cbTotalRead < count) && (0 != (cbChunkRead = await stream.ReadAsync(buffer, startIndex + cbTotalRead, count - cbTotalRead, cancellationToken))))
			{
				cbTotalRead += cbChunkRead;
			}
			if (cbTotalRead < count)
				throw new EndOfStreamException();
		}


		public static async Task<int> ReadAtLeastAsync(
			this Stream stream,
			byte[] buffer,
			int startIndex,
			int maxCount,
			int minBytes,
			CancellationToken cancellationToken)
		{
			if (stream is null)
				throw new ArgumentNullException(nameof(stream));
			ArgValidation.IsNonnegative(minBytes, nameof(minBytes));
			ArgValidation.IsNonnegative(buffer.Length, nameof(buffer));
			ArgValidation.IsMinMax(minBytes, buffer.Length, nameof(minBytes));

			int totalBytesRead = 0;
			do
			{
				int bytesRead = await stream.ReadAsync(buffer, startIndex + totalBytesRead, maxCount - totalBytesRead, cancellationToken);
				if (bytesRead == 0)
					throw new StreamUnderflowException(totalBytesRead);

				totalBytesRead += bytesRead;
			} while (!cancellationToken.IsCancellationRequested && totalBytesRead < minBytes);

			return totalBytesRead;
		}
		/// <summary>
		/// Copies data from one stream into another.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="destination"></param>
		/// <param name="bufferSize"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="ArgumentException"></exception>
		/// <remarks>
		/// This implementation offers more control over the buffer size than <see cref="Stream.CopyToAsync(Stream, int, CancellationToken)"/>.
		/// <see cref="Stream.CopyToAsync(Stream)"/> uses <paramref name="bufferSize"/> as a suggestion but relies
		/// on <see cref="ArrayPool{T}.Rent(int)"/> to determine the actual size.
		/// </remarks>
		public static async Task<long> CopyToAsync2(this Stream source, Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			if (source is null) throw new ArgumentNullException(nameof(source));
			if (destination is null) throw new ArgumentNullException(nameof(destination));
			if (bufferSize < 1)
				throw new ArgumentOutOfRangeException(nameof(bufferSize));

			if (!source.CanRead)
				throw new ArgumentException("Source stream not readable.", nameof(source));
			if (!destination.CanWrite)
				throw new ArgumentException("Destination stream not writable.", nameof(source));


			byte[] buffer = new byte[bufferSize];
			byte[] buffer2 = new byte[bufferSize];

			long cbTotalCopied = 0;
			int cbRead;
			ValueTask<int> readTask = source.ReadAsync(buffer, cancellationToken);
			while (0 != (cbRead = await readTask.ConfigureAwait(false)))
			{
				readTask = source.ReadAsync(buffer2, cancellationToken);
				await destination.WriteAsync(buffer.AsMemory().Slice(0, cbRead), cancellationToken).ConfigureAwait(false);

				cbTotalCopied += cbRead;

				(buffer, buffer2) = (buffer2, buffer);
			}

			return cbTotalCopied;
		}
	}
}
