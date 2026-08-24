using Fasp;
using MS_FASP;
using System.Net.Sockets;
using Titanis.DceRpc;
using Titanis.Winterop;

namespace Titanis.Msrpc.Msfasp
{
	[Flags]
	public enum FirewallRuleStatus : uint
	{
		None = 0,
		Ok = FW_RULE_STATUS_CLASS.FW_RULE_STATUS_CLASS_OK,
		PartiallyIgnored = FW_RULE_STATUS_CLASS.FW_RULE_STATUS_CLASS_PARTIALLY_IGNORED,
		Ignored = FW_RULE_STATUS_CLASS.FW_RULE_STATUS_CLASS_IGNORED,
		ParsingError = FW_RULE_STATUS_CLASS.FW_RULE_STATUS_CLASS_PARSING_ERROR,
		SemanticError = FW_RULE_STATUS_CLASS.FW_RULE_STATUS_CLASS_SEMANTIC_ERROR,
		RuntimeError = FW_RULE_STATUS_CLASS.FW_RULE_STATUS_CLASS_RUNTIME_ERROR,
		Error = FW_RULE_STATUS_CLASS.FW_RULE_STATUS_CLASS_ERROR,
		All = FW_RULE_STATUS_CLASS.FW_RULE_STATUS_CLASS_ALL,
	}
	[Flags]
	public enum FirewallProfiles : uint
	{
		Invalid = FW_PROFILE_TYPE.FW_PROFILE_TYPE_INVALID,
		Domain = FW_PROFILE_TYPE.FW_PROFILE_TYPE_DOMAIN,
		Standard = FW_PROFILE_TYPE.FW_PROFILE_TYPE_STANDARD,
		Private = FW_PROFILE_TYPE.FW_PROFILE_TYPE_PRIVATE,
		Public = FW_PROFILE_TYPE.FW_PROFILE_TYPE_PUBLIC,
		All = FW_PROFILE_TYPE.FW_PROFILE_TYPE_ALL,
		Current = FW_PROFILE_TYPE.FW_PROFILE_TYPE_CURRENT,
		None = FW_PROFILE_TYPE.FW_PROFILE_TYPE_NONE
	}

	public struct FirewallVersion
	{
		public FirewallVersion(SchemaVersion value)
		{
			Value = value;
		}

		public SchemaVersion Value { get; }
		public int Major => (int)this.Value >> 8;
		public int Minor => (int)this.Value & 0xFF;

		public override string ToString() => $"{this.Major}.{this.Minor}";
	}

	record struct FirewallPolicyStoreParams(
		FirewallClient owner,
		FirewallVersion supportedVersion
		);

	public class FirewallPolicyStore : IAsyncDisposable
	{
		internal FirewallPolicyStore(in FirewallPolicyStoreParams args)
		{
			this.owner = args.owner;
			this.SupportedVersion = args.supportedVersion;
		}

		// [MS-FASP] § 2.2.93 FW_QUERY
		private const int QuerySchemaVersion = 0x20A;

		protected readonly FirewallClient owner;
		internal RpcPointer<RpcContextHandle> handle;

		public FirewallVersion SupportedVersion { get; }
		internal virtual SchemaVersion NegotiateVersion => SchemaVersion.Version2_0;

		public async ValueTask DisposeAsync()
		{
			if (this.handle != null)
				await owner.ClientProxy.RRPC_FWClosePolicyStore(this.handle, CancellationToken.None).ConfigureAwait(false);
		}

		public virtual async Task<FirewallRule[]> EnumRules(
			FirewallRuleStatus statusFilter,
			FirewallProfiles profileFilter,
			CancellationToken cancellationToken
			)
		{
			RpcPointer<uint> pdwNumRules = new();
			RpcPointer<RpcPointer<FW_RULE2_0>> ppRules = new();
			var res = (Win32ErrorCode)await owner.ClientProxy.RRPC_FWEnumFirewallRules(
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

		public virtual async Task<FirewallRule[]> QueryRules(Query query, CancellationToken cancellationToken) => throw new NotSupportedException($"The server does not support querying.");

		protected static FW_PORTS MakePortRangeList(PortRange[]? ranges)
		{
			return (ranges == null) ? default
				: new FW_PORTS
				{
					wPortKeywords = 0,
					Ports = new FW_PORT_RANGE_LIST
					{
						dwNumEntries = (uint)ranges.Length,
						pPorts = new RpcPointer<FW_PORT_RANGE[]>(Array.ConvertAll(ranges, r => new FW_PORT_RANGE { wBegin = r.StartPort, wEnd = r.EndPort }))
					}
				};
		}
		public virtual async Task AddTcpUdpRule(
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
			var res = (Win32ErrorCode)await owner.ClientProxy.RRPC_FWAddFirewallRule(
				this.handle.value,
				new FW_RULE2_0
				{
					wSchemaVersion = (ushort)SchemaVersion.Version2_0,
					wszRuleId = ruleId.ToRpcPointerOrNull(),
					wszName = name.ToRpcPointerOrNull(),
					wszDescription = description.ToRpcPointerOrNull(),
					dwProfiles = (uint)profiles,
					Direction = (FW_DIRECTION)direction,
					wIpProtocol = (ushort)protocol,
					unnamed_1 = new Unnamed_1
					{
						wIpProtocol = (ushort)protocol,
						__unnamed_0 = new Unnamed_2
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
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}
	}
}