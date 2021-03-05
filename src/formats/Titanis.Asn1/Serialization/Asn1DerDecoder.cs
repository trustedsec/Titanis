using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Titanis.IO;

namespace Titanis.Asn1.Serialization
{
	/// <summary>
	/// Implements a decoder to read messages encoded with
	/// ASN.1 Distinguished Encoding Rules (DER)
	/// </summary>
	/// <remarks>
	/// Call <see cref="Asn1Encoding.CreateDecoder(IByteSource)"/> or one of its overloads
	/// on <see cref="Asn1DerEncoding"/> to use this class.
	/// </remarks>
	/// <seealso cref="Asn1DerEncoding"/>
	public class Asn1DerDecoder : Asn1Decoder
	{
		internal Asn1DerDecoder(IByteSource reader)
		{
			if (reader is null)
				throw new ArgumentNullException(nameof(reader));

			this._reader = reader;
			if (reader is IByteSource seekable && seekable.CanSeek)
				this._sourceLength = seekable.Length - seekable.Position;
			else
				this._sourceLength = UnboundedLength;
			this._frame = new Asn1DecoderFrame { endIndex = this._sourceLength };
		}

		private IByteSource _reader;
		private long _sourceLength;


		private const long UnboundedLength = long.MinValue;

		private Asn1DecoderFrame _frame;
		private long _endIndex => this._frame.endIndex;
		private bool IsIndefiniteLength => this._frame.IsIndefiniteLength;
		private long BytesLeft => (this.IsIndefiniteLength) ? UnboundedLength : this._endIndex - this.Position;

		/// <inheritdoc/>
		public override bool IsEndOfTuple => this.IsIndefiniteLength
			? this.PeekTag().IsEot
			: this.IsEndOfDefTuple;

		private bool IsEndOfDefTuple
			=> (this.Position >= this._endIndex);

		private long _position = 0;
		private long Position => this._position;

		public IByteSource GetReader() => this._reader;
		private byte _ReadNextByte()
		{
			var b = this._reader.ReadByte();
			this._position++;
			return b;
		}
		private void _Advance(long count)
		{
			this._position += count;
			this._reader.Advance(count);
		}
		private ReadOnlySpan<byte> _Consume(int count)
		{
			this._position += count;
			return this._reader.Consume(count);
		}

		///// <summary>
		///// Reads the next byte from the source without advancing.
		///// </summary>
		///// <returns>The value of the next byte, if available; otherwise, <c>-1</c>.</returns>
		///// <remarks>
		///// IF the decoder is at the end of the current TLV, this method returns
		///// <c>-1</c>, even though more bytes may be available from the
		///// underlying <see cref="IByteSource"/>.
		///// </remarks>
		//internal int PeekByte()
		//	=> (!this.IsEndOfTuple)
		//		? this._reader.PeekByte()
		//		: -1;
		/// <summary>
		/// Reads the next byte from the source.
		/// </summary>
		/// <returns>The value of the next byte, if available; otherwise, <c>-1</c>.</returns>
		/// <exception cref="EndOfStreamException">End of stream or current TLV reached</exception>
		private byte ReadNextByteWithinTuple()
			=> (this._frame.IsIndefiniteLength || !this.IsEndOfDefTuple)
				? this._ReadNextByte()
				: throw new EndOfStreamException();

		private Asn1Tag _peekedTag;
		private bool _hasPeekTag;
		private long _peekedLength;

		/// <inheritdoc/>
		public override Asn1Tag PeekTag()
		{
			if (!this._hasPeekTag)
			{
				// DecodeTag and DecodeLength call IsEndOfTuple

				if (this._frame.IsIndefiniteLength || !this.IsEndOfDefTuple)
				{
					this._peekedTag = this.DecodeTag();
					this._peekedLength = this.DecodeLength();
					this._hasPeekTag = true;
				}
				else
				{
					this._peekedTag = new Asn1Tag();
				}
			}

			return this._peekedTag;
		}

		public static void DoSomething<TClass>(TClass t)
			where TClass : class
		{

		}

