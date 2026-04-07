using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using Titanis.Winterop;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Thrown when an error occurs during a Kerberos protocol exchange.
	/// </summary>
	/// <remarks>
	/// If the KDC returns an extended error code, that error code is returned through the <see cref="IHaveErrorCode"/>.  For Active Directory, this error code may be distinguished from a Kerberos error code by checking that the high bit is set.
	/// </remarks>
	[Serializable]
	public class KerberosException : Exception, IHaveErrorCode
	{
		/// <summary>
		/// Gets the Kerberos error that caused the exception.
		/// </summary>
		public KerberosErrorCode KerberosErrorCode { get; }

		/// <summary>
		/// Gets the underlying error code, if sent.
		/// </summary>
		public Ntstatus? UnderlyingNtstatus { get; set; }

		private int _effErrorCode;
		/// <inheritdoc/>
		int IHaveErrorCode.ErrorCode => this._effErrorCode;

		/// <summary>
		/// Initializes a new <see cref="KerberosException"/>
		/// </summary>
		/// <param name="errorCode">Kerberos error that caused the current exception</param>
		public KerberosException(KerberosErrorCode errorCode, Ntstatus? underlyingNtstatus, string? details = null)
			: base(BuildMessage(errorCode, underlyingNtstatus, details))
		{
			this.KerberosErrorCode = errorCode;
			this.UnderlyingNtstatus = underlyingNtstatus;
			if (underlyingNtstatus.HasValue)
				this._effErrorCode = (int)underlyingNtstatus.Value;
			else
				this._effErrorCode = (int)errorCode;
		}

		private static string BuildMessage(KerberosErrorCode errorCode, Ntstatus? ntstatus, string? details)
		{
			var message = KerberosErrorMessages.TryGetErrorMessage(errorCode);
			if (ntstatus.HasValue)
				message += "  " + ntstatus.Value.GetErrorMessage();
			if (details != null)
				message += "  Details: " + details;
			return message;
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
			this.KerberosErrorCode = (KerberosErrorCode)info.GetInt32(nameof(KerberosErrorCode));
		}

		/// <inheritdoc/>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(this.KerberosErrorCode), (int)this.KerberosErrorCode);
			base.GetObjectData(info, context);
		}
	}
}
