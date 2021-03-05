using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Thrown when a command line contains a parameter that is not recognized.
	/// </summary>
	[Serializable]
	public class UnrecognizedParameterException : ParameterSyntaxException
	{
		/// <summary>
		/// Initializes a new <see cref="UnrecognizedParameterException"/>.
		/// </summary>
		/// <param name="parameterName">Name of unrecognized parameter</param>
		public UnrecognizedParameterException(string parameterName) : base(parameterName, string.Format(Messages.Cli_UnrecognizedParam, parameterName))
		{
		}
		/// <summary>
		/// Initializes a new <see cref="UnrecognizedParameterException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected UnrecognizedParameterException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
		}
	}
}
