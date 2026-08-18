using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;
using Titanis.Msrpc.Msdrsr;

namespace Titanis.Cli.Dsrep;

public class DsrepNgcKey
{
	public LdapDistinguishedName Account { get; set; }
	public HexString Key { get; set; }
}

[Command]
[Description("Gets the msDS-KeyCredentialLink on an object.")]
[OutputRecordType(typeof(DsrepNgcKey))]
public class ReadNgcKeyCommand : DsbindCommand
{
	protected override DsbindScenario Scenario => DsbindScenario.Repnc;

	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Target account DN")]
	public LdapDistinguishedName[] Account { get; set; }

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		foreach (var account in this.Account)
		{
			try
			{
				var key = await dsbind.ReadNgcKey(account, cancellationToken);
				this.WriteRecord(new DsrepNgcKey { Account = account, Key = new HexString(key) });
			}
			catch (Exception ex)
			{
				this.WriteError($"Error retrieving NGC key for {account}: {ex.Message}");
			}
		}

		return 0;
	}
}
