using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Winterop.Security
{
	// [MS-DTYP]
	[Flags]
	public enum FileAccessRights : uint
	{
		None = 0,

		ReadData = 1,
		WriteData = 2,
		AppendData = 4,
		ReadEa = 8,
		WriteEa = 0x10,
		DeleteChild = 0x40,
		Execute = 0x20,
		ReadAttributes = 0x80,
		WriteAttributes = 0x100,

		Delete = 0x00010000,
		ReadControl = 0x00020000,
		WriteDac = 0x00040000,
		WriteOwner = 0x00080000,
		Synchronize = 0x00100000,
		AccessSystemSecurity = 0x01000000,
		MaxAllowed = 0x02000000,

		FileAll = // 0x001F01FF
			0
			// 0x0000000F
			| ReadData
			| WriteData
			| AppendData
			| ReadEa
			// 0x000000F0
			| WriteEa
			| Execute
			| DeleteChild
			| ReadAttributes
			// 0x00000100
			| WriteAttributes
			// 0x000F0000
			| StandardRightsRequired
			// 0x00100000
			| Synchronize,

		FileExecute = // 0x001200A
			0
			// 0x000000A0
			| Execute
			| ReadAttributes
			// 0x00020000
			| ReadControl
			// 0x00100000
			| Synchronize,

		FileWrite = 0x00120116,
		FileRead = 0x00120089,

		StandardRightsRead = ReadControl,
		StandardRightsWrite = ReadControl,
		StandardRightsExecute = ReadControl,
		StandardRightsRequired = 0x000F0000,

		GenericAll = 0x10000000,
		GenericExecute = 0x20000000,
		GenericWrite = 0x40000000,
		GenericRead = 0x80000000
	}
}
