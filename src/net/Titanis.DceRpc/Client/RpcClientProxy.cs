using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Communication;
using Titanis.Security;

namespace Titanis.DceRpc.Client
{
	/// <summary>
	/// Implements a client-side proxy.
	/// </summary>
	/// <remarks>
	/// This client-side proxy is responsible for negotiating the binding with the remote party
	/// as well as facilitating the construction and transmission of PDUs for call operations.
	/// </remarks>
	public abstract class RpcClientProxy : IDisposable, ISecureChannel
	{
		/// <summary>
		/// Gets the UUID of the RPC interface.
		/// </summary>
		/// <seealso cref="AbstractSyntaxId"/>
		public abstract Guid InterfaceUuid { get; }
		/// <summary>
		/// Gets the version of the RPC interface.
		/// </summary>
		/// <seealso cref="AbstractSyntaxId"/>
		public abstract RpcVersion InterfaceVersion { get; }
		/// <summary>
		/// Gets the abstract syntax ID.
		/// </summary>
		/// <seealso cref="InterfaceUuid"/>
		/// <seealso cref="InterfaceVersion"/>
		public RpcInterfaceId AbstractSyntaxId => new RpcInterfaceId(this.InterfaceUuid, this.InterfaceVersion);

		private bool _ownsChannel;
		private protected RpcClientChannel? _channel;
		/// <summary>
		/// Gets the underlying channel.
		/// </summary>
		/// <remarks>
		/// The underlying channel is set by <see cref="BindToAsync(RpcClientChannel, bool, AuthClientContext?, CancellationToken)"/>
		/// </remarks>
		public RpcClientChannel? Channel => this._channel;
		/// <summary>
		/// Gets the secure channel the client proxy communicates over, if any.
		/// </summary>
		public ISecureChannel? SecureChannel => this;


		private protected RpcBindContext? _bindContext;
		public BindAuthContext? BoundAuthContext => this._bindContext?.authContext;
		/// <summary>
		/// Gets the underlying <see cref="RpcEncoding"/>.
		/// </summary>
		/// <remarks>
		/// This property is set when the object is bound in <see cref="BindToAsync(RpcClientChannel, bool, AuthClientContext?, CancellationToken)"/>.
		/// </remarks>
		public RpcEncoding? Encoding => this._bindContext?.encoding;

		/// <summary>
		/// Gets or sets the UUID to use for the RPC interface during binding.
		/// </summary>
		/// <remarks>
		/// Normally, the <see cref="InterfaceUuid"/> and <see cref="InterfaceVersion"/> supplied by the derived class
		/// are used for binding.  This property allows the caller to override this behavior for whatever reason.
		/// </remarks>
		public Guid? OverrideInterfaceUuid { get; set; }
		/// <summary>
		/// Gets or sets the interface version to use during binding.
		/// </summary>
		/// <remarks>
		/// Normally, the <see cref="InterfaceUuid"/> and <see cref="InterfaceVersion"/> supplied by the derived class
		/// are used for binding.  This property allows the caller to override this behavior for whatever reason.
		/// </remarks>
		public RpcVersion? OverrideInterfaceVersion { get; set; }

