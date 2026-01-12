namespace PKIX1Algorithms88
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

	partial class PKIX1Algorithms88Module
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid md2 => new Asn1Oid("1.2.840.113549.2.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid md5 => new Asn1Oid("1.2.840.113549.2.5");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_sha1 => new Asn1Oid("1.3.14.3.2.26");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_dsa => new Asn1Oid("1.2.840.10040.4.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_dsa_with_sha1 => new Asn1Oid("1.2.840.10040.4.3");

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
		public static Titanis.Asn1.Asn1Oid dhpublicnumber => new Asn1Oid("1.2.840.10046.2.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_keyExchangeAlgorithm => new Asn1Oid("2.16.840.1.101.2.1.1.22");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid ansi_X9_62 => new Asn1Oid("1.2.840.10045");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ecSigType => new Asn1Oid("1.2.840.10045.4");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid ecdsa_with_SHA1 => new Asn1Oid("1.2.840.10045.4.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_fieldType => new Asn1Oid("1.2.840.10045.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid prime_field => new Asn1Oid("1.2.840.10045.1.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid characteristic_two_field => new Asn1Oid("1.2.840.10045.1.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_characteristic_two_basis => new Asn1Oid("1.2.840.10045.1.2.3");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid gnBasis => new Asn1Oid("1.2.840.10045.1.2.3.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid tpBasis => new Asn1Oid("1.2.840.10045.1.2.3.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid ppBasis => new Asn1Oid("1.2.840.10045.1.2.3.3");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_publicKeyType => new Asn1Oid("1.2.840.10045.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ecPublicKey => new Asn1Oid("1.2.840.10045.2.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid ellipticCurve => new Asn1Oid("1.2.840.10045.3");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c_TwoCurve => new Asn1Oid("1.2.840.10045.3.0");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2pnb163v1 => new Asn1Oid("1.2.840.10045.3.0.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2pnb163v2 => new Asn1Oid("1.2.840.10045.3.0.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2pnb163v3 => new Asn1Oid("1.2.840.10045.3.0.3");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2pnb176w1 => new Asn1Oid("1.2.840.10045.3.0.4");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2tnb191v1 => new Asn1Oid("1.2.840.10045.3.0.5");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2tnb191v2 => new Asn1Oid("1.2.840.10045.3.0.6");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2tnb191v3 => new Asn1Oid("1.2.840.10045.3.0.7");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2onb191v4 => new Asn1Oid("1.2.840.10045.3.0.8");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2onb191v5 => new Asn1Oid("1.2.840.10045.3.0.9");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2pnb208w1 => new Asn1Oid("1.2.840.10045.3.0.10");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2tnb239v1 => new Asn1Oid("1.2.840.10045.3.0.11");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2tnb239v2 => new Asn1Oid("1.2.840.10045.3.0.12");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2tnb239v3 => new Asn1Oid("1.2.840.10045.3.0.13");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2onb239v4 => new Asn1Oid("1.2.840.10045.3.0.14");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2onb239v5 => new Asn1Oid("1.2.840.10045.3.0.15");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2pnb272w1 => new Asn1Oid("1.2.840.10045.3.0.16");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2pnb304w1 => new Asn1Oid("1.2.840.10045.3.0.17");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2tnb359v1 => new Asn1Oid("1.2.840.10045.3.0.18");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2pnb368w1 => new Asn1Oid("1.2.840.10045.3.0.19");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid c2tnb431r1 => new Asn1Oid("1.2.840.10045.3.0.20");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid primeCurve => new Asn1Oid("1.2.840.10045.3.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid prime192v1 => new Asn1Oid("1.2.840.10045.3.1.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid prime192v2 => new Asn1Oid("1.2.840.10045.3.1.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid prime192v3 => new Asn1Oid("1.2.840.10045.3.1.3");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid prime239v1 => new Asn1Oid("1.2.840.10045.3.1.4");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid prime239v2 => new Asn1Oid("1.2.840.10045.3.1.5");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid prime239v3 => new Asn1Oid("1.2.840.10045.3.1.6");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid prime256v1 => new Asn1Oid("1.2.840.10045.3.1.7");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ecpVer1 => 1;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PKIX1Algorithms88Module()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private static PKIX1Algorithms88Module _instance = new PKIX1Algorithms88Module();
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PKIX1Algorithms88Module Instance => _instance;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Name => "PKIX1Algorithms88";

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Oid => "1.3.6.1.5.5.7.0.17";
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
	partial class RSAPublicKey : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<RSAPublicKey>, IAsn1DerDecodableValue<RSAPublicKey>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger modulus;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger publicExponent;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public RSAPublicKey(System.Numerics.BigInteger modulus, System.Numerics.BigInteger publicExponent)
		{
			this.modulus = modulus;
			this.publicExponent = publicExponent;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBigIntegerTlv(this.publicExponent);
			encoder.EncodeBigIntegerTlv(this.modulus);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static RSAPublicKey DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new RSAPublicKey(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static RSAPublicKey DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = RSAPublicKey.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out RSAPublicKey? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = RSAPublicKey.DecodeValueFrom(decoder);
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
		private RSAPublicKey(Asn1DerDecoder decoder)
		{
			this.modulus = decoder.DecodeIntegerTlvAsBigInteger();
			this.publicExponent = decoder.DecodeIntegerTlvAsBigInteger();
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
			this.validationParms = decoder.CheckTag(new Asn1Tag(0x20000010)) ? decoder.DecodeTlv<ValidationParms>() : default(ValidationParms);
		}
	}

	[Asn1Sequence()]
	partial class FieldID : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<FieldID>, IAsn1DerDecodableValue<FieldID>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid fieldType;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Any parameters;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public FieldID(Asn1Oid fieldType, Asn1Any parameters)
		{
			this.fieldType = fieldType;
			this.parameters = parameters;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			this.parameters.EncodeTlv(encoder);
			encoder.EncodeOidTlv(this.fieldType);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static FieldID DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new FieldID(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static FieldID DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = FieldID.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out FieldID? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = FieldID.DecodeValueFrom(decoder);
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
		private FieldID(Asn1DerDecoder decoder)
		{
			this.fieldType = decoder.DecodeOidTlv();
			this.parameters = decoder.DecodeTlv<Asn1Any>();
		}
	}

	[Asn1Sequence()]
	partial class ECDSA_Sig_Value : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ECDSA_Sig_Value>, IAsn1DerDecodableValue<ECDSA_Sig_Value>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger r;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger s;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ECDSA_Sig_Value(System.Numerics.BigInteger r, System.Numerics.BigInteger s)
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
		public static ECDSA_Sig_Value DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ECDSA_Sig_Value(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ECDSA_Sig_Value DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ECDSA_Sig_Value.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ECDSA_Sig_Value? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ECDSA_Sig_Value.DecodeValueFrom(decoder);
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
		private ECDSA_Sig_Value(Asn1DerDecoder decoder)
		{
			this.r = decoder.DecodeIntegerTlvAsBigInteger();
			this.s = decoder.DecodeIntegerTlvAsBigInteger();
		}
	}

	[Asn1Sequence()]
	partial class Characteristic_two : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Characteristic_two>, IAsn1DerDecodableValue<Characteristic_two>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger m;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid basis;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Any parameters;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Characteristic_two(System.Numerics.BigInteger m, Asn1Oid basis, Asn1Any parameters)
		{
			this.m = m;
			this.basis = basis;
			this.parameters = parameters;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			this.parameters.EncodeTlv(encoder);
			encoder.EncodeOidTlv(this.basis);
			encoder.EncodeBigIntegerTlv(this.m);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Characteristic_two DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Characteristic_two(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Characteristic_two DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Characteristic_two.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Characteristic_two? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Characteristic_two.DecodeValueFrom(decoder);
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
		private Characteristic_two(Asn1DerDecoder decoder)
		{
			this.m = decoder.DecodeIntegerTlvAsBigInteger();
			this.basis = decoder.DecodeOidTlv();
			this.parameters = decoder.DecodeTlv<Asn1Any>();
		}
	}

	[Asn1Sequence()]
	partial class Pentanomial : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Pentanomial>, IAsn1DerDecodableValue<Pentanomial>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger k1;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger k2;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger k3;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Pentanomial(System.Numerics.BigInteger k1, System.Numerics.BigInteger k2, System.Numerics.BigInteger k3)
		{
			this.k1 = k1;
			this.k2 = k2;
			this.k3 = k3;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBigIntegerTlv(this.k3);
			encoder.EncodeBigIntegerTlv(this.k2);
			encoder.EncodeBigIntegerTlv(this.k1);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Pentanomial DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Pentanomial(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Pentanomial DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Pentanomial.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Pentanomial? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Pentanomial.DecodeValueFrom(decoder);
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
		private Pentanomial(Asn1DerDecoder decoder)
		{
			this.k1 = decoder.DecodeIntegerTlvAsBigInteger();
			this.k2 = decoder.DecodeIntegerTlvAsBigInteger();
			this.k3 = decoder.DecodeIntegerTlvAsBigInteger();
		}
	}

	[Asn1Sequence()]
	partial class Curve : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Curve>, IAsn1DerDecodableValue<Curve>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] a;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] b;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString? seed;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Curve(Byte[] a, Byte[] b, Asn1BitString? seed = default)
		{
			this.a = a;
			this.b = b;
			this.seed = seed;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.seed is not null)
				encoder.EncodeBitStringTlv(this.seed.Value);
			encoder.EncodeOctetStringTlv(this.b);
			encoder.EncodeOctetStringTlv(this.a);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Curve DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Curve(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Curve DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Curve.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Curve? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Curve.DecodeValueFrom(decoder);
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
		private Curve(Asn1DerDecoder decoder)
		{
			this.a = decoder.DecodeOctetStringTlv();
			this.b = decoder.DecodeOctetStringTlv();
			this.seed = decoder.CheckTag(new Asn1Tag(0x3)) ? decoder.DecodeBitStringTlv() : default(Asn1BitString? );
		}
	}

	[Asn1Sequence()]
	partial class ECParameters : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ECParameters>, IAsn1DerDecodableValue<ECParameters>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger version;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal FieldID fieldID;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Curve curve;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] @base;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger order;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger? cofactor;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ECParameters(System.Numerics.BigInteger version, FieldID fieldID, Curve curve, Byte[] @base, System.Numerics.BigInteger order, System.Numerics.BigInteger? cofactor = default)
		{
			this.version = version;
			this.fieldID = fieldID;
			this.curve = curve;
			this.@base = @base;
			this.order = order;
			this.cofactor = cofactor;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.cofactor is not null)
				encoder.EncodeBigIntegerTlv(this.cofactor.Value);
			encoder.EncodeBigIntegerTlv(this.order);
			encoder.EncodeOctetStringTlv(this.@base);
			encoder.EncodeValueTlv(this.curve);
			encoder.EncodeValueTlv(this.fieldID);
			encoder.EncodeBigIntegerTlv(this.version);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ECParameters DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ECParameters(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ECParameters DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ECParameters.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ECParameters? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ECParameters.DecodeValueFrom(decoder);
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
		private ECParameters(Asn1DerDecoder decoder)
		{
			this.version = decoder.DecodeIntegerTlvAsBigInteger();
			this.fieldID = decoder.DecodeTlv<FieldID>();
			this.curve = decoder.DecodeTlv<Curve>();
			this.@base = decoder.DecodeOctetStringTlv();
			this.order = decoder.DecodeIntegerTlvAsBigInteger();
			this.cofactor = decoder.CheckTag(new Asn1Tag(0x2)) ? decoder.DecodeIntegerTlvAsBigInteger() : default(System.Numerics.BigInteger? );
		}
	}

	[Asn1Choice()]
	partial class EcpkParameters : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<EcpkParameters>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ECParameters EcParameters
		{
			get => this.ecParameters;
			set
			{
				this.ecParameters = value;
				this._choiceTag = ChoiceIndex.EcParameters;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ECParameters? ecParameters;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Oid NamedCurve
		{
			get => this.namedCurve.Value;
			set
			{
				this.namedCurve = value;
				this._choiceTag = ChoiceIndex.NamedCurve;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Asn1Oid? namedCurve;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Null ImplicitlyCA
		{
			get => this.implicitlyCA.Value;
			set
			{
				this.implicitlyCA = value;
				this._choiceTag = ChoiceIndex.ImplicitlyCA;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Asn1Null? implicitlyCA;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EcpkParameters()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.ImplicitlyCA:
					Debug.Assert(this.implicitlyCA is not null);
					encoder.EncodeNullTlv(this.implicitlyCA.Value);
					break;
				case ChoiceIndex.NamedCurve:
					Debug.Assert(this.namedCurve is not null);
					encoder.EncodeOidTlv(this.namedCurve.Value);
					break;
				case ChoiceIndex.EcParameters:
					Debug.Assert(this.ecParameters is not null);
					encoder.EncodeValueTlv(this.ecParameters);
					break;
				default:
					throw new InvalidOperationException("The object of type EcpkParameters has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EcpkParameters DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!EcpkParameters.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EcpkParameters? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
				instance = new EcpkParameters()
				{
					_choiceTag = ChoiceIndex.EcParameters,
					ecParameters = decoder.DecodeTlv<ECParameters>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x6)))
				instance = new EcpkParameters()
				{
					_choiceTag = ChoiceIndex.NamedCurve,
					namedCurve = decoder.DecodeOidTlv()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x5)))
				instance = new EcpkParameters()
				{
					_choiceTag = ChoiceIndex.ImplicitlyCA,
					implicitlyCA = decoder.DecodeNullTlv()
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
			EcParameters = 536870928U,
			NamedCurve = 6U,
			ImplicitlyCA = 5U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}
}