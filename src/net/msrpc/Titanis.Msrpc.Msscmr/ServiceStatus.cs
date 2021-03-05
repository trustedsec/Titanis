using ms_scmr;

namespace Titanis.Msrpc.Msscmr
{
	public class ServiceStatus
	{
		public ServiceTypes ServiceType { get; set; }
		public ServiceState CurrentState { get; set; }
		public ServiceControlsAccepted ControlsAccepted { get; set; }
		public int Win32ExitCode { get; set; }
		public int ServiceSpecificExitCode { get; set; }
		public int Checkpoint { get; set; }
		public int WaitHint { get; set; }

		public ServiceStatus(SERVICE_STATUS value)
		{
			this.ServiceType = (ServiceTypes)value.dwServiceType;
			this.CurrentState = (ServiceState)value.dwCurrentState;
			this.ControlsAccepted = (ServiceControlsAccepted)value.dwControlsAccepted;
			this.Win32ExitCode = (int)value.dwWin32ExitCode;
			this.ServiceSpecificExitCode = (int)value.dwServiceSpecificExitCode;
			this.Checkpoint = (int)value.dwCheckPoint;
			this.WaitHint = (int)value.dwWaitHint;
		}
	}
}