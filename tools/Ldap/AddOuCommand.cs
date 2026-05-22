using System.ComponentModel;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

/// <task category="LDAP">Create an organizational unit (OU)</task>
[Command]
[Description("Adds a new organizational unit")]
internal class AddOuCommand : AddCommandBase
{
	protected override string RdnName => "OU";
	protected override string NewObjectClass => "organizationalUnit";

	protected override Task GetAttributesFor(LdapDistinguishedName dn, Dictionary<string, object> attributes, LdapClient ldap, CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
