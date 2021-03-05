using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
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
