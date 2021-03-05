using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Serialization
{
	public interface IAsn1DerEncodableTlv : IAsn1DerEncodable
	{
		Asn1Tag Tag { get; }

		void DecodeTlv(Asn1DerDecoder decoder);
		bool TryDecodeTlv(Asn1DerDecoder decoder);
	}
}
