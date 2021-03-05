namespace Titanis.Smb2
{
	// [MS-SMB2] § 2.2.13 - SMB2 CREATE Request
	public enum Smb2ImpersonationLevel : uint
	{
		Anonymous = 0,
		Identification = 1,
		Impersonation = 2,
		Delegate = 3,
	}


}