		// [X.690] § 8.1.2
		internal Asn1Tag DecodeTag()
		{
			uint b0 = this.ReadNextByteWithinTuple();

			const uint TagLeaderByte = 0x1F;
			if ((b0 & TagLeaderByte) == TagLeaderByte)
			{
				// This is a compound tag
				uint tagValue = 0;
				uint tagBytes = 0;
				uint bx;
				do
				{
					bx = this.ReadNextByteWithinTuple();
					if (bx != 0 || tagBytes != 0)
					{
						if (tagValue >= 4)
							throw new InvalidDataException(Messages.Asn1DerDecoder_TagOverflow);

						tagValue <<= 7;
						tagValue |= (bx & 0x7F);

						tagBytes++;
					}
				} while ((bx & 0x80) != 0);

				var flags = (Asn1TagFlags)(b0 & 0xE0);
				return new Asn1Tag((int)tagValue, flags);
			}
			else
			{
				// This is a simple tag
				// Unpack the flag bits
				return new Asn1Tag((byte)b0);
			}
		}
		/// <inheritdoc/>
		public override Asn1DecoderFrame DecodeTlvStart(Asn1Tag expectedTag)
		{
			Asn1Tag tag;
			long length;
			if (this._hasPeekTag)
			{
				tag = this._peekedTag;
				length = this._peekedLength;
				this.ClearPeekState();
			}
			else
			{
				tag = this.DecodeTag();
				length = this.DecodeLength();
			}

			if (
				!expectedTag.IsEmpty
				&& ((tag._value | Asn1Tag.ConstructedFlag) != (expectedTag._value | Asn1Tag.ConstructedFlag))
				)
				throw new FormatException(string.Format(Messages.Asn1_UnexpectedTag, expectedTag.TagNumber, tag));

			long innerEndIndex;
			if (length < 0)
			{
				if (!tag.IsConstructed)
					throw new InvalidDataException(Messages.AsnDerDecoder_IndefLengthPrimitive);

				// Indefinite length encoding
				innerEndIndex = -Math.Abs(this._endIndex);
			}
			else
			{
				// UNDONE: NULL TLVs may still contain data
				//if (actual == (byte)Asn1Tag.Null)
				//	return 0;

				innerEndIndex = this.Position + length;
				if (innerEndIndex > Math.Abs(this._endIndex))
					throw new InvalidDataException(Messages.Asn1DerDecoder_InnerLengthOverflow);
			}

			Asn1DecoderFrame outerFrame = this._frame;
			this._frame = new Asn1DecoderFrame()
			{
				endIndex = innerEndIndex,
				tag = tag
			};
			return outerFrame;
		}

		private void ClearPeekState()
		{
			this._hasPeekTag = false;
		}

		private void EnsurePrimitive()
		{
			if (this._frame.IsConstructed)
				throw new InvalidDataException(Messages.Asn1DerDecoder_ConstructedNotPermitted);
		}

		/// <summary>
		/// Decodes a TLV length.
		/// </summary>
		/// <returns>For definite-length encoding, the length read;
		/// for indefinite-length encoding, <c>-1</c>.</returns>
		/// <exception cref="EndOfStreamException"></exception>
		public long DecodeLength()
		{
			if (!this._frame.IsConstructed && this.IsEndOfDefTuple)
				throw new EndOfStreamException();

			int lengthByte = this.ReadNextByteWithinTuple();
			if (lengthByte < 0x80)
			{
				return lengthByte;
			}
			else if (lengthByte == 0x80)
			{
				return -1;
			}
			else // if (lengthByte >= 0x80)
			{
				lengthByte -= 0x80;

				long length = 0;
				int cbLength = 0;
				for (int i = 0; i < lengthByte; i++)
				{
					byte b = this.ReadNextByteWithinTuple();
					if (b != 0 || cbLength != 0)
					{
						if (cbLength >= 8)
							throw new NotSupportedException(Messages.Asn1DerDecoder_LengthOverflow);

						cbLength++;
					}
					length <<= 8;
					length |= b;
				}
				if (length < 0)
					throw new NotSupportedException(Messages.Asn1DerDecoder_LengthOverflow);

				return length;
			}
		}

		private void SkipTlvs()
		{
			if (this.IsIndefiniteLength)
			{
				while (!this.PeekTag().IsEot)
				{
					var frame = this.DecodeTlvStart(Asn1Tag.Empty);
					this.SkipTlvs();
					this.CloseTlv(frame);
				}
			}
			else
			{
				this._Advance(this.BytesLeft);
			}
		}

