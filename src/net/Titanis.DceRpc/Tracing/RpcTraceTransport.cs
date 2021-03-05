using System;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Communication;

namespace Titanis.DceRpc.Tracing
{
	internal class RpcTraceTransport : RpcTransport
	{
		public RpcTraceTransport() : base(8192)
		{
		}

		public override int MajorVersionNumber => 5;

		public override Task SendPduAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}