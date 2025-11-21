using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;

namespace Ldap;
internal class AddOuCommand : AddCommandBase
{
	protected override string RdnName => "OU";
	protected override string ObjectClass => "organizationalUnit";

	protected override Task GetAttributesFor(LdapDistinguishedName dn, Dictionary<string, object> attributes, LdapClient ldap, CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
