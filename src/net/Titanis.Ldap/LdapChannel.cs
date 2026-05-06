using Lightweight_Directory_Access_Protocol_V3;
using System.Buffers.Binary;
using System.Diagnostics;
using Titanis.Asn1.Serialization;
using Titanis.Security;

namespace Titanis.Ldap
{
	internal abstract class LdapChannel : Runnable
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

		protected bool ShouldSealMessages => (this.AuthContext?.IsComplete ?? false) && this.AuthContext.SupportsEncryption;

		protected override async Task Run(CancellationToken cancellationToken)
		{
			byte[] buf = new byte[1 * 1024 * 1024];

			var stream = this.Stream;
			int cbRecvBuf = 0;
			bool bufferDecrypted = false;

			while (!cancellationToken.IsCancellationRequested)
			{
				var cbMin = 2;
				Memory<byte> messageBytes;
				do
				{
					if (cbMin > buf.Length)
						Array.Resize(ref buf, cbMin);

					var cbRead = (cbRecvBuf < cbMin) ? await stream.ReadAtLeastAsync(buf.AsMemory(cbRecvBuf), (cbMin - cbRecvBuf), false, cancellationToken).ConfigureAwait(false) : 0;

					cbRecvBuf += cbRead;
					messageBytes = buf.AsMemory(0, cbRecvBuf);

					if (this.ShouldSealMessages)
					{
						Debug.Assert(this.AuthContext != null);

						if (bufferDecrypted)
						{
							// Do nothing
						}
						else
						{
							if (cbRecvBuf < 4)
							{
								cbMin = 4;
								continue;
							}
							else
							{
								cbMin = 4 + BinaryPrimitives.ReadInt32BigEndian(buf);
							}

							if (cbRecvBuf < cbMin)
								continue;

							// Decrypt
							var cbPdu = cbMin - 4;
							var cbTrailer = this.AuthContext.GetWrapTokenSize(WrapOptions.Confidentiality);
							var cbBody = cbPdu - cbTrailer;
							messageBytes = buf.AsMemory(4 + cbTrailer, cbBody);
							cbRecvBuf = cbBody;


							this.AuthContext.UnsealMessage(new MessageSealParams(
								buf.AsSpan(4, cbTrailer),
								SecBufferList.Create(SecBuffer.PrivacyWithIntegrity(messageBytes.Span)),
								default
								));
							bufferDecrypted = true;
						}
					}

					int sizeOctet = messageBytes.Span[1];
					if (sizeOctet < 0x80)
					{
						cbMin = 2 + sizeOctet;
					}
					else if (sizeOctet == 0x84)
					{
						// Usual case for Windows KDC
						cbMin = 2 + 4;
						if (cbRecvBuf >= cbMin)
						{
							int realSize = BinaryPrimitives.ReadInt32BigEndian(messageBytes.Span.Slice(2, 4));
							cbMin += realSize;
						}
					}
					else if (sizeOctet > 0x80)
					{
						sizeOctet &= 0x0F;
						if (sizeOctet > 8)
							throw new NotSupportedException($"The reported size octet 0x{sizeOctet:X2} exceeds the limit of this implementation.");

						cbMin = 2 + sizeOctet;

						if (cbRecvBuf >= cbMin)
						{
							ulong realSize = 0;
							for (int i = 0; i < sizeOctet; i++)
							{
								realSize <<= 8;
								realSize |= messageBytes.Span[2 + i];
							}
							if (realSize > MaxPduSize)
								throw new ArgumentException($"The reported size of 0x{realSize:X} exceeds the limit of this implementation.");

							cbMin = 2 + unchecked(sizeOctet + (int)(realSize));
						}
					}
					else if (sizeOctet == 0x80)
					{
						throw new NotImplementedException("Indefinite size encoded");
					}
				} while (cbRecvBuf < cbMin);

				var message = Asn1DerDecoder.DecodeTlv<LDAPMessage>(messageBytes);

				HandleMessage(message);

				if (messageBytes.Length > cbMin)
				{
					int cbRem = messageBytes.Length - cbMin;
					messageBytes.Span.Slice(cbMin, cbRem).CopyTo(buf);
					cbRecvBuf = cbRem;
				}
				else
				{
					cbRecvBuf = 0;
					bufferDecrypted = false;
				}
			}
		}

		protected async Task SendMessage(LDAPMessage_ProtocolOp op, Control[]? controls, uint messageId, CancellationToken cancellationToken)
		{
			var message = new LDAPMessage(messageId, op, controls);
			var bytes = Asn1DerEncoder.EncodeTlv(message, options: Asn1DerEncoderOptions.Ber);

			if (op.SelectedChoice != LDAPMessage_ProtocolOp.ChoiceIndex.BindResponse && this.ShouldSealMessages)
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
	}
}
