using System;

namespace Titanis.Msrpc.Msscmr
{
	[Flags]
	public enum ServiceControlsAccepted
	{
		None = 0,

		AcceptsStop = 0x00000001,
		AcceptsPauseContinue = 0x00000002,
		AcceptsShutdown = 0x00000004,
		AcceptsParamChange = 0x00000008,
		AcceptsNetBindChange = 0x00000010,
		AcceptsHardwareProfileChange = 0x00000020,
		AcceptsPowerEvent = 0x00000040,
		AcceptsSessionChange = 0x00000080,
		AcceptsPreShutdown = 0x00000100,
		AcceptsTimeChange = 0x00000200,
		AcceptsTriggerEvent = 0x00000400,
		AcceptsUserLogoff = 0x00000800,
		AcceptsLowResources = 0x00002000,
		AcceptsSystemLowResources = 0x00004000,
	}
}