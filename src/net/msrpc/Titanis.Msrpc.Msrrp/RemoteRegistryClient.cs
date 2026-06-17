using ms_dtyp;
using ms_rrp;
using System.Xml.Linq;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Winterop;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp
{


	public class RemoteRegistryClient : RpcServiceClient<winregClientProxy>, IRegistryStore
	{
		// [MS-RRP] § 2.1.1
		public override bool SupportsDynamicTcp => true;

		// [MS-RRP] § 1.9
		public override string? WellKnownPipeName => "winreg";

		internal winregClientProxy proxy => this._proxy;

		#region Root Keys
		private delegate Task<int> OpenRootKeyFunc(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken);





		private async Task<RegistryKey> OpenRootKey(PredefinedKey rootKey, RegistryAccessRights access, OpenRootKeyFunc func, CancellationToken cancellationToken)
		{
			DceRpc.RpcPointer<DceRpc.RpcContextHandle> hkey = new();
			var res = (Win32ErrorCode)await func(null, (uint)access, hkey, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			string name = RegistryRootKey.GetRootName(rootKey);
			return new RegistryKey(name, name, hkey.value, this);
		}
		public Task<RegistryKey> OpenClassesRoot(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(PredefinedKey.ClassesRoot, access, this._proxy.OpenClassesRoot, cancellationToken);

		public Task<RegistryKey> OpenCurrentUser(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(PredefinedKey.CurrentUser, access, this._proxy.OpenCurrentUser, cancellationToken);

		async Task<IRegistryKey> IRegistryStore.OpenLocalMachine(RegistryAccessRights access, CancellationToken cancellationToken) => await OpenLocalMachine(access, cancellationToken).ConfigureAwait(false);
		public Task<RegistryKey> OpenLocalMachine(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(PredefinedKey.LocalMachine, access, this._proxy.OpenLocalMachine, cancellationToken);

		public Task<RegistryKey> OpenPerformanceData(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(PredefinedKey.PerformanceData, access, this._proxy.OpenPerformanceData, cancellationToken);

		public Task<RegistryKey> OpenUsers(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(PredefinedKey.Users, access, this._proxy.OpenUsers, cancellationToken);

		public Task<RegistryKey> OpenCurrentConfig(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(PredefinedKey.CurrentConfig, access, this._proxy.OpenCurrentConfig, cancellationToken);

		public Task<RegistryKey> OpenPerformanceText(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(PredefinedKey.PerformanceText, access, this._proxy.OpenPerformanceText, cancellationToken);

		public Task<RegistryKey> OpenPerformanceNlsText(RegistryAccessRights access, CancellationToken cancellationToken)
			=> this.OpenRootKey(PredefinedKey.PerformanceNlsText, access, this._proxy.OpenPerformanceNlsText, cancellationToken);

		public Task<RegistryKey> OpenRootKey(PredefinedKey rootKey, RegistryAccessRights access, CancellationToken cancellationToken)
		{
			Func<RegistryAccessRights, CancellationToken, Task<RegistryKey>> method = rootKey switch
			{
				PredefinedKey.ClassesRoot => this.OpenClassesRoot,
				PredefinedKey.CurrentUser => this.OpenCurrentUser,
				PredefinedKey.LocalMachine => this.OpenLocalMachine,
				PredefinedKey.PerformanceData => this.OpenPerformanceData,
				PredefinedKey.Users => this.OpenUsers,
				PredefinedKey.CurrentConfig => this.OpenCurrentConfig,
				PredefinedKey.PerformanceText => this.OpenPerformanceText,
				PredefinedKey.PerformanceNlsText => this.OpenPerformanceNlsText,
				_ => throw new ArgumentException("Bad root key", nameof(rootKey))
			};

			return method(access, cancellationToken);
		}

		#endregion
	}
}
