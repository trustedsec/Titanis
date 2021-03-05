using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Titanis.Cli
{
	public class FileSizeFormatter : IOutputFormatter
	{
		public static FileSizeFormatter Instance { get; } = new FileSizeFormatter();

		private static readonly Regex rgx = new Regex(@"^(H|h)(?<s>\d+)?$");

		private static readonly string[] Suffixes = new string[]
		{
			"b",
			"KB",
			"MB",
			"GB",
			"TB",
			"PB",
		};

		public static string? FormatValue(object? value, string format)
		{
			Match m = (format != null) ? rgx.Match(format) : null;
			if (m != null && m.Success)
			{
				var gScale = m.Groups["s"];
				int scale = gScale.Success ? int.Parse(gScale.Value) : 0;

				if (value is IConvertible conv)
				{
					double n = conv.ToDouble(null);

					int i;
					for (i = 0; i < Suffixes.Length && (n >= 1024); i++, n /= 1024)
						;

					i = Math.Min(i, Suffixes.Length - 1);
					string fmtstr = (i == 0) ? "N0" : ("N" + scale);
					string str = n.ToString(fmtstr) + " " + Suffixes[i];
					return str;
				}
				else
					throw new ArgumentOutOfRangeException(nameof(value));
			}
			else if (value is IFormattable fmt)
			{
				return fmt.ToString(format, null);
			}
			else
			{
				return value?.ToString();
			}
		}
		public string? FormatValue(object? value, string format, OutputField field, OutputStyle outputStyle)
			=> FormatValue(value, format);
	}
}