		/// <summary>
		/// Binds the proxy.
		/// </summary>
		/// <param name="channel">Underlying channel to bind</param>
		/// <param name="ownsChannel"><see langword="true"/> for this proxy to take ownership of the channel; otherwise, <see langword="false"/></param>
		/// <param name="authContext"><see cref="AuthClientContext"/> for authentication</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <exception cref="ArgumentNullException"><paramref name="channel"/> is <see langword="null"/>.</exception>
		public Task BindToAsync(
			RpcClientChannel channel,
			bool ownsChannel,
			AuthClientContext? authContext,
			CancellationToken cancellationToken)
			=> this.BindToAsync(channel, ownsChannel, authContext, channel.Client.DefaultAuthLevel, null, cancellationToken);
		/// <summary>
		/// Binds the proxy.
		/// </summary>
		/// <param name="channel">Underlying channel to bind</param>
		/// <param name="ownsChannel"><see langword="true"/> for this proxy to take ownership of the channel; otherwise, <see langword="false"/></param>
		/// <param name="authContext"><see cref="AuthClientContext"/> for authentication</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <exception cref="ArgumentNullException"><paramref name="channel"/> is <see langword="null"/>.</exception>
		internal async Task BindToAsync(
			RpcClientChannel channel,
			bool ownsChannel,
			AuthClientContext? authContext,
			RpcAuthLevel authLevel,
			RpcAssocGroup? assocGroup,
			CancellationToken cancellationToken)
		{
			if (channel == null)
				throw new ArgumentNullException(nameof(channel));

			this._bindContext = await channel.Bind(
				this.OverrideInterfaceUuid ?? this.InterfaceUuid,
				this.OverrideInterfaceVersion ?? this.InterfaceVersion,
				authContext,
				authLevel,
				assocGroup,
				cancellationToken).ConfigureAwait(false);
			this._channel = channel;
			this._ownsChannel = ownsChannel;

			var bindAuthContext = this._bindContext.authContext;
			if (bindAuthContext != null)
			{
				if (bindAuthContext.AuthLevel > RpcAuthLevel.None)
				{
					AuthLevel authLevel2;
					switch (bindAuthContext.AuthLevel)
					{
						case RpcAuthLevel.Connect:
							authLevel2 = AuthLevel.Authenticated;
							break;
						// TODO: What do these values actually map to?
						case RpcAuthLevel.Call:
						case RpcAuthLevel.Packet:
						case RpcAuthLevel.PacketIntegrity:
							authLevel2 = AuthLevel.Integrity;
							break;
						case RpcAuthLevel.PacketPrivacy:
							authLevel2 = AuthLevel.Privacy;
							break;
						default:
							authLevel2 = AuthLevel.None;
							break;
					}
					this._authLevel = authLevel2;
					this._sessionKey = authContext.GetSessionKey().ToArray();
				}

				//if (bindAuthContext.authLevel < RpcAuthLevel.PacketIntegrity)
					this._shouldSendPContext = true;
			}
			else
			{
				var secureChannel = this.Channel.TryGetSecureChannelInfo();
				if (secureChannel != null)
				{
					this._authLevel = secureChannel.AuthLevel;
					if (secureChannel.HasSessionKey)
						this._sessionKey = secureChannel.GetSessionKey();
				}
			}
		}

		/// <summary>
		/// Ensures the proxy is bound.
		/// </summary>
		/// <exception cref="InvalidOperationException">The proxy is not bound.</exception>
		private protected void EnsureBound()
		{
			if (this._bindContext == null)
				throw new InvalidOperationException(Messages.RpcClient_ProxyNotBound);
		}
		protected virtual RpcRequestBuilder CreateRequest(ushort opnum)
		{
			this.EnsureBound();
			return new RpcRequestBuilder(opnum, this._bindContext.encoding, new RpcCallContext(null));
		}

		// TODO: Use switch to determine whether to write verificationTrailer
		private bool _shouldSendPContext;

		// [MS-RPCE] § 
		private protected bool GetPContextFlag()
		{
			bool includePContext = this._shouldSendPContext;
			this._shouldSendPContext = false;
			return includePContext;
		}

		protected RpcDecoder SendRequest(IRpcRequestBuilder stubData, CancellationToken cancellationToken)
		{
			this.EnsureBound();
			return this._channel.SendRequestAsync(
				(RpcRequestBuilder)stubData,
				this._bindContext,
				this.GetPContextFlag(),
				cancellationToken
				).Result;
		}

		protected virtual async Task<RpcDecoder> SendRequestAsync(
			IRpcRequestBuilder stubData,
			CancellationToken cancellationToken)
		{
			this.EnsureBound();
			return await this._channel.SendRequestAsync(
				(RpcRequestBuilder)stubData,
				this._bindContext,
				this.GetPContextFlag(),
				cancellationToken
				).ConfigureAwait(false);
		}

		#region Dispose pattern
		private bool _isDisposed;

		protected virtual void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				if (disposing)
				{
					if (this._ownsChannel)
						this._channel.Dispose();
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				_isDisposed = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		#endregion

		#region ISecureChannel
		/// <inheritdoc/>
		private AuthLevel _authLevel;
		AuthLevel ISecureChannel.AuthLevel => this._authLevel;

		private byte[]? _sessionKey;

		/// <inheritdoc/>
		bool ISecureChannel.HasSessionKey => (this._sessionKey != null);
		/// <inheritdoc/>
		byte[]? ISecureChannel.GetSessionKey()
			=> this._sessionKey;
		#endregion
	}
}
