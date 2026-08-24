using MS_FASP;
using System.Buffers.Binary;
using Titanis.DceRpc.Client;
using Titanis.Winterop;

namespace Titanis.Msrpc.Msfasp
{
	public enum FirewallPolicyAccessRights
	{
		None = 0,
		Read = MS_FASP.FW_POLICY_ACCESS_RIGHT.FW_POLICY_ACCESS_RIGHT_READ,
		ReadWrite = MS_FASP.FW_POLICY_ACCESS_RIGHT.FW_POLICY_ACCESS_RIGHT_READ_WRITE,
		Max = MS_FASP.FW_POLICY_ACCESS_RIGHT.FW_POLICY_ACCESS_RIGHT_MAX
	}

	public enum FirewallStoreType : int
	{
		Invalid = FW_STORE_TYPE.FW_STORE_TYPE_INVALID,
		GroupPolicyRsop = FW_STORE_TYPE.FW_STORE_TYPE_GP_RSOP,
		Local = FW_STORE_TYPE.FW_STORE_TYPE_LOCAL,
		//NOT_USED_VALUE_3 = FW_STORE_TYPE.FW_STORE_TYPE_NOT_USED_VALUE_3,
		//NOT_USED_VALUE_4 = FW_STORE_TYPE.FW_STORE_TYPE_NOT_USED_VALUE_4,
		Dynamic = FW_STORE_TYPE.FW_STORE_TYPE_DYNAMIC,
		GroupPolicy = FW_STORE_TYPE.FW_STORE_TYPE_GPO,
		Defaults = FW_STORE_TYPE.FW_STORE_TYPE_DEFAULTS,
		//NOT_USED_VALUE_8 = FW_STORE_TYPE.FW_STORE_TYPE_NOT_USED_VALUE_8,
		//NOT_USED_VALUE_9 = FW_STORE_TYPE.FW_STORE_TYPE_NOT_USED_VALUE_9,
		//NOT_USED_VALUE_10 = FW_STORE_TYPE.FW_STORE_TYPE_NOT_USED_VALUE_10,
		//NOT_USED_VALUE_11 = FW_STORE_TYPE.FW_STORE_TYPE_NOT_USED_VALUE_11,
		//NOT_USED_VALUE_12 = FW_STORE_TYPE.FW_STORE_TYPE_NOT_USED_VALUE_12,
	}

	// [MS-FASP] § 2.2.42 FW_GLOBAL_CONFIG
	public enum SchemaVersion : ushort
	{
		Version2_0 = 0x0200,
		Version2_01 = 0x0201,
		Version2_10 = 0x020A,
		Version2_20 = 0x0214,
		Version2_22 = 0x0216,
		Version2_24 = 0x0218,
		Version2_25 = 0x0219,
		Version2_26 = 0x021A,
		Version2_27 = 0x021B,
		Version2_28 = 0x021C,
		Version2_29 = 0x021D,
		Version2_30 = 0x021E,
		Version2_31 = 0x021F,
		Version2_32 = 0x0220,
	}

	public class FirewallConfiguration
	{
		public FirewallProfiles CurrentProfiles { get; internal set; }
		public bool StatefulFtpDisabled { get; internal set; }
		public bool StatefulPptpDisabled { get; internal set; }
		public bool SaIdleTime { get; internal set; }
	}

	public class FirewallClient : RpcServiceClient<MS_FASP.RemoteFWClientProxy>
	{
		// [MS-FASP] § 2.1 Transport
		public override bool SupportsDynamicTcp => true;
		// [MS-FASP] § 2.1 Transport
		public override bool RequiresEncryptionOverTcp => true;

		internal MS_FASP.RemoteFWClientProxy ClientProxy => this._proxy;

