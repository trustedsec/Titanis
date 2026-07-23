using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Titanis.Security
{
	/// <summary>
	/// Represents the security principal name of a user.
	/// </summary>
	[TypeConverter(typeof(UserPrincipalNameConverter))]
	public sealed class UserPrincipalName : SecurityPrincipalName, IEquatable<UserPrincipalName?>
	{
		/// <summary>
		/// Initializes a new <see cref="UserPrincipalName"/>.
		/// </summary>
		/// <param name="userName">Name of user</param>
		/// <param name="realm">Name of realm</param>
		/// <param name="wireName">Name sent over the wire</param>
		/// <remarks>
		/// If <paramref name="wireName"/> is <see langword="null"/>, it is formed based on <paramref name="nameType"/>: if <paramref name="nameType"/> is <see cref="PrincipalNameType.Enterprise"/>, it is set to <paramref name="userName"/>@<paramref name="realm"/>, else set to <paramref name="userName"/>.
		/// </remarks>
		public UserPrincipalName(string userName, string? realm, string? wireName = null, PrincipalNameType nameType = PrincipalNameType.Principal)
		{
			if (string.IsNullOrEmpty(userName)) throw new ArgumentException($"'{nameof(userName)}' cannot be null or empty.", nameof(userName));

			this.UserName = userName;
			this.Realm = realm;

			if (string.IsNullOrEmpty(wireName))
			{
				if (nameType is PrincipalNameType.Enterprise)
					wireName = string.IsNullOrEmpty(realm) ? userName : $"{userName}@{realm}";
				else
					wireName = userName;
			}

			this.WireName = wireName!;
			this.NameType = nameType;
		}

		/// <summary>
		/// Gets the name as it is sent over the wire.
		/// </summary>
		public string WireName { get; set; }

		/// <summary>
		/// Gets the name of the user.
		/// </summary>
		public string UserName { get; }
		/// <summary>
		/// Gets the name of the realm.
		/// </summary>
		public string? Realm { get; }

		/// <summary>
		/// Gets the original text specified by the user.
		/// </summary>
		public string? OriginalText { get; set; }

		public sealed override PrincipalNameType NameType { get; }
		public sealed override string[] GetNameParts() => new string[] { this.WireName };
		public sealed override int NamePartCount => 1;
		public sealed override string GetNamePart(int index) => index switch
		{
			0 => this.WireName,
			_ => throw new ArgumentOutOfRangeException(nameof(index))
		};

		private static readonly Regex rgxUpn = new Regex(@"^(((?<d>[^\\]+)\\(?<u>.*))|((?<u>[^@]+)(@(?<r>.*))?))$");
		public static bool TryParse(string? text, out UserPrincipalName? upn)
		{
			if (!string.IsNullOrEmpty(text))
			{
				var m = rgxUpn.Match(text);
				if (m.Success)
				{
					var d = m.Groups["d"];
					var u = m.Groups["u"].Value;
					if (d.Success)
					{
						upn = new UserPrincipalName(u, d.Value, u) { OriginalText = text };
					}
					else
					{
						var r = m.Groups["r"].Value;
						if (r.Contains('.'))
							upn = new UserPrincipalName(u, r, text, PrincipalNameType.Enterprise) { OriginalText = text };
						else
						{
							if (r == string.Empty)
								r = null;

							upn = new UserPrincipalName(u, r, u) { OriginalText = text };
						}
					}
					return true;
				}
			}

			upn = null;
			return false;
		}
		public new static UserPrincipalName Parse(string text)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));

			if (TryParse(text, out var upn))
				return upn;
			else
				throw new FormatException("The UPN must be specified as <user name>@<realm>");
		}

		private string? _str;

		/// <inheritdoc/>
		public sealed override string ToString() =>
			(this.OriginalText != null) ? this.OriginalText
			: (this.NameType is PrincipalNameType.Enterprise) ? this.WireName
			: (!string.IsNullOrEmpty(this.Realm)) ? $"{this.Realm}\\{this.UserName}"
			: this.UserName;

		public sealed override bool Equals(object? obj)
		{
			return Equals(obj as UserPrincipalName);
		}

		public bool Equals(UserPrincipalName? other)
		{
			return other is not null &&
				   string.Equals(this.UserName, other.UserName, StringComparison.OrdinalIgnoreCase) &&
				   string.Equals(this.Realm, other.Realm, StringComparison.OrdinalIgnoreCase);
		}

		public sealed override int GetHashCode()
		{
			return System.HashCode.Combine(
				UserName.GetHashCode(StringComparison.OrdinalIgnoreCase),
				Realm?.GetHashCode(StringComparison.OrdinalIgnoreCase) ?? 0
				);
		}

		public UserPrincipalName WithRealm(string? realm)
		{
			return new UserPrincipalName(this.UserName, realm, this.WireName, this.NameType);
		}

		public static bool operator ==(UserPrincipalName? left, UserPrincipalName? right)
		{
			return EqualityComparer<UserPrincipalName>.Default.Equals(left, right);
		}

		public static bool operator !=(UserPrincipalName? left, UserPrincipalName? right)
		{
			return !(left == right);
		}
	}

	public sealed class UserPrincipalNameConverter : TypeConverter
	{
		public sealed override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		public sealed override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
				return UserPrincipalName.Parse(str);
			else
				return base.ConvertFrom(context, culture, value);
		}
	}
}
