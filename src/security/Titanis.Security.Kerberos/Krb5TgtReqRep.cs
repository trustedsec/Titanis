using KerberosV5Spec2;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

namespace Titanis.Security.Kerberos
{
	partial class TGT_REQ : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<TGT_REQ>, IAsn1DerDecodableValue<TGT_REQ>
	{
		internal const string U2uOid = "1.2.840.113554.1.2.2.3";

		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		internal byte pvno = 5;
		internal byte msg_type = (byte)KrbMessageType.Tgtreq;
		internal PrincipalName serverName;
		internal GeneralString realm;

		public TGT_REQ(PrincipalName serverName, GeneralString realm)
		{
			this.serverName = serverName;
			this.realm = realm;
		}
		private TGT_REQ(Asn1DerDecoder decoder)
		{
			this.pvno = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.msg_type = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.serverName = decoder.DecodeTaggedValue(new Asn1Tag(0xA0000002), (encoder) => PrincipalName.DecodeTlvFrom(decoder));
			this.realm = decoder.DecodeTaggedValue(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeStringTlv<GeneralString>());
		}

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000003), this.realm, (encoder, r) =>
			{
				encoder.EncodeStringTlv(r);
			});
			encoder.EncodeExplicitTlv(new Asn1Tag(0xA0000002), this.serverName, (encoder, r) =>
			{
				encoder.EncodeValueTlv(r);
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

		public static TGT_REQ DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out TGT_REQ? value)
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

		public static TGT_REQ DecodeValueFrom(Asn1DerDecoder decoder)
		{
			return new TGT_REQ(decoder);
		}
	}

	partial class TGT_REP : IAsn1DerDecodableTlv<TGT_REP>, IAsn1DerDecodableValue<TGT_REP>, IAsn1DerEncodableValue, IAsn1DerEncodableTlv
	{

		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		internal byte pvno = 5;
		internal byte msg_type = (byte)KrbMessageType.Tgtrep;
		internal Ticket_Tagged1 ticket;

		private TGT_REP(Asn1DerDecoder decoder)
		{
			this.pvno = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.msg_type = decoder.DecodeTaggedValue<byte>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeIntegerTlvAsByte());
			this.ticket = decoder.DecodeTaggedValue<Ticket_Tagged1>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeTaggedValue(new Asn1Tag(0x60000001), (encoder) => Ticket_Tagged1.DecodeTlvFrom(decoder)));

		}

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Ticket_Tagged1>(new Asn1Tag(0xA0000002), this.ticket, (encoder, r) =>
			{
				encoder.EncodeExplicitTlv<Ticket_Tagged1>(new Asn1Tag(0x60000001), this.ticket, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.ticket);
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

		public static TGT_REP DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out TGT_REP? value)
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

		public static TGT_REP DecodeValueFrom(Asn1DerDecoder decoder)
		{
			return new TGT_REP(decoder);
		}
	}
}
