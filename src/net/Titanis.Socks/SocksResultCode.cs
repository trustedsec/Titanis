using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Socks
{
	public enum Socks5ResultCode : byte
	{
		Success = 0,
		GeneralServerFailure = 1,
		ConnectionNotAllowedByRuleset = 2,
		NetworkUnreachable = 3,
		HostUnreachable = 4,
		ConnectionRefused = 5,
		TtlExpired = 6,
		CommandNotSupported = 7,
		AddressTypeNotSupported = 8,
	}
}
