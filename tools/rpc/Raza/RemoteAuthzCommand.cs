using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msraa;
using Titanis.Winterop.Security;

namespace Titanis.Cli.Raza;

public abstract class RemoteAuthzCommand : RpcCommand<RemoteAuthorizationClient>
{
	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("SID of account to check")]
	public SecurityIdentifier[] AccountSid { get; set; }

	protected abstract Task RunAsync(RemoteAuthorizationContext context, RemoteAuthorizationClient client, CancellationToken cancellationToken);

	protected sealed override async Task<int> RunAsync(RemoteAuthorizationClient client, CancellationToken cancellationToken)
	{
		foreach (var sid in this.AccountSid)
		{
			await using (var context = await client.Initialize(sid, RemoteAuthorizationOptions.None, cancellationToken, 0))
			{
				await this.RunAsync(context, client, cancellationToken);
			}
		}

		return 0;
	}
}
