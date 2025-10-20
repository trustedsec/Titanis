using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	public struct Asn1Null : IAsn1DerEncodableValue, IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<Asn1Null>, IAsn1DerDecodableValue<Asn1Null>
	{
		public Asn1Tag Tag => Asn1PredefTag.Null;

		public static Asn1Null DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			decoder.DecodeNullTlv();
			return new Asn1Null();
		}

		public static Asn1Null DecodeValueFrom(Asn1DerDecoder decoder)
		{
			decoder.DecodeNullValue();
			return new Asn1Null();
		}

		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Asn1Null value)
		{
			return decoder.TryDecodeTaggedValue(Asn1PredefTag.Null, out value);
		}

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeNullTlv(this);
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeNullValue();
		}
	}
}
