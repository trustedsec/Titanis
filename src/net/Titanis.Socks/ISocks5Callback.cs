using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Socks
{
	public interface ISocks5Callback
	{
		void OnConnecting(EndPoint socksEP, EndPoint remoteEP);
		void OnConnected(EndPoint socksEP, EndPoint remoteEP, EndPoint? remoteBindEP);
	}
}
