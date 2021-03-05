namespace Titanis.DceRpc
{
	// [MS-RPCE] § 2.2.1.1.7 - Security Providers
	public enum RpcAuthType : byte
	{
		None = 0,
		//Kerberos5=1,
		Ntlm = 10,

		Spnego = 9,
		Tls = 0x0E,
		Kerberos = 0x10,
		Netlogon = 0x44,
		Default = 0xFF,

		// [MSDN] Authentication-Service Constants
		DcePrivateKey = 1,
		DcePublic = 2,
		DecPublicKey = 4,
		DistributedPasswordAuthentication = 17,
		MicrosoftNetwork = 18,
		Digest = 21,
		NegoExtender = 30,
		MicrosoftMessageQueue = 100,
	}
}
