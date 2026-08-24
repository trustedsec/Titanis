using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Msfasp;

namespace Fasp;

[Command]
[Description("Queries firewall rules")]
[OutputRecordType(typeof(FirewallRule), DefaultOutputStyle = OutputStyle.List)]
public class QueryRulesCommand : PolicyCommandBase
{
	public QueryRulesCommand()
	{
		this.Store = FirewallStoreType.Dynamic;
	}

	[Parameter]
	[Description("Match type")]
	[DefaultValue(QueryMatchType.Equal)]
	public QueryMatchType MatchType { get; set; }

	[Parameter]
	[Description("Rule ID to query for")]
	public string? ByRuleId { get; set; }

	[Parameter]
	[Description("Rule status to query for")]
	public FirewallRuleStatus[]? ByStatus { get; set; }

	[Parameter]
	[Description("Rule profile to query for")]
	public FirewallProfiles[]? ByProfile { get; set; }

	[Parameter]
	[Description("Group to query for")]
	public string? ByGroup { get; set; }

	[Parameter]
	[Description("App path to query for")]
	public string? ByAppPath { get; set; }

	[Parameter]
	[Description("Service to query for")]
	public string? ByService { get; set; }

	[Parameter]
	[Description("Protocol to query for")]
	public IpProtocolNumber? ByProtocol { get; set; }

	[Parameter]
	[Description("Local port to query for")]
	public ushort? ByLocalPort { get; set; }

	[Parameter]
	[Description("Remote port to query for")]
	public ushort? ByRemotePort { get; set; }

	[Parameter]
	[Description("Direction to query for")]
	public FirewallRuleDirection? ByDirection { get; set; }


	[DefaultValue(FirewallStoreType.Dynamic)]
	public override FirewallStoreType Store { get => base.Store; set => base.Store = value; }

	protected override FirewallPolicyAccessRights RequiredAccess => FirewallPolicyAccessRights.Read;

	protected override async Task<int> RunAsync(FirewallClient client, FirewallPolicyStore store, CancellationToken cancellationToken)
	{
		QueryMatchType matchType = this.MatchType;

		List<QueryCondition> conds = new List<QueryCondition>();
		if (this.ByRuleId != null)
			conds.Add(QueryCondition.RuleId(matchType, this.ByRuleId));
		if (this.ByStatus != null)
			conds.Add(QueryCondition.Status(matchType, this.ByStatus.Aggregate((FirewallRuleStatus)0, (x, y) => x | y)));
		if (this.ByProfile != null)
			conds.Add(QueryCondition.Profile(matchType, this.ByProfile.Aggregate((FirewallProfiles)0, (x, y) => x | y)));
		if (this.ByAppPath != null)
			conds.Add(QueryCondition.AppPath(matchType, this.ByAppPath));
		if (this.ByService != null)
			conds.Add(QueryCondition.Service(matchType, this.ByService));
		if (this.ByProtocol != null)
			conds.Add(QueryCondition.Protocol(matchType, this.ByProtocol.Value));
		if (this.ByLocalPort != null)
			conds.Add(QueryCondition.LocalPort(matchType, this.ByLocalPort.Value));
		if (this.ByRemotePort != null)
			conds.Add(QueryCondition.RemotePort(matchType, this.ByRemotePort.Value));
		if (this.ByDirection != null)
			conds.Add(QueryCondition.Direction(matchType, this.ByDirection.Value));

		var query = new Query([new QueryConditionGroup(conds.ToArray())]);
		var rules = await store.QueryRules(
			query,
			cancellationToken);
		this.WriteRecords(rules);
		return 0;
	}
}
