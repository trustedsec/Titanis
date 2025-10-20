using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	public struct Asn1Enumerated<TEnum> : IAsn1DerEncodableValue, IAsn1DerEncodableTlv
		where TEnum : struct, Enum, IConvertible
	{
		public Asn1Enumerated(TEnum value)
		{
			this.Value = value;
		}

		public TEnum Value { get; }

		public Asn1Tag Tag => Asn1PredefTag.Enumerated;

		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeEnumeratedValue(this.Value.ToInt64(null));

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeEnumeratedTlv(this.Value.ToInt64(null), Asn1PredefTag.Enumerated);
	}
}
