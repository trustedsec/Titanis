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
	[Callback]
	public interface IRpcCallback
	{
		void OnBindingProxy(RpcClientProxy proxy, RpcClientChannel channel, AuthClientContext? authContext, RpcAuthLevel authLevel);
		void OnConnectingProxy(ISocket socket, EndPoint serviceEP, RpcClientProxy proxy);
	}
}
