using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Titanis.Crypto;
using Titanis.IO;

namespace Titanis.Security.Ntlm
{

	public class NtlmServerContext : AuthServerContext
	{
		private NtlmAuthContextState _state;

		public override bool IsComplete => throw new NotImplementedException();

		public NtlmServerContext(INtlmAuthStore store)
		{
			if (store is null)
				throw new ArgumentNullException(nameof(store));

			this.store = store;

			this.InitializeState();
		}

		private void InitializeState()
		{
			this._state.serverVersion = GetDefaultNtlmVersion();
		}

		/// <inheritdoc/>
		public sealed override int Legs => 2;


		public override ReadOnlySpan<byte> Accept()
		{
			throw new NotImplementedException();
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
			get => this._state.serverVersion;
			set => this._state.serverVersion = value;
		}
		private readonly INtlmAuthStore store;

		private Memory<byte> _token;
		public override ReadOnlySpan<byte> Token => this._token.Span;

		// Used in testing
		internal void SetAuthState(
			NegotiateFlags negFlags,
			ulong serverChallenge,
			DateTime timestamp)
		{
			this._state.negotiateFlags = negFlags;
			this._state.serverChallenge = serverChallenge;
			this._state.timestamp = timestamp;
		}

		/// <summary>
		/// Gets or sets the name of this server.
		/// </summary>
		public string ServerName
		{
			get => this._state.serverName;
			set => this._state.serverName = value;
		}

		/// <summary>
		/// Gets or sets the domain name of this server.
		/// </summary>
		public string DomainName
		{
			get => this._state.domainName;
			set => this._state.domainName = value;
		}
		public string DnsServerName
		{
			get => this._state.dnsServerName;
			set => this._state.dnsServerName = value;
		}
		public string DnsDomainName
		{
			get => this._state.dnsDomainName;
			set => this._state.dnsDomainName = value;
		}

		public override ReadOnlySpan<byte> Accept(ReadOnlySpan<byte> token)
		{
			if (this._state.negotiateFlags == 0)
			{
				this._state.serverChallenge = Ntlm.GenerateChallenge();
				this._state.timestamp = DateTime.UtcNow;

				Memory<byte> reply = HandleNegotiate(ref this._state, token);
				return (this._token = reply).Span;
			}
			else
			{
				Memory<byte> reply = HandleAuth(token, null);
				return (this._token = reply).Span;
			}
		}

		internal NtlmCryptoContext _cryptoContext;

		public override int SessionKeySize => 16;
		public override bool HasSessionKey => !this._cryptoContext.sessionKey.IsEmpty;
		protected override ReadOnlySpan<byte> GetSessionKeyImpl()
			=> this._cryptoContext.sessionKey.AsSpan();

