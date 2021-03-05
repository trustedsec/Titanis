using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	[Flags]
	public enum APOptions
	{
		None = 0,

		Reserved = (1 << 31),
		UseSessionKey = (1 << (31 - 1)),
		MutualRequired = (1 << (31 - 2)),
	}
}
