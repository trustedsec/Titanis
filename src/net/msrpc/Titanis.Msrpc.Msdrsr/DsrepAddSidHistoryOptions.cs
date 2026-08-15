namespace Titanis.Msrpc.Msdrsr
{
	// [MS-DRSR] § 4.1.2.1.5 DRS_ADDSID_FLAGS
	[Flags]
	public enum DsrepAddSidHistoryOptions : uint
	{
		None = 0,
		CheckSecure = 0x40000000,
		DeleteSourceObject = 0x80000000,
	}
}
