using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Ntlm
{
	public class NtlmExchange
	{
		public byte[] NegotiateBytes { get; private set; }
		public byte[] ChallengeBytes { get; private set; }
		public byte[] AuthenticateBytes { get; private set; }

		public static NtlmExchange FromPackets(
			byte[] negotiateBytes,
			byte[] challengeBytes,
			byte[] authenticateBytes
			)
		{
			NtlmExchange exchange = new NtlmExchange()
			{
				NegotiateBytes = negotiateBytes,
				ChallengeBytes = challengeBytes,
				AuthenticateBytes = authenticateBytes
			};
			return exchange;
		}
	}
}
