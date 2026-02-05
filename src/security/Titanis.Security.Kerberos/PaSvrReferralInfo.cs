using KerberosV5Spec2;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

namespace Titanis.Security.Kerberos
{
	// [RFC 6806]
	class PaSvrReferralInfo : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PaSvrReferralInfo>, IAsn1DerDecodableValue<PaSvrReferralInfo>
	{
		public PaSvrReferralInfo(PrincipalName name, GeneralString realm)
		{
			this.Name = name;
			this.Realm = realm;
		}
		private PaSvrReferralInfo(Asn1DerDecoder decoder)
		{
			if (decoder.CheckTag(new Asn1Tag(0xA0000001)))
				this.Name = decoder.DecodeExplicitTaggedTlv<PrincipalName>(new Asn1Tag(0xA0000001));

			this.Realm = decoder.DecodeTaggedValue(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeStringTlv<GeneralString>());
		}

		public PrincipalName? Name { get; }
		public GeneralString Realm { get; }

		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		public static PaSvrReferralInfo DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PaSvrReferralInfo.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		public static PaSvrReferralInfo DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var req = new PaSvrReferralInfo(decoder);
			return req;
		}

		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PaSvrReferralInfo? value)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				value = DecodeTlvFrom(decoder);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			var endpos = encoder.Position;
			this.EncodeValue(encoder);
			encoder.EncodeCloseTlvHeader(this.Tag, endpos);
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			var endpos = encoder.Position;
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000000), this.Realm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(this.Realm);
			});
			if (this.Name != null)
			{
				encoder.EncodeExplicitTlv<PrincipalName>(new Asn1Tag(0xA0000000), this.Name, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.Name);
				});
			}

			encoder.EncodeCloseTlvHeader(new Asn1Tag(0x20000010), endpos);
		}
	}
}
