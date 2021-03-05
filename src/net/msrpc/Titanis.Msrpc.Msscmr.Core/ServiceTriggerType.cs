namespace Titanis.Msrpc.Msscmr
{
	public enum ServiceTriggerType
	{
		InterfaceArrival = 1,
		IpAddressAvailability = 2,
		DomainJoin = 3,
		FirewallPortEvent = 4,
		GroupPolicyChange = 5,

		Etw = 20,   // Documented as 0x20 in [MS-SCMR] but that appears to be wrong

		// Not in [MS-SCMR]
		SystemStateChangeEvent = 7,
		NetworkEvent = 6,
	}
}
