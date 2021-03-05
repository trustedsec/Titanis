using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Thrown when an error occurs during a Kerberos protocol exchange.
	/// </summary>
	[Serializable]
	public class KerberosException : Exception, IHaveErrorCode
	{
		/// <summary>
		/// Gets the Kerberos error that caused the exception.
		/// </summary>
		public KerberosErrorCode ErrorCode { get; }

		/// <inheritdoc/>
		int IHaveErrorCode.ErrorCode => (int)this.ErrorCode;

		/// <summary>
		/// Initializes a new <see cref="KerberosException"/>
		/// </summary>
		/// <param name="errorCode">Kerberos error that caused the current exception</param>
		public KerberosException(KerberosErrorCode errorCode)
			: base(KerberosErrorMessages.TryGetErrorMessage(errorCode))
		{
			this.ErrorCode = errorCode;
		}

		/// <summary>
		/// Initializes a new <see cref="KerberosException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected KerberosException(
		  SerializationInfo info,
		  StreamingContext context) : base(info, context)
		{
			this.ErrorCode = (KerberosErrorCode)info.GetInt32(nameof(ErrorCode));
		}

		/// <inheritdoc/>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(this.ErrorCode), (int)this.ErrorCode);
			base.GetObjectData(info, context);
		}
	}
}
