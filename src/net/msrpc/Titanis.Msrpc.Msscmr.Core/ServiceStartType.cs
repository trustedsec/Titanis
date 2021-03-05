namespace Titanis.Msrpc.Msscmr
{
	public enum ServiceStartType : uint
	{
		Boot = 0x00000000,
		System = 0x00000001,
		Auto = 0x00000002,
		Demand = 0x00000003,
		Disabled = 0x00000004,
	}
}