using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;

namespace Titanis.IO
{
	/// <summary>
	/// Exposes functionality for reading bytes.
	/// </summary>
	/// <seealso cref="ByteMemoryReader"/>
	/// <seealso cref="ByteStreamReader"/>
	/// <seealso cref="ByteSource"/>
	/// <remarks>
	/// Ideally, a byte reader would be implemented as a <c>ref struct</c> that implements this interface
	/// that relies on extension methods; but alas, C# doesn't yet allow this.
	/// </remarks>
	public interface IByteSource
	{
		/// <summary>
		/// Retrieves the next byte from the source, leaving it in the buffer.
		/// </summary>
		/// <returns>The value of the next byte, if available; otherwise, <c>-1</c>.</returns>
		int PeekByte();
		/// <summary>
		/// Retrieves the next byte from the source.
		/// </summary>
		/// <returns>The value of the next byte</returns>
		/// <exception cref="EndOfStreamException">End-of-stream was encountered.</exception>
		byte ReadByte();
		/// <summary>
		/// Advances the position by a specified number of bytes.
		/// </summary>
		/// <param name="count">Number of bytes to skip</param>
		/// <remarks>
		/// This method doesn't necessarily check whether the new position is valid.
		/// An implementation may defer the range check until the next read operation.
		/// Some implementations may allow a negative offset but are not required to.
		/// </remarks>
		void Advance(long count);
		/// <summary>
		/// Consumes a number of bytes from the source.
		/// </summary>
		/// <param name="count">Number of bytes to consume</param>
		/// <returns>A <see cref="ReadOnlySpan{T}"/> of the bytes read from the source.</returns>
		/// <exception cref="EndOfStreamException">End-of-stream was encountered.</exception>
		/// <remarks>
		/// This method shall never return a partial buffer.
		/// Either the operation completes in its entirety, or an exception is thrown.
		/// If the operation fails, the state of the byte source is undefined.
		/// </remarks>
		ReadOnlySpan<byte> Consume(int count);

		/// <summary>
		/// Gets a value indicating whether the object supports seeking.
		/// </summary>
		bool CanSeek { get; }
		/// <summary>
		/// Gets the length of the byte source.
		/// </summary>
		long Length { get; }
		/// <summary>
		/// Gets or sets the position of the next operation.
		/// </summary>
		long Position { get; set; }

		bool SupportsNested { get; }
		public IByteSource CreateNested(long startPosition, long length);
		public IByteSource CreateNested(long length);
	}
	/// <summary>
	/// Implements extension methods for <see cref="IByteSource"/>.
	/// </summary>
	public static class ByteSource
	{
		/// <summary>
		/// Calculates the padding required to achieve the desired alignment.
		/// </summary>
		/// <param name="position">Start position</param>
		/// <param name="align">Desired alignment, in bytes</param>
		/// <param name="bias">Value added to <paramref name="position"/> for calculating the modulus</param>
		/// <returns>The padding to add to <paramref name="position"/> to achieve alignment of <paramref name="align"/></returns>
		public static int CalculateAlignPadding(long position, int align, int bias = 0)
		{
			Debug.Assert(align > 0);
			int mod = unchecked((int)((position - bias) % align));
			return (mod == 0)
				? 0
				: (align - mod);
		}

		public static int Align<TStruc>(this ref TStruc ctx, int align, int bias = 0)
			where TStruc : struct, IByteSource
		{
			var pad = CalculateAlignPadding(ctx.Position, align, bias);
			if (pad != 0)
				ctx.Advance(pad);
			return pad;
		}
		public static int Align<TStruc>(this TStruc ctx, int align, int bias = 0)
			where TStruc : IByteSource
		{
			var pad = CalculateAlignPadding(ctx.Position, align, bias);
			if (pad != 0)
				ctx.Advance(pad);
			return pad;
		}

		[Obsolete("Use ReadBytes instead", true)]
		public static byte[] ReadData(this IByteSource ctx, int size)
			=> ReadBytes(ctx, size);
		public static byte[] ReadBytes(this IByteSource ctx, int size)
			=> ctx.Consume(size).ToArray();
		public static long RemainingLength(this IByteSource ctx)
			=> ctx.Length - ctx.Position;
		public static ReadOnlySpan<byte> Remaining(this IByteBufferReadOnly ctx)
			=> ctx.GetByteSpanReadOnly(ctx.Position);

