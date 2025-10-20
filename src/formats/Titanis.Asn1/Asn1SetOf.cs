using System;
using System.Collections.Generic;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	public abstract class Asn1SetOf : IAsn1DerEncodableTlv, IAsn1DerEncodableValue
	{
		public static Asn1SetOf<T> Create<T>(T[] values)
		where T : IAsn1DerEncodableTlv
			=> new Asn1SetOf<T>(values);

		public abstract Asn1Tag Tag { get; }

		public abstract void EncodeTlv(Asn1DerEncoder encoder);
		public abstract void EncodeValue(Asn1DerEncoder encoder);
	}

	public class Asn1SetOf<T> : Asn1SetOf
		where T : IAsn1DerEncodableTlv
	{
		public Asn1SetOf(T[] values)
		{
			ArgumentNullException.ThrowIfNull(values);
			this.Values = values;
		}

		public override Asn1Tag Tag => new Asn1Tag(Asn1PredefTag.Set, Asn1TagFlags.Constructed);
		public T[] Values { get; }

		public override void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.Values != null)
			{
				for (int i = this.Values.Length - 1; i >= 0; i--)
				{
					var elem = this.Values[i];
					elem.EncodeTlv(encoder);
				}
			}
		}

		public override void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}
	}
}
