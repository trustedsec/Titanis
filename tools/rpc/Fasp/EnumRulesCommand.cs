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
[Description("Enumerates firewall rules")]
[OutputRecordType(typeof(FirewallRule), DefaultOutputStyle = OutputStyle.List)]
public class EnumRulesCommand : PolicyCommandBase
{
	[Parameter]
	[Description("Status filters")]
	public FirewallRuleStatus[]? StatusFilter { get; set; }

	[Parameter]
	[Description("Profile filters")]
	public FirewallProfiles[]? ProfileFilter { get; set; }

	[Parameter]
	[Description("Rule store to enumerate")]
	public override FirewallStoreType Store { get => base.Store; set => base.Store = value; }

	protected override FirewallPolicyAccessRights RequiredAccess => FirewallPolicyAccessRights.Read;

	protected override async Task<int> RunAsync(FirewallClient client, FirewallPolicyStore store, CancellationToken cancellationToken)
	{
		var statusFilter = (this.StatusFilter == null) ? FirewallRuleStatus.All : this.StatusFilter.Aggregate(FirewallRuleStatus.None, (x, y) => x | y);
		var profileFilter = (this.ProfileFilter == null) ? FirewallProfiles.All : this.ProfileFilter.Aggregate(FirewallProfiles.None, (x, y) => x | y);
		var rules = await store.EnumRules(
			statusFilter,
			profileFilter,
			cancellationToken);
		this.WriteRecords(rules);
		return 0;
	}
}
