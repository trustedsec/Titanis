using System.ComponentModel;
using Titanis.Cli;
using Titanis.Msrpc.Msfasp;

namespace Fasp;

public abstract class PolicyCommandBase : FirewallRpcCommand
{
	public PolicyCommandBase()
	{
		this.Store = FirewallStoreType.Local;
	}

	protected abstract FirewallPolicyAccessRights RequiredAccess { get; }

	protected abstract Task<int> RunAsync(FirewallClient client, FirewallPolicyStore store, CancellationToken cancellationToken);

	//[Parameter]
	[Description("Store to open")]
	[DefaultValue(FirewallStoreType.Local)]
	public virtual FirewallStoreType Store { get; set; }

	protected sealed override async Task<int> RunAsync(FirewallClient client, CancellationToken cancellationToken)
	{
		var store = await client.OpenPolicyStore(this.Store, this.RequiredAccess, cancellationToken);
		this.WriteVerbose($"Remote firewall endpoint supports version {store.SupportedVersion}");
		return await this.RunAsync(client, store, cancellationToken);
	}

}
