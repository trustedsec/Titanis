using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.IO
{
	public struct UdpReceiveResult
	{
		public int ReceivedBytes;
		public EndPoint RemoteEndPoint;
		public byte[] Buffer;
	}

	public delegate void UdpPacketAsyncHandler(
		Socket socket,
		in UdpReceiveResult recvResult,
		CancellationToken cancellationToken,
		object context);

	public static class SocketServerExtensions
	{
		public static async Task ServeUdp(
			this Socket socket,
			int bufferSize,
			UdpPacketAsyncHandler handler,
			object context,
			CancellationToken cancellationToken)
		{
			if (socket is null)
				throw new ArgumentNullException(nameof(socket));
			if (handler is null)
				throw new ArgumentNullException(nameof(handler));

			byte[] recvBuffer = new byte[bufferSize];
			TaskCompletionSource<int> cancelTask = new TaskCompletionSource<int>();
			while (!cancellationToken.IsCancellationRequested)
			{
				try
				{
					var recvResult = await socket.ReceiveFromAsync(
						new ArraySegment<byte>(recvBuffer, 0, bufferSize),
						SocketFlags.None,
						new IPEndPoint(IPAddress.Any, 0));
					handler(
						socket,
						new UdpReceiveResult
						{
							ReceivedBytes = recvResult.ReceivedBytes,
							RemoteEndPoint = recvResult.RemoteEndPoint,
							Buffer = recvBuffer
						},
						cancellationToken,
						context);
				}
				catch (ObjectDisposedException)
				{
					break;
				}
				catch
				{
					continue;
				}
			}
		}
	}
}
