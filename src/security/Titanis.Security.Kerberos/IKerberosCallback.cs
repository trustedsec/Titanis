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
		/// <summary>
		/// Called before requesting a ticket-granting ticket.
		/// </summary>
		void OnRequestingTgt(string targetRealm, KerberosCredential credential, TicketParameters ticketParameters, int nonce);
		/// <summary>
		/// Called when the KDC responds to an AS-REQ with <see cref="KerberosErrorCode.KDC_ERR_PREAUTH_REQUIRED"/>.
		/// </summary>
		void OnReceivedAsrepPreauthRequired(Guid correlationId, IList<PA_DATA> padataList);
		/// <summary>
		/// Called when the KDC responds to an AS-REQ with an error that cannot be handled.
		/// </summary>
		void OnReceivedAsrepError(Guid correlationId, KerberosException ex);

		void OnReceivedAsrep(Guid correlationId, KdcRepInfo asrep);

		void OnEncryptingTS(Guid correlationId, SessionKey protocolKey, byte[]? salt);
		void OnProcessETypes(Guid correlationId, IList<ETYPE_INFO_ENTRY> etypeInfos);
		void OnProcessETypes(Guid correlationId, IList<ETYPE_INFO2_ENTRY> etypeInfos);

		void OnReceivedTgt(TicketInfo tgtInfo);

		void OnRequestingTicket(SecurityPrincipalName spn, TicketInfo tgt, TicketParameters ticketParameters);
		void OnReceivedTicket(Guid correlationId, KdcRepInfo tgsrep);

		void OnSendingApreq(Guid correlationId, KerberosClientContextBase? authContext, SecurityPrincipalName targetSpn, TicketInfo ticket, KerberosCredential credential, SecurityCapabilities caps, SessionKey? initiatorSubkey, int sendSeqNbr);
		void OnReceivedAprep(Guid correlationId, KerberosClientContextBase? authContext, uint recvSeqNbr, SessionKey? acceptorSubkey);

		void OnReferralReceived(SecurityPrincipalName spn, TicketInfo ticket);
	}
}
