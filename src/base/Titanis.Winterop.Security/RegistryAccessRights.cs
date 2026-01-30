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

		KeyQueryValue = 1,
		KeySetValue = 2,
		Default = 3,
		KeyCreateSubKey = 4,
		KeyEnumerateSubKey = 8,
		KeyNotify = 0x10,
		KeyCreate = 0x20,
		Delete = 65536,
		ReadControl = 131072,
		WriteDac = 262144,
		WriteOwner = 524288,

		KeyAll=0x000F003F,
		KeyRead=0x00020019,
		KeyExecute=0x00020019,
		KeyWrite=0x00020006,


	}
}
