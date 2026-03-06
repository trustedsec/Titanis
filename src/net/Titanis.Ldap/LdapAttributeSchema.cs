using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	/// <summary>
	/// Describes an attribute schema.
	/// </summary>
	public class LdapAttributeSchema
	{
		internal LdapAttributeSchema(
			string ldapName,
			LdapSyntax? syntax)
		{
			LdapName = ldapName;
			Syntax = syntax;
		}

		public string LdapName { get; }
		public LdapSyntax? Syntax { get; }

		private byte[]? _encodedName;
		internal byte[] GetEncodedName() => (this._encodedName ??= Encoding.UTF8.GetBytes(this.LdapName));
	}
}
