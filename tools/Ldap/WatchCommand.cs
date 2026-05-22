using System.ComponentModel;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

/// <task category="LDAP">Watch for changes to Active Directory</task>
[Command]
[Description("Watches for changes to an object or subtree")]
[OutputRecordType(typeof(LdapEntry), DefaultOutputStyle = OutputStyle.List)]
internal sealed class WatchCommand : QueryCommandBase
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
