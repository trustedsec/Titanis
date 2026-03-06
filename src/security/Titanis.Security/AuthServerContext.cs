using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security
{
	public abstract class AuthServerContext : AuthContext
	{
		private byte[]? _token;
		public sealed override ReadOnlySpan<byte> Token => this._token;
		public ReadOnlySpan<byte> Accept()
		{
			var token = this.AcceptImpl();
			this._token = token.ToArray();
			return token;
		}
		protected abstract ReadOnlySpan<byte> AcceptImpl();

		public ReadOnlySpan<byte> Accept(ReadOnlySpan<byte> token)
		{
			var resp = this.AcceptImpl(token);
			this._token = resp.ToArray();
			return resp;
		}
		protected abstract ReadOnlySpan<byte> AcceptImpl(ReadOnlySpan<byte> token);
	}
}
