using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Winterop.Security
{
	/// <summary>
	/// Specifies standard access rights.
	/// </summary>
	[Flags]
	public enum StandardAccessRights : uint
	{
		Delete = 0x00010000,
		ReadControl = 0x00020000,
		WriteDac = 0x00040000,
		WriteOwner = 0x00080000,
		Synchronize = 0x00100000,
		RequiredRightsMask = 0x000F0000,
		AllStandardRights = 0x001F0000,
		SpecificRightsMask = 0x0000FFFF,
		AccessSystemSecurity = 0x01000000,
		MaxAllowed = 0x02000000,
	}
}
