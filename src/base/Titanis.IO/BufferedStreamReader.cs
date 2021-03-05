using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.IO
{
	/// <summary>
	/// Implements a <see cref="BufferedTextReader"/> that reads from a stream.
	/// </summary>
	public class BufferedStreamReader : BufferedTextReader
	{
		/// <summary>
		/// Default text buffer size
		/// </summary>
		public const int DefaultBufferSize = 4 * 1024;

		/// <summary>
		/// Initializes a new <see cref="BufferedStreamReader"/>
		/// </summary>
		/// <param name="stream">Source stream</param>
		/// <param name="encoding">Text encoding</param>
		/// <param name="bufferSize">Size of text buffer, in characters</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="stream"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="encoding"/> is <c>null</c>.</exception>
		public BufferedStreamReader(
			Stream stream,
			Encoding encoding,
			int bufferSize = DefaultBufferSize)
		{
			if (stream is null)
				throw new ArgumentNullException(nameof(stream));
			if (encoding is null)
				throw new ArgumentNullException(nameof(encoding));
			ArgValidation.IsPositiveNonzero(bufferSize, nameof(bufferSize));

			this.UnderlyingStream = stream;

			this._encoding = encoding;
			this._decoder = encoding.GetDecoder();

			this._textBuffer = new char[bufferSize];
			this._streamBuffer = new byte[bufferSize];
		}
		/// <summary>
		/// Initializes a new <see cref="BufferedStreamReader"/>
		/// </summary>
		/// <param name="stream">Source stream</param>
		/// <param name="encoding">Text encoding</param>
		/// <param name="bufferSize">Size of text buffer, in characters</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="stream"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="encoding"/> is <c>null</c>.</exception>
		public BufferedStreamReader(
			Stream stream,
			Encoding encoding,
			bool ownsStream,
			int bufferSize = DefaultBufferSize)
		{
			if (stream is null)
				throw new ArgumentNullException(nameof(stream));
			if (encoding is null)
				throw new ArgumentNullException(nameof(encoding));
			ArgValidation.IsPositiveNonzero(bufferSize, nameof(bufferSize));

			this.UnderlyingStream = stream;
			this._ownsStream = ownsStream;

			this._encoding = encoding;
			this._decoder = encoding.GetDecoder();

			this._textBuffer = new char[bufferSize];
			this._streamBuffer = new byte[bufferSize];
		}

		/// <summary>
		/// Gets the underlying stream.
		/// </summary>
		public Stream UnderlyingStream { get; }
		private bool _ownsStream;

		/// <inheritdoc/>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this._ownsStream)
					this.UnderlyingStream.Dispose();
			}
		}

		/// <inheritdoc/>
		public void Seek(int offset)
		{
			this.UnderlyingStream.Position = offset;
			this.ClearBuffer();
		}

		/// <summary>
		/// Clears the buffer
		/// </summary>
		private void ClearBuffer()
		{
			this._cchValid = 0;
			this._eof = false;
			this._readIndex = 0;
			this._cbStreamBuf = 0;

			this._decoder = this._encoding.GetDecoder();
		}

		/// <inheritdoc/>
		public override ReadOnlySpan<char> GetBufferedText()
		{
			return this._textBuffer.AsSpan(this._readIndex, this._cchValid);
		}

		/// <summary>
		/// Gets the number of valid characters in the buffer.
		/// </summary>
		private int _cchValid;
		/// <summary>
		/// Gets the text buffer.
		/// </summary>
		private char[] _textBuffer;
		/// <summary>
		/// Gets the index of the first valid character in the buffer.
		/// </summary>
		private int _readIndex;

		/// <summary>
		/// Gets the stream buffer.
		/// </summary>
		private byte[] _streamBuffer;
		/// <summary>
		/// Gets the number of valid bytes in the stream buffer.
		/// </summary>
		private int _cbStreamBuf;
		/// <summary>
		/// Gets a value indicating whether the end of the stream has been reached.
		/// </summary>
		private bool _eof;

		/// <summary>
		/// Gets the text encoding.
		/// </summary>
		private Encoding _encoding;
		/// <summary>
		/// Gets the text decoder.
		/// </summary>
		private Decoder _decoder;

		/// <summary>
		/// Converts a chunk of stream data to characters.
		/// </summary>
		/// <param name="cchToFill">Number of characters to fill</param>
		private void ConvertStreamChunk(int cchToFill)
		{
			int cb = this._cbStreamBuf;
			this._decoder.Convert(
				this._streamBuffer,
				0,
				cb,
				this._textBuffer,
				this._cchValid,
				cchToFill,
				false,
				out int cbDecoded,
				out int cchDecoded,
				out _);
			this._cchValid += cchDecoded;
			int cbStreamBuf = this._cbStreamBuf - cbDecoded;
			if (cbStreamBuf > 0)
			{
				Buffer.BlockCopy(this._streamBuffer, this._cbStreamBuf - cbStreamBuf, this._streamBuffer, 0, cbStreamBuf);
			}
			this._cbStreamBuf = cbStreamBuf;
		}

		public bool LazyFill { get; set; }

		/// <summary>
		/// Fills the text buffer.
		/// </summary>
		/// <returns><c>true</c> if the buffer contains valid data; otherwise, <c>false</c>.</returns>
		public override async ValueTask<bool> FillBufferAsync(CancellationToken cancellationToken)
		{
			MoveTextBuffer();

			int cchToFill;
			while (!this._eof
				&& (cchToFill = this._textBuffer.Length - this._cchValid) > 0
				)
			{
				int cbToRead = this._streamBuffer.Length - this._cbStreamBuf;
				int cbRead = await this.UnderlyingStream.ReadAsync(
					this._streamBuffer,
					this._cbStreamBuf,
					cbToRead,
					cancellationToken);
				if (cbRead == 0)
					this._eof = true;
				else
				{
					this._cbStreamBuf += cbRead;
					ConvertStreamChunk(cchToFill);

					if (cbRead < cbToRead && this.LazyFill)
						break;
				}
			}

			return (this._cchValid > 0);
		}

		private void MoveTextBuffer()
		{
			if (!this._eof && this._readIndex != 0)
			{
				Buffer.BlockCopy(this._textBuffer, this._readIndex * 2, this._textBuffer, 0, this._cchValid * 2);
				this._readIndex = 0;
			}
		}

		/// <inheritdoc/>
		public override bool FillBuffer()
		{
			MoveTextBuffer();

			int cchToFill;
			while (!this._eof
				&& (cchToFill = this._textBuffer.Length - this._cchValid) > 0
				)
			{
				int cbRead = this.UnderlyingStream.Read(
					this._streamBuffer,
					this._cbStreamBuf,
					this._streamBuffer.Length - this._cbStreamBuf);
				if (cbRead == 0)
					this._eof = true;
				else
				{
					this._cbStreamBuf += cbRead;
					ConvertStreamChunk(cchToFill);
				}
			}

			return (this._cchValid > 0);
		}

		/// <inheritdoc/>
		public override int Peek()
		{
			if (
				(this._cchValid > 0)
				|| this.FillBuffer()
				)
			{
				return this._textBuffer[this._readIndex];
			}

			return NoPeekValue;
		}

		/// <inheritdoc/>
		public override async ValueTask<int> PeekAsync(CancellationToken cancellationToken)
		{
			if (
				(this._cchValid > 0)
				|| (await this.FillBufferAsync(cancellationToken))
				)
			{
				return this._textBuffer[this._readIndex];
			}

			return NoPeekValue;
		}

		/// <inheritdoc/>
		protected override void AdvanceCore(int cch)
		{
			ArgValidation.IsNonnegative(cch, nameof(cch));

			// TODO: Validate cch doesn't overrun EOF

			this.AdvanceBuffer(cch);
		}

		private void AdvanceBuffer(int cch)
		{
			Debug.Assert(cch > 0);
			Debug.Assert(cch <= this._cchValid);

			this._readIndex += cch;
			this._cchValid -= cch;
		}

		private char ReadChar()
		{
			Debug.Assert(this._cchValid > 0);

			var c = this._textBuffer[this._readIndex];
			this.AdvanceBuffer(1);
			return c;
		}

		/// <inheritdoc/>
		public override int Read()
		{
			if (
				(this._cchValid > 0)
				|| this.FillBuffer()
				)
			{
				return this.ReadChar();
			}

			return NoPeekValue;
		}

		/// <inheritdoc/>
		public override async ValueTask<int> ReadAsync(CancellationToken cancellationToken)
		{
			if (this._cchValid > 0)
				return this.ReadChar();
			else
			{
				if (await this.FillBufferAsync(cancellationToken))
					return this.ReadChar();
			}

			return NoPeekValue;
		}

		private int ReadChars(char[] buffer, int index, int count)
		{
			Buffer.BlockCopy(this._textBuffer, this._readIndex * 2, buffer, index * 2, count * 2);
			this.AdvanceBuffer(count);
			return count;
		}

		/// <inheritdoc/>
		public override int Read(char[] buffer, int index, int count)
		{
			ArgValidation.IsValidBufferParams(buffer, index, count, nameof(buffer), nameof(index), nameof(count));
			if (count == 0)
				return 0;

			if (count < this._cchValid)
			{
				return this.ReadChars(buffer, index, count);
			}
			else
			{
				this.FillBuffer();
				count = Math.Min(count, this._cchValid);

				return this.ReadChars(buffer, index, count);
			}
		}

		/// <inheritdoc/>
		public override async ValueTask<int> ReadAsync(char[] buffer, int index, int count, CancellationToken cancellationToken)
		{
			ArgValidation.IsValidBufferParams(buffer, index, count, nameof(buffer), nameof(index), nameof(count));
			if (count == 0)
				return 0;

			if (count < this._cchValid)
			{
				return this.ReadChars(buffer, index, count);
			}
			else
			{
				await this.FillBufferAsync(cancellationToken);
				count = Math.Min(count, this._cchValid);

				return this.ReadChars(buffer, index, count);
			}
		}

		/// <inheritdoc/>
		public sealed override async Task<string?> ReadLineAsync()
		{
			return await this.ReadLineAsync(CancellationToken.None);
		}
		/// <inheritdoc/>
		public override async ValueTask<string?> ReadLineAsync(CancellationToken cancellationToken)
		{
			StringBuilder sb = new StringBuilder();
			do
			{
				int c = await ReadAsync(cancellationToken);
				if (c == -1) break;
				if (c == '\r' || c == '\n')
				{
					if (c == '\r' && Peek() == '\n') Read();
					return sb.ToString();
				}
				sb.Append((char)c);
			} while (true);
			if (sb.Length > 0) return sb.ToString();
			return null;
		}
	}
}
