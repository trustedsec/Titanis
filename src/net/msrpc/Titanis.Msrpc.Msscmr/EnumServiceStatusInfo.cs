using System.ComponentModel;

namespace Titanis.Msrpc.Msscmr
{
	public class EnumServiceStatusInfo
	{
		public EnumServiceStatusInfo(string serviceName, string displayName, ServiceStatus status)
		{
			this.ServiceName = serviceName;
			this.DisplayName = displayName;
			this.Status = status;
		}

		public string ServiceName { get; }
		public string DisplayName { get; }
		[Browsable(false)]
		public ServiceStatus Status { get; }

		public ServiceTypes ServiceType => this.Status.ServiceType;
		public ServiceState State => this.Status.CurrentState;
		public int Win32ExitCode => this.Status.Win32ExitCode;
		public int SpecificExitCode => this.Status.ServiceSpecificExitCode;
	}
}