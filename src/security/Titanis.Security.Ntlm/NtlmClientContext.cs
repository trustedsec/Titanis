using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Titanis.Crypto;
using Titanis.IO;

namespace Titanis.Security.Ntlm
{

	[Flags]
	public enum NtlmAuthFlags
	{
		None = 0,

		AuthenticationConstrained = 1,
		HasMic = 2,
		UntrustedSpn = 4,

		Default = HasMic,
	}

	/// <summary>
	/// Implements the client side of an NTLM authentication context.
	/// </summary>
	public sealed class NtlmClientContext : AuthClientContext
	{
		/// <summary>
		/// Initializes a new <see cref="NtlmClientContext"/>.
		/// </summary>
		/// <param name="credential">Credential to use for authentication</param>
		/// <param name="callback">Callback object to receive notifications during authentication</param>
		/// <exception cref="ArgumentNullException"><paramref name="credential"/> is <see langword="null"/></exception>
		public NtlmClientContext(NtlmCredential credential, bool useNtlmV2, INtlmClientCallback? callback = null)
		{
			if (credential is null)
				throw new ArgumentNullException(nameof(credential));

			this.UseNtlmV2 = useNtlmV2;

			this.Credential = credential;
			this._callback = callback;
			this._state.clientVersion = GetDefaultNtlmVersion();
		}

		private readonly INtlmClientCallback? _callback;
		private NtlmAuthContextState _state;
		internal ref NtlmAuthContextState GetState() => ref this._state;

		/// <inheritdoc/>
		public sealed override int Legs => 3;

		/// <inheritdoc/>
		protected sealed override AuthClientContext DuplicateImpl()
			=> this.DuplicateNtlm();
		public NtlmClientContext DuplicateNtlm()
		{
			var dup = new NtlmClientContext(this.Credential, this.UseNtlmV2, this._callback)
			{
				Workstation = this.Workstation,
				WorkstationDomain = this.WorkstationDomain,
				TargetSpn = this.TargetSpn,
				ClientConfigFlags = this.ClientConfigFlags,
				RequiredCapabilities = this.RequiredCapabilities,
				_options = this._options,
				Version = this.Version,
				AuthFlags = this.AuthFlags,

				ClientChannelBindingsUnhashed = this.ClientChannelBindingsUnhashed,
			};
			this.CopyFieldsTo(dup);
			return dup;
		}

		/// <inheritdoc/>
		public sealed override string UserName => this.Credential.UserName;

		/// <summary>
		/// Gets the credential to use for authentication.
		/// </summary>
		public NtlmCredential Credential { get; }
		public NtlmAuthFlags AuthFlags { get; set; } = NtlmAuthFlags.Default;

		/// <inheritdoc/>
		public sealed override bool IsAnonymous => this.Credential.IsAnonymous;

		/// <inheritdoc/>
		public sealed override byte RpcAuthType => 0x0A;

		/// Mechanism ID
		public static readonly Oid NtlmOid = new Oid("1.3.6.1.4.1.311.2.2.10");

		/// <inheritdoc/>
		public sealed override Oid MechOid => NtlmOid;

		/// <inheritdoc/>
		public sealed override SecurityCapabilities NegotiatedCapabilities
		{
			get
			{
				SecurityCapabilities caps = 0;

				// [MS-NLMP] § 3.1.1.2 - Variables Exposed to the Application
				if (0 != (this._state.negotiateFlags & NegotiateFlags.D_NegotiateSign))
					caps |= SecurityCapabilities.Integrity | SecurityCapabilities.ReplayDetection | SecurityCapabilities.SequenceDetection;
				if (0 != (this._state.negotiateFlags & NegotiateFlags.E_NegotiateSeal))
					caps |= SecurityCapabilities.Confidentiality;

				return caps;
			}
		}

		/// <inheritdoc/>
		public sealed override SecurityCapabilities SupportedCapabilities =>
			SecurityCapabilities.Confidentiality
			| SecurityCapabilities.Integrity
			| SecurityCapabilities.ReplayDetection
			| SecurityCapabilities.SequenceDetection;

		/// <summary>
		/// Gets the <see cref="NegotiateFlags"/> value corresponding to
		/// <see cref="NegotiatedCapabilities"/>
		/// </summary>
		/// <returns></returns>
		private static NegotiateFlags GetFlagsForCaps(SecurityCapabilities caps)
		{
			// [MS-NLMP] § 3.1.1.2 - Variables Exposed to the Application

			const SecurityCapabilities IntegrityCaps = (SecurityCapabilities.ReplayDetection | SecurityCapabilities.SequenceDetection | SecurityCapabilities.Integrity);

			NegotiateFlags flags = 0;
			if (0 != (caps & IntegrityCaps))
			{
				flags |= NegotiateFlags.D_NegotiateSign;
			}
			if (0 != (caps & SecurityCapabilities.Confidentiality))
			{
				flags |= NegotiateFlags.E_NegotiateSeal
					| NegotiateFlags.V_NegotiateKeyExchange
					| NegotiateFlags.G_NegotiateLMKey
					| NegotiateFlags.P_NegotiateExtendedSessionSecurity;
			}

			return flags;
		}

