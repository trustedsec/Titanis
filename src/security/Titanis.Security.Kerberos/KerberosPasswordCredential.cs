using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Represents a Kerberos credential specified as a cleartext password.
	/// </summary>
	public class KerberosPasswordCredential : KerberosCredential
	{
		public KerberosPasswordCredential(string userName, string realm, string password)
			: base(userName, realm)
		{
			if (password is null)
				throw new ArgumentNullException(nameof(password));

			this.Password = password;
		}

		public string Password { get; }

		/// <inheritdoc/>
		public sealed override bool SupportsProfile(EncProfile profile) => (profile != null);
		/// <inheritdoc/>
		public sealed override SessionKey DeriveProtocolKeyFor(EncProfile profile, byte[]? salt)
		{
			return profile.StringToKey(this.Password, salt ?? this.GetSalt());
		}
	}
}
