using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	public struct Asn1BitString : IEquatable<Asn1BitString>, IAsn1DerEncodable
	{
		public byte unusedBits;
		public byte[] octets;

		public Asn1BitString(byte[] octets, byte unusedBits)
		{
			this.unusedBits = unusedBits;
			this.octets = octets;
		}
		public Asn1BitString(int octets)
			: this((uint)octets)
		{
		}
		public Asn1BitString(uint octets)
		{
			this.unusedBits = 0;
			if (BitConverter.IsLittleEndian)
				octets = BinaryPrimitives.ReverseEndianness(octets);
			this.octets = BitConverter.GetBytes(octets);
		}

		public uint ToUInt32()
		{
			int cb = this.octets.Length;
			int i = 0;
			if (cb <= 4)
			{
				uint n = this.octets[i++];
				while (i < cb)
				{
					n <<= 8;
					n |= this.octets[i++];
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
			return this.unusedBits == other.unusedBits &&
				   ArrayExtensions.ElementsEqual(this.octets, other.octets);
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(this.unusedBits, ArrayExtensions.GetElementsHashCode(this.octets));
		}

		public void DecodeValue(Asn1DerDecoder decoder)
		{
			this = decoder.DecodeBitString();
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBitString(this);
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
}
