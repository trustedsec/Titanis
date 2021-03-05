using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Serialization
{
	public interface IAsn1DerEncodable
	{
		void DecodeValue(Asn1DerDecoder decoder);
		void EncodeValue(Asn1DerEncoder encoder);
	}
}
