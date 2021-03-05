using System;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Text;
using Titanis.IO;

namespace Titanis.Asn1.Serialization
{
	/// <summary>
	/// Represents a TLV frame within an ASN.1 BER-derived stream.
	/// </summary>
	/// <remarks>
	/// This structure is returned from <see cref="Asn1DerDecoder.DecodeTlvStart(Asn1Tag)"/>
	/// and passed to <see cref="Asn1DerDecoder.CloseTlv(in Asn1DecoderFrame)"/>.
	/// It is opaque to prevent modification.
	/// </remarks>
	public struct Asn1DecoderFrame
	{
		internal long endIndex;
		internal Asn1Tag tag;

		internal bool IsIndefiniteLength => (this.endIndex < 0);
		internal bool IsConstructed => this.tag.IsConstructed;
	}

	/// <summary>
	/// Represents an ASN.1 decoder.
	/// </summary>
	/// <remarks>
	/// An ASN.1 decoder reads data from a stream and decodes according
	/// to a specific ASN.1 rule set.
	/// The class implements methods for handling TLV tuples as well
	/// as reading the raw data within the tuple.
	/// </remarks>
	public abstract class Asn1Decoder
	{
		/// <summary>
		/// Reads the next byte from the source as a tag, without advancing.
		/// </summary>
		/// <returns>The next tag, if available; otherwise, <c>0</c>.</returns>
		public abstract Asn1Tag PeekTag();
		/// <summary>
		/// Reads the tag and length of the next TLV.
		/// </summary>
		/// <param name="expectedTag">Tag expected</param>
		/// <returns><see cref="Asn1DecoderFrame"/> to pass to <see cref="CloseTlv(in Asn1DecoderFrame)"/></returns>
		/// <exception cref="FormatException">The tag read doesn't match <paramref name="expectedTag"/>.</exception>
		/// <remarks>
		/// When reading a tuple, the decoder maintains the notion of a frame
		/// to know where the current tuple ends.  To support nested tuples,
		/// this method returns a value that the caller must provide to
		/// <see cref="CloseTlv(in Asn1DecoderFrame)"/> to restore this frame for the outer
		/// TLV.
		/// </remarks>
		public abstract Asn1DecoderFrame DecodeTlvStart(Asn1Tag expectedTag);
		/// <summary>
		/// Checks that the next tag matches an expected tag
		/// </summary>
		/// <param name="expectedTag">The tag expected</param>
		/// <returns><c>true</c> if the next tag matches <paramref name="expectedTag"/>; otherwise, <c>false</c>.</returns>
		public bool CheckTag(Asn1Tag expectedTag)
		{
			var actual = this.PeekTag();
			return (actual == expectedTag);
		}
		/// <summary>
		/// Closes the current TLV.
		/// </summary>
		/// <param name="frame">Value returned by <see cref="DecodeTlvStart(Asn1Tag)"/></param>
		/// <remarks>
		/// If there is data remaining in the current TLV, the data
		/// is skipped without error.
		/// </remarks>
		public abstract void CloseTlv(in Asn1DecoderFrame frame);
		/// <summary>
		/// Gets a value indicating whether the end of the current
		/// tuple has been reached.
		/// </summary>
		public abstract bool IsEndOfTuple { get; }

