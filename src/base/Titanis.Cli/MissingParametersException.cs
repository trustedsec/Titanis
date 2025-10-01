using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Titanis.Cli
{

	/// <summary>
	/// Thrown when a command line does not specify a value for one or more mandatory parameters.
	/// </summary>
	[Serializable]
	public class MissingParametersException : SyntaxException
	{
		/// <summary>
		/// Initializes a new <see cref="MissingParametersException"/>.
		/// </summary>
		/// <param name="missingParameterNames">Names of missing parameters</param>
		public MissingParametersException(string[] missingParameterNames) : base(string.Format(Messages.Cli_MandatoryArgMissing_ParamNames, string.Join(", ", missingParameterNames)))
		{
		}

		/// <summary>
		/// Initializes a new <see cref="MissingParametersException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected MissingParametersException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
		}
	}
}
