using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security.Kerberos
{
	public static class KerberosErrorMessages
	{
		public const string KDC_ERR_NONE_Message = @"No error";
		public const string KDC_ERR_NAME_EXP_Message = @"Client's entry in database has expired";
		public const string KDC_ERR_SERVICE_EXP_Message = @"Server's entry in database has expired";
		public const string KDC_ERR_BAD_PVNO_Message = @"Requested protocol version number not supported";
		public const string KDC_ERR_C_OLD_MAST_KVNO_Message = @"Client's key encrypted in old master key";
		public const string KDC_ERR_S_OLD_MAST_KVNO_Message = @"Server's key encrypted in old master key";
		public const string KDC_ERR_C_PRINCIPAL_UNKNOWN_Message = @"Client not found in Kerberos database";
		public const string KDC_ERR_S_PRINCIPAL_UNKNOWN_Message = @"Server not found in Kerberos database";
		public const string KDC_ERR_PRINCIPAL_NOT_UNIQUE_Message = @"Multiple principal entries in database";
		public const string KDC_ERR_NULL_KEY_Message = @"The client or server has a null key";
		public const string KDC_ERR_CANNOT_POSTDATE_Message = @"Ticket not eligible for postdating";
		public const string KDC_ERR_NEVER_VALID_Message = @"Requested starttime is later than end time";
		public const string KDC_ERR_POLICY_Message = @"KDC policy rejects request";
		public const string KDC_ERR_BADOPTION_Message = @"KDC cannot accommodate requested option";
		public const string KDC_ERR_ETYPE_NOSUPP_Message = @"KDC has no support for encryption type";
		public const string KDC_ERR_SUMTYPE_NOSUPP_Message = @"KDC has no support for checksum type";
		public const string KDC_ERR_PADATA_TYPE_NOSUPP_Message = @"KDC has no support for padata type";
		public const string KDC_ERR_TRTYPE_NOSUPP_Message = @"KDC has no support for transited type";
		public const string KDC_ERR_CLIENT_REVOKED_Message = @"Clients credentials have been revoked";
		public const string KDC_ERR_SERVICE_REVOKED_Message = @"Credentials for server have been revoked";
		public const string KDC_ERR_TGT_REVOKED_Message = @"TGT has been revoked";
		public const string KDC_ERR_CLIENT_NOTYET_Message = @"Client not yet valid; try again later";
		public const string KDC_ERR_SERVICE_NOTYET_Message = @"Server not yet valid; try again later";
		public const string KDC_ERR_KEY_EXPIRED_Message = @"Password has expired; change password to reset";
		public const string KDC_ERR_PREAUTH_FAILED_Message = @"Pre-authentication information was invalid";
		public const string KDC_ERR_PREAUTH_REQUIRED_Message = @"Additional pre- authentication required";
		public const string KDC_ERR_SERVER_NOMATCH_Message = @"Requested server and ticket don't match";
		public const string KDC_ERR_MUST_USE_USER2USER_Message = @"Server principal valid for user2user only";
		public const string KDC_ERR_PATH_NOT_ACCEPTED_Message = @"KDC Policy rejects transited path";
		public const string KDC_ERR_SVC_UNAVAILABLE_Message = @"A service is not available";
		public const string KRB_AP_ERR_BAD_INTEGRITY_Message = @"Integrity check on decrypted field failed";
		public const string KRB_AP_ERR_TKT_EXPIRED_Message = @"Ticket expired";
		public const string KRB_AP_ERR_TKT_NYV_Message = @"Ticket not yet valid";
		public const string KRB_AP_ERR_REPEAT_Message = @"Request is a replay";
		public const string KRB_AP_ERR_NOT_US_Message = @"The ticket isn't for us";
		public const string KRB_AP_ERR_BADMATCH_Message = @"Ticket and authenticator don't match";
		public const string KRB_AP_ERR_SKEW_Message = @"Clock skew too great";
		public const string KRB_AP_ERR_BADADDR_Message = @"Incorrect net address";
		public const string KRB_AP_ERR_BADVERSION_Message = @"Protocol version mismatch";
		public const string KRB_AP_ERR_MSG_TYPE_Message = @"Invalid msg type";
		public const string KRB_AP_ERR_MODIFIED_Message = @"Message stream modified";
		public const string KRB_AP_ERR_BADORDER_Message = @"Message out of order";
		public const string KRB_AP_ERR_BADKEYVER_Message = @"Specified version of key is not available";
		public const string KRB_AP_ERR_NOKEY_Message = @"Service key not available";
		public const string KRB_AP_ERR_MUT_FAIL_Message = @"Mutual authentication failed";
		public const string KRB_AP_ERR_BADDIRECTION_Message = @"Incorrect message direction";
		public const string KRB_AP_ERR_METHOD_Message = @"Alternative authentication method required";
		public const string KRB_AP_ERR_BADSEQ_Message = @"Incorrect sequence number in message";
		public const string KRB_AP_ERR_INAPP_CKSUM_Message = @"Inappropriate type of checksum in message";
		public const string KRB_AP_PATH_NOT_ACCEPTED_Message = @"Policy rejects transited path";
		public const string KRB_ERR_RESPONSE_TOO_BIG_Message = @"Response too big for UDP; retry with TCP";
		public const string KRB_ERR_GENERIC_Message = @"Generic error(description in e-text)";
		public const string KRB_ERR_FIELD_TOOLONG_Message = @"Field is too long for this implementation";
		public const string KDC_ERROR_CLIENT_NOT_TRUSTED_Message = @"Reserved for PKINIT";
		public const string KDC_ERROR_KDC_NOT_TRUSTED_Message = @"Reserved for PKINIT";
		public const string KDC_ERROR_INVALID_SIG_Message = @"Reserved for PKINIT";
		public const string KDC_ERR_KEY_TOO_WEAK_Message = @"Reserved for PKINIT";
		public const string KDC_ERR_CERTIFICATE_MISMATCH_Message = @"Reserved for PKINIT";
		public const string KRB_AP_ERR_NO_TGT_Message = @"No TGT available to validate USER-TO-USER";
		public const string KDC_ERR_WRONG_REALM_Message = @"Cannot provide a ticket for the target realm";
		public const string KRB_AP_ERR_USER_TO_USER_REQUIRED_Message = @"Ticket must be for USER-TO-USER";
		public const string KDC_ERR_CANT_VERIFY_CERTIFICATE_Message = @"Reserved for PKINIT";
		public const string KDC_ERR_INVALID_CERTIFICATE_Message = @"Reserved for PKINIT";
		public const string KDC_ERR_REVOKED_CERTIFICATE_Message = @"Reserved for PKINIT";
		public const string KDC_ERR_REVOCATION_STATUS_UNKNOWN_Message = @"Reserved for PKINIT";
		public const string KDC_ERR_REVOCATION_STATUS_UNAVAILABLE_Message = @"Reserved for PKINIT";
		public const string KDC_ERR_CLIENT_NAME_MISMATCH_Message = @"Reserved for PKINIT";
		public const string KDC_ERR_KDC_NAME_MISMATCH_Message = @"Reserved for PKINIT";

		public static string GetErrorMessage(KerberosErrorCode errorCode)
		{
			if (_messageTable.TryGetValue(errorCode, out string message))
				return message;
			else
				throw new ArgumentOutOfRangeException(nameof(errorCode));
		}
		public static string TryGetErrorMessage(KerberosErrorCode errorCode)
		{
			_messageTable.TryGetValue(errorCode, out string message);
			return message;
		}

		private static readonly Dictionary<KerberosErrorCode, string> _messageTable = new Dictionary<KerberosErrorCode, string>()
		{
			{ KerberosErrorCode.KDC_ERR_NONE, KDC_ERR_NONE_Message },
			{ KerberosErrorCode.KDC_ERR_NAME_EXP, KDC_ERR_NAME_EXP_Message },
			{ KerberosErrorCode.KDC_ERR_SERVICE_EXP, KDC_ERR_SERVICE_EXP_Message },
			{ KerberosErrorCode.KDC_ERR_BAD_PVNO, KDC_ERR_BAD_PVNO_Message },
			{ KerberosErrorCode.KDC_ERR_C_OLD_MAST_KVNO, KDC_ERR_C_OLD_MAST_KVNO_Message },
			{ KerberosErrorCode.KDC_ERR_S_OLD_MAST_KVNO, KDC_ERR_S_OLD_MAST_KVNO_Message },
			{ KerberosErrorCode.KDC_ERR_C_PRINCIPAL_UNKNOWN, KDC_ERR_C_PRINCIPAL_UNKNOWN_Message },
			{ KerberosErrorCode.KDC_ERR_S_PRINCIPAL_UNKNOWN, KDC_ERR_S_PRINCIPAL_UNKNOWN_Message },
			{ KerberosErrorCode.KDC_ERR_PRINCIPAL_NOT_UNIQUE, KDC_ERR_PRINCIPAL_NOT_UNIQUE_Message },
			{ KerberosErrorCode.KDC_ERR_NULL_KEY, KDC_ERR_NULL_KEY_Message },
			{ KerberosErrorCode.KDC_ERR_CANNOT_POSTDATE, KDC_ERR_CANNOT_POSTDATE_Message },
			{ KerberosErrorCode.KDC_ERR_NEVER_VALID, KDC_ERR_NEVER_VALID_Message },
			{ KerberosErrorCode.KDC_ERR_POLICY, KDC_ERR_POLICY_Message },
			{ KerberosErrorCode.KDC_ERR_BADOPTION, KDC_ERR_BADOPTION_Message },
			{ KerberosErrorCode.KDC_ERR_ETYPE_NOSUPP, KDC_ERR_ETYPE_NOSUPP_Message },
			{ KerberosErrorCode.KDC_ERR_SUMTYPE_NOSUPP, KDC_ERR_SUMTYPE_NOSUPP_Message },
			{ KerberosErrorCode.KDC_ERR_PADATA_TYPE_NOSUPP, KDC_ERR_PADATA_TYPE_NOSUPP_Message },
			{ KerberosErrorCode.KDC_ERR_TRTYPE_NOSUPP, KDC_ERR_TRTYPE_NOSUPP_Message },
			{ KerberosErrorCode.KDC_ERR_CLIENT_REVOKED, KDC_ERR_CLIENT_REVOKED_Message },
			{ KerberosErrorCode.KDC_ERR_SERVICE_REVOKED, KDC_ERR_SERVICE_REVOKED_Message },
			{ KerberosErrorCode.KDC_ERR_TGT_REVOKED, KDC_ERR_TGT_REVOKED_Message },
			{ KerberosErrorCode.KDC_ERR_CLIENT_NOTYET, KDC_ERR_CLIENT_NOTYET_Message },
			{ KerberosErrorCode.KDC_ERR_SERVICE_NOTYET, KDC_ERR_SERVICE_NOTYET_Message },
			{ KerberosErrorCode.KDC_ERR_KEY_EXPIRED, KDC_ERR_KEY_EXPIRED_Message },
			{ KerberosErrorCode.KDC_ERR_PREAUTH_FAILED, KDC_ERR_PREAUTH_FAILED_Message },
			{ KerberosErrorCode.KDC_ERR_PREAUTH_REQUIRED, KDC_ERR_PREAUTH_REQUIRED_Message },
			{ KerberosErrorCode.KDC_ERR_SERVER_NOMATCH, KDC_ERR_SERVER_NOMATCH_Message },
			{ KerberosErrorCode.KDC_ERR_MUST_USE_USER2USER, KDC_ERR_MUST_USE_USER2USER_Message },
			{ KerberosErrorCode.KDC_ERR_PATH_NOT_ACCEPTED, KDC_ERR_PATH_NOT_ACCEPTED_Message },
			{ KerberosErrorCode.KDC_ERR_SVC_UNAVAILABLE, KDC_ERR_SVC_UNAVAILABLE_Message },
			{ KerberosErrorCode.KRB_AP_ERR_BAD_INTEGRITY, KRB_AP_ERR_BAD_INTEGRITY_Message },
			{ KerberosErrorCode.KRB_AP_ERR_TKT_EXPIRED, KRB_AP_ERR_TKT_EXPIRED_Message },
			{ KerberosErrorCode.KRB_AP_ERR_TKT_NYV, KRB_AP_ERR_TKT_NYV_Message },
			{ KerberosErrorCode.KRB_AP_ERR_REPEAT, KRB_AP_ERR_REPEAT_Message },
			{ KerberosErrorCode.KRB_AP_ERR_NOT_US, KRB_AP_ERR_NOT_US_Message },
			{ KerberosErrorCode.KRB_AP_ERR_BADMATCH, KRB_AP_ERR_BADMATCH_Message },
			{ KerberosErrorCode.KRB_AP_ERR_SKEW, KRB_AP_ERR_SKEW_Message },
			{ KerberosErrorCode.KRB_AP_ERR_BADADDR, KRB_AP_ERR_BADADDR_Message },
			{ KerberosErrorCode.KRB_AP_ERR_BADVERSION, KRB_AP_ERR_BADVERSION_Message },
			{ KerberosErrorCode.KRB_AP_ERR_MSG_TYPE, KRB_AP_ERR_MSG_TYPE_Message },
			{ KerberosErrorCode.KRB_AP_ERR_MODIFIED, KRB_AP_ERR_MODIFIED_Message },
			{ KerberosErrorCode.KRB_AP_ERR_BADORDER, KRB_AP_ERR_BADORDER_Message },
			{ KerberosErrorCode.KRB_AP_ERR_BADKEYVER, KRB_AP_ERR_BADKEYVER_Message },
			{ KerberosErrorCode.KRB_AP_ERR_NOKEY, KRB_AP_ERR_NOKEY_Message },
			{ KerberosErrorCode.KRB_AP_ERR_MUT_FAIL, KRB_AP_ERR_MUT_FAIL_Message },
			{ KerberosErrorCode.KRB_AP_ERR_BADDIRECTION, KRB_AP_ERR_BADDIRECTION_Message },
			{ KerberosErrorCode.KRB_AP_ERR_METHOD, KRB_AP_ERR_METHOD_Message },
			{ KerberosErrorCode.KRB_AP_ERR_BADSEQ, KRB_AP_ERR_BADSEQ_Message },
			{ KerberosErrorCode.KRB_AP_ERR_INAPP_CKSUM, KRB_AP_ERR_INAPP_CKSUM_Message },
			{ KerberosErrorCode.KRB_AP_PATH_NOT_ACCEPTED, KRB_AP_PATH_NOT_ACCEPTED_Message },
			{ KerberosErrorCode.KRB_ERR_RESPONSE_TOO_BIG, KRB_ERR_RESPONSE_TOO_BIG_Message },
			{ KerberosErrorCode.KRB_ERR_GENERIC, KRB_ERR_GENERIC_Message },
			{ KerberosErrorCode.KRB_ERR_FIELD_TOOLONG, KRB_ERR_FIELD_TOOLONG_Message },
			{ KerberosErrorCode.KDC_ERROR_CLIENT_NOT_TRUSTED, KDC_ERROR_CLIENT_NOT_TRUSTED_Message },
			{ KerberosErrorCode.KDC_ERROR_KDC_NOT_TRUSTED, KDC_ERROR_KDC_NOT_TRUSTED_Message },
			{ KerberosErrorCode.KDC_ERROR_INVALID_SIG, KDC_ERROR_INVALID_SIG_Message },
			{ KerberosErrorCode.KDC_ERR_KEY_TOO_WEAK, KDC_ERR_KEY_TOO_WEAK_Message },
			{ KerberosErrorCode.KDC_ERR_CERTIFICATE_MISMATCH, KDC_ERR_CERTIFICATE_MISMATCH_Message },
			{ KerberosErrorCode.KRB_AP_ERR_NO_TGT, KRB_AP_ERR_NO_TGT_Message },
			{ KerberosErrorCode.KDC_ERR_WRONG_REALM, KDC_ERR_WRONG_REALM_Message },
			{ KerberosErrorCode.KRB_AP_ERR_USER_TO_USER_REQUIRED, KRB_AP_ERR_USER_TO_USER_REQUIRED_Message },
			{ KerberosErrorCode.KDC_ERR_CANT_VERIFY_CERTIFICATE, KDC_ERR_CANT_VERIFY_CERTIFICATE_Message },
			{ KerberosErrorCode.KDC_ERR_INVALID_CERTIFICATE, KDC_ERR_INVALID_CERTIFICATE_Message },
			{ KerberosErrorCode.KDC_ERR_REVOKED_CERTIFICATE, KDC_ERR_REVOKED_CERTIFICATE_Message },
			{ KerberosErrorCode.KDC_ERR_REVOCATION_STATUS_UNKNOWN, KDC_ERR_REVOCATION_STATUS_UNKNOWN_Message },
			{ KerberosErrorCode.KDC_ERR_REVOCATION_STATUS_UNAVAILABLE, KDC_ERR_REVOCATION_STATUS_UNAVAILABLE_Message },
			{ KerberosErrorCode.KDC_ERR_CLIENT_NAME_MISMATCH, KDC_ERR_CLIENT_NAME_MISMATCH_Message },
			{ KerberosErrorCode.KDC_ERR_KDC_NAME_MISMATCH, KDC_ERR_KDC_NAME_MISMATCH_Message },
		};
	}
}
