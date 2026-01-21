using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Msrrp
{
	[Flags]
	public enum RegistryAccessRights : uint
	{
		None = 0,

		QueryValue = 1,
		SetValue = 2,
		CreateSubkey = 4,
		EnumerateSubkeys = 8,
		CreateLink = 0x20,
		Wow64_Use64 = 0x100,
		Wow64_Use32 = 0x200
	}
}
