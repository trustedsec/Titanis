using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	enum AddressType
	{
		Ipv4 = 2,
		Directional = 3,
		ChaosNet = 5,
		Xns = 6,
		Iso = 7,
		Decnet = 12,
		AppleTalkDDP = 16,
		Netbios = 20,
		Ipv6 = 24,
	}
}
