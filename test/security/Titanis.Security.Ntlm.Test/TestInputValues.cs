using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Ntlm.Test
{
	// [MS-NLMP] § 4.2.1 - Common Values
	internal static class TestInputValues
	{
		internal const string UserName = "User";
		internal const string Domain = "Domain";
		internal const string Password = "Password";
		internal const string ServerName = "Server";
		internal const string Workstation = "COMPUTER";
		internal static readonly byte[] RandomSessionKey = new byte[]
		{
			0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55,
			0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55
		};
		internal static readonly DateTime Time = new DateTime(0);
		internal const ulong ClientChallenge = 0xAAAAAAAA_AAAAAAAA;
		internal const ulong ServerChallenge = 0xEFCDAB89_67452301;

		// [MS-NLMP] § 4.2.2 - NTLM v1 Authentication
		internal const NegotiateFlags ChallengeFlags_422 = (NegotiateFlags)0xE2028233;

		// [MS-NLMP] § 4.2.2.4 - GSS_WrapEx Examples
		internal static readonly byte[] PlaintextBytes = Encoding.Unicode.GetBytes("Plaintext");

		// [MS-NLMP] § 4.2.2.4 - GSS_WrapEx Examples
		internal const uint SeqNbr = 0;

		// [MS-NLMP] § 4.2.2.4 - GSS_WrapEx Examples
		internal const uint RandomPad = 0;





		// [MS-NLMP] § 4.2.3 NTLM v1 with Client Challenge
		internal const NegotiateFlags ChallengeFlags_423 = (NegotiateFlags)0x820a8233;


		// [MS-NLMP] § 4.2.4 NTLMv2 Authentication
		internal const NegotiateFlags ChallengeFlags_424 = (NegotiateFlags)0xe28a8233;
	}
}
