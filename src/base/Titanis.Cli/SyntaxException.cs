using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Thrown when a command line is not properly formed.
	/// </summary>
	public class SyntaxException : Exception
	{
		private const int E_INVALIDARG = unchecked((int)0x80070057);

		/// <summary>
		/// Initializes a new <see cref="SyntaxException"/>.
		/// </summary>
		/// <param name="message">Message that describes the error</param>
		public SyntaxException(string message)
			: base(message)
		{
			this.HResult = E_INVALIDARG;
		}
		/// <summary>
		/// Initializes a new <see cref="SyntaxException"/>.
		/// </summary>
		/// <param name="message">Message that describes the error</param>
		/// <param name="innerException">Exception that is the cause of the current exception, if any</param>
		public SyntaxException(string message, Exception? innerException)
			: base(message, innerException)
		{
			this.HResult = E_INVALIDARG;
		}

		internal string? commandPrefix;
		internal CommandBase? command;

		/// <summary>
		/// Initializes a new <see cref="SyntaxException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected SyntaxException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
		}
	}
}
