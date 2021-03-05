using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Describes an error during parameter validation.
	/// </summary>
	public class ParameterValidationError
	{
		/// <summary>
		/// Initializes a new <see cref="ParameterValidationError"/>.
		/// </summary>
		/// <param name="parameterName">Name of parameter</param>
		/// <param name="message">Message describing the error</param>
		public ParameterValidationError(string? parameterName, string message)
		{
			this.ParameterName = parameterName;
			this.Message = message;
		}

		/// <summary>
		/// Gets the name of the parameter.
		/// </summary>
		public string? ParameterName { get; }
		/// <summary>
		/// Gets the message describing the error.
		/// </summary>
		public string Message { get; }
	}

	/// <summary>
	/// Represents a parameter validation context.
	/// </summary>
	/// <remarks>
	/// Use <see cref="LogError(ParameterValidationError)"/> (or another overload) to report an error.
	/// </remarks>
	/// <seealso cref="Command.ValidateParameters(ParameterValidationContext)"/>
	public class ParameterValidationContext
	{
		private List<ParameterValidationError> _errors = new List<ParameterValidationError>();

		/// <summary>
		/// Gets a list of reported errors.
		/// </summary>
		public IReadOnlyList<ParameterValidationError> Errors => this._errors;

		/// <summary>
		/// Logs a validation error.
		/// </summary>
		/// <param name="message">Message describing the error</param>
		public void LogError(string message)
			=> this.LogError(new ParameterValidationError(null, message));
		/// <summary>
		/// Logs a validation error.
		/// </summary>
		/// <param name="parameterName">Name of parameter</param>
		/// <param name="message">Message describing the error</param>
		public void LogError(string? parameterName, string message)
			=> this.LogError(new ParameterValidationError(parameterName, message));
		/// <summary>
		/// Logs a validation error.
		/// </summary>
		/// <param name="error">Validation error</param>
		public void LogError(ParameterValidationError error)
		{
			if (error is null) throw new ArgumentNullException(nameof(error));
			this._errors.Add(error);
		}

		/// <summary>
		/// Generates a message describing the logged errors.
		/// </summary>
		/// <returns>A string</returns>
		public string GenerateMessage()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("One or more problems were encountered during parameter validation.");
			foreach (var error in this.Errors)
			{
				var name = error.ParameterName;
				if (string.IsNullOrEmpty(name))
					sb.AppendLine(error.Message);
				else
					sb.AppendLine($"{name}: {error.Message}");
			}

			return sb.ToString();
		}
	}
}
