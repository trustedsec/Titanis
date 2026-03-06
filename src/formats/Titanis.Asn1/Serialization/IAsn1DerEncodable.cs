using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Serialization
{
	/// <summary>
	/// Exposes functionality to encode a value using ASN.1 DER.
	/// </summary>
	public interface IAsn1DerEncodableValue
	{
		/// <summary>
		/// Encodes the value.
		/// </summary>
		/// <param name="encoder">Target encoder</param>
		void EncodeValue(Asn1DerEncoder encoder);
	}
	/// <summary>
	/// Exposes functionality to decode a value using ASN.1 DER.
	/// </summary>
	/// <typeparam name="TSelf"></typeparam>
	public interface IAsn1DerDecodableValue<TSelf>
	{
		/// <summary>
		/// Decodes a value of this type.
		/// </summary>
		/// <param name="decoder">Source decoder</param>
		/// <returns>The decoded <typeparamref name="TSelf"/>.</returns>
		static abstract TSelf DecodeValueFrom(Asn1DerDecoder decoder);
	}
}