		/// <inheritdoc/>
		public override void CloseTlv(in Asn1DecoderFrame frame)
		{
			// TODO: Does ''frame'' need to be verified?

			this.SkipTlvs();
			this.ClearPeekState();
			this._frame = frame;
		}

		/// <summary>
		/// Decodes a value from ASN.1 DER-encoded data.
		/// </summary>
		/// <typeparam name="T">Type of value to decode</typeparam>
		/// <param name="bytes">Bytes to decode</param>
		/// <returns>The value decoded.</returns>
		public static T Decode<T>(ReadOnlyMemory<byte> bytes)
			where T : IAsn1DerEncodableTlv, new()
		{
			T obj = new T();
			Decode(bytes, obj);
			return obj;
		}
		/// <summary>
		/// Decodes a value from ASN.1 DER-encoded data.
		/// </summary>
		/// <typeparam name="T">Type of value to decode</typeparam>
		/// <param name="token">Bytes to decode</param>
		/// <param name="obj">Object to decode into</param>
		/// <returns>The value decoded.</returns>
		public static void Decode<T>(ReadOnlyMemory<byte> token, T obj)
			where T : IAsn1DerEncodableTlv, new()
		{
			Asn1DerDecoder decoder = new Asn1DerDecoder(new ByteMemoryReader(token));
			obj.DecodeTlv(decoder);
		}
		/// <summary>
		/// Decodes a value.
		/// </summary>
		/// <typeparam name="T">Type of value to decode</typeparam>
		/// <returns>The value decoded.</returns>
		public T Decode<T>() where T : IAsn1DerEncodableTlv, new()
		{
			T obj = new T();
			obj.DecodeTlv(this);
			return obj;
		}

		/// <inheritdoc/>
		// [X690] § 8.2
		public override bool DecodeBool()
		{
			this.EnsurePrimitive();
			return (0 != this.ReadNextByteWithinTuple());
		}
		#region Integers
		/// <summary>
		/// Decodes an integer.
		/// </summary>
		/// <param name="byteLimit">Maximum number of bytes</param>
		/// <returns>A <see cref="long"/> decoded from the stream</returns>
		/// <remarks>
		/// Any leading zero bytes do not count against <paramref name="byteLimit"/>.
		/// If the high bit of the first byte is set, the number is sign-extended.
		/// </remarks>
		// [X690] § 8.3
		private long DecodeInteger(int byteLimit, bool signed)
		{
			EnsurePrimitive();

			byte b0 = this.ReadNextByteWithinTuple();
			long value = signed ? (sbyte)b0 : b0;
			while (this.BytesLeft > 0)
			{
				if (value != 0)
					byteLimit--;
				if (byteLimit == 0)
					throw new OverflowException(Messages.Asn1_IntegerOverflow);

				value <<= 8;
				value |= this.ReadNextByteWithinTuple();
			}
			return value;
		}
		/// <inheritdoc/>
		public override byte DecodeByte()
			=> (byte)this.DecodeInteger(1, true);
		/// <inheritdoc/>
		public override sbyte DecodeSByte()
			=> (sbyte)this.DecodeInteger(1, true);
		/// <inheritdoc/>
		public override short DecodeInt16()
			=> (short)DecodeInteger(2, true);
		/// <inheritdoc/>
		public override int DecodeInt32()
			=> (int)DecodeInteger(4, true);
		/// <inheritdoc/>
		public override long DecodeInt64()
			=> this.DecodeInteger(8, true);

