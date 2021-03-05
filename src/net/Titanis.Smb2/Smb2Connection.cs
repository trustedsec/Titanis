using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Security;
using Titanis.Smb2.Pdus;
using Titanis.Winterop;
using static Titanis.Smb2.Smb2Connection;

namespace Titanis.Smb2
{
	/// <summary>
	/// Represents a connection to an SMB server.
	/// </summary>
	public partial class Smb2Connection
	{
		internal Smb2Connection(
			Smb2ConnectionOptions options,
			string serverName,
			Smb2Channel channel,
			ISmb2ConnectionOwner? owner
			)
		{
			this.Channel = channel;
			this.owner = owner;

			// TODO: Warn if the above sizes are too small (< 4096) <182>

			this.ClientSecurityMode = options.SecurityMode;
			this.Options = options;
			this.ServerName = serverName;

			// TODO: Add validation of server (cf. ServerList)
		}

		public const int DefaultReceiveBufferSize = 64 * 1024;

		public Smb2ConnectionOptions Options { get; }
		internal readonly Smb2Channel Channel;
		private ValidateNegotiateInfo? _secneginfo;
		internal readonly ISmb2ConnectionOwner? owner;
		public bool IsConnected { get; private set; } = true;

		internal void OnNegotiated(
			Smb2NegotiateResponse pdu,
			ValidateNegotiateInfo? segneginfo
			)
		{
			if (pdu.body.dialect >= Smb2Dialect.Smb3_1_1 && segneginfo != null)
				throw new ArgumentException("SMB 3.1.1+ does not support secure negotiation.");

			this.MaxTransactSize = (int)pdu.body.maxTransactSize;
			this.MaxReadSize = (int)pdu.body.maxReadSize;
			this.MaxWriteSize = (int)pdu.body.maxWriteSize;
			this.ServerGuid = pdu.body.serverGuid;
			this.GssToken = pdu.secToken;

			this._secneginfo = segneginfo;

			this.Dialect = pdu.body.dialect;

			this.ServerConnectTimeUtc = DateTime.FromFileTimeUtc(pdu.body.systemTime);

			var options = this.Options;
			if (!options.SupportedDialects.Contains(pdu.body.dialect))
				throw new SmbNegotiationException(SmbNegotiationFailureType.Dialect);

			this.Capabilities = (options.Capabilities & pdu.body.caps);
			this.ServerSecurityMode = pdu.body.securityMode;

			Smb2ReadOptions allowedReadOptions = Smb2ReadOptions.Unbuffered;
			if (pdu.body.dialect >= Smb2Dialect.Smb3_1_1)
				allowedReadOptions |= Smb2ReadOptions.Compressed;
			this.AllowedReadOptions = allowedReadOptions;

			if (this.Dialect >= Smb2Dialect.Smb3_0)
			{
				this.SigningAlgorithm = SigningAlgorithm.AesCmac;
			}

			if (this.Dialect >= Smb2Dialect.Smb3_1_1)
			{
				if (pdu.preauthSalt == null)
					throw new SmbNegotiationException(SmbNegotiationFailureType.MissingPreauthSalt);

				// TODO: Check preauth algorithms

				if (!pdu.cipherAlgs.IsNullOrEmpty())
					this.CipherId = pdu.cipherAlgs![0];
				else if (options.SupportedCiphers.Length > 0)
					this.CipherId = options.SupportedCiphers[0];

				if (!pdu.signingAlgs.IsNullOrEmpty())
					this.SigningAlgorithm = pdu.signingAlgs![0];

				// TODO: Alert if no encryption

				this.CompressionAlgorithms = pdu.compressionAlgs;
				this.CompressionCaps = pdu.compressionCaps;
				this.RdmaTransforms = pdu.rdmaTransforms;

				// TODO: QUIC TLS

				this.SupportedCiphers = pdu.cipherAlgs;
			}
		}

