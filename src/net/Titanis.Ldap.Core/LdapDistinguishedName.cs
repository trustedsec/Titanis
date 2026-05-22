using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	/// <summary>
	/// Represents a relative distinguished name within LDAP.
	/// </summary>
	public sealed class LdapRelativeDistinguishedName : IEquatable<LdapRelativeDistinguishedName>, IComparable<LdapRelativeDistinguishedName>
	{
		/// <summary>
		/// Initializes a new <see cref="LdapRelativeDistinguishedName"/>.
		/// </summary>
		/// <param name="type">Type of name</param>
		/// <param name="values">Name values</param>
		public LdapRelativeDistinguishedName(string type, params string[] values)
		{
			ArgumentNullException.ThrowIfNull(type);
			ArgumentNullException.ThrowIfNull(values);
			if (values.Contains(null)) throw new ArgumentNullException(nameof(values));

			this.Type = type;
			this.Values = values;
		}
		/// <summary>
		/// Initializes a new <see cref="LdapRelativeDistinguishedName"/>.
		/// </summary>
		/// <param name="type">Type of name</param>
		/// <param name="value">Name value</param>
		public LdapRelativeDistinguishedName(string type, string value)
		{
			ArgumentNullException.ThrowIfNull(type);
			ArgumentNullException.ThrowIfNull(value);
			this.Type = type;
			this.Values = [value];
		}

		/// <summary>
		/// Gets the type of name.
		/// </summary>
		/// <remarks>
		/// This is the LDAP display name of the name attribute.
		/// </remarks>
		public string Type { get; }
		/// <summary>
		/// Gets the name values.
		/// </summary>
		public string[] Values { get; }

		private string? _text;
		/// <summary>
		/// Gets the textual representation of the name.
		/// </summary>
		public string Text => (this._text ??= this.BuildText());

		/// <inheritdoc/>
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
			'"', '+', ',', ';', '<', '>', '\\', '\0', '\n', '\r'
			];
		/// <summary>
		/// Determines whether a string contains characters that must be escaped.
		/// </summary>
		/// <param name="text">Text to check</param>
		/// <returns><see langword="true"/> if <paramref name="text"/> must be escaped; otherwise, <see langword="false"/></returns>
		public static bool MustEscape(string text)
		{
			ArgumentNullException.ThrowIfNull(text);
			bool mustEscape = (text.IndexOfAny(escapeChars) >= 0)
				|| text.StartsWith(' ')
				|| text.EndsWith(' ')
				;
			return mustEscape;
		}
		/// <summary>
		/// Applies any necessary escapement to a string.
		/// </summary>
		/// <param name="text">Text to escape</param>
		/// <returns>A string representing <paramref name="text"/> with any necessary characters escaped</returns>
		public static string Escape(string text)
		{
			ArgumentNullException.ThrowIfNull(text);

			if (!MustEscape(text))
				return text;

			StringBuilder sb = new StringBuilder(text + 1);
			EscapeInto(text, sb);

			return sb.ToString();
		}

		/// <summary>
		/// Applies any necessary escapement to a string into a <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="text">Text to escape</param>
		/// <returns><paramref name="sb"/></returns>
		public static StringBuilder EscapeInto(string text, StringBuilder sb)
		{
			ArgumentNullException.ThrowIfNull(sb);

			var endIndex = text.Length - 1;
			while ((endIndex >= 0) && (text[endIndex] == ' '))
			{
				endIndex--;
			}

			bool leading = true;
			for (int i = 0; i < text.Length; i++)
			{
				var c = text[i];
				if ((leading || i > endIndex) && c == ' ')
				{
					sb.Append(@"\ ");
				}
				else if (c == '\0')
					sb.Append(@"\00");
				else
				{
					leading = false;

					if (c is ',')
					{
						sb.Append('\\');
						sb.Append(c);
					}
					else if (Array.IndexOf(escapeChars, c) >= 0)
						sb.Append($"\\{(byte)c:X2}");
					else
						sb.Append(c);
				}
			}

			return sb;
		}

		public static bool operator ==(LdapRelativeDistinguishedName? x, LdapRelativeDistinguishedName? y) => object.ReferenceEquals(x, y) || (x is not null && x.Equals(y));
		public static bool operator !=(LdapRelativeDistinguishedName? x, LdapRelativeDistinguishedName? y) => !(x == y);
		public sealed override bool Equals(object? obj) => (obj is LdapRelativeDistinguishedName other) && this.Equals(other);
		/// <inheritdoc/>
		public bool Equals(LdapRelativeDistinguishedName? other)
		{
			if (other is null)
				return false;
			if (object.ReferenceEquals(this, other))
				return true;

			return this.Values.SequenceEqual(other.Values);
		}
		/// <inheritdoc/>
		public override int GetHashCode()
		{
			int hash = 0;
			foreach (var value in this.Values)
			{
				hash = HashCode.Combine(hash, value?.GetHashCode() ?? 0);
			}
			return hash;
		}

		/// <inheritdoc/>
		public int CompareTo(LdapRelativeDistinguishedName? other)
		{
			if (other is null)
				return 1;

			int valueCount = Math.Min(this.Values.Length, other.Values.Length);
			for (int i = 0; i < valueCount; i++)
			{
				string? x = this.Values[i];
				string? y = other.Values[i];
				int cmp = string.Compare(x, y);
				if (cmp != 0)
					return cmp;
			}

			return this.Values.Length.CompareTo(other.Values.Length);
		}
	}
	/// <summary>
	/// Represents a distinguished name within LDAP.
	/// </summary>
	[TypeConverter(typeof(LdapDistinguishedNameConverter))]
	public sealed class LdapDistinguishedName : IEquatable<LdapDistinguishedName>, IComparable<LdapDistinguishedName>
	{
		/// <summary>
		/// Initializes a new <see cref="LdapDistinguishedName"/>.
		/// </summary>
		/// <param name="rdns">RDNs of the name</param>
		public LdapDistinguishedName(IEnumerable<LdapRelativeDistinguishedName> rdns)
		{
			ArgumentNullException.ThrowIfNull(rdns);
			this._rdns = rdns.ToArray();
			this.Rdns = new ReadOnlyCollection<LdapRelativeDistinguishedName>(this._rdns);
		}
		/// <summary>
		/// Initializes a new <see cref="LdapDistinguishedName"/>.
		/// </summary>
		/// <param name="rdns">RDNs of the name</param>
		public LdapDistinguishedName(ReadOnlySpan<LdapRelativeDistinguishedName> rdns)
		{
			this._rdns = rdns.ToArray();
			this.Rdns = new ReadOnlyCollection<LdapRelativeDistinguishedName>(this._rdns);
		}

		/// <summary>
		/// Initializes a new <see cref="LdapDistinguishedName"/>.
		/// </summary>
		/// <param name="dn">DN as text</param>
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

				this._rdns = rdns.ToArray();
				this.Rdns = new ReadOnlyCollection<LdapRelativeDistinguishedName>(this._rdns);
			}

		}

		private readonly LdapRelativeDistinguishedName[] _rdns;
		/// <summary>
		/// Gets the RDNs that make up the distinguished name.
		/// </summary>
		public IReadOnlyList<LdapRelativeDistinguishedName> Rdns { get; }

		private string? _text;
		/// <summary>
		/// Gets the textual representation of the name.
		/// </summary>
		public string Text => (this._text ??= this.BuildText());
		/// <inheritdoc/>
		public sealed override string ToString() => this.Text;

		private string BuildText()
		{
			if (this._rdns.IsNullOrEmpty())
				return string.Empty;

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < this._rdns.Length; i++)
			{
				var part = this._rdns[i];

				if (i > 0)
					sb.Append(',');

				part.BuildTextInto(sb);
			}
			return sb.ToString();
		}

		/// <summary>
		/// Creates a new <see cref="LdapDistinguishedName"/> combining this one with a subordinate RDN.
		/// </summary>
		/// <param name="subordinateRdn">Subordinate RDN</param>
		/// <returns>The combined <see cref="LdapDistinguishedName"/></returns>
		public LdapDistinguishedName Combine(LdapRelativeDistinguishedName subordinateRdn)
		{
			ArgumentNullException.ThrowIfNull(subordinateRdn);

			return new LdapDistinguishedName(this._rdns.Prepend(subordinateRdn));
		}

		/// <summary>
		/// Gets the DN of the parent.
		/// </summary>
		/// <returns>A <see cref="LdapDistinguishedName"/> of the parent, or <see langword="null"/> if there is no parent.</returns>
		public LdapDistinguishedName? GetParentName()
		{
			if (this._rdns.Length > 1)
			{
				LdapDistinguishedName parentDN = new LdapDistinguishedName(this._rdns[1..].AsReadOnly());
				return parentDN;
			}
			else
				return null;
		}

		public static bool operator ==(LdapDistinguishedName? x, LdapDistinguishedName? y) => object.ReferenceEquals(x, y) || (x is not null && x.Equals(y));
		public static bool operator !=(LdapDistinguishedName? x, LdapDistinguishedName? y) => !(x == y);
		/// <inheritdoc/>
		public sealed override bool Equals(object? obj) => (obj is LdapDistinguishedName other) && this.Equals(other);
		/// <inheritdoc/>
		public bool Equals(LdapDistinguishedName? other)
		{
			if (other is null)
				return false;
			if (object.ReferenceEquals(this, other))
				return true;

			if (this._rdns.Length != other._rdns.Length)
				return false;

			for (int i = 0; i < _rdns.Length; i++)
			{
				LdapRelativeDistinguishedName? rdn = this._rdns[i];
				LdapRelativeDistinguishedName? otherRdn = other._rdns[i];

				if (rdn != otherRdn)
					return false;
			}

			return true;
		}
		/// <inheritdoc/>
		public override int GetHashCode()
		{
			int hash = 0;
			foreach (var rdn in this._rdns)
			{
				hash = HashCode.Combine(hash, rdn.GetHashCode());
			}
			return hash;
		}

		/// <inheritdoc/>
		public int CompareTo(LdapDistinguishedName? other)
		{
			if (other is null)
				return 1;

			var rdnCount = Math.Min(this._rdns.Length, other._rdns.Length);
			for (int i = 1; i <= rdnCount; i++)
			{
				var x = this._rdns[^i];
				var y = other._rdns[^i];

				int cmp = x.CompareTo(y);
				if (cmp != 0)
					return cmp;
			}

			return this._rdns.Length.CompareTo(other._rdns.Length);
		}
	}

	/// <summary>
	/// Implements a type converter to convert between <see cref="string"/> and <see cref="LdapDistinguishedName"/>.
	/// </summary>
	/// <remarks>
	/// This converter recognizes a few placeholder values:
	/// <list type="table">
	/// <listheader><term>Placeholder</term><description>Member</description></listheader>
	/// <item><term>DomainRoot</term><description><see cref="DomainRoot"/></description></item>
	/// <item><term>ForestRoot</term><description><see cref="ForestRoot"/></description></item>
	/// <item><term>SchemaRoot</term><description><see cref="SchemaRoot"/></description></item>
	/// <item><term>ConfigRoot</term><description><see cref="ConfigRoot"/></description></item>
	/// </list>
	/// </remarks>
	public class LdapDistinguishedNameConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			return (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>
		/// Default domain naming context
		/// </summary>
		public static readonly LdapDistinguishedName DomainRoot = new LdapDistinguishedName("CN=DomainRoot");
		/// <summary>
		/// Forest root naming context
		/// </summary>
		public static readonly LdapDistinguishedName ForestRoot = new LdapDistinguishedName("CN=ForestRoot");
		/// <summary>
		/// Schema naming context
		/// </summary>
		public static readonly LdapDistinguishedName SchemaRoot = new LdapDistinguishedName("CN=SchemaRoot");
		/// <summary>
		/// Configuration naming context
		/// </summary>
		public static readonly LdapDistinguishedName ConfigRoot = new LdapDistinguishedName("CN=ConfigRoot");
		/// <summary>
		/// Root directory service entry
		/// </summary>
		public static readonly LdapDistinguishedName RootDse = new LdapDistinguishedName("");

		/// <inheritdoc/>
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
				else if (str.Equals("RootDse", StringComparison.OrdinalIgnoreCase))
					return RootDse;

				return new LdapDistinguishedName(str);
			}
			else
				return base.ConvertFrom(context, culture, value);
		}
	}
}
