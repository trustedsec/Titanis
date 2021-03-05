using System;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
//using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Titanis.IO
{
	/// <summary>
	/// Implements a <see cref="IByteSource"/> that reads a region of memory.
	/// </summary>
	public class ByteMemoryReader : IByteSource,
		IByteBufferReadOnly
	{
		/// <summary>
		/// Initializes a new <see cref="ByteMemoryReader"/>.
		/// </summary>
		/// <param name="memory">Backing region of memory</param>
		public ByteMemoryReader(ReadOnlyMemory<byte> memory)
		{
			this._ctx = new ByteMemoryReaderContext(memory);
		}

		private ByteMemoryReaderContext _ctx;

		/// <inheritdoc/>
		public bool CanSeek => true;

		public bool IsEof => (this.Position >= this.Length);
		public ReadOnlyMemory<byte> Remaining => this._ctx.GetMemory(this.Position);

		/// <inheritdoc/>
		long IByteSource.Length => this._ctx.Length;
		public int Length => this._ctx.Length;

		/// <inheritdoc/>
		long IByteSource.Position { get => this._ctx.Position; set => this._ctx.Position64 = value; }
		/// <summary>
		/// Gets or sets the position of the next operation.
		/// </summary>
		public int Position { get => this._ctx.Position; set => this._ctx.Position = value; }

		public ReadOnlySpan<byte> PeekBytes(int size)
			=> this._ctx.GetMemory(this.Position, size).Span;

		/// <inheritdoc/>
		public ReadOnlySpan<byte> GetByteSpanReadOnly()
		{
			return this._ctx.GetByteSpanReadOnly();
		}

		/// <inheritdoc/>
		public ReadOnlySpan<byte> GetByteSpanReadOnly(long startIndex)
		{
			return this._ctx.GetByteSpanReadOnly(startIndex);
		}

		/// <inheritdoc/>
		public ReadOnlySpan<byte> GetBytesReadOnly(long startIndex, int count)
		{
			return this._ctx.GetBytesReadOnly(startIndex, count);
		}

		/// <inheritdoc/>
		public int PeekByte()
		{
			return this._ctx.PeekByte();
		}

		/// <inheritdoc/>
		public byte ReadByte()
		{
			return this._ctx.ReadByte();
		}

		/// <inheritdoc/>
		public void Advance(long count)
		{
			this._ctx.Advance(count);
		}

		/// <inheritdoc/>
		public ReadOnlySpan<byte> Consume(int count)
		{
			return this._ctx.Consume(count);
		}


		#region Nested
		public bool SupportsNested => true;

		public ByteMemoryReader CreateNestedReader(int size)
		{
			if (this.RemainingLength() < size)
				throw new EndOfStreamException();

			var pos = this.Position;
			this.Position += size;
			return new ByteMemoryReader(this._ctx.GetMemory(pos, size));
		}

		public IByteSource CreateNested(long startPosition, long length)
			=> this._ctx.CreateNested(startPosition, length);

		public IByteSource CreateNested(long length)
			=> this._ctx.CreateNested(length);
		#endregion
	}
	/// <summary>
	/// Implements <see cref="IByteBufferReadOnly"/> as a struct wrapping a <see cref="ReadOnlyMemory{T}"/> of <see cref="byte"/>.
	/// </summary>
	public struct ByteMemoryReaderContext : IByteBufferReadOnly
	{
		private readonly ReadOnlyMemory<byte> _bytes;
		private int _position;

		public ByteMemoryReaderContext(ReadOnlyMemory<byte> bytes)
		{
			this._bytes = bytes;
		}

		#region Nested
		public bool SupportsNested => true;

		public IByteSource CreateNested(long startPosition, long length)
			// TODO: CheckPosition(length)
			=> new ByteMemoryReader(this._bytes.Slice(CheckPosition(startPosition), CheckPosition(length)));

		public IByteSource CreateNested(long length)
			=> this.CreateNested(this.Position, length);
		#endregion

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
		public long Position64
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
			var span = this._bytes.Span.Slice(this._position, count);
			this.Position += count;
			return span;
		}

		/// <inheritdoc/>
		public readonly ReadOnlySpan<byte> GetByteSpanReadOnly()
		{
			return this._bytes.Span;
		}
		public readonly ReadOnlyMemory<byte> GetMemory()
		{
			return this._bytes;
		}

		/// <inheritdoc/>
		public readonly ReadOnlySpan<byte> GetByteSpanReadOnly(long startIndex)
		{
			return this._bytes.Span.Slice(CheckPosition(startIndex));
		}
		public readonly ReadOnlyMemory<byte> GetMemory(long startIndex)
		{
			return this._bytes.Slice(CheckPosition(startIndex));
		}

		/// <inheritdoc/>
		public readonly ReadOnlySpan<byte> GetBytesReadOnly(long startIndex, int count)
		{
			return this._bytes.Span.Slice(CheckPosition(startIndex), count);
		}
		public readonly ReadOnlyMemory<byte> GetMemory(long startIndex, int count)
		{
			return this._bytes.Slice(CheckPosition(startIndex), count);
		}

		/// <inheritdoc/>
		public readonly int PeekByte()
		{
			return this._bytes.Span[this._position];
		}

		/// <inheritdoc/>
		public byte ReadByte()
		{
			if ((uint)this._position >= (uint)this._bytes.Length)
				throw new EndOfStreamException();

			var b = this._bytes.Span[this._position];
			this.Advance(1);
			return b;
		}
	}
}
