using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Msfasp;

namespace Fasp;

[Command]
[Description("Gets the firewall configuration")]
[OutputRecordType(typeof(FirewallConfiguration), DefaultOutputStyle = OutputStyle.List)]
public class GetConfigCommand : FirewallRpcCommand
{
	[Parameter]
	[Description("Store to open")]
	[DefaultValue(FirewallStoreType.Dynamic)]
	public virtual FirewallStoreType Store { get; set; }

	protected override async Task<int> RunAsync(FirewallClient client, CancellationToken cancellationToken)
	{
		var config = await client.GetConfiguration(this.Store, cancellationToken);
		this.WriteRecord(config);
		return 0;
	}
}
