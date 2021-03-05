using ms_scmr;

namespace Titanis.Msrpc.Msscmr
{
	public class ServiceStatusProcess
	{
		public ServiceTypes ServiceType { get; set; }
		public ServiceState CurrentState { get; set; }
		public ServiceControlsAccepted ControlsAccepted { get; set; }
		public int Win32ExitCode { get; set; }
		public int ServiceSpecificExitCode { get; set; }
		public int Checkpoint { get; set; }
		public int WaitHint { get; set; }
		public int ProcessId { get; set; }
		public bool IsSystemProcess { get; }

		public ServiceStatusProcess(SERVICE_STATUS_PROCESS value)
		{
			this.ServiceType = (ServiceTypes)value.dwServiceType;
			this.CurrentState = (ServiceState)value.dwCurrentState;
			this.ControlsAccepted = (ServiceControlsAccepted)value.dwControlsAccepted;
			this.Win32ExitCode = (int)value.dwWin32ExitCode;
			this.ServiceSpecificExitCode = (int)value.dwServiceSpecificExitCode;
			this.Checkpoint = (int)value.dwCheckPoint;
			this.WaitHint = (int)value.dwWaitHint;
			this.ProcessId = (int)value.dwProcessId;
			this.IsSystemProcess = (0 != (value.dwServiceFlags & 0x01));
		}
	}
}