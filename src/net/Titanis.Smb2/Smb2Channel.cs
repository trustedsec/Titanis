using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Crypto;
using Titanis.IO;
using Titanis.Smb2.Pdus;
using static Titanis.Smb2.Smb2Connection;

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
	partial class Smb2Channel : Runnable
	{
		internal Smb2Channel(Stream stream, int receiveBufferSize)
		{
			if (receiveBufferSize < 1024)
				throw new ArgumentOutOfRangeException(nameof(receiveBufferSize), Messages.Smb2Channel_InsufficientReceiveBuffer);

			this._stream = stream;
			this._recvBuffer = new byte[receiveBufferSize];
		}

		private Smb2Connection? _attachedConnection;

		internal void OnAttaching(Smb2Connection connection)
		{
			Debug.Assert(this._attachedConnection == null);
			this._attachedConnection = connection;
		}

		private byte[] _recvBuffer;
		private int _readIndex;

		private readonly Stream _stream;

		/// <summary>
		/// Reads a frame.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns>The <see cref="Smb2Message"/> read from the channel</returns>
		/// <exception cref="InvalidOperationException"></exception>
		internal async Task<Smb2Message> ReadFrameAsync(CancellationToken cancellationToken)
		{
			int cbRead = this._readIndex;

			var recvBuf = this._recvBuffer;

			const int NbssHeaderSize = 4;
			if (cbRead < NbssHeaderSize)
				cbRead += await this._stream.ReadAtLeastAsync(recvBuf, cbRead, recvBuf.Length - cbRead, NbssHeaderSize - cbRead, cancellationToken).ConfigureAwait(false);

			int cbPdu = (int)BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt32(recvBuf, 0) & ~(uint)0xFF);
			if (cbPdu > (recvBuf.Length - NbssHeaderSize))
			{
				// Expand receive buffer
				byte[] newbuf = new byte[cbPdu + NbssHeaderSize];
				Buffer.BlockCopy(recvBuf, 0, newbuf, 0, cbRead);
				this._recvBuffer = newbuf;
				recvBuf = newbuf;
			}

			int cbFrame = cbPdu + 4;
			if (cbRead < cbFrame)
			{
				await this._stream.ReadAllAsync(recvBuf, cbRead, cbFrame - cbRead, cancellationToken).ConfigureAwait(false);
				cbRead = cbFrame;
			}

			var pduBytes = new Memory<byte>(recvBuf, NbssHeaderSize, cbPdu);
			Smb2Message msg = this._attachedConnection.ParsePdu(pduBytes);

			cbRead -= cbFrame;
			if (cbRead > 0)
			{
				// Multiple frames were read
				Buffer.BlockCopy(recvBuf, cbFrame, recvBuf, 0, cbRead);
			}
			this._readIndex = cbRead;

			return msg;
		}

		protected sealed override Task OnStarting(CancellationToken cancellationToken)
		{
			Debug.Assert(this._attachedConnection != null);
			return Task.CompletedTask;
		}

		/// <inheritdoc/>
		protected override async Task Run(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				Smb2Message msg = await this.ReadFrameAsync(cancellationToken).ConfigureAwait(false);
				await this._attachedConnection.HandlePdu(msg).ConfigureAwait(false);
			}
		}

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
