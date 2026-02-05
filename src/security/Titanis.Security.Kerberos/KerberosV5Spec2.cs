namespace KerberosV5Spec2
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

	partial class KerberosV5Spec2Module
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_krb5 => new Asn1Oid("1.3.6.1.5.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private KerberosV5Spec2Module()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private static KerberosV5Spec2Module _instance = new KerberosV5Spec2Module();
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KerberosV5Spec2Module Instance => _instance;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Name => "KerberosV5Spec2";

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Oid => "1.3.6.1.5.2.4.2";
	}

	[Asn1Sequence()]
	partial class PrincipalName : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PrincipalName>, IAsn1DerDecodableValue<PrincipalName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int name_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString[] name_string;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrincipalName(int name_type, GeneralString[] name_string)
		{
			this.name_type = name_type;
			this.name_string = name_string;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<GeneralString[]>(new Asn1Tag(0xA0000001), this.name_string, (encoder, r) =>
			{
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.name_string, (encoder, r) =>
				{
					encoder.EncodeStringTlv(r);
				});
			});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.name_type, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.name_type);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PrincipalName DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PrincipalName(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PrincipalName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PrincipalName.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PrincipalName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PrincipalName.DecodeValueFrom(decoder);
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
		private PrincipalName(Asn1DerDecoder decoder)
		{
			this.name_type = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.name_string = decoder.DecodeTaggedValue<GeneralString[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeListTlv<GeneralString>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeStringTlv<GeneralString>()));
		}
	}

	[Asn1Sequence()]
	partial class HostAddress : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<HostAddress>, IAsn1DerDecodableValue<HostAddress>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int addr_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] address;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public HostAddress(int addr_type, Byte[] address)
		{
			this.addr_type = addr_type;
			this.address = address;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000001), this.address, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.address);
			});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.addr_type, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.addr_type);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static HostAddress DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new HostAddress(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static HostAddress DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = HostAddress.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out HostAddress? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = HostAddress.DecodeValueFrom(decoder);
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
		private HostAddress(Asn1DerDecoder decoder)
		{
			this.addr_type = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.address = decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOctetStringTlv());
		}
	}

	[Asn1Sequence()]
	partial class AuthorizationData_Element : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AuthorizationData_Element>, IAsn1DerDecodableValue<AuthorizationData_Element>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int ad_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] ad_data;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AuthorizationData_Element(int ad_type, Byte[] ad_data)
		{
			this.ad_type = ad_type;
			this.ad_data = ad_data;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000001), this.ad_data, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.ad_data);
			});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.ad_type, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.ad_type);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AuthorizationData_Element DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AuthorizationData_Element(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AuthorizationData_Element DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AuthorizationData_Element.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AuthorizationData_Element? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AuthorizationData_Element.DecodeValueFrom(decoder);
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
		private AuthorizationData_Element(Asn1DerDecoder decoder)
		{
			this.ad_type = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.ad_data = decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOctetStringTlv());
		}
	}

	[Asn1Sequence()]
	partial class PA_DATA : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PA_DATA>, IAsn1DerDecodableValue<PA_DATA>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int padata_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] padata_value;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PA_DATA(int padata_type, Byte[] padata_value)
		{
			this.padata_type = padata_type;
			this.padata_value = padata_value;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000002), this.padata_value, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.padata_value);
			});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000001), this.padata_type, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.padata_type);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_DATA DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PA_DATA(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_DATA DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PA_DATA.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PA_DATA? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PA_DATA.DecodeValueFrom(decoder);
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
		private PA_DATA(Asn1DerDecoder decoder)
		{
			this.padata_type = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.padata_value = decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeOctetStringTlv());
		}
	}

	[Asn1Sequence()]
	partial class EncryptedData : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<EncryptedData>, IAsn1DerDecodableValue<EncryptedData>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int etype;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint? kvno;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] cipher;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EncryptedData(int etype, Byte[] cipher, uint? kvno = default)
		{
			this.etype = etype;
			this.kvno = kvno;
			this.cipher = cipher;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000002), this.cipher, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.cipher);
			});
			if (this.kvno is not null)
				encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000001), this.kvno.Value, (encoder, r) =>
				{
					encoder.EncodeUInt32Tlv(this.kvno.Value);
				});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.etype, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.etype);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncryptedData DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new EncryptedData(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncryptedData DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = EncryptedData.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EncryptedData? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = EncryptedData.DecodeValueFrom(decoder);
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
		private EncryptedData(Asn1DerDecoder decoder)
		{
			this.etype = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.kvno = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsUInt32()) : default(uint?);
			this.cipher = decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeOctetStringTlv());
		}
	}

	[Asn1Sequence()]
	partial class EncryptionKey : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<EncryptionKey>, IAsn1DerDecodableValue<EncryptionKey>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int keytype;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] keyvalue;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EncryptionKey(int keytype, Byte[] keyvalue)
		{
			this.keytype = keytype;
			this.keyvalue = keyvalue;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000001), this.keyvalue, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.keyvalue);
			});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.keytype, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.keytype);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncryptionKey DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new EncryptionKey(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncryptionKey DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = EncryptionKey.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EncryptionKey? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = EncryptionKey.DecodeValueFrom(decoder);
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
		private EncryptionKey(Asn1DerDecoder decoder)
		{
			this.keytype = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.keyvalue = decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOctetStringTlv());
		}
	}

	[Asn1Sequence()]
	partial class Checksum : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Checksum>, IAsn1DerDecodableValue<Checksum>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int cksumtype;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] checksum;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Checksum(int cksumtype, Byte[] checksum)
		{
			this.cksumtype = cksumtype;
			this.checksum = checksum;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000001), this.checksum, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.checksum);
			});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.cksumtype, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.cksumtype);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Checksum DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Checksum(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Checksum DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Checksum.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Checksum? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Checksum.DecodeValueFrom(decoder);
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
		private Checksum(Asn1DerDecoder decoder)
		{
			this.cksumtype = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.checksum = decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOctetStringTlv());
		}
	}

	partial class Ticket : Asn1Explicit<Ticket_Tagged1>, IAsn1DerDecodableTlv<Ticket>, IAsn1DerDecodableValue<Ticket>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Ticket(Ticket_Tagged1 value) : base(new Asn1Tag(0x60000001), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Ticket DecodeValueFrom(Asn1DerDecoder decoder) => new Ticket(decoder.DecodeTlv<Ticket_Tagged1>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Ticket DecodeTlvFrom(Asn1DerDecoder decoder) => new Ticket(decoder.DecodeExplicitTaggedTlv<Ticket_Tagged1>(new Asn1Tag(0x60000001)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Ticket? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<Ticket_Tagged1>(new Asn1Tag(0x60000001), out var inner))
			{
				value = new Ticket(inner);
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
	partial class Ticket_Tagged1 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Ticket_Tagged1>, IAsn1DerDecodableValue<Ticket_Tagged1>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte tkt_vno;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString realm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName sname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptedData enc_part;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Ticket_Tagged1(byte tkt_vno, GeneralString realm, PrincipalName sname, EncryptedData enc_part)
		{
			this.tkt_vno = tkt_vno;
			this.realm = realm;
			this.sname = sname;
			this.enc_part = enc_part;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<EncryptedData>(new Asn1Tag(0xA0000003), this.enc_part, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.enc_part);
			});
			encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000002), this.sname, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.sname);
			});
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000001), this.realm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.realm);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000000), this.tkt_vno, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.tkt_vno);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Ticket_Tagged1 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Ticket_Tagged1(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Ticket_Tagged1 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Ticket_Tagged1.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Ticket_Tagged1? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Ticket_Tagged1.DecodeValueFrom(decoder);
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
		private Ticket_Tagged1(Asn1DerDecoder decoder)
		{
			this.tkt_vno = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.realm = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeStringTlv<GeneralString>());
			this.sname = decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000002), (encoder) => PrincipalName.DecodeTlvFrom(decoder));
			this.enc_part = decoder.DecodeTaggedValue<EncryptedData>(new Asn1Tag(0xA0000003), (encoder) => EncryptedData.DecodeTlvFrom(decoder));
		}
	}

	[Asn1Sequence()]
	partial class TransitedEncoding : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<TransitedEncoding>, IAsn1DerDecodableValue<TransitedEncoding>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int tr_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] contents;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TransitedEncoding(int tr_type, Byte[] contents)
		{
			this.tr_type = tr_type;
			this.contents = contents;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000001), this.contents, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.contents);
			});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.tr_type, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.tr_type);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TransitedEncoding DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new TransitedEncoding(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TransitedEncoding DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = TransitedEncoding.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out TransitedEncoding? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = TransitedEncoding.DecodeValueFrom(decoder);
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
		private TransitedEncoding(Asn1DerDecoder decoder)
		{
			this.tr_type = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.contents = decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOctetStringTlv());
		}
	}

	partial class EncTicketPart : Asn1Explicit<EncTicketPart_Tagged3>, IAsn1DerDecodableTlv<EncTicketPart>, IAsn1DerDecodableValue<EncTicketPart>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EncTicketPart(EncTicketPart_Tagged3 value) : base(new Asn1Tag(0x60000003), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncTicketPart DecodeValueFrom(Asn1DerDecoder decoder) => new EncTicketPart(decoder.DecodeTlv<EncTicketPart_Tagged3>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncTicketPart DecodeTlvFrom(Asn1DerDecoder decoder) => new EncTicketPart(decoder.DecodeExplicitTaggedTlv<EncTicketPart_Tagged3>(new Asn1Tag(0x60000003)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EncTicketPart? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<EncTicketPart_Tagged3>(new Asn1Tag(0x60000003), out var inner))
			{
				value = new EncTicketPart(inner);
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
	partial class EncTicketPart_Tagged3 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<EncTicketPart_Tagged3>, IAsn1DerDecodableValue<EncTicketPart_Tagged3>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString flags;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptionKey key;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString crealm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName cname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal TransitedEncoding transited;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime authtime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? starttime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime endtime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? renew_till;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal HostAddress[]? caddr;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AuthorizationData_Element[]? authorization_data;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EncTicketPart_Tagged3(Asn1BitString flags, EncryptionKey key, GeneralString crealm, PrincipalName cname, TransitedEncoding transited, GeneralizedTime authtime, GeneralizedTime endtime, GeneralizedTime? starttime = default, GeneralizedTime? renew_till = default, HostAddress[]? caddr = default, AuthorizationData_Element[]? authorization_data = default)
		{
			this.flags = flags;
			this.key = key;
			this.crealm = crealm;
			this.cname = cname;
			this.transited = transited;
			this.authtime = authtime;
			this.starttime = starttime;
			this.endtime = endtime;
			this.renew_till = renew_till;
			this.caddr = caddr;
			this.authorization_data = authorization_data;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.authorization_data is not null)
				encoder.EncodeExplicitTlv<AuthorizationData_Element[]>(new Asn1Tag(0xA000000A), this.authorization_data, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.authorization_data, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			if (this.caddr is not null)
				encoder.EncodeExplicitTlv<HostAddress[]>(new Asn1Tag(0xA0000009), this.caddr, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.caddr, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			if (this.renew_till is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000008), this.renew_till.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.renew_till.Value);
				});
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000007), this.endtime, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.endtime);
			});
			if (this.starttime is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000006), this.starttime.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.starttime.Value);
				});
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000005), this.authtime, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.authtime);
			});
			encoder.EncodeExplicitTlv<TransitedEncoding>(new Asn1Tag(0xA0000004), this.transited, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.transited);
			});
			encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000003), this.cname, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.cname);
			});
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000002), this.crealm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.crealm);
			});
			encoder.EncodeExplicitTlv<EncryptionKey>(new Asn1Tag(0xA0000001), this.key, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.key);
			});
			encoder.EncodeExplicitTlv<Asn1BitString>(new Asn1Tag(0xA0000000), this.flags, (encoder, r) =>
			{
				encoder.EncodeBitStringTlv(this.flags);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncTicketPart_Tagged3 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new EncTicketPart_Tagged3(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncTicketPart_Tagged3 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = EncTicketPart_Tagged3.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EncTicketPart_Tagged3? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = EncTicketPart_Tagged3.DecodeValueFrom(decoder);
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
		private EncTicketPart_Tagged3(Asn1DerDecoder decoder)
		{
			this.flags = decoder.DecodeTaggedValue<Asn1BitString>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeBitStringTlv());
			this.key = decoder.DecodeTaggedValue<EncryptionKey>(new Asn1Tag(0xA0000001), (encoder) => EncryptionKey.DecodeTlvFrom(decoder));
			this.crealm = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeStringTlv<GeneralString>());
			this.cname = decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000003), (encoder) => PrincipalName.DecodeTlvFrom(decoder));
			this.transited = decoder.DecodeTaggedValue<TransitedEncoding>(new Asn1Tag(0xA0000004), (encoder) => TransitedEncoding.DecodeTlvFrom(decoder));
			this.authtime = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000005), (encoder) => decoder.DecodeDateTimeTlv());
			this.starttime = decoder.CheckTag(new Asn1Tag(0xA0000006)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000006), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.endtime = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000007), (encoder) => decoder.DecodeDateTimeTlv());
			this.renew_till = decoder.CheckTag(new Asn1Tag(0xA0000008)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000008), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.caddr = decoder.CheckTag(new Asn1Tag(0xA0000009)) ? decoder.DecodeTaggedValue<HostAddress[]>(new Asn1Tag(0xA0000009), (encoder) => decoder.DecodeListTlv<HostAddress>(new Asn1Tag(0x20000010), (encoder) => HostAddress.DecodeTlvFrom(decoder))) : default(HostAddress[]);
			this.authorization_data = decoder.CheckTag(new Asn1Tag(0xA000000A)) ? decoder.DecodeTaggedValue<AuthorizationData_Element[]>(new Asn1Tag(0xA000000A), (encoder) => decoder.DecodeListTlv<AuthorizationData_Element>(new Asn1Tag(0x20000010), (encoder) => AuthorizationData_Element.DecodeTlvFrom(decoder))) : default(AuthorizationData_Element[]);
		}
	}

	[Asn1Sequence()]
	partial class KDC_REQ_BODY : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KDC_REQ_BODY>, IAsn1DerDecodableValue<KDC_REQ_BODY>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString kdc_options;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName? cname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString realm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName? sname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? from;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime till;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? rtime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int nonce;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int[] etype;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal HostAddress[]? addresses;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptedData? enc_authorization_data;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Ticket_Tagged1[]? additional_tickets;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KDC_REQ_BODY(Asn1BitString kdc_options, GeneralString realm, GeneralizedTime till, int nonce, int[] etype, PrincipalName? cname = default, PrincipalName? sname = default, GeneralizedTime? from = default, GeneralizedTime? rtime = default, HostAddress[]? addresses = default, EncryptedData? enc_authorization_data = default, Ticket_Tagged1[]? additional_tickets = default)
		{
			this.kdc_options = kdc_options;
			this.cname = cname;
			this.realm = realm;
			this.sname = sname;
			this.from = from;
			this.till = till;
			this.rtime = rtime;
			this.nonce = nonce;
			this.etype = etype;
			this.addresses = addresses;
			this.enc_authorization_data = enc_authorization_data;
			this.additional_tickets = additional_tickets;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.additional_tickets is not null)
				encoder.EncodeExplicitTlv<Ticket_Tagged1[]>(new Asn1Tag(0xA000000B), this.additional_tickets, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.additional_tickets, (encoder, r) =>
					{
						encoder.EncodeExplicitTlv<Ticket_Tagged1>(new Asn1Tag(0x60000001), r, (encoder, r) =>
						{
							encoder.EncodeValueTlv(r);
						});
					});
				});
			if (this.enc_authorization_data is not null)
				encoder.EncodeExplicitTlv<EncryptedData>(new Asn1Tag(0xA000000A), this.enc_authorization_data, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.enc_authorization_data);
				});
			if (this.addresses is not null)
				encoder.EncodeExplicitTlv<HostAddress[]>(new Asn1Tag(0xA0000009), this.addresses, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.addresses, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			encoder.EncodeExplicitTlv<int[]>(new Asn1Tag(0xA0000008), this.etype, (encoder, r) =>
			{
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.etype, (encoder, r) =>
				{
					encoder.EncodeInt32Tlv(r);
				});
			});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000007), this.nonce, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.nonce);
			});
			if (this.rtime is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000006), this.rtime.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.rtime.Value);
				});
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000005), this.till, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.till);
			});
			if (this.from is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000004), this.from.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.from.Value);
				});
			if (this.sname is not null)
				encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000003), this.sname, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.sname);
				});
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000002), this.realm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.realm);
			});
			if (this.cname is not null)
				encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000001), this.cname, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.cname);
				});
			encoder.EncodeExplicitTlv<Asn1BitString>(new Asn1Tag(0xA0000000), this.kdc_options, (encoder, r) =>
			{
				encoder.EncodeBitStringTlv(this.kdc_options);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KDC_REQ_BODY DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KDC_REQ_BODY(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KDC_REQ_BODY DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KDC_REQ_BODY.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KDC_REQ_BODY? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KDC_REQ_BODY.DecodeValueFrom(decoder);
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
		private KDC_REQ_BODY(Asn1DerDecoder decoder)
		{
			this.kdc_options = decoder.DecodeTaggedValue<Asn1BitString>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeBitStringTlv());
			this.cname = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000001), (encoder) => PrincipalName.DecodeTlvFrom(decoder)) : default(PrincipalName);
			this.realm = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeStringTlv<GeneralString>());
			this.sname = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000003), (encoder) => PrincipalName.DecodeTlvFrom(decoder)) : default(PrincipalName);
			this.from = decoder.CheckTag(new Asn1Tag(0xA0000004)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000004), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.till = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000005), (encoder) => decoder.DecodeDateTimeTlv());
			this.rtime = decoder.CheckTag(new Asn1Tag(0xA0000006)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000006), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.nonce = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000007), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.etype = decoder.DecodeTaggedValue<int[]>(new Asn1Tag(0xA0000008), (encoder) => decoder.DecodeListTlv<int>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeIntegerTlvAsInt32()));
			this.addresses = decoder.CheckTag(new Asn1Tag(0xA0000009)) ? decoder.DecodeTaggedValue<HostAddress[]>(new Asn1Tag(0xA0000009), (encoder) => decoder.DecodeListTlv<HostAddress>(new Asn1Tag(0x20000010), (encoder) => HostAddress.DecodeTlvFrom(decoder))) : default(HostAddress[]);
			this.enc_authorization_data = decoder.CheckTag(new Asn1Tag(0xA000000A)) ? decoder.DecodeTaggedValue<EncryptedData>(new Asn1Tag(0xA000000A), (encoder) => EncryptedData.DecodeTlvFrom(decoder)) : default(EncryptedData);
			this.additional_tickets = decoder.CheckTag(new Asn1Tag(0xA000000B)) ? decoder.DecodeTaggedValue<Ticket_Tagged1[]>(new Asn1Tag(0xA000000B), (encoder) => decoder.DecodeListTlv<Ticket_Tagged1>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTaggedValue<Ticket_Tagged1>(new Asn1Tag(0x60000001), (encoder) => Ticket_Tagged1.DecodeTlvFrom(decoder)))) : default(Ticket_Tagged1[]);
		}
	}

	[Asn1Sequence()]
	partial class KDC_REQ : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KDC_REQ>, IAsn1DerDecodableValue<KDC_REQ>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte pvno;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte msg_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PA_DATA[]? padata;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal KDC_REQ_BODY req_body;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KDC_REQ(byte pvno, byte msg_type, KDC_REQ_BODY req_body, PA_DATA[]? padata = default)
		{
			this.pvno = pvno;
			this.msg_type = msg_type;
			this.padata = padata;
			this.req_body = req_body;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<KDC_REQ_BODY>(new Asn1Tag(0xA0000004), this.req_body, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.req_body);
			});
			if (this.padata is not null)
				encoder.EncodeExplicitTlv<PA_DATA[]>(new Asn1Tag(0xA0000003), this.padata, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.padata, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000002), this.msg_type, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.msg_type);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000001), this.pvno, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.pvno);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KDC_REQ DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KDC_REQ(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KDC_REQ DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KDC_REQ.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KDC_REQ? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KDC_REQ.DecodeValueFrom(decoder);
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
		private KDC_REQ(Asn1DerDecoder decoder)
		{
			this.pvno = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.msg_type = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.padata = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<PA_DATA[]>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeListTlv<PA_DATA>(new Asn1Tag(0x20000010), (encoder) => PA_DATA.DecodeTlvFrom(decoder))) : default(PA_DATA[]);
			this.req_body = decoder.DecodeTaggedValue<KDC_REQ_BODY>(new Asn1Tag(0xA0000004), (encoder) => KDC_REQ_BODY.DecodeTlvFrom(decoder));
		}
	}

	partial class AS_REQ : Asn1Explicit<KDC_REQ>, IAsn1DerDecodableTlv<AS_REQ>, IAsn1DerDecodableValue<AS_REQ>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AS_REQ(KDC_REQ value) : base(new Asn1Tag(0x6000000A), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AS_REQ DecodeValueFrom(Asn1DerDecoder decoder) => new AS_REQ(decoder.DecodeTlv<KDC_REQ>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AS_REQ DecodeTlvFrom(Asn1DerDecoder decoder) => new AS_REQ(decoder.DecodeExplicitTaggedTlv<KDC_REQ>(new Asn1Tag(0x6000000A)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AS_REQ? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<KDC_REQ>(new Asn1Tag(0x6000000A), out var inner))
			{
				value = new AS_REQ(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	partial class TGS_REQ : Asn1Explicit<KDC_REQ>, IAsn1DerDecodableTlv<TGS_REQ>, IAsn1DerDecodableValue<TGS_REQ>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TGS_REQ(KDC_REQ value) : base(new Asn1Tag(0x6000000C), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TGS_REQ DecodeValueFrom(Asn1DerDecoder decoder) => new TGS_REQ(decoder.DecodeTlv<KDC_REQ>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TGS_REQ DecodeTlvFrom(Asn1DerDecoder decoder) => new TGS_REQ(decoder.DecodeExplicitTaggedTlv<KDC_REQ>(new Asn1Tag(0x6000000C)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out TGS_REQ? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<KDC_REQ>(new Asn1Tag(0x6000000C), out var inner))
			{
				value = new TGS_REQ(inner);
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
	partial class KDC_REP : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KDC_REP>, IAsn1DerDecodableValue<KDC_REP>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte pvno;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte msg_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PA_DATA[]? padata;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString crealm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName cname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Ticket_Tagged1 ticket;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptedData enc_part;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KDC_REP(byte pvno, byte msg_type, GeneralString crealm, PrincipalName cname, Ticket_Tagged1 ticket, EncryptedData enc_part, PA_DATA[]? padata = default)
		{
			this.pvno = pvno;
			this.msg_type = msg_type;
			this.padata = padata;
			this.crealm = crealm;
			this.cname = cname;
			this.ticket = ticket;
			this.enc_part = enc_part;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<EncryptedData>(new Asn1Tag(0xA0000006), this.enc_part, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.enc_part);
			});
			encoder.EncodeExplicitTlv<Ticket_Tagged1>(new Asn1Tag(0xA0000005), this.ticket, (encoder, r) =>
			{
				encoder.EncodeExplicitTlv<Ticket_Tagged1>(new Asn1Tag(0x60000001), this.ticket, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.ticket);
				});
			});
			encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000004), this.cname, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.cname);
			});
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000003), this.crealm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.crealm);
			});
			if (this.padata is not null)
				encoder.EncodeExplicitTlv<PA_DATA[]>(new Asn1Tag(0xA0000002), this.padata, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.padata, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000001), this.msg_type, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.msg_type);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000000), this.pvno, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.pvno);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KDC_REP DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KDC_REP(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KDC_REP DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KDC_REP.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KDC_REP? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KDC_REP.DecodeValueFrom(decoder);
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
		private KDC_REP(Asn1DerDecoder decoder)
		{
			this.pvno = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.msg_type = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.padata = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<PA_DATA[]>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeListTlv<PA_DATA>(new Asn1Tag(0x20000010), (encoder) => PA_DATA.DecodeTlvFrom(decoder))) : default(PA_DATA[]);
			this.crealm = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeStringTlv<GeneralString>());
			this.cname = decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000004), (encoder) => PrincipalName.DecodeTlvFrom(decoder));
			this.ticket = decoder.DecodeTaggedValue<Ticket_Tagged1>(new Asn1Tag(0xA0000005), (encoder) => decoder.DecodeTaggedValue<Ticket_Tagged1>(new Asn1Tag(0x60000001), (encoder) => Ticket_Tagged1.DecodeTlvFrom(decoder)));
			this.enc_part = decoder.DecodeTaggedValue<EncryptedData>(new Asn1Tag(0xA0000006), (encoder) => EncryptedData.DecodeTlvFrom(decoder));
		}
	}

	partial class AS_REP : Asn1Explicit<KDC_REP>, IAsn1DerDecodableTlv<AS_REP>, IAsn1DerDecodableValue<AS_REP>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AS_REP(KDC_REP value) : base(new Asn1Tag(0x6000000B), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AS_REP DecodeValueFrom(Asn1DerDecoder decoder) => new AS_REP(decoder.DecodeTlv<KDC_REP>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AS_REP DecodeTlvFrom(Asn1DerDecoder decoder) => new AS_REP(decoder.DecodeExplicitTaggedTlv<KDC_REP>(new Asn1Tag(0x6000000B)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AS_REP? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<KDC_REP>(new Asn1Tag(0x6000000B), out var inner))
			{
				value = new AS_REP(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	partial class TGS_REP : Asn1Explicit<KDC_REP>, IAsn1DerDecodableTlv<TGS_REP>, IAsn1DerDecodableValue<TGS_REP>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TGS_REP(KDC_REP value) : base(new Asn1Tag(0x6000000D), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TGS_REP DecodeValueFrom(Asn1DerDecoder decoder) => new TGS_REP(decoder.DecodeTlv<KDC_REP>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TGS_REP DecodeTlvFrom(Asn1DerDecoder decoder) => new TGS_REP(decoder.DecodeExplicitTaggedTlv<KDC_REP>(new Asn1Tag(0x6000000D)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out TGS_REP? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<KDC_REP>(new Asn1Tag(0x6000000D), out var inner))
			{
				value = new TGS_REP(inner);
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
	partial class LastReq_Element : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<LastReq_Element>, IAsn1DerDecodableValue<LastReq_Element>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int lr_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime lr_value;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public LastReq_Element(int lr_type, GeneralizedTime lr_value)
		{
			this.lr_type = lr_type;
			this.lr_value = lr_value;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000001), this.lr_value, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.lr_value);
			});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.lr_type, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.lr_type);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static LastReq_Element DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new LastReq_Element(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static LastReq_Element DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = LastReq_Element.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out LastReq_Element? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = LastReq_Element.DecodeValueFrom(decoder);
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
		private LastReq_Element(Asn1DerDecoder decoder)
		{
			this.lr_type = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.lr_value = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeDateTimeTlv());
		}
	}

	[Asn1Sequence()]
	partial class EncKDCRepPart : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<EncKDCRepPart>, IAsn1DerDecodableValue<EncKDCRepPart>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptionKey key;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal LastReq_Element[] last_req;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int nonce;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? key_expiration;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString flags;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime authtime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? starttime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime endtime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? renew_till;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString srealm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName sname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal HostAddress[]? caddr;
		internal PA_DATA[]? padata;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EncKDCRepPart(EncryptionKey key, LastReq_Element[] last_req, int nonce, Asn1BitString flags, GeneralizedTime authtime, GeneralizedTime endtime, GeneralString srealm, PrincipalName sname, GeneralizedTime? key_expiration = default, GeneralizedTime? starttime = default, GeneralizedTime? renew_till = default, HostAddress[]? caddr = default, PA_DATA[]? padata = default)
		{
			this.key = key;
			this.last_req = last_req;
			this.nonce = nonce;
			this.key_expiration = key_expiration;
			this.flags = flags;
			this.authtime = authtime;
			this.starttime = starttime;
			this.endtime = endtime;
			this.renew_till = renew_till;
			this.srealm = srealm;
			this.sname = sname;
			this.caddr = caddr;
			this.padata = padata;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.padata is not null)
				encoder.EncodeExplicitTlv<PA_DATA[]>(new Asn1Tag(0xA000000C), this.padata, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.padata, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			if (this.caddr is not null)
				encoder.EncodeExplicitTlv<HostAddress[]>(new Asn1Tag(0xA000000B), this.caddr, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.caddr, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA000000A), this.sname, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.sname);
			});
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000009), this.srealm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.srealm);
			});
			if (this.renew_till is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000008), this.renew_till.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.renew_till.Value);
				});
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000007), this.endtime, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.endtime);
			});
			if (this.starttime is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000006), this.starttime.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.starttime.Value);
				});
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000005), this.authtime, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.authtime);
			});
			encoder.EncodeExplicitTlv<Asn1BitString>(new Asn1Tag(0xA0000004), this.flags, (encoder, r) =>
			{
				encoder.EncodeBitStringTlv(this.flags);
			});
			if (this.key_expiration is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000003), this.key_expiration.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.key_expiration.Value);
				});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000002), this.nonce, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.nonce);
			});
			encoder.EncodeExplicitTlv<LastReq_Element[]>(new Asn1Tag(0xA0000001), this.last_req, (encoder, r) =>
			{
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.last_req, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			});
			encoder.EncodeExplicitTlv<EncryptionKey>(new Asn1Tag(0xA0000000), this.key, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.key);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncKDCRepPart DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new EncKDCRepPart(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncKDCRepPart DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = EncKDCRepPart.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EncKDCRepPart? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = EncKDCRepPart.DecodeValueFrom(decoder);
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
		private EncKDCRepPart(Asn1DerDecoder decoder)
		{
			this.key = decoder.DecodeTaggedValue<EncryptionKey>(new Asn1Tag(0xA0000000), (encoder) => EncryptionKey.DecodeTlvFrom(decoder));
			this.last_req = decoder.DecodeTaggedValue<LastReq_Element[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeListTlv<LastReq_Element>(new Asn1Tag(0x20000010), (encoder) => LastReq_Element.DecodeTlvFrom(decoder)));
			this.nonce = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.key_expiration = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.flags = decoder.DecodeTaggedValue<Asn1BitString>(new Asn1Tag(0xA0000004), (encoder) => decoder.DecodeBitStringTlv());
			this.authtime = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000005), (encoder) => decoder.DecodeDateTimeTlv());
			this.starttime = decoder.CheckTag(new Asn1Tag(0xA0000006)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000006), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.endtime = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000007), (encoder) => decoder.DecodeDateTimeTlv());
			this.renew_till = decoder.CheckTag(new Asn1Tag(0xA0000008)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000008), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.srealm = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000009), (encoder) => decoder.DecodeStringTlv<GeneralString>());
			this.sname = decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA000000A), (encoder) => PrincipalName.DecodeTlvFrom(decoder));
			this.caddr = decoder.CheckTag(new Asn1Tag(0xA000000B)) ? decoder.DecodeTaggedValue<HostAddress[]>(new Asn1Tag(0xA000000B), (encoder) => decoder.DecodeListTlv<HostAddress>(new Asn1Tag(0x20000010), (encoder) => HostAddress.DecodeTlvFrom(decoder))) : default(HostAddress[]);
			this.padata = decoder.CheckTag(new Asn1Tag(0xA000000C)) ? decoder.DecodeTaggedValue<PA_DATA[]>(new Asn1Tag(0xA000000C), (encoder) => decoder.DecodeListTlv<PA_DATA>(new Asn1Tag(0x20000010), (encoder) => PA_DATA.DecodeTlvFrom(decoder))) : default(PA_DATA[]);
		}
	}

	partial class EncASRepPart : Asn1Explicit<EncKDCRepPart>, IAsn1DerDecodableTlv<EncASRepPart>, IAsn1DerDecodableValue<EncASRepPart>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EncASRepPart(EncKDCRepPart value) : base(new Asn1Tag(0x60000019), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncASRepPart DecodeValueFrom(Asn1DerDecoder decoder) => new EncASRepPart(decoder.DecodeTlv<EncKDCRepPart>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncASRepPart DecodeTlvFrom(Asn1DerDecoder decoder) => new EncASRepPart(decoder.DecodeExplicitTaggedTlv<EncKDCRepPart>(new Asn1Tag(0x60000019)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EncASRepPart? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<EncKDCRepPart>(new Asn1Tag(0x60000019), out var inner))
			{
				value = new EncASRepPart(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	partial class EncTGSRepPart : Asn1Explicit<EncKDCRepPart>, IAsn1DerDecodableTlv<EncTGSRepPart>, IAsn1DerDecodableValue<EncTGSRepPart>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EncTGSRepPart(EncKDCRepPart value) : base(new Asn1Tag(0x6000001A), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncTGSRepPart DecodeValueFrom(Asn1DerDecoder decoder) => new EncTGSRepPart(decoder.DecodeTlv<EncKDCRepPart>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncTGSRepPart DecodeTlvFrom(Asn1DerDecoder decoder) => new EncTGSRepPart(decoder.DecodeExplicitTaggedTlv<EncKDCRepPart>(new Asn1Tag(0x6000001A)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EncTGSRepPart? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<EncKDCRepPart>(new Asn1Tag(0x6000001A), out var inner))
			{
				value = new EncTGSRepPart(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	partial class AP_REQ : Asn1Explicit<AP_REQ_Tagged14>, IAsn1DerDecodableTlv<AP_REQ>, IAsn1DerDecodableValue<AP_REQ>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AP_REQ(AP_REQ_Tagged14 value) : base(new Asn1Tag(0x6000000E), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AP_REQ DecodeValueFrom(Asn1DerDecoder decoder) => new AP_REQ(decoder.DecodeTlv<AP_REQ_Tagged14>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AP_REQ DecodeTlvFrom(Asn1DerDecoder decoder) => new AP_REQ(decoder.DecodeExplicitTaggedTlv<AP_REQ_Tagged14>(new Asn1Tag(0x6000000E)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AP_REQ? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<AP_REQ_Tagged14>(new Asn1Tag(0x6000000E), out var inner))
			{
				value = new AP_REQ(inner);
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
	partial class AP_REQ_Tagged14 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AP_REQ_Tagged14>, IAsn1DerDecodableValue<AP_REQ_Tagged14>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte pvno;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte msg_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString ap_options;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Ticket_Tagged1 ticket;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptedData authenticator;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AP_REQ_Tagged14(byte pvno, byte msg_type, Asn1BitString ap_options, Ticket_Tagged1 ticket, EncryptedData authenticator)
		{
			this.pvno = pvno;
			this.msg_type = msg_type;
			this.ap_options = ap_options;
			this.ticket = ticket;
			this.authenticator = authenticator;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<EncryptedData>(new Asn1Tag(0xA0000004), this.authenticator, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.authenticator);
			});
			encoder.EncodeExplicitTlv<Ticket_Tagged1>(new Asn1Tag(0xA0000003), this.ticket, (encoder, r) =>
			{
				encoder.EncodeExplicitTlv<Ticket_Tagged1>(new Asn1Tag(0x60000001), this.ticket, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.ticket);
				});
			});
			encoder.EncodeExplicitTlv<Asn1BitString>(new Asn1Tag(0xA0000002), this.ap_options, (encoder, r) =>
			{
				encoder.EncodeBitStringTlv(this.ap_options);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000001), this.msg_type, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.msg_type);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000000), this.pvno, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.pvno);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AP_REQ_Tagged14 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AP_REQ_Tagged14(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AP_REQ_Tagged14 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AP_REQ_Tagged14.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AP_REQ_Tagged14? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AP_REQ_Tagged14.DecodeValueFrom(decoder);
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
		private AP_REQ_Tagged14(Asn1DerDecoder decoder)
		{
			this.pvno = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.msg_type = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.ap_options = decoder.DecodeTaggedValue<Asn1BitString>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeBitStringTlv());
			this.ticket = decoder.DecodeTaggedValue<Ticket_Tagged1>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeTaggedValue<Ticket_Tagged1>(new Asn1Tag(0x60000001), (encoder) => Ticket_Tagged1.DecodeTlvFrom(decoder)));
			this.authenticator = decoder.DecodeTaggedValue<EncryptedData>(new Asn1Tag(0xA0000004), (encoder) => EncryptedData.DecodeTlvFrom(decoder));
		}
	}

	partial class Authenticator : Asn1Explicit<Authenticator_Tagged2>, IAsn1DerDecodableTlv<Authenticator>, IAsn1DerDecodableValue<Authenticator>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Authenticator(Authenticator_Tagged2 value) : base(new Asn1Tag(0x60000002), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Authenticator DecodeValueFrom(Asn1DerDecoder decoder) => new Authenticator(decoder.DecodeTlv<Authenticator_Tagged2>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Authenticator DecodeTlvFrom(Asn1DerDecoder decoder) => new Authenticator(decoder.DecodeExplicitTaggedTlv<Authenticator_Tagged2>(new Asn1Tag(0x60000002)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Authenticator? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<Authenticator_Tagged2>(new Asn1Tag(0x60000002), out var inner))
			{
				value = new Authenticator(inner);
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
	partial class Authenticator_Tagged2 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Authenticator_Tagged2>, IAsn1DerDecodableValue<Authenticator_Tagged2>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte authenticator_vno;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString crealm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName cname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Checksum? cksum;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint cusec;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime ctime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptionKey? subkey;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int? seq_number;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AuthorizationData_Element[]? authorization_data;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Authenticator_Tagged2(byte authenticator_vno, GeneralString crealm, PrincipalName cname, uint cusec, GeneralizedTime ctime, Checksum? cksum = default, EncryptionKey? subkey = default, int? seq_number = default, AuthorizationData_Element[]? authorization_data = default)
		{
			this.authenticator_vno = authenticator_vno;
			this.crealm = crealm;
			this.cname = cname;
			this.cksum = cksum;
			this.cusec = cusec;
			this.ctime = ctime;
			this.subkey = subkey;
			this.seq_number = seq_number;
			this.authorization_data = authorization_data;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.authorization_data is not null)
				encoder.EncodeExplicitTlv<AuthorizationData_Element[]>(new Asn1Tag(0xA0000008), this.authorization_data, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.authorization_data, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			if (this.seq_number is not null)
				encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000007), this.seq_number.Value, (encoder, r) =>
				{
					encoder.EncodeInt32Tlv(this.seq_number.Value);
				});
			if (this.subkey is not null)
				encoder.EncodeExplicitTlv<EncryptionKey>(new Asn1Tag(0xA0000006), this.subkey, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.subkey);
				});
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000005), this.ctime, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.ctime);
			});
			encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000004), this.cusec, (encoder, r) =>
			{
				encoder.EncodeUInt32Tlv(this.cusec);
			});
			if (this.cksum is not null)
				encoder.EncodeExplicitTlv<Checksum>(new Asn1Tag(0xA0000003), this.cksum, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.cksum);
				});
			encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000002), this.cname, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.cname);
			});
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000001), this.crealm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.crealm);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000000), this.authenticator_vno, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.authenticator_vno);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Authenticator_Tagged2 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Authenticator_Tagged2(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Authenticator_Tagged2 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Authenticator_Tagged2.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Authenticator_Tagged2? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Authenticator_Tagged2.DecodeValueFrom(decoder);
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
		private Authenticator_Tagged2(Asn1DerDecoder decoder)
		{
			this.authenticator_vno = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.crealm = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeStringTlv<GeneralString>());
			this.cname = decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000002), (encoder) => PrincipalName.DecodeTlvFrom(decoder));
			this.cksum = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<Checksum>(new Asn1Tag(0xA0000003), (encoder) => Checksum.DecodeTlvFrom(decoder)) : default(Checksum);
			this.cusec = decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000004), (encoder) => decoder.DecodeIntegerTlvAsUInt32());
			this.ctime = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000005), (encoder) => decoder.DecodeDateTimeTlv());
			this.subkey = decoder.CheckTag(new Asn1Tag(0xA0000006)) ? decoder.DecodeTaggedValue<EncryptionKey>(new Asn1Tag(0xA0000006), (encoder) => EncryptionKey.DecodeTlvFrom(decoder)) : default(EncryptionKey);
			this.seq_number = decoder.CheckTag(new Asn1Tag(0xA0000007)) ? decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000007), (encoder) => decoder.DecodeIntegerTlvAsInt32()) : default(int?);
			this.authorization_data = decoder.CheckTag(new Asn1Tag(0xA0000008)) ? decoder.DecodeTaggedValue<AuthorizationData_Element[]>(new Asn1Tag(0xA0000008), (encoder) => decoder.DecodeListTlv<AuthorizationData_Element>(new Asn1Tag(0x20000010), (encoder) => AuthorizationData_Element.DecodeTlvFrom(decoder))) : default(AuthorizationData_Element[]);
		}
	}

	partial class AP_REP : Asn1Explicit<AP_REP_Tagged15>, IAsn1DerDecodableTlv<AP_REP>, IAsn1DerDecodableValue<AP_REP>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AP_REP(AP_REP_Tagged15 value) : base(new Asn1Tag(0x6000000F), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AP_REP DecodeValueFrom(Asn1DerDecoder decoder) => new AP_REP(decoder.DecodeTlv<AP_REP_Tagged15>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AP_REP DecodeTlvFrom(Asn1DerDecoder decoder) => new AP_REP(decoder.DecodeExplicitTaggedTlv<AP_REP_Tagged15>(new Asn1Tag(0x6000000F)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AP_REP? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<AP_REP_Tagged15>(new Asn1Tag(0x6000000F), out var inner))
			{
				value = new AP_REP(inner);
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
	partial class AP_REP_Tagged15 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AP_REP_Tagged15>, IAsn1DerDecodableValue<AP_REP_Tagged15>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte pvno;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte msg_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptedData enc_part;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AP_REP_Tagged15(byte pvno, byte msg_type, EncryptedData enc_part)
		{
			this.pvno = pvno;
			this.msg_type = msg_type;
			this.enc_part = enc_part;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<EncryptedData>(new Asn1Tag(0xA0000002), this.enc_part, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.enc_part);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000001), this.msg_type, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.msg_type);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000000), this.pvno, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.pvno);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AP_REP_Tagged15 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AP_REP_Tagged15(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AP_REP_Tagged15 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AP_REP_Tagged15.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AP_REP_Tagged15? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AP_REP_Tagged15.DecodeValueFrom(decoder);
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
		private AP_REP_Tagged15(Asn1DerDecoder decoder)
		{
			this.pvno = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.msg_type = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.enc_part = decoder.DecodeTaggedValue<EncryptedData>(new Asn1Tag(0xA0000002), (encoder) => EncryptedData.DecodeTlvFrom(decoder));
		}
	}

	partial class EncAPRepPart : Asn1Explicit<EncAPRepPart_Tagged27>, IAsn1DerDecodableTlv<EncAPRepPart>, IAsn1DerDecodableValue<EncAPRepPart>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EncAPRepPart(EncAPRepPart_Tagged27 value) : base(new Asn1Tag(0x6000001B), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncAPRepPart DecodeValueFrom(Asn1DerDecoder decoder) => new EncAPRepPart(decoder.DecodeTlv<EncAPRepPart_Tagged27>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncAPRepPart DecodeTlvFrom(Asn1DerDecoder decoder) => new EncAPRepPart(decoder.DecodeExplicitTaggedTlv<EncAPRepPart_Tagged27>(new Asn1Tag(0x6000001B)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EncAPRepPart? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<EncAPRepPart_Tagged27>(new Asn1Tag(0x6000001B), out var inner))
			{
				value = new EncAPRepPart(inner);
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
	partial class EncAPRepPart_Tagged27 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<EncAPRepPart_Tagged27>, IAsn1DerDecodableValue<EncAPRepPart_Tagged27>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime ctime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint cusec;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptionKey? subkey;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint? seq_number;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EncAPRepPart_Tagged27(GeneralizedTime ctime, uint cusec, EncryptionKey? subkey = default, uint? seq_number = default)
		{
			this.ctime = ctime;
			this.cusec = cusec;
			this.subkey = subkey;
			this.seq_number = seq_number;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.seq_number is not null)
				encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000003), this.seq_number.Value, (encoder, r) =>
				{
					encoder.EncodeUInt32Tlv(this.seq_number.Value);
				});
			if (this.subkey is not null)
				encoder.EncodeExplicitTlv<EncryptionKey>(new Asn1Tag(0xA0000002), this.subkey, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.subkey);
				});
			encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000001), this.cusec, (encoder, r) =>
			{
				encoder.EncodeUInt32Tlv(this.cusec);
			});
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000000), this.ctime, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.ctime);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncAPRepPart_Tagged27 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new EncAPRepPart_Tagged27(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncAPRepPart_Tagged27 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = EncAPRepPart_Tagged27.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EncAPRepPart_Tagged27? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = EncAPRepPart_Tagged27.DecodeValueFrom(decoder);
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
		private EncAPRepPart_Tagged27(Asn1DerDecoder decoder)
		{
			this.ctime = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeDateTimeTlv());
			this.cusec = decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsUInt32());
			this.subkey = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<EncryptionKey>(new Asn1Tag(0xA0000002), (encoder) => EncryptionKey.DecodeTlvFrom(decoder)) : default(EncryptionKey);
			this.seq_number = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeIntegerTlvAsUInt32()) : default(uint?);
		}
	}

	[Asn1Sequence()]
	partial class KRB_SAFE_BODY : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KRB_SAFE_BODY>, IAsn1DerDecodableValue<KRB_SAFE_BODY>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] user_data;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? timestamp;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint? usec;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint? seq_number;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal HostAddress s_address;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal HostAddress? r_address;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KRB_SAFE_BODY(Byte[] user_data, HostAddress s_address, GeneralizedTime? timestamp = default, uint? usec = default, uint? seq_number = default, HostAddress? r_address = default)
		{
			this.user_data = user_data;
			this.timestamp = timestamp;
			this.usec = usec;
			this.seq_number = seq_number;
			this.s_address = s_address;
			this.r_address = r_address;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.r_address is not null)
				encoder.EncodeExplicitTlv<HostAddress>(new Asn1Tag(0xA0000005), this.r_address, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.r_address);
				});
			encoder.EncodeExplicitTlv<HostAddress>(new Asn1Tag(0xA0000004), this.s_address, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.s_address);
			});
			if (this.seq_number is not null)
				encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000003), this.seq_number.Value, (encoder, r) =>
				{
					encoder.EncodeUInt32Tlv(this.seq_number.Value);
				});
			if (this.usec is not null)
				encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000002), this.usec.Value, (encoder, r) =>
				{
					encoder.EncodeUInt32Tlv(this.usec.Value);
				});
			if (this.timestamp is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000001), this.timestamp.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.timestamp.Value);
				});
			encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000000), this.user_data, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.user_data);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_SAFE_BODY DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KRB_SAFE_BODY(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_SAFE_BODY DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KRB_SAFE_BODY.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KRB_SAFE_BODY? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KRB_SAFE_BODY.DecodeValueFrom(decoder);
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
		private KRB_SAFE_BODY(Asn1DerDecoder decoder)
		{
			this.user_data = decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeOctetStringTlv());
			this.timestamp = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.usec = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeIntegerTlvAsUInt32()) : default(uint?);
			this.seq_number = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeIntegerTlvAsUInt32()) : default(uint?);
			this.s_address = decoder.DecodeTaggedValue<HostAddress>(new Asn1Tag(0xA0000004), (encoder) => HostAddress.DecodeTlvFrom(decoder));
			this.r_address = decoder.CheckTag(new Asn1Tag(0xA0000005)) ? decoder.DecodeTaggedValue<HostAddress>(new Asn1Tag(0xA0000005), (encoder) => HostAddress.DecodeTlvFrom(decoder)) : default(HostAddress);
		}
	}

	partial class KRB_SAFE : Asn1Explicit<KRB_SAFE_Tagged20>, IAsn1DerDecodableTlv<KRB_SAFE>, IAsn1DerDecodableValue<KRB_SAFE>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KRB_SAFE(KRB_SAFE_Tagged20 value) : base(new Asn1Tag(0x60000014), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_SAFE DecodeValueFrom(Asn1DerDecoder decoder) => new KRB_SAFE(decoder.DecodeTlv<KRB_SAFE_Tagged20>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_SAFE DecodeTlvFrom(Asn1DerDecoder decoder) => new KRB_SAFE(decoder.DecodeExplicitTaggedTlv<KRB_SAFE_Tagged20>(new Asn1Tag(0x60000014)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KRB_SAFE? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<KRB_SAFE_Tagged20>(new Asn1Tag(0x60000014), out var inner))
			{
				value = new KRB_SAFE(inner);
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
	partial class KRB_SAFE_Tagged20 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KRB_SAFE_Tagged20>, IAsn1DerDecodableValue<KRB_SAFE_Tagged20>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte pvno;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte msg_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal KRB_SAFE_BODY safe_body;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Checksum cksum;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KRB_SAFE_Tagged20(byte pvno, byte msg_type, KRB_SAFE_BODY safe_body, Checksum cksum)
		{
			this.pvno = pvno;
			this.msg_type = msg_type;
			this.safe_body = safe_body;
			this.cksum = cksum;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Checksum>(new Asn1Tag(0xA0000003), this.cksum, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.cksum);
			});
			encoder.EncodeExplicitTlv<KRB_SAFE_BODY>(new Asn1Tag(0xA0000002), this.safe_body, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.safe_body);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000001), this.msg_type, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.msg_type);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000000), this.pvno, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.pvno);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_SAFE_Tagged20 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KRB_SAFE_Tagged20(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_SAFE_Tagged20 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KRB_SAFE_Tagged20.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KRB_SAFE_Tagged20? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KRB_SAFE_Tagged20.DecodeValueFrom(decoder);
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
		private KRB_SAFE_Tagged20(Asn1DerDecoder decoder)
		{
			this.pvno = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.msg_type = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.safe_body = decoder.DecodeTaggedValue<KRB_SAFE_BODY>(new Asn1Tag(0xA0000002), (encoder) => KRB_SAFE_BODY.DecodeTlvFrom(decoder));
			this.cksum = decoder.DecodeTaggedValue<Checksum>(new Asn1Tag(0xA0000003), (encoder) => Checksum.DecodeTlvFrom(decoder));
		}
	}

	partial class KRB_PRIV : Asn1Explicit<KRB_PRIV_Tagged21>, IAsn1DerDecodableTlv<KRB_PRIV>, IAsn1DerDecodableValue<KRB_PRIV>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KRB_PRIV(KRB_PRIV_Tagged21 value) : base(new Asn1Tag(0x60000015), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_PRIV DecodeValueFrom(Asn1DerDecoder decoder) => new KRB_PRIV(decoder.DecodeTlv<KRB_PRIV_Tagged21>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_PRIV DecodeTlvFrom(Asn1DerDecoder decoder) => new KRB_PRIV(decoder.DecodeExplicitTaggedTlv<KRB_PRIV_Tagged21>(new Asn1Tag(0x60000015)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KRB_PRIV? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<KRB_PRIV_Tagged21>(new Asn1Tag(0x60000015), out var inner))
			{
				value = new KRB_PRIV(inner);
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
	partial class KRB_PRIV_Tagged21 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KRB_PRIV_Tagged21>, IAsn1DerDecodableValue<KRB_PRIV_Tagged21>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte pvno;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte msg_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptedData enc_part;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KRB_PRIV_Tagged21(byte pvno, byte msg_type, EncryptedData enc_part)
		{
			this.pvno = pvno;
			this.msg_type = msg_type;
			this.enc_part = enc_part;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<EncryptedData>(new Asn1Tag(0xA0000003), this.enc_part, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.enc_part);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000001), this.msg_type, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.msg_type);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000000), this.pvno, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.pvno);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_PRIV_Tagged21 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KRB_PRIV_Tagged21(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_PRIV_Tagged21 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KRB_PRIV_Tagged21.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KRB_PRIV_Tagged21? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KRB_PRIV_Tagged21.DecodeValueFrom(decoder);
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
		private KRB_PRIV_Tagged21(Asn1DerDecoder decoder)
		{
			this.pvno = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.msg_type = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.enc_part = decoder.DecodeTaggedValue<EncryptedData>(new Asn1Tag(0xA0000003), (encoder) => EncryptedData.DecodeTlvFrom(decoder));
		}
	}

	partial class EncKrbPrivPart : Asn1Explicit<EncKrbPrivPart_Tagged28>, IAsn1DerDecodableTlv<EncKrbPrivPart>, IAsn1DerDecodableValue<EncKrbPrivPart>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EncKrbPrivPart(EncKrbPrivPart_Tagged28 value) : base(new Asn1Tag(0x6000001C), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncKrbPrivPart DecodeValueFrom(Asn1DerDecoder decoder) => new EncKrbPrivPart(decoder.DecodeTlv<EncKrbPrivPart_Tagged28>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncKrbPrivPart DecodeTlvFrom(Asn1DerDecoder decoder) => new EncKrbPrivPart(decoder.DecodeExplicitTaggedTlv<EncKrbPrivPart_Tagged28>(new Asn1Tag(0x6000001C)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EncKrbPrivPart? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<EncKrbPrivPart_Tagged28>(new Asn1Tag(0x6000001C), out var inner))
			{
				value = new EncKrbPrivPart(inner);
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
	partial class EncKrbPrivPart_Tagged28 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<EncKrbPrivPart_Tagged28>, IAsn1DerDecodableValue<EncKrbPrivPart_Tagged28>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] user_data;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? timestamp;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint? usec;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint? seq_number;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal HostAddress s_address;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal HostAddress? r_address;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EncKrbPrivPart_Tagged28(Byte[] user_data, HostAddress s_address, GeneralizedTime? timestamp = default, uint? usec = default, uint? seq_number = default, HostAddress? r_address = default)
		{
			this.user_data = user_data;
			this.timestamp = timestamp;
			this.usec = usec;
			this.seq_number = seq_number;
			this.s_address = s_address;
			this.r_address = r_address;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.r_address is not null)
				encoder.EncodeExplicitTlv<HostAddress>(new Asn1Tag(0xA0000005), this.r_address, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.r_address);
				});
			encoder.EncodeExplicitTlv<HostAddress>(new Asn1Tag(0xA0000004), this.s_address, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.s_address);
			});
			if (this.seq_number is not null)
				encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000003), this.seq_number.Value, (encoder, r) =>
				{
					encoder.EncodeUInt32Tlv(this.seq_number.Value);
				});
			if (this.usec is not null)
				encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000002), this.usec.Value, (encoder, r) =>
				{
					encoder.EncodeUInt32Tlv(this.usec.Value);
				});
			if (this.timestamp is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000001), this.timestamp.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.timestamp.Value);
				});
			encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000000), this.user_data, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.user_data);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncKrbPrivPart_Tagged28 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new EncKrbPrivPart_Tagged28(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncKrbPrivPart_Tagged28 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = EncKrbPrivPart_Tagged28.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EncKrbPrivPart_Tagged28? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = EncKrbPrivPart_Tagged28.DecodeValueFrom(decoder);
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
		private EncKrbPrivPart_Tagged28(Asn1DerDecoder decoder)
		{
			this.user_data = decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeOctetStringTlv());
			this.timestamp = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.usec = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeIntegerTlvAsUInt32()) : default(uint?);
			this.seq_number = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeIntegerTlvAsUInt32()) : default(uint?);
			this.s_address = decoder.DecodeTaggedValue<HostAddress>(new Asn1Tag(0xA0000004), (encoder) => HostAddress.DecodeTlvFrom(decoder));
			this.r_address = decoder.CheckTag(new Asn1Tag(0xA0000005)) ? decoder.DecodeTaggedValue<HostAddress>(new Asn1Tag(0xA0000005), (encoder) => HostAddress.DecodeTlvFrom(decoder)) : default(HostAddress);
		}
	}

	partial class KRB_CRED : Asn1Explicit<KRB_CRED_Tagged22>, IAsn1DerDecodableTlv<KRB_CRED>, IAsn1DerDecodableValue<KRB_CRED>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KRB_CRED(KRB_CRED_Tagged22 value) : base(new Asn1Tag(0x60000016), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_CRED DecodeValueFrom(Asn1DerDecoder decoder) => new KRB_CRED(decoder.DecodeTlv<KRB_CRED_Tagged22>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_CRED DecodeTlvFrom(Asn1DerDecoder decoder) => new KRB_CRED(decoder.DecodeExplicitTaggedTlv<KRB_CRED_Tagged22>(new Asn1Tag(0x60000016)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KRB_CRED? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<KRB_CRED_Tagged22>(new Asn1Tag(0x60000016), out var inner))
			{
				value = new KRB_CRED(inner);
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
	partial class KRB_CRED_Tagged22 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KRB_CRED_Tagged22>, IAsn1DerDecodableValue<KRB_CRED_Tagged22>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte pvno;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte msg_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Ticket_Tagged1[] tickets;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptedData enc_part;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KRB_CRED_Tagged22(byte pvno, byte msg_type, Ticket_Tagged1[] tickets, EncryptedData enc_part)
		{
			this.pvno = pvno;
			this.msg_type = msg_type;
			this.tickets = tickets;
			this.enc_part = enc_part;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<EncryptedData>(new Asn1Tag(0xA0000003), this.enc_part, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.enc_part);
			});
			encoder.EncodeExplicitTlv<Ticket_Tagged1[]>(new Asn1Tag(0xA0000002), this.tickets, (encoder, r) =>
			{
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.tickets, (encoder, r) =>
				{
					encoder.EncodeExplicitTlv<Ticket_Tagged1>(new Asn1Tag(0x60000001), r, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000001), this.msg_type, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.msg_type);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000000), this.pvno, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.pvno);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_CRED_Tagged22 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KRB_CRED_Tagged22(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_CRED_Tagged22 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KRB_CRED_Tagged22.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KRB_CRED_Tagged22? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KRB_CRED_Tagged22.DecodeValueFrom(decoder);
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
		private KRB_CRED_Tagged22(Asn1DerDecoder decoder)
		{
			this.pvno = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.msg_type = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.tickets = decoder.DecodeTaggedValue<Ticket_Tagged1[]>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeListTlv<Ticket_Tagged1>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTaggedValue<Ticket_Tagged1>(new Asn1Tag(0x60000001), (encoder) => Ticket_Tagged1.DecodeTlvFrom(decoder))));
			this.enc_part = decoder.DecodeTaggedValue<EncryptedData>(new Asn1Tag(0xA0000003), (encoder) => EncryptedData.DecodeTlvFrom(decoder));
		}
	}

	[Asn1Sequence()]
	partial class KrbCredInfo : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KrbCredInfo>, IAsn1DerDecodableValue<KrbCredInfo>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptionKey key;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString? prealm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName? pname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString? flags;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? authtime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? starttime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? endtime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? renew_till;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString? srealm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName? sname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal HostAddress[]? caddr;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KrbCredInfo(EncryptionKey key, GeneralString? prealm = default, PrincipalName? pname = default, Asn1BitString? flags = default, GeneralizedTime? authtime = default, GeneralizedTime? starttime = default, GeneralizedTime? endtime = default, GeneralizedTime? renew_till = default, GeneralString? srealm = default, PrincipalName? sname = default, HostAddress[]? caddr = default)
		{
			this.key = key;
			this.prealm = prealm;
			this.pname = pname;
			this.flags = flags;
			this.authtime = authtime;
			this.starttime = starttime;
			this.endtime = endtime;
			this.renew_till = renew_till;
			this.srealm = srealm;
			this.sname = sname;
			this.caddr = caddr;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.caddr is not null)
				encoder.EncodeExplicitTlv<HostAddress[]>(new Asn1Tag(0xA000000A), this.caddr, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.caddr, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			if (this.sname is not null)
				encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000009), this.sname, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.sname);
				});
			if (this.srealm is not null)
				encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000008), this.srealm.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.srealm.Value);
				});
			if (this.renew_till is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000007), this.renew_till.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.renew_till.Value);
				});
			if (this.endtime is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000006), this.endtime.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.endtime.Value);
				});
			if (this.starttime is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000005), this.starttime.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.starttime.Value);
				});
			if (this.authtime is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000004), this.authtime.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.authtime.Value);
				});
			if (this.flags is not null)
				encoder.EncodeExplicitTlv<Asn1BitString>(new Asn1Tag(0xA0000003), this.flags.Value, (encoder, r) =>
				{
					encoder.EncodeBitStringTlv(this.flags.Value);
				});
			if (this.pname is not null)
				encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000002), this.pname, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.pname);
				});
			if (this.prealm is not null)
				encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000001), this.prealm.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.prealm.Value);
				});
			encoder.EncodeExplicitTlv<EncryptionKey>(new Asn1Tag(0xA0000000), this.key, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.key);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KrbCredInfo DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KrbCredInfo(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KrbCredInfo DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KrbCredInfo.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KrbCredInfo? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KrbCredInfo.DecodeValueFrom(decoder);
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
		private KrbCredInfo(Asn1DerDecoder decoder)
		{
			this.key = decoder.DecodeTaggedValue<EncryptionKey>(new Asn1Tag(0xA0000000), (encoder) => EncryptionKey.DecodeTlvFrom(decoder));
			this.prealm = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeStringTlv<GeneralString>()) : default(GeneralString?);
			this.pname = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000002), (encoder) => PrincipalName.DecodeTlvFrom(decoder)) : default(PrincipalName);
			this.flags = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<Asn1BitString>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeBitStringTlv()) : default(Asn1BitString?);
			this.authtime = decoder.CheckTag(new Asn1Tag(0xA0000004)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000004), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.starttime = decoder.CheckTag(new Asn1Tag(0xA0000005)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000005), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.endtime = decoder.CheckTag(new Asn1Tag(0xA0000006)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000006), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.renew_till = decoder.CheckTag(new Asn1Tag(0xA0000007)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000007), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.srealm = decoder.CheckTag(new Asn1Tag(0xA0000008)) ? decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000008), (encoder) => decoder.DecodeStringTlv<GeneralString>()) : default(GeneralString?);
			this.sname = decoder.CheckTag(new Asn1Tag(0xA0000009)) ? decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000009), (encoder) => PrincipalName.DecodeTlvFrom(decoder)) : default(PrincipalName);
			this.caddr = decoder.CheckTag(new Asn1Tag(0xA000000A)) ? decoder.DecodeTaggedValue<HostAddress[]>(new Asn1Tag(0xA000000A), (encoder) => decoder.DecodeListTlv<HostAddress>(new Asn1Tag(0x20000010), (encoder) => HostAddress.DecodeTlvFrom(decoder))) : default(HostAddress[]);
		}
	}

	partial class EncKrbCredPart : Asn1Explicit<EncKrbCredPart_Tagged29>, IAsn1DerDecodableTlv<EncKrbCredPart>, IAsn1DerDecodableValue<EncKrbCredPart>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EncKrbCredPart(EncKrbCredPart_Tagged29 value) : base(new Asn1Tag(0x6000001D), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncKrbCredPart DecodeValueFrom(Asn1DerDecoder decoder) => new EncKrbCredPart(decoder.DecodeTlv<EncKrbCredPart_Tagged29>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncKrbCredPart DecodeTlvFrom(Asn1DerDecoder decoder) => new EncKrbCredPart(decoder.DecodeExplicitTaggedTlv<EncKrbCredPart_Tagged29>(new Asn1Tag(0x6000001D)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EncKrbCredPart? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<EncKrbCredPart_Tagged29>(new Asn1Tag(0x6000001D), out var inner))
			{
				value = new EncKrbCredPart(inner);
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
	partial class EncKrbCredPart_Tagged29 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<EncKrbCredPart_Tagged29>, IAsn1DerDecodableValue<EncKrbCredPart_Tagged29>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal KrbCredInfo[] ticket_info;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint? nonce;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? timestamp;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint? usec;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal HostAddress? s_address;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal HostAddress? r_address;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EncKrbCredPart_Tagged29(KrbCredInfo[] ticket_info, uint? nonce = default, GeneralizedTime? timestamp = default, uint? usec = default, HostAddress? s_address = default, HostAddress? r_address = default)
		{
			this.ticket_info = ticket_info;
			this.nonce = nonce;
			this.timestamp = timestamp;
			this.usec = usec;
			this.s_address = s_address;
			this.r_address = r_address;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.r_address is not null)
				encoder.EncodeExplicitTlv<HostAddress>(new Asn1Tag(0xA0000005), this.r_address, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.r_address);
				});
			if (this.s_address is not null)
				encoder.EncodeExplicitTlv<HostAddress>(new Asn1Tag(0xA0000004), this.s_address, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.s_address);
				});
			if (this.usec is not null)
				encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000003), this.usec.Value, (encoder, r) =>
				{
					encoder.EncodeUInt32Tlv(this.usec.Value);
				});
			if (this.timestamp is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000002), this.timestamp.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.timestamp.Value);
				});
			if (this.nonce is not null)
				encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000001), this.nonce.Value, (encoder, r) =>
				{
					encoder.EncodeUInt32Tlv(this.nonce.Value);
				});
			encoder.EncodeExplicitTlv<KrbCredInfo[]>(new Asn1Tag(0xA0000000), this.ticket_info, (encoder, r) =>
			{
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.ticket_info, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncKrbCredPart_Tagged29 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new EncKrbCredPart_Tagged29(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EncKrbCredPart_Tagged29 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = EncKrbCredPart_Tagged29.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EncKrbCredPart_Tagged29? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = EncKrbCredPart_Tagged29.DecodeValueFrom(decoder);
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
		private EncKrbCredPart_Tagged29(Asn1DerDecoder decoder)
		{
			this.ticket_info = decoder.DecodeTaggedValue<KrbCredInfo[]>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeListTlv<KrbCredInfo>(new Asn1Tag(0x20000010), (encoder) => KrbCredInfo.DecodeTlvFrom(decoder)));
			this.nonce = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsUInt32()) : default(uint?);
			this.timestamp = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.usec = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeIntegerTlvAsUInt32()) : default(uint?);
			this.s_address = decoder.CheckTag(new Asn1Tag(0xA0000004)) ? decoder.DecodeTaggedValue<HostAddress>(new Asn1Tag(0xA0000004), (encoder) => HostAddress.DecodeTlvFrom(decoder)) : default(HostAddress);
			this.r_address = decoder.CheckTag(new Asn1Tag(0xA0000005)) ? decoder.DecodeTaggedValue<HostAddress>(new Asn1Tag(0xA0000005), (encoder) => HostAddress.DecodeTlvFrom(decoder)) : default(HostAddress);
		}
	}

	partial class KRB_ERROR : Asn1Explicit<KRB_ERROR_Tagged30>, IAsn1DerDecodableTlv<KRB_ERROR>, IAsn1DerDecodableValue<KRB_ERROR>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KRB_ERROR(KRB_ERROR_Tagged30 value) : base(new Asn1Tag(0x6000001E), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_ERROR DecodeValueFrom(Asn1DerDecoder decoder) => new KRB_ERROR(decoder.DecodeTlv<KRB_ERROR_Tagged30>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_ERROR DecodeTlvFrom(Asn1DerDecoder decoder) => new KRB_ERROR(decoder.DecodeExplicitTaggedTlv<KRB_ERROR_Tagged30>(new Asn1Tag(0x6000001E)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KRB_ERROR? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<KRB_ERROR_Tagged30>(new Asn1Tag(0x6000001E), out var inner))
			{
				value = new KRB_ERROR(inner);
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
	partial class KRB_ERROR_Tagged30 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KRB_ERROR_Tagged30>, IAsn1DerDecodableValue<KRB_ERROR_Tagged30>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte pvno;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte msg_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? ctime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint? cusec;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime stime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint susec;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int error_code;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString? crealm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName? cname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString realm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName sname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString? e_text;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? e_data;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KRB_ERROR_Tagged30(byte pvno, byte msg_type, GeneralizedTime stime, uint susec, int error_code, GeneralString realm, PrincipalName sname, GeneralizedTime? ctime = default, uint? cusec = default, GeneralString? crealm = default, PrincipalName? cname = default, GeneralString? e_text = default, Byte[]? e_data = default)
		{
			this.pvno = pvno;
			this.msg_type = msg_type;
			this.ctime = ctime;
			this.cusec = cusec;
			this.stime = stime;
			this.susec = susec;
			this.error_code = error_code;
			this.crealm = crealm;
			this.cname = cname;
			this.realm = realm;
			this.sname = sname;
			this.e_text = e_text;
			this.e_data = e_data;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.e_data is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA000000C), this.e_data, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.e_data);
				});
			if (this.e_text is not null)
				encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA000000B), this.e_text.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.e_text.Value);
				});
			encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA000000A), this.sname, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.sname);
			});
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000009), this.realm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.realm);
			});
			if (this.cname is not null)
				encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000008), this.cname, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.cname);
				});
			if (this.crealm is not null)
				encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000007), this.crealm.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.crealm.Value);
				});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000006), this.error_code, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.error_code);
			});
			encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000005), this.susec, (encoder, r) =>
			{
				encoder.EncodeUInt32Tlv(this.susec);
			});
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000004), this.stime, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.stime);
			});
			if (this.cusec is not null)
				encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000003), this.cusec.Value, (encoder, r) =>
				{
					encoder.EncodeUInt32Tlv(this.cusec.Value);
				});
			if (this.ctime is not null)
				encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000002), this.ctime.Value, (encoder, r) =>
				{
					encoder.EncodeDateTimeTlv(this.ctime.Value);
				});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000001), this.msg_type, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.msg_type);
			});
			encoder.EncodeExplicitTlv<byte>(new Asn1Tag(0xA0000000), this.pvno, (encoder, r) =>
			{
				encoder.EncodeByteTlv(this.pvno);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_ERROR_Tagged30 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KRB_ERROR_Tagged30(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KRB_ERROR_Tagged30 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KRB_ERROR_Tagged30.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KRB_ERROR_Tagged30? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KRB_ERROR_Tagged30.DecodeValueFrom(decoder);
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
		private KRB_ERROR_Tagged30(Asn1DerDecoder decoder)
		{
			this.pvno = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.msg_type = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.ctime = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeDateTimeTlv()) : default(GeneralizedTime?);
			this.cusec = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeIntegerTlvAsUInt32()) : default(uint?);
			this.stime = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000004), (encoder) => decoder.DecodeDateTimeTlv());
			this.susec = decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000005), (encoder) => decoder.DecodeIntegerTlvAsUInt32());
			this.error_code = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000006), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.crealm = decoder.CheckTag(new Asn1Tag(0xA0000007)) ? decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000007), (encoder) => decoder.DecodeStringTlv<GeneralString>()) : default(GeneralString?);
			this.cname = decoder.CheckTag(new Asn1Tag(0xA0000008)) ? decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000008), (encoder) => PrincipalName.DecodeTlvFrom(decoder)) : default(PrincipalName);
			this.realm = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000009), (encoder) => decoder.DecodeStringTlv<GeneralString>());
			this.sname = decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA000000A), (encoder) => PrincipalName.DecodeTlvFrom(decoder));
			this.e_text = decoder.CheckTag(new Asn1Tag(0xA000000B)) ? decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA000000B), (encoder) => decoder.DecodeStringTlv<GeneralString>()) : default(GeneralString?);
			this.e_data = decoder.CheckTag(new Asn1Tag(0xA000000C)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA000000C), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
		}
	}

	[Asn1Sequence()]
	partial class TYPED_DATA_Element : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<TYPED_DATA_Element>, IAsn1DerDecodableValue<TYPED_DATA_Element>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int data_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? data_value;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TYPED_DATA_Element(int data_type, Byte[]? data_value = default)
		{
			this.data_type = data_type;
			this.data_value = data_value;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.data_value is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000001), this.data_value, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.data_value);
				});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.data_type, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.data_type);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TYPED_DATA_Element DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new TYPED_DATA_Element(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TYPED_DATA_Element DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = TYPED_DATA_Element.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out TYPED_DATA_Element? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = TYPED_DATA_Element.DecodeValueFrom(decoder);
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
		private TYPED_DATA_Element(Asn1DerDecoder decoder)
		{
			this.data_type = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.data_value = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
		}
	}

	[Asn1Sequence()]
	partial class PA_ENC_TS_ENC : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PA_ENC_TS_ENC>, IAsn1DerDecodableValue<PA_ENC_TS_ENC>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime patimestamp;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint? pausec;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PA_ENC_TS_ENC(GeneralizedTime patimestamp, uint? pausec = default)
		{
			this.patimestamp = patimestamp;
			this.pausec = pausec;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.pausec is not null)
				encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000001), this.pausec.Value, (encoder, r) =>
				{
					encoder.EncodeUInt32Tlv(this.pausec.Value);
				});
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000000), this.patimestamp, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.patimestamp);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_ENC_TS_ENC DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PA_ENC_TS_ENC(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_ENC_TS_ENC DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PA_ENC_TS_ENC.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PA_ENC_TS_ENC? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PA_ENC_TS_ENC.DecodeValueFrom(decoder);
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
		private PA_ENC_TS_ENC(Asn1DerDecoder decoder)
		{
			this.patimestamp = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeDateTimeTlv());
			this.pausec = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsUInt32()) : default(uint?);
		}
	}

	[Asn1Sequence()]
	partial class ETYPE_INFO_ENTRY : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ETYPE_INFO_ENTRY>, IAsn1DerDecodableValue<ETYPE_INFO_ENTRY>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int etype;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? salt;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ETYPE_INFO_ENTRY(int etype, Byte[]? salt = default)
		{
			this.etype = etype;
			this.salt = salt;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.salt is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000001), this.salt, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.salt);
				});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.etype, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.etype);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ETYPE_INFO_ENTRY DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ETYPE_INFO_ENTRY(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ETYPE_INFO_ENTRY DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ETYPE_INFO_ENTRY.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ETYPE_INFO_ENTRY? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ETYPE_INFO_ENTRY.DecodeValueFrom(decoder);
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
		private ETYPE_INFO_ENTRY(Asn1DerDecoder decoder)
		{
			this.etype = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.salt = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
		}
	}

	[Asn1Sequence()]
	partial class ETYPE_INFO2_ENTRY : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ETYPE_INFO2_ENTRY>, IAsn1DerDecodableValue<ETYPE_INFO2_ENTRY>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int etype;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString? salt;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? s2kparams;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ETYPE_INFO2_ENTRY(int etype, GeneralString? salt = default, Byte[]? s2kparams = default)
		{
			this.etype = etype;
			this.salt = salt;
			this.s2kparams = s2kparams;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.s2kparams is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000002), this.s2kparams, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.s2kparams);
				});
			if (this.salt is not null)
				encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000001), this.salt.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.salt.Value);
				});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.etype, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.etype);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ETYPE_INFO2_ENTRY DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ETYPE_INFO2_ENTRY(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ETYPE_INFO2_ENTRY DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ETYPE_INFO2_ENTRY.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ETYPE_INFO2_ENTRY? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ETYPE_INFO2_ENTRY.DecodeValueFrom(decoder);
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
		private ETYPE_INFO2_ENTRY(Asn1DerDecoder decoder)
		{
			this.etype = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.salt = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeStringTlv<GeneralString>()) : default(GeneralString?);
			this.s2kparams = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
		}
	}

	[Asn1Sequence()]
	partial class AD_KDCIssued : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AD_KDCIssued>, IAsn1DerDecodableValue<AD_KDCIssued>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Checksum ad_checksum;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString? i_realm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName? i_sname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AuthorizationData_Element[] elements;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AD_KDCIssued(Checksum ad_checksum, AuthorizationData_Element[] elements, GeneralString? i_realm = default, PrincipalName? i_sname = default)
		{
			this.ad_checksum = ad_checksum;
			this.i_realm = i_realm;
			this.i_sname = i_sname;
			this.elements = elements;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<AuthorizationData_Element[]>(new Asn1Tag(0xA0000003), this.elements, (encoder, r) =>
			{
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.elements, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			});
			if (this.i_sname is not null)
				encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000002), this.i_sname, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.i_sname);
				});
			if (this.i_realm is not null)
				encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000001), this.i_realm.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.i_realm.Value);
				});
			encoder.EncodeExplicitTlv<Checksum>(new Asn1Tag(0xA0000000), this.ad_checksum, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.ad_checksum);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AD_KDCIssued DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AD_KDCIssued(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AD_KDCIssued DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AD_KDCIssued.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AD_KDCIssued? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AD_KDCIssued.DecodeValueFrom(decoder);
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
		private AD_KDCIssued(Asn1DerDecoder decoder)
		{
			this.ad_checksum = decoder.DecodeTaggedValue<Checksum>(new Asn1Tag(0xA0000000), (encoder) => Checksum.DecodeTlvFrom(decoder));
			this.i_realm = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeStringTlv<GeneralString>()) : default(GeneralString?);
			this.i_sname = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000002), (encoder) => PrincipalName.DecodeTlvFrom(decoder)) : default(PrincipalName);
			this.elements = decoder.DecodeTaggedValue<AuthorizationData_Element[]>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeListTlv<AuthorizationData_Element>(new Asn1Tag(0x20000010), (encoder) => AuthorizationData_Element.DecodeTlvFrom(decoder)));
		}
	}

	[Asn1Sequence()]
	partial class AD_AND_OR : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AD_AND_OR>, IAsn1DerDecodableValue<AD_AND_OR>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int condition_count;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AuthorizationData_Element[] elements;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AD_AND_OR(int condition_count, AuthorizationData_Element[] elements)
		{
			this.condition_count = condition_count;
			this.elements = elements;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<AuthorizationData_Element[]>(new Asn1Tag(0xA0000001), this.elements, (encoder, r) =>
			{
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.elements, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.condition_count, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.condition_count);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AD_AND_OR DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AD_AND_OR(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AD_AND_OR DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AD_AND_OR.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AD_AND_OR? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AD_AND_OR.DecodeValueFrom(decoder);
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
		private AD_AND_OR(Asn1DerDecoder decoder)
		{
			this.condition_count = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.elements = decoder.DecodeTaggedValue<AuthorizationData_Element[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeListTlv<AuthorizationData_Element>(new Asn1Tag(0x20000010), (encoder) => AuthorizationData_Element.DecodeTlvFrom(decoder)));
		}
	}

	[Asn1Sequence()]
	partial class KERB_ERROR_DATA : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KERB_ERROR_DATA>, IAsn1DerDecodableValue<KERB_ERROR_DATA>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger data_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? data_value;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KERB_ERROR_DATA(System.Numerics.BigInteger data_type, Byte[]? data_value = default)
		{
			this.data_type = data_type;
			this.data_value = data_value;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.data_value is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000002), this.data_value, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.data_value);
				});
			encoder.EncodeExplicitTlv<System.Numerics.BigInteger>(new Asn1Tag(0xA0000001), this.data_type, (encoder, r) =>
			{
				encoder.EncodeBigIntegerTlv(this.data_type);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KERB_ERROR_DATA DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KERB_ERROR_DATA(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KERB_ERROR_DATA DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KERB_ERROR_DATA.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KERB_ERROR_DATA? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KERB_ERROR_DATA.DecodeValueFrom(decoder);
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
		private KERB_ERROR_DATA(Asn1DerDecoder decoder)
		{
			this.data_type = decoder.DecodeTaggedValue<System.Numerics.BigInteger>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsBigInteger());
			this.data_value = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
		}
	}

	[Asn1Sequence()]
	partial class KERB_PA_PAC_REQUEST : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KERB_PA_PAC_REQUEST>, IAsn1DerDecodableValue<KERB_PA_PAC_REQUEST>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal bool include_pac;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KERB_PA_PAC_REQUEST(bool include_pac)
		{
			this.include_pac = include_pac;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<bool>(new Asn1Tag(0xA0000000), this.include_pac, (encoder, r) =>
			{
				encoder.EncodeBoolTlv(this.include_pac);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KERB_PA_PAC_REQUEST DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KERB_PA_PAC_REQUEST(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KERB_PA_PAC_REQUEST DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KERB_PA_PAC_REQUEST.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KERB_PA_PAC_REQUEST? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KERB_PA_PAC_REQUEST.DecodeValueFrom(decoder);
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
		private KERB_PA_PAC_REQUEST(Asn1DerDecoder decoder)
		{
			this.include_pac = decoder.DecodeTaggedValue<bool>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeBoolTlv());
		}
	}

	[Asn1Sequence()]
	partial class KERB_AD_RESTRICTION_ENTRY : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KERB_AD_RESTRICTION_ENTRY>, IAsn1DerDecodableValue<KERB_AD_RESTRICTION_ENTRY>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int restriction_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] restriction;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KERB_AD_RESTRICTION_ENTRY(int restriction_type, Byte[] restriction)
		{
			this.restriction_type = restriction_type;
			this.restriction = restriction;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000001), this.restriction, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.restriction);
			});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.restriction_type, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.restriction_type);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KERB_AD_RESTRICTION_ENTRY DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KERB_AD_RESTRICTION_ENTRY(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KERB_AD_RESTRICTION_ENTRY DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KERB_AD_RESTRICTION_ENTRY.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KERB_AD_RESTRICTION_ENTRY? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KERB_AD_RESTRICTION_ENTRY.DecodeValueFrom(decoder);
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
		private KERB_AD_RESTRICTION_ENTRY(Asn1DerDecoder decoder)
		{
			this.restriction_type = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.restriction = decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOctetStringTlv());
		}
	}

	[Asn1Sequence()]
	partial class PA_PAC_OPTIONS : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PA_PAC_OPTIONS>, IAsn1DerDecodableValue<PA_PAC_OPTIONS>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString flags;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PA_PAC_OPTIONS(Asn1BitString flags)
		{
			this.flags = flags;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Asn1BitString>(new Asn1Tag(0xA0000000), this.flags, (encoder, r) =>
			{
				encoder.EncodeBitStringTlv(this.flags);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_PAC_OPTIONS DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PA_PAC_OPTIONS(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_PAC_OPTIONS DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PA_PAC_OPTIONS.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PA_PAC_OPTIONS? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PA_PAC_OPTIONS.DecodeValueFrom(decoder);
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
		private PA_PAC_OPTIONS(Asn1DerDecoder decoder)
		{
			this.flags = decoder.DecodeTaggedValue<Asn1BitString>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeBitStringTlv());
		}
	}

	[Asn1Sequence()]
	partial class KERB_SUPERSEDED_BY_USER : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KERB_SUPERSEDED_BY_USER>, IAsn1DerDecodableValue<KERB_SUPERSEDED_BY_USER>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName name;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString realm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KERB_SUPERSEDED_BY_USER(PrincipalName name, GeneralString realm)
		{
			this.name = name;
			this.realm = realm;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000001), this.realm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.realm);
			});
			encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000000), this.name, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.name);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KERB_SUPERSEDED_BY_USER DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KERB_SUPERSEDED_BY_USER(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KERB_SUPERSEDED_BY_USER DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KERB_SUPERSEDED_BY_USER.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KERB_SUPERSEDED_BY_USER? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KERB_SUPERSEDED_BY_USER.DecodeValueFrom(decoder);
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
		private KERB_SUPERSEDED_BY_USER(Asn1DerDecoder decoder)
		{
			this.name = decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000000), (encoder) => PrincipalName.DecodeTlvFrom(decoder));
			this.realm = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeStringTlv<GeneralString>());
		}
	}

	[Asn1Sequence()]
	partial class KERB_DMSA_KEY_PACKAGE : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KERB_DMSA_KEY_PACKAGE>, IAsn1DerDecodableValue<KERB_DMSA_KEY_PACKAGE>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptionKey[] current_keys;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptionKey[]? previous_keys;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime expiration_interval;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime fetch_interval;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KERB_DMSA_KEY_PACKAGE(EncryptionKey[] current_keys, GeneralizedTime expiration_interval, GeneralizedTime fetch_interval, EncryptionKey[]? previous_keys = default)
		{
			this.current_keys = current_keys;
			this.previous_keys = previous_keys;
			this.expiration_interval = expiration_interval;
			this.fetch_interval = fetch_interval;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000004), this.fetch_interval, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.fetch_interval);
			});
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000002), this.expiration_interval, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.expiration_interval);
			});
			if (this.previous_keys is not null)
				encoder.EncodeExplicitTlv<EncryptionKey[]>(new Asn1Tag(0xA0000001), this.previous_keys, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.previous_keys, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
				});
			encoder.EncodeExplicitTlv<EncryptionKey[]>(new Asn1Tag(0xA0000000), this.current_keys, (encoder, r) =>
			{
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.current_keys, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KERB_DMSA_KEY_PACKAGE DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KERB_DMSA_KEY_PACKAGE(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KERB_DMSA_KEY_PACKAGE DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KERB_DMSA_KEY_PACKAGE.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KERB_DMSA_KEY_PACKAGE? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KERB_DMSA_KEY_PACKAGE.DecodeValueFrom(decoder);
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
		private KERB_DMSA_KEY_PACKAGE(Asn1DerDecoder decoder)
		{
			this.current_keys = decoder.DecodeTaggedValue<EncryptionKey[]>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeListTlv<EncryptionKey>(new Asn1Tag(0x20000010), (encoder) => EncryptionKey.DecodeTlvFrom(decoder)));
			this.previous_keys = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<EncryptionKey[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeListTlv<EncryptionKey>(new Asn1Tag(0x20000010), (encoder) => EncryptionKey.DecodeTlvFrom(decoder))) : default(EncryptionKey[]);
			this.expiration_interval = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeDateTimeTlv());
			this.fetch_interval = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000004), (encoder) => decoder.DecodeDateTimeTlv());
		}
	}

	[Asn1Sequence()]
	partial class PA_FOR_USER : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PA_FOR_USER>, IAsn1DerDecodableValue<PA_FOR_USER>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName userName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString userRealm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Checksum cksum;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString auth_package;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PA_FOR_USER(PrincipalName userName, GeneralString userRealm, Checksum cksum, GeneralString auth_package)
		{
			this.userName = userName;
			this.userRealm = userRealm;
			this.cksum = cksum;
			this.auth_package = auth_package;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000003), this.auth_package, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.auth_package);
			});
			encoder.EncodeExplicitTlv<Checksum>(new Asn1Tag(0xA0000002), this.cksum, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.cksum);
			});
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000001), this.userRealm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.userRealm);
			});
			encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000000), this.userName, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.userName);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_FOR_USER DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PA_FOR_USER(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_FOR_USER DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PA_FOR_USER.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PA_FOR_USER? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PA_FOR_USER.DecodeValueFrom(decoder);
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
		private PA_FOR_USER(Asn1DerDecoder decoder)
		{
			this.userName = decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000000), (encoder) => PrincipalName.DecodeTlvFrom(decoder));
			this.userRealm = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeStringTlv<GeneralString>());
			this.cksum = decoder.DecodeTaggedValue<Checksum>(new Asn1Tag(0xA0000002), (encoder) => Checksum.DecodeTlvFrom(decoder));
			this.auth_package = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeStringTlv<GeneralString>());
		}
	}

	[Asn1Sequence()]
	partial class S4UUserID : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<S4UUserID>, IAsn1DerDecodableValue<S4UUserID>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int nonce;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName? cname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString crealm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? subject_certificate;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString? options;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public S4UUserID(int nonce, GeneralString crealm, PrincipalName? cname = default, Byte[]? subject_certificate = default, Asn1BitString? options = default)
		{
			this.nonce = nonce;
			this.cname = cname;
			this.crealm = crealm;
			this.subject_certificate = subject_certificate;
			this.options = options;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.options is not null)
				encoder.EncodeExplicitTlv<Asn1BitString>(new Asn1Tag(0xA0000004), this.options.Value, (encoder, r) =>
				{
					encoder.EncodeBitStringTlv(this.options.Value);
				});
			if (this.subject_certificate is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000003), this.subject_certificate, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.subject_certificate);
				});
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000002), this.crealm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.crealm);
			});
			if (this.cname is not null)
				encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000001), this.cname, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.cname);
				});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.nonce, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.nonce);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static S4UUserID DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new S4UUserID(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static S4UUserID DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = S4UUserID.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out S4UUserID? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = S4UUserID.DecodeValueFrom(decoder);
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
		private S4UUserID(Asn1DerDecoder decoder)
		{
			this.nonce = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.cname = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000001), (encoder) => PrincipalName.DecodeTlvFrom(decoder)) : default(PrincipalName);
			this.crealm = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeStringTlv<GeneralString>());
			this.subject_certificate = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
			this.options = decoder.CheckTag(new Asn1Tag(0xA0000004)) ? decoder.DecodeTaggedValue<Asn1BitString>(new Asn1Tag(0xA0000004), (encoder) => decoder.DecodeBitStringTlv()) : default(Asn1BitString?);
		}
	}

	[Asn1Sequence()]
	partial class PA_S4U_X509_USER : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PA_S4U_X509_USER>, IAsn1DerDecodableValue<PA_S4U_X509_USER>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal S4UUserID user_id;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Checksum checksum;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PA_S4U_X509_USER(S4UUserID user_id, Checksum checksum)
		{
			this.user_id = user_id;
			this.checksum = checksum;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Checksum>(new Asn1Tag(0xA0000001), this.checksum, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.checksum);
			});
			encoder.EncodeExplicitTlv<S4UUserID>(new Asn1Tag(0xA0000000), this.user_id, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.user_id);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_S4U_X509_USER DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PA_S4U_X509_USER(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_S4U_X509_USER DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PA_S4U_X509_USER.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PA_S4U_X509_USER? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PA_S4U_X509_USER.DecodeValueFrom(decoder);
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
		private PA_S4U_X509_USER(Asn1DerDecoder decoder)
		{
			this.user_id = decoder.DecodeTaggedValue<S4UUserID>(new Asn1Tag(0xA0000000), (encoder) => S4UUserID.DecodeTlvFrom(decoder));
			this.checksum = decoder.DecodeTaggedValue<Checksum>(new Asn1Tag(0xA0000001), (encoder) => Checksum.DecodeTlvFrom(decoder));
		}
	}

	[Asn1Choice()]
	partial class KDC_REQ_CHOICE : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<KDC_REQ_CHOICE>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KDC_REQ Asreq
		{
			get => this.asreq;
			set
			{
				this.asreq = value;
				this._choiceTag = ChoiceIndex.Asreq;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private KDC_REQ? asreq;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KDC_REQ Tgsreq
		{
			get => this.tgsreq;
			set
			{
				this.tgsreq = value;
				this._choiceTag = ChoiceIndex.Tgsreq;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private KDC_REQ? tgsreq;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KDC_REQ_CHOICE()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.Tgsreq:
					Debug.Assert(this.tgsreq is not null);
					encoder.EncodeExplicitTlv<KDC_REQ>(new Asn1Tag(0x6000000C), this.tgsreq, (encoder, r) =>
					{
						encoder.EncodeValueTlv(this.tgsreq);
					});
					break;
				case ChoiceIndex.Asreq:
					Debug.Assert(this.asreq is not null);
					encoder.EncodeExplicitTlv<KDC_REQ>(new Asn1Tag(0x6000000A), this.asreq, (encoder, r) =>
					{
						encoder.EncodeValueTlv(this.asreq);
					});
					break;
				default:
					throw new InvalidOperationException("The object of type KDC-REQ-CHOICE has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KDC_REQ_CHOICE DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!KDC_REQ_CHOICE.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KDC_REQ_CHOICE? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x6000000A)))
				instance = new KDC_REQ_CHOICE()
				{
					_choiceTag = ChoiceIndex.Asreq,
					asreq = decoder.DecodeTaggedValue<KDC_REQ>(new Asn1Tag(0x6000000A), (encoder) => KDC_REQ.DecodeTlvFrom(decoder))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x6000000C)))
				instance = new KDC_REQ_CHOICE()
				{
					_choiceTag = ChoiceIndex.Tgsreq,
					tgsreq = decoder.DecodeTaggedValue<KDC_REQ>(new Asn1Tag(0x6000000C), (encoder) => KDC_REQ.DecodeTlvFrom(decoder))
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
			Asreq = 1610612746U,
			Tgsreq = 1610612748U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Choice()]
	partial class KDC_REP_CHOICE : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<KDC_REP_CHOICE>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KDC_REP Asrep
		{
			get => this.asrep;
			set
			{
				this.asrep = value;
				this._choiceTag = ChoiceIndex.Asrep;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private KDC_REP? asrep;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KDC_REP Tgsrep
		{
			get => this.tgsrep;
			set
			{
				this.tgsrep = value;
				this._choiceTag = ChoiceIndex.Tgsrep;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private KDC_REP? tgsrep;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KRB_ERROR_Tagged30 Error
		{
			get => this.error;
			set
			{
				this.error = value;
				this._choiceTag = ChoiceIndex.Error;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private KRB_ERROR_Tagged30? error;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KDC_REP_CHOICE()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.Error:
					Debug.Assert(this.error is not null);
					encoder.EncodeExplicitTlv<KRB_ERROR_Tagged30>(new Asn1Tag(0x6000001E), this.error, (encoder, r) =>
					{
						encoder.EncodeValueTlv(this.error);
					});
					break;
				case ChoiceIndex.Tgsrep:
					Debug.Assert(this.tgsrep is not null);
					encoder.EncodeExplicitTlv<KDC_REP>(new Asn1Tag(0x6000000D), this.tgsrep, (encoder, r) =>
					{
						encoder.EncodeValueTlv(this.tgsrep);
					});
					break;
				case ChoiceIndex.Asrep:
					Debug.Assert(this.asrep is not null);
					encoder.EncodeExplicitTlv<KDC_REP>(new Asn1Tag(0x6000000B), this.asrep, (encoder, r) =>
					{
						encoder.EncodeValueTlv(this.asrep);
					});
					break;
				default:
					throw new InvalidOperationException("The object of type KDC-REP-CHOICE has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KDC_REP_CHOICE DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!KDC_REP_CHOICE.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KDC_REP_CHOICE? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x6000000B)))
				instance = new KDC_REP_CHOICE()
				{
					_choiceTag = ChoiceIndex.Asrep,
					asrep = decoder.DecodeTaggedValue<KDC_REP>(new Asn1Tag(0x6000000B), (encoder) => KDC_REP.DecodeTlvFrom(decoder))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x6000000D)))
				instance = new KDC_REP_CHOICE()
				{
					_choiceTag = ChoiceIndex.Tgsrep,
					tgsrep = decoder.DecodeTaggedValue<KDC_REP>(new Asn1Tag(0x6000000D), (encoder) => KDC_REP.DecodeTlvFrom(decoder))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x6000001E)))
				instance = new KDC_REP_CHOICE()
				{
					_choiceTag = ChoiceIndex.Error,
					error = decoder.DecodeTaggedValue<KRB_ERROR_Tagged30>(new Asn1Tag(0x6000001E), (encoder) => KRB_ERROR_Tagged30.DecodeTlvFrom(decoder))
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
			Asrep = 1610612747U,
			Tgsrep = 1610612749U,
			Error = 1610612766U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class ChangePasswdData : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ChangePasswdData>, IAsn1DerDecodableValue<ChangePasswdData>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] newpasswd;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName? targname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString? targrealm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ChangePasswdData(Byte[] newpasswd, PrincipalName? targname = default, GeneralString? targrealm = null)
		{
			this.newpasswd = newpasswd;
			this.targname = targname;
			this.targrealm = targrealm;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.targrealm is not null)
				encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000002), this.targrealm.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.targrealm.Value);
				});
			if (this.targname is not null)
				encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000001), this.targname, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.targname);
				});
			encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000000), this.newpasswd, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.newpasswd);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ChangePasswdData DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ChangePasswdData(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ChangePasswdData DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ChangePasswdData.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ChangePasswdData? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ChangePasswdData.DecodeValueFrom(decoder);
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
		private ChangePasswdData(Asn1DerDecoder decoder)
		{
			this.newpasswd = decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeOctetStringTlv());
			this.targname = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000001), (encoder) => PrincipalName.DecodeTlvFrom(decoder)) : default(PrincipalName);
			this.targrealm = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeStringTlv<GeneralString>()) : default(GeneralString?);
		}
	}
}