using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Titanis.Net;

namespace Titanis.Socks
{
	/// <summary>
	/// Implements <see cref="ISocket"/> over a SOCKS connection.
	/// </summary>
	/// <seealso cref="Socks5Client"/>
	public partial class SocksSocket : ISocket
	{
		internal SocksSocket(Socks5Client socksClient, in SocketInfo socketInfo)
		{
			this._socksClient = socksClient;
			this._socketInfo = socketInfo;
		}

		private Socks5Client _socksClient;
		private readonly SocketInfo _socketInfo;
		private ISocket? _innerSocket;
		private EndPoint? _remoteBindEP;

		/// <inheritdoc/>
		public int ReceiveTimeout { get; set; }
		/// <inheritdoc/>
		public int SendTimeout { get; set; }

		enum SocksState
		{
			New = 0,
			Connected,
		}
		private SocksState _state;
		private ISocket VerifyConnected()
		{
			var socket = this._innerSocket;
			if (socket == null || this._state != SocksState.Connected)
				throw new InvalidOperationException("The socket cannot perform this operation because it is not connected.");

			return socket;
		}

		private void VerifyNotConnected()
		{
			if (this._state != SocksState.New)
				throw new InvalidOperationException("The socket cannot perform this operation because it is already connected.");
		}

		/// <inheritdoc/>
		public async ValueTask ConnectAsync(
			EndPoint remoteEP,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(remoteEP);
			this.VerifyNotConnected();

			(this._innerSocket, this._remoteBindEP) = await _socksClient.ConnectSocksAsync(_socketInfo, remoteEP, cancellationToken).ConfigureAwait(false);
			this._state = SocksState.Connected;
		}

		/// <inheritdoc/>
		public async ValueTask DisposeAsync()
		{
			if (this._innerSocket != null)
			{
				await this._innerSocket.DisposeAsync().ConfigureAwait(false);
			}
			this._state = SocksState.New;
		}

		private Stream? _stream;
		/// <inheritdoc/>
		public Stream GetStream(bool transferOwnership)
		{
			var socket = this.VerifyConnected();
			return this._stream ??= socket.GetStream(transferOwnership);
		}

		/// <inheritdoc/>
		public int Receive(Span<byte> buffer, SocketFlags flags)
		{
			var socket = this.VerifyConnected();
			return socket.Receive(buffer, flags);
		}

		public ValueTask<int> ReceiveAsync(Memory<byte> buffer, SocketFlags flags, CancellationToken cancellationToken)
		{
			var socket = this.VerifyConnected();
			return socket.ReceiveAsync(buffer, flags, cancellationToken);
		}

		/// <inheritdoc/>
		public int Send(ReadOnlySpan<byte> data, SocketFlags flags)
		{
			var socket = this.VerifyConnected();
			return socket.Send(data, flags);
		}

		/// <inheritdoc/>
		public ValueTask<int> SendAsync(ReadOnlyMemory<byte> data, SocketFlags flags, CancellationToken cancellationToken)
		{
			var socket = this.VerifyConnected();
			return socket.SendAsync(data, flags, cancellationToken);
		}

		public void Shutdown(SocketShutdown direction)
		{
			var socket = this.VerifyConnected();
			socket.Shutdown(direction);
			this._state = SocksState.New;
		}
	}
	public partial class SocksSocket : ISocket
	{

		private bool disposedValue;
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}