		private byte[]? _tokenBuffer;

		/// <inheritdoc/>
		public sealed override ReadOnlySpan<byte> Token => this._tokenBuffer;

		/// <summary>
		/// Gets the name of the local workstation to report.
		/// </summary>
		public string? Workstation
		{
			get => this._state.workstationName;
			set => this._state.workstationName = value;
		}
		/// <summary>
		/// Gets the name of the local workstation domain to report.
		/// </summary>
		public string? WorkstationDomain
		{
			get => this._state.workstationDomain;
			set => this._state.workstationDomain = value;
		}
		/// <summary>
		/// Gets a value indicating whether to use NTLMv2.
		/// </summary>
		/// <remarks>
		/// The default value for this property is <see langword="true"/>.
		/// </remarks>
		public bool UseNtlmV2 { get; }
		/// <inheritdoc/>
		public sealed override ServicePrincipalName? TargetSpn
		{
			get => this._state.targetSpn;
			set => this._state.targetSpn = value;
		}

		/// <summary>
		/// Default for <see cref="ClientConfigFlags"/>
		/// </summary>
		public const NegotiateFlags DefaultClientConfigFlags =
			NegotiateFlags.W_Negotiate56
			| NegotiateFlags.V_NegotiateKeyExchange
			| NegotiateFlags.U_Negotiate128
			| NegotiateFlags.T_NegotiateVersion
			| NegotiateFlags.P_NegotiateExtendedSessionSecurity
			| NegotiateFlags.M_NegotiateAlwaysSign
			| NegotiateFlags.H_NegotiateNtlm
			| NegotiateFlags.G_NegotiateLMKey
			| NegotiateFlags.D_NegotiateSign
			| NegotiateFlags.C_RequestTarget
			;

		// TODO: Verify that the value conforms with feature mask
		/// <summary>
		/// Gets or sets the client configuration flags.
		/// </summary>
		/// <seealso cref="DefaultClientConfigFlags"/>
		public NegotiateFlags ClientConfigFlags { get; set; } = DefaultClientConfigFlags;

		private bool GetOption(NtlmOptions option) => (this._options & option) != 0;
		private void SetOption(NtlmOptions option, bool value)
		{
			if (value)
				this._options |= option;
			else
				this._options &= ~(option);
		}

		private NtlmOptions _options = NtlmOptions.NoLMResponseNTLMv1;
		public NtlmOptions Options
		{
			get => this._options;
			set => this._options = value;
		}

		public bool NoLMResponseNTLMv1
		{
			get => GetOption(NtlmOptions.NoLMResponseNTLMv1);
			set => SetOption(NtlmOptions.NoLMResponseNTLMv1, value);
		}
		public bool ClientRequire128bitEncryption
		{
			get => GetOption(NtlmOptions.ClientRequire128bitEncryption);
			set => SetOption(NtlmOptions.ClientRequire128bitEncryption, value);
		}
		public bool SupportsDatagram
		{
			get => GetOption(NtlmOptions.SupportsDatagram);
			set => SetOption(NtlmOptions.SupportsDatagram, value);
		}
		public bool IdentityOnly
		{
			get => GetOption(NtlmOptions.IdentityOnly);
			set => SetOption(NtlmOptions.IdentityOnly, value);
		}
		public bool HasUnverifiedTargetName
		{
			get => GetOption(NtlmOptions.HasUnverifiedTargetName);
			set => SetOption(NtlmOptions.HasUnverifiedTargetName, value);
		}

		/// <summary>
		/// Gets the channel binding.
		/// </summary>
		public Memory<byte> ClientChannelBindingsUnhashed { get; set; }

		/// <inheritdoc/>
		protected sealed override ReadOnlySpan<byte> InitializeImpl()
		{
			var negToken = BuildNegotiateToken(
				ref this._state,
				this.RequiredCapabilities,
				this._options,
				this.ClientConfigFlags,
				this.Credential,
				this._callback);
			// UNDONE: Windows does not pass workstation nor domain in the NEGOTIATE message
			//var buf = WriteNegotiate(flags, this.Workstation, this.WorkstationDomain);
			this._tokenBuffer = negToken;

			this._state.negToken = negToken;

			return negToken;
		}

