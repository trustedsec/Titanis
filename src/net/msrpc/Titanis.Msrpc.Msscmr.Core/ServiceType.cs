using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Msrpc.Msscmr
{
	[Flags]
	public enum ServiceTypes
	{
		None = 0,

		KernelDriver = 1,
		FileSystemDriver = 2,
		OwnProcess = 0x10,
		SharedProcess = 0x20,

		All = 0x33,
	}
}
