using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Winterop.Security
{
	[Flags]
	public enum ServiceAccessRights
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

		// Standard
		Delete = 0x00010000,
		ReadControl = 0x00020000,
		WriteDac = 0x00040000,
		WriteOwner = 0x00080000,
		Synchronize = 0x00100000,

		MaxAllowed = 0x02000000,
	}
}
