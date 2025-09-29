using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Ntlm
{
	/// <summary>
	/// Receives callback notifications during an NTLM client negotiation.
	/// </summary>
	[Callback]
	public interface INtlmClientCallback
	{
		/// <summary>
		/// Called just before constructing a NEGOTIATE token.
		/// </summary>
		/// <param name="flags">Negotiate flags</param>
		/// <param name="version">NTLM version</param>
		/// <remarks>
		/// The implementation may modify these flags.
		/// </remarks>
		void OnNegotiating(ref NegotiateFlags flags, NtlmVersion version);
		/// <summary>
		/// Called just after the context decodes a CHALLENGE token.
		/// </summary>
		/// <param name="challenge">The challenge sent by the remote party</param>
		void OnChallenge(NtlmChallenge challenge);
		/// <summary>
		/// Called just before sending the AUTHENTICATE token.
		/// </summary>
		/// <param name="auth"></param>
		void OnAuth(ref NtlmAuthInfo auth, ref NtlmAuthResult authResult);
	}
}