		/// <summary>
		/// Decodes a <c>BOOLEAN</c> value
		/// </summary>
		/// <returns>A <see cref="bool"/> value decoded from the data</returns>
		public abstract bool DecodeBool();
		/// <summary>
		/// Decodes an ANSI <see cref="char"/> from the data.
		/// </summary>
		/// <returns>The <see cref="char"/> value decoded from the data</returns>
		public abstract char DecodeAnsiChar();
		/// <summary>
		/// Decodes a UTF-8 <see cref="char"/> from the data.
		/// </summary>
		/// <returns>The <see cref="char"/> value decoded from the data</returns>
		/// <exception cref="InvalidDataException">The underlying data is not a valid encoding of a UTF-8 <see cref="char"/>.</exception>
		public abstract char DecodeUtf8Char();
		/// <summary>
		/// Decodes a UTF-8 <see cref="Rune"/> from the data.
		/// </summary>
		/// <returns>The <see cref="Rune"/> value decoded from the data</returns>
		/// <exception cref="InvalidDataException">The underlying data is not a valid encoding of a UTF-8 <see cref="Rune"/>.</exception>
		public abstract Rune DecodeUtf8Rune();
		#region Integers
		/// <summary>
		/// Decodes a <see cref="byte"/> from the data.
		/// </summary>
		/// <returns>The <see cref="byte"/> value decoded from the data</returns>
		/// <exception cref="InvalidDataException">The underlying data is not a valid encoding of a <see cref="byte"/>.</exception>
		/// <remarks>
		/// Not to be confused with <see cref="Stream.ReadByte"/>, this method
		/// may read multiple bytes from the underlying source, depending on the
		/// encoding.
		/// </remarks>
		public abstract byte DecodeByte();
		/// <summary>
		/// Decodes an integer as a <see cref="sbyte"/>.
		/// </summary>
		/// <returns>The decoded <see cref="sbyte"/> value.</returns>
		public abstract sbyte DecodeSByte();
		/// <summary>
		/// Decodes an integer as a <see cref="BigInteger"/>.
		/// </summary>
		/// <returns>The decoded <see cref="BigInteger"/> value.</returns>
		public abstract BigInteger DecodeBigInteger();
		/// <summary>
		/// Decodes an integer as a <see cref="short"/>.
		/// </summary>
		/// <returns>The decoded <see cref="short"/> value.</returns>
		public abstract short DecodeInt16();
		/// <summary>
		/// Decodes an integer as a <see cref="ushort"/>.
		/// </summary>
		/// <returns>The decoded <see cref="ushort"/> value.</returns>
		public abstract ushort DecodeUInt16();
		/// <summary>
		/// Decodes an integer as a <see cref="int"/>.
		/// </summary>
		/// <returns>The decoded <see cref="int"/> value.</returns>
		public abstract int DecodeInt32();
		/// <summary>
		/// Decodes an integer as a <see cref="uint"/>.
		/// </summary>
		/// <returns>The decoded <see cref="uint"/> value.</returns>
		public abstract uint DecodeUInt32();
		/// <summary>
		/// Decodes an integer as a <see cref="long"/>.
		/// </summary>
		/// <returns>The decoded <see cref="long"/> value.</returns>
		public abstract long DecodeInt64();
		/// <summary>
		/// Decodes an integer as a <see cref="ulong"/>.
		/// </summary>
		/// <returns>The decoded <see cref="ulong"/> value.</returns>
		public abstract ulong DecodeUInt64();
		#endregion
		#region Floating-point
		/// <summary>
		/// Decodes a REAL value as a <see cref="float"/>.
		/// </summary>
		/// <returns>The decoded <see cref="float"/> value.</returns>
		public abstract float DecodeSingle();
		/// <summary>
		/// Decodes a REAL value as a <see cref="double"/>.
		/// </summary>
		/// <returns>The decoded <see cref="double"/> value.</returns>
		public abstract double DecodeDouble();
		/// <summary>
		/// Decodes a REAL value as a <see cref="decimal"/>.
		/// </summary>
		/// <returns>The decoded <see cref="decimal"/> value.</returns>
		public abstract decimal DecodeDecimal();
		#endregion
		/// <summary>
		/// Decodes a BITSTRING value.
		/// </summary>
		/// <returns>The decoded <see cref="Asn1BitString"/> value.</returns>
		public abstract Asn1BitString DecodeBitString();
		/// <summary>
		/// Decodes an OCTET STRING value
		/// </summary>
		/// <returns>A byte array containing the value of the decoded octet string.</returns>
		public abstract byte[] DecodeOctetString();
		/// <summary>
		/// Decodes a <c>NULL</c> value.
		/// </summary>
		/// <returns><see cref="DBNull.Value"/></returns>
		/// <remarks>
		/// Since <c>NULL</c> values don't contain any data, this method does not
		/// read from the <see cref="IByteSource"/>.
		/// </remarks>
		public abstract DBNull DecodeNull();
		public abstract Asn1Oid DecodeOid();
		public abstract Asn1OidPart[] DecodeRelativeOid();
		// TODO: Decode IRIs
		#region Strings
		public abstract string DecodeUtf8tring();
		#endregion
		#region Date/time
		public abstract DateTime DecodeUtcTime();
		public abstract DateTime DecodeDateTime();
		#endregion

		public T DecodeString<T>() where T : struct, IAsn1String
		{
			return new T { Value = this.DecodeUtf8tring() };
		}
		public T DecodeDateTime<T>() where T : struct, IAsn1DateTime
		{
			return new T { Value = this.DecodeDateTime() };
		}

		//internal static readonly MethodInfo method_IsEndOfTuple = ReflectionHelper.MethodOf<Asn1DerDecoder>(r => r.IsEndOfTuple());
	}
}
