using GSS_API;
using SPNEGOASNOneSpec;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

namespace Titanis.Security.Spnego
{
	public interface ISpnegoServerContextProvider
	{
		AuthServerContext? TryGetContext(Asn1Oid mechanism);

	}

	public class SpnegoServerContext : AuthServerContext
	{
		public SpnegoServerContext(ISpnegoServerContextProvider provider)
		{
			this._provider = provider;
		}

		private readonly ISpnegoServerContextProvider _provider;

		private AuthServerContext? _mechContext;
		private Asn1Oid[] _mechTypeList;

		private AuthContext VerifyHasContext()
		{
			var context = this._mechContext;
			if (context == null)
				throw new InvalidOperationException("No authentication context has been selected.");

			return context;
		}

		public override bool IsComplete => this._mechContext?.IsComplete ?? false;

		private AuthServerContext GetCompletedContext()
		{
			var ctx = this._mechContext;
			if (ctx == null)
				throw new InvalidOperationException(Messages.Spnego_ContextIncomplete);

			return ctx;
		}

		public override bool HasSessionKey => this._mechContext?.HasSessionKey ?? false;

		public override int SessionKeySize => this.VerifyHasContext().SessionKeySize;

		public override SecurityCapabilities NegotiatedCapabilities => this.VerifyHasContext().NegotiatedCapabilities;

		public override SecurityCapabilities SupportedCapabilities => this.VerifyHasContext().SupportedCapabilities;

		public override int SignTokenSize => this.VerifyHasContext().SignTokenSize;

		/// <inheritdoc/>
		public override int GetWrapTokenSize(WrapOptions options)
			=> this.GetCompletedContext().GetWrapTokenSize(options);
		/// <inheritdoc/>
		public override void GetWrapBufferSizes(WrapOptions options, out int requiredHeaderSize, out int requiredTrailerSize)
		{
			this.GetCompletedContext().GetWrapBufferSizes(options, out requiredHeaderSize, out requiredTrailerSize);
		}



		protected override ReadOnlySpan<byte> AcceptImpl()
		{
			throw new NotImplementedException();
		}

		protected override ReadOnlySpan<byte> AcceptImpl(ReadOnlySpan<byte> token)
		{
			byte[] mechToken;
			if (this._mechContext is null)
			{
				var gssToken = Asn1DerDecoder.DecodeTlv<InitialContextToken>(token.ToArray()).Value;

				if (gssToken.thisMech == SpnegoClientContext.SpnegoOid)
					// TODO: Figure out real error code
					throw new NotImplementedException();

				var neg = Asn1DerDecoder.DecodeTlv<NegotiationToken>(gssToken.innerContextToken.TlvBytes);
				if (neg.SelectedChoice != NegotiationToken.ChoiceIndex.NegTokenInit)
					throw new NotImplementedException();

				var negInit = neg.NegTokenInit;

				this._mechTypeList = negInit.mechTypes;

				var authContext = this._provider.TryGetContext(negInit.mechTypes[0]);
				if (authContext is null)
				{
					// Find first matching
					foreach (var mechOid in neg.NegTokenInit.mechTypes)
					{
						authContext = this._provider.TryGetContext(mechOid);
						if (authContext != null)
							break;
					}

					if (authContext is null)
						throw new NotImplementedException();

					this._mechContext = authContext;

					return Asn1DerEncoder.EncodeTlv(new NegotiationToken()
					{
						NegTokenResp = new NegTokenResp(NegTokenResp_NegState_Tagged0.Request_mic, authContext.MechOid)
					}).Span;

				}

				this._mechContext = authContext;

				mechToken = negInit.mechToken;
			}
			else
			{
				var gssToken = Asn1DerDecoder.DecodeTlv<NegotiationToken>(token.ToArray());
				if (gssToken.SelectedChoice != NegotiationToken.ChoiceIndex.NegTokenResp)
					throw new NotImplementedException();
				// TODO: Check response code
				if (gssToken.NegTokenResp.negState != NegTokenResp_NegState_Tagged0.Accept_incomplete)
					throw new NotImplementedException();

				mechToken = gssToken.NegTokenResp.responseToken;
			}

			var mechContext = this._mechContext;
			var mechRespToken = mechContext.Accept(mechToken);

			var gssRespToken = Asn1DerEncoder.EncodeTlv(new NegotiationToken()
			{
				NegTokenResp = new NegTokenResp(mechContext.IsComplete ? NegTokenResp_NegState_Tagged0.Accept_completed : NegTokenResp_NegState_Tagged0.Accept_incomplete, mechContext.MechOid, mechRespToken.ToArray())
			});

			return gssRespToken.Span;
		}

		public override void IncrementRecvSeqNbr()
		{
		}

		public override void SealMessage(in MessageSealParams sealParams)
		{
			this.VerifyHasContext().SealMessage(sealParams);
		}

		public override void SignMessage(in MessageSignParams signParams, MessageSignOptions options)
		{
			this.VerifyHasContext().SignMessage(signParams, options);
		}

		public override void UnsealMessage(in MessageSealParams unsealParams)
		{
			this.VerifyHasContext().UnsealMessage(unsealParams);
		}

		public override void VerifyMessage(in MessageVerifyParams verifyParams, MessageSignOptions options)
		{
			this.VerifyHasContext().VerifyMessage(verifyParams, options);
		}

		protected override ReadOnlySpan<byte> GetSessionKeyImpl()
		{
			return this.VerifyHasContext().GetSessionKey();
		}
	}
}
