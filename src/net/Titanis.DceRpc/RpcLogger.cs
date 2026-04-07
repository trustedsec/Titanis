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
	[CallbackLogger]
	public class RpcLogger : IRpcCallback
	{
		private readonly ILog _log;
		private readonly IRpcCallback? _chainedCallback;

		public RpcLogger(ILog log, IRpcCallback? chainedCallback = null)
		{
			ArgumentNullException.ThrowIfNull(log);
			this._log = log;
			this._chainedCallback = chainedCallback;
		}

		//private static readonly LogMessageType OversizedPacket = new LogMessageType(LogMessageSeverity.Diagnostic, RpcSourceName, (int)RpcMessageId.OversizedPacket, "The packet must be fragmented: packetSize={0}, frag threshold={1}.", "packetSize", "fragThreshold");

		void IRpcCallback.OnConnectingProxy(ISocket socket, EndPoint serviceEP, RpcClientProxy proxy)
		{
			this._log.WriteDceRpcClientConnectingProxyMessage(serviceEP, proxy.GetType().FullName, proxy.AbstractSyntaxId);

			this._chainedCallback?.OnConnectingProxy(socket, serviceEP, proxy);
		}

		void IRpcCallback.OnBindingProxy(RpcClientProxy proxy, RpcClientChannel channel, AuthClientContext? authContext, RpcAuthLevel authLevel)
		{
			this._log.WriteDceRpcClientBindingProxyMessage(proxy.GetType().FullName, proxy.AbstractSyntaxId, authContext?.UserName ?? "<none>", authLevel);

			this._chainedCallback?.OnBindingProxy(proxy, channel, authContext, authLevel);
		}

		//public void OnPacketTooBig(int pduSize, int fragThreshold)
		//{
		//	this.Write(OversizedPacket, pduSize, fragThreshold);
		//}
	}
}
