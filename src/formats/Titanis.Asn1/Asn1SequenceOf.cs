using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	public abstract class Asn1SequenceOf : IAsn1DerEncodableTlv, IAsn1DerEncodableValue
	{
		public static Asn1SequenceOf<T> Create<T>(T[] values)
			where T : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<T>
			=> new Asn1SequenceOf<T>(values);

		public abstract Asn1Tag Tag { get; }
		public abstract void EncodeTlv(Asn1DerEncoder encoder);
		public abstract void EncodeValue(Asn1DerEncoder encoder);
	}
	public class Asn1SequenceOf<T> : Asn1SequenceOf, IEnumerable<T>, IAsn1DerDecodableTlv<Asn1SequenceOf<T>>, IAsn1DerDecodableValue<Asn1SequenceOf<T>>
		where T : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<T>
	{
		public Asn1SequenceOf(T[] values)
		{
			ArgumentNullException.ThrowIfNull(values);
			this.Values = values;
		}

		public override Asn1Tag Tag => new Asn1Tag(Asn1PredefTag.Sequence, Asn1TagFlags.Constructed);
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

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)this.Values).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Values.GetEnumerator();
		}

		static Asn1SequenceOf<T> IAsn1DerDecodableTlv<Asn1SequenceOf<T>>.DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			return new Asn1SequenceOf<T>(decoder.DecodeListTlv<T>(Asn1PredefTag.Sequence));
		}

		static bool IAsn1DerDecodableTlv<Asn1SequenceOf<T>>.TryDecodeTlvFrom(Asn1DerDecoder decoder, out Asn1SequenceOf<T>? value)
		{
			if (decoder.CheckTag(new Asn1Tag(Asn1PredefTag.Sequence, Asn1TagFlags.Constructed)))
			{
				value = new Asn1SequenceOf<T>(decoder.DecodeListTlv<T>(new Asn1Tag(Asn1PredefTag.Sequence, Asn1TagFlags.Constructed)));
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}

		static Asn1SequenceOf<T> IAsn1DerDecodableValue<Asn1SequenceOf<T>>.DecodeValueFrom(Asn1DerDecoder decoder)
		{
			return new Asn1SequenceOf<T>(decoder.DecodeValueList<T>());
		}
	}
}
