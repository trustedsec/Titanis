using KerberosV5Spec2;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.IO;

namespace Titanis.Security.Kerberos.Asn1
{
	class Krb5Token : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<Krb5Token>
	{
		public Asn1Oid mechId;
		public GssapiTokenId tokenId;
		public AP_REQ apreq;
		public AP_REP aprep;
		public TGT_REQ tgtreq;
		public TGT_REP tgtrep;

		public KRB_ERROR_Tagged30 error;

		static Krb5Token IAsn1DerDecodableTlv<Krb5Token>.DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var frame = decoder.DecodeTlvStart(new Asn1Tag(0x40000000));

			Krb5Token token = new Krb5Token
			{
				mechId = decoder.DecodeOidTlv()
			};
			var reader = decoder.GetReader();
			token.tokenId = (GssapiTokenId)reader.ReadUInt16BE();
			switch (token.tokenId)
			{
				case GssapiTokenId.APReq:
					token.apreq = decoder.DecodeTlv<AP_REQ>();
					break;
				case GssapiTokenId.APRep:
					token.aprep = decoder.DecodeTlv<AP_REP>();
					break;
				case GssapiTokenId.TgtReq:
					token.tgtreq = decoder.DecodeTlv<TGT_REQ>();
					break;
				case GssapiTokenId.TgtRep:
					token.tgtrep = decoder.DecodeTlv<TGT_REP>();
					break;
				case GssapiTokenId.Error:
					token.error = decoder.DecodeTlv<KRB_ERROR>().Value;
					break;
				default:
					throw new FormatException(string.Format(Messages.Krb5_GssapiTokenIdUnknown, (ushort)token.tokenId));
			}

			decoder.CloseTlv(frame);

			return token;
		}

		static bool IAsn1DerDecodableTlv<Krb5Token>.TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Krb5Token? value)
		{
			if (decoder.CheckTag(new Asn1Tag(0x40000000)))
			{
				value = decoder.DecodeTlv<Krb5Token>();
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}

		public Asn1Tag Tag => new Asn1Tag(0x60000000);

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			var pos = encoder.Position;

			EncodeValue(encoder);

			encoder.EncodeCloseTlvHeader(this.Tag, pos);
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			switch (this.tokenId)
			{
				case GssapiTokenId.APReq:
					encoder.EncodeValueTlv(this.apreq);
					break;
				case GssapiTokenId.APRep:
					encoder.EncodeValueTlv(this.aprep);
					break;
				case GssapiTokenId.TgtReq:
					encoder.EncodeValueTlv(this.tgtreq);
					break;
					// TODO: Encode TGT_REP
				case GssapiTokenId.Error:
					encoder.EncodeValueTlv(this.error);
					break;
				default:
					throw new FormatException(string.Format(Messages.Krb5_GssapiTokenIdUnknown, (byte)this.tokenId));
			}
			encoder.GetWriter().WriteUInt16BE((ushort)this.tokenId);

			encoder.EncodeOidTlv(this.mechId);
		}
	}
}