		/// <summary>
		/// Connects to an SMB2 server.
		/// </summary>
		/// <param name="stream">Stream connected to server</param>
		/// <param name="serverName">Server host name</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns>An <see cref="Smb2Connection"/> representing the connection</returns>
		/// <exception cref="ArgumentNullException"><paramref name="serverEP"/> is <see langword="null"/>.</exception>
		public static async Task<Smb2Connection> ConnectToAsync(
			Stream stream,
			string serverName,
			Smb2ConnectionOptions options,
			ISmb2ConnectionOwner? owner,
			CancellationToken cancellationToken)
		{
			if (stream is null)
				throw new ArgumentNullException(nameof(stream));
			if (options is null) throw new ArgumentNullException(nameof(options));
			if (!(stream.CanRead && stream.CanWrite))
				throw new ArgumentException(Messages.Smb2_BadStream, nameof(stream));

			options.VerifyConnectionReady();

			var maxDialect = options.SupportedDialects.Max();
			bool smb3 = maxDialect >= Smb2Dialect.Smb3_0;
			Smb2Capabilities capMask = maxDialect switch
			{
				Smb2Dialect.Smb3_1_1 => Smb2CapabilitySets.Version_3_1_1,
				Smb2Dialect.Smb3_0_2 => Smb2CapabilitySets.Version_3_0_2,
				Smb2Dialect.Smb3_0 => Smb2CapabilitySets.Version_3_0,
				Smb2Dialect.Smb2_1 => Smb2CapabilitySets.Version_2_1,
				Smb2Dialect.Smb2_0_2 => Smb2CapabilitySets.Version_2_0_2,
				_ => Smb2Capabilities.None,
			};

			// TODO: This isn't what the spec says
			//// Only send with SMB 3.x
			Smb2Capabilities caps = capMask;
			//Smb2Capabilities caps = (smb3 || !this.EnforcesVersionCompliance)
			//	? DefaultSmb3Caps
			//	: Smb2Capabilities.None
			//	;

			Smb2NegotiateRequest req = new Smb2NegotiateRequest(options.SupportedDialects)
			{
				body = new Smb2NegotiateRequestBody
				{
					structureSize = 36,
					// dialectCount,
					securityMode = options.SecurityMode,
				},
			};
			if (
				!options.EnforcesVersionCompliance
				|| (maxDialect >= Smb2Dialect.Smb2_1)
				)
			{
				if (
					(maxDialect >= Smb2Dialect.Smb2_1)
					&& !options.EnforcesVersionCompliance
					&& (options.ClientGuid == new Guid())
					)
				{
					// Must have a ClientGuid
					options.ClientGuid = Guid.NewGuid();
				}
				req.body.clientGuid = options.ClientGuid;
			}

			// TODO: Support RDMA

			byte[]? preauthIntegrityValue = null;

			if (maxDialect >= Smb2Dialect.Smb2_1)
				req.body.clientGuid = options.ClientGuid;

			// UNDONE: Windows sends capabilities regardless
			//if (maxDialect >= Smb2Dialect.Smb3_0)
			req.body.caps = caps;

			HashAlgorithm? preauthHashAlg = null;

			if (
				(maxDialect >= Smb2Dialect.Smb3_1_1)
				|| !options.EnforcesVersionCompliance
				)
			{
				// TODO: Which of these is required?

				List<Smb2NegotiateContext> contexts = new List<Smb2NegotiateContext>(4);

				// TODO: Create correct alg based on configuration
				preauthHashAlg = SHA512.Create();
				preauthIntegrityValue = new byte[preauthHashAlg.HashSize / 8];
				var salt = new byte[options.PreauthSaltLength];
				Smb2Client.rng.GetBytes(salt);
				contexts.Add(new Pdus.PreauthIntegrityCapsContext(options.SupportedHashAlgs!, salt));

				if (!options.SupportedCiphers.IsNullOrEmpty())
					contexts.Add(new Pdus.CipherCapsContext(options.SupportedCiphers!));

				if (!options.SupportedCompressionAlgorithms.IsNullOrEmpty())
					contexts.Add(new Pdus.CompressionCapsContext(options.CompressionCaps, options.SupportedCompressionAlgorithms!));

				if (!options.SupportedSigningAlgorithms.IsNullOrEmpty())
					contexts.Add(new Pdus.SigningCapsContext(options.SupportedSigningAlgorithms!));

				if (!string.IsNullOrEmpty(serverName))
					contexts.Add(new Pdus.NetNameContext(serverName));

				// RDMA
				if (!options.SupportedRdmaTransforms.IsNullOrEmpty())
					contexts.Add(new Pdus.RdmaTransformCapsContext(options.SupportedRdmaTransforms!));

				req.contexts = contexts.ToArray();
			}

			Smb2Channel? channel = null;
			try
			{
				channel = new Smb2Channel(stream, DefaultReceiveBufferSize);
				Smb2Connection conn = new Smb2Connection(
					options,
					serverName,
					channel,
					null);
				channel.OnAttaching(conn);

				// Send NEGOTIATE_REQUEST
				Smb2PduFlags pduFlags = (maxDialect >= Smb2Dialect.Smb3_1_1)
					? (Smb2PduFlags)((int)Smb2Priority.Negotiate << 4)
					: 0;
				var negReqPduInfo = conn.BuildPdu(req, pduFlags, null, false);
				await stream.WriteAsync(negReqPduInfo.frameBytes, cancellationToken).ConfigureAwait(false);
				if (preauthHashAlg != null)
					preauthIntegrityValue = UpdatePreauthHash(preauthIntegrityValue!, preauthHashAlg, negReqPduInfo.pduBytes.Span);

				// Read NEGOTIATE_RESPONSE
				var resp = await channel.ReadFrameAsync(cancellationToken).ConfigureAwait(false);
				if (
					(resp.pdu.Command != Smb2Command.Negotiate)
					)
					throw new FormatException(Messages.Smb2_NoNegotiateResponse);
				if (preauthHashAlg != null)
					preauthIntegrityValue = UpdatePreauthHash(preauthIntegrityValue!, preauthHashAlg, resp.pduBytes.Span);

				// TODO: Check signing
				// TODO: Check max transact size etc.
				Smb2NegotiateResponse negresp = (Smb2NegotiateResponse)resp.pdu;
				if (options.RequiresMessageSigning && (0 == negresp.body.securityMode))
					throw new SmbNegotiationException(SmbNegotiationFailureType.Sign);

				ValidateNegotiateInfo? secneginfo =
					(options.RequiresSecureNegotiate && negresp.body.dialect < Smb2Dialect.Smb3_1_1) ? new ValidateNegotiateInfo()
					{
						hdr = new ValidateNegotiateInfoFixed
						{
							capabilities = req.body.caps,
							guid = req.body.clientGuid,
							securityMode = req.body.securityMode,
						},
						dialects = req.Dialects
					}
					: null;

				conn.OnNegotiated(negresp, secneginfo);
				conn._preauthHashAlg = preauthHashAlg;
				conn._preauthIntegrityValue = preauthIntegrityValue;

				channel.Start();
				channel = null;

				return conn;
			}
			finally
			{
				channel?.Dispose();
			}
		}

