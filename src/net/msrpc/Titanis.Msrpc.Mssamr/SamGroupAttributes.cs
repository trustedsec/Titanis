using System;

namespace Titanis.Msrpc.Mssamr
{
    [Flags]
	public enum SamGroupAttributes : uint
	{
		None = 0,

		Mandatory = 1,
		EnabledByDefault = 2,
		Enabled = 4,
	}
}
