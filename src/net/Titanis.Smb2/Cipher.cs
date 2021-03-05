namespace Titanis.Smb2
{
	/// <summary>
	/// Specifies a cipher within SMB2.
	/// </summary>
	public enum Cipher : ushort
	{
		None = 0,

		/// <summary>
		/// AES-128-CCM
		/// </summary>
		Aes128Ccm = 1,
		/// <summary>
		/// AES-128-GCM
		/// </summary>
		Aes128Gcm = 2,
		/// <summary>
		/// AES-256-CCM
		/// </summary>
		Aes256Ccm = 3,
		/// <summary>
		/// AES-256-GCM
		/// </summary>
		Aes256Gcm = 4,
	}
}
