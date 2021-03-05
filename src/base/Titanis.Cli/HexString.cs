using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Represents an argument as a string of hex digits.
	/// </summary>
	/// <remarks>
	/// The class has a <see cref="TypeConverter"/> that can parse a string of hex digits
	/// into a byte array that makes this class suitable for command line parameter properties.
	/// </remarks>
	[TypeConverter(typeof(HexStringConverter))]
	public class HexString
	{
		public HexString(byte[] bytes, string? originalText)
		{
			if (bytes == null)
				throw new ArgumentNullException(nameof(bytes));
			this.Bytes = bytes;
			this.OriginalText = originalText;
		}

		public byte[] Bytes { get; }
		public string? OriginalText { get; }

		public static HexString Parse(string text)
		{
			if (text is null) throw new ArgumentNullException(nameof(text));

			ReadOnlySpan<char> chars = text.AsSpan();
			if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
				chars = chars.Slice(2);

			return new HexString(BinaryHelper.ParseHexString(chars), text);
		}
	}

	/// <summary>
	/// Implements a <see cref="TypeConverter"/> for <see cref="HexString"/>.
	/// </summary>
	public sealed class HexStringConverter : TypeConverter
	{
		/// <inheritdoc/>
		public sealed override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (sourceType == typeof(byte[]) || sourceType == typeof(string));
		}

		/// <inheritdoc/>
		public sealed override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return (destinationType == typeof(HexString));
		}

		/// <inheritdoc/>
		public sealed override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is null)
				throw new ArgumentNullException(nameof(value));

			if (value is byte[] bytes)
			{
				return new HexString(bytes, null);
			}
			else if (value is string text)
			{
				return HexString.Parse(text);
			}
			else
			{
				throw new ArgumentException($"Cannot convert object of type {value.GetType().FullName} to a {nameof(HexString)}.");
			}
		}
	}
}
