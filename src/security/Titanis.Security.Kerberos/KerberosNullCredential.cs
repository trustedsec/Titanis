using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Represents a Kerberos credential with no key.
	/// </summary>
	/// <remarks>
	/// This type of credential is only useful in pass-the-ticket scenarios.
	/// </remarks>
	public sealed class KerberosNullCredential : KerberosCredential
	{
		public KerberosNullCredential(string userName, string realm)
			: base(userName, realm)
		{
		}

		/// <inheritdoc/>
		public sealed override bool SupportsProfile(EncProfile profile) => false;
		/// <inheritdoc/>
		public sealed override SessionKey DeriveProtocolKeyFor(EncProfile profile, byte[]? salt)
		{
			throw new NotSupportedException("This credential cannot be used with this encryption profile.");
		}
	}
}
