using System.ComponentModel;
using System.Globalization;
using Titanis.Cli;

namespace Titanis.Smb2.Cli
{
	[TypeConverter(typeof(TimeWarpTokenConverter))]
	public class TimeWarpToken
	{
		public TimeWarpToken(DateTime timestamp)
		{
			this.Timestamp = timestamp;
		}

		public DateTime Timestamp { get; }

		public static TimeWarpToken Parse(string text)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));

			if (text.StartsWith("@GMT"))
			{
				var token = FileSnapshotInfo.Parse(text);
				return new TimeWarpToken(token.Timestamp);
			}
			else if (DateTime.TryParse(text, out var dt))
			{
				return new TimeWarpToken(dt);
			}
			else
				throw new ArgumentException("The text is not a valid timestamp.  It must either be a date or a @GMT token", nameof(text));
		}
	}

	class TimeWarpTokenConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (sourceType == typeof(DateTime) || sourceType == typeof(string));
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return (destinationType == typeof(TimeWarpToken));
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is null)
				throw new ArgumentNullException(nameof(value));

			if (value is DateTime dt)
			{
				return new TimeWarpToken(dt);
			}
			else if (value is string text)
			{
				return TimeWarpToken.Parse(text);
			}
			else
			{
				throw new ArgumentException($"Cannot convert object of type {value.GetType().FullName} to a {nameof(TimeWarpToken)}.");
			}
		}
	}
}