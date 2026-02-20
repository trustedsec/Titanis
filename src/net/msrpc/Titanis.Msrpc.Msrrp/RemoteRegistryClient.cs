using ms_dtyp;
using ms_rrp;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Winterop;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp
{
	public enum RegistryRootKey
	{
		Invalid = 0,
		ClassesRoot,
		CurrentUser,
		LocalMachine,
		PerformanceData,
		Users,
		CurrentConfig,
		PerformanceText,
		PerformanceNlsText,
	}

	public class RemoteRegistryClient : RpcServiceClient<winregClientProxy>, IRegistryStore
	{
		// [MS-RRP] § 2.1.1
		public override bool SupportsDynamicTcp => true;

		// [MS-RRP] § 1.9
		public override string? WellKnownPipeName => "winreg";

		internal winregClientProxy proxy => this._proxy;

		#region Root Keys
		private delegate Task<int> OpenRootKeyFunc(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken);

		private static readonly string?[] RootNames = new string?[]
		{
			null,
			"HKEY_CLASSES_ROOT",
			"HKEY_CURRENT_USER",
			"HKEY_LOCAL_MACHINE",
			"HKEY_PERFORMANCE_DATA",
			"HKEY_USERS",
			"HKEY_CURRENT_CONFIG",
			"HKEY_PERFORMANCE_TEXT",
			"HKEY_PERFORMANCE_NLS_TEXT"
		};
		public static string GetRootName(RegistryRootKey rootKey)
		{
			var name = ((int)rootKey < RootNames.Length) ? RootNames[(int)rootKey] : null;
			if (name is null)
				throw new ArgumentException("Not a valid root key.", nameof(rootKey));
			return name;
		}
		public static RegistryRootKey TryResolveRootKey(string name)
		{
			RegistryRootKey rootKey = name.ToUpper() switch
			{
				"HKCR" or "HKEY_CLASSES_ROOT" => RegistryRootKey.ClassesRoot,
				"HKCU" or "HKEY_CURRENT_USER" => RegistryRootKey.CurrentUser,
				"HKLM" or "HKEY_LOCAL_MACHINE" => RegistryRootKey.LocalMachine,
				"HKPD" or "HKEY_PERFORMANCE_DATA" => RegistryRootKey.PerformanceData,
				"HKU" or "HKEY_USERS" => RegistryRootKey.Users,
				"HKCC" or "HKEY_CURRENT_CONFIG" => RegistryRootKey.CurrentConfig,
				"HKPT" or "HKEY_PERFORMANCE_TEXT" => RegistryRootKey.PerformanceText,
				"HKPNT" or "HKEY_PERFORMANCE_NLS_TEXT" => RegistryRootKey.PerformanceNlsText,
				_ => RegistryRootKey.Invalid
			};
			return rootKey;
		}

		private async Task<RegistryKey> OpenRootKey(RegistryRootKey rootKey, RegistryAccessRights access, OpenRootKeyFunc func, CancellationToken cancellationToken)
		{
			DceRpc.RpcPointer<DceRpc.RpcContextHandle> hkey = new();
			var res = (Win32ErrorCode)await func(null, (uint)access, hkey, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			string name = GetRootName(rootKey);
			return new RegistryKey(name, name, hkey.value, this);
		}
		public Task<RegistryKey> OpenClassesRoot(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(RegistryRootKey.ClassesRoot, access, this._proxy.OpenClassesRoot, cancellationToken);

		public Task<RegistryKey> OpenCurrentUser(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(RegistryRootKey.CurrentUser, access, this._proxy.OpenCurrentUser, cancellationToken);

		async Task<IRegistryKey> IRegistryStore.OpenLocalMachine(RegistryAccessRights access, CancellationToken cancellationToken) => await OpenLocalMachine(access, cancellationToken).ConfigureAwait(false);
		public Task<RegistryKey> OpenLocalMachine(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(RegistryRootKey.LocalMachine, access, this._proxy.OpenLocalMachine, cancellationToken);

		public Task<RegistryKey> OpenPerformanceData(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(RegistryRootKey.PerformanceData, access, this._proxy.OpenPerformanceData, cancellationToken);

		public Task<RegistryKey> OpenUsers(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(RegistryRootKey.Users, access, this._proxy.OpenUsers, cancellationToken);

		public Task<RegistryKey> OpenCurrentConfig(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(RegistryRootKey.CurrentConfig, access, this._proxy.OpenCurrentConfig, cancellationToken);

		public Task<RegistryKey> OpenPerformanceText(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(RegistryRootKey.PerformanceText, access, this._proxy.OpenPerformanceText, cancellationToken);

		public Task<RegistryKey> OpenPerformanceNlsText(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(RegistryRootKey.PerformanceNlsText, access, this._proxy.OpenPerformanceNlsText, cancellationToken);

		public Task<RegistryKey> OpenRootKey(RegistryRootKey rootKey, RegistryAccessRights access, CancellationToken cancellationToken)
		{
			Func<RegistryAccessRights, CancellationToken, Task<RegistryKey>> method = rootKey switch
			{
				RegistryRootKey.ClassesRoot => this.OpenClassesRoot,
				RegistryRootKey.CurrentUser => this.OpenCurrentUser,
				RegistryRootKey.LocalMachine => this.OpenLocalMachine,
				RegistryRootKey.PerformanceData => this.OpenPerformanceData,
				RegistryRootKey.Users => this.OpenUsers,
				RegistryRootKey.CurrentConfig => this.OpenCurrentConfig,
				RegistryRootKey.PerformanceText => this.OpenPerformanceText,
				RegistryRootKey.PerformanceNlsText => this.OpenPerformanceNlsText,
				_ => throw new ArgumentException("Bad root key", nameof(rootKey))
			};

			return method(access, cancellationToken);
		}

		#endregion
	}
}
