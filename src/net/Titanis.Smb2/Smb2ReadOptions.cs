using System;

namespace Titanis.Smb2
{
	[Flags]
	public enum Smb2ReadOptions : byte
	{
		None = 0,

		Unbuffered = 1,
		Compressed = 2
	}
}
