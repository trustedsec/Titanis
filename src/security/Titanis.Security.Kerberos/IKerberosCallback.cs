using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Security.Kerberos.Asn1.KerberosV5Spec2;

namespace Titanis.Security.Kerberos
{
	public interface IKerberosCallback
	{

		void OnReceiveAsrepPadataList(IList<PA_DATA> padataList) { }
		void OnRequestingTgt(string targetRealm, KerberosCredential credential, int nonce) { }
		void OnEncryptingTS(SessionKey protocolKey, byte[]? salt) { }
		void OnProcessETypes(IList<ETYPE_INFO_ENTRY> etypeInfos) { }
		void OnProcessETypes(IList<ETYPE_INFO2_ENTRY> etypeInfos) { }
		void OnReceivedTgt(TicketInfo tgtInfo) { }

		void OnRequestingTicket(ServicePrincipalName spn, string realm, TicketInfo tgt, KdcOptions kdcOptions);
		void OnReceivedTicket(TicketInfo ticketInfo) { }
		void OnSendingApreq(KerberosClientContext? authContext, ServicePrincipalName targetSpn, KerberosCredential credential, SecurityCapabilities caps, SessionKey sessionKey, uint sendSeqNbr);
		void OnReceivedAprep(KerberosClientContext? authContext, uint recvSeqNbr, SessionKey? acceptorSubkey) { }
	}
}
