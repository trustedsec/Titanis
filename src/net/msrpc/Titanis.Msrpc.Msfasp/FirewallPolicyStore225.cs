using Fasp;
using MS_FASP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.Winterop;

namespace Titanis.Msrpc.Msfasp
{
	internal class FirewallPolicyStore225 : FirewallPolicyStore224
	{
		internal FirewallPolicyStore225(in FirewallPolicyStoreParams args) : base(args)
		{
		}

		internal override SchemaVersion NegotiateVersion => SchemaVersion.Version2_25;

		public override async Task<FirewallRule[]> EnumRules(
			FirewallRuleStatus statusFilter,
			FirewallProfiles profileFilter,
			CancellationToken cancellationToken
			)
		{
			RpcPointer<uint> pdwNumRules = new();
			RpcPointer<RpcPointer<FW_RULE2_25>> ppRules = new();
			var res = (Win32ErrorCode)await owner.ClientProxy.RRPC_FWEnumFirewallRules2_25(
				this.handle.value,
				(uint)statusFilter,
				(uint)profileFilter,
				(ushort)(FW_ENUM_RULES_FLAGS.FW_ENUM_RULES_FLAG_RESOLVE_APPLICATION | FW_ENUM_RULES_FLAGS.FW_ENUM_RULES_FLAG_RESOLVE_DESCRIPTION | FW_ENUM_RULES_FLAGS.FW_ENUM_RULES_FLAG_RESOLVE_NAME | FW_ENUM_RULES_FLAGS.FW_ENUM_RULES_FLAG_RESOLVE_KEYWORD | FW_ENUM_RULES_FLAGS.FW_ENUM_RULES_FLAG_INCLUDE_METADATA
				),
				pdwNumRules,
				ppRules,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			List<FirewallRule> rules = new List<FirewallRule>((int)pdwNumRules.value);
			var pRule = ppRules.value;
			while (pRule != null)
			{
				rules.Add(pRule.value);
				pRule = pRule.value.pNext;
			}

			return rules.ToArray();
		}

		public override async Task AddTcpUdpRule(
			string ruleId,
			string name,
			FirewallRuleDirection direction,
			FirewallRuleAction action,
			IpProtocolNumber protocol,
			CancellationToken cancellationToken,
			string? description = null,
			FirewallProfiles profiles = FirewallProfiles.All,
			PortRange[]? localPortRanges = null,
			PortRange[]? remotePortRanges = null,
			Guid[]? interfaceIds = null,
			InterfaceTypes interfaceTypes = InterfaceTypes.All,
			string? localApplication = null,
			string? localService = null,
			FirewallRuleFlags flags = FirewallRuleFlags.Active,
			string? embeddedContext = null
			)
		{
			RpcPointer<FW_RULE_STATUS> pStatus = new();
			var res = (Win32ErrorCode)await owner.ClientProxy.RRPC_FWAddFirewallRule2_25(
				this.handle.value,
				new FW_RULE2_25
				{
					wSchemaVersion = (ushort)SchemaVersion.Version2_25,
					wszRuleId = ruleId.ToRpcPointerOrNull(),
					wszName = name.ToRpcPointerOrNull(),
					wszDescription = description.ToRpcPointerOrNull(),
					dwProfiles = (uint)profiles,
					Direction = (FW_DIRECTION)direction,
					wIpProtocol = (ushort)protocol,
					unnamed_1 = new Unnamed_9
					{
						wIpProtocol = (ushort)protocol,
						__unnamed_0 = new Unnamed_10
						{
							LocalPorts = MakePortRangeList(localPortRanges),
							RemotePorts = MakePortRangeList(remotePortRanges),
						}
					},
					LocalInterfaceIds = (interfaceIds == null) ? default
						: new FW_INTERFACE_LUIDS
						{
							dwNumLUIDs = (uint)interfaceIds.Length,
							pLUIDs = new RpcPointer<Guid[]>(interfaceIds)
						},
					dwLocalInterfaceTypes = (uint)interfaceTypes,
					wszLocalApplication = localApplication.ToRpcPointerOrNull(),
					wszLocalService = localService?.ToRpcPointerOrNull(),
					Action = (FW_RULE_ACTION)action,
					wFlags = (ushort)flags,
					wszEmbeddedContext = embeddedContext.ToRpcPointerOrNull(),
					Status = FW_RULE_STATUS.FW_RULE_STATUS_OK,

				},
				pStatus,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}
	}
}
