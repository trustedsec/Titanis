using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Net
{
	/// <summary>
	/// Represents a network socket.
	/// </summary>
	/// <seealso cref="ISocketService.CreateSocket(System.Net.Sockets.AddressFamily, System.Net.Sockets.SocketType, System.Net.Sockets.ProtocolType)"/>
	public interface ISocket : IDisposable, IAsyncDisposable
	{
		/// <summary>
		/// Establishes a connection to a remote host.
		/// </summary>
		/// <param name="remoteEP">Endpoint to connect to</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/> that can be used to cancel the operation</param>
		ValueTask ConnectAsync(EndPoint remoteEP, CancellationToken cancellationToken);

		/// <summary>
		/// Gets the receive timeout, in milliseconds.
		/// </summary>
		/// <value>Timeout in milliseconds, where <c>-1</c> indicates no timeout.</value>
		int ReceiveTimeout { get; set; }
		/// <summary>
		/// Gets the send timeout, in milliseconds.
		/// </summary>
		/// <value>Timeout in milliseconds, where <c>-1</c> indicates no timeout.</value>
		int SendTimeout { get; set; }
		/// <summary>
		/// Disables send/receive operations.
		/// </summary>
		/// <param name="direction">Direction to disable</param>
		void Shutdown(SocketShutdown direction);

		/// <summary>
		/// Receives data from the socket into a buffer.
		/// </summary>
		/// <param name="buffer">Buffer to hold received data</param>
		/// <param name="flags">Flags affecting operation</param>
		/// <returns>Number of bytes received</returns>
		int Receive(Span<byte> buffer, SocketFlags flags);

		/// <summary>
		/// Receives data from the socket into a buffer.
		/// </summary>
		/// <param name="buffer">Buffer to hold received data</param>
		/// <param name="flags">Flags affecting operation</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/> that can be used to cancel the operation</param>
		/// <returns>Number of bytes received</returns>
		ValueTask<int> ReceiveAsync(Memory<byte> buffer, SocketFlags flags, CancellationToken cancellationToken);

		/// <summary>
		/// Sends data to the socket.
		/// </summary>
		/// <param name="data">Data to send</param>
		/// <param name="flags">Flags affecting operation</param>
		/// <returns>Number of bytes sent</returns>
		int Send(ReadOnlySpan<byte> data, SocketFlags flags);

		/// <summary>
		/// Sends data to the socket.
		/// </summary>
		/// <param name="data">Data to send</param>
		/// <param name="flags">Flags affecting operation</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/> that can be used to cancel the operation</param>
		/// <returns>Number of bytes sent</returns>
		ValueTask<int> SendAsync(ReadOnlyMemory<byte> data, SocketFlags flags, CancellationToken cancellationToken);

		/// <summary>
		/// Gets a stream that uses the socket for communications.
		/// </summary>
		/// <param name="transferOwnership"><see langword="true"/> to transfer ownership of this socket to the stream.</param>
		/// <returns>A <see cref="Stream"/> that communicates over the socket</returns>
		Stream GetStream(bool transferOwnership);
	}
}
