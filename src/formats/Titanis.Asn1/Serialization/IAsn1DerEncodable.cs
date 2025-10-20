using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Serialization
{
	/// <summary>
	/// Exposes functionality to encode or decode a value using ASN.1 DER.
	/// </summary>
	public interface IAsn1DerEncodableValue
	{
		void EncodeValue(Asn1DerEncoder encoder);
	}

	public interface IAsn1DerDecodableValue<TSelf>
	{
		static abstract TSelf DecodeValueFrom(Asn1DerDecoder decoder);
	}
}
