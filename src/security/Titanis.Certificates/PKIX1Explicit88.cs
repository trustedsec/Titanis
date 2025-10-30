namespace PKIX1Explicit88
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

	partial class PKIX1Explicit88Module
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pkix => new Asn1Oid("1.3.6.1.5.5.7");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pe => new Asn1Oid("1.3.6.1.5.5.7.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_qt => new Asn1Oid("1.3.6.1.5.5.7.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_kp => new Asn1Oid("1.3.6.1.5.5.7.3");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ad => new Asn1Oid("1.3.6.1.5.5.7.48");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_qt_cps => new Asn1Oid("1.3.6.1.5.5.7.2.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_qt_unotice => new Asn1Oid("1.3.6.1.5.5.7.2.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ad_ocsp => new Asn1Oid("1.3.6.1.5.5.7.48.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ad_caIssuers => new Asn1Oid("1.3.6.1.5.5.7.48.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at => new Asn1Oid("2.5.4");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_name => new Asn1Oid("2.5.4.41");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_surname => new Asn1Oid("2.5.4.4");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_givenName => new Asn1Oid("2.5.4.42");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_initials => new Asn1Oid("2.5.4.43");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_generationQualifier => new Asn1Oid("2.5.4.44");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_commonName => new Asn1Oid("2.5.4.3");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_localityName => new Asn1Oid("2.5.4.7");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_stateOrProvinceName => new Asn1Oid("2.5.4.8");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_organizationName => new Asn1Oid("2.5.4.10");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_organizationalUnitName => new Asn1Oid("2.5.4.11");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_title => new Asn1Oid("2.5.4.12");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_dnQualifier => new Asn1Oid("2.5.4.46");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_countryName => new Asn1Oid("2.5.4.6");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid pkcs_9 => new Asn1Oid("1.2.840.113549.1.9");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid emailAddress => new Asn1Oid("1.2.840.113549.1.9.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid pkcs_1 => new Asn1Oid("1.2.840.113549.1.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid rsaEncryption => new Asn1Oid("1.2.840.113549.1.1.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid md2WithRSAEncryption => new Asn1Oid("1.2.840.113549.1.1.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid md5WithRSAEncryption => new Asn1Oid("1.2.840.113549.1.1.4");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid sha1WithRSAEncryption => new Asn1Oid("1.2.840.113549.1.1.5");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_dsa_with_sha1 => new Asn1Oid("1.2.840.10040.4.3");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid dhpublicnumber => new Asn1Oid("1.2.840.10046.2.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_dsa => new Asn1Oid("1.2.840.10040.4.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte common_name => 1;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte teletex_common_name => 2;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte teletex_organization_name => 3;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte teletex_personal_name => 4;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte teletex_organizational_unit_names => 5;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte pds_name => 7;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte physical_delivery_country_name => 8;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte postal_code => 9;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte physical_delivery_office_name => 10;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte physical_delivery_office_number => 11;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte extension_OR_address_components => 12;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte physical_delivery_personal_name => 13;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte physical_delivery_organization_name => 14;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte extension_physical_delivery_address_components => 15;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte unformatted_postal_address => 16;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte street_address => 17;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte post_office_box_address => 18;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte poste_restante_address => 19;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte unique_postal_name => 20;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte local_postal_attributes => 21;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte extended_network_address => 22;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte terminal_type => 23;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte teletex_domain_defined_attributes => 6;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ushort ub_name => 32768;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_common_name => 64;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_locality_name => 128;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_state_name => 128;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_organization_name => 64;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_organizational_unit_name => 64;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_title => 64;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_match => 128;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_emailaddress_length => 128;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_common_name_length => 64;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_country_name_alpha_length => 2;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_country_name_numeric_length => 3;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_domain_defined_attributes => 4;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_domain_defined_attribute_type_length => 8;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_domain_defined_attribute_value_length => 128;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_domain_name_length => 16;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ushort ub_extension_attributes => 256;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_e163_4_number_length => 15;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_e163_4_sub_address_length => 40;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_generation_qualifier_length => 3;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_given_name_length => 16;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_initials_length => 5;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ushort ub_integer_options => 256;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_numeric_user_id_length => 32;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_organization_name_length => 64;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_organizational_unit_name_length => 32;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_organizational_units => 4;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_pds_name_length => 16;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_pds_parameter_length => 30;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_pds_physical_address_lines => 6;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_postal_code_length => 16;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_surname_length => 40;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_terminal_id_length => 24;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_unformatted_address_length => 180;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ub_x121_address_length => 16;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte v1 => 0;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte v2 => 1;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte v3 => 2;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte telex => 3;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte teletex => 4;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte g3_facsimile => 5;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte g4_facsimile => 6;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ia5_terminal => 7;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte videotex => 8;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PKIX1Explicit88Module()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private static PKIX1Explicit88Module _instance = new PKIX1Explicit88Module();
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PKIX1Explicit88Module Instance => _instance;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Name => "PKIX1Explicit88";

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Oid => "1.3.6.1.5.5.7.0.1";
	}

	[Asn1Sequence()]
	partial class Attribute : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Attribute>, IAsn1DerDecodableValue<Attribute>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Any[] values;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Attribute(Asn1Oid type, Asn1Any[] values)
		{
			this.type = type;
			this.values = values;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeListTlv(new Asn1Tag(0x20000011), this.values, (encoder, r) =>
			{
				r.EncodeTlv(encoder);
			});
			encoder.EncodeOidTlv(this.type);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Attribute DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Attribute(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Attribute DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Attribute.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Attribute? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Attribute.DecodeValueFrom(decoder);
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
		private Attribute(Asn1DerDecoder decoder)
		{
			this.type = decoder.DecodeOidTlv();
			this.values = decoder.DecodeListTlv<Asn1Any>(new Asn1Tag(0x20000011), (encoder) => decoder.DecodeTlv<Asn1Any>());
		}
	}

	[Asn1Choice()]
	partial class X520name : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<X520name>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TeletexString TeletexString
		{
			get => this.teletexString.Value;
			set
			{
				this.teletexString = value;
				this._choiceTag = ChoiceIndex.TeletexString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private TeletexString? teletexString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrintableString PrintableString
		{
			get => this.printableString.Value;
			set
			{
				this.printableString = value;
				this._choiceTag = ChoiceIndex.PrintableString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PrintableString? printableString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public UniversalString UniversalString
		{
			get => this.universalString.Value;
			set
			{
				this.universalString = value;
				this._choiceTag = ChoiceIndex.UniversalString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private UniversalString? universalString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public String Utf8String
		{
			get => this.utf8String;
			set
			{
				this.utf8String = value;
				this._choiceTag = ChoiceIndex.Utf8String;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private String? utf8String;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BMPString BmpString
		{
			get => this.bmpString.Value;
			set
			{
				this.bmpString = value;
				this._choiceTag = ChoiceIndex.BmpString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private BMPString? bmpString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public X520name()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.BmpString:
					Debug.Assert(this.bmpString is not null);
					encoder.EncodeStringTlv(this.bmpString.Value);
					break;
				case ChoiceIndex.Utf8String:
					Debug.Assert(this.utf8String is not null);
					encoder.EncodeUtf8StringTlv(this.utf8String);
					break;
				case ChoiceIndex.UniversalString:
					Debug.Assert(this.universalString is not null);
					encoder.EncodeStringTlv(this.universalString.Value);
					break;
				case ChoiceIndex.PrintableString:
					Debug.Assert(this.printableString is not null);
					encoder.EncodeStringTlv(this.printableString.Value);
					break;
				case ChoiceIndex.TeletexString:
					Debug.Assert(this.teletexString is not null);
					encoder.EncodeStringTlv(this.teletexString.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type X520name has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static X520name DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!X520name.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out X520name? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x14)))
				instance = new X520name()
				{
					_choiceTag = ChoiceIndex.TeletexString,
					teletexString = decoder.DecodeStringTlv<TeletexString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x13)))
				instance = new X520name()
				{
					_choiceTag = ChoiceIndex.PrintableString,
					printableString = decoder.DecodeStringTlv<PrintableString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1C)))
				instance = new X520name()
				{
					_choiceTag = ChoiceIndex.UniversalString,
					universalString = decoder.DecodeStringTlv<UniversalString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0xC)))
				instance = new X520name()
				{
					_choiceTag = ChoiceIndex.Utf8String,
					utf8String = decoder.DecodeUtf8StringTlv()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1E)))
				instance = new X520name()
				{
					_choiceTag = ChoiceIndex.BmpString,
					bmpString = decoder.DecodeStringTlv<BMPString>()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			TeletexString = 20U,
			PrintableString = 19U,
			UniversalString = 28U,
			Utf8String = 12U,
			BmpString = 30U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class AttributeTypeAndValue : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AttributeTypeAndValue>, IAsn1DerDecodableValue<AttributeTypeAndValue>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Any value;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttributeTypeAndValue(Asn1Oid type, Asn1Any value)
		{
			this.type = type;
			this.value = value;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			this.value.EncodeTlv(encoder);
			encoder.EncodeOidTlv(this.type);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeTypeAndValue DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AttributeTypeAndValue(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeTypeAndValue DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AttributeTypeAndValue.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AttributeTypeAndValue? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AttributeTypeAndValue.DecodeValueFrom(decoder);
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
		private AttributeTypeAndValue(Asn1DerDecoder decoder)
		{
			this.type = decoder.DecodeOidTlv();
			this.value = decoder.DecodeTlv<Asn1Any>();
		}
	}

	[Asn1Choice()]
	partial class X520CommonName : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<X520CommonName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TeletexString TeletexString
		{
			get => this.teletexString.Value;
			set
			{
				this.teletexString = value;
				this._choiceTag = ChoiceIndex.TeletexString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private TeletexString? teletexString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrintableString PrintableString
		{
			get => this.printableString.Value;
			set
			{
				this.printableString = value;
				this._choiceTag = ChoiceIndex.PrintableString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PrintableString? printableString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public UniversalString UniversalString
		{
			get => this.universalString.Value;
			set
			{
				this.universalString = value;
				this._choiceTag = ChoiceIndex.UniversalString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private UniversalString? universalString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public String Utf8String
		{
			get => this.utf8String;
			set
			{
				this.utf8String = value;
				this._choiceTag = ChoiceIndex.Utf8String;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private String? utf8String;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BMPString BmpString
		{
			get => this.bmpString.Value;
			set
			{
				this.bmpString = value;
				this._choiceTag = ChoiceIndex.BmpString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private BMPString? bmpString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public X520CommonName()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.BmpString:
					Debug.Assert(this.bmpString is not null);
					encoder.EncodeStringTlv(this.bmpString.Value);
					break;
				case ChoiceIndex.Utf8String:
					Debug.Assert(this.utf8String is not null);
					encoder.EncodeUtf8StringTlv(this.utf8String);
					break;
				case ChoiceIndex.UniversalString:
					Debug.Assert(this.universalString is not null);
					encoder.EncodeStringTlv(this.universalString.Value);
					break;
				case ChoiceIndex.PrintableString:
					Debug.Assert(this.printableString is not null);
					encoder.EncodeStringTlv(this.printableString.Value);
					break;
				case ChoiceIndex.TeletexString:
					Debug.Assert(this.teletexString is not null);
					encoder.EncodeStringTlv(this.teletexString.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type X520CommonName has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static X520CommonName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!X520CommonName.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out X520CommonName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x14)))
				instance = new X520CommonName()
				{
					_choiceTag = ChoiceIndex.TeletexString,
					teletexString = decoder.DecodeStringTlv<TeletexString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x13)))
				instance = new X520CommonName()
				{
					_choiceTag = ChoiceIndex.PrintableString,
					printableString = decoder.DecodeStringTlv<PrintableString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1C)))
				instance = new X520CommonName()
				{
					_choiceTag = ChoiceIndex.UniversalString,
					universalString = decoder.DecodeStringTlv<UniversalString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0xC)))
				instance = new X520CommonName()
				{
					_choiceTag = ChoiceIndex.Utf8String,
					utf8String = decoder.DecodeUtf8StringTlv()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1E)))
				instance = new X520CommonName()
				{
					_choiceTag = ChoiceIndex.BmpString,
					bmpString = decoder.DecodeStringTlv<BMPString>()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			TeletexString = 20U,
			PrintableString = 19U,
			UniversalString = 28U,
			Utf8String = 12U,
			BmpString = 30U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Choice()]
	partial class X520LocalityName : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<X520LocalityName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TeletexString TeletexString
		{
			get => this.teletexString.Value;
			set
			{
				this.teletexString = value;
				this._choiceTag = ChoiceIndex.TeletexString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private TeletexString? teletexString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrintableString PrintableString
		{
			get => this.printableString.Value;
			set
			{
				this.printableString = value;
				this._choiceTag = ChoiceIndex.PrintableString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PrintableString? printableString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public UniversalString UniversalString
		{
			get => this.universalString.Value;
			set
			{
				this.universalString = value;
				this._choiceTag = ChoiceIndex.UniversalString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private UniversalString? universalString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public String Utf8String
		{
			get => this.utf8String;
			set
			{
				this.utf8String = value;
				this._choiceTag = ChoiceIndex.Utf8String;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private String? utf8String;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BMPString BmpString
		{
			get => this.bmpString.Value;
			set
			{
				this.bmpString = value;
				this._choiceTag = ChoiceIndex.BmpString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private BMPString? bmpString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public X520LocalityName()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.BmpString:
					Debug.Assert(this.bmpString is not null);
					encoder.EncodeStringTlv(this.bmpString.Value);
					break;
				case ChoiceIndex.Utf8String:
					Debug.Assert(this.utf8String is not null);
					encoder.EncodeUtf8StringTlv(this.utf8String);
					break;
				case ChoiceIndex.UniversalString:
					Debug.Assert(this.universalString is not null);
					encoder.EncodeStringTlv(this.universalString.Value);
					break;
				case ChoiceIndex.PrintableString:
					Debug.Assert(this.printableString is not null);
					encoder.EncodeStringTlv(this.printableString.Value);
					break;
				case ChoiceIndex.TeletexString:
					Debug.Assert(this.teletexString is not null);
					encoder.EncodeStringTlv(this.teletexString.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type X520LocalityName has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static X520LocalityName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!X520LocalityName.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out X520LocalityName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x14)))
				instance = new X520LocalityName()
				{
					_choiceTag = ChoiceIndex.TeletexString,
					teletexString = decoder.DecodeStringTlv<TeletexString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x13)))
				instance = new X520LocalityName()
				{
					_choiceTag = ChoiceIndex.PrintableString,
					printableString = decoder.DecodeStringTlv<PrintableString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1C)))
				instance = new X520LocalityName()
				{
					_choiceTag = ChoiceIndex.UniversalString,
					universalString = decoder.DecodeStringTlv<UniversalString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0xC)))
				instance = new X520LocalityName()
				{
					_choiceTag = ChoiceIndex.Utf8String,
					utf8String = decoder.DecodeUtf8StringTlv()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1E)))
				instance = new X520LocalityName()
				{
					_choiceTag = ChoiceIndex.BmpString,
					bmpString = decoder.DecodeStringTlv<BMPString>()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			TeletexString = 20U,
			PrintableString = 19U,
			UniversalString = 28U,
			Utf8String = 12U,
			BmpString = 30U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Choice()]
	partial class X520StateOrProvinceName : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<X520StateOrProvinceName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TeletexString TeletexString
		{
			get => this.teletexString.Value;
			set
			{
				this.teletexString = value;
				this._choiceTag = ChoiceIndex.TeletexString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private TeletexString? teletexString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrintableString PrintableString
		{
			get => this.printableString.Value;
			set
			{
				this.printableString = value;
				this._choiceTag = ChoiceIndex.PrintableString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PrintableString? printableString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public UniversalString UniversalString
		{
			get => this.universalString.Value;
			set
			{
				this.universalString = value;
				this._choiceTag = ChoiceIndex.UniversalString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private UniversalString? universalString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public String Utf8String
		{
			get => this.utf8String;
			set
			{
				this.utf8String = value;
				this._choiceTag = ChoiceIndex.Utf8String;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private String? utf8String;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BMPString BmpString
		{
			get => this.bmpString.Value;
			set
			{
				this.bmpString = value;
				this._choiceTag = ChoiceIndex.BmpString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private BMPString? bmpString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public X520StateOrProvinceName()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.BmpString:
					Debug.Assert(this.bmpString is not null);
					encoder.EncodeStringTlv(this.bmpString.Value);
					break;
				case ChoiceIndex.Utf8String:
					Debug.Assert(this.utf8String is not null);
					encoder.EncodeUtf8StringTlv(this.utf8String);
					break;
				case ChoiceIndex.UniversalString:
					Debug.Assert(this.universalString is not null);
					encoder.EncodeStringTlv(this.universalString.Value);
					break;
				case ChoiceIndex.PrintableString:
					Debug.Assert(this.printableString is not null);
					encoder.EncodeStringTlv(this.printableString.Value);
					break;
				case ChoiceIndex.TeletexString:
					Debug.Assert(this.teletexString is not null);
					encoder.EncodeStringTlv(this.teletexString.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type X520StateOrProvinceName has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static X520StateOrProvinceName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!X520StateOrProvinceName.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out X520StateOrProvinceName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x14)))
				instance = new X520StateOrProvinceName()
				{
					_choiceTag = ChoiceIndex.TeletexString,
					teletexString = decoder.DecodeStringTlv<TeletexString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x13)))
				instance = new X520StateOrProvinceName()
				{
					_choiceTag = ChoiceIndex.PrintableString,
					printableString = decoder.DecodeStringTlv<PrintableString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1C)))
				instance = new X520StateOrProvinceName()
				{
					_choiceTag = ChoiceIndex.UniversalString,
					universalString = decoder.DecodeStringTlv<UniversalString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0xC)))
				instance = new X520StateOrProvinceName()
				{
					_choiceTag = ChoiceIndex.Utf8String,
					utf8String = decoder.DecodeUtf8StringTlv()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1E)))
				instance = new X520StateOrProvinceName()
				{
					_choiceTag = ChoiceIndex.BmpString,
					bmpString = decoder.DecodeStringTlv<BMPString>()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			TeletexString = 20U,
			PrintableString = 19U,
			UniversalString = 28U,
			Utf8String = 12U,
			BmpString = 30U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Choice()]
	partial class X520OrganizationName : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<X520OrganizationName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TeletexString TeletexString
		{
			get => this.teletexString.Value;
			set
			{
				this.teletexString = value;
				this._choiceTag = ChoiceIndex.TeletexString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private TeletexString? teletexString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrintableString PrintableString
		{
			get => this.printableString.Value;
			set
			{
				this.printableString = value;
				this._choiceTag = ChoiceIndex.PrintableString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PrintableString? printableString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public UniversalString UniversalString
		{
			get => this.universalString.Value;
			set
			{
				this.universalString = value;
				this._choiceTag = ChoiceIndex.UniversalString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private UniversalString? universalString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public String Utf8String
		{
			get => this.utf8String;
			set
			{
				this.utf8String = value;
				this._choiceTag = ChoiceIndex.Utf8String;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private String? utf8String;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BMPString BmpString
		{
			get => this.bmpString.Value;
			set
			{
				this.bmpString = value;
				this._choiceTag = ChoiceIndex.BmpString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private BMPString? bmpString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public X520OrganizationName()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.BmpString:
					Debug.Assert(this.bmpString is not null);
					encoder.EncodeStringTlv(this.bmpString.Value);
					break;
				case ChoiceIndex.Utf8String:
					Debug.Assert(this.utf8String is not null);
					encoder.EncodeUtf8StringTlv(this.utf8String);
					break;
				case ChoiceIndex.UniversalString:
					Debug.Assert(this.universalString is not null);
					encoder.EncodeStringTlv(this.universalString.Value);
					break;
				case ChoiceIndex.PrintableString:
					Debug.Assert(this.printableString is not null);
					encoder.EncodeStringTlv(this.printableString.Value);
					break;
				case ChoiceIndex.TeletexString:
					Debug.Assert(this.teletexString is not null);
					encoder.EncodeStringTlv(this.teletexString.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type X520OrganizationName has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static X520OrganizationName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!X520OrganizationName.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out X520OrganizationName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x14)))
				instance = new X520OrganizationName()
				{
					_choiceTag = ChoiceIndex.TeletexString,
					teletexString = decoder.DecodeStringTlv<TeletexString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x13)))
				instance = new X520OrganizationName()
				{
					_choiceTag = ChoiceIndex.PrintableString,
					printableString = decoder.DecodeStringTlv<PrintableString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1C)))
				instance = new X520OrganizationName()
				{
					_choiceTag = ChoiceIndex.UniversalString,
					universalString = decoder.DecodeStringTlv<UniversalString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0xC)))
				instance = new X520OrganizationName()
				{
					_choiceTag = ChoiceIndex.Utf8String,
					utf8String = decoder.DecodeUtf8StringTlv()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1E)))
				instance = new X520OrganizationName()
				{
					_choiceTag = ChoiceIndex.BmpString,
					bmpString = decoder.DecodeStringTlv<BMPString>()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			TeletexString = 20U,
			PrintableString = 19U,
			UniversalString = 28U,
			Utf8String = 12U,
			BmpString = 30U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Choice()]
	partial class X520OrganizationalUnitName : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<X520OrganizationalUnitName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TeletexString TeletexString
		{
			get => this.teletexString.Value;
			set
			{
				this.teletexString = value;
				this._choiceTag = ChoiceIndex.TeletexString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private TeletexString? teletexString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrintableString PrintableString
		{
			get => this.printableString.Value;
			set
			{
				this.printableString = value;
				this._choiceTag = ChoiceIndex.PrintableString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PrintableString? printableString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public UniversalString UniversalString
		{
			get => this.universalString.Value;
			set
			{
				this.universalString = value;
				this._choiceTag = ChoiceIndex.UniversalString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private UniversalString? universalString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public String Utf8String
		{
			get => this.utf8String;
			set
			{
				this.utf8String = value;
				this._choiceTag = ChoiceIndex.Utf8String;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private String? utf8String;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BMPString BmpString
		{
			get => this.bmpString.Value;
			set
			{
				this.bmpString = value;
				this._choiceTag = ChoiceIndex.BmpString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private BMPString? bmpString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public X520OrganizationalUnitName()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.BmpString:
					Debug.Assert(this.bmpString is not null);
					encoder.EncodeStringTlv(this.bmpString.Value);
					break;
				case ChoiceIndex.Utf8String:
					Debug.Assert(this.utf8String is not null);
					encoder.EncodeUtf8StringTlv(this.utf8String);
					break;
				case ChoiceIndex.UniversalString:
					Debug.Assert(this.universalString is not null);
					encoder.EncodeStringTlv(this.universalString.Value);
					break;
				case ChoiceIndex.PrintableString:
					Debug.Assert(this.printableString is not null);
					encoder.EncodeStringTlv(this.printableString.Value);
					break;
				case ChoiceIndex.TeletexString:
					Debug.Assert(this.teletexString is not null);
					encoder.EncodeStringTlv(this.teletexString.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type X520OrganizationalUnitName has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static X520OrganizationalUnitName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!X520OrganizationalUnitName.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out X520OrganizationalUnitName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x14)))
				instance = new X520OrganizationalUnitName()
				{
					_choiceTag = ChoiceIndex.TeletexString,
					teletexString = decoder.DecodeStringTlv<TeletexString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x13)))
				instance = new X520OrganizationalUnitName()
				{
					_choiceTag = ChoiceIndex.PrintableString,
					printableString = decoder.DecodeStringTlv<PrintableString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1C)))
				instance = new X520OrganizationalUnitName()
				{
					_choiceTag = ChoiceIndex.UniversalString,
					universalString = decoder.DecodeStringTlv<UniversalString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0xC)))
				instance = new X520OrganizationalUnitName()
				{
					_choiceTag = ChoiceIndex.Utf8String,
					utf8String = decoder.DecodeUtf8StringTlv()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1E)))
				instance = new X520OrganizationalUnitName()
				{
					_choiceTag = ChoiceIndex.BmpString,
					bmpString = decoder.DecodeStringTlv<BMPString>()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			TeletexString = 20U,
			PrintableString = 19U,
			UniversalString = 28U,
			Utf8String = 12U,
			BmpString = 30U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Choice()]
	partial class X520Title : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<X520Title>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TeletexString TeletexString
		{
			get => this.teletexString.Value;
			set
			{
				this.teletexString = value;
				this._choiceTag = ChoiceIndex.TeletexString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private TeletexString? teletexString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrintableString PrintableString
		{
			get => this.printableString.Value;
			set
			{
				this.printableString = value;
				this._choiceTag = ChoiceIndex.PrintableString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PrintableString? printableString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public UniversalString UniversalString
		{
			get => this.universalString.Value;
			set
			{
				this.universalString = value;
				this._choiceTag = ChoiceIndex.UniversalString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private UniversalString? universalString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public String Utf8String
		{
			get => this.utf8String;
			set
			{
				this.utf8String = value;
				this._choiceTag = ChoiceIndex.Utf8String;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private String? utf8String;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BMPString BmpString
		{
			get => this.bmpString.Value;
			set
			{
				this.bmpString = value;
				this._choiceTag = ChoiceIndex.BmpString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private BMPString? bmpString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public X520Title()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.BmpString:
					Debug.Assert(this.bmpString is not null);
					encoder.EncodeStringTlv(this.bmpString.Value);
					break;
				case ChoiceIndex.Utf8String:
					Debug.Assert(this.utf8String is not null);
					encoder.EncodeUtf8StringTlv(this.utf8String);
					break;
				case ChoiceIndex.UniversalString:
					Debug.Assert(this.universalString is not null);
					encoder.EncodeStringTlv(this.universalString.Value);
					break;
				case ChoiceIndex.PrintableString:
					Debug.Assert(this.printableString is not null);
					encoder.EncodeStringTlv(this.printableString.Value);
					break;
				case ChoiceIndex.TeletexString:
					Debug.Assert(this.teletexString is not null);
					encoder.EncodeStringTlv(this.teletexString.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type X520Title has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static X520Title DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!X520Title.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out X520Title? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x14)))
				instance = new X520Title()
				{
					_choiceTag = ChoiceIndex.TeletexString,
					teletexString = decoder.DecodeStringTlv<TeletexString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x13)))
				instance = new X520Title()
				{
					_choiceTag = ChoiceIndex.PrintableString,
					printableString = decoder.DecodeStringTlv<PrintableString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1C)))
				instance = new X520Title()
				{
					_choiceTag = ChoiceIndex.UniversalString,
					universalString = decoder.DecodeStringTlv<UniversalString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0xC)))
				instance = new X520Title()
				{
					_choiceTag = ChoiceIndex.Utf8String,
					utf8String = decoder.DecodeUtf8StringTlv()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1E)))
				instance = new X520Title()
				{
					_choiceTag = ChoiceIndex.BmpString,
					bmpString = decoder.DecodeStringTlv<BMPString>()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			TeletexString = 20U,
			PrintableString = 19U,
			UniversalString = 28U,
			Utf8String = 12U,
			BmpString = 30U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Choice()]
	partial class Name : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<Name>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttributeTypeAndValue[][] RdnSequence
		{
			get => this.rdnSequence;
			set
			{
				this.rdnSequence = value;
				this._choiceTag = ChoiceIndex.RdnSequence;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private AttributeTypeAndValue[][]? rdnSequence;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Name()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.RdnSequence:
					Debug.Assert(this.rdnSequence is not null);
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.rdnSequence, (encoder, r) =>
					{
						encoder.EncodeListTlv(new Asn1Tag(0x20000011), r, (encoder, r) =>
						{
							encoder.EncodeValueTlv(r);
						});
					});
					break;
				default:
					throw new InvalidOperationException("The object of type Name has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Name DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!Name.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Name? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
				instance = new Name()
				{
					_choiceTag = ChoiceIndex.RdnSequence,
					rdnSequence = decoder.DecodeListTlv<AttributeTypeAndValue[]>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeListTlv<AttributeTypeAndValue>(new Asn1Tag(0x20000011), (encoder) => AttributeTypeAndValue.DecodeTlvFrom(decoder)))
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			RdnSequence = 536870928U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Choice()]
	partial class DirectoryString : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<DirectoryString>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TeletexString TeletexString
		{
			get => this.teletexString.Value;
			set
			{
				this.teletexString = value;
				this._choiceTag = ChoiceIndex.TeletexString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private TeletexString? teletexString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrintableString PrintableString
		{
			get => this.printableString.Value;
			set
			{
				this.printableString = value;
				this._choiceTag = ChoiceIndex.PrintableString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PrintableString? printableString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public UniversalString UniversalString
		{
			get => this.universalString.Value;
			set
			{
				this.universalString = value;
				this._choiceTag = ChoiceIndex.UniversalString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private UniversalString? universalString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public String Utf8String
		{
			get => this.utf8String;
			set
			{
				this.utf8String = value;
				this._choiceTag = ChoiceIndex.Utf8String;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private String? utf8String;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BMPString BmpString
		{
			get => this.bmpString.Value;
			set
			{
				this.bmpString = value;
				this._choiceTag = ChoiceIndex.BmpString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private BMPString? bmpString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public DirectoryString()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.BmpString:
					Debug.Assert(this.bmpString is not null);
					encoder.EncodeStringTlv(this.bmpString.Value);
					break;
				case ChoiceIndex.Utf8String:
					Debug.Assert(this.utf8String is not null);
					encoder.EncodeUtf8StringTlv(this.utf8String);
					break;
				case ChoiceIndex.UniversalString:
					Debug.Assert(this.universalString is not null);
					encoder.EncodeStringTlv(this.universalString.Value);
					break;
				case ChoiceIndex.PrintableString:
					Debug.Assert(this.printableString is not null);
					encoder.EncodeStringTlv(this.printableString.Value);
					break;
				case ChoiceIndex.TeletexString:
					Debug.Assert(this.teletexString is not null);
					encoder.EncodeStringTlv(this.teletexString.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type DirectoryString has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static DirectoryString DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!DirectoryString.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out DirectoryString? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x14)))
				instance = new DirectoryString()
				{
					_choiceTag = ChoiceIndex.TeletexString,
					teletexString = decoder.DecodeStringTlv<TeletexString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x13)))
				instance = new DirectoryString()
				{
					_choiceTag = ChoiceIndex.PrintableString,
					printableString = decoder.DecodeStringTlv<PrintableString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1C)))
				instance = new DirectoryString()
				{
					_choiceTag = ChoiceIndex.UniversalString,
					universalString = decoder.DecodeStringTlv<UniversalString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0xC)))
				instance = new DirectoryString()
				{
					_choiceTag = ChoiceIndex.Utf8String,
					utf8String = decoder.DecodeUtf8StringTlv()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1E)))
				instance = new DirectoryString()
				{
					_choiceTag = ChoiceIndex.BmpString,
					bmpString = decoder.DecodeStringTlv<BMPString>()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			TeletexString = 20U,
			PrintableString = 19U,
			UniversalString = 28U,
			Utf8String = 12U,
			BmpString = 30U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class AlgorithmIdentifier : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AlgorithmIdentifier>, IAsn1DerDecodableValue<AlgorithmIdentifier>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid algorithm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Any? parameters;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AlgorithmIdentifier(Asn1Oid algorithm, Asn1Any? parameters = default)
		{
			this.algorithm = algorithm;
			this.parameters = parameters;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.parameters is not null)
				this.parameters.EncodeTlv(encoder);
			encoder.EncodeOidTlv(this.algorithm);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AlgorithmIdentifier DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AlgorithmIdentifier(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AlgorithmIdentifier DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AlgorithmIdentifier.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AlgorithmIdentifier? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AlgorithmIdentifier.DecodeValueFrom(decoder);
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
		private AlgorithmIdentifier(Asn1DerDecoder decoder)
		{
			this.algorithm = decoder.DecodeOidTlv();
			this.parameters = decoder.TryDecodeTlv<Asn1Any>(out this.parameters) ? this.parameters : default(Asn1Any);
		}
	}

	[Asn1Choice()]
	partial class Time : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<Time>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public UtcTime UtcTime
		{
			get => this.utcTime.Value;
			set
			{
				this.utcTime = value;
				this._choiceTag = ChoiceIndex.UtcTime;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private UtcTime? utcTime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public GeneralizedTime GeneralTime
		{
			get => this.generalTime.Value;
			set
			{
				this.generalTime = value;
				this._choiceTag = ChoiceIndex.GeneralTime;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private GeneralizedTime? generalTime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Time()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.GeneralTime:
					Debug.Assert(this.generalTime is not null);
					encoder.EncodeDateTimeTlv(this.generalTime.Value);
					break;
				case ChoiceIndex.UtcTime:
					Debug.Assert(this.utcTime is not null);
					encoder.EncodeUtcTimeTlv(this.utcTime.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type Time has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Time DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!Time.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Time? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x17)))
				instance = new Time()
				{
					_choiceTag = ChoiceIndex.UtcTime,
					utcTime = decoder.DecodeUtcTimeTlv()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x18)))
				instance = new Time()
				{
					_choiceTag = ChoiceIndex.GeneralTime,
					generalTime = decoder.DecodeDateTimeTlv()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			UtcTime = 23U,
			GeneralTime = 24U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class Validity : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Validity>, IAsn1DerDecodableValue<Validity>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Time notBefore;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Time notAfter;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Validity(Time notBefore, Time notAfter)
		{
			this.notBefore = notBefore;
			this.notAfter = notAfter;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			this.notAfter.EncodeTlv(encoder);
			this.notBefore.EncodeTlv(encoder);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Validity DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Validity(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Validity DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Validity.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Validity? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Validity.DecodeValueFrom(decoder);
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
		private Validity(Asn1DerDecoder decoder)
		{
			this.notBefore = decoder.DecodeTlv<Time>();
			this.notAfter = decoder.DecodeTlv<Time>();
		}
	}

	[Asn1Sequence()]
	partial class SubjectPublicKeyInfo : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<SubjectPublicKeyInfo>, IAsn1DerDecodableValue<SubjectPublicKeyInfo>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AlgorithmIdentifier algorithm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString subjectPublicKey;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SubjectPublicKeyInfo(AlgorithmIdentifier algorithm, Asn1BitString subjectPublicKey)
		{
			this.algorithm = algorithm;
			this.subjectPublicKey = subjectPublicKey;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBitStringTlv(this.subjectPublicKey);
			encoder.EncodeValueTlv(this.algorithm);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SubjectPublicKeyInfo DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new SubjectPublicKeyInfo(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SubjectPublicKeyInfo DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = SubjectPublicKeyInfo.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out SubjectPublicKeyInfo? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = SubjectPublicKeyInfo.DecodeValueFrom(decoder);
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
		private SubjectPublicKeyInfo(Asn1DerDecoder decoder)
		{
			this.algorithm = AlgorithmIdentifier.DecodeTlvFrom(decoder);
			this.subjectPublicKey = decoder.DecodeBitStringTlv();
		}
	}

	[Asn1Sequence()]
	partial class Extension : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Extension>, IAsn1DerDecodableValue<Extension>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid extnID;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal bool critical;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] extnValue;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Extension(Asn1Oid extnID, Byte[] extnValue, bool critical = false)
		{
			this.extnID = extnID;
			this.critical = critical;
			this.extnValue = extnValue;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeOctetStringTlv(this.extnValue);
			if (this.critical != false)
				encoder.EncodeBoolTlv(this.critical);
			encoder.EncodeOidTlv(this.extnID);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Extension DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Extension(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Extension DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Extension.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Extension? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Extension.DecodeValueFrom(decoder);
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
		private Extension(Asn1DerDecoder decoder)
		{
			this.extnID = decoder.DecodeOidTlv();
			this.critical = decoder.CheckTag(new Asn1Tag(0x1)) ? decoder.DecodeBoolTlv() : false;
			this.extnValue = decoder.DecodeOctetStringTlv();
		}
	}

	[Asn1Sequence()]
	partial class TBSCertificate : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<TBSCertificate>, IAsn1DerDecodableValue<TBSCertificate>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int version;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger serialNumber;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AlgorithmIdentifier signature;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Name issuer;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Validity validity;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Name subject;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal SubjectPublicKeyInfo subjectPublicKeyInfo;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString? issuerUniqueID;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString? subjectUniqueID;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Extension[]? extensions;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TBSCertificate(System.Numerics.BigInteger serialNumber, AlgorithmIdentifier signature, Name issuer, Validity validity, Name subject, SubjectPublicKeyInfo subjectPublicKeyInfo, int version = 0, Asn1BitString? issuerUniqueID = default, Asn1BitString? subjectUniqueID = default, Extension[]? extensions = default)
		{
			this.version = version;
			this.serialNumber = serialNumber;
			this.signature = signature;
			this.issuer = issuer;
			this.validity = validity;
			this.subject = subject;
			this.subjectPublicKeyInfo = subjectPublicKeyInfo;
			this.issuerUniqueID = issuerUniqueID;
			this.subjectUniqueID = subjectUniqueID;
			this.extensions = extensions;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.extensions is not null)
				encoder.EncodeExplicitTlv<Extension[]>(new Asn1Tag(0xA0000003), this.extensions, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.extensions, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			if (this.subjectUniqueID is not null)
				encoder.EncodeBitStringTlv(this.subjectUniqueID.Value);
			if (this.issuerUniqueID is not null)
				encoder.EncodeBitStringTlv(this.issuerUniqueID.Value);
			encoder.EncodeValueTlv(this.subjectPublicKeyInfo);
			this.subject.EncodeTlv(encoder);
			encoder.EncodeValueTlv(this.validity);
			this.issuer.EncodeTlv(encoder);
			encoder.EncodeValueTlv(this.signature);
			encoder.EncodeBigIntegerTlv(this.serialNumber);
			if (this.version != 0)
				encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.version, (encoder, r) =>
				{
					encoder.EncodeInt32Tlv(this.version);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TBSCertificate DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new TBSCertificate(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TBSCertificate DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = TBSCertificate.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out TBSCertificate? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = TBSCertificate.DecodeValueFrom(decoder);
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
		private TBSCertificate(Asn1DerDecoder decoder)
		{
			this.version = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32()) : 0;
			this.serialNumber = decoder.DecodeIntegerTlvAsBigInteger();
			this.signature = AlgorithmIdentifier.DecodeTlvFrom(decoder);
			this.issuer = decoder.DecodeTlv<Name>();
			this.validity = Validity.DecodeTlvFrom(decoder);
			this.subject = decoder.DecodeTlv<Name>();
			this.subjectPublicKeyInfo = SubjectPublicKeyInfo.DecodeTlvFrom(decoder);
			this.issuerUniqueID = decoder.CheckTag(new Asn1Tag(0x80000001)) ? decoder.DecodeBitStringTlv(new Asn1Tag(0x80000001)) : default(Asn1BitString? );
			this.subjectUniqueID = decoder.CheckTag(new Asn1Tag(0x80000002)) ? decoder.DecodeBitStringTlv(new Asn1Tag(0x80000002)) : default(Asn1BitString? );
			this.extensions = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<Extension[]>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeListTlv<Extension>(new Asn1Tag(0x20000010), (encoder) => Extension.DecodeTlvFrom(decoder))) : default(Extension[]);
		}
	}

	[Asn1Sequence()]
	partial class Certificate : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Certificate>, IAsn1DerDecodableValue<Certificate>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal TBSCertificate tbsCertificate;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AlgorithmIdentifier signatureAlgorithm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString signature;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Certificate(TBSCertificate tbsCertificate, AlgorithmIdentifier signatureAlgorithm, Asn1BitString signature)
		{
			this.tbsCertificate = tbsCertificate;
			this.signatureAlgorithm = signatureAlgorithm;
			this.signature = signature;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBitStringTlv(this.signature);
			encoder.EncodeValueTlv(this.signatureAlgorithm);
			encoder.EncodeValueTlv(this.tbsCertificate);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Certificate DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Certificate(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Certificate DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Certificate.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Certificate? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Certificate.DecodeValueFrom(decoder);
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
		private Certificate(Asn1DerDecoder decoder)
		{
			this.tbsCertificate = TBSCertificate.DecodeTlvFrom(decoder);
			this.signatureAlgorithm = AlgorithmIdentifier.DecodeTlvFrom(decoder);
			this.signature = decoder.DecodeBitStringTlv();
		}
	}

	[Asn1Sequence()]
	partial class TBSCertList : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<TBSCertList>, IAsn1DerDecodableValue<TBSCertList>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int? version;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AlgorithmIdentifier signature;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Name issuer;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Time thisUpdate;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Time? nextUpdate;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal TBSCertList_RevokedCertificates_Element[]? revokedCertificates;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Extension[]? crlExtensions;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TBSCertList(AlgorithmIdentifier signature, Name issuer, Time thisUpdate, int? version = default, Time? nextUpdate = default, TBSCertList_RevokedCertificates_Element[]? revokedCertificates = default, Extension[]? crlExtensions = default)
		{
			this.version = version;
			this.signature = signature;
			this.issuer = issuer;
			this.thisUpdate = thisUpdate;
			this.nextUpdate = nextUpdate;
			this.revokedCertificates = revokedCertificates;
			this.crlExtensions = crlExtensions;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.crlExtensions is not null)
				encoder.EncodeExplicitTlv<Extension[]>(new Asn1Tag(0xA0000000), this.crlExtensions, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.crlExtensions, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			if (this.revokedCertificates is not null)
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.revokedCertificates, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			if (this.nextUpdate is not null)
				this.nextUpdate.EncodeTlv(encoder);
			this.thisUpdate.EncodeTlv(encoder);
			this.issuer.EncodeTlv(encoder);
			encoder.EncodeValueTlv(this.signature);
			if (this.version is not null)
				encoder.EncodeInt32Tlv(this.version.Value);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TBSCertList DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new TBSCertList(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TBSCertList DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = TBSCertList.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out TBSCertList? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = TBSCertList.DecodeValueFrom(decoder);
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
		private TBSCertList(Asn1DerDecoder decoder)
		{
			this.version = decoder.CheckTag(new Asn1Tag(0x2)) ? decoder.DecodeIntegerTlvAsInt32() : default(int? );
			this.signature = AlgorithmIdentifier.DecodeTlvFrom(decoder);
			this.issuer = decoder.DecodeTlv<Name>();
			this.thisUpdate = decoder.DecodeTlv<Time>();
			this.nextUpdate = decoder.TryDecodeTlv<Time>(out this.nextUpdate) ? this.nextUpdate : default(Time);
			this.revokedCertificates = decoder.CheckTag(new Asn1Tag(0x20000010)) ? decoder.DecodeListTlv<TBSCertList_RevokedCertificates_Element>(new Asn1Tag(0x20000010), (encoder) => TBSCertList_RevokedCertificates_Element.DecodeTlvFrom(decoder)) : default(TBSCertList_RevokedCertificates_Element[]);
			this.crlExtensions = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeTaggedValue<Extension[]>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeListTlv<Extension>(new Asn1Tag(0x20000010), (encoder) => Extension.DecodeTlvFrom(decoder))) : default(Extension[]);
		}
	}

	[Asn1Sequence()]
	partial class TBSCertList_RevokedCertificates_Element : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<TBSCertList_RevokedCertificates_Element>, IAsn1DerDecodableValue<TBSCertList_RevokedCertificates_Element>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger userCertificate;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Time revocationDate;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Extension[]? crlEntryExtensions;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TBSCertList_RevokedCertificates_Element(System.Numerics.BigInteger userCertificate, Time revocationDate, Extension[]? crlEntryExtensions = default)
		{
			this.userCertificate = userCertificate;
			this.revocationDate = revocationDate;
			this.crlEntryExtensions = crlEntryExtensions;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.crlEntryExtensions is not null)
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.crlEntryExtensions, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			this.revocationDate.EncodeTlv(encoder);
			encoder.EncodeBigIntegerTlv(this.userCertificate);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TBSCertList_RevokedCertificates_Element DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new TBSCertList_RevokedCertificates_Element(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TBSCertList_RevokedCertificates_Element DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = TBSCertList_RevokedCertificates_Element.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out TBSCertList_RevokedCertificates_Element? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = TBSCertList_RevokedCertificates_Element.DecodeValueFrom(decoder);
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
		private TBSCertList_RevokedCertificates_Element(Asn1DerDecoder decoder)
		{
			this.userCertificate = decoder.DecodeIntegerTlvAsBigInteger();
			this.revocationDate = decoder.DecodeTlv<Time>();
			this.crlEntryExtensions = decoder.CheckTag(new Asn1Tag(0x20000010)) ? decoder.DecodeListTlv<Extension>(new Asn1Tag(0x20000010), (encoder) => Extension.DecodeTlvFrom(decoder)) : default(Extension[]);
		}
	}

	[Asn1Sequence()]
	partial class CertificateList : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<CertificateList>, IAsn1DerDecodableValue<CertificateList>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal TBSCertList tbsCertList;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AlgorithmIdentifier signatureAlgorithm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString signature;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public CertificateList(TBSCertList tbsCertList, AlgorithmIdentifier signatureAlgorithm, Asn1BitString signature)
		{
			this.tbsCertList = tbsCertList;
			this.signatureAlgorithm = signatureAlgorithm;
			this.signature = signature;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBitStringTlv(this.signature);
			encoder.EncodeValueTlv(this.signatureAlgorithm);
			encoder.EncodeValueTlv(this.tbsCertList);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static CertificateList DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new CertificateList(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static CertificateList DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = CertificateList.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out CertificateList? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = CertificateList.DecodeValueFrom(decoder);
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
		private CertificateList(Asn1DerDecoder decoder)
		{
			this.tbsCertList = TBSCertList.DecodeTlvFrom(decoder);
			this.signatureAlgorithm = AlgorithmIdentifier.DecodeTlvFrom(decoder);
			this.signature = decoder.DecodeBitStringTlv();
		}
	}

	[Asn1Sequence()]
	partial class Dss_Sig_Value : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Dss_Sig_Value>, IAsn1DerDecodableValue<Dss_Sig_Value>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger r;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger s;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Dss_Sig_Value(System.Numerics.BigInteger r, System.Numerics.BigInteger s)
		{
			this.r = r;
			this.s = s;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBigIntegerTlv(this.s);
			encoder.EncodeBigIntegerTlv(this.r);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Dss_Sig_Value DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Dss_Sig_Value(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Dss_Sig_Value DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Dss_Sig_Value.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Dss_Sig_Value? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Dss_Sig_Value.DecodeValueFrom(decoder);
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
		private Dss_Sig_Value(Asn1DerDecoder decoder)
		{
			this.r = decoder.DecodeIntegerTlvAsBigInteger();
			this.s = decoder.DecodeIntegerTlvAsBigInteger();
		}
	}

	[Asn1Sequence()]
	partial class ValidationParms : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ValidationParms>, IAsn1DerDecodableValue<ValidationParms>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString seed;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger pgenCounter;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ValidationParms(Asn1BitString seed, System.Numerics.BigInteger pgenCounter)
		{
			this.seed = seed;
			this.pgenCounter = pgenCounter;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBigIntegerTlv(this.pgenCounter);
			encoder.EncodeBitStringTlv(this.seed);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ValidationParms DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ValidationParms(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ValidationParms DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ValidationParms.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ValidationParms? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ValidationParms.DecodeValueFrom(decoder);
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
		private ValidationParms(Asn1DerDecoder decoder)
		{
			this.seed = decoder.DecodeBitStringTlv();
			this.pgenCounter = decoder.DecodeIntegerTlvAsBigInteger();
		}
	}

	[Asn1Sequence()]
	partial class DomainParameters : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<DomainParameters>, IAsn1DerDecodableValue<DomainParameters>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger p;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger g;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger q;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger? j;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ValidationParms? validationParms;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public DomainParameters(System.Numerics.BigInteger p, System.Numerics.BigInteger g, System.Numerics.BigInteger q, System.Numerics.BigInteger? j = default, ValidationParms? validationParms = default)
		{
			this.p = p;
			this.g = g;
			this.q = q;
			this.j = j;
			this.validationParms = validationParms;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.validationParms is not null)
				encoder.EncodeValueTlv(this.validationParms);
			if (this.j is not null)
				encoder.EncodeBigIntegerTlv(this.j.Value);
			encoder.EncodeBigIntegerTlv(this.q);
			encoder.EncodeBigIntegerTlv(this.g);
			encoder.EncodeBigIntegerTlv(this.p);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static DomainParameters DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new DomainParameters(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static DomainParameters DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = DomainParameters.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out DomainParameters? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = DomainParameters.DecodeValueFrom(decoder);
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
		private DomainParameters(Asn1DerDecoder decoder)
		{
			this.p = decoder.DecodeIntegerTlvAsBigInteger();
			this.g = decoder.DecodeIntegerTlvAsBigInteger();
			this.q = decoder.DecodeIntegerTlvAsBigInteger();
			this.j = decoder.CheckTag(new Asn1Tag(0x2)) ? decoder.DecodeIntegerTlvAsBigInteger() : default(System.Numerics.BigInteger? );
			this.validationParms = decoder.CheckTag(new Asn1Tag(0x20000010)) ? ValidationParms.DecodeTlvFrom(decoder) : default(ValidationParms);
		}
	}

	[Asn1Sequence()]
	partial class Dss_Parms : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Dss_Parms>, IAsn1DerDecodableValue<Dss_Parms>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger p;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger q;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger g;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Dss_Parms(System.Numerics.BigInteger p, System.Numerics.BigInteger q, System.Numerics.BigInteger g)
		{
			this.p = p;
			this.q = q;
			this.g = g;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBigIntegerTlv(this.g);
			encoder.EncodeBigIntegerTlv(this.q);
			encoder.EncodeBigIntegerTlv(this.p);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Dss_Parms DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Dss_Parms(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Dss_Parms DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Dss_Parms.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Dss_Parms? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Dss_Parms.DecodeValueFrom(decoder);
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
		private Dss_Parms(Asn1DerDecoder decoder)
		{
			this.p = decoder.DecodeIntegerTlvAsBigInteger();
			this.q = decoder.DecodeIntegerTlvAsBigInteger();
			this.g = decoder.DecodeIntegerTlvAsBigInteger();
		}
	}

	partial class CountryName : Asn1Explicit<CountryName_Tagged1>, IAsn1DerDecodableTlv<CountryName>, IAsn1DerDecodableValue<CountryName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public CountryName(CountryName_Tagged1 value) : base(new Asn1Tag(0x60000001), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static CountryName DecodeValueFrom(Asn1DerDecoder decoder) => new CountryName(decoder.DecodeTlv<CountryName_Tagged1>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static CountryName DecodeTlvFrom(Asn1DerDecoder decoder) => new CountryName(decoder.DecodeExplicitTaggedTlv<CountryName_Tagged1>(new Asn1Tag(0x60000001)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out CountryName? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<CountryName_Tagged1>(new Asn1Tag(0x60000001), out var inner))
			{
				value = new CountryName(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Choice()]
	partial class CountryName_Tagged1 : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<CountryName_Tagged1>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NumericString X121_dcc_code
		{
			get => this.x121_dcc_code.Value;
			set
			{
				this.x121_dcc_code = value;
				this._choiceTag = ChoiceIndex.X121_dcc_code;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private NumericString? x121_dcc_code;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrintableString Iso_3166_alpha2_code
		{
			get => this.iso_3166_alpha2_code.Value;
			set
			{
				this.iso_3166_alpha2_code = value;
				this._choiceTag = ChoiceIndex.Iso_3166_alpha2_code;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PrintableString? iso_3166_alpha2_code;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public CountryName_Tagged1()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.Iso_3166_alpha2_code:
					Debug.Assert(this.iso_3166_alpha2_code is not null);
					encoder.EncodeStringTlv(this.iso_3166_alpha2_code.Value);
					break;
				case ChoiceIndex.X121_dcc_code:
					Debug.Assert(this.x121_dcc_code is not null);
					encoder.EncodeStringTlv(this.x121_dcc_code.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type  has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static CountryName_Tagged1 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!CountryName_Tagged1.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out CountryName_Tagged1? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x12)))
				instance = new CountryName_Tagged1()
				{
					_choiceTag = ChoiceIndex.X121_dcc_code,
					x121_dcc_code = decoder.DecodeStringTlv<NumericString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x13)))
				instance = new CountryName_Tagged1()
				{
					_choiceTag = ChoiceIndex.Iso_3166_alpha2_code,
					iso_3166_alpha2_code = decoder.DecodeStringTlv<PrintableString>()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			X121_dcc_code = 18U,
			Iso_3166_alpha2_code = 19U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	partial class AdministrationDomainName : Asn1Explicit<AdministrationDomainName_Tagged2>, IAsn1DerDecodableTlv<AdministrationDomainName>, IAsn1DerDecodableValue<AdministrationDomainName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AdministrationDomainName(AdministrationDomainName_Tagged2 value) : base(new Asn1Tag(0x60000002), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AdministrationDomainName DecodeValueFrom(Asn1DerDecoder decoder) => new AdministrationDomainName(decoder.DecodeTlv<AdministrationDomainName_Tagged2>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AdministrationDomainName DecodeTlvFrom(Asn1DerDecoder decoder) => new AdministrationDomainName(decoder.DecodeExplicitTaggedTlv<AdministrationDomainName_Tagged2>(new Asn1Tag(0x60000002)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AdministrationDomainName? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<AdministrationDomainName_Tagged2>(new Asn1Tag(0x60000002), out var inner))
			{
				value = new AdministrationDomainName(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Choice()]
	partial class AdministrationDomainName_Tagged2 : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<AdministrationDomainName_Tagged2>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NumericString Numeric
		{
			get => this.numeric.Value;
			set
			{
				this.numeric = value;
				this._choiceTag = ChoiceIndex.Numeric;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private NumericString? numeric;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrintableString Printable
		{
			get => this.printable.Value;
			set
			{
				this.printable = value;
				this._choiceTag = ChoiceIndex.Printable;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PrintableString? printable;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AdministrationDomainName_Tagged2()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.Printable:
					Debug.Assert(this.printable is not null);
					encoder.EncodeStringTlv(this.printable.Value);
					break;
				case ChoiceIndex.Numeric:
					Debug.Assert(this.numeric is not null);
					encoder.EncodeStringTlv(this.numeric.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type  has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AdministrationDomainName_Tagged2 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!AdministrationDomainName_Tagged2.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AdministrationDomainName_Tagged2? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x12)))
				instance = new AdministrationDomainName_Tagged2()
				{
					_choiceTag = ChoiceIndex.Numeric,
					numeric = decoder.DecodeStringTlv<NumericString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x13)))
				instance = new AdministrationDomainName_Tagged2()
				{
					_choiceTag = ChoiceIndex.Printable,
					printable = decoder.DecodeStringTlv<PrintableString>()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			Numeric = 18U,
			Printable = 19U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Choice()]
	partial class PrivateDomainName : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<PrivateDomainName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NumericString Numeric
		{
			get => this.numeric.Value;
			set
			{
				this.numeric = value;
				this._choiceTag = ChoiceIndex.Numeric;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private NumericString? numeric;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrintableString Printable
		{
			get => this.printable.Value;
			set
			{
				this.printable = value;
				this._choiceTag = ChoiceIndex.Printable;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PrintableString? printable;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrivateDomainName()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.Printable:
					Debug.Assert(this.printable is not null);
					encoder.EncodeStringTlv(this.printable.Value);
					break;
				case ChoiceIndex.Numeric:
					Debug.Assert(this.numeric is not null);
					encoder.EncodeStringTlv(this.numeric.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type PrivateDomainName has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PrivateDomainName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!PrivateDomainName.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PrivateDomainName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x12)))
				instance = new PrivateDomainName()
				{
					_choiceTag = ChoiceIndex.Numeric,
					numeric = decoder.DecodeStringTlv<NumericString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x13)))
				instance = new PrivateDomainName()
				{
					_choiceTag = ChoiceIndex.Printable,
					printable = decoder.DecodeStringTlv<PrintableString>()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			Numeric = 18U,
			Printable = 19U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Set()]
	partial class PersonalName : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PersonalName>, IAsn1DerDecodableValue<PersonalName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrintableString surname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrintableString? given_name;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrintableString? initials;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrintableString? generation_qualifier;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PersonalName(PrintableString surname, PrintableString? given_name = default, PrintableString? initials = default, PrintableString? generation_qualifier = default)
		{
			this.surname = surname;
			this.given_name = given_name;
			this.initials = initials;
			this.generation_qualifier = generation_qualifier;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000011);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000011);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.generation_qualifier is not null)
				encoder.EncodeExplicitTlv<PrintableString>(new Asn1Tag(0xA0000003), this.generation_qualifier.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.generation_qualifier.Value);
				});
			if (this.initials is not null)
				encoder.EncodeExplicitTlv<PrintableString>(new Asn1Tag(0xA0000002), this.initials.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.initials.Value);
				});
			if (this.given_name is not null)
				encoder.EncodeExplicitTlv<PrintableString>(new Asn1Tag(0xA0000001), this.given_name.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.given_name.Value);
				});
			encoder.EncodeExplicitTlv<PrintableString>(new Asn1Tag(0xA0000000), this.surname, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.surname);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PersonalName DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PersonalName(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PersonalName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000011));
			var instance = PersonalName.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PersonalName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000011)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000011));
				instance = PersonalName.DecodeValueFrom(decoder);
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
		private PersonalName(Asn1DerDecoder decoder)
		{
			while (!decoder.IsEndOfTuple)
				if (decoder.CheckTag(new Asn1Tag(0xA0000000)))
				{
					this.surname = decoder.DecodeTaggedValue<PrintableString>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeStringTlv<PrintableString>());
				}
				else if (decoder.CheckTag(new Asn1Tag(0xA0000001)))
				{
					this.given_name = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<PrintableString>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeStringTlv<PrintableString>()) : default(PrintableString? );
				}
				else if (decoder.CheckTag(new Asn1Tag(0xA0000002)))
				{
					this.initials = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<PrintableString>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeStringTlv<PrintableString>()) : default(PrintableString? );
				}
				else if (decoder.CheckTag(new Asn1Tag(0xA0000003)))
				{
					this.generation_qualifier = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<PrintableString>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeStringTlv<PrintableString>()) : default(PrintableString? );
				}
				else
				{
				}
		}
	}

	[Asn1Sequence()]
	partial class BuiltInStandardAttributes : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<BuiltInStandardAttributes>, IAsn1DerDecodableValue<BuiltInStandardAttributes>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal CountryName_Tagged1? country_name;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AdministrationDomainName_Tagged2? administration_domain_name;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal NumericString? network_address;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrintableString? terminal_identifier;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrivateDomainName? private_domain_name;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrintableString? organization_name;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal NumericString? numeric_user_identifier;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PersonalName? personal_name;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrintableString[]? organizational_unit_names;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BuiltInStandardAttributes(CountryName_Tagged1? country_name = default, AdministrationDomainName_Tagged2? administration_domain_name = default, NumericString? network_address = default, PrintableString? terminal_identifier = default, PrivateDomainName? private_domain_name = default, PrintableString? organization_name = default, NumericString? numeric_user_identifier = default, PersonalName? personal_name = default, PrintableString[]? organizational_unit_names = default)
		{
			this.country_name = country_name;
			this.administration_domain_name = administration_domain_name;
			this.network_address = network_address;
			this.terminal_identifier = terminal_identifier;
			this.private_domain_name = private_domain_name;
			this.organization_name = organization_name;
			this.numeric_user_identifier = numeric_user_identifier;
			this.personal_name = personal_name;
			this.organizational_unit_names = organizational_unit_names;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.organizational_unit_names is not null)
				encoder.EncodeExplicitTlv<PrintableString[]>(new Asn1Tag(0xA0000006), this.organizational_unit_names, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.organizational_unit_names, (encoder, r) =>
					{
						encoder.EncodeStringTlv(r);
					});
				});
			if (this.personal_name is not null)
				encoder.EncodeExplicitTlv<PersonalName>(new Asn1Tag(0xA0000005), this.personal_name, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.personal_name);
				});
			if (this.numeric_user_identifier is not null)
				encoder.EncodeExplicitTlv<NumericString>(new Asn1Tag(0xA0000004), this.numeric_user_identifier.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.numeric_user_identifier.Value);
				});
			if (this.organization_name is not null)
				encoder.EncodeExplicitTlv<PrintableString>(new Asn1Tag(0xA0000003), this.organization_name.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.organization_name.Value);
				});
			if (this.private_domain_name is not null)
				encoder.EncodeExplicitTlv<PrivateDomainName>(new Asn1Tag(0xA0000002), this.private_domain_name, (encoder, r) =>
				{
					this.private_domain_name.EncodeTlv(encoder);
				});
			if (this.terminal_identifier is not null)
				encoder.EncodeExplicitTlv<PrintableString>(new Asn1Tag(0xA0000001), this.terminal_identifier.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.terminal_identifier.Value);
				});
			if (this.network_address is not null)
				encoder.EncodeExplicitTlv<NumericString>(new Asn1Tag(0xA0000000), this.network_address.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.network_address.Value);
				});
			if (this.administration_domain_name is not null)
				encoder.EncodeExplicitTlv<AdministrationDomainName_Tagged2>(new Asn1Tag(0x60000002), this.administration_domain_name, (encoder, r) =>
				{
					this.administration_domain_name.EncodeTlv(encoder);
				});
			if (this.country_name is not null)
				encoder.EncodeExplicitTlv<CountryName_Tagged1>(new Asn1Tag(0x60000001), this.country_name, (encoder, r) =>
				{
					this.country_name.EncodeTlv(encoder);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BuiltInStandardAttributes DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new BuiltInStandardAttributes(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BuiltInStandardAttributes DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = BuiltInStandardAttributes.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out BuiltInStandardAttributes? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = BuiltInStandardAttributes.DecodeValueFrom(decoder);
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
		private BuiltInStandardAttributes(Asn1DerDecoder decoder)
		{
			this.country_name = decoder.CheckTag(new Asn1Tag(0x60000001)) ? decoder.DecodeTaggedValue<CountryName_Tagged1>(new Asn1Tag(0x60000001), (encoder) => decoder.DecodeTlv<CountryName_Tagged1>()) : default(CountryName_Tagged1);
			this.administration_domain_name = decoder.CheckTag(new Asn1Tag(0x60000002)) ? decoder.DecodeTaggedValue<AdministrationDomainName_Tagged2>(new Asn1Tag(0x60000002), (encoder) => decoder.DecodeTlv<AdministrationDomainName_Tagged2>()) : default(AdministrationDomainName_Tagged2);
			this.network_address = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeTaggedValue<NumericString>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeStringTlv<NumericString>()) : default(NumericString? );
			this.terminal_identifier = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<PrintableString>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeStringTlv<PrintableString>()) : default(PrintableString? );
			this.private_domain_name = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<PrivateDomainName>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeTlv<PrivateDomainName>()) : default(PrivateDomainName);
			this.organization_name = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<PrintableString>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeStringTlv<PrintableString>()) : default(PrintableString? );
			this.numeric_user_identifier = decoder.CheckTag(new Asn1Tag(0xA0000004)) ? decoder.DecodeTaggedValue<NumericString>(new Asn1Tag(0xA0000004), (encoder) => decoder.DecodeStringTlv<NumericString>()) : default(NumericString? );
			this.personal_name = decoder.CheckTag(new Asn1Tag(0xA0000005)) ? decoder.DecodeTaggedValue<PersonalName>(new Asn1Tag(0xA0000005), (encoder) => PersonalName.DecodeTlvFrom(decoder)) : default(PersonalName);
			this.organizational_unit_names = decoder.CheckTag(new Asn1Tag(0xA0000006)) ? decoder.DecodeTaggedValue<PrintableString[]>(new Asn1Tag(0xA0000006), (encoder) => decoder.DecodeListTlv<PrintableString>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeStringTlv<PrintableString>())) : default(PrintableString[]);
		}
	}

	[Asn1Sequence()]
	partial class BuiltInDomainDefinedAttribute : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<BuiltInDomainDefinedAttribute>, IAsn1DerDecodableValue<BuiltInDomainDefinedAttribute>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrintableString type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrintableString value;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BuiltInDomainDefinedAttribute(PrintableString type, PrintableString value)
		{
			this.type = type;
			this.value = value;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeStringTlv(this.value);
			encoder.EncodeStringTlv(this.type);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BuiltInDomainDefinedAttribute DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new BuiltInDomainDefinedAttribute(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BuiltInDomainDefinedAttribute DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = BuiltInDomainDefinedAttribute.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out BuiltInDomainDefinedAttribute? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = BuiltInDomainDefinedAttribute.DecodeValueFrom(decoder);
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
		private BuiltInDomainDefinedAttribute(Asn1DerDecoder decoder)
		{
			this.type = decoder.DecodeStringTlv<PrintableString>();
			this.value = decoder.DecodeStringTlv<PrintableString>();
		}
	}

	[Asn1Sequence()]
	partial class ExtensionAttribute : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ExtensionAttribute>, IAsn1DerDecodableValue<ExtensionAttribute>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ushort extension_attribute_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Any extension_attribute_value;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ExtensionAttribute(ushort extension_attribute_type, Asn1Any extension_attribute_value)
		{
			this.extension_attribute_type = extension_attribute_type;
			this.extension_attribute_value = extension_attribute_value;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Asn1Any>(new Asn1Tag(0xA0000001), this.extension_attribute_value, (encoder, r) =>
			{
				this.extension_attribute_value.EncodeTlv(encoder);
			});
			encoder.EncodeExplicitTlv<ushort>(new Asn1Tag(0xA0000000), this.extension_attribute_type, (encoder, r) =>
			{
				encoder.EncodeUInt16Tlv(this.extension_attribute_type);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExtensionAttribute DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ExtensionAttribute(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExtensionAttribute DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ExtensionAttribute.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ExtensionAttribute? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ExtensionAttribute.DecodeValueFrom(decoder);
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
		private ExtensionAttribute(Asn1DerDecoder decoder)
		{
			this.extension_attribute_type = decoder.DecodeTaggedValue<ushort>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsUInt16());
			this.extension_attribute_value = decoder.DecodeTaggedValue<Asn1Any>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<Asn1Any>());
		}
	}

	[Asn1Sequence()]
	partial class ORAddress : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ORAddress>, IAsn1DerDecodableValue<ORAddress>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal BuiltInStandardAttributes built_in_standard_attributes;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal BuiltInDomainDefinedAttribute[]? built_in_domain_defined_attributes;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ExtensionAttribute[]? extension_attributes;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ORAddress(BuiltInStandardAttributes built_in_standard_attributes, BuiltInDomainDefinedAttribute[]? built_in_domain_defined_attributes = default, ExtensionAttribute[]? extension_attributes = default)
		{
			this.built_in_standard_attributes = built_in_standard_attributes;
			this.built_in_domain_defined_attributes = built_in_domain_defined_attributes;
			this.extension_attributes = extension_attributes;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.extension_attributes is not null)
				encoder.EncodeListTlv(new Asn1Tag(0x20000011), this.extension_attributes, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			if (this.built_in_domain_defined_attributes is not null)
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.built_in_domain_defined_attributes, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			encoder.EncodeValueTlv(this.built_in_standard_attributes);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ORAddress DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ORAddress(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ORAddress DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ORAddress.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ORAddress? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ORAddress.DecodeValueFrom(decoder);
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
		private ORAddress(Asn1DerDecoder decoder)
		{
			this.built_in_standard_attributes = BuiltInStandardAttributes.DecodeTlvFrom(decoder);
			this.built_in_domain_defined_attributes = decoder.CheckTag(new Asn1Tag(0x20000010)) ? decoder.DecodeListTlv<BuiltInDomainDefinedAttribute>(new Asn1Tag(0x20000010), (encoder) => BuiltInDomainDefinedAttribute.DecodeTlvFrom(decoder)) : default(BuiltInDomainDefinedAttribute[]);
			this.extension_attributes = decoder.CheckTag(new Asn1Tag(0x20000011)) ? decoder.DecodeListTlv<ExtensionAttribute>(new Asn1Tag(0x20000011), (encoder) => ExtensionAttribute.DecodeTlvFrom(decoder)) : default(ExtensionAttribute[]);
		}
	}

	[Asn1Set()]
	partial class TeletexPersonalName : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<TeletexPersonalName>, IAsn1DerDecodableValue<TeletexPersonalName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal TeletexString surname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal TeletexString? given_name;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal TeletexString? initials;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal TeletexString? generation_qualifier;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TeletexPersonalName(TeletexString surname, TeletexString? given_name = default, TeletexString? initials = default, TeletexString? generation_qualifier = default)
		{
			this.surname = surname;
			this.given_name = given_name;
			this.initials = initials;
			this.generation_qualifier = generation_qualifier;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000011);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000011);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.generation_qualifier is not null)
				encoder.EncodeExplicitTlv<TeletexString>(new Asn1Tag(0xA0000003), this.generation_qualifier.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.generation_qualifier.Value);
				});
			if (this.initials is not null)
				encoder.EncodeExplicitTlv<TeletexString>(new Asn1Tag(0xA0000002), this.initials.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.initials.Value);
				});
			if (this.given_name is not null)
				encoder.EncodeExplicitTlv<TeletexString>(new Asn1Tag(0xA0000001), this.given_name.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.given_name.Value);
				});
			encoder.EncodeExplicitTlv<TeletexString>(new Asn1Tag(0xA0000000), this.surname, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.surname);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TeletexPersonalName DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new TeletexPersonalName(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TeletexPersonalName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000011));
			var instance = TeletexPersonalName.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out TeletexPersonalName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000011)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000011));
				instance = TeletexPersonalName.DecodeValueFrom(decoder);
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
		private TeletexPersonalName(Asn1DerDecoder decoder)
		{
			while (!decoder.IsEndOfTuple)
				if (decoder.CheckTag(new Asn1Tag(0xA0000000)))
				{
					this.surname = decoder.DecodeTaggedValue<TeletexString>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeStringTlv<TeletexString>());
				}
				else if (decoder.CheckTag(new Asn1Tag(0xA0000001)))
				{
					this.given_name = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<TeletexString>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeStringTlv<TeletexString>()) : default(TeletexString? );
				}
				else if (decoder.CheckTag(new Asn1Tag(0xA0000002)))
				{
					this.initials = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<TeletexString>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeStringTlv<TeletexString>()) : default(TeletexString? );
				}
				else if (decoder.CheckTag(new Asn1Tag(0xA0000003)))
				{
					this.generation_qualifier = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<TeletexString>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeStringTlv<TeletexString>()) : default(TeletexString? );
				}
				else
				{
				}
		}
	}

	[Asn1Choice()]
	partial class PhysicalDeliveryCountryName : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<PhysicalDeliveryCountryName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NumericString X121_dcc_code
		{
			get => this.x121_dcc_code.Value;
			set
			{
				this.x121_dcc_code = value;
				this._choiceTag = ChoiceIndex.X121_dcc_code;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private NumericString? x121_dcc_code;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrintableString Iso_3166_alpha2_code
		{
			get => this.iso_3166_alpha2_code.Value;
			set
			{
				this.iso_3166_alpha2_code = value;
				this._choiceTag = ChoiceIndex.Iso_3166_alpha2_code;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PrintableString? iso_3166_alpha2_code;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PhysicalDeliveryCountryName()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.Iso_3166_alpha2_code:
					Debug.Assert(this.iso_3166_alpha2_code is not null);
					encoder.EncodeStringTlv(this.iso_3166_alpha2_code.Value);
					break;
				case ChoiceIndex.X121_dcc_code:
					Debug.Assert(this.x121_dcc_code is not null);
					encoder.EncodeStringTlv(this.x121_dcc_code.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type PhysicalDeliveryCountryName has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PhysicalDeliveryCountryName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!PhysicalDeliveryCountryName.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PhysicalDeliveryCountryName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x12)))
				instance = new PhysicalDeliveryCountryName()
				{
					_choiceTag = ChoiceIndex.X121_dcc_code,
					x121_dcc_code = decoder.DecodeStringTlv<NumericString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x13)))
				instance = new PhysicalDeliveryCountryName()
				{
					_choiceTag = ChoiceIndex.Iso_3166_alpha2_code,
					iso_3166_alpha2_code = decoder.DecodeStringTlv<PrintableString>()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			X121_dcc_code = 18U,
			Iso_3166_alpha2_code = 19U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Choice()]
	partial class PostalCode : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<PostalCode>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NumericString Numeric_code
		{
			get => this.numeric_code.Value;
			set
			{
				this.numeric_code = value;
				this._choiceTag = ChoiceIndex.Numeric_code;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private NumericString? numeric_code;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrintableString Printable_code
		{
			get => this.printable_code.Value;
			set
			{
				this.printable_code = value;
				this._choiceTag = ChoiceIndex.Printable_code;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PrintableString? printable_code;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PostalCode()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.Printable_code:
					Debug.Assert(this.printable_code is not null);
					encoder.EncodeStringTlv(this.printable_code.Value);
					break;
				case ChoiceIndex.Numeric_code:
					Debug.Assert(this.numeric_code is not null);
					encoder.EncodeStringTlv(this.numeric_code.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type PostalCode has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PostalCode DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!PostalCode.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PostalCode? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x12)))
				instance = new PostalCode()
				{
					_choiceTag = ChoiceIndex.Numeric_code,
					numeric_code = decoder.DecodeStringTlv<NumericString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x13)))
				instance = new PostalCode()
				{
					_choiceTag = ChoiceIndex.Printable_code,
					printable_code = decoder.DecodeStringTlv<PrintableString>()
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			Numeric_code = 18U,
			Printable_code = 19U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Set()]
	partial class PDSParameter : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PDSParameter>, IAsn1DerDecodableValue<PDSParameter>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrintableString? printable_string;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal TeletexString? teletex_string;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PDSParameter(PrintableString? printable_string = default, TeletexString? teletex_string = default)
		{
			this.printable_string = printable_string;
			this.teletex_string = teletex_string;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000011);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000011);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.teletex_string is not null)
				encoder.EncodeStringTlv(this.teletex_string.Value);
			if (this.printable_string is not null)
				encoder.EncodeStringTlv(this.printable_string.Value);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PDSParameter DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PDSParameter(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PDSParameter DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000011));
			var instance = PDSParameter.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PDSParameter? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000011)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000011));
				instance = PDSParameter.DecodeValueFrom(decoder);
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
		private PDSParameter(Asn1DerDecoder decoder)
		{
			while (!decoder.IsEndOfTuple)
				if (decoder.CheckTag(new Asn1Tag(0x13)))
				{
					this.printable_string = decoder.CheckTag(new Asn1Tag(0x13)) ? decoder.DecodeStringTlv<PrintableString>() : default(PrintableString? );
				}
				else if (decoder.CheckTag(new Asn1Tag(0x14)))
				{
					this.teletex_string = decoder.CheckTag(new Asn1Tag(0x14)) ? decoder.DecodeStringTlv<TeletexString>() : default(TeletexString? );
				}
				else
				{
				}
		}
	}

	[Asn1Set()]
	partial class UnformattedPostalAddress : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<UnformattedPostalAddress>, IAsn1DerDecodableValue<UnformattedPostalAddress>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrintableString[]? printable_address;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal TeletexString? teletex_string;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public UnformattedPostalAddress(PrintableString[]? printable_address = default, TeletexString? teletex_string = default)
		{
			this.printable_address = printable_address;
			this.teletex_string = teletex_string;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000011);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000011);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.teletex_string is not null)
				encoder.EncodeStringTlv(this.teletex_string.Value);
			if (this.printable_address is not null)
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.printable_address, (encoder, r) =>
				{
					encoder.EncodeStringTlv(r);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static UnformattedPostalAddress DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new UnformattedPostalAddress(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static UnformattedPostalAddress DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000011));
			var instance = UnformattedPostalAddress.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out UnformattedPostalAddress? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000011)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000011));
				instance = UnformattedPostalAddress.DecodeValueFrom(decoder);
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
		private UnformattedPostalAddress(Asn1DerDecoder decoder)
		{
			while (!decoder.IsEndOfTuple)
				if (decoder.CheckTag(new Asn1Tag(0x20000010)))
				{
					this.printable_address = decoder.CheckTag(new Asn1Tag(0x20000010)) ? decoder.DecodeListTlv<PrintableString>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeStringTlv<PrintableString>()) : default(PrintableString[]);
				}
				else if (decoder.CheckTag(new Asn1Tag(0x14)))
				{
					this.teletex_string = decoder.CheckTag(new Asn1Tag(0x14)) ? decoder.DecodeStringTlv<TeletexString>() : default(TeletexString? );
				}
				else
				{
				}
		}
	}

	[Asn1Sequence()]
	partial class PresentationAddress : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PresentationAddress>, IAsn1DerDecodableValue<PresentationAddress>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? pSelector;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? sSelector;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? tSelector;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[][] nAddresses;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PresentationAddress(Byte[][] nAddresses, Byte[]? pSelector = default, Byte[]? sSelector = default, Byte[]? tSelector = default)
		{
			this.pSelector = pSelector;
			this.sSelector = sSelector;
			this.tSelector = tSelector;
			this.nAddresses = nAddresses;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Byte[][]>(new Asn1Tag(0xA0000003), this.nAddresses, (encoder, r) =>
			{
				encoder.EncodeListTlv(new Asn1Tag(0x20000011), this.nAddresses, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(r);
				});
			});
			if (this.tSelector is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000002), this.tSelector, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.tSelector);
				});
			if (this.sSelector is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000001), this.sSelector, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.sSelector);
				});
			if (this.pSelector is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000000), this.pSelector, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.pSelector);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PresentationAddress DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PresentationAddress(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PresentationAddress DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PresentationAddress.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PresentationAddress? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PresentationAddress.DecodeValueFrom(decoder);
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
		private PresentationAddress(Asn1DerDecoder decoder)
		{
			this.pSelector = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
			this.sSelector = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
			this.tSelector = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
			this.nAddresses = decoder.DecodeTaggedValue<Byte[][]>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeListTlv<Byte[]>(new Asn1Tag(0x20000011), (encoder) => decoder.DecodeOctetStringTlv()));
		}
	}

	[Asn1Choice()]
	partial class ExtendedNetworkAddress : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<ExtendedNetworkAddress>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ExtendedNetworkAddress_E163_4_address E163_4_address
		{
			get => this.e163_4_address;
			set
			{
				this.e163_4_address = value;
				this._choiceTag = ChoiceIndex.E163_4_address;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ExtendedNetworkAddress_E163_4_address? e163_4_address;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PresentationAddress Psap_address
		{
			get => this.psap_address;
			set
			{
				this.psap_address = value;
				this._choiceTag = ChoiceIndex.Psap_address;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PresentationAddress? psap_address;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ExtendedNetworkAddress()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.Psap_address:
					Debug.Assert(this.psap_address is not null);
					encoder.EncodeExplicitTlv<PresentationAddress>(new Asn1Tag(0xA0000000), this.psap_address, (encoder, r) =>
					{
						encoder.EncodeValueTlv(this.psap_address);
					});
					break;
				case ChoiceIndex.E163_4_address:
					Debug.Assert(this.e163_4_address is not null);
					encoder.EncodeValueTlv(this.e163_4_address);
					break;
				default:
					throw new InvalidOperationException("The object of type ExtendedNetworkAddress has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExtendedNetworkAddress DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!ExtendedNetworkAddress.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ExtendedNetworkAddress? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
				instance = new ExtendedNetworkAddress()
				{
					_choiceTag = ChoiceIndex.E163_4_address,
					e163_4_address = ExtendedNetworkAddress_E163_4_address.DecodeTlvFrom(decoder)
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000000)))
				instance = new ExtendedNetworkAddress()
				{
					_choiceTag = ChoiceIndex.Psap_address,
					psap_address = decoder.DecodeTaggedValue<PresentationAddress>(new Asn1Tag(0xA0000000), (encoder) => PresentationAddress.DecodeTlvFrom(decoder))
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			E163_4_address = 536870928U,
			Psap_address = 0xA0000000
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class ExtendedNetworkAddress_E163_4_address : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ExtendedNetworkAddress_E163_4_address>, IAsn1DerDecodableValue<ExtendedNetworkAddress_E163_4_address>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal NumericString number;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal NumericString? sub_address;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ExtendedNetworkAddress_E163_4_address(NumericString number, NumericString? sub_address = default)
		{
			this.number = number;
			this.sub_address = sub_address;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.sub_address is not null)
				encoder.EncodeExplicitTlv<NumericString>(new Asn1Tag(0xA0000001), this.sub_address.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.sub_address.Value);
				});
			encoder.EncodeExplicitTlv<NumericString>(new Asn1Tag(0xA0000000), this.number, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.number);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExtendedNetworkAddress_E163_4_address DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ExtendedNetworkAddress_E163_4_address(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExtendedNetworkAddress_E163_4_address DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ExtendedNetworkAddress_E163_4_address.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ExtendedNetworkAddress_E163_4_address? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ExtendedNetworkAddress_E163_4_address.DecodeValueFrom(decoder);
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
		private ExtendedNetworkAddress_E163_4_address(Asn1DerDecoder decoder)
		{
			this.number = decoder.DecodeTaggedValue<NumericString>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeStringTlv<NumericString>());
			this.sub_address = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<NumericString>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeStringTlv<NumericString>()) : default(NumericString? );
		}
	}

	[Asn1Sequence()]
	partial class TeletexDomainDefinedAttribute : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<TeletexDomainDefinedAttribute>, IAsn1DerDecodableValue<TeletexDomainDefinedAttribute>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal TeletexString type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal TeletexString value;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TeletexDomainDefinedAttribute(TeletexString type, TeletexString value)
		{
			this.type = type;
			this.value = value;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeStringTlv(this.value);
			encoder.EncodeStringTlv(this.type);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TeletexDomainDefinedAttribute DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new TeletexDomainDefinedAttribute(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TeletexDomainDefinedAttribute DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = TeletexDomainDefinedAttribute.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out TeletexDomainDefinedAttribute? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = TeletexDomainDefinedAttribute.DecodeValueFrom(decoder);
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
		private TeletexDomainDefinedAttribute(Asn1DerDecoder decoder)
		{
			this.type = decoder.DecodeStringTlv<TeletexString>();
			this.value = decoder.DecodeStringTlv<TeletexString>();
		}
	}
}