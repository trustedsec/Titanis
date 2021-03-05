using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Ntlm
{
	public class NtlmTraceContext : AuthTraceContext
	{
		public override ReadOnlySpan<byte> AcceptToken(ReadOnlySpan<byte> token)
		{
			throw new NotImplementedException();
		}

		public override ReadOnlySpan<byte> InitializeWithToken(ReadOnlySpan<byte> token)
		{
			throw new NotImplementedException();
		}
	}
}
