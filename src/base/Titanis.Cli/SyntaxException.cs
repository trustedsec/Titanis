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
		public SyntaxException(IReadOnlyList<ParameterValidationError> errors, string? message = null)
			: base(message ?? GenerateMessage(errors))
		{
			this.HResult = E_INVALIDARG;
			Errors = errors;
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

		public IReadOnlyList<ParameterValidationError>? Errors { get; }

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


		/// <summary>
		/// Generates a message describing the logged errors.
		/// </summary>
		/// <returns>A string</returns>
		private static string GenerateMessage(IReadOnlyList<ParameterValidationError> errors)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("One or more problems were encountered during parameter validation.");

			if (errors != null)
			{
				foreach (var error in errors)
				{
					var name = error.ParameterName;
					if (string.IsNullOrEmpty(name))
						sb.AppendLine(error.Message);
					else
						sb.AppendLine($"{name}: {error.Message}");
				}
			}

			return sb.ToString();
		}
	}
}
