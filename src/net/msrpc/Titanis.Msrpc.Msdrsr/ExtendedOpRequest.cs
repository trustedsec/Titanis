namespace Titanis.Msrpc.Msdrsr
{
	// [MS-DRSR] § 4.1.10.2.22 EXOP_REQ Codes
	public enum ExtendedOpRequest
	{
		FsmoReqRole = 1,
		FsmoReqRidAlloc = 2,
		FsmoRidReqRole = 3,
		FsmoReqPdf = 4,
		FsmoAbandonRole = 5,
		ReplObject = 6,
		ReplSecrets = 7,
	}
}
