using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Ntlm
{
	public class NtlmExchange
	{
		public byte[]? NegotiateBytes { get; }
		public byte[]? ChallengeBytes { get; }
		public byte[]? AuthenticateBytes { get; }

		public NtlmExchange(
			byte[]? negotiateBytes,
			byte[]? challengeBytes,
			byte[]? authenticateBytes
			)
		{
			this.NegotiateBytes = negotiateBytes;
			this.ChallengeBytes = challengeBytes;
			this.AuthenticateBytes = authenticateBytes;
		}
	}
}
