using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Titanis
{
	public class RelativeTimeConverter : DateTimeConverter
	{
		private static Regex rgxRelative = new Regex(@"^((?<t>today)|(?<n>now))\s*((?<s>\+|-)\s*(?<d>.*))?$", RegexOptions.IgnoreCase);

		public static readonly RelativeTimeConverter Instance = new RelativeTimeConverter();

		public static DateTime Parse(string text)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));
			if (DateTime.TryParse(text, out var dt))
			{
				return dt;
			}
			else
			{
				var m = rgxRelative.Match(text);
				if (m.Success)
					return FromMatch(m);

				throw new ArgumentException($"Could not parse timestamp as either a numeric value or date/time.");
			}
		}

		private static DateTime FromMatch(Match m)
		{
			DateTime dt = m.Groups["t"].Success ? DateTime.UtcNow.Date : DateTime.UtcNow;
			Group signGroup = m.Groups["s"];
			if (signGroup.Success)
			{
				var sign = signGroup.Value[0];
				var duration = Duration.Parse(m.Groups["d"].Value).TimeSpan;
				dt = sign switch
				{
					'-' => dt - duration,
					'+' => dt + duration
				};
			}
			return dt;
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string str)
			{
				var m = rgxRelative.Match(str);
				if (m.Success)
					return FromMatch(m);
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
