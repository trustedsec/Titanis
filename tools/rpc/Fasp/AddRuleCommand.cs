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
[Description("Adds a firewall rule")]
public class AddRuleCommand : PolicyCommandBase
{
	protected override FirewallPolicyAccessRights RequiredAccess => FirewallPolicyAccessRights.ReadWrite;

	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Rule name")]
	public string RuleName { get; set; }

	[Parameter(After = nameof(RuleName))]
	[Mandatory]
	[Description("Action performed by rule")]
	public FirewallRuleAction Action { get; set; }

	[Parameter(After = nameof(Action))]
	[Mandatory]
	[Description("Traffic direction")]
	public FirewallRuleDirection Direction { get; set; }

	[Parameter(After = nameof(Direction))]
	[Mandatory]
	[Description("Protocol rule applies to")]
	public IpProtocolNumber Protocol { get; set; }

	[Parameter]
	[Description("Rule ID")]
	public string? RuleId { get; set; }

	[Parameter]
	[Description("Rule description")]
	public string? Description { get; set; }

	[Parameter]
	[Description("Firewall profiles rule applies to")]
	public FirewallProfiles[]? Profiles { get; set; }

	[Parameter]
	[Description("Local ports")]
	public NumberOrRange[]? LocalPorts { get; set; }

	[Parameter]
	[Description("Remote ports")]
	public NumberOrRange[]? RemotePorts { get; set; }

	[Parameter]
	[Description("Interface ID(s)")]
	public Guid[]? InterfaceId { get; set; }

	[Parameter]
	[Description("Interface types")]
	public InterfaceTypes[]? InterfaceType { get; set; }

	[Parameter]
	[Description("Rule store to add to")]
	public override FirewallStoreType Store { get => base.Store; set => base.Store = value; }

	private static PortRange[] MakePortRanges(NumberOrRange[]? ranges) => (ranges == null) ? null
		: Array.ConvertAll(ranges, r => new PortRange((ushort)r.MinValue, (ushort)r.MaxValue));

	protected override async Task<int> RunAsync(FirewallClient client, FirewallPolicyStore store, CancellationToken cancellationToken)
	{
		var ruleId = this.RuleId ?? $"{this.RuleName.Replace(' ', '-')}-{this.Direction}-{this.Protocol}-Active";
		var profiles = (this.Profiles == null) ? FirewallProfiles.All : this.Profiles.Aggregate(FirewallProfiles.None, (x, y) => x | y);
		var itfTypes = (this.InterfaceType == null) ? InterfaceTypes.All : this.InterfaceType.Aggregate(InterfaceTypes.All, (x, y) => x | y);
		await store.AddTcpUdpRule(
			ruleId,
			this.RuleName,
			this.Direction,
			this.Action,
			this.Protocol,
			cancellationToken,
			this.Description,
			profiles,
			MakePortRanges(this.LocalPorts),
				MakePortRanges(this.RemotePorts),
			this.InterfaceId,
			itfTypes
			);

		this.WriteMessage($"Create rule with ID '{ruleId}'");
		return 0;
	}
}
