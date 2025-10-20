using System;
using System.Collections.Generic;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	public interface IAsn1String : IAsn1Tag, IAsn1DerEncodableTlv, IAsn1DerEncodableValue
	{
		string Value { get; set; }
	}
	public interface IAsn1String<TSelf> : IAsn1String
	{
		static abstract Asn1Tag StaticTag { get; }
	}

	public struct IA5String : IAsn1String<IA5String>
	{
		public IA5String(string value)
		{
			this._value = value;
		}

		public string _value;
		string IAsn1String.Value { get => this._value; set => this._value = value; }

		public static implicit operator string(IA5String str) => str._value;
		public static implicit operator IA5String(string value) => new IA5String(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.IA5String;
		static Asn1Tag IAsn1String<IA5String>.StaticTag => throw new NotImplementedException();

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeStringTlv(this);
		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeStringValue(this._value);


		void IAsn1DerEncodableValue.EncodeValue(Asn1DerEncoder encoder)
		{
			throw new NotImplementedException();
		}
	}

	public struct UTF8String : IAsn1String, IAsn1String<UTF8String>
	{
		public UTF8String(string value)
		{
			this._value = value;
		}

		public string _value;
		string IAsn1String.Value { get => this._value; set => this._value = value; }

		public static implicit operator string(UTF8String str) => str._value;
		public static implicit operator UTF8String(string value) => new UTF8String(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.UTF8String;
		static Asn1Tag IAsn1String<UTF8String>.StaticTag => Asn1PredefTag.UTF8String;

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeStringTlv(this);
		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeStringValue(this._value);
	}

	public struct NumericString : IAsn1String, IAsn1String<NumericString>
	{
		public NumericString(string value)
		{
			this._value = value;
		}

		public string _value;
		string IAsn1String.Value { get => this._value; set => this._value = value; }

		public static implicit operator string(NumericString str) => str._value;
		public static implicit operator NumericString(string value) => new NumericString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.NumericString;
		static Asn1Tag IAsn1String<NumericString>.StaticTag => Asn1PredefTag.NumericString;

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeStringTlv(this);
		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeStringValue(this._value);
	}

	public struct PrintableString : IAsn1String, IAsn1String<PrintableString>
	{
		public PrintableString(string value)
		{
			this._value = value;
		}

		public string _value;
		string IAsn1String.Value { get => this._value; set => this._value = value; }

		public static implicit operator string(PrintableString str) => str._value;
		public static implicit operator PrintableString(string value) => new PrintableString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.PrintableString;
		static Asn1Tag IAsn1String<PrintableString>.StaticTag => Asn1PredefTag.PrintableString;

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeStringTlv(this);
		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeStringValue(this._value);
	}

	public struct TeletexString : IAsn1String, IAsn1String<TeletexString>
	{
		public TeletexString(string value)
		{
			this._value = value;
		}

		public string _value;
		string IAsn1String.Value { get => this._value; set => this._value = value; }

		public static implicit operator string(TeletexString str) => str._value;
		public static implicit operator TeletexString(string value) => new TeletexString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.TeletexString;
		static Asn1Tag IAsn1String<TeletexString>.StaticTag => Asn1PredefTag.TeletexString;

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeStringTlv(this);
		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeStringValue(this._value);
	}

	public struct VideotexString : IAsn1String, IAsn1String<VideotexString>
	{
		public VideotexString(string value)
		{
			this._value = value;
		}

		public string _value;
		string IAsn1String.Value { get => this._value; set => this._value = value; }

		public static implicit operator string(VideotexString str) => str._value;
		public static implicit operator VideotexString(string value) => new VideotexString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.VideotexString;
		static Asn1Tag IAsn1String<VideotexString>.StaticTag => Asn1PredefTag.VideotexString;

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeStringTlv(this);
		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeStringValue(this._value);
	}

	public struct GraphicString : IAsn1String, IAsn1String<GraphicString>
	{
		public GraphicString(string value)
		{
			this._value = value;
		}

		public string _value;
		string IAsn1String.Value { get => this._value; set => this._value = value; }

		public static implicit operator string(GraphicString str) => str._value;
		public static implicit operator GraphicString(string value) => new GraphicString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.GraphicString;
		static Asn1Tag IAsn1String<GraphicString>.StaticTag => Asn1PredefTag.GraphicString;

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeStringTlv(this);
		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeStringValue(this._value);
	}

	public struct Iso646String : IAsn1String, IAsn1String<Iso646String>
	{
		public Iso646String(string value)
		{
			this._value = value;
		}

		public string _value;
		string IAsn1String.Value { get => this._value; set => this._value = value; }

		public static implicit operator string(Iso646String str) => str._value;
		public static implicit operator Iso646String(string value) => new Iso646String(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.Iso646String;
		static Asn1Tag IAsn1String<Iso646String>.StaticTag => Asn1PredefTag.Iso646String;

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeStringTlv(this);
		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeStringValue(this._value);
	}

	public struct GeneralString : IAsn1String, IAsn1String<GeneralString>
	{
		public GeneralString(string value)
		{
			this.Value = value;
		}

		public string Value { get; set; }
		string IAsn1String.Value { get => this.Value; set => this.Value = value; }

		public static implicit operator string(GeneralString str) => str.Value;
		public static implicit operator GeneralString(string value) => new GeneralString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.GeneralString;
		static Asn1Tag IAsn1String<GeneralString>.StaticTag => Asn1PredefTag.GeneralString;

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeStringTlv(this);
		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeStringValue(this.Value);
	}

	public struct UniversalString : IAsn1String, IAsn1String<UniversalString>
	{
		public UniversalString(string value)
		{
			this._value = value;
		}

		public string _value;
		string IAsn1String.Value { get => this._value; set => this._value = value; }

		public static implicit operator string(UniversalString str) => str._value;
		public static implicit operator UniversalString(string value) => new UniversalString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.UniversalString;
		static Asn1Tag IAsn1String<UniversalString>.StaticTag => Asn1PredefTag.UniversalString;

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeStringTlv(this);
		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeStringValue(this._value);
	}

	public struct BMPString : IAsn1String, IAsn1String<BMPString>
	{
		public BMPString(string value)
		{
			this._value = value;
		}

		public string _value;
		string IAsn1String.Value { get => this._value; set => this._value = value; }

		public static implicit operator string(BMPString str) => str._value;
		public static implicit operator BMPString(string value) => new BMPString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.BMPString;
		static Asn1Tag IAsn1String<BMPString>.StaticTag => Asn1PredefTag.BMPString;

		public void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeStringTlv(this);
		public void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeStringValue(this._value);
	}

}
