using System;
using System.Collections.Generic;
using System.Text;
using Titanis.IO;

namespace Titanis.Security.Ntlm
{
	public class NtlmNegotiateMessage
	{
		public NegotiateHeader hdr;
		public string? workstationName;
		public string? workstationDomain;

		public static NtlmNegotiateMessage Parse(ReadOnlySpan<byte> token)
		{
			ByteMemoryReader reader = new ByteMemoryReader(token.ToArray());
			NtlmNegotiateMessage msg = reader.ReadNegotiate();
			return msg;
		}
	}
}
