namespace PKINIT_Old
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
	using PrincipalName = KerberosV5Spec2.PrincipalName;
	using EncryptionKey = KerberosV5Spec2.EncryptionKey;
	using AlgorithmIdentifier = PKIX1Explicit88.AlgorithmIdentifier;

	partial class PKINIT_OldModule
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PKINIT_OldModule()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private static PKINIT_OldModule _instance = new PKINIT_OldModule();
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PKINIT_OldModule Instance => _instance;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Name => "PKINIT-Old";

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Oid => "";
	}

	[Asn1Sequence()]
	partial class PA_PK_AS_REQ : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PA_PK_AS_REQ>, IAsn1DerDecodableValue<PA_PK_AS_REQ>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] signedAuthPack;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PA_PK_AS_REQ(Byte[] signedAuthPack)
		{
			this.signedAuthPack = signedAuthPack;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
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
		}
	}

	[Asn1Sequence()]
	partial class PKAuthenticator : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PKAuthenticator>, IAsn1DerDecodableValue<PKAuthenticator>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName kdc_name;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString kdc_realm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger cusec;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime ctime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger nonce;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PKAuthenticator(PrincipalName kdc_name, GeneralString kdc_realm, System.Numerics.BigInteger cusec, GeneralizedTime ctime, System.Numerics.BigInteger nonce)
		{
			this.kdc_name = kdc_name;
			this.kdc_realm = kdc_realm;
			this.cusec = cusec;
			this.ctime = ctime;
			this.nonce = nonce;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<System.Numerics.BigInteger>(new Asn1Tag(0xA0000004), this.nonce, (encoder, r) =>
			{
				encoder.EncodeBigIntegerTlv(this.nonce);
			});
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000003), this.ctime, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.ctime);
			});
			encoder.EncodeExplicitTlv<System.Numerics.BigInteger>(new Asn1Tag(0xA0000002), this.cusec, (encoder, r) =>
			{
				encoder.EncodeBigIntegerTlv(this.cusec);
			});
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000001), this.kdc_realm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.kdc_realm);
			});
			encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000000), this.kdc_name, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.kdc_name);
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
			this.kdc_name = decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<PrincipalName>());
			this.kdc_realm = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeStringTlv<GeneralString>());
			this.cusec = decoder.DecodeTaggedValue<System.Numerics.BigInteger>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeIntegerTlvAsBigInteger());
			this.ctime = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeDateTimeTlv());
			this.nonce = decoder.DecodeTaggedValue<System.Numerics.BigInteger>(new Asn1Tag(0xA0000004), (encoder) => decoder.DecodeIntegerTlvAsBigInteger());
		}
	}

	[Asn1Sequence()]
	partial class AuthPack : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AuthPack>, IAsn1DerDecodableValue<AuthPack>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PKAuthenticator pkAuthenticator;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AuthPack(PKAuthenticator pkAuthenticator)
		{
			this.pkAuthenticator = pkAuthenticator;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
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
		}
	}

	[Asn1Sequence()]
	partial class KERB_REPLY_KEY_PACKAGE : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KERB_REPLY_KEY_PACKAGE>, IAsn1DerDecodableValue<KERB_REPLY_KEY_PACKAGE>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptionKey replyKey;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger nonce;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KERB_REPLY_KEY_PACKAGE(EncryptionKey replyKey, System.Numerics.BigInteger nonce)
		{
			this.replyKey = replyKey;
			this.nonce = nonce;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<System.Numerics.BigInteger>(new Asn1Tag(0xA0000001), this.nonce, (encoder, r) =>
			{
				encoder.EncodeBigIntegerTlv(this.nonce);
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
		public static KERB_REPLY_KEY_PACKAGE DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KERB_REPLY_KEY_PACKAGE(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KERB_REPLY_KEY_PACKAGE DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KERB_REPLY_KEY_PACKAGE.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KERB_REPLY_KEY_PACKAGE? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KERB_REPLY_KEY_PACKAGE.DecodeValueFrom(decoder);
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
		private KERB_REPLY_KEY_PACKAGE(Asn1DerDecoder decoder)
		{
			this.replyKey = decoder.DecodeTaggedValue<EncryptionKey>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<EncryptionKey>());
			this.nonce = decoder.DecodeTaggedValue<System.Numerics.BigInteger>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsBigInteger());
		}
	}

	[Asn1Sequence()]
	partial class PAChecksum2 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PAChecksum2>, IAsn1DerDecodableValue<PAChecksum2>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] checksum;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AlgorithmIdentifier algorithmIdentifier;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PAChecksum2(Byte[] checksum, AlgorithmIdentifier algorithmIdentifier)
		{
			this.checksum = checksum;
			this.algorithmIdentifier = algorithmIdentifier;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<AlgorithmIdentifier>(new Asn1Tag(0xA0000001), this.algorithmIdentifier, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.algorithmIdentifier);
			});
			encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000000), this.checksum, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.checksum);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PAChecksum2 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PAChecksum2(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PAChecksum2 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PAChecksum2.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PAChecksum2? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PAChecksum2.DecodeValueFrom(decoder);
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
		private PAChecksum2(Asn1DerDecoder decoder)
		{
			this.checksum = decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeOctetStringTlv());
			this.algorithmIdentifier = decoder.DecodeTaggedValue<AlgorithmIdentifier>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<AlgorithmIdentifier>());
		}
	}
}