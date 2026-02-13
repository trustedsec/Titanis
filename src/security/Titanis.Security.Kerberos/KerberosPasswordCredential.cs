using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Represents a Kerberos credential specified as a cleartext password.
	/// </summary>
	public class KerberosPasswordCredential : KerberosKeyCredentialBase
	{
		public KerberosPasswordCredential(UserPrincipalName userName, string password)
			: base(userName)
		{
			if (password is null)
				throw new ArgumentNullException(nameof(password));

			this.Password = password;
		}

		/// <summary>
		/// Gets the cleartext password.
		/// </summary>
		public string Password { get; }

		/// <inheritdoc/>
		internal sealed override bool SupportsPreauthType(PadataType preauthType)
			=> preauthType is PadataType.EncTimestamp;
		/// <inheritdoc/>
		public sealed override bool SupportsProfile(EType etype) => true;
		/// <inheritdoc/>
		public sealed override SessionKey DeriveProtocolKeyFor(EncProfile profile, byte[]? salt)
		{
			return profile.StringToKey(this.Password, salt ?? this.GetSalt());
		}
	}
}
