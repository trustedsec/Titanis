using System;

namespace Titanis.Smb2
{
	/// <summary>
	/// Specifies a preauthentication hash algorithm.
	/// </summary>
	[Flags]
	public enum PreauthHashAlgorithm : ushort
	{
		None = 0,

		Sha512 = (1 << 0),
	}
}
