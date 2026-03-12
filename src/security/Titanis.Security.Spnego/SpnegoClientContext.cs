using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using SPNEGOASNOneSpec;
using GSS_API;

namespace Titanis.Security.Spnego
{
	/// <summary>
	/// Implements the SP-NEGO authentication mechanism.
	/// </summary>
	/// <remarks>
	/// Supply the contexts to be negotiated by adding them to <see cref="Contexts"/>.
	/// </remarks>
	public sealed class SpnegoClientContext : AuthClientContext
	{
		public static readonly Asn1Oid SpnegoOid = new Asn1Oid("1.3.6.1.5.5.2");

		/// <summary>
		/// Initializes a new <see cref="SpnegoClientContext"/>.
		/// </summary>
		public SpnegoClientContext()
		{

		}

		/// <summary>
		/// Gets a list of contexts to negotiate.
		/// </summary>
		public List<AuthClientContext> Contexts { get; } = new List<AuthClientContext>();

		/// <inheritdoc/>
		public sealed override string UserName => this.Contexts.FirstOrDefault()?.UserName ?? string.Empty;
		/// <inheritdoc/>
		public sealed override bool IsAnonymous => this._selectedContext?.IsAnonymous ?? false;

		/// <inheritdoc/>
		public sealed override Asn1Oid MechOid => SpnegoOid;

		/// <inheritdoc/>
		public sealed override byte RpcAuthType => 0x09;

		private SecurityPrincipalName? _targetSpn;
		/// <inheritdoc/>
		public sealed override SecurityPrincipalName? TargetSpn
		{
			get => this._targetSpn;
			set => this._targetSpn = value;
		}

		/// <inheritdoc/>
		public sealed override bool IsComplete =>
			(this._selectedContext != null) && (this._selectedContext.IsComplete);
		private bool _mutualAuthed;

		private byte[]? _token;
		/// <inheritdoc/>
		public sealed override ReadOnlySpan<byte> Token => this._token;

		/// <inheritdoc/>
		/// <remarks>
		/// The supported capabilities depend on underlying contexts.
		/// </remarks>
		public sealed override SecurityCapabilities SupportedCapabilities => SecurityCapabilities.None;

		public sealed override SecurityCapabilities NegotiatedCapabilities =>
			this.IsComplete ? this._selectedContext.NegotiatedCapabilities
			: SecurityCapabilities.None;

		private AuthClientContext GetCompletedContext()
		{
			var ctx = this._selectedContext;
			if (ctx == null)
				throw new InvalidOperationException(Messages.Spnego_ContextIncomplete);

			return ctx;
		}

		/// <inheritdoc/>
		public sealed override int SessionKeySize => this.GetCompletedContext().SessionKeySize;
		/// <inheritdoc/>
		public sealed override bool HasSessionKey => (this._selectedContext != null) && (this._selectedContext.HasSessionKey);
		/// <inheritdoc/>
		protected sealed override ReadOnlySpan<byte> GetSessionKeyImpl()
			=> this.GetCompletedContext().GetSessionKey();

		/// <inheritdoc/>
		public sealed override int SignTokenSize
			=> this.GetCompletedContext().SignTokenSize;
		/// <inheritdoc/>
		public override int GetWrapTokenSize(WrapOptions options)
			=> this.GetCompletedContext().GetWrapTokenSize(options);
		/// <inheritdoc/>
		public override void GetWrapBufferSizes(WrapOptions options, out int requiredHeaderSize, out int requiredTrailerSize)
		{
			this.GetCompletedContext().GetWrapBufferSizes(options, out requiredHeaderSize, out requiredTrailerSize);
		}

		// Used for compute MIC
		private Asn1Oid[]? _mechTypeList;

		private AuthClientContext? _selectedContext;

		enum SpnegoInitiator
		{
			None = 0,
			Client,
			Server
		};
		private SpnegoInitiator _initiator;