		public static TStruct ReadPduStruct<TStruct>(this IByteSource source)
			where TStruct : IPduStruct, new()
		{
			TStruct struc = new TStruct();
			struc.ReadFrom(source);
			return struc;
		}
		public static TStruct ReadPduStruct<TStruct>(this IByteSource source, PduByteOrder byteOrder)
			where TStruct : IPduStruct, new()
		{
			TStruct struc = new TStruct();
			struc.ReadFrom(source, byteOrder);
			return struc;
		}
		public static TStruct ReadPduStruct<TStruct, T>(this IByteSource source, PduByteOrder byteOrder, T arg)
			where TStruct : IPduStruct<T>, new()
		{
			TStruct struc = new TStruct();
			struc.ReadFrom(source, byteOrder, arg);
			return struc;
		}
		public static TStruct ReadPduStruct<TStruct, T1, T2>(this IByteSource source, PduByteOrder byteOrder, T1 arg1, T2 arg2)
			where TStruct : IPduStruct<T1, T2>, new()
		{
			TStruct struc = new TStruct();
			struc.ReadFrom(source, byteOrder, arg1, arg2);
			return struc;
		}

		#region Numerics
		public static sbyte ReadSByte(this IByteSource ctx)
			=> (sbyte)ctx.ReadByte();

		public static short ReadInt16BE(this IByteSource ctx)
			=> BinaryPrimitives.ReadInt16BigEndian(ctx.Consume(2));
		public static ushort ReadUInt16BE(this IByteSource ctx)
			=> BinaryPrimitives.ReadUInt16BigEndian(ctx.Consume(2));
		public static int ReadInt32BE(this IByteSource ctx)
			=> BinaryPrimitives.ReadInt32BigEndian(ctx.Consume(4));
		public static uint ReadUInt32BE(this IByteSource ctx)
			=> BinaryPrimitives.ReadUInt32BigEndian(ctx.Consume(4));
		public static long ReadInt64BE(this IByteSource ctx)
			=> BinaryPrimitives.ReadInt64BigEndian(ctx.Consume(8));
		public static ulong ReadUInt64BE(this IByteSource ctx)
			=> BinaryPrimitives.ReadUInt64BigEndian(ctx.Consume(8));

		public static short ReadInt16LE(this IByteSource ctx)
			=> BinaryPrimitives.ReadInt16LittleEndian(ctx.Consume(2));
		public static ushort ReadUInt16LE(this IByteSource ctx)
			=> BinaryPrimitives.ReadUInt16LittleEndian(ctx.Consume(2));
		public static int ReadInt32LE(this IByteSource ctx)
			=> BinaryPrimitives.ReadInt32LittleEndian(ctx.Consume(4));
		public static uint ReadUInt32LE(this IByteSource ctx)
			=> BinaryPrimitives.ReadUInt32LittleEndian(ctx.Consume(4));
		public static long ReadInt64LE(this IByteSource ctx)
			=> BinaryPrimitives.ReadInt64LittleEndian(ctx.Consume(8));
		public static ulong ReadUInt64LE(this IByteSource ctx)
			=> BinaryPrimitives.ReadUInt64LittleEndian(ctx.Consume(8));

		public static short ReadInt16(this IByteSource ctx)
			=> BitConverter.IsLittleEndian ? ctx.ReadInt16LE() : ctx.ReadInt16BE();
		public static ushort ReadUInt16(this IByteSource ctx)
			=> BitConverter.IsLittleEndian ? ctx.ReadUInt16LE() : ctx.ReadUInt16BE();
		public static int ReadInt32(this IByteSource ctx)
			=> BitConverter.IsLittleEndian ? ctx.ReadInt32LE() : ctx.ReadInt32BE();
		public static uint ReadUInt32(this IByteSource ctx)
			=> BitConverter.IsLittleEndian ? ctx.ReadUInt32LE() : ctx.ReadUInt32BE();
		public static long ReadInt64(this IByteSource ctx)
			=> BitConverter.IsLittleEndian ? ctx.ReadInt64LE() : ctx.ReadInt64BE();
		public static ulong ReadUInt64(this IByteSource ctx)
			=> BitConverter.IsLittleEndian ? ctx.ReadUInt64LE() : ctx.ReadUInt64BE();