		private static byte[] BuildNegotiateToken(
			scoped ref NtlmAuthContextState state,
			SecurityCapabilities requiredCaps,
			NtlmOptions options,
			NegotiateFlags clientConfigFlags,
			NtlmCredential cred,
			INtlmClientCallback? callback
			)
		{
			var isAnon = (cred == null) || cred.IsAnonymous;

			NegotiateFlags negFlags = clientConfigFlags & NegotiateFlags.FeatureMask;
			const NegotiateFlags AnonIgnoreFlags =
				NegotiateFlags.G_NegotiateLMKey
				;

			if (isAnon)
			{
				negFlags &= ~AnonIgnoreFlags;
				// UNDONE: Windows 11 does not send this on the initial negotiate
				//negFlags |= NegotiateFlags.J_Anonymous;
				negFlags |= NegotiateFlags.G_NegotiateLMKey;
			}

			negFlags |= GetFlagsForCaps(requiredCaps);

			if (0 != (requiredCaps & SecurityCapabilities.Integrity))
				negFlags |= NegotiateFlags.D_NegotiateSign;
			if (0 != (requiredCaps & SecurityCapabilities.Confidentiality))
				negFlags |= NegotiateFlags.E_NegotiateSeal
					| NegotiateFlags.V_NegotiateKeyExchange
					| NegotiateFlags.G_NegotiateLMKey
					| NegotiateFlags.P_NegotiateExtendedSessionSecurity
					;
			if (0 != (options & NtlmOptions.SupportsDatagram))
				negFlags |= NegotiateFlags.F_NegotiateDatagram;
			if (0 != (options & NtlmOptions.IdentityOnly))
				negFlags |= NegotiateFlags.Q_NegotiateIdentify;
			if (0 == (negFlags & NegotiateFlags.G_NegotiateLMKey))
				negFlags |= NegotiateFlags.P_NegotiateExtendedSessionSecurity;

			negFlags |=
				NegotiateFlags.B_NegotiateOem
				| NegotiateFlags.C_RequestTarget
				| NegotiateFlags.A_NegotiateUnicode
				| NegotiateFlags.W_Negotiate56
				| NegotiateFlags.U_Negotiate128
				// HACK: Must be set according to spec, but only set if requested by client
				//| NegotiateFlags.H_NegotiateNtlm
				// TODO: Figure out LM flag.  The spec says not to set it, but Windows does anyway
				//| NegotiateFlags.G_NegotiateLMKey
				;

			if (isAnon)
			{
				//NegotiateFlags badAnonFlags = 0
				//	| NegotiateFlags.W_Negotiate56
				//	| NegotiateFlags.V_NegotiateKeyExchange
				//	| NegotiateFlags.U_Negotiate128
				//	| NegotiateFlags.R_RequestNonNTSessionKey
				//	| NegotiateFlags.P_NegotiateExtendedSessionSecurity
				//	| NegotiateFlags.M_NegotiateAlwaysSign
				//	| NegotiateFlags.H_NegotiateNtlm
				//	| NegotiateFlags.G_NegotiateLMKey
				//	| NegotiateFlags.E_NegotiateSeal
				//	| NegotiateFlags.D_NegotiateSign
				//	;
				//var unsupported = (negFlags & badAnonFlags);
				//if (0 != unsupported)
				//	throw new NtlmFeaturesUnsupportedException(unsupported, Messages.Ntlm_AnonFeaturesUnsupported);
			}
			else
			{
				Debug.Assert(cred != null);
				if (!cred.CanProvideResponseKeyNT)
				{
					if (0 != (options & NtlmOptions.UseNtlmV2))
						throw new InvalidOperationException(Messages.Ntlm_NtlmV2RequiresNTHash);
					if (!cred.CanProvideResponseKeyLM)
						throw new InvalidOperationException(Messages.Ntlm_NoCredHash);

					NegotiateFlags badNonNTFlags = 0
						| NegotiateFlags.P_NegotiateExtendedSessionSecurity
						| NegotiateFlags.H_NegotiateNtlm
						| NegotiateFlags.W_Negotiate56
						| NegotiateFlags.V_NegotiateKeyExchange
						| NegotiateFlags.U_Negotiate128
						//| NegotiateFlags.R_RequestNonNTSessionKey
						| NegotiateFlags.M_NegotiateAlwaysSign
						| NegotiateFlags.E_NegotiateSeal
						| NegotiateFlags.D_NegotiateSign
						;
					var unsupported = (negFlags & badNonNTFlags);
					if (0 != unsupported)
						throw new NtlmFeaturesUnsupportedException(unsupported, Messages.Ntlm_LMAuthFeaturesUnsupported);

					bool usesLMKey = (0 != (negFlags & (NegotiateFlags.G_NegotiateLMKey | NegotiateFlags.R_RequestNonNTSessionKey)));
					if (!usesLMKey)
						throw new InvalidOperationException(Messages.Ntlm_LMAuthRequiresLMKey);
				}
			}

			state.negotiateFlags = negFlags;

			//if (!string.IsNullOrEmpty(this.WorkstationDomain))
			//	flags |= NegotiateFlags.K_NegotiateOemDomainSupplied;
			//if (!string.IsNullOrEmpty(this.Workstation))
			//	flags |= NegotiateFlags.L_NegotiateOemWorkstationSupplied;

			callback?.OnNegotiating(ref negFlags, state.serverVersion);

			// Window doesn't pass workstation name/domain
			var buf = WriteNegotiate(negFlags, state.serverVersion, null, null);

			return buf;
		}

