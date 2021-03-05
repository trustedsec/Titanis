using System;

namespace Titanis.Msrpc.Msscmr
{
	public enum ServiceFailureActionType
	{
		None = 0,
		RestartService = 1,
		Reboot = 2,
		RunCommand = 3,
	}
	public class ServiceFailureAction
	{
		public ServiceFailureActionType ActionType { get; set; }
		public TimeSpan Delay { get; set; }
	}
}