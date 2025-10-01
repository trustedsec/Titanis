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
		/// <summary>
		/// Initializes a new <see cref="RpcClient"/>.
		/// </summary>
		/// <param name="socketService"></param>
		/// <param name="callback"></param>
		public RpcClient(
			ISocketService? socketService = null,
			IClientCredentialService? credentialService = null,
			INameResolverService? resolver = null,
			ILog? log = null,
			IRpcCallback? callback = null
			)
		{
			this._socketService = socketService ?? new PlatformSocketService(null, log);
			this._credentialService = credentialService;
			this._resolver = resolver;
			this._log = log;
			this._callback = callback;

			this.DefaultAuthLevel = (credentialService is null) ? RpcAuthLevel.None : RpcAuthLevel.Connect;
		}

		internal readonly ISocketService _socketService;
		internal readonly IClientCredentialService? _credentialService;
		internal readonly INameResolverService? _resolver;
		internal readonly ILog? _log;
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
		public RpcAuthLevel DefaultAuthLevel { get; set; }
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
		/// <returns>A connected instance of <typeparamref name="TClient"/></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public async Task<TClient> ConnectTcp<TClient>(
			EndPoint serviceEP,
			ServicePrincipalName? spn,
			CancellationToken cancellationToken
			)
			where TClient : RpcServiceClient, new()
		{
			if (serviceEP is null)
				throw new ArgumentNullException(nameof(serviceEP));

			TClient svc = new TClient();
			await this.ConnectTcp(svc, serviceEP, spn, this.DefaultAuthLevel, cancellationToken).ConfigureAwait(false);
			return svc;
		}

		/// <summary>
		/// Connects a service client over TCP
		/// </summary>
		/// <typeparam name="TClient">Type of service client</typeparam>
		/// <param name="serviceEP">Remote service endpoint</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A connected instance of <typeparamref name="TClient"/></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public async Task<TClient> ConnectTcp<TClient>(
			EndPoint serviceEP,
			ServicePrincipalName? spn,
			RpcAuthLevel authLevel,
			CancellationToken cancellationToken
			)
			where TClient : RpcServiceClient, new()
		{
			if (serviceEP is null)
				throw new ArgumentNullException(nameof(serviceEP));

			TClient svc = new TClient();
			await this.ConnectTcp(svc.Proxy, serviceEP, spn, authLevel, cancellationToken).ConfigureAwait(false);
			return svc;
		}

		public async Task ConnectTcp(
			RpcServiceClient svc,
			EndPoint serviceEP,
			ServicePrincipalName? spn,
			CancellationToken cancellationToken
			)
		{
			await this.ConnectTcp(svc, serviceEP, spn, this.DefaultAuthLevel, cancellationToken).ConfigureAwait(false);
		}

		public async Task ConnectTcp(
			RpcServiceClient svc,
			EndPoint serviceEP,
			ServicePrincipalName? spn,
			RpcAuthLevel authLevel,
			CancellationToken cancellationToken
			)
		{
			if (spn is null && serviceEP.TryGetHostAndPort(out var host, out var port))
			{
				if (host is not null)
					spn = new ServicePrincipalName(svc.ServiceClass ?? ServiceClassNames.RestrictedKrbHost, host);
			}
			await this.ConnectTcp(svc.Proxy, serviceEP, spn, authLevel, cancellationToken).ConfigureAwait(false);
		}

		public Task ConnectTcp(
			RpcClientProxy proxy,
			EndPoint serviceEP,
			ServicePrincipalName? spn,
			CancellationToken cancellationToken
			)
			=> this.ConnectTcp(proxy, serviceEP, spn, this.DefaultAuthLevel, cancellationToken);

		/// <summary>
		/// Connects a client proxy over TCP.
		/// </summary>
		/// <param name="proxy">Client proxy to connect</param>
		/// <param name="serviceEP">Remote service endpoint</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public async Task ConnectTcp(
			RpcClientProxy proxy,
			EndPoint serviceEP,
			ServicePrincipalName? spn,
			RpcAuthLevel authLevel,
			CancellationToken cancellationToken
			)
		{
			ArgumentNullException.ThrowIfNull(serviceEP);

			if (spn is null && serviceEP.TryGetHostAndPort(out var host, out var port))
			{
				if (host is not null)
					spn = new ServicePrincipalName(ServiceClassNames.RestrictedKrbHost, host);
			}

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
					await BindProxyToStream(proxy, spn, authLevel, stream, cancellationToken).ConfigureAwait(false);
					stream = null;
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

		/// <summary>
		/// Binds a proxy to a stream
		/// </summary>
		/// <param name="proxy">The proxy to bind</param>
		/// <param name="spn"><see cref="ServicePrincipalName"/> for authentication</param>
		/// <param name="authLevel"><see cref="RpcAuthLevel"/> to negotiate</param>
		/// <param name="stream"><see cref="Stream"/> to bind to</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <remarks>
		/// <paramref name="spn"/> is required unless <paramref name="authLevel"/> is <see cref="RpcAuthLevel.None"/>.
		/// </remarks>
		public async Task BindProxyToStream(
			RpcClientProxy proxy,
			ServicePrincipalName? spn,
			RpcAuthLevel authLevel,
			Stream? stream,
			CancellationToken cancellationToken
			)
		{
			ArgumentNullException.ThrowIfNull(proxy);

			if (authLevel is RpcAuthLevel.ConfiguredDefault)
				authLevel = this.DefaultAuthLevel;
			if (authLevel is RpcAuthLevel.ConfiguredDefault)
				authLevel = RpcAuthLevel.Connect;

			if (spn is null && authLevel > RpcAuthLevel.None)
				throw new ArgumentException($"{nameof(authLevel)} requires authentication, but no SPN provided to get credentials.");

			RpcClientChannel? channel = null;
			try
			{
				channel = this.BindTo(stream);

				AuthClientContext? authContext;
				if (authLevel is RpcAuthLevel.None)
					authContext = null;
				else
				{
					var credService = this._credentialService;
					if (credService == null)
						throw new ArgumentException($"Cannot use authentication level {authLevel} because the RpcClient is not configured with {nameof(IClientCredentialService)}.");

					SecurityCapabilities caps = SecurityCapabilities.DceStyle;
					if (authLevel is > RpcAuthLevel.Connect and < RpcAuthLevel.PacketIntegrity)
						authLevel = RpcAuthLevel.PacketIntegrity;
					if (authLevel >= RpcAuthLevel.PacketIntegrity)
						caps |= SecurityCapabilities.Integrity;
					if (authLevel >= RpcAuthLevel.PacketPrivacy)
						caps |= SecurityCapabilities.Confidentiality;

					if (spn is not null)
					{
						authContext = this._credentialService?.GetAuthContextForService(spn, caps, AuthOptions.PreferSpnego);
						if (authContext is null)
							authContext = this._credentialService?.GetAuthContextForService(spn.WithServiceClass(ServiceClassNames.HostU), caps, AuthOptions.PreferSpnego);
					}
					else
						authContext = null;

					if (authContext is null)
						throw new InvalidOperationException($"Cannot bind proxy {proxy.GetType().FullName} because no authentication context is available and authentication level is set to {authLevel}.");
				}

				this._callback?.OnBindingProxy(proxy, channel, authContext, authLevel);
				await proxy.BindToAsync(channel, true, authContext, authLevel, null, cancellationToken).ConfigureAwait(false);
				channel = null;
			}
			finally
			{
				channel?.Dispose();
			}
		}
	}

	public static class ServiceExtensions
	{
		public static RpcClient CreateRpcClient(this IServiceProvider services)
		{
			IRpcCallback? callback = services.GetService<IRpcCallback>();
			if (callback is null)
			{
				var log = services.GetService<ILog>();
				if (log is not null)
					callback = new RpcLogger(log);
			}
			var rpcClient = new RpcClient(
				services.GetService<ISocketService>(),
				services.GetService<IClientCredentialService>(),
				services.GetService<INameResolverService>(),
				services.GetService<ILog>(),
				callback);
			return rpcClient;
		}
	}
}

