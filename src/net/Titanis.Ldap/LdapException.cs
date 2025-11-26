using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	public enum LdapResultCode
	{
		Success = 0,
		OperationsError = 1,
		ProtocolError = 2,
		TimeLimitExceeded = 3,
		SizeLimitExceeded = 4,
		CompareFalse = 5,
		CompareTrue = 6,
		AuthMethodNotSupported = 7,
		StrongerAuthRequired = 8,
		Referral = 10,
		AdminLimitExceeded = 11,
		UnavailableCriticalExtension = 12,
		ConfidentialityRequired = 13,
		SaslBindInProgress = 14,
		NoSuchAttribute = 16,
		UndefinedAttributeType = 17,
		InappropriateMatching = 18,
		ConstraintViolation = 19,
		AttributeOrValueExists = 20,
		InvalidAttributeSyntax = 21,
		NoSuchObject = 32,
		AliasProblem = 33,
		InvalidDnSyntax = 34,
		AliasDeferenecingProblem = 36,
		InappropriateAuthentication = 48,
		InvalidCredentials = 49,
		InsufficientAccessRights = 50,
		Busy = 51,
		Unavailable = 52,
		UnwillingToPerform = 53,
		LoopDetect = 54,
		NamingViolation = 64,
		ObjectClassViolation = 65,
		NotAllowedOnNonLeaf = 66,
		NotAllowedOnRdn = 67,
		EntryAlreadyExists = 68,
		ObjectClassModsProhibited = 69,
		AffectsMultipleDsas = 71,
		Other = 80,
	}

	[Serializable]
	public class LdapException : Exception, IHaveErrorCode
	{
		public LdapException(LdapResultCode resultCode, string message) : base(message ?? GetMessageFor(resultCode))
		{
			ResultCode = resultCode;
		}
		public LdapException(LdapResultCode resultCode, Exception inner) : base(GetMessageFor(resultCode), inner) { }
		protected LdapException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
			this.ResultCode = (LdapResultCode)info.GetInt32(nameof(ResultCode));
		}

		public LdapResultCode ResultCode { get; }

		public int ErrorCode => (int)this.ResultCode;

		private static string GetMessageFor(LdapResultCode resultCode)
			=> $"An LDAP error has occurred: {resultCode}";

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue(nameof(ResultCode), (int)this.ResultCode);
		}
	}
}
