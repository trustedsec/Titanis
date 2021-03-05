using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.IO
{
	/// <summary>
	/// Implements a <see cref="TextReader"/> that does not provide buffering.
	/// </summary>
	/// <remarks>
	/// Since this implementation doesn't buffer data (other than a peeked byte),
	/// the caller may continue to read data directly from  the underlying
	/// <see cref="Stream"/>.  Use <see cref="HasPeekByte"/> to determine whether
	/// a peeked byte is buffered.
	/// </remarks>
	public class UnbufferedStreamReader : AsyncTextReader, IHaveStreamPosition
	{
		public UnbufferedStreamReader(Stream stream)
		{
			if (stream is null)
				throw new ArgumentNullException(nameof(stream));

			this._stream = stream;
		}

		private Stream _stream;

		private bool _eof;
		private byte[]? _peekBuf;
		private int _peekByte = NoPeekValue;

		/// <summary>
		/// Gets a value indicating whether this object holds a peeked byte.
		/// </summary>
		public bool HasPeekByte => this._peekByte >= 0;

		public long StreamPosition
		{
			get
			{
				long pos = this._stream.Position;
				if (this.HasPeekByte)
					pos--;
				return pos;
			}
		}

		private void ResetPeek() => this._peekByte = NoPeekValue;

		public override int Peek()
		{
			if (this.HasPeekByte)
				return this._peekByte;
			else if (this._eof)
				return NoPeekValue;
			else
			{
				if (!this.HasPeekByte)
				{
					this._peekByte = this._stream.ReadByte();
					if (this._peekByte < 0)
						this._eof = true;
				}

				return this._peekByte;
			}
		}

		public override async ValueTask<int> PeekAsync(CancellationToken cancellationToken)
		{
			if (this.HasPeekByte)
				return this._peekByte;
			else if (this._eof)
				return NoPeekValue;
			else
			{
				if (!this.HasPeekByte)
				{
					int byteRead = await ReadByteFromStreamAsync(cancellationToken);
					this._peekByte = byteRead;
				}

				return this._peekByte;
			}
		}

		private async Task<int> ReadByteFromStreamAsync(CancellationToken cancellationToken)
		{
			byte[] peekBuf = (this._peekBuf ??= new byte[1]);
			int cb = await this._stream.ReadAsync(peekBuf, 0, 1, cancellationToken);
			int byteRead;
			if (cb < 1)
			{
				this._eof = true;
				byteRead = -1;
			}
			else
			{
				byteRead = peekBuf[0];
			}

			return byteRead;
		}

		public override int Read()
		{
			if (this.HasPeekByte)
			{
				var peekByte = this._peekByte;
				this.ResetPeek();
				return peekByte;
			}
			else if (this._eof)
				return NoPeekValue;
			else
			{
				int b = this._stream.ReadByte();
				if (b < 0)
					this._eof = true;

				return b;
			}
		}

		public override async ValueTask<int> ReadAsync(CancellationToken cancellationToken)
		{
			if (this.HasPeekByte)
			{
				var peekByte = this._peekByte;
				this.ResetPeek();
				return peekByte;
			}
			else if (this._eof)
				return NoPeekValue;
			else
			{
				int b = await this.ReadByteFromStreamAsync(cancellationToken);
				if (b < 0)
					this._eof = true;

				return b;
			}
		}

		public override int Read(char[] buffer, int index, int count)
		{
			ArgValidation.IsValidBufferParams(buffer, index, count, nameof(buffer), nameof(index), nameof(count));

			bool peeked = this.HasPeekByte;
			if (peeked)
			{
				buffer[index] = (char)this._peekByte;
				this.ResetPeek();

				index++;
				count--;
			}

			byte[] bytes = new byte[count];
			int cbRead = this._stream.Read(bytes, 0, count);
			cbRead = Encoding.UTF8.GetChars(bytes, 0, cbRead, buffer, index);
			if (peeked)
				cbRead++;

			return cbRead;
		}

		public override async ValueTask<int> ReadAsync(char[] buffer, int index, int count, CancellationToken cancellationToken)
		{
			ArgValidation.IsValidBufferParams(buffer, index, count, nameof(buffer), nameof(index), nameof(count));

			bool peeked = this.HasPeekByte;
			if (peeked)
			{
				buffer[index] = (char)this._peekByte;
				this.ResetPeek();

				index++;
				count--;
			}

			byte[] bytes = new byte[count];
			int cbRead = await this._stream.ReadAsync(bytes, 0, count, cancellationToken);
			cbRead = Encoding.UTF8.GetChars(bytes, 0, cbRead, buffer, index);
			if (peeked)
				cbRead++;

			return cbRead;
		}

		public sealed override async Task<string?> ReadLineAsync()
		{
			return await this.ReadLineAsync(CancellationToken.None);
		}
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
