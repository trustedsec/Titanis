using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;

namespace Ldap;
internal abstract class AddCommandBase : LdapObjectCommandBase
{
	protected abstract string ObjectClass { get; }

	protected abstract Task GetAttributesFor(LdapDistinguishedName dn, Dictionary<string, object> attributes, LdapClient ldap, CancellationToken cancellationToken);

	protected override async Task RunAsync(LdapClient ldap, LdapDistinguishedName objName, CancellationToken cancellationToken)
	{
		var attributes = new Dictionary<string, object>();
		attributes.Add("objectClass", this.ObjectClass);
		await this.GetAttributesFor(objName, attributes, ldap, cancellationToken);
		await ldap.Add(objName, attributes, cancellationToken);
	}
}