		internal static byte[] UpdatePreauthHash(
			byte[] preauthIntegrityValue,
			HashAlgorithm preauthHashAlg,
			ReadOnlySpan<byte> pduBytes)
		{
			Debug.Assert(preauthIntegrityValue != null);
			byte[] hashInput = new byte[preauthIntegrityValue.Length + pduBytes.Length];
			preauthIntegrityValue.CopyTo(hashInput, 0);
			pduBytes.CopyTo(new Span<byte>(hashInput, preauthIntegrityValue.Length, hashInput.Length - preauthIntegrityValue.Length));
			preauthIntegrityValue = preauthHashAlg.ComputeHash(hashInput);
			return preauthIntegrityValue;
		}

		internal HashAlgorithm? _preauthHashAlg;
		internal byte[]? _preauthIntegrityValue;

		public byte[] GssToken { get; set; }

		public int MaxTransactSize { get; set; }
		public int MaxReadSize { get; set; }
		public int MaxWriteSize { get; set; }

		public Smb2ReadOptions AllowedReadOptions { get; private set; }

		public Guid ServerGuid { get; set; }
		// [MS-SMB2] <181>
		public bool IsConnectedToSelf => this.ServerGuid == this.Options.ClientGuid;

		public Smb2SecurityMode ClientSecurityMode { get; set; }
		public bool RequiresSigning => 0 != (this.ServerSecurityMode & Smb2SecurityMode.SigningRequired);
		public string ServerName { get; set; }
		public Smb2Dialect Dialect { get; set; }

		public Smb2Capabilities Capabilities { get; set; }
		public bool SupportsFileLeasing => 0 != (this.Capabilities & Smb2Capabilities.Leasing);
		public bool SupportsMultiCredit => 0 != (this.Capabilities & Smb2Capabilities.LargeMtu);
		public bool SupportsDirectoryLeasing => 0 != (this.Capabilities & Smb2Capabilities.DirectoryLeasing);
		public bool SupportsMultiChannel => 0 != (this.Capabilities & Smb2Capabilities.MultiChannel);
		public bool SupportsPersistentHandles => 0 != (this.Capabilities & Smb2Capabilities.PersistentHandles);
		public bool SupportsEncryption => 0 != (this.Capabilities & Smb2Capabilities.Encryption) || (this.CipherId != Cipher.None);
		public bool SupportsNotifications => 0 != (this.Capabilities & Smb2Capabilities.Notifications);

		public IReadOnlyList<Cipher>? SupportedCiphers { get; private set; }

		public DateTime ServerConnectTimeUtc { get; private set; }

		public Smb2SecurityMode ServerSecurityMode { get; set; }
		public Cipher CipherId { get; set; }
		public SigningAlgorithm SigningAlgorithm { get; private set; }
		public CompressionAlgorithm[]? CompressionAlgorithms { get; set; }
		public CompressionCaps CompressionCaps { get; set; }
		public RdmaTransformId[] RdmaTransforms { get; private set; }

		public bool SupportsChainedCompression => 0 != (this.CompressionCaps & CompressionCaps.Chained);

		private readonly List<Smb2Session> _openSessions = new List<Smb2Session>();
		private readonly Dictionary<ulong, Smb2Session> _openSessionsById = new Dictionary<ulong, Smb2Session>();
		internal Smb2Session? GetSession(ulong sessionId)
		{
			this._openSessionsById.TryGetValue(sessionId, out var session);
			return session;
		}

