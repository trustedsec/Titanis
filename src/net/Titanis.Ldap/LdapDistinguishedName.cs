using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	public sealed class LdapRelativeDistinguishedName
	{
		public LdapRelativeDistinguishedName(string type, params string[] values)
		{
			ArgumentException.ThrowIfNullOrEmpty(type);
			ArgumentNullException.ThrowIfNull(values);
			this.Type = type;
			this.Values = values;
		}

		public string Type { get; }
		public string[] Values { get; }

		private string? _text;
		public string Text => (this._text ??= this.BuildText());
		public sealed override string ToString() => this.Text;

		private string BuildText()
		{
			StringBuilder sb = new StringBuilder();
			return BuildTextInto(sb);
		}

		internal string BuildTextInto(StringBuilder sb)
		{
			sb.Append(this.Type).Append('=');
			for (int i = 0; i < Values.Length; i++)
			{
				if (i > 0)
					sb.Append('+');

				string? value = this.Values[i];
				EscapeInto(value, sb);
			}

			return sb.ToString();
		}

		private static readonly char[] escapeChars = [
			'"', '+', ',', ';', '<', '>', '\\', '\0'
			];
		public static bool MustEscape(string value)
		{
			ArgumentNullException.ThrowIfNull(value);
			bool mustEscape = (value.IndexOfAny(escapeChars) >= 0)
				|| value.StartsWith(' ')
				|| value.EndsWith(' ')
				;
			return mustEscape;
		}
		public static string Escape(string value)
		{
			ArgumentNullException.ThrowIfNull(value);

			if (!MustEscape(value))
				return value;

			StringBuilder sb = new StringBuilder(value + 1);
			EscapeInto(value, sb);

			return sb.ToString();
		}

		private static void EscapeInto(string value, StringBuilder sb)
		{
			var endIndex = value.Length - 1;
			while ((endIndex >= 0) && (value[endIndex] == ' '))
			{
				endIndex--;
			}

			bool leading = true;
			for (int i = 0; i < value.Length; i++)
			{
				var c = value[i];
				if ((leading || i > endIndex) && c == ' ')
				{
					sb.Append(@"\ ");
				}
				else if (c == '\0')
					sb.Append(@"\00");
				else
				{
					leading = false;

					if (Array.IndexOf(escapeChars, c) >= 0)
						sb.Append('\\');
					sb.Append(c);
				}
			}
		}
	}
	/// <summary>
	/// Represents a distinguished name within LDAP.
	/// </summary>
	[TypeConverter(typeof(LdapDistinguishedNameConverter))]
	public sealed class LdapDistinguishedName
	{
		public LdapDistinguishedName(IEnumerable<LdapRelativeDistinguishedName> rdns)
		{
			ArgumentNullException.ThrowIfNull(rdns);
			this.Rdns = rdns.ToArray();
		}
		public LdapDistinguishedName(ReadOnlySpan<LdapRelativeDistinguishedName> rdns)
		{
			this.Rdns = rdns.ToArray();
		}
		public LdapDistinguishedName(ReadOnlySpan<char> dn)
		{
			if (dn.Length == 0)
			{
				this.Rdns = Array.Empty<LdapRelativeDistinguishedName>();
			}
			else
			{
				int offStart = 0;
				int offValue = -1;
				int escaped = 0;
				int charValue = 0;
				StringBuilder sb = new StringBuilder(dn.Length);
				string? attrName = null;
				List<string> values = new List<string>(1);
				List<LdapRelativeDistinguishedName> rdns = new List<LdapRelativeDistinguishedName>();
				for (int i = 0; i < dn.Length; i++)
				{
					var c = dn[i];

					if (escaped > 0)
					{
						if (escaped == 1)
						{
							if (char.IsAsciiHexDigit(c))
							{
								charValue = BinaryHelper.ParseHexChar(c);
								charValue <<= 4;
								escaped = 2;
							}
							else
							{
								sb.Append(c);
								escaped = 0;
							}
						}
						else if (escaped == 2)
						{
							// Must be second hex digit
							if (!char.IsAsciiHexDigit(c))
								throw new ArgumentException($"Expected second hex digit at character {i}.", nameof(dn));

							charValue |= BinaryHelper.ParseHexChar(c);
							sb.Append((char)charValue);
							charValue = 0;
							escaped = 0;
						}
					}
					else if (c == '\\')
					{
						escaped = 1;
					}
					else if (attrName is null)
					{
						if (c == '=')
						{
							attrName = sb.ToString();
							sb.Clear();
							offValue = i + 1;
						}
						else if (c == ',')
							throw new ArgumentException($"RDN starting at {offStart} isn't of the form name=value.", nameof(dn));
						else
							sb.Append(c);
					}
					else if (c == '+')
					{
						values.Add(sb.ToString());
						sb.Clear();
						offValue = i + 1;
					}
					else if (c == ',')
					{
						values.Add(sb.ToString());
						sb.Clear();
						rdns.Add(new LdapRelativeDistinguishedName(attrName, values.ToArray()));
						attrName = null;
						values.Clear();
					}
					else
						sb.Append(c);
				}

				// Add final
				{
					values.Add(sb.ToString());
					sb.Clear();
					rdns.Add(new LdapRelativeDistinguishedName(attrName, values.ToArray()));
					values.Clear();
				}

				this.Rdns = rdns.ToArray();
			}
		}

		public LdapRelativeDistinguishedName[] Rdns { get; }

		private string? _text;
		public string Text => (this._text ??= this.BuildText());
		public sealed override string ToString() => this.Text;

		private string BuildText()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < this.Rdns.Length; i++)
			{
				var part = this.Rdns[i];

				if (i > 0)
					sb.Append(',');

				part.BuildTextInto(sb);
			}
			return sb.ToString();
		}

		public LdapDistinguishedName Combine(LdapRelativeDistinguishedName subordinateRdn)
		{
			ArgumentNullException.ThrowIfNull(subordinateRdn);

			return new LdapDistinguishedName(this.Rdns.Prepend(subordinateRdn));
		}
	}

	public class LdapDistinguishedNameConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			return (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
		}

		public static readonly LdapDistinguishedName DomainRoot = new LdapDistinguishedName("CN=DomainRoot");
		public static readonly LdapDistinguishedName ForestRoot = new LdapDistinguishedName("CN=ForestRoot");
		public static readonly LdapDistinguishedName SchemaRoot = new LdapDistinguishedName("CN=SchemaRoot");
		public static readonly LdapDistinguishedName ConfigRoot = new LdapDistinguishedName("CN=ConfigRoot");
		public static readonly LdapDistinguishedName RootDse = new LdapDistinguishedName("");

		public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
			{
				if (str.Equals("DomainRoot", StringComparison.OrdinalIgnoreCase))
					return DomainRoot;
				else if (str.Equals("ForestRoot", StringComparison.OrdinalIgnoreCase))
					return DomainRoot;
				else if (str.Equals("SchemaRoot", StringComparison.OrdinalIgnoreCase))
					return SchemaRoot;
				else if (str.Equals("ConfigRoot", StringComparison.OrdinalIgnoreCase))
					return ConfigRoot;

				return new LdapDistinguishedName(str);
			}
			else
				return base.ConvertFrom(context, culture, value);
		}
	}
}
