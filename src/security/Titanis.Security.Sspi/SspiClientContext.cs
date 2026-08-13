using System.Buffers.Binary;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using Titanis.Asn1;
using Titanis.IO;
using Titanis.Winterop;

namespace Titanis.Security.Sspi
{
	public class SspiClientContext : AuthClientContext
	{
		internal SspiClientContext(
			string userName,
			SecHandle hcred,
			byte rpcAuthType
			)
		{
			this.UserName = userName;
			this.hcred = hcred;
			this.RpcAuthType = rpcAuthType;
		}

		private SecHandle hcred;

		private const string NtlmName = "NTLM";
		private const string KerberosName = "Kerberos";
		private const string NegotiateName = "Negotiate";

		public static SspiClientContext ForNtlm(string? userName)
		{
			if (!OperatingSystem.IsWindows())
				throw new PlatformNotSupportedException($"Only supported on Windows");
			NativeMethods.AcquireCredentialsHandle(
				userName,
				NtlmName,
				SECPKG_CRED.SECPKG_CRED_OUTBOUND,
				IntPtr.Zero,
				IntPtr.Zero,
				null,
				0,
				out var hcred,
				out var expiry
				).CheckAndThrow();
			return new SspiClientContext(userName, hcred, 10);
		}

		public static SspiClientContext ForNegotiate(string? userName)
		{
			if (!OperatingSystem.IsWindows())
				throw new PlatformNotSupportedException($"Only supported on Windows");
			NativeMethods.AcquireCredentialsHandle(
				userName,
				NegotiateName,
				SECPKG_CRED.SECPKG_CRED_OUTBOUND,
				IntPtr.Zero,
				IntPtr.Zero,
				null,
				0,
				out var hcred,
				out var expiry
				).CheckAndThrow();
			return new SspiClientContext(userName, hcred, 9);
		}

		private static void EnsureWindows()
		{
			if (!OperatingSystem.IsWindows())
				throw new PlatformNotSupportedException($"Only supported on Windows");
		}


		/// <remarks>Returns a value indicating SP-NEGO</remarks>
		public override byte RpcAuthType { get; }
		public static readonly Asn1Oid SpnegoOid = new Asn1Oid("1.3.6.1.5.5.2");

		/// <remarks>Returns a value indicating SP-NEGO</remarks>
		public override Asn1Oid MechOid => SpnegoOid;

		public override int Legs => (this.RpcAuthType == 10) ? 3 : base.Legs;


		public override string UserName { get; }

		public override SecurityPrincipalName? TargetSpn { get; set; }

		private bool _isComplete;
		public override bool IsComplete => this._isComplete;

		private byte[]? _token;
		public override ReadOnlySpan<byte> Token => this._token;

		private byte[]? _sessionKey;
		public override bool HasSessionKey => this._sessionKey != null;

		public override int SessionKeySize => this._sessionKey?.Length ?? 0;

		private SecurityCapabilities _negcap;
		public override SecurityCapabilities NegotiatedCapabilities => this._negcap;

		public override SecurityCapabilities SupportedCapabilities => SecurityCapabilities.Confidentiality | SecurityCapabilities.SequenceDetection | SecurityCapabilities.ReplayDetection | SecurityCapabilities.Integrity;

		private SecPkgContext_Sizes _sizes;
		private SecPkgContext_StreamSizes _streamSizes;

		public override int GetWrapTokenSize(WrapOptions options) => this._sizes.cbSecurityTrailer;
		public override int SignTokenSize => this._sizes.cbMaxSignature;

		public override void IncrementRecvSeqNbr()
		{
			this._sealSeqnbr = 1;
		}

		private int _sealSeqnbr = -1;
		private int _unsealSeqnbr = -1;

