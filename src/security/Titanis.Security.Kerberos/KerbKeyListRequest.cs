using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

namespace Titanis.Security.Kerberos
{
	// [MS-KILE] § 2.2.11 KERB-KEY-LIST-REQ
	class KerbKeyListRequest : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<KerbKeyListRequest>, IAsn1DerDecodableValue<KerbKeyListRequest>
	{
		public KerbKeyListRequest(EType[] etypes)
		{
			this.ETypes = etypes;
		}
		private KerbKeyListRequest(Asn1DerDecoder decoder)
		{
			List<EType> etypes = new List<EType>();
			while (!decoder.IsEndOfTuple)
			{
				var etype = (EType)decoder.DecodeIntegerTlvAsInt32();
				etypes.Add(etype);
			}
			this.ETypes = etypes.ToArray();
		}

		public EType[] ETypes { get; }

		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		public static KerbKeyListRequest DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = KerbKeyListRequest.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		public static KerbKeyListRequest DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var req = new KerbKeyListRequest(decoder);
			return req;
		}

		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out KerbKeyListRequest? value)
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
			for (int i = this.ETypes.Length - 1; i >= 0; i--)
			{
				var etype = this.ETypes[i];
				encoder.EncodeInt32Tlv((int)etype);
			}
		}
	}
}
