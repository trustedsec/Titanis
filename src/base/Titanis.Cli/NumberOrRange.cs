using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Titanis.Cli
{
	[TypeConverter(typeof(NumberOrRangeConverter))]
	public struct NumberOrRange
	{
		public NumberOrRange(int value)
		{
			MinValue = value;
			MaxValue = value;
		}
		public NumberOrRange(int? min, int? max)
		{
			MinValue = min;
			MaxValue = max;
		}

		public int? MinValue { get; }
		public int? MaxValue { get; }

		public bool Contains(int value) =>
			(!this.MinValue.HasValue || value >= this.MinValue.Value)
			&& (!this.MaxValue.HasValue || value <= this.MaxValue.Value);
	}

	class NumberOrRangeConverter : TypeConverter
	{
		public sealed override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			else
				return base.CanConvertFrom(context, sourceType);
		}

		private static readonly Regex rgxRange = new Regex(@"^(?<a>(\d+|\*))?-(?<b>(\d+)|\*)?$");
		public sealed override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
			{
				if (int.TryParse(str, out var n))
					return new NumberOrRange(n);
				else
				{
					var m = rgxRange.Match(str);
					if (m.Success)
					{
						var minText = m.Groups["a"].Value;
						var maxText = m.Groups["b"].Value;

						var range = new NumberOrRange(ParseBound(minText), ParseBound(maxText));
						return range;
					}
					else
					{
						throw new FormatException($"The range was not in the correct format of <number>-<number>");
					}
				}
			}

			return base.ConvertFrom(context, culture, value);
		}

		private static int? ParseBound(string text)
		{
			return (string.IsNullOrEmpty(text) || text == "*") ? default(int?) : int.Parse(text);
		}
	}
}