		/// <inheritdoc/>
		/// <exception cref="InvalidOperationException">No contexts added to <see cref="Contexts"/>.</exception>
		protected sealed override ReadOnlySpan<byte> InitializeImpl()
		{
			if (this._initiator != SpnegoInitiator.None)
				throw new InvalidOperationException(Messages.Spnego_AlreadyInitialized);
			if (this.Contexts.Count == 0)
				throw new InvalidOperationException(Messages.Spnego_NoContexts);

			foreach (var ctx in this.Contexts)
			{
				SyncAuthSettings(ctx);
			}

			this.Contexts.Add(new NegoexClientContext());

			this._initiator = SpnegoInitiator.Client;

			Asn1Oid[] mechList = new Asn1Oid[this.Contexts.Count];
			for (int i = 0; i < mechList.Length; i++)
			{
				var mechOid = this.Contexts[i].MechOid;
				if (mechOid.IsEmpty)
					throw new InvalidOperationException(Messages.Spnego_NoContextMechOid);

				mechList[i] = mechOid;
			}
			this._mechTypeList = mechList;

			// TODO: Include optimistic token
			var optContext = this.Contexts[0];
			var optToken = optContext.Initialize();

			var spnegoToken = new NegotiationToken()
			{
				NegTokenInit = new NegTokenInit(
					mechTypes: this._mechTypeList,
					mechToken: optToken.ToArray()
					)
			};

			var gssToken = new InitialContextToken(
				new InitialContextToken_Tagged0(
					SpnegoOid,
					Asn1Any.CreateFromObject(spnegoToken)
					));

			return this._token = Asn1DerEncoder.EncodeTlv(gssToken).ToArray();
		}

		/// <inheritdoc/>
		protected sealed override ReadOnlySpan<byte> InitializeWithToken(ReadOnlySpan<byte> token)
		{
			this._token = null;

			byte[]? tokenBytes;

			ReadOnlySpan<byte> innerTokenBytes;
			NegotiationToken spnegoToken;
			InitialContextToken gssRespToken;
			if (this._initiator == SpnegoInitiator.None)
			{
				InitialContextToken gssToken;
				try
				{
					gssToken = Asn1DerDecoder.DecodeTlv<InitialContextToken>(token.ToArray());
				}
				catch (Exception ex)
				{
					throw new FormatException(Messages.Spnego_InvalidRespToken, ex);
				}

				if (gssToken.Value.innerContextToken.Tag != new Asn1Tag(0, Asn1TagFlags.Context | Asn1TagFlags.Constructed))
					throw new FormatException(Messages.Spnego_InvalidRespToken);

				var initToken = Asn1DerDecoder.DecodeTlv<NegotiationToken2>(gssToken.Value.innerContextToken.TlvBytes).Value;

				this._initiator = SpnegoInitiator.Server;

				var mechList = initToken.mechTypes;
				if (mechList.IsNullOrEmpty())
					throw new FormatException(Messages.Spnego_NoInitMechs);

				var ctx = this._selectedContext = FindMatchingContext(
					mechList,
					out bool preferred);
				if (ctx == null)
					throw new SecurityException(Messages.Spnego_NoSupportedMechs);

				SyncAuthSettings(ctx);

				innerTokenBytes = preferred
					? this._selectedContext.Initialize(initToken.mechToken)
					: this._selectedContext.Initialize();

				mechList = new Asn1Oid[] { this._selectedContext.MechOid };
				this._mechTypeList = mechList;
				spnegoToken = new NegotiationToken
				{
					NegTokenInit = new NegTokenInit(
						mechList,
						mechToken: innerTokenBytes.ToArray()
					)
				};

				gssRespToken = new InitialContextToken(
					new InitialContextToken_Tagged0(
						SpnegoOid,
						Asn1Any.CreateFromObject(spnegoToken)
					));
				tokenBytes = Asn1DerEncoder.EncodeTlv(gssRespToken).ToArray();

			}
			else
			{
				NegotiationToken negToken;
				try
				{
					negToken = Asn1DerDecoder.DecodeTlv<NegotiationToken>(token.ToArray());
				}
				catch (Exception ex)
				{
					throw new FormatException(Messages.Spnego_InvalidRespToken, ex);
				}

				if (negToken.SelectedChoice != NegotiationToken.ChoiceIndex.NegTokenResp)
					throw new FormatException(Messages.Spnego_InvalidRespToken);

				NegTokenResp respToken = negToken.NegTokenResp;

				if (this._selectedContext == null)
				{
					if (this._initiator == SpnegoInitiator.Client)
					{
						var ctx = this._selectedContext = FindMatchingContext(respToken.supportedMech.Value);
						if (ctx == null)
							throw new SecurityException(Messages.Spnego_NoSupportedMechs);

						innerTokenBytes = ctx.Initialize(respToken.responseToken);
					}
					else
						throw new SecurityException(Messages.Spnego_NoSelectedContext);
				}
				else
				{
					// Pass token to selected context
					if (respToken.responseToken != null)
						innerTokenBytes = this._selectedContext.Initialize(respToken.responseToken);
					else
						innerTokenBytes = default;
				}

				// If acceptor provided MIC
				var acceptorMic = respToken.mechListMIC;
				if (!acceptorMic.IsNullOrEmpty())
				{
					var mechListBytes = Asn1DerEncoder.EncodeTlv(Asn1SequenceOf.Create(this._mechTypeList));
					this._selectedContext.VerifyMessage(mechListBytes.Span, acceptorMic, MessageSignOptions.SpnegoMechList);
				}

				if (respToken.negState is NegTokenResp_NegState_Tagged0.Accept_completed)
				{
					tokenBytes = null;
				}
				else
				{
					byte[]? mic;
					if (this._selectedContext.IsComplete && innerTokenBytes.Length > 0)
					{
						var mechListBytes = Asn1DerEncoder.EncodeTlv(Asn1SequenceOf.Create(this._mechTypeList)).ToArray();
						mic = new byte[this._selectedContext.SignTokenSize];
						this._selectedContext.SignMessage(mechListBytes, mic, MessageSignOptions.SpnegoMechList);
					}
					else
					{
						mic = null;
					}

					spnegoToken = new NegotiationToken
					{
						NegTokenResp = new NegTokenResp
						{
							negState = NegTokenResp_NegState_Tagged0.Accept_incomplete,
							//supportedMech = new Asn1Oid(this._selectedContext.MechOid),
							responseToken = innerTokenBytes.ToArray(),
							mechListMIC = mic
						}
					};

					tokenBytes = Asn1DerEncoder.EncodeTlv(spnegoToken).ToArray();
				}
			}

			return this._token = tokenBytes;
		}