		public override void SealMessage(in MessageSealParams sealParams) =>
			this.TransformMessage(
				sealParams.Header,
				sealParams.Trailer,
				sealParams.bufferList,
				ref this._sealSeqnbr,
				true);
		public override void UnsealMessage(in MessageSealParams unsealParams) =>
			this.TransformMessage(
				unsealParams.Header,
				unsealParams.Trailer,
				unsealParams.bufferList,
				ref this._unsealSeqnbr,
				false);
		private void TransformMessage(
			Span<byte> header,
			Span<byte> trailer,
			in SecBufferList buffers,
			ref int seqnbr_,
			bool seal)
		{
			int cbufs = buffers.BufferCount;
			int cbMessage = buffers.TotalIntegrityLength;
			byte[] message = new byte[cbMessage];

			Span<SspiSecBuffer> secbufs = stackalloc SspiSecBuffer[1 + cbufs];
			int writeIndex = 0;
			int bufIndex;
			unsafe
			{
				fixed (byte* pMessage = message)
				{
					for (bufIndex = 0; bufIndex < cbufs; bufIndex++)
					{
						var buf = buffers.GetBuffer(bufIndex);
						buf.ReadOnlySpan.CopyTo(message.AsSpan(writeIndex));
						secbufs[bufIndex] = new SspiSecBuffer(
							buf.Length,
							buf.ShouldEncrypt ? SspiSecBufferType.DATA : (SspiSecBufferType.DATA | SspiSecBufferType.READONLY_WITH_CHECKSUM),
							new IntPtr(pMessage + writeIndex)
							);

						writeIndex += buf.Length;
					}

					fixed (byte* pHeader = header)
					{
						fixed (byte* pTrailer = trailer)
						{
							secbufs[bufIndex] = new SspiSecBuffer(header.Length, SspiSecBufferType.TOKEN, new IntPtr(pHeader));

							fixed (SspiSecBuffer* pBuffers = secbufs)
							{
								var seqnbr = Interlocked.Increment(ref seqnbr_);

								SspiSecBufferDesc secbufdesc = new SspiSecBufferDesc(secbufs.Length, new IntPtr(pBuffers));
								if (!OperatingSystem.IsWindows())
									throw new PlatformNotSupportedException();

								if (seal)
								{
									var res = NativeMethods.EncryptMessage(
										ref this._hctx,
										0,
										ref secbufdesc,
										seqnbr
										);
									res.CheckAndThrow();
								}
								else
								{
									var res = NativeMethods.DecryptMessage(
										ref this._hctx,
										ref secbufdesc,
										seqnbr,
										out var qop
										);
									res.CheckAndThrow();
								}
							}
						}
					}
				}
			}

			writeIndex = 0;
			for (bufIndex = 0; bufIndex < cbufs; bufIndex++)
			{
				var buf = buffers.GetBuffer(bufIndex);
				if (!buf.IsReadOnly)
				{
					message.AsSpan(writeIndex, buf.Length).CopyTo(buf.Span);
				}

				writeIndex += buf.Length;
			}
		}

		private int _sendSeqNbr;
		private int _recvSeqNbr;
		public override void SignMessage(in MessageSignParams signParams, MessageSignOptions options) =>
			this.SignOrVerifyMessage(
				signParams.MacBuffer,
				signParams.bufferList,
				options,
				ref this._sendSeqNbr,
				true
				);
		public override void VerifyMessage(in MessageVerifyParams verifyParams, MessageSignOptions options) =>
			this.SignOrVerifyMessage(
				verifyParams.MacBuffer,
				verifyParams.bufferList,
				options,
				ref this._recvSeqNbr,
				false
				);

		private void SignOrVerifyMessage(
			ReadOnlySpan<byte> macBuffer,
			in SecBufferList bufferList,
			MessageSignOptions options,
			ref int seqnbr_,
			bool sign)
		{
			byte[] message = bufferList.ToArray(MessageSecBufferOptions.Integrity);

			Span<SspiSecBuffer> secbufs = stackalloc SspiSecBuffer[2];
			unsafe
			{
				fixed (byte* pMessage = message)
				{
					fixed (byte* pHeader = macBuffer)
					{
						secbufs[0] = new SspiSecBuffer(macBuffer.Length, SspiSecBufferType.TOKEN, new IntPtr(pHeader));
						secbufs[1] = new SspiSecBuffer(message.Length, SspiSecBufferType.DATA, new IntPtr(pMessage));

						fixed (SspiSecBuffer* pBuffers = secbufs)
						{
							var seqnbr = Interlocked.Increment(ref seqnbr_);

							SspiSecBufferDesc secbufdesc = new SspiSecBufferDesc(secbufs.Length, new IntPtr(pBuffers));
							if (!OperatingSystem.IsWindows())
								throw new PlatformNotSupportedException();

							if (sign)
							{
								var res = NativeMethods.MakeSignature(
									ref this._hctx,
									0,
									ref secbufdesc,
									seqnbr
									);
								res.CheckAndThrow();
							}
							else
							{
								var res = NativeMethods.VerifySignature(
									ref this._hctx,
									ref secbufdesc,
									seqnbr,
									out var qop
									);
								res.CheckAndThrow();
							}
						}
					}
				}
			}
		}

