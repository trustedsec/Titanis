using System;

namespace Titanis.Msrpc.Msscmr
{
	[Flags]
	public enum ServiceStates
	{
		None = 0,

		Active = 1,
		Inactive = 2,
		All = 3,
	}
}
