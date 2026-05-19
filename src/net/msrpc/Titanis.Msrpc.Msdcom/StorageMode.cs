using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Msdcom
{
	public enum StorageMode
	{
		// Access
		Read = 0,
		Write = 1,
		ReadWrite = 2,

		// Sharing
		ShareDenyNone = 0x40,
		ShareDenyRead = 0x30,
		ShareDenyWrite = 0x20,
		ShareExclusive = 0x10,
		Priority = 0x00040000,

		// Creation
		Create = 0x00001000,
		Convert = 0x00020000,
		FailIfThere = 0x00000000,
		// Transactioning
		Direct = 0x00000000,
		Transacted = 0x00010000,
		// Transactioning Performance
		NoScratch = 0x00100000,
		NoSnapshot = 0x00200000,
		// Direct SWMR and Simple
		Simple = 0x08000000,
		DirectSwmr = 0x00400000,
		// Delete On Release
		DeleteOnRelease = 0x04000000,
	}
}
