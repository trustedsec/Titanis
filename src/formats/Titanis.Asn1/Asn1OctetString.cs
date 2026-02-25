using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	/// <summary>
	/// Represents an <c>OCTET STRING</c>.
	/// </summary>
	public struct Asn1OctetString : IEquatable<Asn1OctetString>, IAsn1DerEncodableValue, IAsn1DerEncodableTlv, IAsn1DerDecodableValue<Asn1OctetString>, IAsn1DerDecodableTlv<Asn1OctetString>
	{
		public Asn1OctetString(byte[] octets)
		{
			ArgumentNullException.ThrowIfNull(octets);

			this.Octets = octets;
		}

		public byte[] Octets { get; private set; }

		public Asn1Tag Tag => Asn1PredefTag.OctetString;

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
			return obj is Asn1OctetString @string && this.Equals(@string);
		}

		public bool Equals(Asn1OctetString other)
		{
			return ArrayExtensions.ElementsEqual(this.Octets, other.Octets);
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(ArrayExtensions.GetElementsHashCode(this.Octets));
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeOctetStringValue(this.Octets);
		}

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeOctetStringTlv(this.Octets, this.Tag);
		}

		public static bool operator ==(Asn1OctetString left, Asn1OctetString right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Asn1OctetString left, Asn1OctetString right)
		{
			return !(left == right);
		}

		static Asn1OctetString IAsn1DerDecodableValue<Asn1OctetString>.DecodeValueFrom(Asn1DerDecoder decoder)
		{
			return new Asn1OctetString(decoder.DecodeOctetStringValue());
		}

		static Asn1OctetString IAsn1DerDecodableTlv<Asn1OctetString>.DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			return decoder.DecodeTaggedValue<Asn1OctetString>(Asn1PredefTag.OctetString);
		}

		static bool IAsn1DerDecodableTlv<Asn1OctetString>.TryDecodeTlvFrom(Asn1DerDecoder decoder, out Asn1OctetString value)
		{
			return decoder.TryDecodeTaggedValue<Asn1OctetString>(Asn1PredefTag.OctetString, out value);
		}
	}
}
