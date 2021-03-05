using System;

namespace Titanis.Smb2
{
	[Flags]
	public enum Smb2CloseOptions : ushort
	{
		None = 0,

		QueryAttributes = 1
	}
}