		[StructLayout(LayoutKind.Explicit)]
		struct UInt32Single
		{
			[FieldOffset(0)]
			internal uint u32;
			[FieldOffset(0)]
			internal float f32;
		}

		public static float ReadSingleBE(this IByteSource ctx)
			//=> BinaryPrimitives.ReadSingleBigEndian(ctx.Consume(4));
			=> new UInt32Single { u32 = BinaryPrimitives.ReadUInt32BigEndian(ctx.Consume(4)) }.f32;
		public static float ReadSingleLE(this IByteSource ctx)
			//=> BinaryPrimitives.ReadSingleLittleEndian(ctx.Consume(4));
			=> new UInt32Single { u32 = BinaryPrimitives.ReadUInt32LittleEndian(ctx.Consume(4)) }.f32;
		public static float ReadSingle(this IByteSource ctx)
			=> BitConverter.IsLittleEndian ? ctx.ReadSingleLE() : ctx.ReadSingleBE();


		[StructLayout(LayoutKind.Explicit)]
		struct UInt64Double
		{
			[FieldOffset(0)]
			internal ulong u64;
			[FieldOffset(0)]
			internal double f64;
		}
		public static double ReadDoubleBE(this IByteSource ctx)
			//=> BinaryPrimitives.ReadDoubleBigEndian(ctx.Consume(4));
			=> new UInt64Double { u64 = BinaryPrimitives.ReadUInt64BigEndian(ctx.Consume(4)) }.f64;
		public static double ReadDoubleLE(this IByteSource ctx)
			//=> BinaryPrimitives.ReadDoubleLittleEndian(ctx.Consume(4));
			=> new UInt64Double { u64 = BinaryPrimitives.ReadUInt64LittleEndian(ctx.Consume(4)) }.f64;
		public static double ReadDouble(this IByteSource ctx)
			=> BitConverter.IsLittleEndian ? ctx.ReadDoubleLE() : ctx.ReadDoubleBE();

		public static decimal ReadDecimal(this IByteSource ctx)
			=> MemoryMarshal.Read<decimal>(ctx.Consume(16));
		#endregion

		public static Guid ReadGuid(this IByteSource ctx)
			//=> new Guid(ctx.Consume(16));
			=> MemoryMarshal.Read<Guid>(ctx.Consume(16));

		public static char ReadUniChar(this IByteSource ctx)
			=> (char)ctx.ReadUInt16();

		public static char ReadUniChar(
			this IByteBufferReadOnly ctx,
			long position)
			=> (char)BinaryPrimitives.ReadUInt16LittleEndian(ctx.GetBytesReadOnly(position, 2));

		public static string ReadStringUtf8(this IByteSource ctx, int byteCount)
			=> ctx.ReadString(byteCount, Encoding.UTF8);
		public static string ReadStringUni(this IByteSource ctx, int byteCount)
			=> ctx.ReadString(byteCount, Encoding.Unicode);
		public static string ReadStringAnsi(this IByteSource ctx, int byteCount)
			=> ctx.ReadString(byteCount, ByteCharEncoding.Instance);
		public static string ReadString(this IByteSource ctx, int byteCount, Encoding encoding)
		{
			string str = (byteCount != 0)
				? encoding.GetString(ctx.Consume(byteCount))
				: string.Empty
				;
			return str;
		}

		#region Z string
		private static object? _sbCache = null;
		public static string ReadZStringUni(this IByteSource ctx)
		{
			if (ctx is IByteBufferReadOnly buf)
			{
				return ReadZStringUni(buf);
			}
			else
			{
				StringBuilder sb =
					(StringBuilder?)Interlocked.Exchange(ref _sbCache, null)
					?? new StringBuilder();
				char c;
				while (0 != (c = ctx.ReadUniChar()))
					sb.Append(c);

				string str = sb.ToString();
				sb.Clear();
				Interlocked.Exchange(ref _sbCache, sb);
				return str;
			}
		}