		private Smb2SessionRequest BuildSessionReq(
			AuthClientContext authContext,
			byte[] preauthIntegrityValue
			)
		{
			Smb2SessionCaps sessionCaps = ((this.Capabilities & Smb2Capabilities.Dfs) != 0) ? Smb2SessionCaps.Dfs : 0;
			Smb2SessionRequest req = new Smb2SessionRequest
			{
				body = new Smb2SessionRequestBody
				{
					flags = Smb2SessionReqFlags.None,
					securityMode = (byte)this.ClientSecurityMode,
					caps = sessionCaps,
					channel = 0,
					prevSessionId = 0
				},
				//secToken = secToken.ToArray(),
				preauthHashAlg = this._preauthHashAlg,
				preauthIntegrityValue = preauthIntegrityValue
			};
			if (authContext != null)
				req.secToken = authContext.Token.ToArray();
			return req;
		}

		public Task<Smb2Session> AuthenticateAsync(
			AuthClientContext authContext,
			bool mustEncrypt,
			CancellationToken cancellationToken
			)
		{
			return this.AuthenticateAsync(authContext, mustEncrypt, null, 0, cancellationToken);
		}
		internal async Task<Smb2Session> AuthenticateAsync(
			AuthClientContext authContext,
			bool mustEncrypt,
			Smb2Session? existingSession,
			ushort channelId,
			CancellationToken cancellationToken
			)
		{
			if (authContext is null)
				throw new ArgumentNullException(nameof(authContext));

			// TODO: Allow caller to specify client-initiated authentication
			//authContext.Initialize();
			authContext.Initialize(this.GssToken);
			var preauthIntegrityValue = this._preauthIntegrityValue;
			ulong sessionId = existingSession?.SessionId ?? 0;
			Smb2SessionResponse sessionResp;
			var cryptInfo = existingSession?.CryptInfo;
			var primaryChannel = existingSession?.primaryChannelBinding;
			do
			{
				Smb2SessionRequest req = BuildSessionReq(authContext, preauthIntegrityValue);
				//req.pduhdr.ChannelSequence = channelId;
				req.SessionId = sessionId;
				if (existingSession != null)
				{
					//req.body.prevSessionId = existingSession.SessionId;
					req.pduhdr.flags |= Smb2PduFlags.Signed;
					req.body.flags |= Smb2SessionReqFlags.Binding;
				}
				sessionResp = (Smb2SessionResponse)await this.SendSyncPduAsync(req, cancellationToken, cryptInfo, primaryChannel, false).ConfigureAwait(false);

				// [MS-SMB2] § 3.2.5.3.1

				sessionId = sessionResp.pduhdr.sessionId;
				if (sessionResp.pduhdr.status == Ntstatus.STATUS_MORE_PROCESSING_REQUIRED)
				{
					preauthIntegrityValue = req.preauthIntegrityValue_resp;
					authContext.Initialize(sessionResp.secToken);
				}
				else if (sessionResp.pduhdr.status == 0)
				{
					preauthIntegrityValue = req.preauthIntegrityValue;
					authContext.Initialize(sessionResp.secToken);

					// TODO: Verify server MIC
					break;
				}
				else
				{
					throw new NtstatusException(sessionResp.pduhdr.status);
				}
			} while (true);

			// [MS-SMB2] § 3.2.5.3.1
			// HACK: The response isn't signed if anonymous session
			// Windows 2025 (and probably others) does not set the Guest or Anonymous bits
			if (this.Dialect >= Smb2Dialect.Smb3_1_1 && !sessionResp.IsSigned && !authContext.IsAnonymous)
				throw new ProtocolViolationException("The session setup response was not signed as required.");

			mustEncrypt |= 0 != (sessionResp.body.flags & Smb2SessionFlags.EncryptData);

			// TODO: Check PreAuthSessionTable

			// SMB does not use the auth context encryption or signing, so don't check

			if (existingSession != null)
			{
				existingSession.BindChannel(
					this,
					preauthIntegrityValue,
					this._secneginfo
					);
				return existingSession;
			}
			else
			{
				var session = new Smb2Session(
					this,
					sessionId,
					authContext,
					(this.RequiresSigning || this.Options.RequiresMessageSigning) && (0 == (sessionResp.body.flags & Smb2SessionFlags.EncryptData)),
					mustEncrypt,
					preauthIntegrityValue,
					this._secneginfo
					);

				if (sessionResp.IsSigned)
				{
					var pduBytes = sessionResp.pduBytes;

					session.CryptInfo.Verify(
						session.primaryChannelBinding.signingKey,
						pduBytes.Span,
						sessionResp.pduhdr.messageId,
						Smb2SignFlags.Server,
						pduBytes.Span.Slice(Smb2PduSyncHeader.StructSize - Smb2PduSyncHeader.SigSize, Smb2PduSyncHeader.SigSize));
				}

				this._secneginfo = null;

				lock (this._openSessions)
				{
					this._openSessions.Add(session);
					this._openSessionsById.Add(session.SessionId, session);
				}

				return session;
			}
		}

		public bool AutoClose { get; set; }

