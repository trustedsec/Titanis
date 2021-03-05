using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Net
{
	/// <summary>
	/// Implements <see cref="ISocketService"/> using <see cref="Socket"/>.
	/// </summary>
	/// <remarks>
	/// <see cref="CreateSocket(AddressFamily, SocketType, ProtocolType)"/> creates <see cref="PlatformSocket"/>
	/// objects that wrap <see cref="Socket"/> objects.
	/// </remarks>
	public class PlatformSocketService : ISocketService
	{
		public PlatformSocket CreateSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			var socket = new Socket(addressFamily, socketType, protocolType);
			return new PlatformSocket(socket);
		}
		ISocket ISocketService.CreateSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
			=> this.CreateSocket(addressFamily, socketType, protocolType);
	}
	/// <summary>
	/// Implements <see cref="ISocket"/> on a <see cref="Socket"/>.
	/// </summary>
	public class PlatformSocket : ISocket
	{
		private readonly Socket _socket;

		internal PlatformSocket(Socket socket)
		{
			Debug.Assert(socket is not null);
			this._socket = socket;
		}

		/// <inheritdoc/>
		public int ReceiveTimeout
		{
			get
			{
				int timeout = (int)this._socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout)!;
				// For sockets, 0 == never, so map to -1
				return (timeout == 0) ? -1 : timeout;
			}
			set
			{
				if (value == 0)
					throw new NotSupportedException("Socket does not support timeout == 0.");

				if (value == -1)
					value = 0;

				this._socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, value);
			}
		}

		/// <inheritdoc/>
		public int SendTimeout
		{
			get
			{
				int timeout = (int)this._socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout)!;
				// For sockets, 0 == never, so map to -1
				return (timeout == 0) ? -1 : timeout;
			}
			set
			{
				if (value == 0)
					throw new NotSupportedException("Socket does not support timeout == 0.");

				if (value == -1)
					value = 0;

				this._socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, value);
			}
		}

		/// <inheritdoc/>
		public ValueTask ConnectAsync(EndPoint remoteEP, CancellationToken cancellationToken)
			=> this._socket.ConnectAsync(remoteEP, cancellationToken);

		/// <inheritdoc/>
		public int Receive(Span<byte> buffer, SocketFlags flags)
			=> this._socket.Receive(buffer, flags);

		/// <inheritdoc/>
		public int Send(ReadOnlySpan<byte> data, SocketFlags flags)
			=> this._socket.Send(data, flags);

		/// <inheritdoc/>
		public void Dispose()
		{
			((IDisposable)this._socket).Dispose();
		}

		public ValueTask DisposeAsync()
		{
			this.Dispose();
			return new ValueTask();
		}

		/// <inheritdoc/>
		public void Shutdown(SocketShutdown direction)
			=> this._socket.Shutdown(direction);

		/// <inheritdoc/>
		public ValueTask<int> ReceiveAsync(Memory<byte> buffer, SocketFlags flags, CancellationToken cancellationToken)
			=> this._socket.ReceiveAsync(buffer, flags, cancellationToken);

		/// <inheritdoc/>
		public ValueTask<int> SendAsync(ReadOnlyMemory<byte> data, SocketFlags flags, CancellationToken cancellationToken)
			=> this._socket.SendAsync(data, flags, cancellationToken);

		private Stream? _stream;
		/// <inheritdoc/>
		public Stream GetStream(bool transferOwnership)
		{
			if (transferOwnership)
				return this._stream ??= new NetworkStream(this._socket, true);
			else
				return new NetworkStream(this._socket, false);
		}
	}
}
