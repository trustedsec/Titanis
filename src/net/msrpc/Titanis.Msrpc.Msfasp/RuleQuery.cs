using Fasp;
using MS_FASP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Msfasp
{
	public class Query
	{
		public Query(QueryConditionGroup[] groups)
		{
			if (groups is null || groups.Length == 0 || groups.Contains(null))
				throw new ArgumentNullException(nameof(groups));

			this.ConditionGroups = groups;
		}

		public QueryConditionGroup[] ConditionGroups { get; }

		internal FW_QUERY ToRpc(SchemaVersion version) => new FW_QUERY
		{
			wSchemaVersion = (ushort)version,
			dwNumEntries = (uint)this.ConditionGroups.Length,
			ORConditions = new RpcPointer<FW_QUERY_CONDITIONS[]>(Array.ConvertAll(this.ConditionGroups, r => new FW_QUERY_CONDITIONS
			{
				dwNumEntries = (uint)r.Conditions.Length,
				AndedConditions = new RpcPointer<FW_QUERY_CONDITION[]>(Array.ConvertAll(r.Conditions, r => r.ToRpc()))
			})),
			Status = FW_RULE_STATUS.FW_RULE_STATUS_OK,
		};
	}

	public class QueryConditionGroup
	{
		public QueryConditionGroup(QueryCondition[] conditions)
		{
			if (conditions is null || conditions.Length == 0 || conditions.Contains(null))
				throw new ArgumentNullException(nameof(conditions));
			this.Conditions = conditions;
		}

		public QueryCondition[] Conditions { get; }
	}


	public enum QueryMatchKey : int
	{
		Profile = FW_MATCH_KEY.FW_MATCH_KEY_PROFILE,
		Status = FW_MATCH_KEY.FW_MATCH_KEY_STATUS,
		ObjectId = FW_MATCH_KEY.FW_MATCH_KEY_OBJECTID,
		FilterId = FW_MATCH_KEY.FW_MATCH_KEY_FILTERID,
		AppPath = FW_MATCH_KEY.FW_MATCH_KEY_APP_PATH,
		Protocol = FW_MATCH_KEY.FW_MATCH_KEY_PROTOCOL,
		LocalPort = FW_MATCH_KEY.FW_MATCH_KEY_LOCAL_PORT,
		RemotePort = FW_MATCH_KEY.FW_MATCH_KEY_REMOTE_PORT,
		Group = FW_MATCH_KEY.FW_MATCH_KEY_GROUP,
		ServiceName = FW_MATCH_KEY.FW_MATCH_KEY_SVC_NAME,
		Direction = FW_MATCH_KEY.FW_MATCH_KEY_DIRECTION,
		LocalUserOwner = FW_MATCH_KEY.FW_MATCH_KEY_LOCAL_USER_OWNER,
		PackageId = FW_MATCH_KEY.FW_MATCH_KEY_PACKAGE_ID,
		Fqbn = FW_MATCH_KEY.FW_MATCH_KEY_FQBN,
		CompartmentId = FW_MATCH_KEY.FW_MATCH_KEY_COMPARTMENT_ID,
		RemoteUserAuthList = FW_MATCH_KEY.FW_MATCH_KEY_REMOTE_USER_AUTH_LIST,
		PackageFamilyName = FW_MATCH_KEY.FW_MATCH_KEY_PACKAGE_FAMILY_NAME,
	}

	public enum QueryMatchType : int
	{
		TrafficMatch = FW_MATCH_TYPE.FW_MATCH_TYPE_TRAFFIC_MATCH,
		Equal = FW_MATCH_TYPE.FW_MATCH_TYPE_EQUAL,
	}

	public class QueryCondition
	{
		private readonly FW_MATCH_VALUE matchValueStruc;

		internal QueryCondition(QueryMatchKey matchKey, QueryMatchType matchType, object? matchValue, FW_MATCH_VALUE matchValueStruc)
		{
			MatchKey = matchKey;
			MatchType = matchType;
			MatchValue = matchValue;
			this.matchValueStruc = matchValueStruc;
		}

		public QueryMatchKey MatchKey { get; }
		public QueryMatchType MatchType { get; }
		public object? MatchValue { get; }

		internal FW_QUERY_CONDITION ToRpc() => new FW_QUERY_CONDITION
		{
			matchKey = (FW_MATCH_KEY)this.MatchKey,
			matchValue = this.matchValueStruc
		};

		private static FW_MATCH_VALUE CreateMatchValue(string value) => new FW_MATCH_VALUE
		{
			type = FW_DATA_TYPE.FW_DATA_TYPE_UNICODE_STRING,
			unnamed_1 = new Unnamed_32
			{
				type = FW_DATA_TYPE.FW_DATA_TYPE_UNICODE_STRING,
				__unnamed_4 = new Unnamed_33
				{
					wszString = new RpcPointer<string>(value)
				}
			}
		};
		private static FW_MATCH_VALUE CreateMatchValue(uint value) => new FW_MATCH_VALUE
		{
			type = FW_DATA_TYPE.FW_DATA_TYPE_UINT32,
			unnamed_1 = new Unnamed_32
			{
				type = FW_DATA_TYPE.FW_DATA_TYPE_UINT32,
				uInt32 = value,
			}
		};
		private static FW_MATCH_VALUE CreateMatchValue(ushort value) => new FW_MATCH_VALUE
		{
			type = FW_DATA_TYPE.FW_DATA_TYPE_UINT16,
			unnamed_1 = new Unnamed_32
			{
				type = FW_DATA_TYPE.FW_DATA_TYPE_UINT16,
				uInt16 = value,
			}
		};
		private static FW_MATCH_VALUE CreateMatchValue(byte value) => new FW_MATCH_VALUE
		{
			type = FW_DATA_TYPE.FW_DATA_TYPE_UINT8,
			unnamed_1 = new Unnamed_32
			{
				type = FW_DATA_TYPE.FW_DATA_TYPE_UINT8,
				uInt8 = value,
			}
		};
		private static FW_MATCH_VALUE CreateMatchValue(ulong value) => new FW_MATCH_VALUE
		{
			type = FW_DATA_TYPE.FW_DATA_TYPE_UINT64,
			unnamed_1 = new Unnamed_32
			{
				type = FW_DATA_TYPE.FW_DATA_TYPE_UINT64,
				uInt64 = value,
			}
		};

		public static QueryCondition RuleId(QueryMatchType matchType, string ruleId) => new QueryCondition(QueryMatchKey.ObjectId, matchType, ruleId, CreateMatchValue(ruleId));
		public static QueryCondition Group(QueryMatchType matchType, string group) => new QueryCondition(QueryMatchKey.Group, matchType, group, CreateMatchValue(group));
		public static QueryCondition Status(QueryMatchType matchType, FirewallRuleStatus status) => new QueryCondition(QueryMatchKey.Status, matchType, status, CreateMatchValue((uint)status));
		public static QueryCondition Profile(QueryMatchType matchType, FirewallProfiles profile) => new QueryCondition(QueryMatchKey.Profile, matchType, profile, CreateMatchValue((uint)profile));
		public static QueryCondition AppPath(QueryMatchType matchType, string appPath) => new QueryCondition(QueryMatchKey.AppPath, matchType, appPath, CreateMatchValue(appPath));
		public static QueryCondition Service(QueryMatchType matchType, string service) => new QueryCondition(QueryMatchKey.ServiceName, matchType, service, CreateMatchValue(service));
		public static QueryCondition Protocol(QueryMatchType matchType, IpProtocolNumber protocol) => new QueryCondition(QueryMatchKey.Protocol, matchType, protocol, CreateMatchValue((ushort)protocol));
		public static QueryCondition LocalPort(QueryMatchType matchType, ushort port) => new QueryCondition(QueryMatchKey.LocalPort, matchType, port, CreateMatchValue(port));
		public static QueryCondition RemotePort(QueryMatchType matchType, ushort port) => new QueryCondition(QueryMatchKey.RemotePort, matchType, port, CreateMatchValue(port));
		public static QueryCondition Direction(QueryMatchType matchType, FirewallRuleDirection direction) => new QueryCondition(QueryMatchKey.Direction, matchType, direction, CreateMatchValue((uint)direction));
	}
}
