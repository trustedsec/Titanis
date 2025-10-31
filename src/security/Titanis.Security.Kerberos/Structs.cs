using KerberosV5Spec2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

namespace Titanis.Security.Kerberos
{
	static class Structs
	{
		internal static HostAddress HostAddress(AddressType type, string netbiosName)
		{
			Debug.Assert(!string.IsNullOrEmpty(netbiosName));
			return new HostAddress(
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

		internal static PA_DATA PAData_APRep(AP_REQ apreq)
			=> PAData(PadataType.TgsReq, apreq);

		internal static PA_DATA PAData_PacOptions(PacOptions options)
			=> PAData(PadataType.PacOptions, new PA_PAC_OPTIONS(new Asn1BitString((uint)options)));

		internal static PA_DATA PAData_TSEnc(byte[] encts)
			=> PAData(PadataType.EncTimestamp, encts);

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

		internal static KerberosV5Spec2.PrincipalName PrincipalName(SecurityPrincipalName spn) => new PrincipalName((int)spn.NameType, Array.ConvertAll(spn.GetNameParts(), r => new GeneralString(r)));


		internal static KDC_REQ_BODY KdcReqBody(
			TicketParameters ticketParameters,
			KerberosV5Spec2.PrincipalName cname,
			string crealm,
			KerberosV5Spec2.PrincipalName sname,
			int nonce,
			int[] etypes,
			HostAddress[]? hostAddresses
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
				hostAddresses
			);

		internal static Checksum Checksum(EncChecksumType type, byte[] value)
			=> new Checksum((int)type, value);

		internal static Authenticator Authenticator(
			KerberosV5Spec2.PrincipalName cname,
			string crealm,
			Checksum cksum,
			int seqnbr,
			EncryptionKey subkey
			)
		{
			var now = KerberosTime.Now();
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

	}
}
