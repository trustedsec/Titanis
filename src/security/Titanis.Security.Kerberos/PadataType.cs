using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Specifies the type of preauthentication data.
	/// </summary>
	// [RFC 4120] § 7.5.2
	public enum PadataType : ushort // Underlying used by CCache
	{
		TgsReq = 1,
		EncTimestamp = 2,
		PasswordSalt = 3,
		ETypeInfo = 11,
		PkASreqOld = 14,
		PkASrepOld = 15,
		ETypeInfo2 = 19,
		PacRequest = 128,

		// [RFC 4556] § 3.1.3
		PkASReq = 16,
		PkASRep = 17,


		// [RFC 8070]
		AsFreshness = 150,

		// [MS-SFU] § 2.2.1
		S4u2Self_PaForUser = 129,
		// [MS-SFU] § 2.2.2
		S4u2Self_X509User = 130,

		// [RFC 6806] Appendix A
		SvrReferralInfo = 20,

		// [RFC 6113] § 6.4
		FxCookie = 133,
		AuthenticationSet = 134,
		AuthSetSelected = 135,
		FxFast = 136,
		FxError = 137,
		EncryptedChallenge = 138,

		// [MS-KILE] § 3.1.5.1 - Pre-authentication Data
		SupportedEncTypes = 165,
		PacOptions = 167,
		KerbKeyListReq = 161,
		KerbKeyListRep = 162,

		// [RFC 8636] § 4
		TdCmsDigestAlgorithms = 111,
	}
}
