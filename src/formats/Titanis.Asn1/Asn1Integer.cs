using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	public struct Asn1Integer : IAsn1DerEncodableValue, IAsn1DerEncodableTlv, IAsn1DerDecodableValue<Asn1Integer>, IAsn1DerDecodableTlv<Asn1Integer>
	{
		public Asn1Integer(BigInteger value)
		{
			this.Value = value;
		}

		public BigInteger Value { get; }

		public Asn1Tag Tag => Asn1PredefTag.Integer;

		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeBigIntegerValue(this.Value);

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeBigIntegerTlv(this.Value, Asn1PredefTag.Integer);

		static Asn1Integer IAsn1DerDecodableValue<Asn1Integer>.DecodeValueFrom(Asn1DerDecoder decoder)
		{
			return new Asn1Integer(decoder.DecodeBigIntegerValue());
		}

		static Asn1Integer IAsn1DerDecodableTlv<Asn1Integer>.DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			return new Asn1Integer(decoder.DecodeIntegerTlvAsBigInteger());
		}

		static bool IAsn1DerDecodableTlv<Asn1Integer>.TryDecodeTlvFrom(Asn1DerDecoder decoder, out Asn1Integer value)
		{
			return (decoder.TryDecodeTaggedValue<Asn1Integer>(Asn1PredefTag.Integer, out value));
		}
	}
}