		internal void OnSessionEnded(Smb2Session session)
		{
			lock (this._openSessions)
			{
				this._openSessions.Remove(session);
				this._openSessionsById.Remove(session.SessionId);
			}

			if (this.AutoClose && this._openSessions.Count == 0)
				this.Dispose();
		}

		#region Requests

		/// <summary>
		/// Represents a pending request.
		/// </summary>
		class PendingRequest
		{
			internal readonly ulong messageId;
			internal readonly ulong sessionId;

			/// <summary>
			/// Async ID reported by server.
			/// </summary>
			/// <remarks>
			/// This value is returned by the server with a STATUS_PENDING response.
			/// This member is positioned at the beginning of the structure to
			/// avoid having padding
			/// on the off chance this code is run in a 32-bit environment.
			/// </remarks>
			internal ulong asyncId;
			internal CancellationTokenRegistration cancelReg;
			internal bool isCanceled;
			internal bool isAsync;
			internal readonly TaskCompletionSource<Smb2Message> taskSource;
			internal readonly Smb2SessionCryptInfoBase? cryptInfo;
			internal readonly Smb2ChannelBindingInfo channelBinding;
			internal readonly Smb2Pdu requestPdu;
			internal readonly bool encrypted;

			public PendingRequest(
				ulong messageId,
				ulong sessionId,
				Smb2SessionCryptInfoBase? cryptInfo,
				Smb2ChannelBindingInfo channelBinding,
				Smb2Pdu requestPdu,
				bool encrypted
				)
			{
				this.messageId = messageId;
				this.sessionId = sessionId;
				this.cryptInfo = cryptInfo;
				this.channelBinding = channelBinding;
				this.requestPdu = requestPdu;
				this.encrypted = encrypted;
				this.taskSource = new TaskCompletionSource<Smb2Message>();
			}
		}

		/// <summary>
		/// Tracks pending requests by <see cref="PduInfo.messageId"/>.
		/// </summary>
		private readonly Dictionary<ulong, PendingRequest> _pendingRequests = new Dictionary<ulong, PendingRequest>();

		/// <summary>
		/// Describes a PDU.
		/// </summary>
		/// <seealso cref="BuildPdu(Smb2Pdu, Smb2PduFlags, Smb2SessionCryptInfoBase?, bool)"/>
		internal struct PduInfo
		{
			internal ulong messageId;
			internal Memory<byte> frameBytes;
			internal Memory<byte> pduBytes;
			internal Memory<byte> sigBytes;
		}

		void OnRequestCanceled(PendingRequest pendingReq)
		{
			lock (this._pendingRequests)
				this._pendingRequests.Remove(pendingReq.messageId);

			pendingReq.isCanceled = true;
			if (pendingReq.asyncId != 0)
			{
				_ = this.SendSyncPduAsync(
					new Smb2CancelRequest() { SessionId = pendingReq.sessionId, AsyncId = pendingReq.asyncId, OriginalMessageId = pendingReq.messageId },
					CancellationToken.None,
					pendingReq.cryptInfo,
					pendingReq.channelBinding,
					pendingReq.encrypted
					);
			}
			pendingReq.taskSource.TrySetCanceled();
		}
		#endregion

		const int PriorityShift = 4;

		#region Credits
		private object _creditLock = new object();
		private int _credits = 1;

		private readonly int _preferredCredits = 33; // Windows 10 default

		private long _nextMessageId = 0;
		private ulong GetNextMessageId(int credits)
		{
			Debug.Assert(this._credits > 0);
			Debug.Assert(credits > 0);
			Debug.Assert(credits <= this._credits);

			var id = this._nextMessageId;
			this._credits -= credits;
			Interlocked.Add(ref this._nextMessageId, credits);
			return (ulong)id;
		}
		#endregion

