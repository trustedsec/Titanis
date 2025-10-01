using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Titanis.Security.Kerberos.Asn1.KerberosV5Spec2;

namespace Titanis.Security.Kerberos
{
	[CallbackLogger]
	public class KerberosDiagnosticLogger : IKerberosCallback
	{
		public const string KerberosLogSource = "Kerberos";

		private const LogMessageSeverity LogSeverity = LogMessageSeverity.Diagnostic;
		private readonly ILog log;
		private readonly IKerberosCallback? _chainedCallback;

		public KerberosDiagnosticLogger(ILog log, IKerberosCallback? chainedCallback = null)
		{
			ArgumentNullException.ThrowIfNull(log);
			this.log = log;
			this._chainedCallback = chainedCallback;
		}

		private void WriteMessage(string text)
		{
			this.log.WriteMessage(new LogMessage(LogSeverity, KerberosLogSource, text));
		}

		void IKerberosCallback.OnReceiveAsrepPadataList(IList<PA_DATA> padataList)
		{
			foreach (var padata in padataList)
			{
				WriteMessage($"KDC supports PA-DATA type {(PadataType)padata.padata_type} ({padata.padata_type})");
			}

			this._chainedCallback?.OnReceiveAsrepPadataList(padataList);
		}

		void IKerberosCallback.OnRequestingTgt(string targetRealm, KerberosCredential credential, int nonce)
		{
			WriteMessage($"Requesting TGT for realm {targetRealm} for user {credential.UserName} (nonce={nonce})");

			this._chainedCallback?.OnRequestingTgt(targetRealm, credential, nonce);
		}

		void IKerberosCallback.OnEncryptingTS(SessionKey protocolKey, byte[]? salt)
		{
			WriteMessage($"Encrypting timestamp with {protocolKey.EncryptionProfile.EType} key {protocolKey.KeyBytes.ToHexString()} (salt={((salt != null) ? salt.ToHexString() : "<none>")}.");

			this._chainedCallback?.OnEncryptingTS(protocolKey, salt);
		}

		void IKerberosCallback.OnProcessETypes(IList<ETYPE_INFO_ENTRY> etypeInfos)
		{
			foreach (var item in etypeInfos)
			{
				WriteMessage($"KDC supports EType {(EType)item.etype}");
			}

			this._chainedCallback?.OnProcessETypes(etypeInfos);
		}

		void IKerberosCallback.OnProcessETypes(IList<ETYPE_INFO2_ENTRY> etypeInfos)
		{
			foreach (var item in etypeInfos)
			{
				WriteMessage($"KDC supports EType {(EType)item.etype} salt={((item.salt.HasValue) ? item.salt.Value.value : "<none>")}");
			}

			this._chainedCallback?.OnProcessETypes(etypeInfos);
		}

		void IKerberosCallback.OnReceivedTgt(TicketInfo tgtInfo)
		{
			this.WriteMessage($"Received TGT for realm {tgtInfo.TicketRealm}: {tgtInfo.SessionKey.EType} session key {tgtInfo.SessionKey.KeyBytes.ToHexString()}");

			this._chainedCallback?.OnReceivedTgt(tgtInfo);
		}

		void IKerberosCallback.OnRequestingTicket(SecurityPrincipalName spn, TicketInfo tgt, KdcOptions kdcOptions)
		{
			// Since this is a TGT, the TicketRealm indicates the issuing realm,
			// but ServiceInstance indicates the target realm
			this.WriteMessage($"Requesting ticket for {spn} within {tgt.ServiceInstance} for user {tgt.UserName}@{tgt.UserRealm} (KDC options = {kdcOptions})");

			this._chainedCallback?.OnRequestingTicket(spn, tgt, kdcOptions);
		}

		void IKerberosCallback.OnReceivedTicket(TicketInfo ticketInfo)
		{
			this.WriteMessage($"Received ticket for {ticketInfo.TargetSpn} within {ticketInfo.TicketRealm} for user {ticketInfo.UserName}@{ticketInfo.UserRealm}: {ticketInfo.SessionKey.EType} session key {ticketInfo.SessionKey.KeyBytes.ToHexString()}");

			this._chainedCallback?.OnReceivedTicket(ticketInfo);
		}

		void IKerberosCallback.OnSendingApreq(KerberosClientContext? authContext, SecurityPrincipalName targetSpn, TicketInfo ticket, KerberosCredential credential, SecurityCapabilities caps, SessionKey sessionKey, uint sendSeqNbr)
		{
			this.WriteMessage($"Sending AP-REQ to {targetSpn} for user {ticket.UserName}@{ticket.UserRealm} with session key {sessionKey.EType} {sessionKey.KeyBytes.ToHexString()} (sendSeqNbr={sendSeqNbr})(gssFlags={caps})");

			this._chainedCallback?.OnSendingApreq(authContext, targetSpn, ticket, credential, caps, sessionKey, sendSeqNbr);
		}

		void IKerberosCallback.OnReceivedAprep(KerberosClientContext? authContext, uint recvSeqNbr, SessionKey? acceptorSubkey)
		{
			//this.WriteMessage($"Received AP-REP from {authContext.TargetSpn} for user {authContext.UserName}@{authContext.Credential.Realm} {((acceptorSubkey != null) ? $"with session key {acceptorSubkey.EType} {acceptorSubkey.KeyBytes.ToHexString()} (recvSeqNbr={recvSeqNbr})" : "(no session key)")}");
			this.WriteMessage($"Received AP-REP {((acceptorSubkey != null) ? $"with session key {acceptorSubkey.EType} {acceptorSubkey.KeyBytes.ToHexString()} (recvSeqNbr={recvSeqNbr})" : "(no session key)")}");

			this._chainedCallback?.OnReceivedAprep(authContext, recvSeqNbr, acceptorSubkey);
		}

		void IKerberosCallback.OnReferralReceived(SecurityPrincipalName spn, TicketInfo ticket)
		{
			this.WriteMessage($"Received referral for {spn} to realm {ticket.ServiceInstance}");
		}
	}
}
