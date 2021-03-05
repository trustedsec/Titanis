using System;

namespace Titanis.Smb2
{
	/// <summary>
	/// Specifies SMB capabilities that are supported on a connection.
	/// </summary>
	/// <seealso cref="Smb2CapabilitySets"/>
	[Flags]
	public enum Smb2Capabilities : int
	{
		None = 0,

		Dfs = 1,
		Leasing = 2,
		LargeMtu = 4,
		MultiChannel = 8,
		PersistentHandles = 0x10,
		DirectoryLeasing = 0x20,
		Encryption = 0x40,
		Notifications = 0x80,

	}

	/// <summary>
	/// Specifies sets of SMB capabilities by version.
	/// </summary>
	/// <remarks>
	/// These values are not included in <see cref="Smb2Capabilities"/> for formatting reasons.
	/// </remarks>
	public static class Smb2CapabilitySets
	{
		public const Smb2Capabilities Version_2_0_2 = Smb2Capabilities.Dfs;
		public const Smb2Capabilities Version_2_1 = Version_2_0_2 | Smb2Capabilities.Leasing | Smb2Capabilities.LargeMtu;
		public const Smb2Capabilities Version_3_0 = Version_2_1 | Smb2Capabilities.MultiChannel | Smb2Capabilities.PersistentHandles | Smb2Capabilities.DirectoryLeasing | Smb2Capabilities.Encryption;
		public const Smb2Capabilities Version_3_0_2 = Version_3_0;
		public const Smb2Capabilities Version_3_1_1 = Version_3_0_2;
	}
}
