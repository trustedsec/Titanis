using Titanis.Cli;

namespace Titanis.Cli.Registry
{
	public sealed class RegistryItemNameFormatter : IOutputFormatter
	{
		/// <summary>
		/// Placeholder for the default value in a key.
		/// </summary>
		private const string DefaultValueName = "(Default)";

		// In order for the formatter to get called, a string type must pass a non-null formatString, otherwise the formatterType specified won't be called in OutputField.FormatValue
		public const string DefaultIfEmptyFormat = "RegistryItemNameFormatter";

		public static string? FormatValue(object? value)
		{
			if (value is string name)
			{
				return string.IsNullOrEmpty(name) ? DefaultValueName : name;
			}
			// This shouldn't happen, but if it does, format as a name.
			return (value?.ToString() ?? DefaultValueName);
		}
		/// <inheritdoc/>
		public string? FormatValue(object? value, string format, OutputField field, OutputStyle outputStyle)
			=> FormatValue(value);
	}
}

