using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
		public PlatformSocketService(INameResolverService? resolver, ILog? log)
		{
			this._resolver = resolver;
			this._log = log;
		}

		internal readonly INameResolverService? _resolver;
		private readonly ILog? _log;

		public PlatformSocket CreateSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			var socket = new Socket(addressFamily, socketType, protocolType);
			return new PlatformSocket(socket, addressFamily, this._resolver);
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
		private readonly AddressFamily _addrFamily;
		private readonly INameResolverService? _resolver;

		internal PlatformSocket(Socket socket, AddressFamily addrFamily, INameResolverService? resolver)
		{
			Debug.Assert(socket is not null);
			this._socket = socket;
			this._addrFamily = addrFamily;
			this._resolver = resolver;
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
		public async ValueTask ConnectAsync(EndPoint remoteEP, CancellationToken cancellationToken)
		{
			if (remoteEP is DnsEndPoint dnsep && this._resolver is not null)
			{
				var hostEntry = await _resolver.ResolveAsync(dnsep.Host, cancellationToken).ConfigureAwait(false);
				var addr = hostEntry.FirstOrDefault(r => r.AddressFamily == this._addrFamily);
				if (addr is null)
					throw new ArgumentException($"Unable to resolve {dnsep.Host} to an address with family {this._addrFamily}");

				remoteEP = new IPEndPoint(addr, dnsep.Port);
			}
			await _socket.ConnectAsync(remoteEP, cancellationToken).ConfigureAwait(false);
		}

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
