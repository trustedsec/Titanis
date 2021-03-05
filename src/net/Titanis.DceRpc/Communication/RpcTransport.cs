using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Security;

namespace Titanis.DceRpc.Communication
{
	/// <summary>
	/// Provides a means to send and receive RPC PDUs.
	/// </summary>
	/// <remarks>
	/// This class adds a layer of abstraction so that connection-oriented and connectionless transports
	/// may be handled similarly by other RPC components.
	/// </remarks>
	public abstract class RpcTransport : Runnable, IDisposable
	{
		protected RpcTransport(int maxReceiveFragmentSize)
		{
			this._maxReceiveFragmentSize = maxReceiveFragmentSize;
		}

		protected RpcChannel? _channel;
		internal void AttachTo(RpcChannel channel)
		{
			if (this._channel != null)
				throw new InvalidOperationException("The transport is already attached to a channel.");

			this._channel = channel;
			this.OnAttachingTo(channel);
		}

		protected RpcChannel EnsureChannel()
		{
			if (this._channel == null)
				throw new InvalidOperationException("The transport is not attached to a channel.");

			return this._channel;
		}
		protected virtual void OnAttachingTo(RpcChannel channel)
		{

		}

		#region MaxReceiveFragmentSize
		private int _maxReceiveFragmentSize;
		public int MaxReceiveFragmentSize
		{
			get => _maxReceiveFragmentSize;
			set
			{
				value = this.OnMaxReceiveFragmentSizeChanging(value);
				_maxReceiveFragmentSize = value;
			}
		}

		protected virtual int OnMaxReceiveFragmentSizeChanging(int value)
		{
			return Math.Min(this._maxReceiveFragmentSize, value);
		}
		#endregion

		/// <summary>
		/// Gets the major version number of the transport.
		/// </summary>
		public abstract int MajorVersionNumber { get; }

		/// <summary>
		/// Sends a PDU.
		/// </summary>
		/// <param name="buffer">Buffer to contain PDU</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>Number of bytes received into <paramref name="buffer"/></returns>
		public abstract Task SendPduAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken);

		/// <summary>
		/// Gets a value indicating whether the transport supporst transceiving.
		/// </summary>
		public virtual bool SupportsTransceive => false;
		/// <summary>
		/// Sends and PDU and receives the reply.
		/// </summary>
		/// <param name="pduToSend">PDU to send</param>
		/// <param name="bubbleCancel">Indicates whether to bubble <see cref="OperationCanceledException"/></param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>Number of bytes received into <paramref name="receiveBuffer"/></returns>
		/// <remarks>
		/// The implementation must ensure that a complete PDU is delivered.
		/// In the event that the transceive operation returns a fragment,
		/// the implementation must continue to read until a complete PDU is received
		/// and processed.
		/// </remarks>
		public virtual Task TransceivePduAsync(
			ReadOnlyMemory<byte> pduToSend,
			bool bubbleCancel,
			CancellationToken cancellationToken)
			=> throw new NotSupportedException("The transport does not support transceiving.");

		/// <summary>
		/// Gets securet channel information, if implemented.
		/// </summary>
		/// <returns>A <see cref="ISecureChannel"/> describing the secure channel, if implemented</returns>
		public virtual ISecureChannel? TryGetSecureChannelInfo() => null;

		#region Dispose pattern
		private bool disposedValue;

		protected void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					this.OnDisposing(disposing);
				}

				disposedValue = true;
			}
		}

		protected virtual void OnDisposing(bool disposing)
		{
		}

		/// <inheritdoc/>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
