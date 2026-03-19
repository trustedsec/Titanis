using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Titanis
{
	/// <summary>
	/// Describes a duration.
	/// </summary>
	/// <remarks>
	/// This type has a type converter that parser a string describing the duration.
	/// The string is a number followed by a suffix indicating the unit that may be one of
	/// { ms, s, m, h }.
	/// </remarks>
	[TypeConverter(typeof(DurationConverter))]
	public class Duration
	{
		public Duration(TimeSpan timeSpan)
		{
			this.TimeSpan = timeSpan;
		}

		public TimeSpan TimeSpan { get; }

		public override string ToString()
		{
			var ts = this.TimeSpan;
			if (ts == default)
				return "0ms";

			StringBuilder sb = new StringBuilder();
			if (ts.Days > 0)
				sb.Append($"{ts.Days}d");
			if (ts.Hours > 0)
				sb.Append($"{ts.Hours}h");
			if (ts.Minutes > 0)
				sb.Append($"{ts.Minutes}m");
			if (ts.Seconds > 0)
				sb.Append($"{ts.Seconds}s");
			if (ts.Milliseconds > 0)
				sb.Append($"{ts.Milliseconds}ms");

			return sb.ToString();
		}

		private static readonly Regex rgxDuration = new Regex(@"^((?<d>\d+)d)?((?<h>\d+)h)?((?<m>\d+)m(?!s))?((?<s>\d+)s)?((?<ms>\d+)ms)?$");

		public static Duration Parse(string text)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));

			var m = rgxDuration.Match(text);
			if (!m.Success)
				throw new FormatException($"The value '{text}' does not indicate a duration.");

			static int ParseIf(Match m, string group)
			{
				var g = m.Groups[group];
				return g.Success ? int.Parse(g.Value) : 0;
			}

			int units = ParseIf(m, "d"); units *= 24;
			units += ParseIf(m, "h"); units *= 60;
			units += ParseIf(m, "m"); units *= 60;
			units += ParseIf(m, "s"); units *= 1000;
			units += ParseIf(m, "ms");

			return new Duration(TimeSpan.FromMilliseconds(units));
		}
	}

	public class DurationConverter : TypeConverter
	{

		public sealed override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			return (sourceType == typeof(string));
		}
		public sealed override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
				return Duration.Parse(str);

			return base.ConvertFrom(context, culture, value);
		}
	}
}
