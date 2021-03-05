using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies the format string and formatter to use for an output field.
	/// </summary>
	/// <remarks>
	/// Apply this to a <see cref="Command"/> implementation to override the format string
	/// for a field or to supply a custom <see cref="IOutputFormatter"/>.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class OutputFieldFormatAttribute : Attribute
	{
		public OutputFieldFormatAttribute(string fieldName, string? formatString, Type? formatterType)
		{
			this.FieldName = fieldName;
			this.FormatString = formatString;
			this.FormatterType = formatterType;
		}

		public string FieldName { get; }
		public string? FormatString { get; }
		public Type? FormatterType { get; }
	}
}
