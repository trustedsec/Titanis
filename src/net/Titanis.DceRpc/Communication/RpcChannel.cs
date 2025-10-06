using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.WireProtocol;
using Titanis.IO;
using Titanis.Security;

namespace Titanis.DceRpc.Communication
{
	[Flags]
	public enum RpcChannelSendOptions
	{
		None = 0,
		ExpectsResponse = 1,
	}

	/// <summary>
	/// Implements a channel for RPC.
	/// </summary>
	public abstract partial class RpcChannel : Runnable
	{
		private protected RpcChannel(RpcTransport transport, TimeSpan callTimeout, IRpcCallback? callback)
		{
			Debug.Assert(transport != null);
			this._transport = transport;
			this._transport.AttachTo(this);

			this.CallTimeout = callTimeout;
			this._callback = callback;
		}

		private RpcTransport _transport;
		private readonly IRpcCallback? _callback;

		public RpcTransport Transport => this._transport;
		internal ISecureChannel TryGetSecureChannelInfo()
			=> (this._transport.TryGetSecureChannelInfo());

		protected override async Task OnStarting(CancellationToken cancellationToken)
		{
			await base.OnStarting(cancellationToken).ConfigureAwait(false);
			this._transport.Start();
		}

		private Dictionary<int, PduFragGroup> _fragGroups = new Dictionary<int, PduFragGroup>();

		private void CloseChannel() => this._transport.Dispose();

		// [C706] § Table K-2
		internal const int MustRecvFragSize_CO = 1432;
		// [C706] § Table K-2
		internal const int MustRecvFragSize_CL = 1464;

		// TODO: When did this change?
		// Windows 11 uses 5840
		internal const int WindowsDefaultMaxFragCO = 5840;
		private const int AuthVerifierSize = 8;

		//internal const int WindowsDefaultMaxFragCO = 4280;

		public TimeSpan CallTimeout { get; set; }

		#region Auth contexts
		private Dictionary<uint, BindAuthContext> _bindAuthContexts = new Dictionary<uint, BindAuthContext>();
		private protected void RegisterAuthContext(uint authContextId, BindAuthContext authContext)
		{
			lock (this._bindAuthContexts)
			{
				this._bindAuthContexts.Add(authContextId, authContext);
			}
		}
		#endregion

		private protected int MaxRecvFrag => this._transport.MaxReceiveFragmentSize;
		private protected int MaxXmitFrag { get; private set; } = WindowsDefaultMaxFragCO;

		private protected void UpdateMaxParams(int maxRecvFrag, int maxXmitFrag)
		{
			this._transport.MaxReceiveFragmentSize = maxRecvFrag;
			this.MaxXmitFrag = Math.Min(maxXmitFrag, this.MaxXmitFrag);
		}

