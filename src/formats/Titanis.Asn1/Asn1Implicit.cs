using System;
using System.Collections.Generic;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	public class Asn1Implicit<T> : IAsn1DerEncodableTlv
		where T : IAsn1DerEncodable, new()
	{
		public Asn1Tag Tag { get; set; }
		public T Value { get; set; }

		public Asn1Implicit() { }
		public Asn1Implicit(Asn1Tag tag)
		{
			this.Tag = tag;
		}

		public void DecodeTlv(Asn1DerDecoder decoder)
		{
			var end = decoder.DecodeTlvStart(this.Tag);
			if (this.Value == null)
				this.Value = new T();

			this.Value.DecodeValue(decoder);
			decoder.CloseTlv(end);
		}

		public void DecodeValue(Asn1DerDecoder decoder)
		{
			this.Value.DecodeValue(decoder);
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			this.Value.EncodeValue(encoder);
		}

		public bool TryDecodeTlv(Asn1DerDecoder decoder)
		{
			if (decoder.CheckTag(this.Tag))
			{
				var end = decoder.DecodeTlvStart(this.Tag);
				if (this.Value == null)
					this.Value = new T();

				this.Value.DecodeValue(decoder);
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
