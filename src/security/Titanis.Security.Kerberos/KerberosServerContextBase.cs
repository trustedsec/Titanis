using KerberosV5Spec2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Implements the server side of a Kerberos authentication context.
	/// </summary>
	/// <seealso cref="MskileClientContext"/>
	/// <seealso cref="KerberosServerContext"/>
	public abstract class KerberosServerContextBase : AuthServerContext
	{
		public KerberosServerContextBase(IKerberosKeyStore keystore)
		{
			this._keystore = keystore;
		}

		private bool _isComplete;
		public override bool IsComplete => this._isComplete;


		private SessionKey? _sessionKey;
		public override bool HasSessionKey => this._sessionKey != null;

		public override int SessionKeySize => (this._sessionKey ?? throw new InvalidOperationException(Messages.Krb5_ContextNotInitialized)).key.keyvalue.Length;

		private SessionKey? _initiatorSubkey;
		private SessionKey? _acceptorSubkey;
		private bool _useAcceptorSubkey;

		private readonly IKerberosKeyStore _keystore;

		private SecurityCapabilities _negcap;

		/// <inheritdoc/>
		public override SecurityCapabilities NegotiatedCapabilities => this._negcap;
		/// <summary>
		/// Gets a value indicating whether confidentiality has been negotiated.
		/// </summary>
		public bool NegotiatedConfidentiality => 0 != (this.NegotiatedCapabilities & SecurityCapabilities.Confidentiality);
		/// <summary>
		/// Gets a value indicating whether integrity has been negotiated.
		/// </summary>
		public bool NegotiatedIntegrity => 0 != (this.NegotiatedCapabilities & SecurityCapabilities.Integrity);

		/// <inheritdoc/>
		public override SecurityCapabilities SupportedCapabilities => SecurityCapabilities.Confidentiality | SecurityCapabilities.Integrity | SecurityCapabilities.ReplayDetection | SecurityCapabilities.MutualAuthentication | SecurityCapabilities.SequenceDetection | SecurityCapabilities.DceStyle | SecurityCapabilities.Delegation | SecurityCapabilities.ExtendedError;

		/// <inheritdoc/>
		public sealed override int SignTokenSize => this.VerifySessionKey().EncryptionProfile.SignTokenSize;

		/// <inheritdoc/>
		/// <remarks>
		/// Kerberos supports header rotation to effectively combine the header and trailer into a single token buffer.
		/// </remarks>
		public sealed override int GetWrapTokenSize(WrapOptions options)
		{
			this.GetWrapBufferSizes(options, out var header, out var trailer);
			return header + trailer;
		}
		/// <inheritdoc/>
		public override void GetWrapBufferSizes(WrapOptions options, out int requiredHeaderSize, out int requiredTrailerSize)
		{
			var encProfile = this.VerifySessionKey().EncryptionProfile;
			encProfile.GetWrapBufferSizes(options, out requiredHeaderSize, out requiredTrailerSize);
		}


		protected override ReadOnlySpan<byte> AcceptImpl()
		{
			throw new NotImplementedException();
		}

		private static readonly KerberosClient krb = new KerberosClient();

		protected override ReadOnlySpan<byte> AcceptImpl(ReadOnlySpan<byte> token)
		{
			AP_REQ apreq;
			if (!this.IsDceRpcStyle)
			{
				//apreq = Asn1DerDecoder.DecodeTlv<AP_REQ>(token.ToArray());

				var krb5Token = Asn1DerDecoder.DecodeTlv<Asn1.Krb5Token>(token.ToArray());

				var mechOid = krb5Token.mechId;
				if (!(
					mechOid == KerberosClientContextBase.KerberosOid
					|| mechOid == KerberosClientContextBase.MskileOid
					))
					throw new NotImplementedException();
				if (krb5Token.tokenId != GssapiTokenId.APReq)
					throw new NotImplementedException();

				apreq = krb5Token.apreq;
			}
			else
			{
				throw new NotImplementedException();
			}
			var apreq_ = apreq.Value;
			if (apreq_.pvno != 5)
				throw new NotImplementedException();

			var ticket = apreq_.ticket;
			var spn = ticket.sname.ToSecurityPrincipalName();

			var encProf = krb.TryGetEncProfile((EType)ticket.enc_part.etype);
			if (encProf == null)
				throw new NotImplementedException();

			var ltsKey = this._keystore.TryGetKeyFor(spn, encProf);
			if (ltsKey == null)
				throw new NotImplementedException();

			var encPartTicketBytes = ltsKey.Decrypt(KeyUsage.Asrep_Tgsrep_Ticket, ticket.enc_part);
			var encPartTicket = Asn1DerDecoder.DecodeTlv<EncTicketPart>(encPartTicketBytes).Value;

			// TODO: Check ticket flags
			// TODO: Check ticket in general

			// TODO: Verify authorization; for now just do it.
			var sessionKey = krb.CreateSessionKeyFor(encPartTicket.key);
			this._sessionKey = sessionKey;

			var authBytes = sessionKey.Decrypt(KeyUsage.ApreqAuth_AppSessionKey_IncludesAuthSubkey, apreq_.authenticator);
			var auth = Asn1DerDecoder.DecodeTlv<Authenticator>(authBytes).Value;

			this.RecvSeqNbr = (uint)auth.seq_number.GetValueOrDefault(0);

			if (auth.cksum is null)
				throw new NotImplementedException();
			if (auth.cksum.cksumtype != AuthChecksumToken.ChecksumType)
				throw new NotImplementedException();

			AuthChecksumToken authChecksum = KerberosReader.ReadAuthChecksum(auth.cksum.checksum);

			this._negcap = authChecksum.capabilities;

			var initiatorSubkey = (auth.subkey != null) ? krb.CreateSessionKeyFor(auth.subkey) : null;
			this._initiatorSubkey = initiatorSubkey;

			var apopts = (APOptions)apreq_.ap_options.ToUInt64();
			if (0 != (apopts & APOptions.UseSessionKey))
				throw new NotImplementedException();

			SessionKey? acceptorSubkey;
			if (0 != (apopts & APOptions.MutualRequired))
			{
				if (initiatorSubkey.EType is EType.Rc4Hmac or EType.Rc4HmacExp)
				{
					acceptorSubkey = initiatorSubkey;
				}
				else
				{
					acceptorSubkey = encProf.GenerateSubkey();
					this._useAcceptorSubkey = true;
				}
				this._acceptorSubkey = acceptorSubkey;
			}
			else
			{
				acceptorSubkey = null;
			}

			var sendSeqnbr = (uint)KerberosClient.GenerateNonce();
			this.SendSeqNbr = sendSeqnbr;
			EncAPRepPart encAprep = new EncAPRepPart(new EncAPRepPart_Tagged27(
				auth.ctime,
				auth.cusec,
				acceptorSubkey?.key,
				sendSeqnbr
				));
			var aprep = new AP_REP(new AP_REP_Tagged15(5, (byte)KrbMessageType.Aprep, sessionKey.EncryptTlv(KeyUsage.APRep_EncPart, encAprep)));

			this._isComplete = true;

			if (!this.IsDceRpcStyle)
			{
				return Asn1DerEncoder.EncodeTlv(new Asn1.Krb5Token()
				{
					tokenId = GssapiTokenId.APRep,
					aprep = aprep,
					mechId = KerberosClientContextBase.KerberosOid
				}).ToArray();
			}
			else
			{
				throw new NotImplementedException();
			}

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


		public uint SendSeqNbr { get; private set; }
		public uint RecvSeqNbr { get; private set; }
		private SessionKey VerifySessionKey()
		{
			var sessionKey = this._sessionKey;
			if (sessionKey == null)
				throw new InvalidOperationException(Messages.Krb5_ContextNotInitialized);
			return sessionKey;
		}
		private uint GetSeqNbrForReceive()
		{
			return (uint)(this.RecvSeqNbr++);
		}

		private uint GetSeqNbrForSend()
		{
			var seqNbr = (uint)(this.SendSeqNbr++);
#if DEBUG
			Debug.WriteLine($"[krb5]: Sending with seq # {seqNbr}");
#endif
			return seqNbr;
		}

		public override void UnsealMessage(in MessageSealParams unsealParams)
		{
			Debug.Assert(this.NegotiatedConfidentiality);
			var sessionKey =
				this._acceptorSubkey ?? this._initiatorSubkey
				?? this.VerifySessionKey();

			uint seqNbr = this.GetSeqNbrForReceive();
			var flags = this._useAcceptorSubkey
				? (WrapFlags.Sealed | WrapFlags.AcceptorSubkey)
				: WrapFlags.Sealed;
			sessionKey.UnsealMessage(
				KeyUsage.InitiatorSeal,
				seqNbr,
				flags,
				in unsealParams
				);
		}

		public override void VerifyMessage(in MessageVerifyParams verifyParams, MessageSignOptions options)
		{
			throw new NotImplementedException();
		}

		protected override ReadOnlySpan<byte> GetSessionKeyImpl()
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>
	/// Implementation of <see cref="KerberosServerContextBase"/> that identifies itself using the mechanism OID for [MS-KILE].
	/// </summary>
	public class MskileServerContext : KerberosServerContextBase
	{
		public MskileServerContext(IKerberosKeyStore keystore)
			: base(keystore)
		{
		}

		/// <inheritdoc/>
		public override Asn1Oid MechOid => KerberosClientContextBase.MskileOid;
	}
	/// <summary>
	/// Implementation of <see cref="KerberosServerContextBase"/> that identifies itself using the mechanism OID for [RFC 4120].
	/// </summary>
	public class KerberosServerContext : KerberosServerContextBase
	{
		public KerberosServerContext(IKerberosKeyStore keystore)
			: base(keystore)
		{
		}

		/// <inheritdoc/>
		public override Asn1Oid MechOid => KerberosClientContextBase.KerberosOid;
	}
}
