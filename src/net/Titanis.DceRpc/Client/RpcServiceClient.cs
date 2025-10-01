using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Net;
using Titanis.Security;

namespace Titanis.DceRpc.Client
{
	/// <summary>
	/// Implements a client of an RPC service.
	/// </summary>
	/// <remarks>
	/// This object wraps a <see cref="RpcClientProxy"/> that sends the requests to the RPC service.
	/// </remarks>
	public abstract class RpcServiceClient : IDisposable
	{
		/// <summary>
		/// Gets the underlying <see cref="RpcClientProxy"/>.
		/// </summary>
		public abstract RpcClientProxy Proxy { get; }
		public bool IsBound { get; private set; }

		public RpcInterfaceId AbstractSyntaxId => this.Proxy.AbstractSyntaxId;

		/// <summary>
		/// Initializes a new <see cref="RpcServiceClient"/>.
		/// </summary>
		protected RpcServiceClient()
		{
		}

		#region Connection parameters
		/// <summary>
		/// Gets the name of the service class used by this client.
		/// </summary>
		public virtual string? ServiceClass => null;
		//public abstract string? ServiceClass { get; }

		/// <summary>
		/// Gets the name of the pipe to connect to over SMB.
		/// </summary>
		/// <remarks>if this service does not support named pipes, this property returns <see langword="null"/>.</remarks>
		public virtual string? WellKnownPipeName => null;

		/// <summary>
		/// Gets a value indicating whether the protocol supports dynamic TCP endpoints.
		/// </summary>
		public virtual bool SupportsDynamicTcp => false;

		/// <summary>
		/// Gets the well-known TCP port.
		/// </summary>
		/// <remarks>If the service doesn't have a well-known TCP port, this property returns <c>0</c>.</remarks>
		public virtual int WellKnownTcpPort => 0;

		/// <summary>
		/// Gets a value indicating whether to negotiate NDR64 for this protocol.
		/// </summary>
		public virtual bool SupportsNdr64 => false;
		/// <summary>
		/// Gets a value indicating whether to renegotiate authentication over a named pipe.
		/// </summary>
		public virtual bool SupportsReauthOverNamedPipes => false;
		/// <summary>
		/// Gets a value indicating whether TCP connections must be encrypted.
		/// </summary>
		public virtual bool RequiresEncryptionOverTcp => false;

		/// <summary>
		/// Gets the <see cref="ServicePrincipalName"/> for a specified endpoint.
		/// </summary>
		/// <param name="ep">Endpoint</param>
		/// <returns><see cref="ServicePrincipalName"/> to use for <paramref name="ep"/></returns>
		public ServicePrincipalName? GetSpnFor(EndPoint ep)
		{
			ArgumentNullException.ThrowIfNull(ep);

			if (ep.TryGetHostAndPort(out var host, out var port))
			{
				if (!string.IsNullOrEmpty(host))
					return GetSpnFor(host);
			}

			return null;
		}

		/// <summary>
		/// Gets the <see cref="ServicePrincipalName"/> for a specified host.
		/// </summary>
		/// <param name="host">Host name</param>
		/// <returns><see cref="ServicePrincipalName"/> to use for <paramref name="host"/></returns>
		public ServicePrincipalName GetSpnFor(string host)
		{
			ArgumentException.ThrowIfNullOrEmpty(host);
			return new ServicePrincipalName(this.ServiceClass ?? ServiceClassNames.RestrictedKrbHost, host);
		}
		#endregion

		/// <summary>
		/// Binds this object to a <see cref="RpcClientChannel"/>.
		/// </summary>
		/// <param name="channel"><see cref="RpcClient"/> to bind to</param>
		/// <param name="ownsChannel"><see langword="true"/> to transfer ownership of <paramref name="channel"/> to this object</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="channel"/> is <see langword="null"/></exception>
		/// <exception cref="InvalidOperationException">The object is already bound.</exception>
		public Task BindToAsync(
			RpcClientChannel channel,
			bool ownsChannel,
			CancellationToken cancellationToken
			)
			=> this.BindToAsync(channel, ownsChannel, null, RpcAuthLevel.Call, cancellationToken);

		/// <summary>
		/// Binds this object to a <see cref="RpcClientChannel"/>.
		/// </summary>
		/// <param name="channel"><see cref="RpcClient"/> to bind to</param>
		/// <param name="ownsChannel"><see langword="true"/> to transfer ownership of <paramref name="channel"/> to this object</param>
		/// <param name="authContext"><see cref="AuthClientContext"/> to use for binding</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="channel"/> is <see langword="null"/></exception>
		/// <exception cref="InvalidOperationException">The object is already bound.</exception>
		public async Task BindToAsync(
			RpcClientChannel channel,
			bool ownsChannel,
			AuthClientContext? authContext,
			RpcAuthLevel authLevel,
			CancellationToken cancellationToken
			)
		{
			if (channel is null)
				throw new ArgumentNullException(nameof(channel));
			if (this.IsBound)
				throw new InvalidOperationException(Messages.RpcClient_AlreadyConnected);

			RpcClientProxy proxy = this.Proxy;
			await proxy.BindToAsync(channel, ownsChannel, authContext, authLevel, null, cancellationToken).ConfigureAwait(false);

			this.IsBound = true;
		}

		/// <summary>
		/// Converts a string to an <see cref="RpcPointer{T}"/>.
		/// </summary>
		/// <param name="str"><see cref="string"/> to convert</param>
		/// <returns>An <see cref="RpcPointer{T}"/> representing <paramref name="str"/>.</returns>
		public static RpcPointer<string>? StringToPointer(string? str)
		{
			return (str != null) ? new RpcPointer<string>(str) : null;
		}

		#region Dispose pattern
		private bool isDisposed;

		protected void Dispose(bool disposing)
		{
			if (!isDisposed)
			{
				if (disposing)
				{
					this.Proxy.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				isDisposed = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~MsrpcServiceClient()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}

	public abstract class RpcServiceClient<TProxy> : RpcServiceClient
		where TProxy : RpcClientProxy, new()
	{
		protected TProxy _proxy;

		/// <inheritdoc/>
		public sealed override RpcClientProxy Proxy => (this._proxy ??= new TProxy());

		/// <summary>
		/// Initializes a new <see cref="RpcServiceClient"/>.
		/// </summary>
		public RpcServiceClient()
		{
			this._proxy = new TProxy();
		}

		/// <summary>
		/// String representing the local host.
		/// </summary>
		protected const string LocalName = ".";
		/// <summary>
		/// <see cref="RpcPointer{T}"/> representing local host.
		/// </summary>
		protected static readonly RpcPointer<string> LocalNamePtr = new DceRpc.RpcPointer<string>(LocalName);
	}
}
