using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Titanis.IO;

namespace Titanis.Asn1.Serialization
{
	public enum Asn1DerDecoderOptions
	{
		None = 0,
		AllowBer = 1
	}

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
		internal Asn1DerDecoder(IByteSource reader, Asn1DerDecoderOptions options)
		{
			if (reader is null)
				throw new ArgumentNullException(nameof(reader));

			this._reader = reader;
			this._options = options;
			if (reader is IByteSource seekable && seekable.CanSeek)
				this._sourceLength = seekable.Length - seekable.Position;
			else
				this._sourceLength = UnboundedLength;

			this._frame = new Asn1DecoderFrame(this._sourceLength, Asn1Tag.Empty);
		}

		private IByteSource _reader;
		private readonly Asn1DerDecoderOptions _options;
		private long _sourceLength;
		private bool AllowBer => 0 != (this._options & Asn1DerDecoderOptions.AllowBer);

		/// <summary>
		/// The value indicating an unbounded length.  Used for BER.
		/// </summary>
		private const long UnboundedLength = long.MinValue;

		private Asn1DecoderFrame _frame;
		private long FrameEndPosition => this._frame.endPosition;
		private bool IsIndefiniteLength => this._frame.IsIndefiniteLength;
		private long BytesLeftInTuple => (this.IsIndefiniteLength) ? UnboundedLength : this.FrameEndPosition - this.Position;

		/// <inheritdoc/>
		public override bool IsEndOfTuple => this.IsIndefiniteLength
			? this.PeekTag().IsEot
			: this.IsEndOfDefTuple;

		private bool IsEndOfDefTuple
			=> (this.Position >= this.FrameEndPosition);

		//private long _position = 0;
		private long Position
		{
			get => this._reader.Position;
			set => this._reader.Position = value;
		}

		public IByteSource GetReader() => this._reader;
		private byte _ReadNextByte()
		{
			var b = this._reader.ReadByte();
			return b;
		}
		private void _Advance(long count)
		{
			if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

			this._reader.Advance(count);
		}
		private ReadOnlySpan<byte> _Consume(int count)
		{
			return this._reader.Consume(count);
		}

		/// <summary>
		/// Reads the next byte from the source.
		/// </summary>
		/// <returns>The value of the next byte, if available; otherwise, <c>-1</c>.</returns>
		/// <exception cref="EndOfStreamException">End of stream or current TLV reached</exception>
		private byte ReadNextByteWithinTuple()
			=> (this._frame.IsIndefiniteLength || !this.IsEndOfDefTuple)
				? this._ReadNextByte()
				: throw new EndOfStreamException();

		private long _peekedTagPos = -1;
		private Asn1Tag _peekedTag;

		/// <inheritdoc/>
		public override Asn1Tag PeekTag()
		{
			if (this.Position != this._peekedTagPos)
			{
				// DecodeTag and DecodeLength call IsEndOfTuple

				if (this._frame.IsIndefiniteLength || !this.IsEndOfDefTuple)
				{
					this._peekedTagPos = this.Position;
					this._peekedTag = this.DecodeTag();
					this.Position = this._peekedTagPos;
				}
				else
				{
					this._peekedTag = new Asn1Tag();
				}
			}

			return this._peekedTag;
		}

		public static T DecodeTlv<T>(ReadOnlyMemory<byte> bytes)
			where T : IAsn1DerDecodableTlv<T>
		{
			Debug.Assert(bytes.Span[0] != 0);
			Asn1DerDecoder decoder = new Asn1DerDecoder(new ByteMemoryReader(bytes), Asn1DerDecoderOptions.AllowBer);
			var value = decoder.DecodeTlv<T>();
			return value;
		}

		public T DecodeTlv<T>()
			where T : IAsn1DerDecodableTlv<T>
		{
			return T.DecodeTlvFrom(this);
		}

		public static bool TryDecodeTlv<T>(ReadOnlyMemory<byte> bytes, out T? value)
			where T : IAsn1DerDecodableTlv<T>
		{
			Asn1DerDecoder decoder = new Asn1DerDecoder(new ByteMemoryReader(bytes), Asn1DerDecoderOptions.AllowBer);
			return T.TryDecodeTlvFrom(decoder, out value);
		}

