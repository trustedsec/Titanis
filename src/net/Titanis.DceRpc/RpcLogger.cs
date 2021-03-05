using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc.Client;
using Titanis.Net;
using Titanis.Security;

namespace Titanis.DceRpc
{
	public enum RpcMessageId
	{
		Othe = 0,
		ConnectingProxy,
		BindingProxy,
		OversizedPacket,
	}
	public class RpcLogger : IRpcCallback
	{
		private readonly ILog _log;

		public RpcLogger(ILog log)
		{
			ArgumentNullException.ThrowIfNull(log);
			this._log = log;
		}

		public const string RpcSourceName = "Rpc";

		private static readonly LogMessageType ConnectingProxy = new LogMessageType(LogMessageSeverity.Diagnostic, RpcSourceName, (int)RpcMessageId.ConnectingProxy, "Connecting socket to {0} for proxy {1} (syntax={2})", "serviceEP", "proxyType", "abstractSyntax");
		private static readonly LogMessageType BindingProxy = new LogMessageType(LogMessageSeverity.Diagnostic, RpcSourceName, (int)RpcMessageId.BindingProxy, "Binding proxy {0} with syntax {1} using auth context {2} with level {3}.", "proxyType", "abstractSyntax", "userName", "authLevel");
		//private static readonly LogMessageType OversizedPacket = new LogMessageType(LogMessageSeverity.Diagnostic, RpcSourceName, (int)RpcMessageId.OversizedPacket, "The packet must be fragmented: packetSize={0}, frag threshold={1}.", "packetSize", "fragThreshold");

		private void Write(LogMessageType messageType, params object[] parameters)
		{
			this._log.WriteMessage(messageType.Create(parameters));
		}
		public void OnConnectingProxy(ISocket socket, EndPoint serviceEP, RpcClientProxy proxy)
		{
			this.Write(ConnectingProxy, serviceEP, proxy.GetType().FullName, proxy.AbstractSyntaxId);
		}

		public void OnBinding(RpcClientProxy proxy, RpcClientChannel channel, AuthClientContext? authContext, RpcAuthLevel authLevel)
		{
			this.Write(BindingProxy, proxy.GetType().FullName, proxy.AbstractSyntaxId, authContext?.UserName ?? "<none>", authLevel);
		}

		//public void OnPacketTooBig(int pduSize, int fragThreshold)
		//{
		//	this.Write(OversizedPacket, pduSize, fragThreshold);
		//}
	}
}
