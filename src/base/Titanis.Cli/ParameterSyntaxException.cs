using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Thrown when there is a problem with a command line parameter.
	/// </summary>
	public class ParameterSyntaxException : SyntaxException
	{
		/// <summary>
		/// Initializes a new <see cref="ParameterSyntaxException"/>.
		/// </summary>
		/// <param name="parameterName">Name of problematic parameter</param>
		/// <param name="message">Messaging describing the error</param>
		public ParameterSyntaxException(string parameterName, string message)
			: base(message)
		{
		}
		/// <summary>
		/// Initializes a new <see cref="ParameterSyntaxException"/>.
		/// </summary>
		/// <param name="parameterName">Name of problematic parameter</param>
		/// <param name="message">Message that describes the error</param>
		/// <param name="innerException">Exception that is the cause of the current exception, if any</param>
		public ParameterSyntaxException(string parameterName, string message, Exception? innerException)
			: base(message, innerException)
		{
		}
		/// <summary>
		/// Initializes a new <see cref="UnrecognizedParameterException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected ParameterSyntaxException(
		  SerializationInfo info,
		  StreamingContext context) : base(info, context)
		{
			this.ParameterName = info.GetString(nameof(ParameterName));
		}

		/// <summary>
		/// Gets the name of the problematic parameter.
		/// </summary>
		public string ParameterName { get; }

		/// <inheritdoc/>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(ParameterName), this.ParameterName);
			base.GetObjectData(info, context);
		}
	}
}
