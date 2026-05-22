using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;
using Titanis.Ldap.FilterExpressions;

namespace Titanis.Cli.LdapTool;

/// <task category="LDAP;Enumeration">List partitions (naming contexts) within an Active Directory forest</task>
[Command]
[Description("Gets a list of partitions in the Active Directory forest")]
[OutputRecordType(typeof(LdapEntry), DefaultFields = [nameof(LdapAttributeTypes.DistinguishedName), nameof(LdapAttributeTypes.NCName), nameof(LdapAttributeTypes.NETBIOSName), nameof(LdapAttributeTypes.DnsRoot)])]
[DetailedHelpText("This command queries all crossRef objects in the CN=Partitions container within the configuration NC.")]
[Example("List all partitions","{0} LUMON-DC1 -UserName marks@LUMON -Password She's@live!! -Kdc LUMON-DC1", Tag ="lspart")]
public class ListPartitionsCommand : LdapGenericSearchCommandBase
{
	protected override async Task<int> RunAsync(LdapClient ldap, CancellationToken cancellationToken)
	{
		var partitionContainer = ldap.ConfigurationRoot.Combine(new LdapRelativeDistinguishedName("CN", "Partitions"));

		var query = new LdapQuery(partitionContainer, LdapSearchScope.SingleLevel, new FilterExpression(FilterExpression.Equal("objectClass", new LiteralAssertionValue("crossRef"))).ToFilter(), null);

		await base.BuildAndRunQuery(ldap, query, cancellationToken);

		return 0;
	}
}
