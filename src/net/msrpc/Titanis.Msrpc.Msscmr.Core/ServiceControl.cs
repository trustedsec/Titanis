namespace Titanis.Msrpc.Msscmr
{
	public enum ServiceControl
	{
		Stop = 0x00000001,
		Pause = 0x00000002,
		Continue = 0x00000003,
		Interrogate = 0x00000004,
		Shutdown = 0x00000005,
		ParamChange = 0x00000006,
		NetBindAdd = 0x00000007,
		NetBindRemove = 0x00000008,
		NetBindEnable = 0x00000009,
		NetBindDisable = 0x0000000A,
		DeviceEvent = 0x0000000B,
		HardwareProfileChange = 0x0000000C,
		PowerEvent = 0x0000000D,
		SessionChange = 0x0000000E,
		PreShutdown = 0x0000000F,
		TimeChange = 0x00000010,
		TriggerEvent = 0x00000020,
		LowResources = 0x00000060,
		SystemLowResources = 0x00000061,
	}
}