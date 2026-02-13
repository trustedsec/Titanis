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
		public KerberosNullCredential(UserPrincipalName userName)
			: base(userName)
		{
		}

		/// <inheritdoc/>
		internal sealed override bool SupportsPreauthType(PadataType preauthType) => false;
		/// <inheritdoc/>
		public sealed override bool SupportsProfile(EType etype) => false;
		/// <inheritdoc/>
		public sealed override SessionKey DeriveProtocolKeyFor(EncProfile profile, byte[]? salt)
		{
			throw new NotSupportedException("This credential cannot be used with this encryption profile.");
		}

		internal override PreauthContext CreatePreauthContext(KerberosClient client, IKerberosCallback? callback)
		{
			return new PreauthNullContext(client, this, callback);
		}
	}
}
