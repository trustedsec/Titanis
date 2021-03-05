namespace Titanis.Smb2
{
	// [MS-SMB2] § 2.2.13 - SMB2 CREATE Request
	public enum Smb2OplockLevel : byte
	{
		None = 0,
		Level2 = 1,
		Exclusive = 0x08,
		Batch = 0x09,
		Lease = 0xFF
	}


}
