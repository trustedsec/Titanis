using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security
{
	/// <summary>
	/// Implements an authentication context used for tracing.
	/// </summary>
	public abstract class AuthTraceContext
	{
		public abstract ReadOnlySpan<byte> AcceptToken(ReadOnlySpan<byte> token);
		public abstract ReadOnlySpan<byte> InitializeWithToken(ReadOnlySpan<byte> token);
	}
}
