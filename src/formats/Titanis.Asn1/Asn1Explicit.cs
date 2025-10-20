using System;
using System.Collections.Generic;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	/// <summary>
	/// Wraps an ASN.1 TLV in a TLV with an explicit tag.
	/// </summary>
	/// <typeparam name="T">Type of inner TLV</typeparam>
	public class Asn1Explicit<T> : IAsn1DerEncodableTlv, IAsn1DerEncodableValue
		where T : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<T>
	{
		/// <summary>
		/// Initializes a new <see cref="Asn1Explicit{T}"/>.
		/// </summary>
		/// <param name="tag">Tag</param>
		public Asn1Explicit(Asn1Tag tag, T value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		/// <summary>
		/// Gets the tag of the outer TLV.
		/// </summary>
		public Asn1Tag Tag { get; }
		/// <summary>
		/// Gets the value.
		/// </summary>
		public T Value { get; }

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			var pos = encoder.Position;
			this.Value.EncodeTlv(encoder);
			encoder.EncodeCloseTlvHeader(this.Tag, pos);
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			this.Value.EncodeTlv(encoder);
		}
	}
}
