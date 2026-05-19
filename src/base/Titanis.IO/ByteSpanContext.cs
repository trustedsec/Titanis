using System;

namespace Titanis.IO
{
	public ref struct ByteSpanContext : IByteSource
	{
		public ByteSpanContext(ReadOnlySpan<byte> bytes)
		{
			this.Bytes = bytes;
		}

		public ReadOnlySpan<byte> Bytes { get; }

		public bool CanSeek => true;

		public long Length => this.Bytes.Length;

		public int Position { get; set; }
		long IByteSource.Position { get => this.Position; set => this.Position = checked((int)value); }

		public void Advance(long count)
		{
			this.Position = checked((int)(this.Position + count));
		}

		public ReadOnlySpan<byte> Consume(int count)
		{
			var consumed = this.Bytes.Slice(this.Position, count);
			this.Advance(count);
			return consumed;
		}

		public int PeekByte()
		{
			return (this.Position <= this.Length) ? this.Bytes[this.Position] : -1;
		}

		public byte ReadByte()
		{
			var b = this.Bytes[this.Position];
			this.Position++;
			return b;
		}
	}
}
