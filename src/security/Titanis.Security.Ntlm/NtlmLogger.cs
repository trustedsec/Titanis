using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Ntlm
{
	[CallbackLogger]
	public class NtlmDiagnosticLogger : INtlmClientCallback
	{
		public NtlmDiagnosticLogger(ILog log, INtlmClientCallback? chainedCallback = null)
		{
			ArgumentNullException.ThrowIfNull(log);
			this.Log = log;
			this._chainedCallback = chainedCallback;
		}

		public ILog Log { get; }
		private readonly INtlmClientCallback? _chainedCallback;


		void INtlmClientCallback.OnNegotiating(ref NegotiateFlags flags, NtlmVersion version)
		{
			this.Log.WriteNtlmClientSendingNegotiateMessage(version.ToString(), flags);
			this._chainedCallback?.OnNegotiating(ref flags, version);
		}

		void INtlmClientCallback.OnChallenge(NtlmChallenge challenge)
		{
			var negFlags = challenge.hdr.negotiateFlags;
			var info = challenge.targetInfo;
			string targetType = ((0 != (negFlags & NegotiateFlags.O_TargetTypeServer)) ? "server" : (0 != (negFlags & NegotiateFlags.N_TargetTypeDomain)) ? "domain" : "<unspecified>");
			this.Log.WriteNtlmClientReceivedChallengeMessage(challenge.hdr.version.ToString(), negFlags, targetType,info?.NbComputerName,info?.DnsComputerName,info?.NbDomainName,info?.DnsDomainName,info?.DnsTreeName,info?.timestamp,challenge.hdr.serverChallenge);

			this._chainedCallback?.OnChallenge(challenge);
		}

		void INtlmClientCallback.OnAuth(ref NtlmAuthInfo authInfo, ref NtlmAuthResult authResult)
		{
			this.Log.WriteNtlmClientSendingAuthMessage(
				authInfo.negotiateFlags,
				authInfo.version.ToString(),
				authInfo.workstationName,
				authInfo.userName,
				authInfo.userDomain,
				authInfo.challengeFromClient,
				authInfo.resp.SessionBaseKey.AsReadOnlySpan().ToHexString(),
				authInfo.kxkey.AsReadOnlySpan().ToHexString(),
				authInfo.exportedSessionKey.AsReadOnlySpan().ToHexString(),
				authResult.signKeyC2S.AsReadOnlySpan().ToHexString(),
				authResult.signKeyS2C.AsReadOnlySpan().ToHexString(),
				authResult.sealKeyC2S.AsReadOnlySpan().ToHexString(),
				authResult.sealKeyS2C.AsReadOnlySpan().ToHexString()
				);

			this._chainedCallback?.OnAuth(ref authInfo, ref authResult);
		}
	}
}
