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
[Description("Modifies a firewall rule")]
public class SetRuleCommand : PolicyCommandBase
{
	protected override FirewallPolicyAccessRights RequiredAccess => FirewallPolicyAccessRights.ReadWrite;

	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Rule ID")]
	public string RuleId { get; set; }

	[Parameter]
	[Description("Rule name")]
	public string SetRuleName { get; set; }

	[Parameter]
	[Description("Action performed by rule")]
	public FirewallRuleAction? SetAction { get; set; }

	[Parameter]
	[Description("Traffic direction")]
	public FirewallRuleDirection? SetDirection { get; set; }

	[Parameter]
	[Description("Protocol rule applies to")]
	public IpProtocolNumber? SetProtocol { get; set; }

	[Parameter]
	[Description("Rule description")]
	public string? SetDescription { get; set; }

	[Parameter]
	[Description("Firewall profiles rule applies to")]
	public FirewallProfiles[]? SetProfiles { get; set; }

	[Parameter]
	[Description("Local ports")]
	public NumberOrRange[]? SetLocalPorts { get; set; }

	[Parameter]
	[Description("Remote ports")]
	public NumberOrRange[]? SetRemotePorts { get; set; }

	[Parameter]
	[Description("Interface ID(s)")]
	public Guid[]? SetInterfaceId { get; set; }

	[Parameter]
	[Description("Interface types")]
	public InterfaceTypes[]? SetInterfaceType { get; set; }

	private static PortRange[]? MakePortRanges(NumberOrRange[]? ranges) => (ranges == null) ? null
		: Array.ConvertAll(ranges, r => new PortRange((ushort)r.MinValue, (ushort)r.MaxValue));

	protected override async Task<int> RunAsync(FirewallClient client, FirewallPolicyStore store, CancellationToken cancellationToken)
	{
		await using (var queryStore = await client.OpenPolicyStore(FirewallStoreType.Dynamic, FirewallPolicyAccessRights.Read, cancellationToken))
		{


			var ruleId = this.RuleId ?? $"{this.SetRuleName.Replace(' ', '-')}-{this.SetDirection}-Active";
			FirewallProfiles? profiles = (this.SetProfiles != null) ? this.SetProfiles.Aggregate(FirewallProfiles.None, (x, y) => x | y) : null;
			InterfaceTypes? itfTypes = (this.SetInterfaceType != null) ? this.SetInterfaceType.Aggregate(InterfaceTypes.All, (x, y) => x | y) : null;
			//await queryStore.SetTcpUdpRule(
			//	ruleId,
			//	cancellationToken,
			//	this.SetRuleName,
			//	this.SetDirection,
			//	this.SetAction,
			//	this.SetProtocol,
			//	this.SetDescription,
			//	profiles,
			//	MakePortRanges(this.SetLocalPorts),
			//		MakePortRanges(this.SetRemotePorts),
			//	this.SetInterfaceId,
			//	itfTypes
			//	);
		}

		//this.WriteMessage($"Create rule with ID '{ruleId}'");
		return 0;
	}
}