		public bool TryDecodeTlv<T>([NotNullWhen(true)] out T? value)
			where T : IAsn1DerDecodableTlv<T>
		{
			return T.TryDecodeTlvFrom(this, out value);
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
			if (expectedTag.IsEmpty) throw new ArgumentNullException(nameof(expectedTag));

			var tag = this.PeekTag();
			// Verify the tag but don't consume it yet
			// This gives an exception handler a chance to handle the condition.
			if (((tag._value | Asn1Tag.ConstructedFlag) != (expectedTag._value | Asn1Tag.ConstructedFlag)))
				throw new Asn1UnexpectedTagException(this, expectedTag, tag, null);

			tag = this.DecodeTag();
			var length = this.DecodeLength();

			long innerEndIndex;
			if (length < 0)
			{
				if (!tag.IsConstructed)
					throw new InvalidDataException(Messages.AsnDerDecoder_IndefLengthPrimitive);

				// Indefinite length encoding
				innerEndIndex = -Math.Abs(this.FrameEndPosition);
			}
			else
			{
				// UNDONE: NULL TLVs may still contain data
				//if (actual == (byte)Asn1Tag.Null)
				//	return 0;

				innerEndIndex = this.Position + length;
				if (innerEndIndex > Math.Abs(this.FrameEndPosition))
					throw new InvalidDataException(Messages.Asn1DerDecoder_InnerLengthOverflow);
			}

			Asn1DecoderFrame outerFrame = this._frame;
			this._frame = new Asn1DecoderFrame(innerEndIndex, tag);
			return outerFrame;
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

				// Eat the end tag
				var tag = this.DecodeTag();
				Debug.Assert(tag == default);
				var length = this.DecodeLength();
				// TODO: How to handle if length isn't 0
				Debug.Assert(length == 0);
			}
			else
			{
				this._Advance(this.BytesLeftInTuple);
			}
		}

		/// <inheritdoc/>
		public override void CloseTlv(in Asn1DecoderFrame frame)
		{
			// TODO: Does ''frame'' need to be verified?

			this.SkipTlvs();
			this._frame = frame;
		}

		public T[] DecodeListTlv<T>(Asn1Tag listTag)
			where T : IAsn1DerDecodableTlv<T>
		{
			var frame = this.DecodeTlvStart(listTag);
			var elems = DecodeValueList<T>();

			this.CloseTlv(frame);

			return elems;
		}

		public T[] DecodeValueList<T>() where T : IAsn1DerDecodableTlv<T>
		{
			if (this.IsEndOfDefTuple)
				return Array.Empty<T>();

			List<T> elems = new List<T>();
			do
			{
				var elem = T.DecodeTlvFrom(this);
				elems.Add(elem);
			} while (!this.IsEndOfTuple);

			return elems.ToArray();
		}

		public T[] DecodeListTlv<T>(Asn1Tag listTag, Func<Asn1DerDecoder, T> decodeItemFunc)
		{
			var frame = this.DecodeTlvStart(listTag);
			List<T> elems = new List<T>();
			while (!this.IsEndOfTuple)
			{
				var elem = decodeItemFunc(this);
				elems.Add(elem);
			}

			this.CloseTlv(frame);

			return elems.ToArray();
		}

		public T DecodeTaggedValue<T>(Asn1Tag tag, Func<Asn1DerDecoder, T> decodeFunc)
		{
			var frame = this.DecodeTlvStart(tag);
			var inner = decodeFunc(this);
			this.CloseTlv(frame);

			return inner;
		}

		public T DecodeExplicitTaggedTlv<T>(Asn1Tag tag)
			where T : IAsn1DerDecodableTlv<T>
		{
			var frame = this.DecodeTlvStart(tag);
			var inner = T.DecodeTlvFrom(this);
			this.CloseTlv(frame);

			return inner;
		}

		public static T DecodeValue<T>(ReadOnlyMemory<byte> bytes)
			where T : IAsn1DerDecodableValue<T>
		{
			Asn1DerDecoder decoder = new Asn1DerDecoder(new ByteMemoryReader(bytes), Asn1DerDecoderOptions.AllowBer);
			var value = decoder.DecodeValue<T>();
			return value;
		}

		struct StringWrapper<TString> : IAsn1DerDecodableTlv<StringWrapper<TString>>, IAsn1DerDecodableValue<StringWrapper<TString>>
			where TString : struct, IAsn1String, IAsn1String<TString>
		{
			private StringWrapper(string str)
			{
				this.str = str;
			}

			internal string str;

			public static StringWrapper<TString> DecodeTlvFrom(Asn1DerDecoder decoder)
			{
				var str = decoder.DecodeStringTlv<TString>();
				return new StringWrapper<TString>(str.Value);
			}

			public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out StringWrapper<TString> value)
			{
				if (decoder.CheckTag(TString.StaticTag))
				{
					value = DecodeTlvFrom(decoder);
					return true;
				}
				else
				{
					value = default;
					return false;
				}
			}

			public static StringWrapper<TString> DecodeValueFrom(Asn1DerDecoder decoder)
			{
				return new StringWrapper<TString>(decoder.DecodeValue<StringWrapper<TString>>().str);
			}
		}

		public static string DecodeStringTlv<T>(ReadOnlyMemory<byte> bytes)
			where T : struct, IAsn1String, IAsn1String<T>
		{
			return DecodeTlv<StringWrapper<T>>(bytes).str;
		}

		public T DecodeValue<T>()
			where T : IAsn1DerDecodableValue<T>
		{
			var inner = T.DecodeValueFrom(this);
			return inner;
		}

		public T DecodeTaggedValue<T>(Asn1Tag tag)
			where T : IAsn1DerDecodableValue<T>
		{
			var frame = this.DecodeTlvStart(tag);
			var inner = T.DecodeValueFrom(this);
			this.CloseTlv(frame);

			return inner;
		}

		public bool TryDecodeExplicitTaggedTlv<T>(Asn1Tag tag, [NotNullWhen(true)] out T? value)
			where T : IAsn1DerDecodableTlv<T>
		{
			if (this.PeekTag() == tag)
			{
				var frame = this.DecodeTlvStart(tag);
				var inner = T.DecodeTlvFrom(this);
				this.CloseTlv(frame);

				value = inner;
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}

		public bool TryDecodeTaggedValue<T>(Asn1Tag tag, [NotNullWhen(true)] out T? value)
			where T : IAsn1DerDecodableValue<T>
		{
			if (this.PeekTag() == tag)
			{
				var frame = this.DecodeTlvStart(tag);
				var inner = T.DecodeValueFrom(this);
				this.CloseTlv(frame);

				value = inner;
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}

		/// <inheritdoc/>
		// [X690] § 8.2
		public override bool DecodeBoolTlv(Asn1Tag tag)
		{
			var frame = this.DecodeTlvStart(tag);
			this.EnsurePrimitive();
			var value = (0 != this.ReadNextByteWithinTuple());
			this.CloseTlv(frame);

			return value;
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
		private long DecodeIntegerTlv(Asn1Tag expectedTag, int byteLimit, bool signed)
		{
			var frame = this.DecodeTlvStart(expectedTag);

			EnsurePrimitive();

			byte b0 = this.ReadNextByteWithinTuple();
			long value = signed ? (sbyte)b0 : b0;
			while (this.BytesLeftInTuple > 0)
			{
				if (value != 0)
					byteLimit--;
				if (byteLimit == 0)
					throw new OverflowException(Messages.Asn1_IntegerOverflow);

				value <<= 8;
				value |= this.ReadNextByteWithinTuple();
			}

			this.CloseTlv(frame);
			return value;
		}
		/// <inheritdoc/>
		public override byte DecodeIntegerTlvAsByte(Asn1Tag tag)
			=> (byte)this.DecodeIntegerTlv(tag, 1, true);
		/// <inheritdoc/>
		public override sbyte DecodeIntegerTlvAsSByte(Asn1Tag tag)
			=> (sbyte)this.DecodeIntegerTlv(tag, 1, true);
		/// <inheritdoc/>
		public override short DecodeIntegerTlvAsInt16(Asn1Tag tag)
			=> (short)DecodeIntegerTlv(tag, 2, true);
		/// <inheritdoc/>
		public override int DecodeIntegerTlvAsInt32(Asn1Tag tag)
			=> (int)DecodeIntegerTlv(tag, 4, true);
		/// <inheritdoc/>
		public override long DecodeIntegerTlvAsInt64(Asn1Tag tag)
			=> this.DecodeIntegerTlv(tag, 8, true);

		#region Unsigned
		/// <inheritdoc/>
		public override ushort DecodeIntegerTlvAsUInt16(Asn1Tag tag)
			=> (ushort)this.DecodeIntegerTlv(tag, 2, false);
		/// <inheritdoc/>
		public override uint DecodeIntegerTlvAsUInt32(Asn1Tag tag)
			=> (uint)this.DecodeIntegerTlv(tag, 4, false);
		/// <inheritdoc/>
		public override ulong DecodeIntegerTlvAsUInt64(Asn1Tag tag)
			=> (ulong)this.DecodeIntegerTlv(tag, 8, false);
		/// <inheritdoc/>
		public override BigInteger DecodeIntegerTlvAsBigInteger(Asn1Tag tag)
		{
			var frame = this.DecodeTlvStart(tag);

			BigInteger bigint = this.DecodeBigIntegerValue();
			this.CloseTlv(frame);

			return bigint;
		}

		public BigInteger DecodeBigIntegerValue()
		{
			var byteCount = this.GetLength();
			byte[] bytes = this._Consume(byteCount).ToArray();
			BigInteger bigint = new BigInteger(bytes, false, true);
			return bigint;
		}
		#endregion
		#endregion

		public long DecodeEnumeratedTlv() => this.DecodeIntegerTlv(Asn1PredefTag.Enumerated, 8, false);
		public long DecodeEnumeratedTlv(Asn1Tag tag) => this.DecodeIntegerTlv(tag, 8, false);

		#region Floating-point
		// [X690] § 8.5
		public override decimal DecodeRealTlvAsDecimal(Asn1Tag tag)
		{
			// TODO: Floating-point encodings
			throw new NotImplementedException();
		}
		// [X690] § 8.5
		public override double DecodeRealTlvAsDouble(Asn1Tag tag)
		{
			throw new NotImplementedException();
		}
		// [X690] § 8.5
		public override float DecodeRealTlvAsSingle(Asn1Tag tag)
		{
			throw new NotImplementedException();
		}
		#endregion

		public override Asn1BitString DecodeBitStringTlv(Asn1Tag tag)
		{
			var frame = this.DecodeTlvStart(tag);
			var bitstring = this.DecodeBitStringValue();
			this.CloseTlv(frame);

			return bitstring;
		}
		public TEnum DecodeBitStringTlv<TEnum>(Asn1Tag tag)
			where TEnum : struct, Enum, IConvertible
		{
			var frame = this.DecodeTlvStart(tag);
			var bitstring = this.DecodeBitStringValue();
			this.CloseTlv(frame);

			var value = bitstring.ToUInt64();
			var enumValue = (TEnum)Enum.ToObject(typeof(TEnum), value);
			return enumValue;
		}
		/// <inheritdoc/>
		// [X690] § 8.6
		public Asn1BitString DecodeBitStringValue()
		{
			if (this.IsIndefiniteLength)
			{
				// TODO: OPT
				MemoryStream bits = new MemoryStream();
#if DEBUG
				var frame = this._frame;
#endif
				int unusedBits = 0;
				this.DecodeTupleContentInto((b, f) =>
				{
					Debug.Assert(b.Length > 0);
					unusedBits = b[0];
					b = b.Slice(1);

					bits.Write(b);
				});

#if DEBUG
				Debug.Assert(frame.endPosition == this._frame.endPosition);
#endif

				return new Asn1BitString(bits.ToArray(), (byte)unusedBits);
			}
			else
			{
				if (this.BytesLeftInTuple == 0)
				{
					return new Asn1BitString(Array.Empty<byte>(), 0);
				}
				else
				{
					byte unusedBits = this.ReadNextByteWithinTuple();
					var bits = this._Consume(checked((int)this.BytesLeftInTuple)).ToArray();
					return new Asn1BitString(bits, unusedBits);
				}
			}
		}

		/// <inheritdoc/>
		// [X690] § 8.7
		public override byte[] DecodeOctetStringTlv(Asn1Tag tag)
		{
			var frame = this.DecodeTlvStart(tag);
			byte[] bytes = this.DecodeOctetStringValue();
			this.CloseTlv(frame);
			return bytes;
		}

		public byte[] DecodeOctetStringValue()
		{
			return this.DecodeTupleContent();
		}

		/// <inheritdoc/>
		// [X690] § 8.8
		public override Asn1Null DecodeNullTlv(Asn1Tag tag)
		{
			var frame = this.DecodeTlvStart(tag);
			this.DecodeNullValue();
			this.CloseTlv(frame);

			return new Asn1Null();
		}

		public void DecodeNullValue()
		{
			if (this._frame.IsConstructed)
				throw new InvalidDataException(Messages.Asn1DerDecoder_ConstructedNotPermitted);
			if (this.BytesLeftInTuple > 0)
				throw new InvalidDataException(Messages.Asn1DerDecoder_NullHasValue);
		}

		/// <inheritdoc/>
		// [X690] § 8.19
		public override Asn1Oid DecodeOidTlv(Asn1Tag tag)
		{
			var frame = this.DecodeTlvStart(tag);
			this.EnsurePrimitive();

			var oid = this.DecodeOidValue();
			this.CloseTlv(frame);

			return oid;
		}
		public override Asn1Oid DecodeOidValue()
		{
			var bytes = this._Consume(this.GetLength());
			return DecodeOidBytes(bytes);
		}

		/// <summary>
		/// Decodes an OID from a byte sequence.
		/// </summary>
		/// <param name="bytes">Byte sequence containing DER-encoded OID.</param>
		/// <returns></returns>
		/// <exception cref="FormatException"><paramref name="bytes"/> is improperly formatted</exception>
		// [X690] § 8.19
		public static Asn1Oid DecodeOidBytes(ReadOnlySpan<byte> bytes)
		{
			if (bytes.Length == 0)
				throw new FormatException(Messages.Asn1DerDecoder_BadOidBytes);

			var initial = bytes[0];
			uint[] etc;
			if (bytes.Length > 1)
			{
				etc = DecodeRelativeOidValueFromBytes(bytes);
			}
			else
			{
				etc = null;
			}

			var oid = new Asn1Oid(
				[
				(initial / 40U),
				(initial % 40U),
				..etc
				]);
			return oid;
		}

		// [X690] § 8.20
		public static uint[] DecodeRelativeOidValueFromBytes(ReadOnlySpan<byte> bytes)
		{
			uint[] etc;
			List<uint> parts = null;
			uint value = 0;
			for (int i = 1; i < bytes.Length; i++)
			{
				byte b = bytes[i];
				value <<= 7;
				if (b >= 0x80)
				{
					value |= (b & 0x7FU);
				}
				else
				{
					value |= b;
					if (parts == null)
						parts = new List<uint>();
					parts.Add((value));
					value = 0;
				}
			}
			if (value != 0)
				throw new FormatException(Messages.Asn1DerDecoder_BadOidBytes);

			etc = parts?.ToArray();
			return etc;
		}

		// [X690] § 8.20
		public override uint[] DecodeRelativeOidTlv(Asn1Tag tag)
		{
			var frame = this.DecodeTlvStart(tag);

			uint[] etc = DecodeRelativeOidValueFromBytes(this._Consume(this.GetLength()));

			this.CloseTlv(frame);

			return etc;
		}

		///// <inheritdoc/>
		//public override char DecodeAnsiChar()
		//	=> (char)this.ReadNextByteWithinTuple();
		///// <inheritdoc/>
		//public override char DecodeUtf8Char()
		//{
		//	var r = this.DecodeUtf8Rune();
		//	uint value = (uint)r.Value;
		//	if (value < 0xD800)
		//		return (char)value;
		//	else if (value < 0xE000)
		//		// Surrogate range
		//		throw new NotSupportedException(Messages.Asn1DerDecoder_Utf8CharOverflow);
		//	else if (value < 0x10000)
		//		return (char)value;
		//	else
		//		throw new NotSupportedException(Messages.Asn1DerDecoder_Utf8CharOverflow);
		//}
		///// <inheritdoc/>
		//protected override Rune DecodeUtf8Rune()
		//{
		//	byte b0 = this.ReadNextByteWithinTuple();
		//	if (b0 < 0x80)
		//		return new Rune(b0);
		//	else
		//	{
		//		int value = (b0 & 0x1F);
		//		int count =
		//			((b0 & 0xE0) == 0xC0) ? 1
		//			: ((b0 & 0xF0) == 0xE0) ? 2
		//			: ((b0 & 0xF1) == 0xF0) ? 3
		//			: throw new InvalidDataException(Messages.Asn1DerDecoder_Utf8CharOverflow);
		//		while (--count >= 0)
		//		{
		//			byte b1 = this.ReadNextByteWithinTuple();
		//			if ((b1 & 0xC0) != 0x80)
		//				throw new InvalidDataException(Messages.Asn1DerDecoder_InvalidUtf8Data);

		//			value <<= 6;
		//			value |= (b1 & 0x3F);
		//		}

		//		return new Rune(value);
		//	}
		//}

		/// <inheritdoc/>
		// [X690] § 8.23
		public override string DecodeUtf8StringTlv(Asn1Tag tag)
		{
			var frame = this.DecodeTlvStart(tag);

			string str;
			if (this._frame.IsConstructed)
			{
				var bytes = this.DecodeTupleContent();
				str = Encoding.UTF8.GetString(bytes);
			}
			else
			{
				var byteCount = this.GetLength();
				str = Encoding.UTF8.GetString(this._Consume(byteCount));
			}

			this.CloseTlv(frame);
			return str;
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
		public override DateTime DecodeUtcTimeTlv(Asn1Tag tag)
		{
			var frame = this.DecodeTlvStart(tag);

			//"20370913024805Z"
			// TODO: Verify actual format used
			var str = this.DecodeTupleContent();
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

			this.CloseTlv(frame);

			DateTime dt = new DateTime(year, month, day, hour, minute, seconds);
			return dt;
		}

		public override DateTime DecodeDateTimeTlv(Asn1Tag tag)
		{
			var frame = this.DecodeTlvStart(tag);

			//"20370913024805Z"
			// TODO: Verify actual format used
			var str = this.DecodeTupleContent();
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

			this.CloseTlv(frame);

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
			var byteCount = this.BytesLeftInTuple;
			if (byteCount > int.MaxValue)
				throw new InvalidDataException(Messages.Asn1DerDecoder_LengthOverflow);
			return (int)byteCount;
		}

		private delegate void ChunkReceiver(ReadOnlySpan<byte> data, bool isFirstInTuple);

		private void DecodeTupleContentInto(ChunkReceiver receiver)
		{
			if (this._frame.IsIndefiniteLength)
			{
				while (this.PeekTag() == this._frame.tag)
				{
					var outer = this.DecodeTlvStart(this._frame.tag);
					this.DecodeTupleContentInto(receiver);
					this.CloseTlv(outer);
				}
			}
			else
			{
				bool first = true;
				do
				{
					var block = this._Consume((int)Math.Min(4096, this.BytesLeftInTuple));
					receiver(block, first);
					first = false;
				} while (this.BytesLeftInTuple > 0);
			}
		}

		/// <summary>
		/// Reads the next tuple as a string of bytes.
		/// </summary>
		/// <returns>A byte array of the tuple bytes</returns>
		public byte[] DecodeNextTupleAsBytes()
		{
			var startPos = this.Position;
			var tag = this.DecodeTag();
			var valueLength = this.DecodeLength();
			var totalLength = this.Position - startPos + valueLength;

			this._reader.Position = startPos;
			var bytes = this._reader.ReadBytes(checked((int)totalLength));
			return bytes;
		}

		/// <summary>
		/// Reads the contents of the current tuple as bytes.
		/// </summary>
		/// <returns>A byte array containing the content</returns>
		/// <exception cref="InvalidDataException"></exception>
		public byte[] DecodeTupleContent()
		{
			if (this._frame.IsIndefiniteLength)
			{
				MemoryStream memstream = new MemoryStream();
				this.DecodeTupleContentInto((b, _) => memstream.Write(b));
				return memstream.ToArray();
			}
			else
			{
				return this._Consume(this.GetLength()).ToArray();
			}
		}
	}
}
