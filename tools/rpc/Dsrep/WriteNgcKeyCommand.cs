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
[Description("Updates msDS-KeyCredentialLink on an object.")]
public class WriteNgcKeyCommand : DsbindCommand
{
	protected override DsbindScenario Scenario => DsbindScenario.Repnc;

	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Target account DN")]
	public LdapDistinguishedName Account { get; set; }

	[Parameter(After = nameof(Account))]
	[Mandatory]
	[Description("Key, as a hex string")]
	public HexString KeyBytes { get; set; }

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		await dsbind.WriteNgcKey(this.Account, this.KeyBytes.Bytes, cancellationToken);
		return 0;
	}
}
