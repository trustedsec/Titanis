using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Titanis.Security
{
	/// <summary>
	/// Represents a security principal.
	/// </summary>
	public abstract class SecurityPrincipal
	{
	}
	/// <summary>
	/// Represents a name within a security context.
	/// </summary>
	[TypeConverter(typeof(SecurityPrincipalNameConverter))]
	public abstract class SecurityPrincipalName : IEquatable<SecurityPrincipalName?>
	{
		/// <summary>
		/// Gets a <see cref="PrincipalNameType"/> indicating the type of name.
		/// </summary>
		public abstract PrincipalNameType NameType { get; }

		public static SecurityPrincipalName Create(PrincipalNameType nameType, string[] nameParts)
		{
			if (nameParts is null || nameParts.Length is 0) throw new ArgumentNullException(nameof(nameParts));

			if (nameType is PrincipalNameType.ServiceInstance && nameParts.Length is 2)
			{
				return new ServicePrincipalName(nameParts[0], nameParts[1]);
			}
			else if (nameType is PrincipalNameType.Principal && nameParts.Length is 1)
			{
				return new SimplePrincipalName(nameParts[0]);
			}
			else
			{
				return new GenericPrincipalName(nameType, nameParts);
			}
		}

		/// <summary>
		/// Gets the parts of the name.
		/// </summary>
		public abstract string[] GetNameParts();
		/// <summary>
		/// Gets the number of parts in the name.
		/// </summary>
		public abstract int NamePartCount { get; }
		/// <summary>
		/// Gets an individual part of the name.
		/// </summary>
		/// <param name="index">Index of part</param>
		/// <returns>The requested part of the name</returns>
		public abstract string GetNamePart(int index);

		/// <inheritdoc/>
		public override bool Equals(object? obj)
		{
			return Equals(obj as SecurityPrincipalName);
		}

		/// <inheritdoc/>
		public bool Equals(SecurityPrincipalName? other)
		{
			return other is not null &&
				   NameType == other.NameType &&
				   NamePartCount == other.NamePartCount;
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			int hash = 0;
			var partCount = this.NamePartCount;
			for (int i = 0; i < partCount; i++)
			{
				var part = this.GetNamePart(i);
				hash = HashCode.Combine(hash, part?.ToUpper());
			}
			return hash;
		}

		public static bool operator ==(SecurityPrincipalName? left, SecurityPrincipalName? right)
		{
			if (object.ReferenceEquals(left, right))
				return true;
			if (left is null || right is null)
				return false;

			// Ignore NameType, since Kerberos declares names are treated as equal if their parts are equal

			var leftCount = left.NamePartCount;
			var rightCount = right.NamePartCount;
			if (leftCount != rightCount) return false;
			for (int i = 0; i < leftCount; i++)
			{
				var leftPart = left.GetNamePart(i);
				var rightPart = right.GetNamePart(i);
				if (!string.Equals(leftPart, rightPart, StringComparison.OrdinalIgnoreCase))
					return false;
			}
			return true;
		}

		public static bool operator !=(SecurityPrincipalName? left, SecurityPrincipalName? right)
		{
			return !(left == right);
		}
	}

	/// <summary>
	/// Implements a <see cref="TypeConverter"/> for <see cref="SecurityPrincipalName"/>.
	/// </summary>
	public class SecurityPrincipalNameConverter : TypeConverter
	{
		/// <inheritdoc/>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			else
				return base.CanConvertFrom(context, sourceType);
		}

		/// <inheritdoc/>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string str)
			{
				if (ServicePrincipalName.TryParse(str, out var spn))
					return spn;
				else
					return new SimplePrincipalName(str);
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
