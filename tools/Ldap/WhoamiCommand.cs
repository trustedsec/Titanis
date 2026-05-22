using System.ComponentModel;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

/// <task category="LDAP">Get your current user name</task>
[Command]
[Description("Gets the name of the authenticated user")]
[OutputRecordType(typeof(SaslIdentity), DefaultOutputStyle = OutputStyle.Freeform)]
internal class WhoamiCommand : LdapCommandBase
{
	protected override async Task<int> RunAsync(LdapClient ldap, CancellationToken cancellationToken)
	{
		var value = await ldap.Whoami(cancellationToken);

		this.WriteRecord(value);

		return 0;
	}
}