		protected override ReadOnlySpan<byte> GetSessionKeyImpl()
		{
			return this._sessionKey;
		}

		protected override ReadOnlySpan<byte> InitializeImpl()
		{
			return this.InitializeWithToken(default);
		}

		private static SspiCaps TranslateCaps(SecurityCapabilities caps)
		{
			SspiCaps iscCaps = (SspiCaps)(caps & SecurityCapabilities.Rfc1509Mask);
			if (0 != (caps & SecurityCapabilities.DceStyle)) iscCaps |= SspiCaps.ISC_REQ_USE_DCE_STYLE;
			if (0 != (caps & SecurityCapabilities.Integrity)) iscCaps |= SspiCaps.ISC_REQ_INTEGRITY;
			else iscCaps |= SspiCaps.ISC_REQ_NO_INTEGRITY;

			if (0 != (caps & SecurityCapabilities.Confidentiality)) iscCaps |= SspiCaps.ISC_REQ_CONFIDENTIALITY;
			if (0 != (caps & SecurityCapabilities.ExtendedError)) iscCaps |= SspiCaps.ISC_REQ_EXTENDED_ERROR;
			if (0 != (caps & SecurityCapabilities.MutualAuthentication)) iscCaps |= SspiCaps.ISC_REQ_MUTUAL_AUTH;
			if (0 != (caps & SecurityCapabilities.SequenceDetection)) iscCaps |= SspiCaps.ISC_REQ_SEQUENCE_DETECT;
			if (0 != (caps & SecurityCapabilities.ReplayDetection)) iscCaps |= SspiCaps.ISC_REQ_REPLAY_DETECT;
			if (0 != (caps & SecurityCapabilities.Delegation)) iscCaps |= SspiCaps.ISC_REQ_DELEGATE;
			if (0 != (caps & SecurityCapabilities.IdentifyOnly)) iscCaps |= SspiCaps.ISC_REQ_IDENTIFY;

			// TODO: Others
			iscCaps |= SspiCaps.ISC_REQ_CONNECTION;

			return iscCaps;
		}

		private DateTime _credExpiry;
		private DateTime _contextExpiry;
		private SecHandle _hctx;
		private bool initial;

