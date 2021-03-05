using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.DceRpc.Communication
{
	/// <summary>
	/// Implements a transport over a <see cref="Stream"/> that implements <see cref="IAsyncPipeStream"/>.
	/// </summary>
	sealed class RpcPipeTransport : RpcStreamTransport
	{
		internal RpcPipeTransport(Stream stream, int maxReceiveFragmentSize)
			: base(stream, maxReceiveFragmentSize)
		{
			this._pipe = (IAsyncPipeStream)stream;
		}

		private IAsyncPipeStream _pipe;

		/// <inheritdoc/>
		protected sealed override async ValueTask<int> ReadFromStreamAsync(
			Memory<byte> buffer,
			CancellationToken cancellationToken)
		{
			// TODO: Pipes should receive entire messages, but what if a fragment is returned?  Or two?
			return await this.Stream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public sealed override bool SupportsTransceive => true;

		/// <inheritdoc/>
		public sealed override async Task TransceivePduAsync(
			ReadOnlyMemory<byte> pduToSend,
			bool bubbleCancel,
			CancellationToken cancellationToken)
		{
			var channel = this.EnsureChannel();
			var recvBuf = this.EnsureBuffer();
			int cbRecv = this._cbRecv;

			try
			{
				cbRecv += await this._pipe.Transceive(
					pduToSend,
					recvBuf.AsMemory().Slice(cbRecv, this.MaxReceiveFragmentSize - cbRecv),
					cancellationToken).ConfigureAwait(false);
				if (await channel.ProcessPduAsync(recvBuf.AsMemory().Slice(0, cbRecv), cancellationToken).ConfigureAwait(false))
				{
					await this.ReceiveAndDeliverPdu(cancellationToken).ConfigureAwait(false);
				}
			}
			catch (OperationCanceledException ex)
			{
				if (bubbleCancel)
					throw;
			}
		}
	}
}
