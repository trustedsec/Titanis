using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Represents a Kerberos credential provided as a protocol key.
	/// </summary>
	public class KerberosKeyCredential : KerberosCredential
	{
		/// <summary>
		/// Initializes a new <see cref="KerberosKeyCredential"/>.
		/// </summary>
		/// <param name="userName">User name</param>
		/// <param name="realm">Realm</param>
		/// <param name="etype">Encryption type of the key</param>
		/// <param name="keyBytes">Bytes of the key</param>
		/// <exception cref="ArgumentNullException"></exception>
		public KerberosKeyCredential(string userName, string realm, EType etype, byte[] keyBytes)
			: base(userName, realm)
		{
			if (keyBytes is null)
				throw new ArgumentNullException(nameof(keyBytes));

			this.KeyBytes = keyBytes;
			this.EType = etype;
		}

		/// <summary>
		/// Gets the bytes of the hash or key.
		/// </summary>
		public byte[] KeyBytes { get; }
		/// <summary>
		/// Gets the encryption type of the key.
		/// </summary>
		internal EType EType { get; }

		/// <inheritdoc/>
		public sealed override bool SupportsProfile(EncProfile profile) => (profile != null) && (profile.EType == this.EType);
		/// <inheritdoc/>
		public override SessionKey DeriveProtocolKeyFor(EncProfile profile, byte[]? salt)
		{
			if (profile.EType == this.EType)
			{
				return new SessionKey(profile, this.KeyBytes);
			}
			else
			{
				throw new NotSupportedException(Messages.Krb5_CredETypeMismatch);
			}
		}
	}
}
