using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	[Flags]
	enum PacOptions : uint
	{
		None = 0,
		Claims = (1U << 31),
		BranchAware = (1U << 30),
		ForwardToFullDc = (1U << 29),
	}
}
