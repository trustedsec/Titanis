using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

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
		public static readonly Oid SpnegoOid = new Oid("1.3.6.1.5.5.2");

		public SpnegoClientContext()
		{

		}

		/// <inheritdoc/>
		protected sealed override AuthClientContext DuplicateImpl()
			=> this.DuplicateSpnego();
		public SpnegoClientContext DuplicateSpnego()
		{
			var dup = new SpnegoClientContext();
			foreach (var ctx in this.Contexts)
			{
				var dup2 = ctx.Duplicate();
				dup.Contexts.Add(dup2);
			}
			this.CopyFieldsTo(dup);
			return dup;
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
		public sealed override Oid MechOid => SpnegoOid;

		/// <inheritdoc/>
		public sealed override byte RpcAuthType => 0x09;

		private ServicePrincipalName? _targetSpn;
		/// <inheritdoc/>
		public sealed override ServicePrincipalName? TargetSpn
		{
			get => this._targetSpn;
			set
			{
				this._targetSpn = value;
				foreach (var ctx in this.Contexts)
				{
					ctx.TargetSpn = value;
				}
			}
		}

		/// <inheritdoc/>
		public sealed override bool IsComplete =>
			(this._selectedContext != null) && (this._selectedContext.IsComplete);
		private bool _mutualAuthed;

		private byte[] _token;
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
		public sealed override int SealHeaderSize => this.GetCompletedContext().SealHeaderSize;
		/// <inheritdoc/>
		public sealed override int SealTrailerSize => this.GetCompletedContext().SealTrailerSize;

		private Asn1.SPNEGOASNOneSpec.MechTypeList _mechTypeList;

		private AuthClientContext _selectedContext;

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
				ctx.TargetSpn = this.TargetSpn;
				ctx.IsTargetSpnUntrusted = this.IsTargetSpnUntrusted;
			}

			this._initiator = SpnegoInitiator.Client;

			Asn1Oid[] mechList = new Asn1Oid[this.Contexts.Count];
			for (int i = 0; i < mechList.Length; i++)
			{
				var mechOid = this.Contexts[i].MechOid;
				if (mechOid == null)
					throw new InvalidOperationException(Messages.Spnego_NoContextMechOid);

				mechList[i] = new Asn1Oid(mechOid);
			}
			this._mechTypeList = new Asn1.SPNEGOASNOneSpec.MechTypeList
			{
				mechTypes = mechList
			};

			// TODO: Include optimistic token
			var optContext = this.Contexts[0];
			var optToken = optContext.Initialize();

			Asn1.SPNEGOASNOneSpec.NegotiationToken spnegoToken = new Asn1.SPNEGOASNOneSpec.NegotiationToken
			{
				negTokenInit = new Asn1.SPNEGOASNOneSpec.NegTokenInit
				{
					mechTypes = this._mechTypeList.mechTypes,
					mechToken = optToken.ToArray()
				}
			};

			Asn1.GSS_API.InitialContextToken gssToken = new Asn1.GSS_API.InitialContextToken
			{
				Value = new Asn1.GSS_API.InitialContextToken_Unnamed_0
				{
					thisMech = new Asn1Oid(SpnegoOid),
					innerContextToken = Asn1Any.CreateFrom(spnegoToken)
				}
			};

			return this._token = Asn1DerEncoder.EncodeTlv(gssToken).ToArray();
		}

		/// <inheritdoc/>
		protected sealed override ReadOnlySpan<byte> InitializeWithToken(ReadOnlySpan<byte> token)
		{
			this._token = null;

			ReadOnlySpan<byte> innerToken;
			Asn1.SPNEGOASNOneSpec.NegotiationToken spnegoToken;
			Asn1.GSS_API.InitialContextToken gssRespToken;
			if (this._initiator == SpnegoInitiator.None)
			{
				Asn1.GSS_API.InitialContextToken gssToken;
				try
				{
					gssToken = Asn1DerDecoder.Decode<Asn1.GSS_API.InitialContextToken>(token.ToArray());
				}
				catch (Exception ex)
				{
					throw new FormatException(Messages.Spnego_InvalidRespToken, ex);
				}

				Asn1.SPNEGOASNOneSpec.NegTokenInit2 initToken = gssToken.Value.innerContextToken.DecodeAs<Asn1.SPNEGOASNOneSpec.NegotiationToken2>().negTokenInit2;
				if (initToken == null)
					throw new FormatException(Messages.Spnego_InvalidRespToken);

				this._initiator = SpnegoInitiator.Server;

				var mechList = initToken.mechTypes;
				if (mechList.IsNullOrEmpty())
					throw new FormatException(Messages.Spnego_NoInitMechs);

				var ctx = this._selectedContext = FindMatchingContext(
					mechList,
					out bool preferred);
				if (ctx == null)
					throw new SecurityException(Messages.Spnego_NoSupportedMechs);

				innerToken = preferred
					? this._selectedContext.Initialize(initToken.mechToken)
					: this._selectedContext.Initialize();

				mechList = new Asn1Oid[] { new Asn1Oid(this._selectedContext.MechOid) };
				this._mechTypeList = new Asn1.SPNEGOASNOneSpec.MechTypeList
				{
					mechTypes = mechList
				};
				spnegoToken = new Asn1.SPNEGOASNOneSpec.NegotiationToken
				{
					negTokenInit = new Asn1.SPNEGOASNOneSpec.NegTokenInit
					{
						mechTypes = mechList,
						mechToken = innerToken.ToArray(),
					}
				};

				gssRespToken = new Asn1.GSS_API.InitialContextToken
				{
					Value = new Asn1.GSS_API.InitialContextToken_Unnamed_0
					{
						thisMech = new Asn1Oid(SpnegoOid),
						innerContextToken = Asn1Any.CreateFrom(spnegoToken)
					}
				};
				this._token = Asn1DerEncoder.EncodeTlv(gssRespToken).ToArray();

			}
			else
			{
				Asn1.SPNEGOASNOneSpec.NegotiationToken negToken;
				try
				{
					negToken = Asn1DerDecoder.Decode<Asn1.SPNEGOASNOneSpec.NegotiationToken>(token.ToArray());
				}
				catch (Exception ex)
				{
					throw new FormatException(Messages.Spnego_InvalidRespToken, ex);
				}

				Asn1.SPNEGOASNOneSpec.NegTokenResp respToken = negToken.negTokenResp;
				if (respToken == null)
					throw new FormatException(Messages.Spnego_InvalidRespToken);

				if (this._selectedContext == null)
				{
					if (this._initiator == SpnegoInitiator.Client)
					{
						var ctx = this._selectedContext = FindMatchingContext(respToken.supportedMech.Value);
						if (ctx == null)
							throw new SecurityException(Messages.Spnego_NoSupportedMechs);
					}
					else
						throw new SecurityException(Messages.Spnego_NoSelectedContext);
				}


				if (respToken.negState == Asn1.SPNEGOASNOneSpec.NegTokenResp_NegState_NegState.accept_completed)
				{
					var mic = respToken.mechListMIC;
					if (respToken.responseToken != null)
						innerToken = this._selectedContext.Initialize(respToken.responseToken);

					if (!mic.IsNullOrEmpty())
					{
						byte[] mechListBytes = Asn1DerEncoder.EncodeTlv(this._mechTypeList).ToArray();
						this._selectedContext.VerifyMessage(mechListBytes, mic, MessageSignOptions.SpnegoMechList);
					}
				}
				else
				{

					// TODO: Check mech ID of response
					// TODO: Check MIC
					innerToken = this._selectedContext.Initialize(respToken.responseToken);


					byte[] mic;
					if (this._selectedContext.IsComplete && innerToken.Length > 0)
					{
						byte[] mechListBytes = Asn1DerEncoder.EncodeTlv(this._mechTypeList).ToArray();
						mic = new byte[this._selectedContext.SignTokenSize];
						this._selectedContext.SignMessage(mechListBytes, mic, MessageSignOptions.SpnegoMechList);
					}
					else
					{
						mic = null;
					}

					spnegoToken = new Asn1.SPNEGOASNOneSpec.NegotiationToken
					{
						negTokenResp = new Asn1.SPNEGOASNOneSpec.NegTokenResp
						{
							negState = Asn1.SPNEGOASNOneSpec.NegTokenResp_NegState_NegState.accept_incomplete,
							//supportedMech = new Asn1Oid(this._selectedContext.MechOid),
							responseToken = innerToken.ToArray(),
							mechListMIC = mic
						}
					};

					this._token = Asn1DerEncoder.EncodeTlv(spnegoToken).ToArray();
				}
			}

			return this._token;
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
				var mechOid = mech.ToOid();
				foreach (var ctx in this.Contexts)
				{
					if (mechOid.Value.Equals(ctx.MechOid?.Value))
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
		public sealed override void SealMessage(in MessageSealParams sealParams)
			=> this._selectedContext.SealMessage(sealParams);
		/// <inheritdoc/>
		public sealed override void SignMessage(
			in MessageSignParams signParams,
			MessageSignOptions options
			)
			=> this._selectedContext.SignMessage(signParams, options);
		/// <inheritdoc/>
		public sealed override void UnsealMessage(in MessageSealParams unsealParams)
			=> this._selectedContext.UnsealMessage(unsealParams);
		/// <inheritdoc/>
		public sealed override void VerifyMessage(in MessageVerifyParams verifyParams, MessageSignOptions options)
			=> this._selectedContext.VerifyMessage(verifyParams, options);
	}
}
