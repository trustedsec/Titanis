using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	public enum KeyUsage
	{
		AsreqPaEncTimestamp = 1,
		Asrep_Tgsrep_Ticket = 2,
		AsrepEncPart = 3,
		TgsReq_KdcReqBody_AuthData_SessionKey = 4,
		TgsReq_KdcReqBody_AuthData_AuthSubkey = 5,
		TgsreqPatgsreqPadataApreqAuthChecksum_TgsSessionKey = 6,
		TgsreqPatgsreqPadataApreqAuthChecksum_TgsSessionKey_IncludesAuthSubkey = 7,
		TgsrepEncPart_SessionKey = 8,
		TgsrepEncPart_AuthSubkeyKey = 9,
		ApreqAuthChecksum_AppSessionKey = 10,
		ApreqAuth_AppSessionKey_IncludesAuthSubkey = 11,
		APRep_EncPart = 12,

		Priv = 13,
		Cred = 14,
		Safe = 15,

		// [MS-KILE] § 3.1.5.9
		NonKerbSalt = 16,
		NonKerbChecksumSalt = 17,

		// [RFC 4121] § Key Derivation for Per-Message Tokens
		AcceptorSeal = 22,
		AcceptorSign = 23,
		InitiatorSeal = 24,
		InitiatorSign = 25,
	}
}
