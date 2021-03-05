using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Security.Kerberos.Asn1.KerberosV5Spec2;

namespace Titanis.Security.Kerberos
{
	static class Structs
	{
		internal static HostAddress HostAddress(AddressType type, string netbiosName)
		{
			Debug.Assert(!string.IsNullOrEmpty(netbiosName));
			return new HostAddress
			{
				addr_type = (int)type,
				address = Encoding.UTF8.GetBytes(netbiosName)
			};
		}

		#region PA_DATA
		// [MS-KILE] § 2.2.3 - KERB-PA-PAC-REQUEST
		internal static PA_DATA PAData_PacRequest(bool includePac)
			=> PAData(PadataType.PacRequest, new KERB_PA_PAC_REQUEST
			{
				include_pac = includePac
			});

		internal static PA_DATA PAData(
			PadataType patype,
			byte[] value
			)
			=> new PA_DATA
			{
				padata_type = (int)patype,
				padata_value = value
			};

		internal static PA_DATA PAData<TPadata>(
			PadataType patype,
			in TPadata padata
			)
			where TPadata : IAsn1DerEncodableTlv, new()
			=> PAData(patype, Asn1DerEncoder.EncodeTlv(padata).ToArray());

		internal static PA_DATA PAData_APRep(AP_REQ apreq)
			=> PAData(PadataType.TgsReq, apreq);

		internal static PA_DATA PAData_PacOptions(PacOptions options)
			=> PAData(PadataType.PacOptions, new PA_PAC_OPTIONS
			{
				flags = new Asn1BitString((uint)options)
			});

		internal static PA_DATA PAData_TSEnc(byte[] encts)
			=> PAData(PadataType.EncTimestamp, encts);

		internal static PA_ENC_TS_ENC PAEnc_TSEnc(TimeSpan skew)
		{
			var now = KerberosTime.Now(skew);
			return new PA_ENC_TS_ENC
			{
				patimestamp = now.dt,
				pausec = now.usec
			};
		}
		#endregion

		internal static EncryptedData EncryptedData(EType etype, byte[] cipher)
			=> new EncryptedData
			{
				etype = (int)etype,
				cipher = cipher
			};

		internal static EncryptionKey EncryptionKey(EType etype, byte[] keyBytes)
			=> new EncryptionKey
			{
				keytype = (int)etype,
				keyvalue = keyBytes
			};

		internal static PrincipalName PrincipalName(NameType nameType, string name)
			=> new PrincipalName
			{
				name_type = (int)nameType,
				name_string = new GeneralString[]
				{
					name
				}
			};

		internal static PrincipalName PrincipalName(ServicePrincipalName spn)
			=> new PrincipalName
			{
				name_type = (int)NameType.ServiceInstance,
				name_string = new GeneralString[]
				{
					spn.ServiceClass, spn.ServiceInstance
				}
			};

		internal static PrincipalName PrincipalName(NameType nameType, string name1, string name2)
			=> new PrincipalName
			{
				name_type = (int)nameType,
				name_string = new GeneralString[]
				{
					name1, name2
				}
			};

		internal static KDC_REQ_BODY KdcReqBody(
			TicketParameters ticketParameters,
			PrincipalName cname,
			string crealm,
			PrincipalName sname,
			uint nonce,
			int[] etypes,
			HostAddress[]? hostAddresses
			)
			=> new KDC_REQ_BODY
			{
				kdc_options = new Titanis.Asn1.Asn1BitString((uint)ticketParameters.Options),
				cname = cname,
				realm = crealm,
				sname = sname,
				from = ticketParameters.StartTime,
				till = ticketParameters.EndTime ?? TicketParameters.DefaultEndTime,
				rtime = ticketParameters.RenewTill,
				nonce = (int)nonce,
				etype = etypes,
				addresses = hostAddresses
			};

		internal static Checksum Checksum(EncChecksumType type, byte[] value)
			=> new Checksum
			{
				cksumtype = (int)type,
				checksum = value
			};

		internal static Authenticator_Outer Authenticator(
			PrincipalName cname,
			string crealm,
			Checksum cksum,
			uint seqnbr,
			EncryptionKey subkey
			)
		{
			var now = KerberosTime.Now();
			return new Authenticator_Outer
			{
				Value = new Authenticator_Unnamed_4
				{
					authenticator_vno = 5,
					crealm = crealm,
					cname = cname,
					cksum = cksum,
					cusec = now.usec,
					ctime = now.dt,
					seq_number = seqnbr,
					subkey = subkey
				}
			};
		}

		internal static KDC_REQ TgsReq(
			PA_DATA[] padatas,
			KDC_REQ_BODY reqBody
			)
			=> new KDC_REQ
			{
				pvno = 5,
				msg_type = (byte)KrbMessageType.Tgsreq,
				padata = padatas,
				req_body = reqBody
			};

		internal static KDC_REQ ASReq(
			PA_DATA[] padata,
			KDC_REQ_BODY reqBody
			)
			=> new KDC_REQ
			{
				pvno = 5,
				msg_type = (byte)KrbMessageType.Asreq,
				padata = padata,
				req_body = reqBody
			};

		internal static AP_REQ APReq(
			APOptions options,
			Ticket_Ticket ticket,
			EncryptedData authenticator
			)
			=> new AP_REQ
			{
				Value = new AP_REQ_Unnamed_3
				{
					pvno = 5,
					msg_type = (byte)KrbMessageType.Apreq,
					ap_options = new Asn1BitString((uint)options),
					ticket = ticket,
					authenticator = authenticator
				}
			};

	}
}
