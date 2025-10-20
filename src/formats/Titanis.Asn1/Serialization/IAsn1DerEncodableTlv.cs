using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Titanis.Asn1.Serialization
{
	/// <summary>
	/// Exposes functionality to decode a TLV using ASN.1 DER.
	/// </summary>
	public interface IAsn1DerEncodableTlv : IAsn1Tag
	{
		void EncodeTlv(Asn1DerEncoder encoder);
	}
	public interface IAsn1DerDecodableTlv<TSelf>
	{
		static abstract TSelf DecodeTlvFrom(Asn1DerDecoder decoder);
		static abstract bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out TSelf? value);
	}
}