		private static NtlmVersion GetDefaultNtlmVersion() => new NtlmVersion
		{
			majorVersion = 10,
			minorVersion = 0,
			build = 18362,
			revision = 15,
		};
		public NtlmVersion Version
		{
			get => this._state.clientVersion;
			set => this._state.clientVersion = value;
		}
		private unsafe static byte[] WriteNegotiate(
			NegotiateFlags flags,
			NtlmVersion version,
			string? workstationName,
			string? workstationDomain
			)
		{
			Encoding encoding = Encoding.UTF8;
			int cbDomain = (workstationDomain == null) ? 0 : encoding.GetByteCount(workstationDomain);
			int cbWorkstation = (workstationName == null) ? 0 : encoding.GetByteCount(workstationName);

			int offWorkstation = NegotiateHeader.StructSize;
			int offDomainName = offWorkstation + cbWorkstation;

			int bufferSize = NegotiateHeader.StructSize + cbWorkstation + cbDomain;
			byte[] buf = new byte[bufferSize];

			fixed (byte* pBuf = buf)
			{
				ref NegotiateHeader hdr = ref *(NegotiateHeader*)pBuf;
				hdr = new NegotiateHeader
				{
					signature = NegotiateHeader.ValidSignature,
					messageType = NtlmMessageType.Negotiate,
					negotiatedFlags = flags,
					domain = new NtlmStringInfo((ushort)cbDomain, offDomainName),
					workstation = new NtlmStringInfo((ushort)cbWorkstation, offWorkstation),
					version = version
				};
			}

			if (cbDomain > 0)
				encoding.GetBytes(workstationName, buf.Slice(offWorkstation, cbWorkstation));
			if (cbWorkstation > 0)
				encoding.GetBytes(workstationDomain, buf.Slice(offDomainName, cbDomain));

			return buf;
		}

		private bool _isComplete;
		public sealed override bool IsComplete => this._isComplete;

		public sealed override int SessionKeySize => 16;
		public sealed override bool HasSessionKey => !this._cryptoContext.sessionKey.IsEmpty;
		protected sealed override ReadOnlySpan<byte> GetSessionKeyImpl()
			=> this._cryptoContext.sessionKey.AsSpan();

		internal NtlmCryptoContext _cryptoContext;

		public void SyncCryptoStateFrom(NtlmClientContext source)
		{
			if (source is null)
				throw new ArgumentNullException(nameof(source));
			this._cryptoContext = source._cryptoContext;
		}

		public void SyncCryptoStateFrom(NtlmServerContext source)
		{
			if (source is null)
				throw new ArgumentNullException(nameof(source));
			this._cryptoContext = source._cryptoContext;
		}

		/// <inheritdoc/>
		protected sealed override ReadOnlySpan<byte> InitializeWithToken(scoped ReadOnlySpan<byte> token)
		{
			bool extendedSecurity = (0 != (this._state.negotiateFlags & NegotiateFlags.P_NegotiateExtendedSessionSecurity));
			this._state.challengeFromClient = extendedSecurity ? Ntlm.GenerateChallenge() : 0;

			return InitializeInternal(token);
		}


		internal ReadOnlySpan<byte> InitializeInternal(scoped ReadOnlySpan<byte> token)
		{
			NtlmChallenge challenge = NtlmChallenge.Parse(token);

			this._state.clientTime = DateTime.Now;
			Ntlm.GetRandomData(this._state.randomKey.AsSpan());

			this._state.UpdateWithChallenge(challenge);
			this._state.challengeToken = token.ToArray();

			this._callback?.OnChallenge(challenge);

			this._state.negAuthFlags = DetermineAuthFlags(ref this._state);

			return this.BuildAuthToken(challenge.targetInfo);
		}

