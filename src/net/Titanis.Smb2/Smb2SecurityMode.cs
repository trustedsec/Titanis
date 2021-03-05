using System;

namespace Titanis.Smb2
{
	/// <summary>
	/// Specifies the security mode.
	/// </summary>
	[Flags]
	public enum Smb2SecurityMode : ushort
	{
		None = 0,

		SigningEnabled = 1,
		SigningRequired = 2
	}
}
