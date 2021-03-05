using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msscmr
{
	public class Service : ServiceRpcObjectBase
	{
		public string ServiceName { get; }

		internal Service(
			RpcContextHandle handle,
			string serviceName,
			ScmClient client)
			: base(handle, client)
		{
			ServiceName = serviceName;
		}

		/// <inheritdoc/>
		protected sealed override Task CloseAsync(CancellationToken cancellationToken)
			=> this.client.CloseScm(this.handle, cancellationToken);

		public Task<ServiceConfig> QueryConfigAsync(CancellationToken cancellationToken)
			=> this.client.QueryServiceConfig(this.handle, cancellationToken);

		public Task<string> QueryDescriptionAsync(CancellationToken cancellationToken)
			=> this.client.QueryServiceDescription(this.handle, cancellationToken);

		public Task SetDescriptionAsync(string description, CancellationToken cancellationToken)
			=> this.client.SetServiceDescription(this.handle, description, cancellationToken);

		public Task ChangeConfigAsync(ServiceConfig config, CancellationToken cancellationToken)
			=> this.client.ChangeConfig(this.handle, config, cancellationToken);

		public Task<ServiceTrigger[]> QueryTriggersAsync(CancellationToken cancellationToken)
			=> this.client.QueryServiceTriggers(this.handle, this.ServiceName, cancellationToken);

		public async Task SetBinaryPathAsync(string binaryPath, CancellationToken cancellationToken)
		{
			if (binaryPath is null)
				throw new ArgumentNullException(nameof(binaryPath));
			ServiceConfig config = await this.QueryConfigAsync(cancellationToken).ConfigureAwait(false);
			config.BinaryPathName = binaryPath;
			await this.ChangeConfigAsync(config, cancellationToken).ConfigureAwait(false);
		}

		public Task<ServiceStatus> ControlAsync(ServiceControl control, CancellationToken cancellationToken)
			=> this.client.ControlService(this.handle, control, cancellationToken);

		public Task<ServiceStatus> ControlAsync(int control, CancellationToken cancellationToken) => this.ControlAsync((ServiceControl)control, cancellationToken);
		public Task<ServiceStatus> StopAsync(CancellationToken cancellationToken) => this.ControlAsync(ServiceControl.Stop, cancellationToken);
		public Task<ServiceStatus> PauseAsync(CancellationToken cancellationToken) => this.ControlAsync(ServiceControl.Pause, cancellationToken);
		public Task<ServiceStatus> ContinueAsync(CancellationToken cancellationToken) => this.ControlAsync(ServiceControl.Continue, cancellationToken);
		public Task<ServiceStatus> InterrogateAsync(CancellationToken cancellationToken) => this.ControlAsync(ServiceControl.Interrogate, cancellationToken);

		public Task DeleteAsync(CancellationToken cancellationToken)
			=> this.client.DeleteService(this.handle, cancellationToken);

		public Task<byte[]> QuerySecurityAsync(SecurityInfo sections, CancellationToken cancellationToken)
			=> this.client.QuerySecurity(this.handle, sections, cancellationToken);

		public Task<ServiceStatus> QueryStatusAsync(CancellationToken cancellationToken)
			=> this.client.QueryStatus(this.handle, cancellationToken);

		public Task SetStatusAsync(ServiceStatus status, CancellationToken cancellationToken)
			=> this.client.SetStatus(this.handle, status, cancellationToken);

		public Task SetSecurityAsync(byte[] sd, SecurityInfo sections, CancellationToken cancellationToken)
			=> this.client.SetSecurity(this.handle, sd, sections, cancellationToken);

		public Task<IList<EnumServiceStatusInfo>> GetDependentServices(ServiceStates states, CancellationToken cancellationToken)
			=> this.client.GetDependentServices(this.handle, states, cancellationToken);

		public Task<IList<EnumServiceStatusInfo>> GetDependentServices(CancellationToken cancellationToken)
			=> this.client.GetDependentServices(this.handle, ServiceStates.All, cancellationToken);

		public Task StartAsync(CancellationToken cancellationToken) => this.client.StartService(this.handle, null, cancellationToken);
		public Task StartAsync(string[] args, CancellationToken cancellationToken)
			=> this.client.StartService(this.handle, args, cancellationToken);

		public Task<ServiceStatusProcess> QueryProcessInfoAsync(CancellationToken cancellationToken)
			=> this.client.QueryServiceProcessInfo(this.handle, cancellationToken);
	}
}