		public static string ReadZStringUni(this IByteBufferReadOnly ctx)
		{
			var cbString = ctx.MeasureZStringUni(ctx.Position);
			string str = ctx.ReadStringUni(cbString);
			ctx.Position += 2;  // Eat terminating null
			return str;
		}

		public static string ReadZStringUtf8(this IByteSource ctx)
		{
			if (ctx is IByteBufferReadOnly buf)
			{
				return ReadZStringUtf8(buf);
			}
			else
			{
				StringBuilder sb =
					(StringBuilder?)Interlocked.Exchange(ref _sbCache, null)
					?? new StringBuilder();
				byte c;
				while (0 != (c = ctx.ReadByte()))
					sb.Append((char)c);

				string str = sb.ToString();
				sb.Clear();
				Interlocked.Exchange(ref _sbCache, sb);
				return str;
			}
		}

		public static string ReadZStringUtf8(this IByteBufferReadOnly ctx)
		{
			var cbString = ctx.MeasureZStringAnsi(ctx.Position);
			string str = ctx.ReadStringUtf8(cbString);
			ctx.Position++; // Eat terminating null
			return str;
		}


		public static int MeasureZStringAnsi(this IByteBufferReadOnly ctx, long startPosition)
		{
			ReadOnlySpan<byte> span = ctx.GetByteSpanReadOnly(startPosition);
			return MeasureZByteString(span);
		}

		public static int MeasureZByteString(ReadOnlySpan<byte> span)
		{
			int i;
			for (i = 0; i < span.Length; i++)
			{
				byte c = span[i];
				if (c == 0)
					break;
			}

			return i;
		}

		public static string ExtractZStringUtf8(this IByteBufferReadOnly ctx, long startPosition)
			=> ctx.ExtractZByteString(startPosition, Encoding.UTF8);
		public static string ExtractZStringAnsi(this IByteBufferReadOnly ctx, long startPosition)
			=> ctx.ExtractZByteString(startPosition, ByteCharEncoding.Instance);
		public static string ExtractZByteString(this IByteBufferReadOnly ctx, long startPosition, Encoding encoding)
		{
			if (encoding == null)
				throw new ArgumentNullException(nameof(encoding));

			var byteCount = ctx.MeasureZStringAnsi(startPosition);
			string str = (byteCount != 0)
				? encoding.GetString(ctx.GetBytesReadOnly(startPosition, byteCount))
				: string.Empty
				;
			return str;
		}
		public static string ExtractZByteString(ReadOnlySpan<byte> bytes, Encoding encoding)
		{
			if (encoding == null)
				throw new ArgumentNullException(nameof(encoding));

			var byteCount = MeasureZByteString(bytes);
			string str = (byteCount != 0)
				? encoding.GetString(bytes.Slice(0, byteCount))
				: string.Empty
				;
			return str;
		}
		public static string ExtractZStringUni(ReadOnlySpan<byte> bytes)
		{
			var byteCount = MeasureZStringUni(bytes);
			string str = (byteCount != 0)
				? Encoding.Unicode.GetString(bytes.Slice(0, byteCount))
				: string.Empty
				;
			return str;
		}



		public static int MeasureZStringUni(
			this IByteBufferReadOnly ctx,
			long startPosition)
		{
			ReadOnlySpan<byte> span = ctx.GetByteSpanReadOnly(startPosition);
			return MeasureZStringUni(span);
		}

		public static int MeasureZStringUni(ReadOnlySpan<byte> span)
		{
			int i;
			var chars = MemoryMarshal.Cast<byte, char>(span);
			for (i = 0; i < chars.Length; i++)
			{
				char c = chars[i];
				if (c == 0)
					break;
			}

			return i * 2;
		}

		public static string ExtractZStringUni(this IByteBufferReadOnly ctx, long startPosition)
		{
			var cbStr = ctx.MeasureZStringUni(startPosition);
			if (cbStr > int.MaxValue)
				throw new InternalBufferOverflowException();

			string str = (cbStr != 0)
				? Encoding.Unicode.GetString(ctx.GetBytesReadOnly(startPosition, cbStr))
				: string.Empty
				;
			return str;
		}
		#endregion
	}
}