		internal ReadOnlySpan<byte> BuildAuthToken(NtlmAvInfo? targetInfo)
		{
			if (
				(this.ClientRequire128bitEncryption)
				&& (0 == (this._state.negAuthFlags & NegotiateFlags.U_Negotiate128))
				)
				throw new SecurityException(Messages.Ntlm_InsufficientEncryption);

			return this._tokenBuffer = (
				this.UseNtlmV2
				? BuildAuth_V2(targetInfo)
				: BuildAuth_V1()
				);
		}

		private static NegotiateFlags DetermineAuthFlags(ref NtlmAuthContextState state)
		{
			NegotiateFlags negFlags = state.negotiateFlags;

			NegotiateFlags authNegotiateFlags = 0;
			var challengeFlags = state.challengeFlags;
			challengeFlags &= negFlags;
			if (0 != (challengeFlags & NegotiateFlags.U_Negotiate128))
			{
				authNegotiateFlags |= NegotiateFlags.U_Negotiate128 | NegotiateFlags.W_Negotiate56;
			}
			else
			{
				if (0 != (challengeFlags & NegotiateFlags.W_Negotiate56))
					authNegotiateFlags |= NegotiateFlags.W_Negotiate56;
			}

			if (0 == (negFlags & NegotiateFlags.TargetsMask))
				negFlags |= (challengeFlags & NegotiateFlags.TargetsMask);

			if (0 != (challengeFlags & NegotiateFlags.P_NegotiateExtendedSessionSecurity))
				authNegotiateFlags |= NegotiateFlags.P_NegotiateExtendedSessionSecurity;
			else if (0 != (challengeFlags & NegotiateFlags.G_NegotiateLMKey))
				authNegotiateFlags |= NegotiateFlags.G_NegotiateLMKey;

			Encoding encoding;
			if (0 != (challengeFlags & NegotiateFlags.A_NegotiateUnicode))
			{
				authNegotiateFlags |= NegotiateFlags.A_NegotiateUnicode;
				encoding = Encoding.Unicode;
			}
			else if (0 != (challengeFlags & NegotiateFlags.B_NegotiateOem))
			{
				authNegotiateFlags |= NegotiateFlags.B_NegotiateOem;
				encoding = Encoding.UTF8;
			}
			else
				throw new SecurityException(Messages.Ntlm_NoCharset);

			authNegotiateFlags |= (challengeFlags & negFlags & NegotiateFlags.FeatureMask);
			if (0 != (challengeFlags & NegotiateFlags.H_NegotiateNtlm))
				authNegotiateFlags |= NegotiateFlags.H_NegotiateNtlm;
			authNegotiateFlags |= NegotiateFlags.S_NegotiateTargetInfo;

			return authNegotiateFlags;
		}

		internal byte[] BuildAuth_V1()
		{
			ref var state = ref this._state;
			var authFlags = state.negAuthFlags;

			var challengeFromClient = state.challengeFromClient;

			var creds = this.Credential;
			bool isAnon = (creds == null) || creds.IsAnonymous;
			NtlmResponse resp;
			Buffer128 kxkey;

			if (isAnon)
			{
				resp = new NtlmResponse
				{
					LmChallengeResponse = Buffer192.EmptyLMResponse,
					NtChallengeResponse = NtlmCredential.EmptyBytes
				};
				kxkey = new Buffer128();
				authFlags |= NegotiateFlags.J_Anonymous;
			}
			else
			{
				resp = Ntlm.ComputeResponseV1(
					authFlags,
					this.Credential,
					challengeFromClient,
					state.serverChallenge
					);

				kxkey = Ntlm.KxkeyV1(
					authFlags,
					resp,
					state.serverChallenge);
			}

			NtlmAuthInfo authInfo = new NtlmAuthInfo
			{
				negotiateFlags = authFlags,
				version = this.Version,
				workstationName = this.Workstation,
				userName = creds?.UserName,
				userDomain = creds?.Domain,
				resp = resp,
				kxkey = kxkey,
				exportedSessionKey = Ntlm.GetExportedSessionKey(authFlags, kxkey, state.randomKey),
			};

			NtlmAuthResult authResult = BuildAuthMessage(
				ref authInfo,
				ref state
				);
			this._callback?.OnAuth(ref authInfo, ref authResult);
			return this.HandleAuthResult(authResult);
		}

		internal byte[] HandleAuthResult(in NtlmAuthResult authResult)
		{
			this._state.negAuthFlags = authResult.negFlags;
			this._cryptoContext.SetCryptoContext(authResult);
			this._isComplete = true;
			return authResult.authMessage;
		}

