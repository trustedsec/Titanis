using System;
using System.Collections.Generic;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.IO;

namespace Titanis.Security.Kerberos.Asn1
{
	class Krb5Token : IAsn1DerEncodableTlv
	{
		public Asn1Tag Tag => new Asn1Tag(0x60);

		public Asn1Oid mechId;
		public GssapiTokenId tokenId;
		public KerberosV5Spec2.AP_REQ apreq;
		public KerberosV5Spec2.AP_REP aprep;
		public KerberosV5Spec2.KRB_ERROR_Unnamed_12 error;

		public void DecodeTlv(Asn1DerDecoder decoder)
		{
			var end = decoder.DecodeTlvStart(new Asn1Tag(0x60));
			this.DecodeValue(decoder);
			decoder.CloseTlv(end);
		}

		public void DecodeValue(Asn1DerDecoder decoder)
		{
			var end_mechId = decoder.DecodeTlvStart(Asn1PredefTag.ObjectIdentifier);
			this.mechId = decoder.DecodeOid();
			decoder.CloseTlv(end_mechId);

			var reader = decoder.GetReader();
			this.tokenId = (GssapiTokenId)reader.ReadUInt16BE();
			switch (this.tokenId)
			{
				case GssapiTokenId.APReq:
					this.apreq = new KerberosV5Spec2.AP_REQ();
					this.apreq.DecodeTlv(decoder);
					break;
				case GssapiTokenId.APRep:
					this.aprep = new KerberosV5Spec2.AP_REP();
					this.aprep.DecodeTlv(decoder);
					break;
				case GssapiTokenId.Error:
					this.error = new KerberosV5Spec2.KRB_ERROR_Unnamed_12();
					this.error.DecodeTlv(decoder);
					break;
				default:
					throw new FormatException(string.Format(Messages.Krb5_GssapiTokenIdUnknown, (byte)this.tokenId));
			}
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			switch (this.tokenId)
			{
				case GssapiTokenId.APReq:
					encoder.EncodeObjTlv(this.apreq);
					break;
				case GssapiTokenId.APRep:
					encoder.EncodeObjTlv(this.aprep);
					break;
				case GssapiTokenId.Error:
					encoder.EncodeObjTlv(this.error);
					break;
				default:
					throw new FormatException(string.Format(Messages.Krb5_GssapiTokenIdUnknown, (byte)this.tokenId));
			}
			encoder.GetWriter().WriteUInt16BE((ushort)this.tokenId);

			int end_mechId = encoder.Position;
			encoder.EncodeOid(this.mechId);
			encoder.EncodeCloseTlvHeader(Asn1PredefTag.ObjectIdentifier, end_mechId);
		}

		public bool TryDecodeTlv(Asn1DerDecoder decoder)
		{
			if (decoder.CheckTag(new Asn1Tag(0x60)))
			{
				this.DecodeTlv(decoder);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