		private void SyncAuthSettings(AuthClientContext ctx)
		{
			ctx.ChannelBinding = this.ChannelBinding;
			ctx.TargetSpn = this.TargetSpn;
			ctx.IsTargetSpnUntrusted = this.IsTargetSpnUntrusted;
		}

		private AuthClientContext FindMatchingContext(params Asn1Oid[] mechList)
			=> this.FindMatchingContext(mechList, out _);

		private AuthClientContext FindMatchingContext(
			Asn1Oid[] mechList,
			out bool preferred
			)
		{
			for (int i = 0; i < mechList.Length; i++)
			{
				var mech = mechList[i];
				var mechOid = mech;
				foreach (var ctx in this.Contexts)
				{
					if (mechOid.Equals(ctx.MechOid))
					{
						preferred = (i == 0);
						return ctx;
					}
				}
			}

			preferred = false;
			return null;
		}

		/// <inheritdoc/>
		public override AuthClientContext GetMechContext() => this.GetCompletedContext().GetMechContext();

		public sealed override void IncrementRecvSeqNbr()
		{
			this.GetCompletedContext().IncrementRecvSeqNbr();
		}

		/// <inheritdoc/>
		public sealed override void SealMessage(in MessageSealParams sealParams)
			=> this.GetCompletedContext().SealMessage(sealParams);
		/// <inheritdoc/>
		public sealed override void SignMessage(
			in MessageSignParams signParams,
			MessageSignOptions options
			)
			=> this.GetCompletedContext().SignMessage(signParams, options);
		/// <inheritdoc/>
		public sealed override void UnsealMessage(in MessageSealParams unsealParams)
			=> this.GetCompletedContext().UnsealMessage(unsealParams);
		/// <inheritdoc/>
		public sealed override void VerifyMessage(in MessageVerifyParams verifyParams, MessageSignOptions options)
			=> this.GetCompletedContext().VerifyMessage(verifyParams, options);
	}
}