		protected override ReadOnlySpan<byte> InitializeWithToken(ReadOnlySpan<byte> token)
		{
			EnsureWindows();
			if (!OperatingSystem.IsWindows())
				throw new PlatformNotSupportedException($"Only supported on Windows");

			GlobalMemHandle? hgblToken = null;
			GlobalMemHandle? hgblChannel = null;
			try
			{
				Span<SspiSecBuffer> inbufs = stackalloc SspiSecBuffer[2];
				int secbufCount = 0;

				if (token.Length > 0)
				{
					hgblToken = GlobalMemHandle.Alloc(token);
					inbufs[secbufCount++] = new SspiSecBuffer(hgblToken.Size, SspiSecBufferType.TOKEN, hgblToken.DangerousGetHandle());
				}

				var channelBinding = this.ChannelBinding;
				if (channelBinding != null)
				{
					int cbhdr = Marshal.SizeOf<SEC_CHANNEL_BINDINGS>();
					byte[] sspiBinding = new byte[cbhdr - 20 + channelBinding.RequiredLength];
					var bytes = channelBinding.GetBytes();
					//channelBinding.GetBytes(sspiBinding.AsSpan(cbhdr - 20));

					bytes.AsSpan(20).CopyTo(sspiBinding.Slice(cbhdr));

					ref var bindhdr = ref MemoryMarshal.AsRef<SEC_CHANNEL_BINDINGS>(sspiBinding);
					bindhdr.cbApplicationDataLength = channelBinding.RequiredLength - 20;
					bindhdr.dwApplicationDataOffset = cbhdr;

					hgblChannel = GlobalMemHandle.Alloc(sspiBinding);
					inbufs[secbufCount++] = new SspiSecBuffer(hgblChannel.Size, SspiSecBufferType.CHANNEL_BINDINGS, hgblChannel.DangerousGetHandle());
				}

				SspiSecBuffer[] secbufArray = inbufs.ToArray();
				unsafe
				{
					Span<SspiSecBuffer> outbufs = stackalloc SspiSecBuffer[1];
					ref var outTokenBuf = ref outbufs[0];
					outTokenBuf = new SspiSecBuffer(0, SspiSecBufferType.TOKEN, 0);

					fixed (SspiSecBuffer* pInBufs = inbufs)
					{
						fixed (SspiSecBuffer* pOutBufs = outbufs)
						{
							Debug.Assert(secbufCount <= inbufs.Length);
							SspiSecBufferDesc indesc = new SspiSecBufferDesc(secbufCount, new IntPtr(pInBufs));
							SspiSecBufferDesc outdesc = new SspiSecBufferDesc(outbufs.Length, new IntPtr(pOutBufs));

							try
							{
								Hresult hres;
								SspiCaps effCaps;
								string? pszTargetName = this.TargetSpn?.ToString();
								if (initial)
								{
									hres = NativeMethods.InitializeSecurityContext(
										this.hcred,
										ref this._hctx,
										pszTargetName,
										TranslateCaps(this.RequiredCapabilities) | SspiCaps.ISC_REQ_ALLOCATE_MEMORY,
										0,
										SspiDrep.SECURITY_NATIVE_DREP,
										ref indesc,
										0,
										out this._hctx,
										ref outdesc,
										out effCaps,
										out var expiry
										);
								}
								else
								{
									hres = NativeMethods.InitializeSecurityContext(
										this.hcred,
										IntPtr.Zero,
										pszTargetName,
										TranslateCaps(this.RequiredCapabilities) | SspiCaps.ISC_REQ_ALLOCATE_MEMORY,
										0,
										SspiDrep.SECURITY_NATIVE_DREP,
										ref indesc,
										0,
										out this._hctx,
										ref outdesc,
										out effCaps,
										out var expiry
										);
								}

								hres.CheckAndThrow();
								initial = true;

								if (hres == 0)
								{
									this._isComplete = true;

									// Get session key
									var res = NativeMethods.QueryContextAttributes(ref this._hctx, SECPKG_ATTR.SECPKG_ATTR_SESSION_KEY, out SecPkgContext_SessionKey skbuf);
									if (res == 0)
									{
										try
										{
											this._sessionKey = new byte[skbuf.SessionKeyLength];
											Marshal.Copy(skbuf.SessionKey, this._sessionKey, 0, skbuf.SessionKeyLength);
										}
										finally
										{
											NativeMethods.FreeContextBuffer(skbuf.SessionKey);
										}
									}

									SecurityCapabilities negcaps = SecurityCapabilities.None;
									if (0 != (effCaps & SspiCaps.ISC_REQ_CONFIDENTIALITY))
										negcaps |= SecurityCapabilities.Confidentiality;
									if (0 != (effCaps & SspiCaps.ISC_REQ_INTEGRITY))
										negcaps |= SecurityCapabilities.Integrity;
									if (0 != (effCaps & SspiCaps.ISC_REQ_SEQUENCE_DETECT))
										negcaps |= SecurityCapabilities.SequenceDetection;
									if (0 != (effCaps & SspiCaps.ISC_REQ_REPLAY_DETECT))
										negcaps |= SecurityCapabilities.ReplayDetection;
									if (0 != (effCaps & SspiCaps.ISC_REQ_USE_DCE_STYLE))
										negcaps |= SecurityCapabilities.DceStyle;
									if (0 != (effCaps & SspiCaps.ISC_REQ_DELEGATE))
										negcaps |= SecurityCapabilities.Delegation;
									if (0 != (effCaps & SspiCaps.ISC_REQ_MUTUAL_AUTH))
										negcaps |= SecurityCapabilities.MutualAuthentication;
									if (0 != (effCaps & SspiCaps.ISC_REQ_IDENTIFY))
										negcaps |= SecurityCapabilities.IdentifyOnly;

									this._negcap = negcaps;

									// Sizes
									res = NativeMethods.QueryContextAttributes(ref this._hctx, SECPKG_ATTR.SECPKG_ATTR_SIZES, out this._sizes);
									res = NativeMethods.QueryContextAttributes(ref this._hctx, SECPKG_ATTR.SECPKG_ATTR_STREAM_SIZES, out this._streamSizes);
									hres.CheckAndThrow();
								}

								if (outTokenBuf.cbBuffer > 0 && outTokenBuf.pvBuffer != 0)
								{
									byte[] outToken = new byte[outbufs[0].cbBuffer];
									Marshal.Copy(outbufs[0].pvBuffer, outToken, 0, outbufs[0].cbBuffer);
									this._token = outToken;

									return outToken;
								}
								else
								{
									this._token = null;
									return [];
								}
							}
							finally
							{
								NativeMethods.FreeContextBuffer(outbufs[0].pvBuffer);
							}
						}
					}
				}
			}
			finally
			{
				hgblToken?.Dispose();
				hgblChannel?.Dispose();
			}
		}

	}
}
