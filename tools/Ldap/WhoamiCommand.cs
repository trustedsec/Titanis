using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;

namespace Ldap;

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
