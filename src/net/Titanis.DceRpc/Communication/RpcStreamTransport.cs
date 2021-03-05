using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.WireProtocol;
using Titanis.IO;
using Titanis.Security;

namespace Titanis.DceRpc.Communication
{
	/// <summary>
	/// Implements a transport over a <see cref="Stream"/>.
	/// </summary>
	public abstract class RpcStreamTransportBase : RpcTransport
	{
		protected RpcStreamTransportBase(int maxReceiveFragmentSize)
			: base(maxReceiveFragmentSize)
		{
		}

		/// <inheritdoc/>
		public sealed override int MajorVersionNumber => 5;

		/// <summary>
		/// Reads data from the underlying stream.
		/// </summary>
		/// <param name="buffer">Buffer to read into</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>Number of bytes received into the buffer.</returns>
		protected abstract ValueTask<int> ReadFromStreamAsync(
			Memory<byte> buffer,
			CancellationToken cancellationToken);

		protected sealed override async Task Run(CancellationToken cancellationToken)
		{
			if (!this.SupportsTransceive)
			{
				try
				{
					await RunReceiveLoop(cancellationToken).ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					var channel = this.EnsureChannel();
					channel.OnTransportAborted(ex);
				}
				this.Dispose();
			}
		}

		private byte[]? _buffer;
		protected int _cbRecv;
		protected bool _isEos;

		/// <summary>
		/// Ensures the buffer is created.
		/// </summary>
		/// <returns>Receive buffer array</returns>
		protected byte[] EnsureBuffer()
			=> this._buffer ??= new byte[this.MaxReceiveFragmentSize];

		/// <summary>
		/// Runs a receive loop until the end of the stream is reached.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		private async Task RunReceiveLoop(CancellationToken cancellationToken)
		{
			var channel = this.EnsureChannel();
			byte[] buffer = this.EnsureBuffer();
			try
			{
				do
				{
					await this.ReceiveAndDeliverPdu(cancellationToken).ConfigureAwait(false);
				} while (!this._isEos && !cancellationToken.IsCancellationRequested);
			}
			catch (EndOfStreamException ex)
			{

			}
		}

		/// <summary>
		/// Receives and delivers a PDU.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <remarks>
		/// This method doesn't return until either the end of stream is reached or a complete PDU is processed.
		/// </remarks>
		protected async Task ReceiveAndDeliverPdu(CancellationToken cancellationToken)
		{
			var channel = this.EnsureChannel();
			var buffer = this.EnsureBuffer();

			int cbRecv;
			while ((cbRecv = await this.ReceivePduAsync(buffer, this._cbRecv, cancellationToken).ConfigureAwait(false)) > 0)
			{
				ushort fragLength = RpcPduReader.ReadFragLength(buffer);

				// TODO: Track outstanding PDUs
				bool moreFragsRequired = await channel.ProcessPduAsync(buffer.AsMemory().Slice(0, fragLength), cancellationToken).ConfigureAwait(false);

				if (cbRecv > fragLength)
				{
					Buffer.BlockCopy(buffer, fragLength, buffer, 0, cbRecv - fragLength);
					cbRecv -= fragLength;
				}
				else
					cbRecv = 0;


				if (moreFragsRequired)
				{
					this._cbRecv = cbRecv;
					continue;
				}
				else
					break;
			}

			this._cbRecv = cbRecv;
		}

		/// <summary>
		/// Receives a PDU.
		/// </summary>
		/// <param name="buffer">Receive buffer</param>
		/// <param name="cbRecv">Number of bytes already received into buffer</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns><c>0</c> if no data is received; otherwise, the number of bytes in the buffer, including the bytes reported by <paramref name="cbRecv"/>.</returns>
		/// <exception cref="EndOfStreamException">End of stream reached before reading a complete PDU.</exception>
		/// <remarks>
		/// This method does not return until either reading a complete PDU or encountering the end of the stream.
		/// It attempts to fill <paramref name="buffer"/>.  This can result in the buffer containing multiple PDUs.
		/// The only guarantee in this case is that at least one complete PDU was read into <paramref name="buffer"/>.
		/// </remarks>
		private async Task<int> ReceivePduAsync(Memory<byte> buffer, int cbRecv, CancellationToken cancellationToken)
		{
			int cbChunk;
			while (cbRecv < PduHeader.PduStructSize)
			{
				cbChunk = await this.ReadFromStreamAsync(buffer.Slice(cbRecv, buffer.Length - cbRecv), cancellationToken).ConfigureAwait(false);
				if (cbChunk == 0)
				{
					this._isEos = true;
					return 0;
				}

				cbRecv += cbChunk;
			}

			if (cbRecv >= PduHeader.PduStructSize)
			{
				ushort fragLength = RpcPduReader.ReadFragLength(buffer.Span);
				if (cbRecv < fragLength)
				{
					do
					{
						cbChunk = await this.ReadFromStreamAsync(buffer.Slice(cbRecv, buffer.Length - cbRecv), cancellationToken).ConfigureAwait(false);
						cbRecv += cbChunk;
					} while (cbChunk > 0 && cbRecv < fragLength);

					if (cbChunk == 0)
						throw new EndOfStreamException("Partial PDU received");
				}

				return cbRecv;
			}
			else
				throw new EndOfStreamException("Partial PDU received");

		}
	}

	public class RpcStreamTransport : RpcStreamTransportBase
	{
		public RpcStreamTransport(Stream stream, int maxReceiveFragmentSize)
			: base(maxReceiveFragmentSize)
		{
			ArgumentNullException.ThrowIfNull(stream);
			this.Stream = stream;
		}


		/// <summary>
		/// Gets the underlying stream.
		/// </summary>
		protected Stream Stream { get; }

		/// <inheritdoc/>
		protected sealed override void OnDisposing(bool disposing)
		{
			base.OnDisposing(disposing);
			if (disposing)
				this.Stream.Dispose();
		}

		/// <inheritdoc/>
		public sealed override ISecureChannel? TryGetSecureChannelInfo() => (this.Stream as ISecureChannel);

		protected override ValueTask<int> ReadFromStreamAsync(Memory<byte> buffer, CancellationToken cancellationToken)
			=> this.Stream.ReadAsync(buffer, cancellationToken);
		/// <inheritdoc/>
		public sealed override async Task SendPduAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken)
		{
			await this.Stream.WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
		}
	}
}
