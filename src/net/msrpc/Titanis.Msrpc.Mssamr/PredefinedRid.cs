namespace Titanis.Msrpc.Mssamr
{
    public enum PredefinedRid : uint
	{
		Admin = 0x1F4,
		Guest = 0x1F5,
		Krbtgt = 0x1F6,
		Users = 0x201,
		Computers = 0x203,
		Controllers = 0x204,
		Admins = 0x220,
		ReadOnlyControllers = 0x209,
	}
}
