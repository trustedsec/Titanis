using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Ntlm
{
	public enum NtlmLogMessageId
	{
		Other = 0,

		Negotiating,
		Challenge,
		Auth,
	}

	public class NtlmDiagnosticLogger : INtlmClientCallback
	{
		public NtlmDiagnosticLogger(ILog log)
		{
			ArgumentNullException.ThrowIfNull(log);
			this.Log = log;
		}

		public ILog Log { get; }

		private static readonly string SourceName = typeof(Ntlm).FullName!;

		private static readonly LogMessageType SendingNegotiate = new LogMessageType(LogMessageSeverity.Diagnostic, SourceName, (int)NtlmLogMessageId.Negotiating, @"Negotiating NTLM
	Flags: {0}
	Version: {1}", "flags", "version");
		private static readonly LogMessageType ReceivedChallenge = new LogMessageType(LogMessageSeverity.Diagnostic, SourceName, (int)NtlmLogMessageId.Challenge, @"Received NTLM challenge:
	Server version: {8}
	Target type: {0}
	Host name (NetBIOS): {1}
	Host domain (NetBIOS): {2}
	Host name (DNS): {3}
	Host domain (DNS): {4}
	Host DNS tree (DNS): {5}
	Timestamp: {6:O}
	Challenge: {7} (0x{7:X8})", "targetType", "nbComputerName", "nbDomainName", "dnsComputerName", "dnsDomainName", "dnsTree", "timestamp", "challenge", "serverVersion");
		private static readonly LogMessageType SendingAuth = new LogMessageType(LogMessageSeverity.Diagnostic, SourceName, (int)NtlmLogMessageId.Auth, @"Sending NTLM_AUTHENTICATE:
	Flags: {0}
	Version: {1}
	Workstation name: {2}
	User name: {3}
	User domain: {4}
	Session base key: {5}
	KX key: {6}
	Exported session key: {7}
	Signing key (client-to-server): {8}
	Signing key (server-to-client): {9}
	Sealing key (client-to-server): {10}
	Sealing key (server-to-client): {11}", "flags", "version", "workstation", "userName", "userDomain", "sessionBaseKey", "keyExchangeKey", "exportedSessionKey", "signKeyC2S", "signKeyS2C", "sealKeyC2S", "sealKeyS2C");

		void INtlmClientCallback.OnNegotiating(ref NegotiateFlags flags, NtlmVersion version)
		{
			this.Log.WriteMessage(SendingNegotiate.Create(flags, version));
		}

		void INtlmClientCallback.OnChallenge(NtlmChallenge challenge)
		{
			var negFlags = challenge.hdr.negotiateFlags;
			var info = challenge.targetInfo;
			this.Log.WriteMessage(ReceivedChallenge.Create(
				((0 != (negFlags & NegotiateFlags.O_TargetTypeServer)) ? "server" : (0 != (negFlags & NegotiateFlags.N_TargetTypeDomain)) ? "domain" : "<unspecified>"),
				info?.NbComputerName,
				info?.NbDomainName,
				info?.DnsComputerName,
				info?.DnsDomainName,
				info?.DnsTreeName,
				info?.timestamp,
				challenge.hdr.serverChallenge,
				challenge.hdr.version
				));
		}

		void INtlmClientCallback.OnAuth(ref NtlmAuthInfo authInfo, ref NtlmAuthResult authResult)
		{
			this.Log.WriteMessage(SendingAuth.Create(
				authInfo.negotiateFlags,
				authInfo.version,
				authInfo.workstationName,
				authInfo.userName,
				authInfo.userDomain,
				authInfo.resp.SessionBaseKey.AsReadOnlySpan().ToHexString(),
				authInfo.kxkey.AsReadOnlySpan().ToHexString(),
				authInfo.exportedSessionKey.AsReadOnlySpan().ToHexString(),
				authResult.signKeyC2S.AsReadOnlySpan().ToHexString(),
				authResult.signKeyS2C.AsReadOnlySpan().ToHexString(),
				authResult.sealKeyC2S.AsReadOnlySpan().ToHexString(),
				authResult.sealKeyS2C.AsReadOnlySpan().ToHexString()
				));
		}
	}
}
