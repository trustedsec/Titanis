using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.IO
{
	public class BufferedStringReader : BufferedTextReader
	{
		public BufferedStringReader(string str)
		{
			if (str == null)
				throw new ArgumentNullException(nameof(str));

			this._textBuffer = str.ToCharArray();
		}
		public BufferedStringReader(char[] chars)
		{
			if (chars == null)
				throw new ArgumentNullException(nameof(chars));

			this._textBuffer = chars;
		}

		public override ReadOnlySpan<char> GetBufferedText()
		{
			return this._textBuffer.AsSpan(this._readIndex, this._textBuffer.Length - this._readIndex);
		}

		private char[] _textBuffer;

		private int _readIndex;

		private bool Eof => this.CharsLeft <= 0;
		private int CharsLeft => this._textBuffer.Length - this._readIndex;

		/// <summary>
		/// Fills the text buffer.
		/// </summary>
		/// <returns><c>true</c> if the buffer contains valid data; otherwise, <c>false</c>.</returns>
		public override ValueTask<bool> FillBufferAsync(CancellationToken cancellationToken)
		{
			return new ValueTask<bool>(this.FillBuffer());
		}

		public override bool FillBuffer()
		{
			return !this.Eof;
		}

		public override int Peek()
		{
			if (!this.Eof)
			{
				return this._textBuffer[this._readIndex];
			}

			return NoPeekValue;
		}

		public override ValueTask<int> PeekAsync(CancellationToken cancellationToken)
		{
			return new ValueTask<int>(this.Peek());
		}

		protected override void AdvanceCore(int cch)
		{
			this.AdvanceBuffer(cch);
		}

		private void AdvanceBuffer(int cch)
		{
			Debug.Assert(cch > 0);
			Debug.Assert(cch <= this.CharsLeft);

			this._readIndex += cch;
		}

		private char ReadChar()
		{
			Debug.Assert(!this.Eof);

			var c = this._textBuffer[this._readIndex];
			this.AdvanceBuffer(1);
			return c;
		}

		public override int Read()
		{
			if (!this.Eof)
			{
				return this.ReadChar();
			}

			return NoPeekValue;
		}

		public override ValueTask<int> ReadAsync(CancellationToken cancellationToken)
		{
			return new ValueTask<int>(this.Read());
		}

		private int ReadChars(char[] buffer, int index, int count)
		{
			Buffer.BlockCopy(this._textBuffer, this._readIndex, buffer, index, count);
			this.AdvanceBuffer(count);
			return count;
		}

		public override int Read(char[] buffer, int index, int count)
		{
			ArgValidation.IsValidBufferParams(buffer, index, count, nameof(buffer), nameof(index), nameof(count));
			if (count == 0)
				return 0;

			count = Math.Min(count, this.CharsLeft);
			return this.ReadChars(buffer, index, count);
		}

		public override ValueTask<int> ReadAsync(char[] buffer, int index, int count, CancellationToken cancellationToken)
		{
			return new ValueTask<int>(this.Read(buffer, index, count));
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
