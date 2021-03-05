using System;

namespace Titanis.Msrpc.Msscmr
{
	[Flags]
	public enum ScmAccess
	{
		None = 0,

		Connect = 0x0001,
		CreateService = 0x0002,
		EnumerateService = 0x0004,
		Lock = 0x0008,
		QueryLockStatus = 0x0010,
		ModifyBootConfig = 0x0020,
	}
}
