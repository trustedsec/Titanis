using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Msscmr
{
	/// <summary>
	/// Represents the Service Control Manager.
	/// </summary>
	public sealed class Scm : ServiceRpcObjectBase
	{
		public Scm(RpcContextHandle handle, ScmClient client)
			: base(handle, client)
		{
		}

		/// <inheritdoc/>
		protected sealed override Task CloseAsync(CancellationToken cancellationToken)
			=> this.client.CloseScm(this.handle, cancellationToken);

		public async Task<IList<EnumServiceStatusInfo>> GetServicesAsync(
			ServiceTypes types,
			ServiceStates states,
			CancellationToken cancellationToken
			)
		{
			return await this.client.GetServices(this.handle, types, states, cancellationToken).ConfigureAwait(false);
		}
		public async Task<IList<EnumServiceStatusInfo>> GetServicesAsync(
			ServiceStates states,
			CancellationToken cancellationToken
			)
		{
			return await this.client.GetServices(this.handle, ServiceTypes.All, states, cancellationToken).ConfigureAwait(false);
		}
		public async Task<IList<EnumServiceStatusInfo>> GetServicesAsync(
			ServiceTypes types,
			CancellationToken cancellationToken
			)
		{
			return await this.client.GetServices(this.handle, types, ServiceStates.All, cancellationToken).ConfigureAwait(false);
		}
		public async Task<IList<EnumServiceStatusInfo>> GetServicesAsync(CancellationToken cancellationToken)
		{
			return await this.client.GetServices(this.handle, ServiceTypes.All, ServiceStates.All, cancellationToken).ConfigureAwait(false);
		}


		public async Task<IList<EnumServiceStatusInfo>> GetServicesInGroupAsync(
			string group,
			ServiceTypes types,
			ServiceStates states,
			CancellationToken cancellationToken
			)
		{
			return await this.client.GetServicesInGroup(this.handle, group, types, states, cancellationToken).ConfigureAwait(false);
		}
		public async Task<IList<EnumServiceStatusInfo>> GetServicesInGroupAsync(
			string group,
			ServiceTypes types,
			CancellationToken cancellationToken
			)
		{
			return await this.client.GetServicesInGroup(this.handle, group, types, ServiceStates.All, cancellationToken).ConfigureAwait(false);
		}
		public async Task<IList<EnumServiceStatusInfo>> GetServicesInGroupAsync(
			string group,
			ServiceStates states,
			CancellationToken cancellationToken
			)
		{
			return await this.client.GetServicesInGroup(this.handle, group, ServiceTypes.All, states, cancellationToken).ConfigureAwait(false);
		}


		public Task<Service> OpenServiceAsync(string serviceName, ServiceAccess access, CancellationToken cancellationToken)
		{
			return this.client.OpenService(this.handle, serviceName, access, cancellationToken);
		}

		public Task<ScmLock> LockAsync(CancellationToken cancellationToken)
		{
			return this.client.LockScm(this.handle, cancellationToken);
		}

		public Task<Service> CreateServiceAsync(string serviceName, ServiceConfig config, ServiceAccess access, CancellationToken cancellationToken)
		{
			return this.client.CreateService(this.handle, serviceName, config, access, cancellationToken);
		}

		public Task<ScmLockStatus> QueryLockStatusAsync(CancellationToken cancellationToken)
		{
			return this.client.QueryLockStatus(this.handle, cancellationToken);
		}

		public Task<string> GetDisplayNameOf(string serviceName, CancellationToken cancellationToken)
			=> this.client.GetServiceDisplayName(this.handle, serviceName, cancellationToken);
		public Task<string> GetKeyNameOf(string serviceName, CancellationToken cancellationToken)
			=> this.client.GetServiceKeyName(this.handle, serviceName, cancellationToken);
	}
}
