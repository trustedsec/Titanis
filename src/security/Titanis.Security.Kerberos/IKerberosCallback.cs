using KerberosV5Spec2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos
{
	[Callback]
	public interface IKerberosCallback
	{

		void OnReceiveAsrepPadataList(IList<PA_DATA> padataList) { }
		void OnReceivedAsrepEncPart(AsrepInfo asrep);
		void OnRequestingTgt(string targetRealm, KerberosCredential credential, int nonce) { }
		void OnEncryptingTS(SessionKey protocolKey, byte[]? salt) { }
		void OnProcessETypes(IList<ETYPE_INFO_ENTRY> etypeInfos) { }
		void OnProcessETypes(IList<ETYPE_INFO2_ENTRY> etypeInfos) { }
		void OnReceivedTgt(TicketInfo tgtInfo) { }

		void OnRequestingTicket(SecurityPrincipalName spn, TicketInfo tgt, KdcOptions kdcOptions);
		void OnReceivedTicket(TicketInfo ticketInfo) { }
		void OnSendingApreq(KerberosClientContextBase? authContext, SecurityPrincipalName targetSpn, TicketInfo ticket, KerberosCredential credential, SecurityCapabilities caps, SessionKey initiatorSubkey, int sendSeqNbr);
		void OnReceivedAprep(KerberosClientContextBase? authContext, uint recvSeqNbr, SessionKey? acceptorSubkey) { }

		void OnReferralReceived(SecurityPrincipalName spn, TicketInfo ticket);
	}
}