		private unsafe byte[] BuildAuth_V2(
			NtlmAvInfo? targetInfo
			)
		{
			if (targetInfo == null)
				throw new SecurityException("The target machine did not send the information required for NTLMv2");

			return HandleChallengeV2(
				ref this._state,
				this.RequiredCapabilities,
				this._options,
				this.Credential,
				targetInfo,
				Ntlm.GetExportedSessionKey(this._state.negAuthFlags, new Buffer128(), this._state.randomKey),
				this._callback
				);
		}

		internal void SetClientChallenge(ulong challengeFromClient)
		{
			this._state.challengeFromClient = challengeFromClient;
		}

		internal byte[] HandleChallengeV2(
			scoped ref NtlmAuthContextState state,
			SecurityCapabilities requiredCaps,
			NtlmOptions options,
			NtlmCredential cred,
			NtlmAvInfo challengeTargetInfo,
			Buffer128 exportedSessionKey,
			INtlmClientCallback? callback
			)
		{
			// TODO: Verify required features

			if (
				(0 != (requiredCaps & (SecurityCapabilities.Integrity | SecurityCapabilities.Confidentiality)))
				&& (
					string.IsNullOrEmpty(state.serverComputerName)
					|| string.IsNullOrEmpty(state.domainName)
				))
			{
				throw new SecurityException(Messages.Ntlm_NoComputerNameOrDomain);
			}

			var authFlags = state.negAuthFlags;

			bool isAnon = (cred == null) || cred.IsAnonymous;
			if (isAnon)
				authFlags |= NegotiateFlags.J_Anonymous;

			var authTargetInfo = challengeTargetInfo;
			authTargetInfo.targetName = state.targetSpn?.ToString();

			authTargetInfo.flags = this.AuthFlags;

			bool canComputeMic = (state.challengeToken != null) && (state.negToken != null);
			if (canComputeMic)
				authTargetInfo.flags |= NtlmAuthFlags.HasMic;
			if (this.IsTargetSpnUntrusted)
				authTargetInfo.flags |= NtlmAuthFlags.UntrustedSpn;
			// TODO: Can this be serialized without allocating an array?
			var targetInfoData = authTargetInfo.ToBytes();

			NtlmAuthInfo authInfo = new NtlmAuthInfo
			{
				negotiateFlags = authFlags,
				version = state.clientVersion,
				workstationName = state.workstationName,
				userName = cred?.UserName,
				userDomain = cred?.Domain ?? authTargetInfo.NbDomainName,
				exportedSessionKey = exportedSessionKey,
			};
			ref var resp = ref authInfo.resp;
			// TODO: Test what happens for anonymous authentication
			resp = Ntlm.ComputeResponseV2(
				ref state,
				cred.GetResponseKeyNTv2(),
				targetInfoData.Span
				);
			if (challengeTargetInfo.timestamp.HasValue)
			{
				// Don't send LM response
				resp.LmChallengeResponse = new Buffer192();
			}

			if (isAnon)
				resp.SessionBaseKey = new Buffer128();

			authInfo.kxkey = Ntlm.KxkeyV2(resp.SessionBaseKey);

			var authResult = BuildAuthMessage(
				ref authInfo,
				ref state
				);
			callback?.OnAuth(ref authInfo, ref authResult);

			return this.HandleAuthResult(authResult);
		}