		private Memory<byte> HandleAuth(ReadOnlySpan<byte> token, NtlmCredential? credential)
		{
			NtlmAuthenticate msg = NtlmAuthenticate.Parse(token, out var targetInfoBytes);
			return this.HandleAuth(msg, credential, targetInfoBytes);
		}
		private Memory<byte> HandleAuth(
			NtlmAuthenticate msg,
			NtlmCredential? credential,
			ReadOnlySpan<byte> targetInfoBytes)
		{
			if (credential != null && !credential.CanProvideResponseKeyNT)
				throw new ArgumentException(Messages.Ntlm_NTKeySupportRequired, nameof(credential));

			ref var state = ref this._state;
			state.UpdateWithAuth(msg);

			var negFlags = msg.hdr.negotiatedFlags;

			// TODO: Verify auth flags
			// TODO: Don't overwrite flags received from NTLM_NEGOTIATE

			// TODO: Verify AUTH MIC
			// TODO: Verify target name

			var serverChallenge = state.serverChallenge;

			var authRecord = credential?.ToAuthRecord()
				?? this.store.GetUserAuthRecord(msg.userName);
			var avInfo = msg.avInfo;
			bool isNtlmV2 = (avInfo != null);
			bool extendedSecurity = (0 != (negFlags & NegotiateFlags.P_NegotiateExtendedSessionSecurity));

			NtlmResponse resp;
			Buffer128 kxkey;
			if (isNtlmV2)
			{
				// TODO: This check appears to be too strict
				bool isValid =
					(!avInfo.timestamp.HasValue || avInfo.timestamp == state.timestamp)
					&& (string.IsNullOrEmpty(state.serverName) || avInfo.NbComputerName == state.serverName)
					&& (string.IsNullOrEmpty(state.domainName) || avInfo.NbDomainName == state.domainName)
					&& (string.IsNullOrEmpty(state.dnsServerName) || avInfo.DnsComputerName == state.dnsServerName)
					&& (string.IsNullOrEmpty(state.dnsDomainName) || avInfo.DnsDomainName == state.dnsDomainName)
					&& (new DateTime(msg.clientChallenge.time) == state.timestamp)
					;
				if (!isValid)
					throw new AuthenticationException();

				var clientChallenge = msg.clientChallenge.clientChallenge;
				resp = Ntlm.ComputeResponseV2(
					ref this._state,
					Ntlm.NtowfV2(msg.userName, msg.domain, authRecord.NtKey),
					targetInfoBytes
					);
				kxkey = Ntlm.KxkeyV2(resp.SessionBaseKey);

				if (new Buffer128(msg.ntResponse) != resp.NTProofStr)
					throw new AuthenticationException();
			}
			else
			{
				var clientChallenge = extendedSecurity ? msg.lmResponse.k1 : 0;
				resp = Ntlm.ComputeResponseV1(
					negFlags,
					new NtlmHashCredential(
						msg.userName,
						authRecord.LmKey,
						authRecord.NtKey
						),
					clientChallenge,
					serverChallenge
					);
				kxkey = Ntlm.KxkeyV1(negFlags, resp, serverChallenge);

				if (msg.ntResponse != new Buffer192(resp.NtChallengeResponse.Span))
					throw new AuthenticationException();
			}

			Buffer128 exportedSessionKey;
			if (msg.sessionKey.IsEmpty)
			{
				exportedSessionKey = kxkey;
			}
			else
			{
				exportedSessionKey = new Buffer128();
				Rc4.Transform(kxkey.AsSpan(), msg.sessionKey.AsSpan(), exportedSessionKey.AsSpan());
			}

			var authResult = new NtlmAuthResult()
			{
				negFlags = negFlags,
				exportedSessionKey = exportedSessionKey,
			};
			Ntlm.DeriveKeys(ref authResult);

			this._cryptoContext.SetCryptoContext(authResult);

			return new Memory<byte>();
		}

		private static Memory<byte> HandleNegotiate(
			ref NtlmAuthContextState state,
			ReadOnlySpan<byte> token)
		{
			NtlmNegotiateMessage msg = NtlmNegotiateMessage.Parse(token);
			state.UpdateWithNegotiate(msg);

			return BuildChallengeToken(in state);
		}

		private static Memory<byte> BuildChallengeToken(ref readonly NtlmAuthContextState state)
		{
			var negFlags = state.negotiateFlags;

			NegotiateFlags authFlags = negFlags & NegotiateFlags.FeatureMask;
			authFlags |= 0
				| NegotiateFlags.A_NegotiateUnicode
				| NegotiateFlags.O_TargetTypeServer
				;

			bool includeVersion = (0 != (negFlags & NegotiateFlags.T_NegotiateVersion));
			bool includeTargetInfo = (0 != (negFlags & NegotiateFlags.C_RequestTarget));

			DateTime timestamp = state.timestamp;
			var serverName = state.serverName;

			NtlmVersion version = state.serverVersion;


			int cbServerName = string.IsNullOrEmpty(serverName) ? 0 : Encoding.Unicode.GetByteCount(serverName);
			int offServerName = NtlmChallengeHeader.StructSize;

			NtlmAvInfo targetInfo;
			NtlmStringInfo targetStrInfo;
			if (includeTargetInfo)
			{
				targetInfo = new NtlmAvInfo
				{
					NbComputerName = serverName,
					NbDomainName = state.domainName,
					DnsComputerName = state.dnsServerName,
					DnsDomainName = state.dnsDomainName,
					timestamp = timestamp
				};

				int cbTargetInfo = targetInfo.Measure();
				int offTargetInfo = offServerName + cbServerName;

				targetStrInfo = new NtlmStringInfo((ushort)cbTargetInfo, offTargetInfo);

				authFlags |= NegotiateFlags.S_NegotiateTargetInfo;
			}
			else
			{
				targetInfo = null;
				targetStrInfo = new NtlmStringInfo();
			}

			if (includeVersion)
				authFlags |= NegotiateFlags.S_NegotiateTargetInfo;

			NtlmChallenge challenge = new NtlmChallenge
			{
				hdr = new NtlmChallengeHeader
				{
					signature = NegotiateHeader.ValidSignature,
					messageType = NtlmMessageType.Challenge,
					targetName = new NtlmStringInfo((ushort)cbServerName, offServerName),
					negotiateFlags = authFlags,
					serverChallenge = state.serverChallenge,
					targetInfo = targetStrInfo,
					version = version
				},
				serverName = state.serverName,
				targetInfo = targetInfo
			};

			ByteWriter writer = new ByteWriter(NtlmChallengeHeader.StructSize + cbServerName + targetStrInfo.len);
			writer.WriteChallenge(challenge);
			var reply = writer.GetData();
			return reply;
		}

