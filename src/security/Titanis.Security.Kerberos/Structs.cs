using KerberosPreauthFramework;
using KerberosV5Spec2;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

namespace Titanis.Security.Kerberos
{
	static class Structs
	{
		internal static KerberosV5Spec2.HostAddress HostAddress(AddressType type, string netbiosName)
		{
			Debug.Assert(!string.IsNullOrEmpty(netbiosName));
			return new KerberosV5Spec2.HostAddress(
				(int)type,
				Encoding.UTF8.GetBytes(netbiosName)
			);
		}

		#region PA_DATA
		// [MS-KILE] § 2.2.3 - KERB-PA-PAC-REQUEST
		internal static PA_DATA PAData_PacRequest(bool includePac)
			=> PAData(PadataType.PacRequest, new KERB_PA_PAC_REQUEST(includePac));

		internal static PA_DATA PAData(PadataType patype, byte[] value) => new PA_DATA((int)patype, value);
		internal static PA_DATA PAData<TPadata>(
			PadataType patype,
			in TPadata padata
			)
			where TPadata : IAsn1DerEncodableTlv
			=> PAData(patype, Asn1DerEncoder.EncodeTlv(padata).ToArray());

		internal static PA_DATA PAData_APReq(AP_REQ apreq)
			=> PAData(PadataType.TgsReq, apreq);

		internal static PA_DATA PAData_FastReq(PA_FX_FAST_REQUEST fastReq)
			=> PAData(PadataType.FxFast, fastReq);

		internal static PA_DATA PAData_FastCookie(byte[] cookie)
			=> PAData(PadataType.FxCookie, cookie);

		internal static PA_DATA PAData_PacOptions(PacOptions options)
			=> PAData(PadataType.PacOptions, new PA_PAC_OPTIONS(new Asn1BitString((uint)options)));

		internal static PA_DATA PAData_KerbKeyListReq(EType[] etypes)
		{
			return PAData(PadataType.KerbKeyListReq, new KerbKeyListRequest(etypes));
		}

		internal static PA_DATA PAData_TSEnc(PadataType type, byte[] encts)
			=> PAData(type, encts);

		internal static PA_ENC_TS_ENC PAEnc_TSEnc(TimeSpan skew)
		{
			var now = KerberosTime.Now(skew);
			return new PA_ENC_TS_ENC(now.dt, now.usec);
		}
		#endregion

		internal static EncryptedData EncryptedData(EType etype, byte[] cipher) => new EncryptedData((int)etype, cipher);
		internal static EncryptionKey EncryptionKey(EType etype, byte[] keyBytes) => new EncryptionKey((int)etype, keyBytes);

		internal static PrincipalName PrincipalName(PrincipalNameType nameType, string name) => new PrincipalName((int)nameType, new GeneralString[] { name });

		internal static KerberosV5Spec2.PrincipalName PrincipalName(PrincipalNameType nameType, string name, string instance) => new PrincipalName((int)nameType, new GeneralString[] { name, instance });

		internal static KerberosV5Spec2.PrincipalName PrincipalName(PrincipalNameType nameType, string[] nameParts) => new PrincipalName((int)nameType, Array.ConvertAll(nameParts, r => new GeneralString(r)));

		internal static KerberosV5Spec2.PrincipalName PrincipalName(this SecurityPrincipalName spn) => new PrincipalName((int)spn.NameType, Array.ConvertAll(spn.GetNameParts(), r => new GeneralString(r)));


		internal static KDC_REQ_BODY KdcReqBody(
			TicketParameters ticketParameters,
			KerberosV5Spec2.PrincipalName? cname,
			string crealm,
			KerberosV5Spec2.PrincipalName? sname,
			int nonce,
			int[] etypes,
			KerberosV5Spec2.HostAddress[]? hostAddresses,
			EncryptedData? authzData
			)
			=> new KDC_REQ_BODY(
				new Asn1BitString((uint)ticketParameters.Options),
				crealm,
				ticketParameters.EndTime ?? TicketParameters.DefaultEndTime,
				nonce,
				etypes,
				cname,
				sname,
				ticketParameters.StartTime,
				ticketParameters.RenewTill,
				hostAddresses,
				authzData,
				additional_tickets: (ticketParameters.addlTicketStruc != null) ? [ticketParameters.addlTicketStruc] : null
			);

		internal static Checksum Checksum(EncChecksumType type, byte[] value)
			=> new Checksum((int)type, value);

		internal static Authenticator Authenticator(
			KerberosV5Spec2.PrincipalName cname,
			string crealm,
			Checksum? cksum,
			KerberosTime now,
			int seqnbr,
			EncryptionKey? subkey
			)
		{
			return new Authenticator(new Authenticator_Tagged2(
				5,
				crealm,
				cname,
				now.usec,
				now.dt,
				cksum,
				subkey,
				seqnbr
				));
		}

		internal static KDC_REQ TgsReq(
			PA_DATA[] padatas,
			KDC_REQ_BODY reqBody
			)
			=> new KDC_REQ(
				5,
				(byte)KrbMessageType.Tgsreq,
				reqBody,
				padatas
			);

		internal static KDC_REQ ASReq(
			PA_DATA[] padata,
			KDC_REQ_BODY reqBody
			)
			=> new KDC_REQ(
				5,
				(byte)KrbMessageType.Asreq,
				reqBody,
				padata
			);

		internal static AP_REQ APReq(
			APOptions options,
			Ticket_Tagged1 ticket,
			EncryptedData authenticator
			)
			=> new AP_REQ(new AP_REQ_Tagged14(
				5,
				(byte)KrbMessageType.Apreq,
				new Asn1BitString((uint)options),
				ticket,
				authenticator
				));

		internal static ETYPE_INFO2_ENTRY PAData_ETypeInfo2Entry(EType etype, string? salt) => new ETYPE_INFO2_ENTRY((int)etype, (salt is null) ? null : new GeneralString(salt));
		internal static PA_DATA PAData_ETypeInfo2(params ETYPE_INFO2_ENTRY[] entries) => new PA_DATA((int)PadataType.ETypeInfo2, Asn1DerEncoder.EncodeTlv(new Asn1SequenceOf<ETYPE_INFO2_ENTRY>(entries)).ToArray());
	}
}
