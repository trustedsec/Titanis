using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Winterop.Security
{
	[Flags]
	public enum Smb2FileAccessRights : uint
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

		StandardRightsRead = ReadControl,
		StandardRightsWrite = ReadControl,
		StandardRightsExecute = ReadControl,
		StandardRightsRequired = 0x000F0000,

		FileGenericExecute = Execute | ReadAttributes | Execute | StandardRightsExecute | Synchronize,
		FileGenericRead = ReadAttributes | ReadData | ReadEa | StandardRightsRead | Synchronize,
		FileGenericWrite = AppendData | WriteAttributes | WriteData | WriteEa | StandardRightsWrite | Synchronize,
		FullAccess = StandardRightsRequired | Synchronize | 0x1FF,

		GenericAll = 0x10000000,
		GenericExecute = 0x20000000,
		GenericWrite = 0x40000000,
		GenericRead = 0x80000000
	}
}
