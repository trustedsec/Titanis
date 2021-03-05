using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Represents a credential used to authenticate to the Kerberos Authentication Service.
	/// </summary>
	public abstract class KerberosCredential
	{
		/// <summary>
		/// Initializes a new <see cref="KerberosCredential"/>.
		/// </summary>
		/// <param name="userName">User name</param>
		/// <param name="realm">Realm</param>
		/// <exception cref="ArgumentNullException"><paramref name="userName"/> or <paramref name="realm"/> is <see langword="null"/> or empty.</exception>
		protected KerberosCredential(string userName, string realm)
		{
			if (string.IsNullOrEmpty(userName))
				throw new ArgumentNullException(nameof(userName));
			if (string.IsNullOrEmpty(realm))
				throw new ArgumentNullException(nameof(realm));

			this.UserName = userName;
			this.Realm = realm.ToUpper();
		}

		/// <summary>
		/// Gets the user name.
		/// </summary>
		public string UserName { get; }
		/// <summary>
		/// Gets the realm.
		/// </summary>
		public string Realm { get; }

		/// <summary>
		/// Gets the key salt.
		/// </summary>
		/// <returns>A byte array of key salt data</returns>
		// [MS-KILE] § 3.1.1.2 Cryptographic Material
		public byte[] GetSalt()
			=> Encoding.UTF8.GetBytes(this.Realm.ToUpper() + this.UserName);

		/// <summary>
		/// Checks whether this credential supports a specified profile.
		/// </summary>
		/// <param name="profile">Encryption profile</param>
		/// <returns><see langword="true"/> if this credential supports <paramref name="profile"/>; otherwise, <see langword="false"/>.</returns>
		/// 
		public abstract bool SupportsProfile(EncProfile profile);
		/// <summary>
		/// Derives a protocol key from the credential.
		/// </summary>
		/// <param name="profile">Encryption profile</param>
		/// <param name="salt">Key salt</param>
		/// <returns>A <see cref="SessionKey"/> suitable for <paramref name="profile"/></returns>
		public abstract SessionKey DeriveProtocolKeyFor(EncProfile profile, byte[]? salt);
	}
}
