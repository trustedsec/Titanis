using System;
using System.Collections.Generic;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	public class Asn1Implicit<T> : IAsn1DerEncodableTlv, IAsn1DerEncodableValue
		where T : IAsn1DerEncodableValue, IAsn1DerDecodableValue<T>
	{
		public Asn1Implicit(Asn1Tag tag, T value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		public Asn1Tag Tag { get; }
		public T Value { get; }

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			this.Value.EncodeValue(encoder);
		}

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}
	}
}
