using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Security.Kerberos
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct GssChannelBinding
	{
		internal uint initiatorAddrType;
		internal uint initiatorAddr;
		internal uint acceptorAddrType;
		internal uint acceptorAddr;
	}
}
