using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Smb2
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
		GenericAll = 0x10000000,
		GenericExecute = 0x20000000,
		GenericWrite = 0x40000000,
		GenericRead = 0x80000000,

		// Captured from Notepad
		DefaultCreateAccess = 0x0012019f,
		// Also CMD agrees
		DefaultOpenReadAccess = 0x00120089,

		// From command prompt
		DefaultCreateDirAccess = 0x00100081,
		DefaultRemoveDirAccess = 0x00110080,
		DefaultDeleteFileAccess = 0x00010080,
		/// <summary>
		/// Access mask used by CMD.EXE to list the contents of a directory within a share.
		/// </summary>
		DefaultOpenDirAccess = 0x00100081,
	}
}
