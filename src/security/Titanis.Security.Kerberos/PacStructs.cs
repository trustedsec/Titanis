using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos
{

	// [MS-PAC] § 2.2.1 - KERB_SID_AND_ATTRIBUTES
	[Flags]
	public enum SidAttributes
	{
		None = 0,

		Mandatory = 1,
		EnabledByDefault = 2,
		Enabled = 4,
		Owner = 8,
		Resource = (1 << 29),
	}

	// [MS-PAC] § 2.10 - UPN_DNS_INFO
	[Flags]
	enum UpnDnsInfoFlags
	{
		None = 0,
		SyntheticUpn = 1,
		HasSidInfo = 2
	}
}
