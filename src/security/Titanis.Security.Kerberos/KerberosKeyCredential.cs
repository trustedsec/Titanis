using KerberosV5Spec2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Represents a Kerberos credential provided as a protocol key.
	/// </summary>
	public class KerberosKeyCredential : KerberosKeyCredentialBase
	{
		/// <summary>
		/// Initializes a new <see cref="KerberosKeyCredential"/>.
		/// </summary>
		/// <param name="userName">User name</param>
		/// <param name="etype">Encryption type of the key</param>
		/// <param name="keyBytes">Bytes of the key</param>
		/// <exception cref="ArgumentNullException"></exception>
		public KerberosKeyCredential(UserPrincipalName userName, EType etype, byte[] keyBytes)
			: base(userName)
		{
			if (keyBytes is null)
				throw new ArgumentNullException(nameof(keyBytes));

			this._keys = new Dictionary<EType, EncryptionKey>(1) {
				{ etype, new EncryptionKey((int)etype, keyBytes) }
			};
		}
		/// <summary>
		/// Initializes a new <see cref="KerberosKeyCredential"/>.
		/// </summary>
		/// <param name="userName">User name</param>
		/// <param name="key">Encryption key</param>
		/// <exception cref="ArgumentNullException"></exception>
		public KerberosKeyCredential(UserPrincipalName userName, IEnumerable<EncryptionKey> key)
			: base(userName)
		{
			ArgumentNullException.ThrowIfNull(key);

			this._keys = key.ToDictionary(r => (EType)r.keytype);
		}

		private Dictionary<EType, EncryptionKey> _keys;

		/// <inheritdoc/>
		internal sealed override bool SupportsPreauthType(PadataType preauthType)
			=> preauthType is PadataType.EncTimestamp;
		/// <inheritdoc/>
		public sealed override bool SupportsProfile(EType etype) => this._keys.ContainsKey(etype);
		/// <inheritdoc/>
		public override SessionKey DeriveProtocolKeyFor(EncProfile profile, byte[]? salt)
		{
			if (this._keys.TryGetValue(profile.EType, out var key))
			{
				return new SessionKey(profile, key);
			}
			else
			{
				throw new NotSupportedException(Messages.Krb5_CredETypeMismatch);
			}
		}
	}
}
