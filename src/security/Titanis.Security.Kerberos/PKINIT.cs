namespace KerberosV5_PK_INIT_SPEC
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
	using SubjectPublicKeyInfo = PKIX1Explicit88.SubjectPublicKeyInfo;
	using AlgorithmIdentifier = PKIX1Explicit88.AlgorithmIdentifier;
	using PrincipalName = KerberosV5Spec2.PrincipalName;
	using EncryptionKey = KerberosV5Spec2.EncryptionKey;
	using Checksum = KerberosV5Spec2.Checksum;

	partial class KerberosV5_PK_INIT_SPECModule
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pkinit => new Asn1Oid("1.3.6.1.5.2.3");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pkinit_authData => new Asn1Oid("1.3.6.1.5.2.3.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pkinit_DHKeyData => new Asn1Oid("1.3.6.1.5.2.3.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pkinit_rkeyData => new Asn1Oid("1.3.6.1.5.2.3.3");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pkinit_KPClientAuth => new Asn1Oid("1.3.6.1.5.2.3.4");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pkinit_KPKdc => new Asn1Oid("1.3.6.1.5.2.3.5");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pkinit_san => new Asn1Oid("1.3.6.1.5.2.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte pa_pk_as_req => 16;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte pa_pk_as_rep => 17;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte ad_initial_verified_cas => 9;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte td_trusted_certifiers => 104;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte td_invalid_certificates => 105;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte td_dh_parameters => 109;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private KerberosV5_PK_INIT_SPECModule()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private static KerberosV5_PK_INIT_SPECModule _instance = new KerberosV5_PK_INIT_SPECModule();
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KerberosV5_PK_INIT_SPECModule Instance => _instance;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Name => "KerberosV5-PK-INIT-SPEC";

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Oid => "1.3.6.1.5.2.4.5";
	}

	[Asn1Sequence()]
	partial class ExternalPrincipalIdentifier : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ExternalPrincipalIdentifier>, IAsn1DerDecodableValue<ExternalPrincipalIdentifier>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? subjectName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? issuerAndSerialNumber;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? subjectKeyIdentifier;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ExternalPrincipalIdentifier(Byte[]? subjectName = default, Byte[]? issuerAndSerialNumber = default, Byte[]? subjectKeyIdentifier = default)
		{
			this.subjectName = subjectName;
			this.issuerAndSerialNumber = issuerAndSerialNumber;
			this.subjectKeyIdentifier = subjectKeyIdentifier;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.subjectKeyIdentifier is not null)
				encoder.EncodeOctetStringTlv(this.subjectKeyIdentifier, new Asn1Tag(0x80000002));
			if (this.issuerAndSerialNumber is not null)
				encoder.EncodeOctetStringTlv(this.issuerAndSerialNumber, new Asn1Tag(0x80000001));
			if (this.subjectName is not null)
				encoder.EncodeOctetStringTlv(this.subjectName, new Asn1Tag(0x80000000));
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExternalPrincipalIdentifier DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ExternalPrincipalIdentifier(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExternalPrincipalIdentifier DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ExternalPrincipalIdentifier.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ExternalPrincipalIdentifier? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ExternalPrincipalIdentifier.DecodeValueFrom(decoder);
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
		private ExternalPrincipalIdentifier(Asn1DerDecoder decoder)
		{
			this.subjectName = decoder.CheckTag(new Asn1Tag(0x80000000)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000000)) : default(Byte[]);
			this.issuerAndSerialNumber = decoder.CheckTag(new Asn1Tag(0x80000001)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000001)) : default(Byte[]);
			this.subjectKeyIdentifier = decoder.CheckTag(new Asn1Tag(0x80000002)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000002)) : default(Byte[]);
		}
	}

	[Asn1Sequence()]
	partial class PA_PK_AS_REQ : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PA_PK_AS_REQ>, IAsn1DerDecodableValue<PA_PK_AS_REQ>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] signedAuthPack;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ExternalPrincipalIdentifier[]? trustedCertifiers;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? kdcPkId;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PA_PK_AS_REQ(Byte[] signedAuthPack, ExternalPrincipalIdentifier[]? trustedCertifiers = default, Byte[]? kdcPkId = default)
		{
			this.signedAuthPack = signedAuthPack;
			this.trustedCertifiers = trustedCertifiers;
			this.kdcPkId = kdcPkId;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.kdcPkId is not null)
				encoder.EncodeOctetStringTlv(this.kdcPkId, new Asn1Tag(0x80000002));
			if (this.trustedCertifiers is not null)
				encoder.EncodeExplicitTlv<ExternalPrincipalIdentifier[]>(new Asn1Tag(0xA0000001), this.trustedCertifiers, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.trustedCertifiers, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			encoder.EncodeOctetStringTlv(this.signedAuthPack, new Asn1Tag(0x80000000));
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_PK_AS_REQ DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PA_PK_AS_REQ(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_PK_AS_REQ DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PA_PK_AS_REQ.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PA_PK_AS_REQ? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PA_PK_AS_REQ.DecodeValueFrom(decoder);
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
		private PA_PK_AS_REQ(Asn1DerDecoder decoder)
		{
			this.signedAuthPack = decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000000));
			this.trustedCertifiers = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<ExternalPrincipalIdentifier[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeListTlv<ExternalPrincipalIdentifier>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<ExternalPrincipalIdentifier>())) : default(ExternalPrincipalIdentifier[]);
			this.kdcPkId = decoder.CheckTag(new Asn1Tag(0x80000002)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000002)) : default(Byte[]);
		}
	}

	// [MS-PKCA] § 2.2.3 PA-PK-AS-REQ
	partial class PKChecksum2 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PKChecksum2>, IAsn1DerDecodableValue<PKChecksum2>
	{
		internal byte[] checksum;
		internal AlgorithmIdentifier algorithmIdentifier;

		public PKChecksum2(byte[] checksum, AlgorithmIdentifier algorithmIdentifier)
		{
			this.checksum = checksum;
			this.algorithmIdentifier = algorithmIdentifier;
		}

		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv(new Asn1Tag(0xA0000001), this.algorithmIdentifier, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.algorithmIdentifier);
			});
			encoder.EncodeExplicitTlv(new Asn1Tag(0xA0000000), this.checksum, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.checksum);
			});
		}

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		public static PKChecksum2 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PKChecksum2(decoder);
			return instance;
		}

		public static PKChecksum2 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PKChecksum2.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PKChecksum2? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PKChecksum2.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		private PKChecksum2(Asn1DerDecoder decoder)
		{
			this.checksum = decoder.DecodeTaggedValue<byte[]>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeOctetStringTlv());
			this.algorithmIdentifier = decoder.DecodeTaggedValue<AlgorithmIdentifier>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<AlgorithmIdentifier>());
		}
	}

	[Asn1Sequence()]
	partial class PKAuthenticator : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PKAuthenticator>, IAsn1DerDecodableValue<PKAuthenticator>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint cusec;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime ctime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint nonce;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? paChecksum;

		internal byte[]? freshnessToken;
		internal PKChecksum2? checksum2;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PKAuthenticator(uint cusec, GeneralizedTime ctime, uint nonce, Byte[]? paChecksum = default, byte[]? freshnessToken = null, PKChecksum2? pkauth2 = null)
		{
			this.cusec = cusec;
			this.ctime = ctime;
			this.nonce = nonce;
			this.paChecksum = paChecksum;
			this.freshnessToken = freshnessToken;
			this.checksum2 = pkauth2;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.checksum2 is not null)
				encoder.EncodeExplicitTlv<PKChecksum2>(new Asn1Tag(0xA0000005), this.checksum2, (encoder, r) =>
				{
					encoder.EncodeValueTlv<PKChecksum2>(r);
				});
			if (this.freshnessToken is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000004), this.freshnessToken, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.freshnessToken);
				});
			if (this.paChecksum is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000003), this.paChecksum, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.paChecksum);
				});
			encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000002), this.nonce, (encoder, r) =>
			{
				encoder.EncodeUInt32Tlv(this.nonce);
			});
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000001), this.ctime, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.ctime);
			});
			encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000000), this.cusec, (encoder, r) =>
			{
				encoder.EncodeUInt32Tlv(this.cusec);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PKAuthenticator DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PKAuthenticator(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PKAuthenticator DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PKAuthenticator.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PKAuthenticator? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PKAuthenticator.DecodeValueFrom(decoder);
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
		private PKAuthenticator(Asn1DerDecoder decoder)
		{
			this.cusec = decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsUInt32());
			this.ctime = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeDateTimeTlv());
			this.nonce = decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeIntegerTlvAsUInt32());
			this.paChecksum = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
			this.freshnessToken = decoder.CheckTag(new Asn1Tag(0xA0000004)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000004), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
			this.checksum2 = decoder.CheckTag(new Asn1Tag(0xA0000005)) ? decoder.DecodeTaggedValue<PKChecksum2>(new Asn1Tag(0xA0000005), (encoder) => decoder.DecodeTlv<PKChecksum2>()) : default(PKChecksum2);
		}
	}

	[Asn1Sequence()]
	partial class AuthPack : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AuthPack>, IAsn1DerDecodableValue<AuthPack>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PKAuthenticator pkAuthenticator;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal SubjectPublicKeyInfo? clientPublicValue;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AlgorithmIdentifier[]? supportedCMSTypes;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? clientDHNonce;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AuthPack(PKAuthenticator pkAuthenticator, SubjectPublicKeyInfo? clientPublicValue = default, AlgorithmIdentifier[]? supportedCMSTypes = default, Byte[]? clientDHNonce = default)
		{
			this.pkAuthenticator = pkAuthenticator;
			this.clientPublicValue = clientPublicValue;
			this.supportedCMSTypes = supportedCMSTypes;
			this.clientDHNonce = clientDHNonce;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.clientDHNonce is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000003), this.clientDHNonce, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.clientDHNonce);
				});
			if (this.supportedCMSTypes is not null)
				encoder.EncodeExplicitTlv<AlgorithmIdentifier[]>(new Asn1Tag(0xA0000002), this.supportedCMSTypes, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.supportedCMSTypes, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			if (this.clientPublicValue is not null)
				encoder.EncodeExplicitTlv<SubjectPublicKeyInfo>(new Asn1Tag(0xA0000001), this.clientPublicValue, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.clientPublicValue);
				});
			encoder.EncodeExplicitTlv<PKAuthenticator>(new Asn1Tag(0xA0000000), this.pkAuthenticator, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.pkAuthenticator);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AuthPack DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AuthPack(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AuthPack DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AuthPack.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AuthPack? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AuthPack.DecodeValueFrom(decoder);
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
		private AuthPack(Asn1DerDecoder decoder)
		{
			this.pkAuthenticator = decoder.DecodeTaggedValue<PKAuthenticator>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<PKAuthenticator>());
			this.clientPublicValue = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<SubjectPublicKeyInfo>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<SubjectPublicKeyInfo>()) : default(SubjectPublicKeyInfo);
			this.supportedCMSTypes = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<AlgorithmIdentifier[]>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeListTlv<AlgorithmIdentifier>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<AlgorithmIdentifier>())) : default(AlgorithmIdentifier[]);
			this.clientDHNonce = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
		}
	}

	[Asn1Sequence()]
	partial class KRB5PrincipalName : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KRB5PrincipalName>, IAsn1DerDecodableValue<KRB5PrincipalName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString realm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName principalName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KRB5PrincipalName(GeneralString realm, PrincipalName principalName)
		{
			this.realm = realm;
			this.principalName = principalName;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000001), this.principalName, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.principalName);
			});
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000000), this.realm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.realm);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB5PrincipalName DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KRB5PrincipalName(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB5PrincipalName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KRB5PrincipalName.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KRB5PrincipalName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KRB5PrincipalName.DecodeValueFrom(decoder);
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
		private KRB5PrincipalName(Asn1DerDecoder decoder)
		{
			this.realm = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeStringTlv<GeneralString>());
			this.principalName = decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<PrincipalName>());
		}
	}

	[Asn1Sequence()]
	partial class DHRepInfo : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<DHRepInfo>, IAsn1DerDecodableValue<DHRepInfo>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] dhSignedData;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? serverDHNonce;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public DHRepInfo(Byte[] dhSignedData, Byte[]? serverDHNonce = default)
		{
			this.dhSignedData = dhSignedData;
			this.serverDHNonce = serverDHNonce;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.serverDHNonce is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000001), this.serverDHNonce, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.serverDHNonce);
				});
			encoder.EncodeOctetStringTlv(this.dhSignedData, new Asn1Tag(0x80000000));
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static DHRepInfo DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new DHRepInfo(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static DHRepInfo DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = DHRepInfo.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out DHRepInfo? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = DHRepInfo.DecodeValueFrom(decoder);
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
		private DHRepInfo(Asn1DerDecoder decoder)
		{
			this.dhSignedData = decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000000));
			this.serverDHNonce = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
		}
	}

	[Asn1Choice()]
	partial class PA_PK_AS_REP : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<PA_PK_AS_REP>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public DHRepInfo DhInfo
		{
			get => this.dhInfo;
			set
			{
				this.dhInfo = value;
				this._choiceTag = ChoiceIndex.DhInfo;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private DHRepInfo? dhInfo;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Byte[] EncKeyPack
		{
			get => this.encKeyPack;
			set
			{
				this.encKeyPack = value;
				this._choiceTag = ChoiceIndex.EncKeyPack;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Byte[]? encKeyPack;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PA_PK_AS_REP()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.EncKeyPack:
					Debug.Assert(this.encKeyPack is not null);
					encoder.EncodeOctetStringTlv(this.encKeyPack, new Asn1Tag(0x80000001));
					break;
				case ChoiceIndex.DhInfo:
					Debug.Assert(this.dhInfo is not null);
					encoder.EncodeExplicitTlv<DHRepInfo>(new Asn1Tag(0xA0000000), this.dhInfo, (encoder, r) =>
					{
						encoder.EncodeValueTlv(this.dhInfo);
					});
					break;
				default:
					throw new InvalidOperationException("The object of type PA-PK-AS-REP has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_PK_AS_REP DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!PA_PK_AS_REP.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PA_PK_AS_REP? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0xA0000000)))
				instance = new PA_PK_AS_REP()
				{
					_choiceTag = ChoiceIndex.DhInfo,
					dhInfo = decoder.DecodeTaggedValue<DHRepInfo>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<DHRepInfo>())
				};
			else if (decoder.CheckTag(new Asn1Tag(0x80000001)))
				instance = new PA_PK_AS_REP()
				{
					_choiceTag = ChoiceIndex.EncKeyPack,
					encKeyPack = decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000001))
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
			DhInfo = 0xA0000000,
			EncKeyPack = 0x80000001
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class KDCDHKeyInfo : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KDCDHKeyInfo>, IAsn1DerDecodableValue<KDCDHKeyInfo>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString subjectPublicKey;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint nonce;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? dhKeyExpiration;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KDCDHKeyInfo(Asn1BitString subjectPublicKey, uint nonce, GeneralizedTime? dhKeyExpiration = default)
		{
			this.subjectPublicKey = subjectPublicKey;
			this.nonce = nonce;
			this.dhKeyExpiration = dhKeyExpiration;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.dhKeyExpiration is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000002), this.dhKeyExpiration.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.dhKeyExpiration.Value);
				});
			encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000001), this.nonce, (encoder, r) =>
			{
				encoder.EncodeUInt32Tlv(this.nonce);
			});
			encoder.EncodeExplicitTlv<Asn1BitString>(new Asn1Tag(0xA0000000), this.subjectPublicKey, (encoder, r) =>
			{
				encoder.EncodeBitStringTlv(this.subjectPublicKey);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KDCDHKeyInfo DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KDCDHKeyInfo(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KDCDHKeyInfo DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KDCDHKeyInfo.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KDCDHKeyInfo? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KDCDHKeyInfo.DecodeValueFrom(decoder);
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
		private KDCDHKeyInfo(Asn1DerDecoder decoder)
		{
			this.subjectPublicKey = decoder.DecodeTaggedValue<Asn1BitString>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeBitStringTlv());
			this.nonce = decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsUInt32());
			this.dhKeyExpiration = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
		}
	}

	[Asn1Sequence()]
	partial class ReplyKeyPack : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ReplyKeyPack>, IAsn1DerDecodableValue<ReplyKeyPack>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptionKey replyKey;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Checksum asChecksum;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ReplyKeyPack(EncryptionKey replyKey, Checksum asChecksum)
		{
			this.replyKey = replyKey;
			this.asChecksum = asChecksum;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Checksum>(new Asn1Tag(0xA0000001), this.asChecksum, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.asChecksum);
			});
			encoder.EncodeExplicitTlv<EncryptionKey>(new Asn1Tag(0xA0000000), this.replyKey, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.replyKey);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ReplyKeyPack DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ReplyKeyPack(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ReplyKeyPack DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ReplyKeyPack.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ReplyKeyPack? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ReplyKeyPack.DecodeValueFrom(decoder);
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
		private ReplyKeyPack(Asn1DerDecoder decoder)
		{
			this.replyKey = decoder.DecodeTaggedValue<EncryptionKey>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<EncryptionKey>());
			this.asChecksum = decoder.DecodeTaggedValue<Checksum>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<Checksum>());
		}
	}
}