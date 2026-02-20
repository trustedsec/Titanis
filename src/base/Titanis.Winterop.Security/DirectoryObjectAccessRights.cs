using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Winterop.Security
{
	// [MS-DTYP]
	[Flags]
	public enum DirectoryObjectAccessRights
	{
		None = 0,

		ControlAccess = 0x00000100,
		ListObject = 0x00000080,
		DeleteTree = 0x00000040,
		WriteProperty = 0x00000020,
		ReadProperty = 0x00000010,
		SelfWrite = 0x00000008,
		ListChildren = 0x00000004,
		DeleteChild = 0x00000002,
		CreateChild = 0x00000001,

		Delete = 0x00010000,
		ReadControl = 0x00020000,
		WriteDac = 0x00040000,
		WriteOwner = 0x00080000,
		Synchronize = 0x00100000,
		RequiredRightsMask = 0x000F0000,

		AllStandardRights = 0x001F0000,
	}
}
