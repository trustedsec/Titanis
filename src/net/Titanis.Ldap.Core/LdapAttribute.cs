using System.Diagnostics;
using System.Text;

namespace Titanis.Ldap
{
	/// <summary>
	/// Represents a named value (or values) of an LDAP entry.
	/// </summary>
	/// <seealso cref="LdapEntry.Attributes"/>
	public class LdapAttribute
	{
		public LdapAttribute(LdapAttributeDescription attrDesc, AttributeTypeDescription attrType, object[] values)
		{
			ArgumentNullException.ThrowIfNull(attrDesc);
			ArgumentNullException.ThrowIfNull(values);

			this.AttributeTypeDescription = attrDesc;
			this.AttributeType = attrType;
			this.Values = values;
		}
		public LdapAttribute(AttributeTypeDescription attrType, object[] values)
			: this(new LdapAttributeDescription((attrType ?? throw new ArgumentNullException(nameof(attrType))).Name), attrType, values)
		{
			ArgumentNullException.ThrowIfNull(attrType);
			ArgumentNullException.ThrowIfNull(values);
		}

		/// <summary>
		/// Gets the attribute's type description.
		/// </summary>
		public LdapAttributeDescription AttributeTypeDescription { get; }
		/// <summary>
		/// Gets the name of the attribute type.
		/// </summary>
		public string TypeName => this.AttributeTypeDescription.TypeName;
		/// <summary>
		/// Gets the attribute type.
		/// </summary>
		public AttributeTypeDescription? AttributeType { get; }

		/// <summary>
		/// Gets the values of the attribute.
		/// </summary>
		public object[] Values { get; }
		/// <summary>
		/// Gets teh value of the attribute.
		/// </summary>
		/// <remarks>
		/// If the attribute has multiple values, this property returns the first value.
		/// </remarks>
		public object? Value => this.Values.FirstOrDefault();

		/// <inheritdoc/>
		/// <remarks>
		/// This method builds a string with the attribute description and values joined with a semicolon.
		/// </remarks>
		public sealed override string ToString()
			=> $"{this.AttributeTypeDescription}: {string.Join(";", this.Value)}";


		public static string ParseSpecialValue(string attrDesc, string assertionValue)
		{
			if (assertionValue.StartsWith("0x") && ulong.TryParse(assertionValue.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out var ul))
				return ul.ToString();
			else if (NamedBitGroups.GroupsByName.TryGetValue(attrDesc, out var group))
			{
				string[] parts = assertionValue.Split(',', options: StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

				ulong value = 0;
				foreach (var part in parts)
				{
					if (
						ulong.TryParse(part, out ul)
						|| group.NamedBits.TryGetValue(part, out ul)
						)
						value |= ul;
					else
						return assertionValue;
				}

				assertionValue = value.ToString();
			}

			return assertionValue;
		}
	}
}
