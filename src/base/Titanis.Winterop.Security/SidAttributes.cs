using System;

namespace Titanis.Winterop.Security
{
	// [MS-PAC] § 2.2.1 - KERB_SID_AND_ATTRIBUTES
	[Flags]
	public enum SidAttributes
	{
		None = 0,

		Mandatory = 1,
		EnabledByDefault = 2,
		Enabled = 4,
		Owner = 8,
		Resource = (1 << 29),
	}
}
