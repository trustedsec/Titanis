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

		public abstract void EncodeOctetString(ReadOnlySpan<byte> octets);
		public abstract void EncodeBitString(Asn1BitString bitstring);
		public abstract void EncodeNull(DBNull dbnull);
		public abstract void EncodeBool(bool v);
		public abstract void EncodeInt32(int n);
		public abstract void EncodeUInt32(uint n);
		public abstract void EncodeUtf8har(char c);
		public abstract void EncodeSByte(sbyte n);
		public abstract void EncodeByte(byte n);
		public abstract void EncodeInt16(short n);
		public abstract void EncodeUInt16(ushort n);
		public abstract void EncodeInt64(long n);
		public abstract void EncodeUInt64(ulong m);
		public abstract void EncodeSingle(float n);
		public abstract void EncodeDouble(double n);
		public abstract void EncodeDecimal(decimal n);
		public abstract void EncodeOid(Asn1Oid oid);
		public void WriteOid(Oid oid)
		{
			if (oid == null)
				throw new ArgumentNullException(nameof(oid));

			this.EncodeOid(new Asn1Oid(oid));
		}
		public abstract void EncodeRelativeOid(Asn1Oid oid);
		public abstract void EncodeUtcTime(DateTime dt);
		public abstract void EncodeDateTime(DateTime dt);
		public abstract void EncodeString(string str);
		public abstract void EncodeBigInteger(BigInteger n);
	}
}
