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
[Description("Watches for changes to an object or subtree")]
[OutputRecordType(typeof(LdapEntry), DefaultOutputStyle = OutputStyle.List)]
internal sealed class WatchCommand : LdapSearchCommandBase
{
	protected override void ValidateParameters(ParameterValidationContext context)
	{
		if (this.ConsoleOutputStyle is OutputStyle.Table)
			this.WriteWarning($"Output style `Table` will not write any results until the end; to view changes as they occur, use List (the default).");
		base.ValidateParameters(context);
	}
	protected sealed override LdapQuery CreateQuery(LdapDistinguishedName searchBase)
	{
		var query = new LdapQuery(searchBase, this.Scope ?? LdapSearchScope.WholeSubtree, FilterFactory.Any(), [])
		{
			WatchForChanges = true
		};
		return query;
	}
}