		#region Unsigned
		/// <inheritdoc/>
		public override ushort DecodeUInt16()
			=> (ushort)this.DecodeInteger(2, false);
		/// <inheritdoc/>
		public override uint DecodeUInt32()
			=> (uint)this.DecodeInteger(4, false);
		/// <inheritdoc/>
		public override ulong DecodeUInt64()
			=> (ulong)this.DecodeInteger(8, false);
		/// <inheritdoc/>
		public override BigInteger DecodeBigInteger()
		{
			var byteCount = this.GetLength();
			byte[] bytes = this._Consume(byteCount).ToArray();
			if (BitConverter.IsLittleEndian)
				Array.Reverse(bytes);
			BigInteger bigint = new BigInteger(bytes);
			return bigint;
		}
		#endregion
		#endregion
		#region Floating-point
		// [X690] § 8.5
		public override decimal DecodeDecimal()
		{
			// TODO: Floating-point encodings
			throw new NotImplementedException();
		}
		// [X690] § 8.5
		public override double DecodeDouble()
		{
			throw new NotImplementedException();
		}
		// [X690] § 8.5
		public override float DecodeSingle()
		{
			throw new NotImplementedException();
		}
		#endregion
		/// <inheritdoc/>
		// [X690] § 8.6
		public override Asn1BitString DecodeBitString()
		{
			MemoryStream bits = new MemoryStream();
			int unused = -1;
			MemoryStream lastBits = new MemoryStream();
			this.DecodeByteStringInto(Asn1PredefTag.BitString, (b, f) =>
			{
				if (f)
				{
					if (unused >= 0)
					{
						bits.WriteByte((byte)unused);
						lastBits.Position = 0;
						lastBits.CopyTo(bits);
					}
					unused = b[0];
					lastBits.SetLength(0);
					lastBits.Write(b.Slice(1));
				}
				else
				{
					lastBits.Write(b);
				}
			});
			lastBits.Position = 0;
			if (bits.Length > 0)
			{
				lastBits.CopyTo(bits);
			}
			else
			{
				bits = lastBits;
			}

			return new Asn1BitString(bits.ToArray(), (byte)unused);
		}

		/// <inheritdoc/>
		// [X690] § 8.7
		public override byte[] DecodeOctetString()
		{
			return this.DecodeByteString(Asn1PredefTag.OctetString);
		}
		/// <inheritdoc/>
		// [X690] § 8.8
		public override DBNull DecodeNull()
		{
			if (this._frame.IsConstructed)
				throw new InvalidDataException(Messages.Asn1DerDecoder_ConstructedNotPermitted);
			if (this.BytesLeft > 0)
				throw new InvalidDataException(Messages.Asn1DerDecoder_NullHasValue);

			return DBNull.Value;
		}

		/// <inheritdoc/>
		// [X690] § 8.19
		public override Asn1Oid DecodeOid()
		{
			if (this._frame.IsConstructed)
				throw new InvalidDataException(Messages.Asn1DerDecoder_ConstructedNotPermitted);

			var bytes = this._Consume(this.GetLength());
			return DecodeOid(bytes);
		}

		/// <summary>
		/// Decodes an OID from a byte sequence.
		/// </summary>
		/// <param name="bytes">Byte sequence containing DER-encoded OID.</param>
		/// <returns></returns>
		/// <exception cref="FormatException"><paramref name="bytes"/> is improperly formatted</exception>
		// [X690] § 8.19
		public static Asn1Oid DecodeOid(ReadOnlySpan<byte> bytes)
		{
			if (bytes.Length == 0)
				throw new FormatException(Messages.Asn1DerDecoder_BadOidBytes);

			var initial = bytes[0];
			Asn1OidPart[] etc;
			if (bytes.Length > 1)
			{
				etc = DecodeRelativeOid(bytes);
			}
			else
			{
				etc = null;
			}

			var oid = new Asn1Oid(
				new Asn1OidPart(initial / 40),
				new Asn1OidPart(initial % 40),
				etc
				);
			return oid;
		}

		// [X690] § 8.20
		public static Asn1OidPart[] DecodeRelativeOid(ReadOnlySpan<byte> bytes)
		{
			Asn1OidPart[] etc;
			List<Asn1OidPart> parts = null;
			int value = 0;
			for (int i = 1; i < bytes.Length; i++)
			{
				byte b = bytes[i];
				value <<= 7;
				if (b >= 0x80)
				{
					value |= (b & 0x7F);
				}
				else
				{
					value |= b;
					if (parts == null)
						parts = new List<Asn1OidPart>();
					parts.Add(new Asn1OidPart(value));
					value = 0;
				}
			}
			if (value != 0)
				throw new FormatException(Messages.Asn1DerDecoder_BadOidBytes);

			etc = parts?.ToArray();
			return etc;
		}