		internal static unsafe NtlmAuthResult BuildAuthMessage(
			ref NtlmAuthInfo authInfo,
			ref NtlmAuthContextState state
			)
		{
			NtlmAuthResult authResult = new NtlmAuthResult()
			{
				negFlags = authInfo.negotiateFlags
			};

			if (authInfo.exportedSessionKey.IsEmpty)
				authInfo.exportedSessionKey = authInfo.kxkey;

			authResult.exportedSessionKey = authInfo.exportedSessionKey;
			Ntlm.DeriveKeys(ref authResult);

			bool fIncludeMic = (state.negToken != null && state.challengeToken != null);
			int cbMic = fIncludeMic ? Buffer128.StructSize : 0;

			Encoding encoding = Encoding.Unicode;
			int cbUserDomain = (authInfo.userDomain == null) ? 0 : encoding.GetByteCount(authInfo.userDomain);
			int cbWorkstation = (authInfo.workstationName == null) ? 0 : encoding.GetByteCount(authInfo.workstationName);
			int cbUser = (authInfo.userName == null) ? 0 : encoding.GetByteCount(authInfo.userName);
			int cbSessionKey = ((0 != (authInfo.negotiateFlags & NegotiateFlags.V_NegotiateKeyExchange)) ? Ntlm.SessionKeySize : 0);

			bool isAnon = string.IsNullOrEmpty(authInfo.userName);

			int cbLM = (isAnon ? 1 : Buffer192.StructSize); // resp.LmChallengeResponse.Length
			int cbNT = (isAnon ? 0 : authInfo.resp.NtChallengeResponse.Length);
			int bufferSize = NtlmAuthenticateHeader.StructSize
				+ cbUserDomain
				+ cbUser
				+ cbWorkstation
				+ cbLM
				+ cbNT
				+ cbSessionKey
				+ cbMic
				;


			int offMic = NtlmAuthenticateHeader.StructSize;
			int offPayload = offMic + cbMic;
			int offUserDomain = offPayload;// + Buffer128.StructSize;
			int offUserName = offUserDomain + cbUserDomain;
			int offWorkstation = offUserName + cbUser;
			int offLMChallengeResponse = offWorkstation + cbWorkstation;
			int offNTChallengeResponse = offLMChallengeResponse + cbLM;
			int offSessionKey = offNTChallengeResponse + cbNT;

			byte[] buf = new byte[bufferSize];
			fixed (byte* pBuf = buf)
			{
				ref NtlmAuthenticateHeader hdr = ref *(NtlmAuthenticateHeader*)pBuf;
				hdr = new NtlmAuthenticateHeader
				{
					signature = NegotiateHeader.ValidSignature,
					messageType = NtlmMessageType.Authenticate,

					lmChallengeResponse = new NtlmStringInfo((ushort)cbLM, offLMChallengeResponse),
					ntChallengeResponse = new NtlmStringInfo((ushort)cbNT, offNTChallengeResponse),
					domain = new NtlmStringInfo((ushort)cbUserDomain, offUserDomain),
					userName = new NtlmStringInfo((ushort)cbUser, offUserName),
					workstation = new NtlmStringInfo((ushort)cbWorkstation, offWorkstation),
					sessionKey = new NtlmStringInfo((ushort)cbSessionKey, offSessionKey),

					negotiatedFlags = authInfo.negotiateFlags,
					version = authInfo.version
				};
			}

			if (cbUserDomain > 0)
				encoding.GetBytes(authInfo.userDomain, buf.Slice(offUserDomain, cbUserDomain));
			if (cbUser > 0)
				encoding.GetBytes(authInfo.userName, buf.Slice(offUserName, cbUser));
			if (cbWorkstation > 0)
				encoding.GetBytes(authInfo.workstationName, buf.Slice(offWorkstation, cbWorkstation));

			if (!isAnon)
			{
				authInfo.resp.LmChallengeResponse.AsSpan().CopyTo(buf.Slice(offLMChallengeResponse, Buffer192.StructSize));
				//Buffer.BlockCopy(resp.LmChallengeResponse, 0, buf, offLMChallengeResponse, resp.LmChallengeResponse.Length);
				authInfo.resp.NtChallengeResponse.CopyTo(new Memory<byte>(buf, offNTChallengeResponse, authInfo.resp.NtChallengeResponse.Length));
			}

			if (0 != (authInfo.negotiateFlags & NegotiateFlags.V_NegotiateKeyExchange))
			{
				Debug.Assert(isAnon || !authInfo.kxkey.IsEmpty);
				Debug.Assert(!authResult.exportedSessionKey.IsEmpty);
				Rc4.Transform(
					authInfo.kxkey.AsSpan(),
					authResult.exportedSessionKey.AsSpan(),
					buf.Slice(offSessionKey, cbSessionKey));
			}

			if (cbMic > 0)
			{
				fixed (byte* pBuf = buf)
				{
					ref Buffer128 mic = ref *(Buffer128*)(pBuf + offMic);
					mic = Ntlm.ComputeHmacMd5(
						authResult.exportedSessionKey,
						SecBufferList.Create(
							SecBuffer.Integrity(state.negToken),
							SecBuffer.Integrity(state.challengeToken),
							SecBuffer.Integrity(buf)
						));
				}
			}

			authResult.authMessage = buf;
			return authResult;
		}

		#region Message security
		/// <inheritdoc/>
		public sealed override int SignTokenSize => NtlmMessageSignatureV1.StructSize;

		public sealed override void SignMessage(
			in MessageSignParams signParams,
			MessageSignOptions options
			)
		{
			if (signParams.MacBuffer.Length != this.SignTokenSize)
				throw Ntlm.MakeBadMacSizeException(nameof(signParams));

			Ntlm.SignMessage(
				in signParams,
				this._cryptoContext.GetNextSeqNbrC2S(),
				this._state.negAuthFlags,
				this._cryptoContext.signKeyC2S,
				ref this._cryptoContext.sealKeyC2S
				);

			// [MS-SPNG] § 3.3.5.1 - NTLM RC4 Key State for MechListMIC and First Signed Message
			if (0 != (options & MessageSignOptions.SpnegoMechList))
			{
				this._cryptoContext.ResetC2S();
			}
		}

