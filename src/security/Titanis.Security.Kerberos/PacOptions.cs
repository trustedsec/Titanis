using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	[Flags]
	public enum PacOptions : uint
	{
		// [MS-KILE] § 2.2.10
		None = 0,
		Claims = (1U << 31),
		BranchAware = (1U << 30),
		ForwardToFullDc = (1U << 29),

		// [MS-SFU] § 2.2.5
		ResourceBasedConstrainedDelegation = (1U << 28),
	}
}
