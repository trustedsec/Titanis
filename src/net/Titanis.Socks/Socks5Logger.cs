using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Socks
{
	public class Socks5Logger : ISocks5Callback
	{
		private readonly ILog _log;

		public Socks5Logger(ILog log)
		{
			if (log is null) throw new ArgumentNullException(nameof(log));
			this._log = log;
		}

		void ISocks5Callback.OnConnecting(EndPoint socksEP, EndPoint remoteEP)
		{
			this._log.WriteSocksClientConnectingMessage(5, socksEP, remoteEP);
		}

		void ISocks5Callback.OnConnected(EndPoint socksEP, EndPoint remoteEP, EndPoint? remoteBindEP)
		{
			this._log.WriteSocksClientConnectedMessage(5, socksEP, remoteEP, remoteBindEP);
		}
	}
}
