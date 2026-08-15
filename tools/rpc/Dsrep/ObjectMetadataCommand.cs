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
[Description("Gets object metadata")]
[OutputRecordType(typeof(DsrepObjectMetadata))]
public class ObjectMetadataCommand : DsbindCommand
{
	protected override DsbindScenario Scenario => DsbindScenario.Repnc;

	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Object DN")]
	public LdapDistinguishedName Object { get; set; }

	[Parameter]
	[Description("Object metadata info level")]
	[DefaultValue(DsrepObjectMetadataLevel.Metadata2)]
	public DsrepObjectMetadataLevel Level { get; set; }

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		var infos = await dsbind.GetObjectMetadata(this.Object, this.Level, cancellationToken);
		this.WriteRecords(infos);
		return 0;
	}
}