		// [X690] § 8.20
		public override Asn1OidPart[] DecodeRelativeOid()
		{
			Asn1OidPart[] etc = DecodeRelativeOid(this._Consume(this.GetLength()));
			return etc;
		}

		/// <inheritdoc/>
		public override char DecodeAnsiChar()
			=> (char)this.ReadNextByteWithinTuple();
		/// <inheritdoc/>
		public override char DecodeUtf8Char()
		{
			var r = this.DecodeUtf8Rune();
			uint value = (uint)r.Value;
			if (value < 0xD800)
				return (char)value;
			else if (value < 0xE000)
				// Surrogate range
				throw new NotSupportedException(Messages.Asn1DerDecoder_Utf8CharOverflow);
			else if (value < 0x10000)
				return (char)value;
			else
				throw new NotSupportedException(Messages.Asn1DerDecoder_Utf8CharOverflow);
		}
		/// <inheritdoc/>
		public override Rune DecodeUtf8Rune()
		{
			byte b0 = this.ReadNextByteWithinTuple();
			if (b0 < 0x80)
				return new Rune(b0);
			else
			{
				int value = (b0 & 0x1F);
				int count =
					((b0 & 0xE0) == 0xC0) ? 1
					: ((b0 & 0xF0) == 0xE0) ? 2
					: ((b0 & 0xF1) == 0xF0) ? 3
					: throw new InvalidDataException(Messages.Asn1DerDecoder_Utf8CharOverflow);
				while (--count >= 0)
				{
					byte b1 = this.ReadNextByteWithinTuple();
					if ((b1 & 0xC0) != 0x80)
						throw new InvalidDataException(Messages.Asn1DerDecoder_InvalidUtf8Data);

					value <<= 6;
					value |= (b1 & 0x3F);
				}

				return new Rune(value);
			}
		}

		/// <inheritdoc/>
		// [X690] § 8.23
		public override string DecodeUtf8tring()
		{
			if (this._frame.IsConstructed)
			{
				var bytes = this.DecodeByteString(Asn1PredefTag.UTF8String);
				string str = Encoding.UTF8.GetString(bytes);
				return str;
			}
			else
			{
				var byteCount = this.GetLength();
				string str = Encoding.UTF8.GetString(this._Consume(byteCount));
				return str;
			}
		}

		private static int ParseDigit(byte c)
			// Input has already been validated
			=> c - '0';
		//throw new InvalidDataException(Messages.Asn1DerDecoder_InvalidDecimalDigit);
		private static int ParseDoubleDigit(ReadOnlySpan<byte> str, int index)
		{
			return
				ParseDigit(str[index]) * 10
				+ ParseDigit(str[index + 1])
				;
		}
		private static bool IsNumeric(ReadOnlySpan<byte> chars)
		{
			foreach (var c in chars)
			{
				if (!char.IsDigit((char)c))
					return false;
			}
			return true;
		}
		public override DateTime DecodeUtcTime()
		{
			//"20370913024805Z"
			// TODO: Verify actual format used
			var str = this.DecodeByteString(Asn1PredefTag.UtcTime);
			if (
				str.Length < 12
				|| !IsNumeric(str.AsSpan()[..12])
				)
				throw new FormatException(Messages.Asn1_InvalidDateTimeData);

			int year = 2000 + ParseDoubleDigit(str, 0);
			int month = ParseDoubleDigit(str, 2);
			int day = ParseDoubleDigit(str, 4);

			int hour = ParseDoubleDigit(str, 6);
			int minute = ParseDoubleDigit(str, 8);
			int seconds = ParseDoubleDigit(str, 10);

			// TODO: Handle Z

			// TODO: Implement milliseconds
			if (str.Length > 12 && str[12] == '.')
				;

			DateTime dt = new DateTime(year, month, day, hour, minute, seconds);
			return dt;
		}

