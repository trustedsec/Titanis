using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.DceRpc.Server
{
	public class TcpEndpoint : RpcEndpoint
	{
		public TcpEndpoint(IPEndPoint bindEP)
		{
			ArgumentNullException.ThrowIfNull(bindEP);
			BindEndpoint = bindEP;
		}

		public IPEndPoint BindEndpoint { get; }

		private Socket? _socket;
		protected override Task OnStarting(CancellationToken cancellationToken)
		{
			if (!this.IsBound)
				throw new InvalidOperationException($"The endpoint is not bound.  It must be bound before it is started.");

			var socket = new Socket(this.BindEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				socket.Bind(this.BindEndpoint);
				socket.Listen();
				this._socket = socket;
				socket = null;

				return Task.CompletedTask;
			}
			finally
			{
				socket?.Dispose();
			}
		}

		protected override async Task Run(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				var client = await _socket.AcceptAsync(cancellationToken).ConfigureAwait(false);
				this.owner.OnClientConnected(client);
			}
		}
	}
}
