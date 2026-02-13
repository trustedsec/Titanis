using KerberosV5Spec2;
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
		/// <exception cref="ArgumentNullException"><paramref name="userName"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="userName"/> doesn't specify a <see cref="UserPrincipalName.Realm"/>.</exception>
		protected KerberosCredential(UserPrincipalName userName)
		{
			ArgumentNullException.ThrowIfNull(userName);

			this.UserName = userName;
			if (string.IsNullOrEmpty(userName.Realm))
				throw new ArgumentException($"The user name must specify a realm.", nameof(userName));

			this.UserName = userName;
		}

		/// <summary>
		/// Gets the user name.
		/// </summary>
		public UserPrincipalName UserName { get; }
		/// <summary>
		/// Gets the name of the realm.
		/// </summary>
		public string Realm => this.UserName.Realm!;

		/// <summary>
		/// Gets the key salt.
		/// </summary>
		/// <returns>A byte array of key salt data</returns>
		// [MS-KILE] § 3.1.1.2 Cryptographic Material
		public byte[] GetSalt()
			=> Encoding.UTF8.GetBytes(this.Realm.ToUpper() + this.UserName.UserName);

		internal abstract bool SupportsPreauthType(PadataType preauthType);
		/// <summary>
		/// Checks whether this credential supports a specified profile.
		/// </summary>
		/// <param name="etype">Encryption type</param>
		/// <returns><see langword="true"/> if this credential supports <paramref name="etype"/>; otherwise, <see langword="false"/>.</returns>
		/// 
		public abstract bool SupportsProfile(EType etype);
		/// <summary>
		/// Derives a protocol key from the credential.
		/// </summary>
		/// <param name="profile">Encryption profile</param>
		/// <param name="salt">Key salt</param>
		/// <returns>A <see cref="SessionKey"/> suitable for <paramref name="profile"/></returns>
		public abstract SessionKey DeriveProtocolKeyFor(EncProfile profile, byte[]? salt);

		internal abstract PreauthContext CreatePreauthContext(KerberosClient client, IKerberosCallback? callback);
	}
}
