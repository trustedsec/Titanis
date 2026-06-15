namespace Titanis.Msrpc.Msdrsr
{
	// [MS-DRSR] § 5.39 DRS_EXTENSIONS_INT
	[Flags]
	public enum DrsBindFlags : uint
	{
		None = 0,

		Base = 1,
		AsyncRepl = 2,
		RemoveApi = 4,
		MoveReqV2 = 8,
		GetChgDeflate = 0x10,
		DcinfoV1 = 0x20,
		RestoreUsnOptimization = 0x40,
		AddEntry = 0x80,
		KccExecute = 0x100,
		AddEntryV2 = 0x200,
		LinkedValueReplication = 0x400,
		DcinfoV2 = 0x800,
		InstanceTypeNotRequiredOnMod = 0x1000,
		CryptoBind = 0x2000,
		GetReplInfo = 0x4000,
		StrongEncryption = 0x8000,
		DcinfoVF = 0x1_0000,
		TransitiveMembership = 0x2_0000,
		AddSidHistory = 0x4_0000,
		PostBeta3 = 0x8_0000,
		GetChgReqV5 = 0x10_0000,
		GetMemberships2 = 0x20_0000,
		GetChgReqV6 = 0x40_0000,
		NondomainNcs = 0x80_0000,
		GetChgReqV8 = 0x100_0000,
		GetChgReplyV5 = 0x200_0000,
		GetChgReplyV6 = 0x400_0000,
		WhistlerBeta3 = 0x800_0000,
		W2K3Deflate = 0x1000_0000,
		GetChgReqV10 = 0x2000_0000,
		Res1 = 0x4000_0000,
		Res2 = 0x8000_0000,
	}
}
