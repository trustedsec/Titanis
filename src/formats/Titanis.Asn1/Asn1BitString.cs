using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	/// <summary>
	/// Represents a <c>BIT STRING</c>.
	/// </summary>
	public struct Asn1BitString : IEquatable<Asn1BitString>, IAsn1DerEncodableValue, IAsn1DerEncodableTlv
	{
		public Asn1BitString(byte[] octets, byte unusedBits)
		{
			ArgumentNullException.ThrowIfNull(octets);
			if ((uint)unusedBits > (uint)(octets.Length * 8))
				throw new ArgumentOutOfRangeException(nameof(unusedBits));

			this.UnusedBits = unusedBits;
			this.Octets = octets;
		}
		public Asn1BitString(uint value32)
			:this(CreateOctetsFromUInt32(value32), 0)
		{
		}

		private static byte[] CreateOctetsFromUInt32(uint value32)
		{
			byte[] bytes = new byte[4];
			BinaryPrimitives.WriteUInt32BigEndian(bytes, value32);
			return bytes;
		}

		public byte UnusedBits { get; }
		public byte[] Octets { get; }

		public Asn1Tag Tag => Asn1PredefTag.BitString;

		public ulong ToUInt64()
		{
			int cb = this.Octets.Length;
			int i = 0;
			if (cb <= 8)
			{
				ulong n = this.Octets[i++];
				while (i < cb)
				{
					n <<= 8;
					n |= this.Octets[i++];
				}
				return n;
			}
			else
				throw new OverflowException(Messages.Asn1_IntegerOverflow);
		}

		public uint ToUInt32()
		{
			int cb = this.Octets.Length;
			int i = 0;
			if (cb <= 4)
			{
				uint n = this.Octets[i++];
				while (i < cb)
				{
					n <<= 8;
					n |= this.Octets[i++];
				}
				return n;
			}
			else
				throw new OverflowException(Messages.Asn1_IntegerOverflow);
		}

		public override bool Equals(object obj)
		{
			return obj is Asn1BitString @string && this.Equals(@string);
		}

		public bool Equals(Asn1BitString other)
		{
			return this.UnusedBits == other.UnusedBits &&
				   ArrayExtensions.ElementsEqual(this.Octets, other.Octets);
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(this.UnusedBits, ArrayExtensions.GetElementsHashCode(this.Octets));
		}

		public void DecodeValue(Asn1DerDecoder decoder)
		{
			this = decoder.DecodeBitStringValue();
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBitStringValue(this);
		}

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeBitStringTlv(this.Octets, this.UnusedBits, this.Tag);
		}

		public static bool operator ==(Asn1BitString left, Asn1BitString right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Asn1BitString left, Asn1BitString right)
		{
			return !(left == right);
		}
	}

	public struct Asn1BitString<TEnum> : IAsn1DerEncodableValue, IAsn1DerEncodableTlv
		where TEnum : struct, Enum, IConvertible
	{
		public Asn1BitString(TEnum value, int bitCount)
		{
			this.Value = value;
			this.BitCount = bitCount;
		}

		public TEnum Value { get; }
		public int BitCount { get; }

		public Asn1Tag Tag => Asn1PredefTag.BitString;

		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeBitStringValue(this.Value.ToUInt64(null), this.BitCount);

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeEnumeratedTlv(this.Value.ToInt64(null), Asn1PredefTag.BitString);
	}
}
