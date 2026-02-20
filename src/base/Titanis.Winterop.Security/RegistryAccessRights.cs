using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Winterop.Security
{
	// [MS-DTYP]
	/// <summary>
	/// Specifies the access rights that can be applied to registry keys.
	/// </summary>
	[Flags]
	public enum RegistryAccessRights : uint
	{
		None=0,

		QueryValue = 1,
		SetValue = 2,
		Default = 3,
		CreateSubkey = 4,
		EnumerateSubkeys = 8,
		Notify = 0x10,
		CreateLink = 0x20,

		Wow64_Use64 = 0x100,
		Wow64_Use32 = 0x200,

		Delete = 0x00010000,
		ReadControl = 0x00020000,
		WriteDac = 0x00040000,
		WriteOwner = 0x00080000,
		Synchronize = 0x00100000,

		KeyAll=0x000F003F,
		KeyRead=0x00020019,
		KeyExecute=0x00020019,
		KeyWrite=0x00020006,


	}
}