		record struct ConfigEntry(FW_GLOBAL_CONFIG configId, int size, Action<FirewallConfiguration, ArraySegment<byte>> apply);
		public async Task<FirewallConfiguration> GetConfiguration(
			FirewallStoreType storeType,
			CancellationToken cancellationToken)
		{
			ConfigEntry[] entries = [
				new ConfigEntry(FW_GLOBAL_CONFIG.FW_GLOBAL_CONFIG_CURRENT_PROFILE, 4, (c,x)=>c.CurrentProfiles=(FirewallProfiles)BinaryPrimitives.ReadUInt32LittleEndian(x)),
				new ConfigEntry(FW_GLOBAL_CONFIG.FW_GLOBAL_CONFIG_DISABLE_STATEFUL_FTP, 4, (c,x)=>c.StatefulFtpDisabled=0!=BinaryPrimitives.ReadUInt32LittleEndian(x)),
				new ConfigEntry(FW_GLOBAL_CONFIG.FW_GLOBAL_CONFIG_DISABLE_STATEFUL_PPTP, 4, (c,x)=>c.StatefulPptpDisabled=0!=BinaryPrimitives.ReadUInt32LittleEndian(x)),
				new ConfigEntry(FW_GLOBAL_CONFIG.FW_GLOBAL_CONFIG_SA_IDLE_TIME, 4, (c,x)=>c.SaIdleTime=0!=BinaryPrimitives.ReadUInt32LittleEndian(x)),

				];
			SchemaVersion version = await GetSupportedVersion(storeType, cancellationToken).ConfigureAwait(false);

			var config = new FirewallConfiguration();
			foreach (var entry in entries)
			{
				DceRpc.RpcPointer<uint> pcbTransmittedLen = new();
				DceRpc.RpcPointer<uint> pcbRequired = new();
				DceRpc.RpcPointer<ArraySegment<byte>> pBuffer = new(new ArraySegment<byte>(new byte[entry.size], 0, 0));
				var res = (Win32ErrorCode)await _proxy.RRPC_FWGetGlobalConfig(
					(ushort)version,
					(FW_STORE_TYPE)storeType,
					entry.configId,
					(uint)FW_CONFIG_FLAGS.FW_CONFIG_FLAG_RETURN_DEFAULT_IF_NOT_FOUND,
					pBuffer,
					(uint)entry.size,
					pcbTransmittedLen,
					pcbRequired,
					cancellationToken
					).ConfigureAwait(false);
				if (res == Win32ErrorCode.ERROR_SUCCESS)
				{
					entry.apply(config, pBuffer.value);
				}
				else
					;
			}

			return config;
		}

		public async Task<FirewallPolicyStore> OpenPolicyStore(
			FirewallStoreType storeType,
			FirewallPolicyAccessRights access,
			CancellationToken cancellationToken)
		{
			SchemaVersion version = await GetSupportedVersion(storeType, cancellationToken).ConfigureAwait(false);

			FirewallPolicyStoreParams args = new FirewallPolicyStoreParams(this, new FirewallVersion(version));
			var store = version switch
			{
				SchemaVersion.Version2_0 => new FirewallPolicyStore(args),
				SchemaVersion.Version2_10 => new FirewallPolicyStore210(args),
				SchemaVersion.Version2_20 => new FirewallPolicyStore220(args),
				SchemaVersion.Version2_24 => new FirewallPolicyStore224(args),
				SchemaVersion.Version2_25 => new FirewallPolicyStore225(args),
				SchemaVersion.Version2_26 => new FirewallPolicyStore226(args),
				SchemaVersion.Version2_27 => new FirewallPolicyStore227(args),
				SchemaVersion.Version2_31 or > SchemaVersion.Version2_31 => new FirewallPolicyStore231(args),
				_ => new FirewallPolicyStore227(args)
			};

			DceRpc.RpcPointer<DceRpc.RpcContextHandle> phPolicyStore = new();
			var res = (Win32ErrorCode)await _proxy.RRPC_FWOpenPolicyStore(
				(ushort)store.NegotiateVersion,
				(FW_STORE_TYPE)storeType,
				(MS_FASP.FW_POLICY_ACCESS_RIGHT)access,
				0,
				phPolicyStore,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
			store.handle = phPolicyStore;
			return store;
		}

		private async Task<SchemaVersion> GetSupportedVersion(FirewallStoreType storeType, CancellationToken cancellationToken)
		{
			DceRpc.RpcPointer<uint> pcbTransmittedLen = new();
			DceRpc.RpcPointer<uint> pcbRequired = new();
			DceRpc.RpcPointer<ArraySegment<byte>> pBuffer = new(new ArraySegment<byte>(new byte[4], 0, 0));
			var res = (Win32ErrorCode)await _proxy.RRPC_FWGetGlobalConfig(
				(ushort)SchemaVersion.Version2_0,
				(FW_STORE_TYPE)storeType,
				FW_GLOBAL_CONFIG.FW_GLOBAL_CONFIG_BINARY_VERSION_SUPPORTED,
				0,
				pBuffer,
				4,
				pcbTransmittedLen,
				pcbRequired,
				 cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			var version = (SchemaVersion)BinaryPrimitives.ReadUInt32LittleEndian(pBuffer.value);
			return version;
		}
	}
}
