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
	public class Asn1Explicit<T> : IAsn1DerEncodableTlv
		where T : IAsn1DerEncodableTlv, new()
	{
		public Asn1Tag Tag { get; set; }
		public T Value { get; set; }

		public Asn1Explicit() { }
		public Asn1Explicit(Asn1Tag tag)
		{
			this.Tag = tag;
		}

		public void DecodeTlv(Asn1DerDecoder decoder)
		{
			var end = decoder.DecodeTlvStart(this.Tag);
			if (this.Value == null)
				this.Value = new T();

			this.Value.DecodeTlv(decoder);
			decoder.CloseTlv(end);
		}

		public void DecodeValue(Asn1DerDecoder decoder)
		{
			this.Value.DecodeTlv(decoder);
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeObjTlv(this.Value);
		}

		public bool TryDecodeTlv(Asn1DerDecoder decoder)
		{
			if (decoder.CheckTag(this.Tag))
			{
				var end = decoder.DecodeTlvStart(this.Tag);
				if (this.Value == null)
					this.Value = new T();

				this.Value.DecodeTlv(decoder);
				decoder.CloseTlv(end);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
