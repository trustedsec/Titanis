using System;

namespace Titanis.Smb2
{
	[Flags]
	public enum Smb2ShareCaps : uint
	{
		None = 0,

		Dfs = 8,
		ContinuousAvailability = 0x10,
		ScaleOut = 0x20,
		Cluster = 0x40,
		Asymmetric = 0x80,
		RedirectToOwner = 0x100,
	}
}
