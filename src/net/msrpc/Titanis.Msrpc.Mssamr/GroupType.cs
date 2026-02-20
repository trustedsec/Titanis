namespace Titanis.Msrpc.Mssamr
{
    public enum GroupType : uint
	{
		None = 0,
		AccountGroup = 2,
		ResourceGroup = 4,
		UniversalGroup = 8,
		SecurityEnabled = 0x80000000,
		SecurityAccount = 0x80000002,
		SecurityResource = 0x80000004,
		SecurityUniversal = 0x80000008,
	}
}