		#region Message security
		public override int SignTokenSize => NtlmMessageSignatureV1.StructSize;
		public override int SealTrailerSize => NtlmMessageSignatureV1.StructSize;

		public sealed override void SignMessage(
			in MessageSignParams signParams,
			MessageSignOptions options
			)
		{
			if (signParams.MacBuffer.Length != this.SignTokenSize)
				throw Ntlm.MakeBadMacSizeException(nameof(signParams));

			Ntlm.SignMessage(
				signParams,
				this._cryptoContext.GetNextSeqNbrS2C(),
				this._state.negAuthFlags,
				this._cryptoContext.signKeyS2C,
				ref this._cryptoContext.sealKeyS2C
				);

			if (options != MessageSignOptions.None)
				throw new NotImplementedException();
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
				this._cryptoContext.GetNextSeqNbrC2S(),
				this._state.negAuthFlags,
				this._cryptoContext.signKeyC2S,
				ref this._cryptoContext.sealKeyC2S
				);
		}

		public sealed override void SealMessage(
			in MessageSealParams sealParams
			)
		{
			if (sealParams.Header.Length != this.SealHeaderSize
				|| sealParams.Trailer.Length != this.SealTrailerSize
				)
				throw new ArgumentException("The header or trailer buffer size is incorrect.");
			Ntlm.SealMessage(
				sealParams,
				this._cryptoContext.GetNextSeqNbrS2C(),
				this._state.negAuthFlags,
				ref this._cryptoContext.sealKeyS2C,
				this._cryptoContext.signKeyS2C
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
				this._cryptoContext.GetNextSeqNbrC2S(),
				this._state.negAuthFlags,
				ref this._cryptoContext.sealKeyC2S,
				this._cryptoContext.signKeyC2S
				);
		}
		#endregion

		public void InitializeFromExchange(NtlmExchange exchange, NtlmCredential credential)
		{
			if (exchange is null) throw new ArgumentNullException(nameof(exchange));
			if (credential is null) throw new ArgumentNullException(nameof(credential));

			// serverChallenge
			// timestamp
			// SetAuthState

			HandleNegotiate(ref this._state, exchange.NegotiateBytes);

			// serverChallenge
			var challenge = NtlmChallenge.Parse(exchange.ChallengeBytes);
			this._state.serverChallenge = challenge.hdr.serverChallenge;
			if (challenge.targetInfo.timestamp.HasValue)
				// TODO: What if this is missing?
				this._state.timestamp = challenge.targetInfo.timestamp.Value;

			var auth = NtlmAuthenticate.Parse(exchange.AuthenticateBytes, out var targetInfoBytes);
			var avInfo = auth.avInfo;
			if (avInfo != null)
			{
				this.ServerName = avInfo.NbComputerName;
				this.DomainName = avInfo.NbDomainName;
				this.DnsServerName = avInfo.DnsComputerName;
				this.DnsDomainName = avInfo.DnsDomainName;
			}
			this.HandleAuth(auth, credential, targetInfoBytes);
		}
	}
}
