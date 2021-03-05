
using System;
using System.Buffers.Binary;
using System.Threading.Channels;
using Titanis.Net;

namespace Titanis.Ssh
{
	public partial class SshConnection : Runnable
	{
		internal SshConnection(Stream stream)
		{
			this._stream = stream;
		}

		// [RFC4253] § 6.1. Maximum Packet Length
		public const int MaxPacketSize = 32765;
		private const int BufferSize = 32 * 1024;

		private readonly Stream _stream;
		private bool _isEos;

		struct PduReadHandler : IPduReadHandler
		{
			public int macLength;
			public int HeaderSize => SshPacket.HeaderSize + 1;

			public int ExtractPacketSize(ReadOnlySpan<byte> header)
				=> 4 + BinaryPrimitives.ReadInt32LittleEndian(header)
					+ this.macLength
				;
		}

		private int _macLength;
		protected sealed override async Task Run(CancellationToken cancellationToken)
		{
			byte[] buffer = new byte[BufferSize];

			int cbRecv = 0;
			int cbMac = 0;
			while ((cbRecv = await this._stream.ReadPduAsync(buffer, cbRecv, new PduReadHandler() { macLength=cbMac }, cancellationToken).ConfigureAwait(false)) > 0)
			{
				int cbPacket = BinaryPrimitives.ReadInt32LittleEndian(buffer);
				if (cbPacket == 0)
				{
					// End of stream reached
				}
				else
				{
					byte cbPadding = buffer[5];

					await this.HandlePacket(buffer.AsMemory().Slice(4, cbPacket), buffer.AsMemory().Slice(4+cbPacket, cbMac)).ConfigureAwait(false);

					if (cbRecv > cbPacket)
					{
						Buffer.BlockCopy(buffer, cbPacket, buffer, 0, cbRecv - cbPacket);
						cbRecv -= cbPacket;
					}
					else
						cbRecv = 0;
				}
			}
		}

		private async Task HandlePacket(Memory<byte> payload, Memory<byte> mac)
		{
		}
	}

	partial class SshConnection : IDisposable
	{
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~SshConnection()
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
	}
}