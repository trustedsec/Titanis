using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Communication;
using Titanis.IO;
using Titanis.Net;
using Titanis.Security;

[assembly: InternalsVisibleTo("Titanis.DceRpc.Epm")]

namespace Titanis.DceRpc.Client
{
	/// <summary>
	/// Implements the client side of the RPC runtime.
	/// </summary>
	public class RpcClient
	{
		public RpcClient(
			ISocketService? socketService = null,
			IRpcCallback? callback = null
			)
		{
			this._socketService = socketService ?? Singleton.SingleInstance<PlatformSocketService>();
			this._callback = callback;
		}

		private readonly ISocketService _socketService;
		private readonly IRpcCallback? _callback;

		/// <summary>
		/// Gets or sets the connection timeout.
		/// </summary>
		/// <remarks>
		/// The connection timeout determines how long the client waits for a connection to complete.
		/// </remarks>
		public TimeSpan ConnectTimeout { get; set; } = TimeSpan.FromSeconds(10);
		/// <summary>
		/// Gets or sets the default call timeout.
		/// </summary>
		/// <remarks>
		/// The call timeout determines how long the RPC runtime will wait for a call to
		/// return before canceling it.
		/// <para>
		/// When a channel is bound, it sets its own call timeout to the value of this property.
		/// </para>
		/// <para>
		/// The default is 10 seconsd.
		/// </para>
		/// </remarks>
		public TimeSpan DefaultCallTimeout { get; set; } = TimeSpan.FromSeconds(10);

		/// <summary>
		/// Gets or sets a value specifying the default authentication level.
		/// </summary>
		public RpcAuthLevel DefaultAuthLevel { get; set; } = RpcAuthLevel.Connect;
		/// <summary>
		/// Gets a value indicating whether to prefer AUTH3 when possible when binding.
		/// </summary>
		public bool PreferAuth3 { get; set; } = true;

		/// <summary>
		/// Binds to a <see cref="Stream"/> to establish a channel.
		/// </summary>
		/// <param name="stream"><see cref="Stream"/> to bind to</param>
		/// <returns>An <see cref="RpcClientChannel"/> representing the established channel.</returns>
		/// <exception cref="ArgumentNullException"><see cref="Stream"/> is <see langword="null"/></exception>
		public RpcClientChannel BindTo(Stream stream)
		{
			if (stream is null)
				throw new ArgumentNullException(nameof(stream));
			if (!stream.CanRead)
				throw new ArgumentException("The stream does not support reading.", nameof(stream));
			if (!stream.CanWrite)
				throw new ArgumentNullException("The stream does not support writing.", nameof(stream));

			RpcClientChannel channel = new RpcClientChannel(
				this,
				(stream is IAsyncPipeStream) ? new RpcPipeTransport(stream, RpcChannel.WindowsDefaultMaxFragCO)
				: new RpcStreamTransport(stream, RpcChannel.WindowsDefaultMaxFragCO),
				this._callback
				);
			channel.Start();
			return channel;
		}

		/// <summary>
		/// Connects a service client over TCP
		/// </summary>
		/// <typeparam name="TClient">Type of service client</typeparam>
		/// <param name="serviceEP">Remote service endpoint</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <param name="authContext">Authentication context used to authenticate</param>
		/// <returns>A connected instance of <typeparamref name="TClient"/></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public async Task<TClient> ConnectTcp<TClient>(
			EndPoint serviceEP,
			CancellationToken cancellationToken,
			AuthClientContext? authContext = null
			)
			where TClient : RpcServiceClient, new()
		{
			if (serviceEP is null)
				throw new ArgumentNullException(nameof(serviceEP));

			TClient svc = new TClient();
			await this.ConnectTcp(svc, serviceEP, cancellationToken, authContext).ConfigureAwait(false);
			return svc;
		}

		public async Task ConnectTcp(
			RpcServiceClient svc,
			EndPoint serviceEP,
			CancellationToken cancellationToken
			)
		{
			await this.ConnectTcp(svc.Proxy, serviceEP, cancellationToken, null, RpcAuthLevel.None).ConfigureAwait(false);
		}

		public async Task ConnectTcp(
			RpcServiceClient svc,
			EndPoint serviceEP,
			CancellationToken cancellationToken,
			AuthClientContext? authContext = null
			)
		{
			await this.ConnectTcp(svc.Proxy, serviceEP, cancellationToken, authContext, this.DefaultAuthLevel).ConfigureAwait(false);
		}

		public Task ConnectTcp(
			RpcClientProxy proxy,
			EndPoint serviceEP,
			CancellationToken cancellationToken
			)
			=> this.ConnectTcp(proxy, serviceEP, cancellationToken, null, RpcAuthLevel.None);

		public Task ConnectTcp(
			RpcClientProxy proxy,
			EndPoint serviceEP,
			CancellationToken cancellationToken,
			AuthClientContext? authContext
			)
			=> this.ConnectTcp(proxy, serviceEP, cancellationToken, authContext, this.DefaultAuthLevel);

		/// <summary>
		/// Connects a client proxy over TCP.
		/// </summary>
		/// <param name="proxy">Client proxy to connect</param>
		/// <param name="serviceEP">Remote service endpoint</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <param name="authContext">Authentication context used to authenticate</param>
		public async Task ConnectTcp(
			RpcClientProxy proxy,
			EndPoint serviceEP,
			CancellationToken cancellationToken,
			AuthClientContext? authContext,
			RpcAuthLevel authLevel
			)
		{
			ISocket? socket = null;
			try
			{
				socket = this._socketService.CreateTcpSocket(serviceEP.AddressFamilyOrDefault(AddressFamily.InterNetwork));

				CancellationTokenSource connectCancel = new CancellationTokenSource(this.ConnectTimeout);
				var cancelReg = cancellationToken.Register(() => connectCancel.Cancel());
				try
				{
					this._callback?.OnConnectingProxy(socket, serviceEP, proxy);
					await socket.ConnectAsync(serviceEP, connectCancel.Token).ConfigureAwait(false);
				}
				catch (OperationCanceledException ex)
				{
					if (connectCancel.IsCancellationRequested)
						throw new TimeoutException("The connection attempt timed out.");
					else
						throw;
				}
				finally
				{
					cancelReg.Unregister();
				}

				Stream? stream = null;
				try
				{
					stream = socket.GetStream(true);
					socket = null;
					RpcClientChannel? channel = null;
					try
					{
						channel = this.BindTo(stream);
						stream = null;

						this._callback?.OnBinding(proxy, channel, authContext, authLevel);
						await proxy.BindToAsync(channel, true, authContext, authLevel, null, cancellationToken).ConfigureAwait(false);
						channel = null;
					}
					finally
					{
						channel?.Dispose();
					}
				}
				finally
				{
					stream?.Dispose();
				}
			}
			finally
			{
				socket?.Dispose();
			}
		}
	}
}

