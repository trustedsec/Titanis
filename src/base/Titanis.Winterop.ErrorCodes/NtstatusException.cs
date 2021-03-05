using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace Titanis.Winterop
{
	/// <summary>
	/// Thrown when an operation returns an <see cref="Ntstatus"/> that indicates an error.
	/// </summary>
	[Serializable]
	public class NtstatusException : Exception
	{
		/// <summary>
		/// Gets the <see cref="Ntstatus"/> that specifies the error.
		/// </summary>
		public Ntstatus StatusCode { get; }

		/// <summary>
		/// Initializes a new 
		/// </summary>
		public NtstatusException() { }

		private static string BuildErrorMessage(Ntstatus statusCode)
		{
			return $"The operation failed with: {statusCode} (0x{(uint)statusCode:X8}).";
		}

		/// <summary>
		/// Initializes a new <see cref="NtstatusException"/>.
		/// </summary>
		/// <param name="statusCode">Status code</param>
		public NtstatusException(Ntstatus statusCode) : base(BuildErrorMessage(statusCode))
		{
			this.HResult = (int)statusCode;
			this.StatusCode = statusCode;
		}

		/// <summary>
		/// Initializes a new <see cref="NtstatusException"/>.
		/// </summary>
		/// <param name="statusCode">Status code</param>
		/// <param name="message">Message that describes the error</param>
		public NtstatusException(Ntstatus statusCode, string message) : base(message)
		{
			this.HResult = (int)statusCode;
			this.StatusCode = statusCode;
		}

		/// <summary>
		/// Initializes a new <see cref="NtstatusException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected NtstatusException(
		  SerializationInfo info,
		  StreamingContext context) : base(info, context)
		{
			this.StatusCode = (Ntstatus)info.GetInt32(nameof(StatusCode));
		}

		/// <inheritdoc/>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info is null) throw new ArgumentNullException(nameof(info));
			info.AddValue(nameof(StatusCode), (int)this.StatusCode);
			base.GetObjectData(info, context);
		}

		/// <summary>
		/// Checks an NTSTATUS value and throws if it indicates an error.
		/// </summary>
		/// <param name="status">NTSTATUS value</param>
		/// <exception cref="NtstatusException"><paramref name="status"/> indicates an error</exception>
		/// <returns>The original value of <paramref name="status"/></returns>
		public static Ntstatus CheckAndThrow(Ntstatus status)
		{
			if ((int)status < 0)
				throw new NtstatusException(status);

			return status;
		}
	}
}
