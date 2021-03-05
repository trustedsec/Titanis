using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security
{
	public abstract class AuthServerContext : AuthContext
	{
		public abstract ReadOnlySpan<byte> Accept();
		public abstract ReadOnlySpan<byte> Accept(ReadOnlySpan<byte> token);
	}
}
