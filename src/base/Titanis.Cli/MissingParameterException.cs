using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Titanis.Cli
{

	/// <summary>
	/// Thrown when a command line does not specify a value for a mandatory parameter.
	/// </summary>
	[Serializable]
	public class MissingParameterException : ParameterSyntaxException
	{
		/// <summary>
		/// Initializes a new <see cref="MissingParameterException"/>.
		/// </summary>
		/// <param name="parameterName">Name of problematic parameter</param>
		public MissingParameterException(string parameterName) : base(parameterName, string.Format(Messages.Cli_MandatoryArgMissing_ParamName, parameterName))
		{
		}

		/// <summary>
		/// Initializes a new <see cref="MissingParameterException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected MissingParameterException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
		}
	}
}
