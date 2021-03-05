using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1
{
	public interface IAsn1String : IAsn1Tag
	{
		string Value { get; set; }
	}

	public struct IA5String : IAsn1String
	{
		public IA5String(string value)
		{
			this.value = value;
		}

		public string value;
		string IAsn1String.Value { get => this.value; set => this.value = value; }

		public static implicit operator string(IA5String str) => str.value;
		public static implicit operator IA5String(string value) => new IA5String(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.IA5String;
	}

	public struct UTF8String : IAsn1String
	{
		public UTF8String(string value)
		{
			this.value = value;
		}

		public string value;
		string IAsn1String.Value { get => this.value; set => this.value = value; }

		public static implicit operator string(UTF8String str) => str.value;
		public static implicit operator UTF8String(string value) => new UTF8String(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.UTF8String;
	}

	public struct NumericString : IAsn1String
	{
		public NumericString(string value)
		{
			this.value = value;
		}

		public string value;
		string IAsn1String.Value { get => this.value; set => this.value = value; }

		public static implicit operator string(NumericString str) => str.value;
		public static implicit operator NumericString(string value) => new NumericString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.NumericString;
	}

	public struct PrintableString : IAsn1String
	{
		public PrintableString(string value)
		{
			this.value = value;
		}

		public string value;
		string IAsn1String.Value { get => this.value; set => this.value = value; }

		public static implicit operator string(PrintableString str) => str.value;
		public static implicit operator PrintableString(string value) => new PrintableString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.PrintableString;
	}

	public struct TeletexString : IAsn1String
	{
		public TeletexString(string value)
		{
			this.value = value;
		}

		public string value;
		string IAsn1String.Value { get => this.value; set => this.value = value; }

		public static implicit operator string(TeletexString str) => str.value;
		public static implicit operator TeletexString(string value) => new TeletexString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.TeletexString;
	}

	public struct VideotexString : IAsn1String
	{
		public VideotexString(string value)
		{
			this.value = value;
		}

		public string value;
		string IAsn1String.Value { get => this.value; set => this.value = value; }

		public static implicit operator string(VideotexString str) => str.value;
		public static implicit operator VideotexString(string value) => new VideotexString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.VideotexString;
	}

	public struct GraphicString : IAsn1String
	{
		public GraphicString(string value)
		{
			this.value = value;
		}

		public string value;
		string IAsn1String.Value { get => this.value; set => this.value = value; }

		public static implicit operator string(GraphicString str) => str.value;
		public static implicit operator GraphicString(string value) => new GraphicString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.GraphicString;
	}

	public struct Iso646String : IAsn1String
	{
		public Iso646String(string value)
		{
			this.value = value;
		}

		public string value;
		string IAsn1String.Value { get => this.value; set => this.value = value; }

		public static implicit operator string(Iso646String str) => str.value;
		public static implicit operator Iso646String(string value) => new Iso646String(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.Iso646String;
	}

	public struct GeneralString : IAsn1String
	{
		public GeneralString(string value)
		{
			this.value = value;
		}

		public string value;
		string IAsn1String.Value { get => this.value; set => this.value = value; }

		public static implicit operator string(GeneralString str) => str.value;
		public static implicit operator GeneralString(string value) => new GeneralString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.GeneralString;
	}

	public struct UniversalString : IAsn1String
	{
		public UniversalString(string value)
		{
			this.value = value;
		}

		public string value;
		string IAsn1String.Value { get => this.value; set => this.value = value; }

		public static implicit operator string(UniversalString str) => str.value;
		public static implicit operator UniversalString(string value) => new UniversalString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.UniversalString;
	}

	public struct BMPString : IAsn1String
	{
		public BMPString(string value)
		{
			this.value = value;
		}

		public string value;
		string IAsn1String.Value { get => this.value; set => this.value = value; }

		public static implicit operator string(BMPString str) => str.value;
		public static implicit operator BMPString(string value) => new BMPString(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.BMPString;
	}

}
