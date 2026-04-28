using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Linterop.Fuse
{
	public enum LinuxFileType
	{
		Fifo = (1 << 12),
		CharacterDevice = (2 << 12),
		Directory = (4 << 12),
		BlockDevice = (6 << 12),
		RegularFile = (8 << 12),
		SymbolicLink = (10 << 12),
		Socket = (12 << 12),
	}
}
