namespace KerberosPreauthFramework
{
	using System;
	using System.CodeDom.Compiler;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using Titanis.Asn1;
	using Titanis.Asn1.Metadata;
	using Titanis.Asn1.Serialization;
	using Checksum = KerberosV5Spec2.Checksum;
	using EncryptedData = KerberosV5Spec2.EncryptedData;
	using PA_DATA = KerberosV5Spec2.PA_DATA;
	using KDC_REQ_BODY = KerberosV5Spec2.KDC_REQ_BODY;
	using EncryptionKey = KerberosV5Spec2.EncryptionKey;
	using PrincipalName = KerberosV5Spec2.PrincipalName;

	partial class KerberosPreauthFrameworkModule
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private KerberosPreauthFrameworkModule()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private static KerberosPreauthFrameworkModule _instance = new KerberosPreauthFrameworkModule();
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KerberosPreauthFrameworkModule Instance => _instance;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Name => "KerberosPreauthFramework";

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Oid => "1.3.6.1.5.2.4.3";
	}

	[Asn1Sequence()]
	partial class PA_AUTHENTICATION_SET_ELEM : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PA_AUTHENTICATION_SET_ELEM>, IAsn1DerDecodableValue<PA_AUTHENTICATION_SET_ELEM>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int pa_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? pa_hint;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? pa_value;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PA_AUTHENTICATION_SET_ELEM(int pa_type, Byte[]? pa_hint = default, Byte[]? pa_value = default)
		{
			this.pa_type = pa_type;
			this.pa_hint = pa_hint;
			this.pa_value = pa_value;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.pa_value is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000002), this.pa_value, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.pa_value);
				});
			if (this.pa_hint is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000001), this.pa_hint, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.pa_hint);
				});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.pa_type, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.pa_type);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_AUTHENTICATION_SET_ELEM DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PA_AUTHENTICATION_SET_ELEM(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_AUTHENTICATION_SET_ELEM DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PA_AUTHENTICATION_SET_ELEM.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PA_AUTHENTICATION_SET_ELEM? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PA_AUTHENTICATION_SET_ELEM.DecodeValueFrom(decoder);
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
		private PA_AUTHENTICATION_SET_ELEM(Asn1DerDecoder decoder)
		{
			this.pa_type = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.pa_hint = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
			this.pa_value = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
		}
	}

	[Asn1Sequence()]
	partial class KrbFastArmor : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KrbFastArmor>, IAsn1DerDecodableValue<KrbFastArmor>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal int armor_type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] armor_value;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KrbFastArmor(int armor_type, Byte[] armor_value)
		{
			this.armor_type = armor_type;
			this.armor_value = armor_value;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000001), this.armor_value, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(this.armor_value);
			});
			encoder.EncodeExplicitTlv<int>(new Asn1Tag(0xA0000000), this.armor_type, (encoder, r) =>
			{
				encoder.EncodeInt32Tlv(this.armor_type);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KrbFastArmor DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KrbFastArmor(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KrbFastArmor DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KrbFastArmor.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KrbFastArmor? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KrbFastArmor.DecodeValueFrom(decoder);
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
		private KrbFastArmor(Asn1DerDecoder decoder)
		{
			this.armor_type = decoder.DecodeTaggedValue<int>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsInt32());
			this.armor_value = decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOctetStringTlv());
		}
	}

	[Asn1Sequence()]
	partial class KrbFastArmoredReq : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KrbFastArmoredReq>, IAsn1DerDecodableValue<KrbFastArmoredReq>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal KrbFastArmor? armor;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Checksum req_checksum;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptedData enc_fast_req;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KrbFastArmoredReq(Checksum req_checksum, EncryptedData enc_fast_req, KrbFastArmor? armor = default)
		{
			this.armor = armor;
			this.req_checksum = req_checksum;
			this.enc_fast_req = enc_fast_req;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<EncryptedData>(new Asn1Tag(0xA0000002), this.enc_fast_req, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.enc_fast_req);
			});
			encoder.EncodeExplicitTlv<Checksum>(new Asn1Tag(0xA0000001), this.req_checksum, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.req_checksum);
			});
			if (this.armor is not null)
				encoder.EncodeExplicitTlv<KrbFastArmor>(new Asn1Tag(0xA0000000), this.armor, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.armor);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KrbFastArmoredReq DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KrbFastArmoredReq(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KrbFastArmoredReq DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KrbFastArmoredReq.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KrbFastArmoredReq? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KrbFastArmoredReq.DecodeValueFrom(decoder);
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
		private KrbFastArmoredReq(Asn1DerDecoder decoder)
		{
			this.armor = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeTaggedValue<KrbFastArmor>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<KrbFastArmor>()) : default(KrbFastArmor);
			this.req_checksum = decoder.DecodeTaggedValue<Checksum>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<Checksum>());
			this.enc_fast_req = decoder.DecodeTaggedValue<EncryptedData>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeTlv<EncryptedData>());
		}
	}

	[Asn1Choice()]
	partial class PA_FX_FAST_REQUEST : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<PA_FX_FAST_REQUEST>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KrbFastArmoredReq Armored_data
		{
			get => this.armored_data;
			set
			{
				this.armored_data = value;
				this._choiceTag = ChoiceIndex.Armored_data;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private KrbFastArmoredReq? armored_data;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PA_FX_FAST_REQUEST()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.Armored_data:
					Debug.Assert(this.armored_data is not null);
					encoder.EncodeExplicitTlv<KrbFastArmoredReq>(new Asn1Tag(0xA0000000), this.armored_data, (encoder, r) =>
					{
						encoder.EncodeValueTlv(this.armored_data);
					});
					break;
				default:
					throw new InvalidOperationException("The object of type PA-FX-FAST-REQUEST has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_FX_FAST_REQUEST DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!PA_FX_FAST_REQUEST.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PA_FX_FAST_REQUEST? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0xA0000000)))
				instance = new PA_FX_FAST_REQUEST()
				{
					_choiceTag = ChoiceIndex.Armored_data,
					armored_data = decoder.DecodeTaggedValue<KrbFastArmoredReq>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<KrbFastArmoredReq>())
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
			Armored_data = 0xA0000000
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class KrbFastReq : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KrbFastReq>, IAsn1DerDecodableValue<KrbFastReq>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString fast_options;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PA_DATA[] padata;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal KDC_REQ_BODY req_body;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KrbFastReq(Asn1BitString fast_options, PA_DATA[] padata, KDC_REQ_BODY req_body)
		{
			this.fast_options = fast_options;
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
			encoder.EncodeExplicitTlv<KDC_REQ_BODY>(new Asn1Tag(0xA0000002), this.req_body, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.req_body);
			});
			encoder.EncodeExplicitTlv<PA_DATA[]>(new Asn1Tag(0xA0000001), this.padata, (encoder, r) =>
			{
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.padata, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			});
			encoder.EncodeExplicitTlv<Asn1BitString>(new Asn1Tag(0xA0000000), this.fast_options, (encoder, r) =>
			{
				encoder.EncodeBitStringTlv(this.fast_options);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KrbFastReq DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KrbFastReq(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KrbFastReq DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KrbFastReq.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KrbFastReq? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KrbFastReq.DecodeValueFrom(decoder);
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
		private KrbFastReq(Asn1DerDecoder decoder)
		{
			this.fast_options = decoder.DecodeTaggedValue<Asn1BitString>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeBitStringTlv());
			this.padata = decoder.DecodeTaggedValue<PA_DATA[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeListTlv<PA_DATA>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<PA_DATA>()));
			this.req_body = decoder.DecodeTaggedValue<KDC_REQ_BODY>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeTlv<KDC_REQ_BODY>());
		}
	}

	[Asn1Sequence()]
	partial class KrbFastArmoredRep : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KrbFastArmoredRep>, IAsn1DerDecodableValue<KrbFastArmoredRep>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptedData enc_fast_rep;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KrbFastArmoredRep(EncryptedData enc_fast_rep)
		{
			this.enc_fast_rep = enc_fast_rep;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<EncryptedData>(new Asn1Tag(0xA0000000), this.enc_fast_rep, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.enc_fast_rep);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KrbFastArmoredRep DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KrbFastArmoredRep(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KrbFastArmoredRep DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KrbFastArmoredRep.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KrbFastArmoredRep? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KrbFastArmoredRep.DecodeValueFrom(decoder);
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
		private KrbFastArmoredRep(Asn1DerDecoder decoder)
		{
			this.enc_fast_rep = decoder.DecodeTaggedValue<EncryptedData>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<EncryptedData>());
		}
	}

	[Asn1Choice()]
	partial class PA_FX_FAST_REPLY : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<PA_FX_FAST_REPLY>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KrbFastArmoredRep Armored_data
		{
			get => this.armored_data;
			set
			{
				this.armored_data = value;
				this._choiceTag = ChoiceIndex.Armored_data;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private KrbFastArmoredRep? armored_data;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PA_FX_FAST_REPLY()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.Armored_data:
					Debug.Assert(this.armored_data is not null);
					encoder.EncodeExplicitTlv<KrbFastArmoredRep>(new Asn1Tag(0xA0000000), this.armored_data, (encoder, r) =>
					{
						encoder.EncodeValueTlv(this.armored_data);
					});
					break;
				default:
					throw new InvalidOperationException("The object of type PA-FX-FAST-REPLY has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PA_FX_FAST_REPLY DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!PA_FX_FAST_REPLY.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PA_FX_FAST_REPLY? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0xA0000000)))
				instance = new PA_FX_FAST_REPLY()
				{
					_choiceTag = ChoiceIndex.Armored_data,
					armored_data = decoder.DecodeTaggedValue<KrbFastArmoredRep>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<KrbFastArmoredRep>())
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
			Armored_data = 0xA0000000
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class KrbFastFinished : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KrbFastFinished>, IAsn1DerDecodableValue<KrbFastFinished>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime timestamp;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint usec;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString crealm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PrincipalName cname;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Checksum ticket_checksum;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KrbFastFinished(GeneralizedTime timestamp, uint usec, GeneralString crealm, PrincipalName cname, Checksum ticket_checksum)
		{
			this.timestamp = timestamp;
			this.usec = usec;
			this.crealm = crealm;
			this.cname = cname;
			this.ticket_checksum = ticket_checksum;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Checksum>(new Asn1Tag(0xA0000004), this.ticket_checksum, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.ticket_checksum);
			});
			encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000003), this.cname, (encoder, r) =>
			{
				encoder.EncodeValueTlv(this.cname);
			});
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000002), this.crealm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.crealm);
			});
			encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000001), this.usec, (encoder, r) =>
			{
				encoder.EncodeUInt32Tlv(this.usec);
			});
			encoder.EncodeExplicitTlv<GeneralizedTime>(new Asn1Tag(0xA0000000), this.timestamp, (encoder, r) =>
			{
				encoder.EncodeDateTimeTlv(this.timestamp);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KrbFastFinished DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KrbFastFinished(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KrbFastFinished DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KrbFastFinished.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KrbFastFinished? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KrbFastFinished.DecodeValueFrom(decoder);
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
		private KrbFastFinished(Asn1DerDecoder decoder)
		{
			this.timestamp = decoder.DecodeTaggedValue<GeneralizedTime>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeDateTimeTlv());
			this.usec = decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsUInt32());
			this.crealm = decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeStringTlv<GeneralString>());
			this.cname = decoder.DecodeTaggedValue<PrincipalName>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeTlv<PrincipalName>());
			this.ticket_checksum = decoder.DecodeTaggedValue<Checksum>(new Asn1Tag(0xA0000004), (encoder) => decoder.DecodeTlv<Checksum>());
		}
	}

	[Asn1Sequence()]
	partial class KrbFastResponse : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KrbFastResponse>, IAsn1DerDecodableValue<KrbFastResponse>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PA_DATA[] padata;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal EncryptionKey? strengthen_key;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal KrbFastFinished? finished;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint nonce;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public KrbFastResponse(PA_DATA[] padata, uint nonce, EncryptionKey? strengthen_key = default, KrbFastFinished? finished = default)
		{
			this.padata = padata;
			this.strengthen_key = strengthen_key;
			this.finished = finished;
			this.nonce = nonce;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<uint>(new Asn1Tag(0xA0000003), this.nonce, (encoder, r) =>
			{
				encoder.EncodeUInt32Tlv(this.nonce);
			});
			if (this.finished is not null)
				encoder.EncodeExplicitTlv<KrbFastFinished>(new Asn1Tag(0xA0000002), this.finished, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.finished);
				});
			if (this.strengthen_key is not null)
				encoder.EncodeExplicitTlv<EncryptionKey>(new Asn1Tag(0xA0000001), this.strengthen_key, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.strengthen_key);
				});
			encoder.EncodeExplicitTlv<PA_DATA[]>(new Asn1Tag(0xA0000000), this.padata, (encoder, r) =>
			{
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.padata, (encoder, r) =>
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
		public static KrbFastResponse DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new KrbFastResponse(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static KrbFastResponse DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KrbFastResponse.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KrbFastResponse? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = KrbFastResponse.DecodeValueFrom(decoder);
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
		private KrbFastResponse(Asn1DerDecoder decoder)
		{
			this.padata = decoder.DecodeTaggedValue<PA_DATA[]>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeListTlv<PA_DATA>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<PA_DATA>()));
			this.strengthen_key = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<EncryptionKey>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<EncryptionKey>()) : default(EncryptionKey);
			this.finished = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<KrbFastFinished>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeTlv<KrbFastFinished>()) : default(KrbFastFinished);
			this.nonce = decoder.DecodeTaggedValue<uint>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeIntegerTlvAsUInt32());
		}
	}
}