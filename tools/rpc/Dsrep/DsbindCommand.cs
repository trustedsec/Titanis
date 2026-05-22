using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Msdrsr;

namespace Dsrep;

public abstract class DsbindCommand : RpcCommand<DirectoryReplicationClient>
{
	protected sealed override async Task<int> RunAsync(DirectoryReplicationClient client, CancellationToken cancellationToken)
	{
		await using (var bind = await client.Dsbind(cancellationToken))
		{
			return await this.RunAsync(client, bind, cancellationToken);
		}
	}

	protected abstract Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken);
}
