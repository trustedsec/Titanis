using System;
using Titanis.IO;

namespace Titanis.Security.Ntlm
{
	/// <summary>
	/// Represents the challenge sent by the remote party.
	/// </summary>
	public class NtlmChallenge
	{
		public NtlmChallengeHeader hdr;
		public string? serverName;
		public NtlmAvInfo? targetInfo;

		public static NtlmChallenge Parse(ReadOnlySpan<byte> token)
		{
			// TODO: Avoid calling ToArray
			var reader = new ByteMemoryReader(token.ToArray());
			NtlmChallenge challenge = reader.ReadChallenge();

			return challenge;
		}
	}
}