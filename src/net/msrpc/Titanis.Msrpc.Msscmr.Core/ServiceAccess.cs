using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Msrpc.Msscmr
{
	[Flags]
	public enum ServiceAccess
	{
		None = 0,

		QueryConfig = 0x0001,
		ChangeConfig = 0x0002,
		QueryStatus = 0x0004,
		EnumerateDependents = 0x0008,
		Start = 0x0010,
		Stop = 0x0020,
		PauseContinue = 0x0040,
		Interrogate = 0x0080,
		UserDefinedControl = 0x0100,
		AllRights = 0x000F01FF,

		MaxAllowed = 0x02000000,
	}
}
