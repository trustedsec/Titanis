using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos
{

	// [MS-PAC] § 2.10 - UPN_DNS_INFO
	[Flags]
	enum UpnDnsInfoFlags
	{
		None = 0,
		SyntheticUpn = 1,
		HasSidInfo = 2
	}
}
