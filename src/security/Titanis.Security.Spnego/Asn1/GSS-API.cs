namespace GSS_API
{
	using System;
	using System.CodeDom.Compiler;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.Collections.Generic;
	using Titanis.Asn1;
	using Titanis.Asn1.Metadata;
	using Titanis.Asn1.Serialization;

	partial class GSS_APIModule
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private GSS_APIModule()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private static GSS_APIModule _instance = new GSS_APIModule();
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static GSS_APIModule Instance => _instance;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Name => "GSS-API";

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Oid => "";
	}

	partial class InitialContextToken : Asn1Implicit<InitialContextToken_Tagged0>, IAsn1DerDecodableTlv<InitialContextToken>, IAsn1DerDecodableValue<InitialContextToken>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public InitialContextToken(InitialContextToken_Tagged0 value) : base(new Asn1Tag(0x60000000), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static InitialContextToken DecodeValueFrom(Asn1DerDecoder decoder) => new InitialContextToken(decoder.DecodeValue<InitialContextToken_Tagged0>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static InitialContextToken DecodeTlvFrom(Asn1DerDecoder decoder) => new InitialContextToken(decoder.DecodeTaggedValue<InitialContextToken_Tagged0>(new Asn1Tag(0x60000000)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out InitialContextToken? value)
		{
			if (decoder.TryDecodeTaggedValue<InitialContextToken_Tagged0>(new Asn1Tag(0x60000000), out var inner))
			{
				value = new InitialContextToken(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Sequence()]
	partial class InitialContextToken_Tagged0 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<InitialContextToken_Tagged0>, IAsn1DerDecodableValue<InitialContextToken_Tagged0>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid thisMech;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Any innerContextToken;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public InitialContextToken_Tagged0(Asn1Oid thisMech, Asn1Any innerContextToken)
		{
			this.thisMech = thisMech;
			this.innerContextToken = innerContextToken;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			this.innerContextToken.EncodeTlv(encoder);
			encoder.EncodeOidTlv(this.thisMech);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static InitialContextToken_Tagged0 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new InitialContextToken_Tagged0(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static InitialContextToken_Tagged0 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = InitialContextToken_Tagged0.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out InitialContextToken_Tagged0? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = InitialContextToken_Tagged0.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private InitialContextToken_Tagged0(Asn1DerDecoder decoder)
		{
			this.thisMech = decoder.DecodeOidTlv();
			this.innerContextToken = decoder.DecodeTlv<Asn1Any>();
		}
	}
}