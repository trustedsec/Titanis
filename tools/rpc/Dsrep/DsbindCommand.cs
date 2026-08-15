using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Msdrsr;

namespace Titanis.Cli.Dsrep;

public abstract class DsbindCommand : RpcCommand<DirectoryReplicationClient>
{
	[Parameter]
	[Description("Accept data compressed with Windows Server 2003 Deflate")]
	[DefaultValue(true)]
	public SwitchParam Accept2003Deflate { get; set; }

	protected abstract DsbindScenario Scenario { get; }

	protected sealed override async Task<int> RunAsync(DirectoryReplicationClient client, CancellationToken cancellationToken)
	{
		var pid = Random.Shared.Next(100, 500) * 4;
		var flags = DirectoryReplicationClient.Windows2025BindFlags;
		if (!this.Accept2003Deflate.IsSet)
			flags &= ~DrsBindFlags.W2K3Deflate;
		await using (var bind = await client.Dsbind(this.Scenario, DirectoryReplicationClient.NtdsapiClientGuid, Guid.Empty, pid, cancellationToken, flags))
		{
			return await this.RunAsync(client, bind, cancellationToken);
		}
	}

	protected abstract Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken);
}
