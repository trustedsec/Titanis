namespace Titanis.Msrpc.Mssamr
{
    public enum SamAccountType : uint
	{
		None = 0,

		DomainObject = 0,
		GroupObject = 0x10000000,
		NonSecurityGroupObject = 0x10000001,
		AliasObject = 0x20000000,
		NonSecurityAliasObject = 0x20000001,
		UserObject = 0x30000000,
		MachineAccount = 0x30000001,
		TrustAccount = 0x30000002,
		AppBasicGroup = 0x40000000,
		AppQueryGroup = 0x40000001,
	}
}
