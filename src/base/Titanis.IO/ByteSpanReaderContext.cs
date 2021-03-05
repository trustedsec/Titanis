using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Titanis.IO
{
	/// <summary>
	/// Implements <see cref="IByteBufferReadOnly"/> as a ref struct wrapping a <see cref="ReadOnlySpan{T}"/> of <see cref="byte"/>.
	/// </summary>
	public ref struct ByteSpanReaderContext : IByteBufferReadOnly
	{
		private readonly ReadOnlySpan<byte> _bytes;
		private int _position;

		public ByteSpanReaderContext(ReadOnlySpan<byte> bytes)
		{
			this._bytes = bytes;
		}

		/// <inheritdoc/>
		public readonly bool CanSeek => true;

		/// <inheritdoc/>
		readonly long IByteSource.Length => this._bytes.Length;
		/// <summary>
		/// Gets the length of the byte source.
		/// </summary>
		public readonly int Length => this._bytes.Length;

		/// <inheritdoc/>
		long IByteSource.Position
		{
			readonly get => _position;
			set => _position = CheckPosition(value);
		}
		/// <summary>
		/// Gets or sets the position of the next operation.
		/// </summary>
		public int Position
		{
			readonly get => _position;
			set => _position = CheckPosition(value);
		}

		private static int CheckPosition(long value)
		{
			if ((uint)value > (uint)int.MaxValue)
				throw new NotSupportedException("The implementation only supports a range that can be represented as a positive 32-bit signed integer.");

			return unchecked((int)value);
		}

		/// <inheritdoc/>
		public void Advance(long count)
		{
			// The following check is whether `count` itself is out of range,
			// not the resulting position.
			// Checking the position itself would cause an error when reading
			// the last byte of a span int.MaxValue in length.

			this._position += CheckPosition(count);
		}

		/// <inheritdoc/>
		public ReadOnlySpan<byte> Consume(int count)
		{
			var span = this._bytes.Slice(this._position, count);
			this.Position += count;
			return span;
		}

		/// <inheritdoc/>
		public readonly ReadOnlySpan<byte> GetByteSpanReadOnly()
		{
			return this._bytes;
		}

		/// <inheritdoc/>
		public readonly ReadOnlySpan<byte> GetByteSpanReadOnly(long startIndex)
		{
			return this._bytes.Slice(CheckPosition(startIndex));
		}

		/// <inheritdoc/>
		public readonly ReadOnlySpan<byte> GetBytesReadOnly(long startIndex, int count)
		{
			return this._bytes.Slice(CheckPosition(startIndex), count);
		}

		/// <inheritdoc/>
		public readonly int PeekByte()
		{
			return ((uint)this._position >= (uint)this._bytes.Length)
				? -1
				: this._bytes[this._position];
		}

		/// <inheritdoc/>
		public byte ReadByte()
		{
			if ((uint)this._position >= (uint)this._bytes.Length)
				throw new EndOfStreamException();

			var b = this._bytes[this._position];
			this.Advance(1);
			return b;
		}

		public bool SupportsNested => true;

		public IByteSource CreateNested(long startPosition, long length)
		{
			throw new NotImplementedException();
		}

		public IByteSource CreateNested(long length)
		{
			throw new NotImplementedException();
		}
	}
}
