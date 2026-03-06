using Lightweight_Directory_Access_Protocol_V3;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Net;
using System.Text;
using System.Transactions;
using Titanis.Asn1.Serialization;
using Titanis.Net;
using Titanis.Security;

namespace Titanis.Ldap
{
	internal class LdapChannel : Runnable
	{
		internal LdapChannel(Stream stream, bool useSsl)
		{
			this._stream = stream;
			this._useSsl = useSsl;
		}

		private readonly Stream _stream;
		private readonly bool _useSsl;
		const uint MaxPduSize = 32 * 1024;
		private const string GssSpnegoMechName = "GSS-SPNEGO";

		private AuthClientContext? _authContext;

		private bool ShouldSealMessages => (this._authContext != null) && this._authContext.SupportsEncryption;

		internal async Task<LdapResponse> Bind(AuthClientContext authContext, CancellationToken cancellationToken)
		{
			var resp = await this.SendMessage(new LDAPMessage_ProtocolOp()
			{
				BindRequest = new BindRequest_Tagged0(3, Array.Empty<byte>(), new AuthenticationChoice()
				{
					Sasl = new SaslCredentials(Encoding.UTF8.GetBytes(GssSpnegoMechName), authContext.Initialize().ToArray())
				})
			}, cancellationToken).ConfigureAwait(false);
			var saslResult = resp.message.protocolOp.BindResponse.resultCode;

			while (saslResult == LDAPResult_ResultCode.SaslBindInProgress || (saslResult == LDAPResult_ResultCode.Success && !authContext.IsComplete))
			{
				var token = authContext.Initialize(resp.message.protocolOp.BindResponse.serverSaslCreds).ToArray();
				if (token.Length > 0)
				{
					resp = await this.SendMessage(new LDAPMessage_ProtocolOp()
					{
						BindRequest = new BindRequest_Tagged0(3, Array.Empty<byte>(), new AuthenticationChoice()
						{
							Sasl = new SaslCredentials(Encoding.UTF8.GetBytes(GssSpnegoMechName), token)
						})
					}, cancellationToken).ConfigureAwait(false);
				}

				saslResult = resp.message.protocolOp.BindResponse.resultCode;
			}

			if (saslResult != LDAPResult_ResultCode.Success)
				throw new LdapException((LdapResultCode)saslResult, Encoding.UTF8.GetString(resp.message.protocolOp.BindResponse.diagnosticMessage));
			// TODO: Why?  I didn't see anything in the spec, but the first message received in NTLM has seq# 1
			authContext.IncrementRecvSeqNbr();
			this._authContext = authContext;

			return resp;
		}

		private static void EnsureResponseChoice(LDAPMessage response, LDAPMessage_ProtocolOp.ChoiceIndex expected)
		{
			if (response.protocolOp.SelectedChoice != expected)
				throw new ProtocolViolationException($"The LDAP server returned an unexpected response.  Expected {expected} but received {response.protocolOp.SelectedChoice}");
		}

		internal static void CheckAndThrow(LDAPResult resultMessage)
		{
			switch (resultMessage.resultCode)
			{
				case LDAPResult_ResultCode.Success:
					break;
				default:
					{
						throw new LdapException((LdapResultCode)resultMessage.resultCode, Encoding.UTF8.GetString(resultMessage.diagnosticMessage));
					}
			}
		}

		internal async Task<LdapResponse> Search(SearchRequest_Tagged3 request, ILdapChannelSearchCallback? searchCallback, Control[]? controls, CancellationToken cancellationToken)
		{
			var resp = await this.SendMessage(
				new LDAPMessage_ProtocolOp()
				{
					SearchRequest = request,
				}, cancellationToken, searchCallback: searchCallback, controls: controls).ConfigureAwait(false);
			EnsureResponseChoice(resp.message, LDAPMessage_ProtocolOp.ChoiceIndex.SearchResDone);

			CheckAndThrow(resp.message.protocolOp.SearchResDone);

			return resp;
		}




		private int _nextMessageID;
		internal uint GetNextMessageId() => (uint)Interlocked.Increment(ref this._nextMessageID);

		private ConcurrentDictionary<uint, LdapResponse> _outstandingMessages = new ConcurrentDictionary<uint, LdapResponse>();

