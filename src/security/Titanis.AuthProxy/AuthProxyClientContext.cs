using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks.Dataflow;
using Titanis.Asn1;
using Titanis.IO;
using Titanis.Net;
using Titanis.Security;
using Titanis.Winterop;

namespace Titanis.AuthProxy
{
	public class AuthProxyClientContext : AuthClientContext
	{
		public AuthProxyClientContext(
			string userName,
			ISocket authSocket
			)
		{
			this.UserName = userName;
			this._authSocket = authSocket;
		}

		private readonly ISocket _authSocket;

		/// <remarks>Returns a value indicating SP-NEGO</remarks>
		public override byte RpcAuthType => 0x09;
		public static readonly Asn1Oid SpnegoOid = new Asn1Oid("1.3.6.1.5.5.2");
		/// <remarks>Returns a value indicating SP-NEGO</remarks>
		public override Asn1Oid MechOid => SpnegoOid;


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

		public override int GetWrapTokenSize(WrapOptions options)
		{
			throw new NotImplementedException();
		}

		public override void IncrementRecvSeqNbr()
		{
			throw new NotImplementedException();
		}

		public override void SealMessage(in MessageSealParams sealParams)
		{
			throw new NotImplementedException();
		}

		public override void SignMessage(in MessageSignParams signParams, MessageSignOptions options)
		{
			throw new NotImplementedException();
		}

		public override void UnsealMessage(in MessageSealParams unsealParams)
		{
			throw new NotImplementedException();
		}

		public override void VerifyMessage(in MessageVerifyParams verifyParams, MessageSignOptions options)
		{
			throw new NotImplementedException();
		}

		protected override ReadOnlySpan<byte> GetSessionKeyImpl()
		{
			return this._sessionKey;
		}

		protected override ReadOnlySpan<byte> InitializeImpl()
		{
			return this.InitializeWithToken(default);
		}

		private static ushort Measure(string? str, ref int offset)
		{
			if (str is null)
				return 0;

			var cbStr = Encoding.Unicode.GetByteCount(str) + 2;
			var off = (ushort)offset;
			offset += cbStr;
			return off;
		}
		private static ushort Measure(ReadOnlySpan<byte> bytes, ref int offset)
		{
			if (bytes.Length == 0)
				return 0;

			var off = (ushort)offset;
			offset += bytes.Length;
			return off;
		}
		private static ushort Measure(byte[] bytes, ref int offset) => Measure(bytes.AsSpan(), ref offset);

		private static SspiCaps TranslateCaps(SecurityCapabilities caps)
		{
			SspiCaps iscCaps = (SspiCaps)(caps & SecurityCapabilities.Rfc1509Mask);
			if (0 != (caps & SecurityCapabilities.DceStyle)) iscCaps |= SspiCaps.DceStyle;
			if (0 != (caps & SecurityCapabilities.Integrity)) iscCaps |= SspiCaps.Integrity;
			if (0 != (caps & SecurityCapabilities.ExtendedError)) iscCaps |= SspiCaps.ExtendedError;

			// TODO: Others

			return iscCaps;
		}

