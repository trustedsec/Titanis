using System;
using System.ComponentModel;
using System.Globalization;

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
	}

	class DurationConverter : TypeConverter
	{
		public sealed override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			return (sourceType == typeof(string));
		}
		public sealed override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
			{
				int mult = 0;
				int cchSuffix = 0;
				if (str.EndsWith("ms"))
					(mult, cchSuffix) = (1, 2);
				else if (str.EndsWith("s"))
					(mult, cchSuffix) = (1000, 1);
				else if (str.EndsWith("m"))
					(mult, cchSuffix) = (60 * 1000, 1);
				else if (str.EndsWith("h"))
					(mult, cchSuffix) = (60 * 60 * 1000, 1);

				if (mult == 0)
					throw new FormatException($"The value '{str}' does not indicate the unit of time.");

				double amount = double.Parse(str.Substring(0, str.Length - cchSuffix)) * mult;
				return new Duration(TimeSpan.FromMilliseconds(amount));
			}

			return base.ConvertFrom(context, culture, value);
		}
	}
}
