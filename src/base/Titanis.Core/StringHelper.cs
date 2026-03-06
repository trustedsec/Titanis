using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Titanis
{
	public static class StringHelper
	{
		public static int GetHashCode(this string str, StringComparison comparison)
		{
			bool ignoreCase = 0 != ((uint)comparison & 1);
			comparison = (comparison & (StringComparison)~1);
			var culture = comparison switch
			{
				StringComparison.CurrentCulture => CultureInfo.CurrentCulture,
				StringComparison.InvariantCulture or StringComparison.Ordinal => CultureInfo.InvariantCulture,
				_ => CultureInfo.CurrentCulture
			};
			return culture.CompareInfo.GetHashCode(str, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}


		enum EscapeState
		{
			Normal,
			Escaped = 1,
			Hex = 2,
			Utf16 = 4,
			Utf32 = 8,
			Octal = 3,
		}
		public static string UnescapeCStyle(this string str, bool allowBadEscapes = false)
		{
			if (str.IndexOf('\\') < 0)
				return str;

			StringBuilder sb = new StringBuilder(str.Length);
			char lastChar = '\0';
			EscapeState escapeState = EscapeState.Normal;
			uint runeValue = 0;
			int digitCount = 0;
			for (int i = 0; i <= str.Length; i++)
			{
				bool end = i == str.Length;
				var c = end ? '\0' : str[i];

				switch (escapeState)
				{
					case EscapeState.Escaped:
						if (c is 'x')
							escapeState = EscapeState.Hex;
						else if (c is 'u')
							escapeState = EscapeState.Utf16;
						else if (c is 'U')
							escapeState = EscapeState.Utf32;
						else if ((uint)(c - '0') < 8U)
						{
							runeValue = (uint)(c - '0');
							digitCount = 1;
							escapeState = EscapeState.Octal;
						}
						else
						{
							char ce = c switch
							{
								'a' => '\a',
								'b' => '\b',
								'e' => '\x1B',
								'f' => '\f',
								'n' => '\n',
								'r' => '\r',
								't' => '\t',
								'v' => '\v',
								'\\' => '\\',
								'\'' => '\'',
								'\"' => '\"',
								'?' => '?',
								_ => allowBadEscapes ? c : throw new FormatException($"The character '{c}' at position {i} is not a valid escape character.")
							};
							sb.Append(ce);
							escapeState = EscapeState.Normal;
						}
						break;
					case EscapeState.Octal:
						if ((uint)(c - '0') < 8U)
						{
							runeValue *= 8;
							runeValue += (uint)(c - '0');
							digitCount++;

							if (digitCount == 3)
							{
								sb.Append((char)runeValue);
								runeValue = 0;
								digitCount = 0;
								escapeState = EscapeState.Normal;
							}
						}
						else
						{
							// Characte isn't octal, commit the octal and reprocess

							sb.Append((char)runeValue);
							runeValue = 0;
							digitCount = 0;
							escapeState = EscapeState.Normal;

							i--;
							continue;
						}
						break;
					case EscapeState.Hex:
					case EscapeState.Utf16:
					case EscapeState.Utf32:
						{
							var n = BinaryHelper.TryParseHexChar(c);
							if (n == -1)
								throw new FormatException($"Encountered character '{c}' at position {i} where a hexadecimal digit was expected.");

							runeValue <<= 4;
							runeValue |= (uint)n;

							digitCount++;
							// The value of the escape state is also the number of digits it requires.
							if (digitCount == (int)escapeState)
							{
								if (runeValue < 0x1_0000)
									sb.Append((char)runeValue);
								else if (runeValue < 0x11_0000)
								{
									runeValue -= 0x10_0000;
									uint low = runeValue & ((1 << 10) - 1);
									runeValue >>= 10;
									sb.Append((char)(0xD800 + runeValue));
									sb.Append((char)(0xDC00 + low));
								}
								else
								{
									throw new ArgumentException($"Codepoint U+{runeValue:X} cannot be encoded.", nameof(str));
								}

								runeValue = 0;
								digitCount = 0;
								escapeState = EscapeState.Normal;
							}
						}
						break;
					default:
						if (c == '\\')
							escapeState = EscapeState.Escaped;
						else if (!end)
							sb.Append(c);
						break;
				}
			}

			return sb.ToString();
		}
	}
}
