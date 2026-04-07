using KerberosV5Spec2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Titanis.Certificates;

namespace Titanis.Security.Kerberos
{
	[CallbackLogger]
	public class KerberosDiagnosticLogger : IKerberosCallback
	{
		public KerberosDiagnosticLogger(ILog log, IKerberosCallback? chainedCallback = null)
		{
			ArgumentNullException.ThrowIfNull(log);
			this.log = log;
			this._chainedCallback = chainedCallback;
		}

		private readonly ILog log;
		private readonly IKerberosCallback? _chainedCallback;

		/// <inheritdoc/>
		void IKerberosCallback.OnRequestingTgt(string targetRealm, KerberosCredential credential, TicketParameters ticketParameters, int nonce)
		{
			this.log.WriteKerberosClientRequestingTgtMessage(ticketParameters.CorrelationId, credential.UserName.WireName, credential.UserName.NameType, targetRealm, ticketParameters.Options, nonce);

			this._chainedCallback?.OnRequestingTgt(targetRealm, credential, ticketParameters, nonce);
		}

		/// <inheritdoc/>
		void IKerberosCallback.OnReceivedAsrepPreauthRequired(Guid correlationId, IList<PA_DATA> padataList)
		{
			foreach (var padata in padataList)
			{
				this.log.WriteKerberosClientReceivedPreauthRequired_PadataTypeMessage(correlationId, (PadataType)padata.padata_type, padata.padata_type);
			}

			this._chainedCallback?.OnReceivedAsrepPreauthRequired(correlationId, padataList);
		}

		void IKerberosCallback.OnReceivedAsrepError(Guid correlationId, KerberosException ex)
		{
			this.log.WriteKerberosClientReceivedAsrepErrorMessage(correlationId, ex.KerberosErrorCode, (int)ex.KerberosErrorCode, ex.UnderlyingNtstatus, (uint?)ex.UnderlyingNtstatus, ex.Message);
		}

		void IKerberosCallback.OnReceivedAsrep(Guid correlationId, KdcRepInfo asrep)
		{
			var client = asrep.ClientName;
			var spn = asrep.Spn;
			this.log.WriteKerberosClientReceivedAsrepMessage(correlationId, asrep.Salt, asrep.TicketRealm, client.ToString(), client.NameType, asrep.ClientRealm, spn.ToString(), spn.NameType, asrep.ServiceRealm, asrep.TicketFlags, asrep.AsrepKey?.KeyBytes.ToHexString(), asrep.AsrepKey?.EType, asrep.SessionKey.KeyBytes.ToHexString(), asrep.SessionKey.EType, asrep.SupportedETypes);
		}

		private void WriteMessage(string message)
		{
			this.log.WriteDiagnostic(message);
		}

		void IKerberosCallback.OnEncryptingTS(Guid correlationId, SessionKey protocolKey, byte[]? salt)
		{
			this.log.WriteKerberosClientEncryptingTimestampMessage(correlationId, salt.ToHexString(), protocolKey.EType, protocolKey.KeyBytes.ToHexString());

			this._chainedCallback?.OnEncryptingTS(correlationId, protocolKey, salt);
		}

		void IKerberosCallback.OnProcessETypes(Guid correlationId, IList<ETYPE_INFO_ENTRY> etypeInfos)
		{
			foreach (var item in etypeInfos)
			{
				this.log.WriteKerberosClientReceivedPreauthRequired_ETypeMessage(correlationId, (EType)item.etype, (int)item.etype, null);
			}

			this._chainedCallback?.OnProcessETypes(correlationId, etypeInfos);
		}

		void IKerberosCallback.OnProcessETypes(Guid correlationId, IList<ETYPE_INFO2_ENTRY> etypeInfos)
		{
			foreach (var item in etypeInfos)
			{
				this.log.WriteKerberosClientReceivedPreauthRequired_ETypeMessage(correlationId, (EType)item.etype, (int)item.etype, item.salt?.Value);
			}

			this._chainedCallback?.OnProcessETypes(correlationId, etypeInfos);
		}

		void IKerberosCallback.OnReceivedTgt(TicketInfo tgtInfo)
		{
			// UNDONE: The details are logged with the AS-REP
			//this.WriteMessage($"Received TGT for realm {tgtInfo.TicketRealm}: {tgtInfo.SessionKey.EType} session key {tgtInfo.SessionKey.KeyBytes.ToHexString()}");

			this._chainedCallback?.OnReceivedTgt(tgtInfo);
		}

		void IKerberosCallback.OnRequestingTicket(SecurityPrincipalName spn, TicketInfo tgt, TicketParameters ticketParameters)
		{
			// Since this is a TGT, the TicketRealm indicates the issuing realm,
			// but ServiceInstance indicates the target realm
			this.log.WriteKerberosClientRequestingTicketMessage(ticketParameters.CorrelationId, tgt.ClientName, tgt.ClientRealm, tgt.TicketRealm, spn.ToString(), spn.NameType, ticketParameters.Options);

			this._chainedCallback?.OnRequestingTicket(spn, tgt, ticketParameters);
		}

		void IKerberosCallback.OnReceivedTicket(Guid correlationId, KdcRepInfo tgsrep)
		{
			var spn = tgsrep.Spn;
			this.log.WriteKerberosClientReceivedTicketMessage(correlationId, tgsrep.TicketRealm, tgsrep.ClientName.ToString(), tgsrep.ClientName.NameType, tgsrep.ClientRealm, spn.ToString(), spn.NameType, tgsrep.ServiceRealm, tgsrep.TicketFlags, tgsrep.SessionKey.KeyBytes.ToHexString(), tgsrep.SessionKey.EType, tgsrep.SupportedETypes);

			this._chainedCallback?.OnReceivedTicket(correlationId, tgsrep);
		}

		void IKerberosCallback.OnSendingApreq(Guid correlationId, KerberosClientContextBase? authContext, SecurityPrincipalName targetSpn, TicketInfo ticket, KerberosCredential credential, SecurityCapabilities caps, SessionKey? initiatorSubkey, int sendSeqNbr)
		{
			this.log.WriteKerberosClientSendingApreqMessage(correlationId, ticket.ClientName, targetSpn.ToString(), targetSpn.NameType, ticket.SessionKey.KeyBytes.ToHexString(), ticket.SessionKey.EType, initiatorSubkey?.KeyBytes?.ToHexString(), initiatorSubkey?.EType, sendSeqNbr);

			this._chainedCallback?.OnSendingApreq(correlationId, authContext, targetSpn, ticket, credential, caps, initiatorSubkey, sendSeqNbr);
		}

		void IKerberosCallback.OnReceivedAprep(Guid correlationId, KerberosClientContextBase? authContext, uint recvSeqNbr, SessionKey? acceptorSubkey)
		{
			this.log.WriteKerberosClientReceivedAprepMessage(correlationId, acceptorSubkey?.KeyBytes?.ToHexString(), acceptorSubkey?.EType, recvSeqNbr);

			this._chainedCallback?.OnReceivedAprep(correlationId, authContext, recvSeqNbr, acceptorSubkey);
		}

		void IKerberosCallback.OnReferralReceived(SecurityPrincipalName spn, TicketInfo ticket)
		{
			this.WriteMessage($"Received referral for {spn} to realm {ticket.ServiceInstance}");
		}
	}
}
