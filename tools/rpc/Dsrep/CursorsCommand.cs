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
[Description("Gets replication cursor info")]
[OutputRecordType(typeof(DsrepCursor))]
public class CursorsCommand : DsbindCommand
{
	protected override DsbindScenario Scenario => DsbindScenario.Repnc;

	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Domain DN")]
	public LdapDistinguishedName Domain { get; set; }

	[Parameter]
	[Description("Cursor info level")]
	[DefaultValue(DsrepCursorLevel.Cursor3)]
	public DsrepCursorLevel Level { get; set; }

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		var infos = await dsbind.GetReplicationCursors(this.Domain, this.Level, cancellationToken);
		this.WriteRecords(infos);
		return 0;
	}
}
