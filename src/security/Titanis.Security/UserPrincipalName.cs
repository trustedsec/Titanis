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
	/// Represents a user principal name.
	/// </summary>
	[TypeConverter(typeof(UserPrincipalNameConverter))]
	public sealed class UserPrincipalName : SecurityPrincipalName, IEquatable<UserPrincipalName?>
	{
		/// <summary>
		/// Initializes a new <see cref="UserPrincipalName"/>.
		/// </summary>
		/// <param name="userName">Name of user</param>
		/// <param name="realm">Name of realm</param>
		public UserPrincipalName(string userName, string? realm, string? originalText = null)
		{
			UserName = userName;
			Realm = realm;

			if (string.IsNullOrEmpty(originalText))
				originalText = string.IsNullOrEmpty(realm) ? userName : $"{userName}@{realm}";

			this.OriginalText = originalText;
		}

		public string OriginalText { get; set; }

		/// <summary>
		/// Gets the name of the user.
		/// </summary>
		public string UserName { get; }
		/// <summary>
		/// Gets the name of the realm.
		/// </summary>
		public string? Realm { get; }

		public sealed override PrincipalNameType NameType => PrincipalNameType.Enterprise;
		public sealed override string[] GetNameParts() => new string[] { this.OriginalText };
		public sealed override int NamePartCount => 1;
		public sealed override string GetNamePart(int index) => index switch
		{
			0 => this.OriginalText,
			_ => throw new ArgumentOutOfRangeException(nameof(index))
		};

		private static readonly Regex rgxUpn = new Regex(@"^((?<r>[^\\]+)\\)?((?<u>[^@]+)(@(?<r>.*))?)$");
		public static UserPrincipalName Parse(string text)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));

			var m = rgxUpn.Match(text);
			if (!m.Success)
				throw new FormatException("The UPN must be specified as <user name>@<realm>");

			var u = m.Groups["u"].Value;
			var r = m.Groups["r"].Value;
			if (r == string.Empty)
				r = null;
			return new UserPrincipalName(u, r, text);
		}

		private string? _str;

		/// <inheritdoc/>
		public sealed override string ToString() => this.OriginalText;

		public sealed override bool Equals(object? obj)
		{
			return Equals(obj as UserPrincipalName);
		}

		public bool Equals(UserPrincipalName? other)
		{
			return other is not null &&
				   UserName.Equals(other.UserName, StringComparison.OrdinalIgnoreCase) &&
				   Realm.Equals(other.Realm, StringComparison.OrdinalIgnoreCase);
		}

		public sealed override int GetHashCode()
		{
			return System.HashCode.Combine(UserName.GetHashCode(StringComparison.OrdinalIgnoreCase), Realm.GetHashCode(StringComparison.OrdinalIgnoreCase));
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
