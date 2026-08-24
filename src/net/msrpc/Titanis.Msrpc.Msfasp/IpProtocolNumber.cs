using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasp;

public enum IpProtocolNumber : ushort
{
	IP = 0,
	IPv6HopByHopOptions = 0,
	Icmp = 1,
	Igmp = 2,
	Ggp = 3,
	IPv4 = 4,
	Tcp = 6,
	Pup = 12,
	Udp = 17,
	Idp = 22,
	IPv6 = 41,
	IPv6RoutingHeader = 43,
	IPv6FragmentHeader = 44,
	IPSecEncapsulatingSecurityPayload = 50,
	IPSecAuthenticationHeader = 51,
	IcmpV6 = 58,
	IPv6NoNextHeader = 59,
	IPv6DestinationOptions = 60,
	ND = 77,
	Raw = 255,
	Unspecified = 0,
	Ipx = 1000,
	Spx = 1256,
	SpxII = 1257,

	Any = 256,
}
