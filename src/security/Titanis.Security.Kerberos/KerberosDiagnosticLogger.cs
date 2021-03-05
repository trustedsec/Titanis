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
	public class KerberosDiagnosticLogger : IKerberosCallback
	{
		public const string KerberosLogSource = "Kerberos";

		private const LogMessageSeverity LogSeverity = LogMessageSeverity.Diagnostic;
		private readonly ILog log;

		public KerberosDiagnosticLogger(ILog log)
		{
			ArgumentNullException.ThrowIfNull(log);
			this.log = log;
		}

		private void WriteMessage(string text)
		{
			this.log.WriteMessage(new LogMessage(LogSeverity, KerberosLogSource, text));
		}

		public void OnReceiveAsrepPadataList(IList<PA_DATA> padataList)
		{
			foreach (var padata in padataList)
			{
				WriteMessage($"KDC supports PA-DATA type {(PadataType)padata.padata_type} ({padata.padata_type})");
			}
		}

		public void OnRequestingTgt(string targetRealm, KerberosCredential credential, int nonce)
		{
			WriteMessage($"Requesting TGT for realm {targetRealm} for user {credential.UserName} (nonce={nonce}).");
		}

		public void OnEncryptingTS(SessionKey protocolKey, byte[]? salt)
		{
			WriteMessage($"Encrypting timestamp with {protocolKey.EncryptionProfile.EType} key {protocolKey.KeyBytes.ToHexString()} (salt={((salt != null) ? salt.ToHexString() : "<none>")}.");
		}

		public void OnProcessETypes(IList<ETYPE_INFO_ENTRY> etypeInfos)
		{
			foreach (var item in etypeInfos)
			{
				WriteMessage($"KDC supports EType {(EType)item.etype}.");
			}
		}

		public void OnProcessETypes(IList<ETYPE_INFO2_ENTRY> etypeInfos)
		{
			foreach (var item in etypeInfos)
			{
				WriteMessage($"KDC supports EType {(EType)item.etype} salt={((item.salt.HasValue) ? item.salt.Value.value : "<none>")}.");
			}
		}

		public void OnReceivedTgt(TicketInfo tgtInfo)
		{
			this.WriteMessage($"Received TGT: {tgtInfo.SessionKey.EType} session key {tgtInfo.SessionKey.KeyBytes.ToHexString()}");
		}

		public void OnRequestingTicket(ServicePrincipalName spn, string realm, TicketInfo tgt, KdcOptions kdcOptions)
		{
			this.WriteMessage($"Requesting ticket for {spn} within {realm} for user {tgt.UserName}@{tgt.UserRealm} (KDC options = {kdcOptions})");
		}

		public void OnReceivedTicket(TicketInfo ticketInfo)
		{
			this.WriteMessage($"Received ticket: {ticketInfo.SessionKey.EType} session key {ticketInfo.SessionKey.KeyBytes.ToHexString()}");
		}

		public void OnSendingApreq(KerberosClientContext? authContext, ServicePrincipalName targetSpn, KerberosCredential credential, SecurityCapabilities caps, SessionKey sessionKey, uint sendSeqNbr)
		{
			this.WriteMessage($"Sending AP-REQ to {targetSpn} for user {credential.UserName}@{credential.Realm} with session key {sessionKey.EType} {sessionKey.KeyBytes.ToHexString()} (sendSeqNbr={sendSeqNbr})(gssFlags={caps})");
		}

		public void OnReceivedAprep(KerberosClientContext? authContext, uint recvSeqNbr, SessionKey? acceptorSubkey)
		{
			//this.WriteMessage($"Received AP-REP from {authContext.TargetSpn} for user {authContext.UserName}@{authContext.Credential.Realm} {((acceptorSubkey != null) ? $"with session key {acceptorSubkey.EType} {acceptorSubkey.KeyBytes.ToHexString()} (recvSeqNbr={recvSeqNbr})" : "(no session key)")}");
			this.WriteMessage($"Received AP-REP {((acceptorSubkey != null) ? $"with session key {acceptorSubkey.EType} {acceptorSubkey.KeyBytes.ToHexString()} (recvSeqNbr={recvSeqNbr})" : "(no session key)")}");
		}
	}
}
