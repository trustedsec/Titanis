using System;
using System.Collections.Generic;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	public class Asn1SequenceOf<T> : IAsn1DerEncodableTlv
		where T : IAsn1DerEncodableTlv, new()
	{
		public Asn1Tag Tag => new Asn1Tag(Asn1PredefTag.Sequence, Asn1TagFlags.Constructed);
		public List<T> Values { get; set; }

		public Asn1SequenceOf() { }

		public void DecodeTlv(Asn1DerDecoder decoder)
		{
			var end = decoder.DecodeTlvStart(this.Tag);

			this.DecodeValue(decoder);
			decoder.CloseTlv(end);
		}

		public void DecodeValue(Asn1DerDecoder decoder)
		{
			List<T> values = this.Values = new List<T>();
			while (!decoder.IsEndOfTuple)
			{
				T elem = new T();
				elem.DecodeTlv(decoder);
				values.Add(elem);
			}
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.Values != null)
			{
				for (int i = this.Values.Count - 1; i >= 0; i--)
				{
					var elem = this.Values[i];
					encoder.EncodeObjTlv(elem);
				}
			}
		}

		public bool TryDecodeTlv(Asn1DerDecoder decoder)
		{
			if (decoder.CheckTag(this.Tag))
			{
				var end = decoder.DecodeTlvStart(this.Tag);
				this.DecodeValue(decoder);
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
