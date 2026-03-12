using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;

namespace Titanis.Asn1.Serialization
{
	public abstract class Asn1Encoder
	{
		public Asn1Encoding Encoding { get; }

		protected Asn1Encoder(Asn1Encoding encoding)
		{
			if (encoding is null)
				throw new ArgumentNullException(nameof(encoding));

			this.Encoding = encoding;
		}

		public abstract Memory<byte> GetBytes();

		public abstract void EncodeCloseTlvHeader(Asn1Tag tag, int endPos);

		public void EncodeOctetStringTlv(ReadOnlySpan<byte> octets) => this.EncodeOctetStringTlv(octets, Asn1PredefTag.OctetString);
		public abstract void EncodeOctetStringTlv(ReadOnlySpan<byte> octets, Asn1Tag tag);


		// As object
		public void EncodeBitStringTlv(Asn1BitString bitstring) => this.EncodeBitStringTlv(bitstring.Octets, bitstring.UnusedBits, Asn1PredefTag.BitString);
		public void EncodeBitStringTlv(Asn1BitString bitstring, Asn1Tag tag) => this.EncodeBitStringTlv(bitstring.Octets, bitstring.UnusedBits, tag);
		public void EncodeBitStringValue(Asn1BitString bitstring) => this.EncodeBitStringValue(bitstring.Octets, bitstring.UnusedBits);
		public void EncodeBitStringTlv(ulong bitstringValue, int bitCount) => this.EncodeBitStringTlv(bitstringValue, bitCount, Asn1PredefTag.BitString);
		public abstract void EncodeBitStringTlv(ulong bitstringValue, int bitCount, Asn1Tag tag);
		public abstract void EncodeBitStringValue(ulong bitstringValue, int bitCount);
		public void EncodeBitStringTlv(ReadOnlySpan<byte> bitstring, byte unusedBits) => this.EncodeBitStringTlv(bitstring, unusedBits, Asn1PredefTag.BitString);
		public abstract void EncodeBitStringTlv(ReadOnlySpan<byte> bitstring, byte unusedBits, Asn1Tag tag);
		public abstract void EncodeBitStringValue(ReadOnlySpan<byte> bitstring, byte unusedBits);




		public void EncodeNullTlv(Asn1Null nul) => this.EncodeNullTlv(nul, Asn1PredefTag.Null);
		public abstract void EncodeNullTlv(Asn1Null nul, Asn1Tag tag);
		public void EncodeBoolTlv(bool v) => this.EncodeBoolTlv(v, Asn1PredefTag.Boolean);
		public abstract void EncodeBoolTlv(bool v, Asn1Tag tag);
		#region Integers
		public void EncodeInt32Tlv(int n) => this.EncodeInt32Tlv(n, Asn1PredefTag.Integer);
		public abstract void EncodeInt32Tlv(int n, Asn1Tag tag);
		public void EncodeUInt32Tlv(uint n) => this.EncodeUInt32Tlv(n, Asn1PredefTag.Integer);
		public abstract void EncodeUInt32Tlv(uint n, Asn1Tag tag);
		public void EncodeSByteTlv(sbyte n) => this.EncodeSByteTlv(n, Asn1PredefTag.Integer);
		public abstract void EncodeSByteTlv(sbyte n, Asn1Tag tag);
		public void EncodeByteTlv(byte n) => this.EncodeByteTlv(n, Asn1PredefTag.Integer);
		public abstract void EncodeByteTlv(byte n, Asn1Tag tag);
		public void EncodeInt16Tlv(short n) => this.EncodeInt16Tlv(n, Asn1PredefTag.Integer);
		public abstract void EncodeInt16Tlv(short n, Asn1Tag tag);
		public void EncodeUInt16Tlv(ushort n) => this.EncodeUInt16Tlv(n, Asn1PredefTag.Integer);
		public abstract void EncodeUInt16Tlv(ushort n, Asn1Tag tag);
		public void EncodeInt64Tlv(long n) => this.EncodeInt64Tlv(n, Asn1PredefTag.Integer);
		public abstract void EncodeInt64Tlv(long n, Asn1Tag tag);
		public abstract void EncodeInt64Value(long n);
		public void EncodeUInt64Tlv(ulong m) => this.EncodeUInt64Tlv(m, Asn1PredefTag.Integer);
		public abstract void EncodeUInt64Tlv(ulong m, Asn1Tag tag);
		public abstract void EncodeUInt64Value(ulong n);
		#endregion
		#region Floating-point
		public void EncodeSingleTlv(float n) => this.EncodeSingleTlv(n, Asn1PredefTag.Real);
		public abstract void EncodeSingleTlv(float n, Asn1Tag tag);
		public void EncodeDoubleTlv(double n) => this.EncodeDoubleTlv(n, Asn1PredefTag.Real);
		public abstract void EncodeDoubleTlv(double n, Asn1Tag tag);
		public void EncodeDecimalTlv(decimal n) => this.EncodeDecimalTlv(n, Asn1PredefTag.Real);
		public abstract void EncodeDecimalTlv(decimal n, Asn1Tag tag);
		#endregion

		public void EncodeEnumeratedTlv(long value) => this.EncodeEnumeratedTlv(value, Asn1PredefTag.Enumerated);
		public abstract void EncodeEnumeratedTlv(long value, Asn1Tag tag);
		public abstract void EncodeEnumeratedValue(long value);

		public void EncodeOidTlv(Asn1Oid oid) => this.EncodeOidTlv(oid, Asn1PredefTag.ObjectIdentifier);
		public abstract void EncodeOidTlv(Asn1Oid oid, Asn1Tag tag);
		public void EncodeRelativeOidTlv(Asn1Oid oid) => this.EncodeRelativeOidTlv(oid, Asn1PredefTag.RelativeOid);
		public abstract void EncodeRelativeOidTlv(Asn1Oid oid, Asn1Tag tag);
		public void EncodeUtcTimeTlv(DateTime dt) => this.EncodeUtcTimeTlv(dt, Asn1PredefTag.UtcTime);
		public abstract void EncodeUtcTimeTlv(DateTime dt, Asn1Tag tag);
		public void EncodeDateTimeTlv<TDate>(TDate dt) where TDate : IAsn1DateTime => this.EncodeDateTimeTlv(dt.Value, dt.Tag);
		public abstract void EncodeDateTimeTlv(DateTime dt, Asn1Tag tag);
		public void EncodeBigIntegerTlv(BigInteger n) => this.EncodeBigIntegerTlv(n, Asn1PredefTag.Integer);
		public abstract void EncodeBigIntegerTlv(BigInteger n, Asn1Tag tag);
		public abstract void EncodeBigIntegerValue(BigInteger n);
		protected abstract void EncodeStringBytes(string str);
		public void EncodeStringValue(string str) => this.EncodeStringBytes(str);

		public void EncodeUtf8StringTlv(string str) => this.EncodeUtf8StringTlv(str, Asn1PredefTag.UTF8String);
		public abstract void EncodeUtf8StringTlv(string str, Asn1Tag tag);
		public void EncodeStringTlv<T>(T str) where T : IAsn1String => this.EncodeStringTlv(str, str.Tag);
		public abstract void EncodeStringTlv<T>(T str, Asn1Tag tag) where T : IAsn1String;
	}
}