		/// <summary>
		/// Sends a synchronous PDU and waits for a response.
		/// </summary>
		/// <param name="pdu">PDU to send</param>
		/// <param name="cryptInfo">Cryptographic info</param>
		/// <returns>The <see cref="Smb2Message"/> sent by the server in response.</returns>
		/// <exception cref="FormatException"></exception>
		internal async Task<Smb2Pdu> SendSyncPduAsync(
			Smb2Pdu pdu,
			CancellationToken cancellationToken,
			Smb2SessionCryptInfoBase? cryptInfo,
			Smb2ChannelBindingInfo channelBinding,
			bool encrypt
			)
		{
			cancellationToken.ThrowIfCancellationRequested();

			// UNDONE: SMB doesn't use the encryption capability of the auth context
			//if (encrypt && (cryptInfo == null || !cryptInfo.SupportsEncryption))
			if (encrypt && (cryptInfo == null))
				throw new InvalidOperationException("Connection does not support the requested encryption.");

			Smb2PduFlags pduFlags = pdu.pduhdr.flags;

			if (cryptInfo != null && cryptInfo.ShouldSign && !encrypt)
				pduFlags |= Smb2PduFlags.Signed;
			if (this.Dialect >= Smb2Dialect.Smb3_1_1)
				pduFlags |= (Smb2PduFlags)((int)(pdu.Priority & Smb2Priority.Mask) << PriorityShift);

			var pduInfo = BuildPdu(pdu, pduFlags, cryptInfo, encrypt);

			if (0 != (pduFlags & Smb2PduFlags.Signed))
			{
				Debug.Assert(!encrypt);

				Smb2SignFlags signFlags = (pdu is Smb2CancelRequest cancel) ? Smb2SignFlags.CancelRequest : Smb2SignFlags.None;
				cryptInfo.Sign(channelBinding.signingKey, pduInfo.pduBytes.Span, pduInfo.messageId, signFlags, pduInfo.sigBytes.Span);
			}

			pdu.OnSending(pduInfo.pduBytes.Span);

			bool isCancel = (pdu.Command == Smb2Command.Cancel);
			PendingRequest pendingReq = new PendingRequest(
				pduInfo.messageId,
				pdu.pduhdr.sessionId,
				cryptInfo,
				channelBinding,
				pdu,
				encrypt);

			if (!isCancel)
			{
				pendingReq.cancelReg = cancellationToken.Register(() => this.OnRequestCanceled(pendingReq));

				lock (this._pendingRequests)
					this._pendingRequests.Add(pduInfo.messageId, pendingReq);
			}

			await this.Channel.SendFrameAsync(pduInfo.frameBytes).ConfigureAwait(false);

			if (isCancel)
				return null;

			var resp = await pendingReq.taskSource.Task.ConfigureAwait(false);
			if (resp.hdr.command != pdu.Command)
				throw new FormatException(Messages.Smb2_CommandResponseMismatch);

			return resp.pdu;
		}

		internal PduInfo BuildPdu(
			Smb2Pdu pdu,
			Smb2PduFlags pduFlags,
			Smb2SessionCryptInfoBase? cryptInfo,
			bool encrypt)
		{
			ByteWriter writer = new ByteWriter();
			int offPduSize = writer.AllocDSHeader();

			int offXform = writer.Position;
			if (encrypt)
				writer.Alloc(Smb2TransformHeader.StructSize);

			int offPdu = writer.Position;

			ref Smb2PduHeaderBuffer hdrbuf = ref pdu.pduhdrbuf;
			ref Smb2PduSyncHeader hdr = ref hdrbuf.sync;
			hdr.protocolId = Smb2PduSyncHeader.ValidSignature;
			hdr.structSize = Smb2PduSyncHeader.StructSize;
			hdr.command = pdu.Command;
			hdr.flags = pduFlags;

			var cancelReq = pdu as Smb2CancelRequest;
			bool isCancel = (cancelReq != null);
			if (isCancel)
			{
				if (hdrbuf.async.asyncId != 0)
				{
					hdrbuf.async.messageId = 0;
					hdrbuf.async.flags |= Smb2PduFlags.AsyncCommand;
				}
			}
			else
			{
				// Overwrites AsyncId
				hdr.processId = Smb2PduSyncHeader.ProcessId;
			}

			// UNDONE: Negotiate populates the credit fields
			//if (hdr.command != Smb2Command.Negotiate)
			if (!isCancel)
			{
				hdr.creditCharge = pdu.CreditCharge;
				lock (this._creditLock)
				{
					if (hdr.creditCharge > this._credits && pdu.CanReducePayload)
					{
						pdu.AdjustPayload((hdr.creditCharge - this._credits) * Smb2Pdu.CreditChunkSize);
						hdr.creditCharge = pdu.CreditCharge;
					}

					if (hdr.creditCharge > this._credits)
						throw new InvalidOperationException("The packet is too large to send with the available credits.");
					hdr.messageId = this.GetNextMessageId(hdr.creditCharge);
				}
				if (this._credits < this._preferredCredits)
					hdr.creditReqResp = (ushort)(this._preferredCredits - this._credits);
			}
			else
			{
				// Nothing special for CANCEL
			}

			writer.Write(hdr);
			pdu.WriteTo(writer);

			var frameBytes = writer.GetData();
			int pduSize = writer.Position - offPdu;
			if (encrypt)
			{
				// [MS-SMB2] § 3.1.4.3 - Encrypting the message
				ref Smb2TransformHeader xform = ref MemoryMarshal.AsRef<Smb2TransformHeader>(frameBytes.Span.Slice(offXform, Smb2TransformHeader.StructSize));
				xform.protocolId = Smb2TransformHeader.Smb2TransformSignature;
				xform.originalMessageSize = pduSize;
				xform.sessionId = hdr.sessionId;
				xform.flags_encAlgo = 1;
				Span<byte> nonceBytes = xform.NonceBytes.Slice(0, cryptInfo.NonceSize);
				Smb2Client.rng.GetBytes(nonceBytes);

				var authData = frameBytes.Span.Slice(offXform + 20, 8 * 4);
				cryptInfo.Encrypt(
					nonceBytes,
					frameBytes.Span.Slice(offPdu),
					authData,
					frameBytes.Span.Slice(offPdu),
					xform.SignatureBytes
					);
			}



			int offEnd = writer.Position;
#if DEBUG
			offEnd = frameBytes.Length;
#endif
			writer.SetPosition(offPduSize);
			writer.WriteInt32LE(BinaryPrimitives.ReverseEndianness(offEnd - offXform));
			var pduBytes = frameBytes[4..];

			return new PduInfo
			{
				messageId = hdr.messageId,
				frameBytes = frameBytes,
				pduBytes = pduBytes,
				sigBytes = pduBytes.Slice(Smb2PduSyncHeader.StructSize - Smb2PduSyncHeader.SigSize, Smb2PduSyncHeader.SigSize)
			};
		}

