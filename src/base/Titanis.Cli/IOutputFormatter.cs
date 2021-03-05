using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Provides functionality to format a value for output.
	/// </summary>
	public interface IOutputFormatter
	{
		public string? FormatValue(object? value, string format, OutputField field, OutputStyle outputStyle);
	}
}
