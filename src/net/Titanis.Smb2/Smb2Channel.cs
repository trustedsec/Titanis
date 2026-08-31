using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Net;
using Titanis.Smb2.Pdus;

namespace Titanis.Smb2
{
	/// <summary>
	/// Represents an SMB message.
	/// </summary>
	/// <remarks>
	/// A message encapsulates the PDU along with the header.
	/// </remarks>
	struct Smb2Message
	{
		public Smb2Message(Smb2Pdu pdu, Memory<byte> pduBytes)
		{
			Debug.Assert(pdu != null);

			this.pdu = pdu;
			this.pduBytes = pduBytes;
		}

		internal Smb2PduSyncHeader hdr => this.pdu.pduhdr;
		internal readonly Smb2Pdu pdu;
		internal readonly Memory<byte> pduBytes;
	}

	// [MS-SMB2] § 3.1.4.1 Signing An Outgoing Message
	enum Smb2SignFlags : uint
	{
		None = 0,
		Server = 1,
		CancelRequest = 2,
	}

	/// <summary>
	/// Represents the transport-layer channel between an SMB client and server.
	/// </summary>
	/// <remarks>
	/// Generally, this represents the underlying TCP connection.
	/// </remarks>
	partial class Smb2Channel : Runnable, IStreamTransportHandler
	{
		internal Smb2Channel(Stream stream, int receiveBufferSize)
		{
			if (receiveBufferSize < 1024)
				throw new ArgumentOutOfRangeException(nameof(receiveBufferSize), Messages.Smb2Channel_InsufficientReceiveBuffer);

			this._stream = stream;
			this._receiveBufferSize = receiveBufferSize;
			var transport = new StreamTransport(this._stream);
			this._transport = transport;
		}

		const int NbssHeaderSize = 4;
		private Smb2Connection? _attachedConnection;

		private readonly Stream _stream;
		private readonly int _receiveBufferSize;
		private readonly StreamTransport _transport;

		internal void OnAttaching(Smb2Connection connection)
		{
			Debug.Assert(this._attachedConnection == null);
			this._attachedConnection = connection;
		}

		protected sealed override Task OnStarting(CancellationToken cancellationToken)
		{
			Debug.Assert(this._attachedConnection != null);
			return Task.CompletedTask;
		}

		/// <inheritdoc/>
		protected override Task Run(CancellationToken cancellationToken) => this._transport.Run(this, this._receiveBufferSize, NbssHeaderSize, cancellationToken);

		protected override Task OnStopping()
		{
			this._attachedConnection?.OnChannelStopping();
			return base.OnStopping();
		}

		protected override Task OnAborting()
		{
			this._attachedConnection?.OnChannelAborting(this.Exception);
			return base.OnAborting();
		}

		internal ValueTask SendFrameAsync(Memory<byte> frameBytes)
			=> this._stream.WriteAsync(frameBytes);



		int IStreamTransportHandler.ExtractMessageFrameSize(ReadOnlySpan<byte> buffer)
		{
			int cbPdu = (int)(BinaryPrimitives.ReadUInt32BigEndian(buffer) & 0x00FF_FFFF);
			return cbPdu + 4;
		}

		async Task IStreamTransportHandler.HandleFrame(Memory<byte> frame)
		{
			var pduBytes = frame.Slice(NbssHeaderSize);
			Smb2Message msg = this._attachedConnection.ParsePdu(pduBytes);
			await this._attachedConnection.HandlePdu(msg).ConfigureAwait(false);
		}
	}

	partial class Smb2Channel : IDisposable, IAsyncDisposable
	{
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					this.Stop(TimeSpan.FromSeconds(1));
					this._stream.Dispose();
				}

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public async ValueTask DisposeAsync()
		{
			if (!this.disposedValue)
			{
				await this.Stop(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
				await this._stream.DisposeAsync().ConfigureAwait(false);
				this.disposedValue = true;
			}
		}
	}
}