		internal Smb2Message ParsePdu(Memory<byte> pduBytes)
		{
			var sig = BinaryPrimitives.ReadUInt32LittleEndian(pduBytes.Span);
			if (sig == Smb2TransformHeader.Smb2TransformSignature)
			{
				ref var xform = ref MemoryMarshal.AsRef<Smb2TransformHeader>(pduBytes.Span);
				var session = this.GetSession(xform.sessionId);
				if (session == null)
					throw new ProtocolViolationException("The session ID in the transform header does not match an open session.");

				var cryptInfo = session.CryptInfo;
				if (cryptInfo == null)
					throw new ProtocolViolationException("The session does not have the cryptographic info to decrypt an encrypted packet.");

				var nonceBytes = xform.NonceBytes.Slice(0, cryptInfo.NonceSize);
				var wrapped = pduBytes.Span.Slice(Smb2TransformHeader.StructSize, xform.originalMessageSize);
				var authData = pduBytes.Span.Slice(20, 8 * 4);
				cryptInfo.Decrypt(
					nonceBytes,
					wrapped,
					xform.SignatureBytes,
					wrapped,
					authData
					);
				pduBytes = pduBytes.Slice(Smb2TransformHeader.StructSize, xform.originalMessageSize);
			}

			ByteMemoryReader reader = new ByteMemoryReader(pduBytes);
			ref readonly Smb2PduSyncHeader hdr = ref reader.ReadSmb2PduSyncHeader();

			int size = BinaryPrimitives.ReadUInt16LittleEndian(reader.PeekBytes(2));
			Smb2Pdu pdu = CreatePdu(hdr, size);
			if (this._pendingRequests.TryGetValue(hdr.messageId, out var request))
				pdu.request = request.requestPdu;

			try
			{
				if (pdu != null)
				{
					pdu.ReadFrom(reader, in hdr);
				}
				else
					throw new NotImplementedException();
			}
			catch
			{
				pdu = null;
			}

			// TODO: Do message processing like checking signature
			if (pdu.IsSigned)
			{
				if (request != null)
				{
					var cryptInfo = request.cryptInfo;
					var channelBinding = request.channelBinding;
					if (cryptInfo != null && channelBinding != null)
					{
						Smb2SignFlags signFlags = Smb2SignFlags.Server;
						if (pdu.Command == Smb2Command.Cancel)
							signFlags |= Smb2SignFlags.CancelRequest;

						var sigBytes = pduBytes.Span.Slice(Smb2PduSyncHeader.StructSize - Smb2PduSyncHeader.SigSize, Smb2PduSyncHeader.SigSize);
						cryptInfo.Verify(channelBinding.signingKey, pduBytes.Span, pdu.pduhdr.messageId, signFlags, sigBytes);
					}
					else
					{
						// This should only happen on SessionSetup response when authentication is complete.

						Debug.Assert(pdu.Command == Smb2Command.SessionSetup);

						var sessionResp = (Smb2SessionResponse)pdu;
						// Used for message verification
						sessionResp.pduBytes = pduBytes;
					}
				}
			}

			this._credits += pdu.pduhdr.creditReqResp;

			return new Smb2Message(
				pdu,
				pduBytes
			);
		}

		private static bool IsError(Ntstatus status, Smb2Command command)
		{
			bool isNotError = ((status == 0)
				|| ((command == Smb2Command.SessionSetup) && (status == Ntstatus.STATUS_MORE_PROCESSING_REQUIRED))
				|| ((command == Smb2Command.Ioctl) && (status == Ntstatus.STATUS_BUFFER_OVERFLOW))
				|| ((command == Smb2Command.Read) && (status == Ntstatus.STATUS_BUFFER_OVERFLOW))
				|| ((command == Smb2Command.Ioctl) && (status == Ntstatus.STATUS_INVALID_PARAMETER))
				|| ((command == Smb2Command.ChangeNotify) && (status == Ntstatus.STATUS_NOTIFY_ENUM_DIR))
				// HACK: [MS-SMB2] § 3.3.5.10
				|| ((command == Smb2Command.ChangeNotify) && (status == Ntstatus.STATUS_NOTIFY_CLEANUP))
				);
			return !isNotError;
		}

