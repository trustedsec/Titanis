using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Specifies the type of preauthentication data.
	/// </summary>
	// [RFC 4120] § 7.5.2
	enum PadataType : ushort // Underlying used by CCache
	{
		TgsReq = 1,
		EncTimestamp = 2,
		PasswordSalt = 3,
		ETypeInfo = 11,
		PkASreqOld = 14,
		PkASrepOld = 15,
		PkASReq = 16,
		PkASRep = 17,
		ETypeInfo2 = 19,
		PacRequest = 128,

		// [RFC 6806] Appendix A
		SvrReferralInfo = 20,

		// [RFC 6113]
		FxCookie = 133,
		FxFast = 136,
		FxError = 137,
		EncryptedChallenge = 138,

		// [MS-KILE] § 3.1.5.1 - Pre-authentication Data
		SupportedEncTypes = 165,
		PacOptions = 167,
		KerbKeyListReq = 161,
		KerbKeyListRep = 162,
	}
}