		public override DateTime DecodeDateTime()
		{
			//"20370913024805Z"
			// TODO: Verify actual format used
			var str = this.DecodeByteString(Asn1PredefTag.DateTime);
			if (
				str.Length < 8
				|| !IsNumeric(str.AsSpan().Slice(0, 8))
				)
				throw new FormatException(Messages.Asn1_InvalidDateTimeData);

			int year = ParseDoubleDigit(str, 0) * 100 + ParseDoubleDigit(str, 2);
			int month = ParseDoubleDigit(str, 4);
			int day = ParseDoubleDigit(str, 6);

			if (
				str.Length < 14
				|| !IsNumeric(str.AsSpan().Slice(8, 6))
				)
				throw new FormatException(Messages.Asn1_InvalidDateTimeData);

			int hour = ParseDoubleDigit(str, 8);
			int minute = ParseDoubleDigit(str, 10);
			int seconds = ParseDoubleDigit(str, 12);

			// TODO: Implement milliseconds
			if (str.Length > 16 && str[14] == '.')
				;

			DateTime dt = new DateTime(year, month, day, hour, minute, seconds);
			return dt;
		}


		/// <summary>
		/// Gets the number of bytes left as an <see cref="int"/>.
		/// </summary>
		/// <returns>An <see cref="int"/> count of bytes left.</returns>
		/// <exception cref="InvalidDataException">The number of bytes is too large for <see cref="int"/></exception>
		/// <remarks>
		/// Although X.690 doesn't specify a maximum number of bytes,
		/// this implementation can only handle sizes up to <see cref="int.MaxValue"/>.
		/// </remarks>
		private int GetLength()
		{
			var byteCount = this.BytesLeft;
			if (byteCount > int.MaxValue)
				throw new InvalidDataException(Messages.Asn1DerDecoder_LengthOverflow);
			return (int)byteCount;
		}

		private delegate void ChunkReceiver(ReadOnlySpan<byte> data, bool isFirstInTuple);

		private void DecodeByteStringInto(
			Asn1PredefTag tag,
			ChunkReceiver receiver
			)
		{
			if (this._frame.IsIndefiniteLength)
			{
				while (this.PeekTag().TagNumber == (uint)tag)
				{
					var outer = this.DecodeTlvStart(tag);
					this.DecodeByteStringInto(tag, receiver);
					this.CloseTlv(outer);
				}
			}
			else
			{
				bool first = true;
				do
				{
					var block = this._Consume((int)Math.Min(4096, this.BytesLeft));
					receiver(block, first);
					first = false;
				} while (this.BytesLeft > 0);
			}
		}

		/// <summary>
		/// Reads a string of bytes.
		/// </summary>
		/// <param name="tag">Tag denoting type of string</param>
		/// <returns></returns>
		/// <exception cref="InvalidDataException"></exception>
		private byte[] DecodeByteString(Asn1PredefTag tag)
		{
			if (this._frame.IsConstructed)
			{
				MemoryStream memstream = new MemoryStream();
				this.DecodeByteStringInto(tag, (b, _) => memstream.Write(b));
				return memstream.ToArray();
			}
			else
			{
				return this._Consume(this.GetLength()).ToArray();
			}
		}

		//public void DecodeObjTlv<T>(T value)
		//	where T : IAsn1DerEncodable
		//{
		//	value.DecodeTlv(this);
		//}

		//public void DecodeObjTlv<T>(Asn1Tag tag, T value)
		//	where T : IAsn1DerEncodable
		//{
		//	int endIndex = this.ReadTlvStart((byte)tag);
		//	value.DecodeValue(this);
		//	this.CloseTlv(endIndex);
		//}

		//public T DecodeObjTlv<T, TNode>(TNode node)
		//	where TNode : IAsn1Node<T>
		//{
		//	return node.DecodeTlv(this);
		//}

		//public T DecodeObjTlv<T, TNode>(Asn1Tag expectedTag, TNode node)
		//	where TNode : IAsn1Node<T>
		//{
		//	int outerEndIndex = this.ReadTlvStart(expectedTag);
		//	T value = node.DecodeValue(this);
		//	this.CloseTlv(outerEndIndex);
		//	return value;
		//}

		//public bool TryDecodeObjTlv<T, TNode>(Asn1Tag expectedTag, TNode node, out T value)
		//	where TNode : IAsn1Node<T>
		//{
		//	if (this.PeekTag() == expectedTag)
		//	{
		//		value = this.DecodeObjTlv<T, TNode>(expectedTag, node);
		//		return true;
		//	}
		//	else
		//	{
		//		value = default;
		//		return false;
		//	}
		//}
	}
}