		#region Message security
		private protected static void VerifyPdu(
			in PduHeader header,
			in Span<byte> message,
			BindAuthContext authContext)
		{
			authContext.AuthContext.VerifyMessage(
				new MessageVerifyParams(
					message.Slice(message.Length - header.authLength, header.authLength),
					SecBufferList.Create(
						SecBuffer.Integrity(header.AsSpan()),
						SecBuffer.Integrity(message.Slice(0, message.Length - header.authLength))
					)), MessageSignOptions.None);
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		struct ResponseHeaderUnseal
		{
			internal static unsafe int StructSize => sizeof(ResponseHeaderUnseal);

			internal PduHeader pduhdr;
			internal ResponsePduHeader resphdr;

			internal unsafe Span<byte> AsSpan()
			{
				fixed (PduHeader* pHdr = &this.pduhdr)
				{
					return new Span<byte>((byte*)pHdr, StructSize);
				}
			}
		}

		private protected static unsafe void UnsealResponse(
			in PduHeader header,
			in Span<byte> message,
			BindAuthContext authContext)
		{
			ResponseHeaderUnseal hdrs;
			fixed (byte* pResp = message)
			{
				hdrs = new ResponseHeaderUnseal
				{
					pduhdr = header,
					resphdr = *(ResponsePduHeader*)pResp
				};
			}

			int cbRespHeader = RequestPduHeader.StructSize;
			int cbBody = message.Length - cbRespHeader - header.authLength - AuthVerifierHeader.PduStructSize;
			authContext.AuthContext.UnsealMessage(
				new MessageSealParams(
					default,
					SecBufferList.Create(
						SecBuffer.Integrity(hdrs.AsSpan()),
						SecBuffer.PrivacyWithIntegrity(message.Slice(cbRespHeader, cbBody)),
						SecBuffer.Integrity(message.Slice(cbRespHeader + cbBody, AuthVerifierHeader.PduStructSize))
					),
					message.Slice(message.Length - header.authLength, header.authLength)
				));
		}
		#endregion

		/// <summary>
		/// Processes a PDU.
		/// </summary>
		/// <param name="frag">PDU fragment</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns><see langword="true"/> if more fragments are required to process the PDU; otherwise, <see langword="false"/>.</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="FormatException"></exception>
		/// <exception cref="ProtocolViolationException"></exception>
		public async Task<bool> ProcessPduAsync(Memory<byte> frag, CancellationToken cancellationToken)
		{
			if (frag.Length < PduHeader.PduStructSize)
				throw new ArgumentException(Messages.RpcServer_ReceivedFragTooShort);

			PduHeader hdr = MemoryMarshal.AsRef<PduHeader>(frag.Span);
			if (hdr.rpcVersMajor != this._transport.MajorVersionNumber)
			{
				// TODO: Why bind_nak here?
				await this.SendBindNak(hdr.callId, BindRejectReason.ProtocolVersionNotSupported, cancellationToken).ConfigureAwait(false);
				return false;
			}

			if (hdr.fragLength != frag.Length)
				throw new FormatException(Messages.RpcServer_IncompleteFragment);

#if DEBUG
			// TODO: Verify this?
			int minorVers = hdr.rpcVersMinor;
#endif


			// TODO: Compare key fields of PDU
			var ptype = hdr.ptype;
			var headerSize = PduHeader.PduStructSize + ptype switch
			{
				PduType.Request => RequestPduHeader.StructSize,
				PduType.Response => ResponsePduHeader.StructSize,
				PduType.Bind
				or PduType.AlterContext => PduHeader.PduStructSize,
				PduType.BindAck
				or PduType.AlterContextResp => BindAckPduHeader.StructSize,
				PduType.BindNak => BindNakPduHeader.StructSize,
				PduType.Fault => FaultPduHeader.StructSize,
				// TODO: Add AlterContext and AlterContextResp
			};

			int stubLength = frag.Length - headerSize;

			bool isFirstFrag = /* UNDONE (minorVers == 0) || */ (0 != (hdr.pfc_flags & PfcFlags.FirstFrag));
			bool isLastFrag = /* UNDONE: (minorVers != 0) || */ (0 != (hdr.pfc_flags & PfcFlags.LastFrag));


			// TODO: Check whether the packet meets the minimum message security requirements for the context (e.g. does the context require signing?)
			int cbAuthTrailer = 0;
			if (hdr.authLength > 0)
			{
				cbAuthTrailer = AuthVerifierSize + hdr.authLength;
				stubLength -= cbAuthTrailer;

				if (hdr.ptype is PduType.Response or PduType.Request)
				{
					int offAuth = frag.Length - hdr.authLength - AuthVerifierSize;
					var authReader = new ByteMemoryReader(frag.Slice(offAuth));
					var authVerifier = authReader.ReadAuthVerifier(hdr.authLength);
					if (this._bindAuthContexts.TryGetValue(authVerifier.hdr.auth_context_id, out var bindAuthContext))
					{
						var stubData = frag.Slice(PduHeader.PduStructSize);
						if (bindAuthContext.AuthLevel == RpcAuthLevel.PacketIntegrity)
						{
							VerifyPdu(hdr, stubData.Span, bindAuthContext);
						}
						else if (bindAuthContext.AuthLevel == RpcAuthLevel.PacketPrivacy)
						{
							UnsealResponse(hdr, stubData.Span, bindAuthContext);
						}
					}
					else
					{
						// TODO: Log?
						throw new ProtocolViolationException("Bad auth context");
					}
				}
			}
			if (isFirstFrag && isLastFrag)
			{
				// TODO: CancellationToken here?
				byte[] buf = frag.Slice(PduHeader.PduStructSize).ToArray();
				_ = Task.Factory.StartNew(() => this.DispatchPduAsync(hdr, buf, cancellationToken));
				return false;
			}
			else
			{
				PduFragGroup? fragGroup;
				if (isFirstFrag)
				{
					int allocHist;
					if (hdr.ptype is PduType.Request)
					{
						// TODO: Use alloc_hint to size buffer
					}

					fragGroup = new PduFragGroup(hdr);
					lock (this._fragGroups)
						this._fragGroups.Add(fragGroup.CallId, fragGroup);

					// Include the PDU header after PduHeader for the first packet
					fragGroup.AppendChunk(frag.Slice(PduHeader.PduStructSize, frag.Length - PduHeader.PduStructSize - cbAuthTrailer).Span);
				}
				else
				{
					if (!this._fragGroups.TryGetValue(hdr.callId, out fragGroup))
						throw new FormatException(Messages.RpcServer_BadFragCallId);

					fragGroup.AppendChunk(frag.Slice(headerSize, stubLength).Span);
				}

				if (isLastFrag)
				{
					lock (this._fragGroups)
						this._fragGroups.Remove(hdr.callId);

					_ = this.DispatchPduAsync(fragGroup.header, fragGroup.Reassemble(), cancellationToken).ConfigureAwait(false);
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		public virtual Task DispatchPduAsync(
			PduHeader header,
			Memory<byte> message,
			CancellationToken cancellationToken)
			=> header.ptype switch
			{
				PduType.Bind => this.DispatchBind(header, message, cancellationToken),
				PduType.Request => this.DispatchRequest(header, message, cancellationToken),
				_ => throw new NotImplementedException()
			};

		private async Task DispatchBind(
			PduHeader header,
			Memory<byte> message,
			CancellationToken cancellationToken)
		{
			ByteMemoryReader reader = new ByteMemoryReader(message);
			BindPdu bind = new BindPdu();
			bind.ReadFrom(reader, PduByteOrder.LittleEndian);
			AuthVerifier? authVerifier = null;
			if (header.authLength > 0)
			{
				int pad = (int)reader.Align(4);
				authVerifier = reader.ReadAuthVerifier(header.authLength);
				// TODO: Process auth token
			}

			await this.HandleBind(header, message, bind, authVerifier, cancellationToken).ConfigureAwait(false);
		}

		private protected virtual Task HandleBind(
			PduHeader header,
			Memory<byte> message,
			BindPdu bind,
			AuthVerifier? authVerifier,
			CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		private async Task DispatchRequest(
			PduHeader header,
			Memory<byte> message,
			CancellationToken cancellationToken)
		{
			ByteMemoryReader reader = new ByteMemoryReader(message);
			bool hasObjectId = 0 != (header.pfc_flags & PfcFlags.ObjectUuid);
			RequestPdu request = reader.ReadRequest(hasObjectId);

			await this.HandleRequest(header, message, request, reader, cancellationToken).ConfigureAwait(false);
		}

		private protected virtual Task HandleRequest(
			PduHeader header,
			Memory<byte> message,
			RequestPdu request,
			ByteMemoryReader reader,
			CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Writes final headers and applies appropriate message security (i.e. signing and sealing).
		/// </summary>
		/// <param name="pduFlags"></param>
		/// <param name="authLength"></param>
		/// <param name="authContext"></param>
		/// <returns></returns>
		internal static void ApplyMessageSecurity(
			Span<byte> buffer,
			PfcFlags pduFlags,
			int authLength,
			BindAuthContext? authContext
			)
		{
			Debug.Assert(authContext != null);

			int cbFrag = buffer.Length;

			if (authContext.AuthLevel == RpcAuthLevel.PacketIntegrity)
			{
				int cbBody = cbFrag - authLength;
				authContext.AuthContext.SignMessage(
					message: buffer.Slice(0, cbBody),
					macBuffer: buffer.Slice(cbBody, authLength),
					MessageSignOptions.None);
			}
			else if (authContext.AuthLevel == RpcAuthLevel.PacketPrivacy)
			{
				int cbHeader = PduHeader.PduStructSize + ((0 != (pduFlags & PfcFlags.ObjectUuid)) ? RequestPduHeader.StructSizeWithObjectId : RequestPduHeader.StructSize);
				int cbBody = cbFrag - cbHeader - authLength - AuthVerifierHeader.PduStructSize;
				authContext.AuthContext.SealMessage(
					new MessageSealParams(
						default,
						SecBufferList.Create(
							SecBuffer.Integrity(buffer.Slice(0, cbHeader)),
							SecBuffer.PrivacyWithIntegrity(buffer.Slice(cbHeader, cbBody)),
							SecBuffer.Integrity(buffer.Slice(cbHeader + cbBody, AuthVerifierHeader.PduStructSize))
						),
						buffer.Slice(cbFrag - authLength, authLength)
					));
			}
		}

		/// <summary>
		/// Sends a PDU.
		/// </summary>
		/// <param name="pduType"></param>
		/// <param name="pduFlags"></param>
		/// <param name="callId"></param>
		/// <param name="writer"></param>
		/// <param name="authLength">Length of the authentication data, EXCLUDING the auth verifier</param>
		/// <param name="cancellationToken"></param>
		/// <param name="authContext"></param>
		private protected async Task SendPduAsync(
			PduType pduType,
			PfcFlags pduFlags,
			int callId,
			ByteWriter writer,
			int authLength,
			CancellationToken cancellationToken,
			BindAuthContext? authContext = null)
		{
			var fragThreshold = this.MaxXmitFrag;
			if (authLength > 0)
			{
				fragThreshold -= authLength;
				fragThreshold -= AuthVerifierHeader.PduStructSize;
				fragThreshold -= (fragThreshold % AuthVerifierHeader.Alignment);
			}

			var cbPdu = writer.Length;

			// TODO: Replace with actual version
			RpcVersion version = new RpcVersion(5, 0);
			// TODO: Replace with actual drep
			const uint drep = 0x0010;

			bool isOversized = cbPdu > fragThreshold;
			if (isOversized && !(pduType is PduType.Bind or PduType.AlterContext))
			{
				// The call must be fragmented

				var buf = writer.GetData();

				int cbHeader = pduType switch
				{
					PduType.Request => PduHeader.PduStructSize + ((0 != (pduFlags & PfcFlags.ObjectUuid)) ? RequestPduHeader.StructSizeWithObjectId : RequestPduHeader.StructSize),
					_ => throw new NotImplementedException($"Cannot fragment PDU type {pduType}")
				};

				int cbStubData = cbPdu - cbHeader;
				int cbMaxFragBody = fragThreshold - cbHeader;

				int iFrag = 0;
				byte[] fragbuf = new byte[this.MaxXmitFrag];

				// Copy header
				buf.Span.Slice(0, cbHeader).CopyTo(fragbuf);

				MemoryMarshal.AsRef<PduHeader>(fragbuf) = new PduHeader
				{
					rpcVersMajor = (byte)version.Major,
					rpcVersMinor = (byte)version.Minor,
					ptype = pduType,
					pfc_flags = pduFlags,
					drep = drep,
					authLength = (ushort)authLength,
					callId = callId
				};
				while (iFrag < cbStubData)
				{
					var cbChunk = cbStubData - iFrag;

					PfcFlags fragFlags = pduFlags;
					if ((iFrag == 0))
						fragFlags |= PfcFlags.FirstFrag;

					bool isLast = cbChunk <= cbMaxFragBody;
					if (isLast)
						fragFlags |= PfcFlags.LastFrag;
					else
						cbChunk = cbMaxFragBody;

					// Copy chunk from the PDU
					buf.Span.Slice(cbHeader + iFrag, cbChunk).CopyTo(fragbuf.AsSpan().Slice(cbHeader, cbChunk));

					var fragLength = (ushort)(cbChunk + cbHeader);

					if (authLength > 0)
					{
						fragLength += (ushort)(authLength + AuthVerifierHeader.PduStructSize);
						MemoryMarshal.AsRef<AuthVerifierHeader>(fragbuf.AsSpan().Slice(cbHeader + cbChunk, AuthVerifierHeader.PduStructSize)) = new AuthVerifierHeader
						{
							auth_type = (RpcAuthType)authContext.AuthContext.RpcAuthType,
							auth_level = authContext.AuthLevel,
							auth_pad_length = (byte)0,
							auth_reserved = 0,
							auth_context_id = authContext.ContextId
						};
					}

					SetFragFlagsAndLength(fragbuf, fragFlags, fragLength);

					if (authContext != null && pduType is PduType.Request or PduType.Response)
					{
						ApplyMessageSecurity(
							fragbuf.Slice(0, fragLength),
							pduFlags,
							authLength,
							authContext);
					}

					RpcChannelSendOptions sendOptions = RpcChannelSendOptions.None;
					if (isLast && pduType is PduType.Request or PduType.Bind or PduType.AlterContext)
						sendOptions |= RpcChannelSendOptions.ExpectsResponse;
					await this.SendPduAsync(
						fragbuf.AsMemory().Slice(0, fragLength),
						sendOptions,
						cancellationToken).ConfigureAwait(false);

					iFrag += cbChunk;
				}
			}
			else
			{
				// No fragmentation necessary

				bool includeAuthTrailer = (authContext != null) && (authContext.AuthLevel >= RpcAuthLevel.PacketIntegrity);

				if (includeAuthTrailer && authContext != null)
				{
					int pad = writer.Align(AuthVerifierHeader.Alignment, -24);
					writer.WritePduStruct(new AuthVerifierHeader
					{
						auth_type = (RpcAuthType)authContext.AuthContext.RpcAuthType,
						auth_level = authContext.AuthLevel,
						auth_pad_length = (byte)pad,
						auth_reserved = 0,
						auth_context_id = authContext.ContextId
					});

					int macSize = authContext.MessageAuthTokenSize;
					writer.Consume(macSize);
				}

				cbPdu = writer.Length;
				var buf = writer.GetData();

				MemoryMarshal.AsRef<PduHeader>(buf.Span) = new PduHeader
				{
					rpcVersMajor = (byte)version.Major,
					rpcVersMinor = (byte)version.Minor,
					ptype = pduType,
					pfc_flags = PfcFlags.FirstFrag | PfcFlags.LastFrag | pduFlags,
					drep = drep,
					fragLength = (ushort)cbPdu,
					authLength = (ushort)authLength,
					callId = callId
				};

				if (authContext != null && pduType is PduType.Request or PduType.Response)
				{
					ApplyMessageSecurity(
						buf.Span,
						pduFlags,
						authLength,
						authContext);
				}

				RpcChannelSendOptions sendOptions = RpcChannelSendOptions.None;
				if (pduType is PduType.Request or PduType.Bind or PduType.AlterContext)
					sendOptions |= RpcChannelSendOptions.ExpectsResponse;
				await this.SendPduAsync(writer.GetData(), sendOptions, cancellationToken).ConfigureAwait(false);
			}
		}

		private static PduHeader SetFragFlagsAndLength(byte[] fragbuf, PfcFlags fragFlags, ushort fragLength)
		{
			ref PduHeader fraghdr = ref MemoryMarshal.AsRef<PduHeader>(fragbuf);
			fraghdr.pfc_flags = fragFlags;
			fraghdr.fragLength = fragLength;
			return fraghdr;
		}

		protected async Task SendPduAsync(
			Memory<byte> buffer,
			RpcChannelSendOptions options,
			CancellationToken cancellationToken)
		{
			if (this._transport.SupportsTransceive && (0 != (options & RpcChannelSendOptions.ExpectsResponse)))
			{
				await this._transport.TransceivePduAsync(
					buffer,
					false,
					cancellationToken).ConfigureAwait(false);
			}
			else
			{
				await this._transport.SendPduAsync(buffer, cancellationToken).ConfigureAwait(false);
			}
		}

		private protected Task SendBindNak(
			int callId,
			BindRejectReason reason,
			CancellationToken cancellationToken)
		{
			ByteWriter writer = RpcPduWriter.Create();
			writer.WriteBindNak(new BindNakPdu(new BindNakPduHeader(reason)));
			return this.SendPduAsync(PduType.BindNak, PfcFlags.None, callId, writer, 0, cancellationToken);
		}

		/// <summary>
		/// Called by the transport to notify the channel that the underlying transport has failed.
		/// </summary>
		/// <param name="exception"></param>
		internal abstract void OnTransportAborted(Exception? exception);
	}
	partial class RpcChannel : IDisposable
	{
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					this.CloseChannel();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~RpcChannel()
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
