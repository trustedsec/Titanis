using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Communication;
using Titanis.DceRpc.WireProtocol;

namespace Titanis.DceRpc.Tracing
{
	public class RpcTraceChannel : RpcChannel
	{
		public RpcTraceChannel() : base(new RpcTraceTransport(), Timeout.InfiniteTimeSpan, null)
		{
		}

		internal sealed override void OnTransportAborted(Exception? exception)
		{
			// Do nothing
		}
	}
}
