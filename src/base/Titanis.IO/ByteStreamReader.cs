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
	/// Implements a <see cref="IByteSource"/> that is backed by a <see cref="Stream"/>.
	/// </summary>
	public partial class ByteStreamReader : IByteSource
	{
		public ByteStreamReader(Stream stream, bool ownsStream)
		{
			if (stream is null) throw new ArgumentNullException(nameof(stream));
			this._stream = stream;
			this._ownsStream = ownsStream;

			this._startPosition = -1;
			this._length = -1;
		}
		public ByteStreamReader(Stream stream, bool ownsStream, long startPosition, long length)
		{
			if (stream is null) throw new ArgumentNullException(nameof(stream));
			this._stream = stream;
			this._ownsStream = ownsStream;

			this._startPosition = startPosition;
			this._length = length;
		}

		private Stream _stream;
		private bool _ownsStream;
		private readonly long _startPosition;
		private readonly long _length;

		/// <inheritdoc/>
		public bool CanSeek => this._stream.CanSeek;

		private const int NoPeek = -1;
		private int _peekByte = NoPeek;
		private bool HasPeekByte => (this._peekByte >= 0);
		private int PeekAdj => this.HasPeekByte ? -1 : 0;
		private void ClearPeek()
		{
			this._peekByte = NoPeek;
		}

		/// <inheritdoc cref="IByteSource.Position"/>
		public long Position
		{
			get => this._stream.Position + this.PeekAdj;
			set
			{
				this.ClearPeek();
				this._stream.Position = value;
			}
		}

		/// <inheritdoc cref="IByteSource.Length"/>
		public long Length => this._stream.Length;


		/// <inheritdoc cref="IByteSource.Advance(long)"/>
		public void Advance(long count)
		{
			if (count < 0)
				throw new ArgumentOutOfRangeException(nameof(count));
			if (count == 0)
				return;

			count += this.PeekAdj;
			this.ClearPeek();
			if (count == 0)
				return;

			if (this._stream.CanSeek)
				this._stream.Seek(count, SeekOrigin.Current);
			else
			{
				// Drain the stream
				const int MaxStackAlloc = 256;
				Span<byte> buf = stackalloc byte[(int)Math.Min(count, MaxStackAlloc)];
				do
				{
					int cbRead = this._stream.Read(buf);
					count -= cbRead;
				}
				while (count >= 0);
			}
		}

		/// <inheritdoc cref="IByteSource.Consume(int)"/>
		public ReadOnlySpan<byte> Consume(int size)
		{
			byte[] buf = new byte[size];
			if (this.HasPeekByte)
			{
				buf[0] = (byte)this._peekByte;
				this.ClearPeek();
				this._stream.ReadAll(buf, 1, size - 1);
			}
			else
				this._stream.ReadAll(buf, 0, size);

			return buf;
		}
		public int PeekByte()
		{
			if (!this.HasPeekByte)
				this._peekByte = this._stream.ReadByte();

			return this._peekByte;
		}

		public byte ReadByte()
		{
			if (this.HasPeekByte)
			{
				var b = this._peekByte;
				this.ClearPeek();
				return (byte)b;
			}
			else
			{
				var b = this._stream.ReadByte();
				if (b == -1)
					throw new EndOfStreamException();
				return (byte)b;
			}
		}


		#region Nesting
		public bool SupportsNested => this.CanSeek;

		public IByteSource CreateNested(long startPosition, long length)
		{
			throw new NotImplementedException();
		}

		public IByteSource CreateNested(long length)
		{
			throw new NotImplementedException();
		}
		#endregion
	}

	partial class ByteStreamReader : IDisposable
	{
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					if (this._ownsStream)
						this._stream.Dispose();
				}

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
