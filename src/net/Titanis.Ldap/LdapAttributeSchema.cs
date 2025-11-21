using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	public class LdapAttributeSchema
	{
		internal LdapAttributeSchema(
			string schemaName,
			string ldapName,
			LdapSyntax? syntax)
		{
			SchemaName = schemaName;
			LdapName = ldapName;
			Syntax = syntax;
		}

		public string SchemaName { get; }
		public string LdapName { get; }
		public LdapSyntax? Syntax { get; }

		private byte[]? _encodedName;
		internal byte[] GetEncodedName() => (this._encodedName ??= Encoding.UTF8.GetBytes(this.LdapName));
	}
}
