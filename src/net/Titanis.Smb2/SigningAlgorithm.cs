using System;

namespace Titanis.Smb2
{
	[Flags]
	public enum SigningAlgorithm : ushort
	{
		HmacSha256 = 0,
		AesCmac = 1,
		AesGmac = 2,
	}
}