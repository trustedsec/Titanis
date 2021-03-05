using System;

namespace Titanis.Smb2
{
	[Flags]
	public enum Smb2WriteOptions : uint
	{
		None = 0,

		WriteThrough = 1,
		Unbuffered = 2
	}
}
