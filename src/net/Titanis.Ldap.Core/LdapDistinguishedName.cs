using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop.Security;

namespace Titanis.Ldap
{
	[Flags]
	public enum LdapDistinguishedNameOptions
	{
		None = 0,
		GuidAsText = 1,
		SidAsText = 2,
		WellKnownGuidAsText = 4,
	}

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
		}
		/// <summary>
		/// Initializes a new <see cref="LdapDistinguishedName"/>.
		/// </summary>
		/// <param name="rdns">RDNs of the name</param>
		public LdapDistinguishedName(ReadOnlySpan<LdapRelativeDistinguishedName> rdns)
		{
			this._rdns = rdns.ToArray();
		}

		/// <summary>
		/// Initializes a new <see cref="LdapDistinguishedName"/>.
		/// </summary>
		/// <param name="dn">DN as text</param>
		public LdapDistinguishedName(ReadOnlySpan<char> dn)
		{
			if (!TryParse(dn, out this._rdns, out var guid, out var sid, out var wkguid, out var wkDN
				, out var ttl, out var ttlDN, out var options, out var errorInd, out var ex))
				throw ex;

			this.ObjectGuid = guid;
			this.ObjectSid = sid;
			this.WellKnownGuid = wkguid;
			this.WellKnownDN = wkDN;
			this.Ttl = ttl;
			this.TtlDN = ttlDN;
			this.Options = options;
		}

		private LdapDistinguishedName(
			LdapRelativeDistinguishedName[] rdns,
			Guid? guid = default,
			SecurityIdentifier? sid = default,
			Guid? wkguid = default,
			LdapDistinguishedName? wkDN = default,
			int? ttl = default,
			LdapDistinguishedName? ttlDN = default,
			LdapDistinguishedNameOptions options = default)
		{
			this._rdns = rdns;
			this.ObjectGuid = guid;
			this.ObjectSid = sid;
			this.WellKnownGuid = wkguid;
			this.WellKnownDN = wkDN;
			this.Ttl = ttl;
			this.TtlDN = ttlDN;
			this.Options = options;
		}

		private static LdapDistinguishedName? _empty;
		public static LdapDistinguishedName Empty => (_empty ??= new LdapDistinguishedName(Array.Empty<LdapRelativeDistinguishedName>()));

		private static bool TryParseDirectoryGuid(ReadOnlySpan<char> text, out Guid guid, out int errorIndex, out LdapDistinguishedNameOptions options)
		{
			// TODO: Parse as span instead of string
			errorIndex = 0;
			if (text.IndexOf('-') > 0)
			{
				options = LdapDistinguishedNameOptions.GuidAsText;
				return Guid.TryParse(text, out guid);
			}
			else
			{
				options = LdapDistinguishedNameOptions.None;
				if (BinaryHelper.TryParseHexString(text, out var bytes, out errorIndex))
				{
					guid = new Guid(bytes);
					return true;
				}
				else
				{
					guid = default;
					return false;
				}
			}
		}

		private static bool TryParse(
			ReadOnlySpan<char> dn,
			[NotNullWhen(true)] out LdapRelativeDistinguishedName[]? parsed,
			out Guid? guid,
			out SecurityIdentifier? sid,
			out Guid? wkguid,
			out LdapDistinguishedName? wkdn,
			out int? ttl,
			out LdapDistinguishedName? ttldn,
			out LdapDistinguishedNameOptions options,
			out int errorIndex,
			out Exception? parseException)
		{
			errorIndex = -1;
			parseException = null;
			guid = null;
			sid = null;
			wkguid = null;
			wkdn = null;
			ttl = default;
			ttldn = default;
			options = LdapDistinguishedNameOptions.None;

			bool bracketed = false;
			parsed = null;

			if (dn.Length == 0)
			{
				parsed = [];
				return true;
			}
			else
			{
				int offName = 0;
				int offValue = -1;
				int escaped = 0;
				int charValue = 0;
				int offSep = -1;
				StringBuilder sb = new StringBuilder(dn.Length);
				string? attrName = null;
				List<string> values = new List<string>(1);
				List<LdapRelativeDistinguishedName> rdns = new List<LdapRelativeDistinguishedName>();
				for (int i = 0; i <= dn.Length; i++)
				{
					var c = (i < dn.Length) ? dn[i] : '\0';

					if (escaped > 0)
					{
						if (c == '\0')
						{
							errorIndex = i;
							parseException = new FormatException("The DN ended with an incomplete escape.");
							return false;
						}

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
							{
								errorIndex = i;
								parseException = new ArgumentException($"Expected second hex digit at character {i}.", nameof(dn));
								return false;
							}

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
						else if (c == ';' && sb.Length == 0)
						{
							// Probably post >, ignore it
							offName = i + 1;
						}
						else if (!bracketed && c == '<' && (sb.Length == 0))
						{
							bracketed = true;
						}
						else if (c == ',')
						{
							if (sb.Length > 0)
							{
								errorIndex = i;
								parseException = new ArgumentException($"RDN starting at {offName} isn't of the form name=value.", nameof(dn));
								return false;
							}
							else
							{
								// Allow empty RDNs; this occurs with <TTL=nn>,...
								offName = i + 1;
							}
						}
						else
							sb.Append(c);
					}
					else if (c == '+')
					{
						values.Add(sb.ToString());
						sb.Clear();
						offValue = i + 1;
					}
					else if ((!bracketed && (c is ',' or '\0')) || (bracketed && c is '>'))
					{
						// [MS-ADTS] § 3.1.1.3.1.2.4 Alternative Forms of DNs 
						if (bracketed)
						{
							var valueText = dn[offValue..i];
							if ("SID".Equals(attrName, StringComparison.OrdinalIgnoreCase))
							{
								// TODO: TryParse
								if (valueText.StartsWith("S-"))
								{
									options |= LdapDistinguishedNameOptions.SidAsText;
									sid = SecurityIdentifier.Parse(valueText);
								}
								else
								{
									if (BinaryHelper.TryParseHexString(valueText, out var bytes, out var charIndex))
									{
										sid = new SecurityIdentifier(bytes);
									}
									else
									{
										errorIndex = i;
										parseException = new FormatException($"Unable to parse SID as hex digits.");
										return false;
									}
								}
							}
							else if ("GUID".Equals(attrName, StringComparison.OrdinalIgnoreCase))
							{
								if (!TryParseDirectoryGuid(valueText, out var guid_, out _, out var guidOptions))
								{
									errorIndex = i;
									parseException = new FormatException($"Unable to parse GUID.");
									return false;
								}
								options |= guidOptions;
								guid = guid_;
							}
							else if ("WKGUID".Equals(attrName, StringComparison.OrdinalIgnoreCase))
							{
								if (!TryParseDirectoryGuid(dn[offValue..offSep], out var guid_, out _, out var guidOptions))
								{
									errorIndex = i;
									parseException = new FormatException($"Unable to parse WKGUID.");
									return false;
								}
								wkguid = guid_;
								if (guidOptions == LdapDistinguishedNameOptions.GuidAsText)
									options |= LdapDistinguishedNameOptions.WellKnownGuidAsText;

								if (!TryParse(dn[(offSep + 1)..i], out wkdn, out _, out _))
								{
									errorIndex = i;
									parseException = new FormatException($"Unable to parse WKDN.");
									return false;
								}
							}
							else if ("TTL".Equals(attrName, StringComparison.OrdinalIgnoreCase))
							{
								if (!int.TryParse(((offSep >= 0) ? dn[offValue..offSep] : valueText), out var ttl_))
								{
									errorIndex = i;
									parseException = new FormatException($"Unable to parse TTL.");
									return false;
								}
								ttl = ttl_;

								if (offSep >= 0)
								{
									if (!TryParse(dn[(offSep + 1)..i], out ttldn, out _, out _))
									{
										errorIndex = i;
										parseException = new FormatException($"Unable to parse TTL-DN.");
										return false;
									}
								}
							}
							else
							{
								parsed = null;
								errorIndex = i;
								parseException = new FormatException($"Unknown extended DN name: {attrName}");
								return false;
							}

							attrName = null;
							bracketed = false;
							offSep = -1;
						}
						else
						{
							string valueText = sb.ToString();
							sb.Clear();

							values.Add(valueText);
							rdns.Add(new LdapRelativeDistinguishedName(attrName, values.ToArray()));
							attrName = null;
							values.Clear();
						}
					}
					else
					{
						if (!bracketed)
						{
							sb.Append(c);
						}
						else
						{
							if (c == ',' && offSep < 0)
								offSep = i;
						}
					}
				}

				parsed = rdns.ToArray();
				return true;
			}
		}
		public static LdapDistinguishedName Parse(ReadOnlySpan<char> text)
		{
			if (TryParse(text, out LdapDistinguishedName dn, out int errorIndex, out var ex))
			{
				return dn;
			}
			else
				throw ex;
		}
		public static bool TryParse(ReadOnlySpan<char> text, [NotNullWhen(true)] out LdapDistinguishedName? dn, out int errorIndex, out Exception? parseException)
		{
			if (text.Length == 0)
			{
				errorIndex = -1;
				parseException = null;
				dn = Empty;
				return true;
			}
			else if (TryParse(text, out LdapRelativeDistinguishedName[] rdns, out Guid? guid, out SecurityIdentifier? sid, out Guid? wkguid, out LdapDistinguishedName? wkdn, out var ttl, out var ttlDN, out var options, out errorIndex, out parseException))
			{
				dn = new LdapDistinguishedName(rdns, guid, sid, wkguid, wkdn, ttl, ttlDN, options);
				return true;
			}
			else
			{
				dn = null;
				return false;
			}
		}

		private readonly LdapRelativeDistinguishedName[] _rdns;

		public Guid? ObjectGuid { get; }
		public SecurityIdentifier? ObjectSid { get; }
		public Guid? WellKnownGuid { get; }
		public LdapDistinguishedName? WellKnownDN { get; }
		public int? Ttl { get; }
		public LdapDistinguishedName? TtlDN { get; }
		public LdapDistinguishedNameOptions Options { get; }

		private IReadOnlyList<LdapRelativeDistinguishedName>? _rdnList;
		/// <summary>
		/// Gets the RDNs that make up the distinguished name.
		/// </summary>
		public IReadOnlyList<LdapRelativeDistinguishedName> Rdns => (this._rdnList ??= new ReadOnlyCollection<LdapRelativeDistinguishedName>(this._rdns));

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
			char sep = '\0';
			if (this.Ttl != null)
			{
				sb.Append("<TTL=")
					.Append(this.Ttl);
				if (this.TtlDN != null)
				{
					sb.Append(',')
					.Append(this.TtlDN);
				}
				sb.Append('>');
				sep = ',';
			}
			if (this.ObjectGuid.HasValue)
			{
				if (sep != '\0')
					sb.Append(sep);

				var guid = this.ObjectGuid.Value;
				sb.Append("<GUID=")
					// TODO: ToString("n") doesn't print the raw binary form
					.Append((0 != (this.Options & LdapDistinguishedNameOptions.GuidAsText)) ? guid.ToString() : guid.ToByteArray().ToHexString())
					.Append('>');
				sep = ';';
			}
			if (this.WellKnownGuid.HasValue)
			{
				if (sep != '\0')
					sb.Append(sep);

				var guid = this.WellKnownGuid.Value;
				sb.Append("<WKGUID=")
					// TODO: ToString("n") doesn't print the raw binary form
					.Append((0 != (this.Options & LdapDistinguishedNameOptions.WellKnownGuidAsText)) ? guid.ToString() : guid.ToByteArray().ToHexString())
					.Append(',')
					.Append(this.WellKnownDN)
					.Append('>');
				sep = ';';
			}
			if (this.ObjectSid != null)
			{
				if (sep != '\0')
					sb.Append(sep);

				sb.Append("<SID=")
					.Append((0 != (this.Options & LdapDistinguishedNameOptions.SidAsText)) ? this.ObjectSid.ToString() : this.ObjectSid.GetBytes().ToHexString())
					.Append('>');
				sep = ';';
			}

			if (this._rdns.Length > 0)
			{
				if (sep != '\0')
					sb.Append(sep);

				for (int i = 0; i < this._rdns.Length; i++)
				{
					var part = this._rdns[i];

					if (i > 0)
						sb.Append(',');

					part.BuildTextInto(sb);
				}
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
