using System;

namespace Titanis.Smb2
{
	[Flags]
	public enum CompressionCaps : uint
	{
		None = 0,

		Chained = 1
	}
}
