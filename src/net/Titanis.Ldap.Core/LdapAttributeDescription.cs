using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	// [RFC 4511] § 2.5 - Attribute Descriptions
	/// <summary>
	/// Represents an LDAP attribute description.
	/// </summary>
	/// <remarks>
	/// An attribute description consists of an attribute type (OID or name) and zero or more options.
	/// </remarks>
	public class LdapAttributeDescription
	{
		public LdapAttributeDescription(string name)
			: this(name, null)
		{ }
		public LdapAttributeDescription(string name, string[]? options)
		{
			ArgumentException.ThrowIfNullOrEmpty(name);

			this.TypeName = name;
			this.Options = options ?? Array.Empty<string>();
		}
		public LdapAttributeDescription(byte[] descriptionBytes)
		{
			ArgumentNullException.ThrowIfNull(descriptionBytes);

			string typeName = Encoding.UTF8.GetString(descriptionBytes);
			int isep = typeName.IndexOf(';');
			string[] options;
			if (isep >= 0)
			{
				options = typeName.Substring(isep + 1).Split(';');
				typeName = typeName.Substring(0, isep);
			}
			else
			{
				options = Array.Empty<string>();
			}

			this.TypeName = typeName;
			this.Options = options;
		}

		/// <summary>
		/// Gets the name or OID of the attribute type.
		/// </summary>
		public string TypeName { get; }
		/// <summary>
		/// Gets the options specified for the attribute.
		/// </summary>
		public string[] Options { get; }

		private string? _text;
		public override string ToString() => (this._text ??= this.BuildText());

		private string BuildText()
		{
			return string.Join(";", [this.TypeName, .. this.Options]);
		}
	}
}