		protected override ReadOnlySpan<byte> InitializeWithToken(ReadOnlySpan<byte> token)
		{
			if (this._authSessionId == 0)
			{
				int offset = AuthProxyRequestHeader.StructSize + AcquireCredsRequest.StructSize;

				string? principalName = this.UserName;
				ReadOnlySpan<byte> authData = null;
				string? targetSpn = this.TargetSpn?.ToString();

				var channelBinding = this.ChannelBinding?.GetBytes();
				string? packageName = null;

				AcquireCredsRequest req = new AcquireCredsRequest()
				{
					requiredCaps = TranslateCaps(this.RequiredCapabilities),
					package = AuthPackageType.Negotiate,
					offPackageName = Measure((string)null, ref offset),
					offPrincipalName = Measure(principalName, ref offset),
					offTargetSpn = Measure(targetSpn, ref offset),
					cbAuthData = (ushort)authData.Length,
					offAuthData = Measure(authData, ref offset),
					cbAuthToken = (ushort)token.Length,
					offAuthToken = Measure(token, ref offset),
					cbChannelBinding = (ushort)(channelBinding?.Length ?? 0),
					offChannelBinding = Measure(channelBinding, ref offset),
				};
				AuthProxyRequestHeader hdr = new AuthProxyRequestHeader()
				{
					cbMessage = offset,
					messageType = AuthProxyMessageType.AcquireCreds
				};

				ByteWriter writer = new ByteWriter(offset);
				MemoryMarshal.Write(writer.Consume(AuthProxyRequestHeader.StructSize), hdr);
				MemoryMarshal.Write(writer.Consume(AcquireCredsRequest.StructSize), req);

				void WriteStringIf(string? str, ushort expectedOffset)
				{
					if (!string.IsNullOrEmpty(str))
					{
						Debug.Assert(writer.Position == expectedOffset);
						writer.WriteStringUni(str);
						writer.WriteUInt16LE(0);
					}
				}
				void WriteBytesIf(ReadOnlySpan<byte> bytes, ushort expectedOffset)
				{
					if (bytes.Length > 0)
					{
						Debug.Assert(writer.Position == expectedOffset);
						writer.WriteBytes(bytes);
					}
				}

				WriteStringIf(packageName, req.offPackageName);
				WriteStringIf(principalName, req.offPrincipalName);
				WriteStringIf(targetSpn, req.offTargetSpn);
				WriteBytesIf(authData, req.offAuthData);
				WriteBytesIf(token, req.offAuthToken);
				WriteBytesIf(channelBinding, req.offChannelBinding);

				this._authSocket.Send(writer.GetData().Span, SocketFlags.None);

				byte[] replyBuf = new byte[64 * 1024];
				int cbReply = this.ReceiveReply(ref replyBuf);

				// TODO: Validate the message

				ref var replyHdr = ref MemoryMarshal.AsRef<AuthProxyReplyHeader>(replyBuf);
				replyHdr.status.CheckAndThrow();

				this._isComplete = replyHdr.status == 0;

				ref var acquireReply = ref MemoryMarshal.AsRef<AcquireCredsReply>(replyBuf.AsSpan(AuthProxyReplyHeader.StructSize));

				this._authSessionId = acquireReply.sessionId;

				//this._credExpiry = DateTime.FromFileTime(acquireReply.tsCredExpiry);
				//this._contextExpiry = DateTime.FromFileTime(acquireReply.tsContextExpiry);

				this.HandleInitContextReply(replyBuf, ref replyHdr, ref acquireReply.initContext);

				return this._token;
			}
			else
			{
				int cb = AuthProxyRequestHeader.StructSize + InitializeContextRequest.StructSize;
				int offToken = cb;
				cb += token.Length;

				byte[] buf = new byte[cb];

				ref var reqhdr = ref MemoryMarshal.AsRef<AuthProxyRequestHeader>(buf);
				reqhdr = new AuthProxyRequestHeader
				{
					cbMessage = cb,
					messageType = AuthProxyMessageType.InitContext
				};

				ref var initreq = ref MemoryMarshal.AsRef<InitializeContextRequest>(buf.AsSpan(AuthProxyRequestHeader.StructSize));
				initreq = new InitializeContextRequest
				{
					authSessionId = this._authSessionId,
					cbToken = token.Length,
					offToken = offToken
				};

				token.CopyTo(buf.AsSpan(offToken, token.Length));

				var socket = this._authSocket;
				socket.Send(buf, SocketFlags.None);

				byte[] replyBuf = new byte[64 * 1024];
				int cbReply = this.ReceiveReply(ref replyBuf);


				// TODO: Validate the message

				ref var replyHdr = ref MemoryMarshal.AsRef<AuthProxyReplyHeader>(replyBuf);
				replyHdr.status.CheckAndThrow();

				// Extract token and expiration
				ref var initContextReply = ref MemoryMarshal.AsRef<InitializeContextReply>(replyBuf.AsSpan(AuthProxyReplyHeader.StructSize));

				HandleInitContextReply(replyBuf, ref replyHdr, ref initContextReply);


				return this._token;
			}
		}

		private void HandleInitContextReply(Span<byte> replyBuffer, ref AuthProxyReplyHeader reply, ref InitializeContextReply initContext)
		{
			this._token = replyBuffer.Slice(initContext.offToken, initContext.cbToken).ToArray();

			bool isComplete = reply.status == 0;
			if (isComplete)
			{
				this._isComplete = true;
				this._negcap = (SecurityCapabilities)initContext.flags & SecurityCapabilities.Rfc1509Mask;

				if (initContext.cbSessionKey != 0)
				{
					this._sessionKey = replyBuffer.Slice(initContext.offSessionKey, initContext.cbSessionKey).ToArray();
				}
			}
		}

		private int _authSessionId;
		private DateTime _credExpiry;
		private DateTime _contextExpiry;


		private int ReceiveReply(ref byte[] replyBuffer)
		{
			var socket = this._authSocket;
			int cbTotalRecv = 0;
			int cbChunk = 0;
			int cbMin = 4;
			while ((cbTotalRecv < cbMin) &&
				0 != (cbChunk = socket.Receive(replyBuffer.AsSpan(cbTotalRecv), SocketFlags.None)))
			{
				cbTotalRecv += cbChunk;
				if (cbTotalRecv >= 4)
				{
					cbMin = BinaryPrimitives.ReadInt32LittleEndian(replyBuffer);

					if (cbMin > replyBuffer.Length)
					{
						Array.Resize(ref replyBuffer, cbMin);
					}
				}
			}

			Debug.Assert(cbTotalRecv == cbMin);

			if (cbMin > AuthProxyReplyHeader.StructSize)
			{
				ref var replyHdr = ref MemoryMarshal.AsRef<AuthProxyReplyHeader>(replyBuffer);
				replyHdr.status.CheckAndThrow();
			}

			return cbTotalRecv;
		}

	}
}