		internal async Task<LdapResponse> SendMessage(LDAPMessage_ProtocolOp op, CancellationToken cancellationToken, ILdapChannelSearchCallback? searchCallback = null, Control[]? controls = null)
		{
			var message = new LDAPMessage(this.GetNextMessageId(), op, controls);
			var bytes = Asn1DerEncoder.EncodeTlv(message, options: Asn1DerEncoderOptions.Ber);

			if (this.ShouldSealMessages)
			{
				int offHeader = 4;
				int offBody = offHeader + this._authContext.SealHeaderSize + this._authContext.SealTrailerSize;
				int offTrailer = offBody + bytes.Length;
				int cbSealed = offTrailer + 0;// + this._authContext.SealTrailerSize;

				byte[] encrypted = new byte[cbSealed];
				bytes.CopyTo(encrypted.AsMemory(offBody, bytes.Length));

				this._authContext.SealMessage(new MessageSealParams(
					encrypted.AsSpan(offHeader, offBody - offHeader),
					SecBufferList.Create(
						SecBuffer.PrivacyWithIntegrity(encrypted.AsSpan(offBody, bytes.Length))),
					default
					));

				BinaryPrimitives.WriteInt32BigEndian(encrypted, encrypted.Length - 4);
				bytes = encrypted;
			}

			var resp = new LdapResponse() { searchCallback = searchCallback };
			resp.cancelReg = cancellationToken.Register(() => resp.taskSource.TrySetCanceled(cancellationToken));

			if (this.IsRunning)
			{
				this._outstandingMessages.TryAdd(message.messageID, resp);
				if (this.IsRunning)
				{
					await this._stream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);

					await resp.taskSource.Task.ConfigureAwait(false);
					resp.cancelReg.Unregister();

					this._outstandingMessages.TryRemove(message.messageID, out _);

					return resp;
				}
			}

			throw ChannelClosedException();
		}

		private static InvalidOperationException ChannelClosedException()
		{
			return new InvalidOperationException("The channel is no longer connected to the server.");
		}

		protected override Task OnStopping()
		{
			while (this._outstandingMessages.Count > 0)
			{
				var entry = this._outstandingMessages.FirstOrDefault();
				entry.Value.taskSource.TrySetException(ChannelClosedException());
				this._outstandingMessages.TryRemove(entry);
			}
			return base.OnStopping();
		}

		protected override async Task Run(CancellationToken cancellationToken)
		{
			byte[] buf = new byte[1 * 1024 * 1024];

			var stream = this._stream;
			int cbRecvBuf = 0;
			bool bufferDecrypted = false;

			while (!cancellationToken.IsCancellationRequested)
			{
				var cbMin = 2;
				Memory<byte> messageBytes;
				do
				{
					var cbRead = (cbRecvBuf < cbMin) ? await stream.ReadAtLeastAsync(buf.AsMemory(cbRecvBuf), (cbMin - cbRecvBuf), false, cancellationToken).ConfigureAwait(false) : 0;

					cbRecvBuf += cbRead;
					messageBytes = buf.AsMemory(0, cbRecvBuf);

					if (this.ShouldSealMessages)
					{
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
							var cbTrailer = (this._authContext.SealHeaderSize + this._authContext.SealTrailerSize);
							var cbBody = cbPdu - cbTrailer;
							messageBytes = buf.AsMemory(4 + cbTrailer, cbBody);
							cbRecvBuf = cbBody;


							this._authContext.UnsealMessage(new MessageSealParams(
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

				if (this._outstandingMessages.TryGetValue(message.messageID, out var resp))
				{
					switch (message.protocolOp.SelectedChoice)
					{
						case LDAPMessage_ProtocolOp.ChoiceIndex.SearchResEntry:
							resp.searchCallback?.OnEntry(message.protocolOp.SearchResEntry);
							break;
						case LDAPMessage_ProtocolOp.ChoiceIndex.SearchResRef:
							foreach (var entry in message.protocolOp.SearchResRef)
							{
								string text = Encoding.UTF8.GetString(entry);
								resp.searchCallback?.OnReference(text);
							}
							break;
						case LDAPMessage_ProtocolOp.ChoiceIndex.SearchResDone:
						default:
							resp.message = message;
							resp.taskSource.TrySetResult(0);
							break;
					}
				}
				else
				{
					// TODO: Report spurious reply
				}

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
	}
}
