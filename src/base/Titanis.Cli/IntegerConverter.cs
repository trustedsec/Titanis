using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Type converter for integer values.
	/// </summary>
	/// <typeparam name="TResult">Type of integer</typeparam>
	/// <remarks>
	/// This implementation enhances the built in <see cref="Int32Converter"/> et al.
	/// by parsing strings prefixed with <c>0x</c> for hex or <c>0b</c> for binary.
	/// </remarks>
	internal sealed class IntegerConverter<TResult> : TypeConverter
		where TResult : struct
	{
		private readonly TypeConverter _baseConv;
		private readonly Func<string, NumberStyles, TResult> _parseFunc;
		private readonly int _maxBits;

		public IntegerConverter(
			TypeConverter baseConverter,
			Func<string, NumberStyles, TResult> parseFunc,
			int maxBits
			)
		{
			this._baseConv = baseConverter;
			this._parseFunc = parseFunc;
			this._maxBits = maxBits;
		}

		public sealed override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			=> this._baseConv.CanConvertFrom(context, sourceType);
		public sealed override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			=> this._baseConv.CanConvertTo(context, destinationType);
		public sealed override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string str)
			{
				NumberStyles styles;
				if (str.StartsWith("0b"))
				{
					str = str.Substring(2);

					ulong uval = 0;
					int bits = 0;
					foreach (var c in str)
					{
						if (c == '_')
						{
							// Do nothing
						}
						else if (c is '0' or '1')
						{
							if (bits >= this._maxBits)
								throw new FormatException($"The value '{str}' has too many digits.");

							uval <<= 1;
							if (c == '1')
								uval |= 1;

							bits++;
						}
					}

					return Convert.ChangeType(uval, Type.GetTypeCode(typeof(TResult)));
					// UNDONE: The type converter will not even attempt narrowing
					//return this._baseConv.ConvertFrom(uval);
				}
				else if (str.StartsWith("0x"))
				{
					str = str.Substring(2);
					styles = NumberStyles.HexNumber;
				}
				else
					styles = NumberStyles.AllowLeadingSign;

				return this._parseFunc(str, styles);
			}
			else
				return this._baseConv.ConvertFrom(value);
		}

		public sealed override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			=> this._baseConv.ConvertTo(context, culture, value, destinationType);
		public sealed override bool IsValid(ITypeDescriptorContext context, object value)
			=> this._baseConv.IsValid(context, value);
	}
}
