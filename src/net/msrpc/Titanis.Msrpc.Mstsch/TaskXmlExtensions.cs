using System.Text;
using System.Text.RegularExpressions;

namespace Titanis.Msrpc.Mstsch
{
	static class TaskXmlExtensions
	{
		public static void SetNullable<T>(this object obj, ref T field, ref bool specified, T? value)
			where T : struct
		{
			if (value.HasValue)
			{
				specified = true;
				field = value.Value;
			}
			else
			{
				specified = false;
			}
		}
		public static T? GetNullable<T>(this object obj, T field, bool specified)
			where T : struct
		{
			if (specified)
				return field;
			else
				return null;
		}

		private static readonly Regex rgxXmlDuration = new Regex(@"^P((?<y>\d+)Y)?((?<mo>\d+)M)?((?<d>\d+)D)?T((?<h>\d+)H)?((?<mi>\d+)M)?((?<s>(\d|\.)+)S)?$");
		public static string ToXmlDuration(this TimeSpan timespan)
		{
			if (timespan.Ticks == 0)
				return "PT0S";

			StringBuilder sb = new StringBuilder("P");
			if (timespan.Days > 0) sb.Append($"{timespan.Days}D");
			if (timespan.TotalDays > timespan.Days)
			{
				sb.Append('T');
				if (timespan.Hours > 0) sb.Append($"{timespan.Hours}H");
				if (timespan.Minutes > 0) sb.Append($"{timespan.Minutes}M");
				if (timespan.Seconds > 0 || timespan.Milliseconds > 0) sb.Append($"{timespan.Seconds + (timespan.Milliseconds / 1000.0)}S");
			}
			return sb.ToString();
		}

		public static string? ToXmlDuration(this TimeSpan? timespan) => timespan.HasValue ? timespan.Value.ToXmlDuration() : null;
		public static TimeSpan? ParseXmlDurationNullable(string? text) => string.IsNullOrEmpty(text) ? null : ParseXmlDuration(text);

		private static int ParseIntOrZero(this Group group) => group.Success ? int.Parse(group.Value) : 0;
		private static double ParseFloatOrZero(this Group group) => group.Success ? double.Parse(group.Value) : 0.0;
		public static TimeSpan ParseXmlDuration(string text)
		{
			ArgumentException.ThrowIfNullOrEmpty(text);

			var m = rgxXmlDuration.Match(text);
			if (!m.Success)
				throw new ArgumentException($"The text '{text}' is not a proper XML duration.", nameof(text));

			var y = m.Groups["y"];
			var mo = m.Groups["mo"];
			var d = m.Groups["d"];
			int days = 0
				+ (y.ParseIntOrZero() * 365)
				+ (mo.ParseIntOrZero() * 30)
				+ d.ParseIntOrZero()
				;
			int ms = m.Groups["s"].ParseIntOrZero() * 1000;
			return new TimeSpan(days,
				m.Groups["h"].ParseIntOrZero(),
				m.Groups["mi"].ParseIntOrZero(),
				(ms / 1000),
				(ms % 1000)
				);
		}
	}
}
