using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.IO
{
	[Flags]
	public enum ByteWriterOptions
	{
		None = 0,

		Reverse = 1
	}

	public class ByteWriter
	{
		private byte[] _buffer;
		private int _pos;
		private ByteWriterOptions _options;

		public ByteWriter()
			: this(0x20)
		{ }
		public ByteWriter(int initialSize)
		{
			this._buffer = new byte[initialSize];
		}

		public ByteWriter(int initialSize, ByteWriterOptions options)
			: this(initialSize)
		{
			this._options = options;
		}

		public void Reset()
		{
			this._pos = 0;
			this.Length = 0;
		}

		public byte[] GetBuffer() => this._buffer;
		public int Position => this._pos;
		public int WriteIndex => this.IsReverse ? (this._buffer.Length - this.Position) : this.Position;
		public int Length { get; private set; }
		public void SetPosition(int newPosition)
		{
			if (newPosition >= this._buffer.Length)
			{
				int newSize = Math.Max(this._buffer.Length * 2, newPosition + 0x10);
				byte[] newbuf = new byte[newSize];
				if (this.IsReverse)
				{
					int cbSlack = this._buffer.Length - this.Length;
					int cbNewSlack = newSize - this.Length;
					Buffer.BlockCopy(this._buffer, cbSlack, newbuf, cbNewSlack, this.Length);
				}
				else
				{
					Buffer.BlockCopy(this._buffer, 0, newbuf, 0, this.Length);
				}
				this._buffer = newbuf;
			}
			this._pos = newPosition;
			this.Length = Math.Max(this.Length, newPosition);
		}

		public void MarkEndOfStream()
		{
			this.Length = this.Position;
		}

		public Memory<byte> GetData()
		{
			if (this.IsReverse)
			{
				return new Memory<byte>(this.GetBuffer(), this.WriteIndex, this.Length);
			}
			else
			{
				return new Memory<byte>(this.GetBuffer(), 0, this.Length);
			}
		}

		public int Align(int align)
		{
			Debug.Assert(align > 0);

			int mod = this.Position % align;
			if (mod != 0)
			{
				int n = align - mod;
				this.Advance(n);
				return n;
			}
			else
				return 0;
		}

		public int Align(int align, int bias)
		{
			Debug.Assert(align > 0);

			int mod = (this.Position - bias) % align;
			if (mod != 0)
			{
				int n = align - mod;
				this.Advance(n);
				return n;
			}
			else
				return 0;
		}

		public bool IsReverse => (0 != (this._options & ByteWriterOptions.Reverse));
		public void Advance(int size)
		{
			this.SetPosition(this._pos + size);
		}

		public Span<byte> Consume(int size)
		{
			if (this.IsReverse)
			{
				this.Advance(size);
				return this._buffer.AsSpan(this.WriteIndex, size);
			}
			else
			{
				int pos = this._pos;
				this.Advance(size);
				return this._buffer.AsSpan(pos, size);
			}
		}

		[Obsolete("Use WriteBytes instead", true)]
		public void WriteData(ReadOnlySpan<byte> span)
			=> this.WriteBytes(span);
		public void WriteBytes(ReadOnlySpan<byte> span)
		{
			if (span.Length > 0)
				span.CopyTo(this.Consume(span.Length));
		}

		public bool IsLittleEndian => BitConverter.IsLittleEndian;

		public void WriteByte(byte v) => this.Consume(1)[0] = v;
		public void WriteSByte(sbyte v) => this.WriteByte((byte)v);

		public void WriteInt16BE(short v) => BinaryPrimitives.WriteInt16BigEndian(this.Consume(2), v);
		public void WriteUInt16BE(ushort v) => BinaryPrimitives.WriteUInt16BigEndian(this.Consume(2), v);
		public void WriteInt32BE(int v) => BinaryPrimitives.WriteInt32BigEndian(this.Consume(4), v);
		public void WriteUInt32BE(uint v) => BinaryPrimitives.WriteUInt32BigEndian(this.Consume(4), v);
		public void WriteInt64BE(long v) => BinaryPrimitives.WriteInt64BigEndian(this.Consume(8), v);
		public void WriteUInt64BE(ulong v) => BinaryPrimitives.WriteUInt64BigEndian(this.Consume(8), v);

		public void WriteInt16LE(short v) => BinaryPrimitives.WriteInt16LittleEndian(this.Consume(2), v);
		public void WriteUInt16LE(ushort v) => BinaryPrimitives.WriteUInt16LittleEndian(this.Consume(2), v);
		public void WriteUInt16SpanLE(ReadOnlySpan<ushort> span)
		{
			Span<ushort> buf = MemoryMarshal.Cast<byte, ushort>(this.Consume(2 * span.Length));
			if (BitConverter.IsLittleEndian)
			{
				span.CopyTo(buf);
			}
			else
			{
				for (int i = 0; i < span.Length; i++)
					buf[i] = BinaryPrimitives.ReverseEndianness(span[i]);
			}
		}
		public void WriteInt32LE(int v) => BinaryPrimitives.WriteInt32LittleEndian(this.Consume(4), v);
		public void WriteUInt32LE(uint v) => BinaryPrimitives.WriteUInt32LittleEndian(this.Consume(4), v);
		public void WriteInt64LE(long v) => BinaryPrimitives.WriteInt64LittleEndian(this.Consume(8), v);
		public void WriteUInt64LE(ulong v) => BinaryPrimitives.WriteUInt64LittleEndian(this.Consume(8), v);

		public unsafe void WriteSingle(float v)
		{
			fixed (byte* pBuf = this.Consume(sizeof(float)))
			{
				*(float*)pBuf = v;
			}
		}
		public unsafe void WriteDouble(double v)
		{
			fixed (byte* pBuf = this.Consume(sizeof(double)))
			{
				*(double*)pBuf = v;
			}
		}
		public unsafe void WriteDecimal(decimal v)
		{
			fixed (byte* pBuf = this.Consume(sizeof(decimal)))
			{
				*(decimal*)pBuf = v;
			}
		}
		public unsafe void WriteGuid(Guid g)
		{
			fixed (byte* pBuf = this.Consume(sizeof(Guid)))
			{
				*(Guid*)pBuf = g;
			}
		}

		public unsafe void WriteStringAnsi(string str) => this.WriteString(str, ByteCharEncoding.Instance);
		public unsafe void WriteStringUtf8(string str) => this.WriteString(str, Encoding.UTF8);
		public unsafe void WriteStringUni(string str) => this.WriteString(str, Encoding.Unicode);
		public void WriteString(string str, Encoding encoding)
		{
			if (!string.IsNullOrEmpty(str))
			{
				int cb = encoding.GetByteCount(str);
				var span = this.Consume(cb);
				encoding.GetBytes(str.AsSpan(), span);
			}
		}

		public void WritePduStruct<TStruct>(TStruct struc)
			where TStruct : IPduStruct
			=> struc.WriteTo(this);

		public void WritePduStruct<TStruct>(TStruct struc, PduByteOrder byteOrder)
			where TStruct : IPduStruct
			=> struc.WriteTo(this, byteOrder);

		public void WritePduStruct<TStruct, T>(TStruct struc, PduByteOrder byteOrder, T arg)
			where TStruct : IPduStruct<T>
			=> struc.WriteTo(this, byteOrder, arg);

		public void WritePduStruct<TStruct, T1, T2>(TStruct struc, PduByteOrder byteOrder, T1 arg1, T2 arg2)
			where TStruct : IPduStruct<T1, T2>
			=> struc.WriteTo(this, byteOrder, arg1, arg2);
	}
}