		private static Smb2Pdu CreatePdu(in Smb2PduSyncHeader hdr, int size)
		{
			Smb2Pdu pdu =
				IsError(hdr.status, hdr.command) ? new Smb2ErrorResponse()
				: hdr.command switch
				{
					Smb2Command.Negotiate => new Smb2NegotiateResponse(),
					Smb2Command.SessionSetup => new Smb2SessionResponse(),
					Smb2Command.Logoff => new Smb2LogoffResponse(),
					Smb2Command.TreeConnect => new Smb2TreeConnectResponse(),
					Smb2Command.TreeDisconnect => new Smb2TreeDisconnectResponse(),
					Smb2Command.Create => new Smb2CreateResponse(),
					Smb2Command.Close => new Smb2CloseResponse(),
					Smb2Command.Flush => new Smb2FlushResponse(),
					Smb2Command.Read => new Smb2ReadResponse(),
					Smb2Command.Write => new Smb2WriteResponse(),
					Smb2Command.Ioctl => new Smb2IoctlResponse(),
					Smb2Command.Echo => new Smb2EchoResponse(),
					Smb2Command.SetInfo => new Smb2SetInfoResponse(),
					Smb2Command.QueryDirectory => new Smb2QueryDirResponse(),
					Smb2Command.ChangeNotify => new Smb2ChangeNotifyResponse(),
					Smb2Command.QueryInfo => new Smb2QueryInfoResponse(),
					Smb2Command.OplockBreak => (size == Smb2LeaseBreak.LeaseBreakSize ? new Smb2LeaseBreak() : new Smb2OplockBreak()),
					_ => throw new NotImplementedException()
				};
			pdu.pduhdr = hdr;
			// TODO: Handle all PDUs
			//case Smb2Command.Lock:
			//	break;
			//case Smb2Command.QueryInfo:
			//	break;
			//case Smb2Command.SetInfo:
			//	break;
			//case Smb2Command.Cancel:
			//	break;
			//case Smb2Command.OplockBreak:
			//	break;
			//default:
			//	break;

			return pdu;
		}

		internal Task HandlePdu(Smb2Message msg)
		{
			if (msg.hdr.command == Smb2Command.OplockBreak && msg.pdu != null)
			{
				// TODO: Once leasing functionality is supported, take the appropriate steps
				// For now, just acknowledge

				var oplockBreak = (ISmb2OplockBreak)msg.pdu;
				if (oplockBreak.IsAckRequired)
				{
					// TODO: ???? Windows doesn't acknowledge, and instead closes the file handle
					// For now, do nothing
				}
			}
			else if (this._pendingRequests.TryGetValue(msg.hdr.messageId, out PendingRequest pendingReq))
			{
				bool pend = false;

				pendingReq.requestPdu.OnResponse(msg);

				if (!pendingReq.isCanceled)
				{
					if (msg.pdu == null)
					{
						// This only ever happens if there was an error parsing a packet
						pendingReq.taskSource.SetException(new ProtocolViolationException("The server sent a response that could not be processed."));
					}
					else if (msg.pdu is Smb2ErrorResponse error)
					{
						if (
							((msg.hdr.flags & Smb2PduFlags.AsyncCommand) != 0)
							&& (msg.hdr.status == Ntstatus.STATUS_PENDING)
							)
						{
							pendingReq.asyncId = msg.pdu.pduhdrbuf.async.asyncId;
							pend = true;
						}
						else
						{
							pendingReq.taskSource.SetException(error.CreateException(msg.hdr.status));
						}
					}
					else
					{
						pendingReq.taskSource.SetResult(msg);
					}
				}

				if (!pend)
				{
					pendingReq.cancelReg.Unregister();
					lock (this._pendingRequests)
						this._pendingRequests.Remove(msg.hdr.messageId);

				}
				else
					Debug.Assert(!pendingReq.taskSource.Task.IsCompleted);
			}
			else
			{
				// TODO: Notify bad message ID
			}

			return Task.CompletedTask;
		}

		internal void OnChannelStopping()
		{
			lock (this._pendingRequests)
			{
				foreach (var request in this._pendingRequests.Values)
				{
					request.taskSource.TrySetException(new OperationCanceledException("The underlying SMB connection was disconnected."));
				}
			}

			this.IsConnected = false;
		}

		internal void OnChannelAborting(Exception? exception)
		{
			this.IsConnected = false;
			this.owner?.OnConnectionAborted(this, this.Channel.Exception);
		}
	}
	public partial class Smb2Connection : IDisposable, IAsyncDisposable
	{
		#region Dispose pattern
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					this.Channel.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~Smb2Connection()
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

		public async ValueTask DisposeAsync()
		{
			if (!this.disposedValue)
			{
				await this.Channel.DisposeAsync().ConfigureAwait(false);
				this.disposedValue = true;
			}
		}
		#endregion
	}
}