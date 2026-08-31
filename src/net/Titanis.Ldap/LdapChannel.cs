using Lightweight_Directory_Access_Protocol_V3;
using System.Buffers.Binary;
using System.Diagnostics;
using Titanis.Asn1.Serialization;
using Titanis.Net;
using Titanis.Security;

namespace Titanis.Ldap
{
	internal abstract class LdapChannel : Runnable, IStreamTransportHandler
	{
		internal LdapChannel(Stream stream)
		{
			this._stream = stream;
		}

		private readonly Stream _stream;
		protected Stream Stream => this._stream;

		const uint MaxPduSize = 32 * 1024;

		protected abstract Task HandleMessage(LDAPMessage message);

		protected abstract AuthContext? AuthContext { get; }

		protected bool ShouldSealMessages { get; set; }

		protected override Task Run(CancellationToken cancellationToken)
		{
			var transport = new StreamTransport(this._stream);
			return transport.Run(this, 1 * 1024 * 1024, 1, cancellationToken);
		}

		protected async Task SendMessage(LDAPMessage_ProtocolOp op, Control[]? controls, uint messageId, CancellationToken cancellationToken)
		{
			var message = new LDAPMessage(messageId, op, controls);
			var bytes = Asn1DerEncoder.EncodeTlv(message, options: Asn1DerEncoderOptions.Ber);

			if (this.ShouldSealMessages)
			{
				Debug.Assert(this.AuthContext != null);

				int offHeader = 4;
				int offBody = offHeader + this.AuthContext.GetWrapTokenSize(WrapOptions.Confidentiality);
				int offTrailer = offBody + bytes.Length;
				int cbSealed = offTrailer + 0;// + this._authContext.SealTrailerSize;

				byte[] encrypted = new byte[cbSealed];
				bytes.CopyTo(encrypted.AsMemory(offBody, bytes.Length));

				this.AuthContext.SealMessage(new MessageSealParams(
					encrypted.AsSpan(offHeader, offBody - offHeader),
					SecBufferList.Create(
						SecBuffer.PrivacyWithIntegrity(encrypted.AsSpan(offBody, bytes.Length))),
					default
					));

				BinaryPrimitives.WriteInt32BigEndian(encrypted, encrypted.Length - 4);
				bytes = encrypted;
			}

			if (this.IsRunning)
			{
				await this.Stream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
			}
			else
				throw ChannelClosedException();
		}

		private protected static InvalidOperationException ChannelClosedException()
		{
			return new InvalidOperationException("The channel is no longer connected to the server.");
		}

		int IStreamTransportHandler.ExtractMessageFrameSize(ReadOnlySpan<byte> buffer)
		{
			if (this.ShouldSealMessages)
			{
				if (buffer.Length < 4)
					return 4;

				int cbMin = 4 + BinaryPrimitives.ReadInt32BigEndian(buffer);
				return cbMin;
			}
			else
			{
				int size = ReadAsn1Size(buffer);
				if (size < 0)
					size = ~size;

				return size;
			}
		}

		private static int ReadAsn1Size(ReadOnlySpan<byte> message)
		{
			int cbMin = 2;
			if (message.Length >= cbMin)
			{
				int sizeOctet = message[1];
				if (sizeOctet < 0x80)
				{
					return 2 + sizeOctet;
				}
				else if (sizeOctet == 0x84)
				{
					// Usual case for Windows KDC
					cbMin = 2 + 4;
					if (message.Length >= cbMin)
					{
						int realSize = BinaryPrimitives.ReadInt32BigEndian(message.Slice(2, 4));
						return cbMin + realSize;
					}
				}
				else if (sizeOctet > 0x80)
				{
					sizeOctet &= 0x0F;
					if (sizeOctet > 8)
						throw new NotSupportedException($"The reported size octet 0x{sizeOctet:X2} exceeds the limit of this implementation.");

					cbMin = 2 + sizeOctet;

					if (message.Length >= cbMin)
					{
						ulong realSize = 0;
						for (int i = 0; i < sizeOctet; i++)
						{
							realSize <<= 8;
							realSize |= message[2 + i];
						}
						if (realSize > MaxPduSize)
							throw new ArgumentException($"The reported size of 0x{realSize:X} exceeds the limit of this implementation.");

						return 2 + unchecked(sizeOctet + (int)(realSize));
					}
				}
				else // if (sizeOctet == 0x80)
				{
					throw new NotImplementedException("Indefinite size encoded");
				}
			}

			return ~cbMin;
		}

		Task IStreamTransportHandler.HandleFrame(Memory<byte> frame)
		{
			// Trim length header
			if (this.ShouldSealMessages)
			{
				frame = frame.Slice(4);
				var cbToken = this.AuthContext.GetWrapTokenSize(WrapOptions.Confidentiality);

				var messageBytes = frame.Slice(cbToken);

				this.AuthContext.UnsealMessage(new MessageSealParams(
					frame.Span.Slice(0, cbToken),
					SecBufferList.Create(SecBuffer.PrivacyWithIntegrity(messageBytes.Span)),
					default
					));

				frame = messageBytes;
			}

			while (frame.Length > 0)
			{
				var messageBytes = frame;
				int cbRecvBuf = messageBytes.Length;

				var message = Asn1DerDecoder.DecodeTlv<LDAPMessage>(messageBytes);
				HandleMessage(message);

				int cbMessage = ReadAsn1Size(frame.Span);
				Debug.Assert(cbMessage > 0);
				frame = frame.Slice(cbMessage);
			}

			return Task.CompletedTask;
		}
	}
}
