using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{

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
