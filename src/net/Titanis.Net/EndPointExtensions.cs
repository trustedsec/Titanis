using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Net
{
	public static class EndPointExtensions
	{
		/// <summary>
		/// Gets the host name or address and port from an <see cref="EndPoint"/>.
		/// </summary>
		/// <param name="ep"><see cref="EndPoint"/> to inspect</param>
		/// <param name="host">Host name or address</param>
		/// <param name="port">Port</param>
		/// <returns><see langword="true"/> if <paramref name="host"/> and <paramref name="port"/> were extracted; otherwise, <see langword="false"/></returns>
		public static bool TryGetHostAndPort(this EndPoint ep, out string? host, out int port)
		{
			if (ep is DnsEndPoint dnsep)
			{
				host = dnsep.Host;
				port = dnsep.Port;
			}
			else if (ep is IPEndPoint ipep)
			{
				host = ipep.Address.ToString();
				port = ipep.Port;
			}
			else
			{
				host = null;
				port = 0;
				return false;
			}

			return true;
		}
	}
}
