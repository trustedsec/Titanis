using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;
using Titanis.Msrpc.Msdrsr;

namespace Titanis.Cli.Dsrep;

[Command]
[Description("Gets metadata for attribute link values")]
[OutputRecordType(typeof(DsrepAttributeValueMetadata))]
public class AttributeMetadataCommand : DsbindCommand
{
	protected override DsbindScenario Scenario => DsbindScenario.Repnc;

	[Parameter(After = nameof(ServerName))]
	[Description("Domain")]
	public LdapDistinguishedName Domain { get; set; }

	[Parameter(After = nameof(Domain))]
	[Description("Attribute to retrieve")]
	[Mandatory]
	public string Attribute { get; set; }

	[Parameter(After = nameof(Attribute))]
	[Description("Value to retrieve")]
	[Mandatory]
	public string Value { get; set; }

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		var result = await dsbind.GetAttributeMetadata(this.Domain, this.Attribute, this.Value, cancellationToken);
		if (result != null)
		{
			this.WriteMessage($"Enumeration context: {result.EnumContext}");
			this.WriteRecords(result.Values);
		}
		return 0;
	}
}
