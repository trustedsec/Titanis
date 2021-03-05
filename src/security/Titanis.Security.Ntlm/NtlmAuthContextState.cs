using System;

namespace Titanis.Security.Ntlm
{
	struct NtlmAuthContextState
	{
		// Originate from client
		//internal NegotiateFlags negFlags;
		internal NegotiateFlags negotiateFlags;
		internal NegotiateFlags negAuthFlags;
		internal string? workstationName;
		internal string? workstationDomain;
		internal ulong challengeFromClient;
		internal ServicePrincipalName? targetSpn;
		internal NtlmVersion clientVersion;
		internal DateTime clientTime;

		// Client crypto
		internal Buffer128 randomKey;

		// Tokens
		internal byte[]? negToken;
		internal byte[]? challengeToken;

		// Originate from server
		internal NegotiateFlags challengeFlags;
		internal DateTime timestamp;
		internal string serverComputerName;
		internal ulong serverChallenge;
		internal string? serverName;
		internal NtlmVersion serverVersion;
		internal string? domainName;
		internal string? dnsServerName;
		internal string? dnsDomainName;

		internal void UpdateWithNegotiate(NtlmNegotiateMessage neg)
		{
			this.negotiateFlags = neg.hdr.negotiatedFlags;
			this.workstationDomain = neg.workstationDomain;
			this.workstationName = neg.workstationName;
		}
		internal void UpdateWithAuth(NtlmAuthenticate auth)
		{
			this.negAuthFlags = auth.hdr.negotiatedFlags;
			this.challengeFromClient = auth.clientChallenge.clientChallenge;
			this.clientVersion = auth.hdr.version;
			this.timestamp = DateTime.FromFileTime(auth.clientChallenge.time);
		}
		internal void UpdateWithChallenge(NtlmChallenge msg)
		{
			this.challengeFlags = msg.hdr.negotiateFlags;
			this.serverChallenge = msg.hdr.serverChallenge;
			this.serverName = msg.serverName;
			this.serverVersion = msg.hdr.version;

			var targetInfo = msg.targetInfo;
			if (targetInfo != null)
			{
				this.timestamp = targetInfo.timestamp ?? this.clientTime;
				this.serverComputerName = targetInfo.NbComputerName;
				this.domainName = targetInfo.NbDomainName;
				this.dnsServerName = targetInfo.DnsComputerName;
				this.dnsDomainName = targetInfo.DnsDomainName;
			}
		}
	}
}
