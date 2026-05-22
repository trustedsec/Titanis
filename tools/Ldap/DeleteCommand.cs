using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

/// <task category="LDAP">Delete an object from Active Directory</task>
/// <task category="LDAP">Delete a user account</task>
/// <task category="LDAP">Delete a computer account</task>
[Command]
[Description("Deletes an object from the directory")]
public class DeleteCommand : LdapObjectCommandBase
{
	protected override async Task RunAsync(LdapClient ldap, LdapDistinguishedName objName, LdapEntry? existingEntry, CancellationToken cancellationToken)
	{
		await ldap.Delete(objName, cancellationToken).ConfigureAwait(false);
	}
}
