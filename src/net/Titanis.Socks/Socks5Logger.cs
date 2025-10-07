using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Socks
{
	public enum Socks5MessageId
	{
		Other = 0,
		Connecting,
		Connected,
	}
	public class Socks5Logger : ISocks5Callback
	{
		private readonly ILog _log;

		public Socks5Logger(ILog log)
		{
			this._log = log;
		}

		private const string SourceName = "Socks5";

		private static readonly LogMessageType ConnectingMessage = new LogMessageType(LogMessageSeverity.Diagnostic, SourceName, (int)Socks5MessageId.Connecting, "Connecting to SOCKS5 server {0} for upstream connection to {1}", "socksEP", "upstreamEP");

		void ISocks5Callback.OnConnecting(EndPoint socksEP, EndPoint remoteEP)
		{
			this._log.WriteMessage(ConnectingMessage.Create(socksEP, remoteEP));
		}

		private static readonly LogMessageType ConnectedMessage = new LogMessageType(LogMessageSeverity.Diagnostic, SourceName, (int)Socks5MessageId.Connecting, "Connected to SOCKS5 server {0} for upstream connection to {1} with remote bind EP {2}", "socksEP", "upstreamEP", "remoteBindEP");
		void ISocks5Callback.OnConnected(EndPoint socksEP, EndPoint remoteEP, EndPoint? remoteBindEP)
		{
			this._log.WriteMessage(ConnectedMessage.Create(socksEP, remoteEP, remoteBindEP));
		}
	}
}
