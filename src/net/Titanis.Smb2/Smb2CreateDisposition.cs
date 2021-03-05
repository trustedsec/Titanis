namespace Titanis.Smb2
{
	// [MS-SMB2] § 2.2.13 - SMB2 CREATE Request
	public enum Smb2CreateDisposition : uint
	{
		None = 0,

		/// <summary>
		/// If the file already exists, supersede it. Otherwise, create the file.
		/// </summary>
		Supersede = 0,
		/// <summary>
		/// If the file already exists, return success; otherwise, fail the operation. 
		/// </summary>
		Open = 1,
		/// <summary>
		/// If the file already exists, fail the operation; otherwise, create the file.
		/// </summary>
		Create = 2,
		/// <summary>
		/// Open the file if it already exists; otherwise, create the file.
		/// </summary>
		OpenIf = 3,
		/// <summary>
		/// Overwrite the file if it already exists; otherwise, fail the operation. 
		/// </summary>
		Overwrite = 4,
		/// <summary>
		/// Overwrite the file if it already exists; otherwise, create the file.
		/// </summary>
		OverwriteIf = 5,
	}

}