		public sealed override void VerifyMessage(
			in MessageVerifyParams verifyParams,
			MessageSignOptions options
			)
		{
			if (verifyParams.MacBuffer.Length != this.SignTokenSize)
				throw Ntlm.MakeBadMacSizeException(nameof(verifyParams));

			Ntlm.VerifyMessage(
				in verifyParams,
				this._cryptoContext.GetNextSeqNbrS2C(),
				this._state.negAuthFlags,
				this._cryptoContext.signKeyS2C,
				ref this._cryptoContext.sealKeyS2C
				);

			// [MS-SPNG] § 3.3.5.1 - NTLM RC4 Key State for MechListMIC and First Signed Message
			if (0 != (options & MessageSignOptions.SpnegoMechList))
			{
				this._cryptoContext.ResetS2C();
			}
		}



		public sealed override int SealHeaderSize => 0;
		public sealed override int SealTrailerSize => NtlmMessageSignatureV1.StructSize;

		public sealed override void SealMessage(
			in MessageSealParams sealParams
			)
		{
			if (sealParams.Header.Length != this.SealHeaderSize
				|| sealParams.Trailer.Length != this.SealTrailerSize
				)
				throw new ArgumentException("The header or trailer buffer size is incorrect.");
			Ntlm.SealMessage(
				in sealParams,
				this._cryptoContext.GetNextSeqNbrC2S(),
				this._state.negAuthFlags,
				ref this._cryptoContext.sealKeyC2S,
				this._cryptoContext.signKeyC2S
				);
		}

		public sealed override void UnsealMessage(
			in MessageSealParams unsealParams
			)
		{
			if (unsealParams.Header.Length != this.SealHeaderSize
				|| unsealParams.Trailer.Length != this.SealTrailerSize
				)
				throw new ArgumentException("The header or trailer buffer size is incorrect.");
			Ntlm.UnsealMessage(
				in unsealParams,
				this._cryptoContext.GetNextSeqNbrS2C(),
				this._state.negAuthFlags,
				ref this._cryptoContext.sealKeyS2C,
				this._cryptoContext.signKeyS2C
				);
		}

		public void InitializeFromExchange(NtlmExchange exchange, NtlmCredential cred)
		{
			if (exchange is null) throw new ArgumentNullException(nameof(exchange));
			if (cred is null) throw new ArgumentNullException(nameof(cred));

			this._state.negToken = exchange.NegotiateBytes;
			this._state.challengeToken = exchange.ChallengeBytes;

			var negotiate = NtlmNegotiateMessage.Parse(exchange.NegotiateBytes);
			this._state.negotiateFlags = negotiate.hdr.negotiatedFlags;
			var auth = NtlmAuthenticate.Parse(exchange.AuthenticateBytes);
			// TODO: Finish
			throw new NotImplementedException();
		}
		#endregion
	}


	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct NtlmClientChallenge
	{
		public static unsafe int StructSize => sizeof(NtlmClientChallenge);

		public byte Responserversion;
		public byte HiResponserversion;
		public ushort zero0;
		public uint zero1;
		public long time;
		public ulong clientChallenge;
		public uint zero2;
	}

	struct NtlmResponseKeys
	{
		internal Buffer128 ResponseKeyNT;
		internal Buffer128 ResponseKeyLM;
	}

	struct NtlmResponse
	{
		internal NtlmResponseKeys keys;

		internal Memory<byte> NtChallengeResponse;
		internal Buffer192 LmChallengeResponse;
		internal Buffer128 SessionBaseKey;

		internal Buffer128 NTProofStr;
	}

	public struct NtlmAuthInfo
	{
		internal NegotiateFlags negotiateFlags;
		internal NtlmVersion version;
		internal string? workstationName;
		internal string? userName;
		internal string? userDomain;
		internal NtlmResponse resp;
		internal Buffer128 kxkey;
		internal Buffer128 exportedSessionKey;
	}

	public struct NtlmAuthResult
	{
		internal NegotiateFlags negFlags;
		internal Buffer128 exportedSessionKey;
		internal byte[] authMessage;
		internal bool shortSealKey;

		public Buffer128 signKeyC2S;
		public Buffer128 signKeyS2C;
		public Buffer128 sealKeyC2S;
		public Buffer128 sealKeyS2C;
	}

}
