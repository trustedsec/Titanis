namespace Titanis.Msrpc.Msdrsr
{
	// [MS-DRSR] § 5.39 DRS_EXTENSIONS_INT
	[Flags]
	enum DrsBindMoreFlags
	{
		None = 0,
		Adam = 1,
		LonghornBeta2 = 2,
		RecycleBin = 4,
		GetchgreplyV9 = 0x0100,
		RpcCorrelationId1 = 0x0400
	}
}
