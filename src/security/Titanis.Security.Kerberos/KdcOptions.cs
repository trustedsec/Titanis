using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	[Flags]
	public enum KdcOptions
	{
		None = 0,

		Reserved = (1 << 31),
		Forwardable = (1 << (31 - 1)),
		Forwarded = (1 << (31 - 2)),
		Proxiable = (1 << (31 - 3)),
		Proxy = (1 << (31 - 4)),
		AllowPostdate = (1 << (31 - 5)),
		Postdated = (1 << (31 - 6)),
		Unused = (1 << (31 - 7)),
		Renewable = (1 << (31 - 8)),
		Initial = (1 << (31 - 9)),
		Preauthenticated = (1 << (31 - 10)),
		HardwareAuthenticated = (1 << (31 - 11)),
		TransitedPolicyChecked = (1 << (31 - 12)),
		OkAsDelegate = (1 << (31 - 13)),
		Canonicalize = (1 << (31 - 15)),
		DisableTransitedCheck = (1 << (31 - 26)),
		RenewableOK = (1 << (31 - 27)),
		EncTicketInSKey = (1 << (31 - 28)),
		Renew = (1 << (31 - 30)),
		Validate = (1 << (31 - 31)),
	}
}
