using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Winterop.Security
{
	[Flags]
	public enum SecurityInfo : uint
	{
		None = 0,

		Owner = 0x00000001,
		Group = 0x00000002,
		Dacl = 0x00000004,
		Sacl = 0x00000008,
		Label = 0x00000010,
		Attribute = 0x00000020,
		Scope = 0x00000040,
		ProcessTrustLabel = 0x00000080,
		AccessFilter = 0x00000100,
		Backup = 0x00010000,
		ProtectedDacl = 0x80000000,
		ProtectedSacl = 0x40000000,
		UnprotectedDacl = 0x20000000,
		UnprotectedSacl = 0x10000000,
	}
}
