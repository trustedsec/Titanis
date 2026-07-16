using KerberosV5_PK_INIT_SPEC;
using KerberosV5Spec2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Crypto;
using Titanis.Crypto.DiffieHellman;

namespace Titanis.Security.Kerberos
{
	class PreauthPkinitContext : PreauthContext
	{
		public PreauthPkinitContext(KerberosClient client, KerberosPkinitCredential credential, IKerberosCallback? callback = null) : base(client, callback)
		{
			this.Credential = credential;

			ModpKeyPair dhkey = ModpKeyPair.Generate(ModpGroups.Group14, 2048 - 1);
			this._dhkey = dhkey;

			// Windows uses a 32-byte nonce
			byte[] clientDhNonce = RandomNumberGenerator.GetBytes(32);
			this._clientDhNonce = clientDhNonce;
		}

		private byte[] _clientDhNonce;

		protected override KerberosPkinitCredential Credential { get; }

		// [RFC 4556] § 3.2.1
		private static readonly Asn1Oid IdSignedData = new Asn1Oid("1.2.840.113549.1.7.2");
		private static readonly Asn1Oid DhPublicNumber = new Asn1Oid("1.2.840.10046.2.1");
		private readonly ModpKeyPair _dhkey;

		protected override bool ProcessPadata(Guid correlationId, PA_DATA padata)
		{
			switch ((PadataType)padata.padata_type)
			{
				// [RFC 4556] § 3.1.3
				case PadataType.PkASreqOld:
					break;

				case PadataType.PkASReq:
					this.ProcessPkAsreq(padata.padata_value);
					return true;
				case PadataType.PkASRep:
					this.ProcessPkAsrep(padata.padata_value);
					return true;

				// [RFC 8070]
				case PadataType.AsFreshness:
					this.ProcessFreshness(padata.padata_value);
					break;

					// [MS-PKCA] 
					//case PadataType.PkASreqOld:
					//	this.ProcessPkAsrepOld(padata.padata_value);
					//	break;
			}
			return base.ProcessPadata(correlationId, padata);
		}

		private PadataType _pkinitType;
		private byte[] _zz;
		private byte[]? _serverNonce;

		private void ProcessPkAsreq(byte[] data)
		{
			if (this._pkinitType is 0 or PadataType.PkASreqOld)
			{
				this._pkinitType = PadataType.PkASReq;
			}
		}

		private void ProcessPkAsreqOld(byte[] data)
		{
			if (this._pkinitType is 0)
			{
				this._pkinitType = PadataType.PkASreqOld;
			}
		}

		private void ProcessPkAsrep(byte[] padata_value)
		{
			var pkasrep = Asn1DerDecoder.DecodeTlv<PA_PK_AS_REP>(padata_value);
			var signed = new SignedCms();
			signed.Decode(pkasrep.DhInfo.dhSignedData);
			// TODO: Verify signatures and trust and all that stuff

			if (KerberosV5_PK_INIT_SPECModule.id_pkinit_DHKeyData != signed.ContentInfo.ContentType)
				throw new ProtocolViolationException($"The server returned a PK-AS-REP with the wrong content type.  Expected '{KerberosV5_PK_INIT_SPECModule.id_pkinit_DHKeyData.Text}' but received '{signed.ContentInfo.ContentType}'");

			// TODO: Certificate and signature verification

			var dhInfo = Asn1DerDecoder.DecodeTlv<KDCDHKeyInfo>(signed.ContentInfo.Content);
			var serverNonce = pkasrep.DhInfo.serverDHNonce;
			var zz = this._dhkey.GenerateSessionKey(dhInfo.subjectPublicKey);
			this._zz = zz;
			this._serverNonce = serverNonce;
		}

		private byte[]? _freshnessToken;
		private void ProcessFreshness(byte[] token)
		{
			this._freshnessToken = token;
		}

		internal static void DeriveKey(ReadOnlySpan<byte> zz, ReadOnlySpan<byte> clientNonce, ReadOnlySpan<byte> serverNonce, Span<byte> seed)
		{
			var cbDigest = Sha1Context.StaticDigestSizeBytes;

			Span<byte> sha1 = stackalloc byte[cbDigest];
			Span<byte> prefix = stackalloc byte[1];

			int cBlocks = (seed.Length + cbDigest - 1) / cbDigest;
			for (int i = 0; i < cBlocks; i += 1)
			{
				Sha1Context ctx = new Sha1Context();
				ctx.Initialize();

				prefix[0] = (byte)i;
				ctx.HashData(prefix);
				ctx.HashData(zz);
				ctx.HashData(clientNonce);
				ctx.HashData(serverNonce);
				ctx.HashFinal(sha1);

				int left = Math.Min(seed.Length - i * sha1.Length, sha1.Length);
				sha1.Slice(0, left).CopyTo(seed.Slice(i * sha1.Length));
			}
		}

		public static SessionKey DeriveProtocolKey(EncProfile encProfile, byte[] zz, byte[] clientDhNonce, byte[] serverNonce)
		{
			Span<byte> seed = stackalloc byte[encProfile.KeyGenerationSeedSizeBytes];
			DeriveKey(zz, clientDhNonce, serverNonce, seed);

#if DEBUG
			Debug.Print($"protokey = " + seed.ToHexString());
#endif
			var key = encProfile.RandomToKey(seed);
			return key;
		}
		public override SessionKey DeriveProtocolKey(EncProfile encProfile) => DeriveProtocolKey(encProfile, this._zz, this._clientDhNonce, this._serverNonce);

		private PA_DATA? _pkinitResponse;

		public PA_DATA DoPkinit(KDC_REQ_BODY kdcReqBody, X509Certificate2 cert)
		{
			Debug.Assert(kdcReqBody != null);
			var kdcReqBodyBytes = Asn1DerEncoder.EncodeTlv(kdcReqBody).ToArray();
			var paChecksum = new byte[20];
			SHA1.HashData(kdcReqBodyBytes.AsSpan(), paChecksum);

			KerberosTime kerbTime = KerberosTime.Now();

			var nonce = (uint)kdcReqBody.nonce;

			PKChecksum2? cksum2 = null;
			if (this.SupportCmsAlgorithms != null)
			{
				foreach (var algId in this.SupportCmsAlgorithms)
				{
					var alg = TlsServerEndPointChannelBinding.TryGetHashAlg(algId.algorithm.ToOid());
					if (alg != null)
					{
						byte[] algHash = alg.ComputeHash(kdcReqBodyBytes);
						cksum2 = new PKChecksum2(
							algHash,
					new PKIX1Explicit88.AlgorithmIdentifier(new Asn1Oid(SignatureAlgorithms.Sha512NoSign.Value), null)
							);
						break;
					}
				}
			}

			// [RFC 4556] § 3.2.1 - Generation of Client Request

			AuthPack authPack = new AuthPack(new PKAuthenticator(
					kerbTime.usec,
					kerbTime.dt,
					(uint)nonce,
					paChecksum,
					this._freshnessToken,
					cksum2
				),
				new PKIX1Explicit88.SubjectPublicKeyInfo(
					new PKIX1Explicit88.AlgorithmIdentifier(DhPublicNumber, this._dhkey.Group.EncodeDomainParameters()),
					new Asn1BitString(this._dhkey.EncodePublicExponent(), 0)
					),
				[],
				this._clientDhNonce
				);

			var authPackBytes = Asn1DerEncoder.EncodeTlv(authPack).ToArray();

			// Now pack it in a CMS ContentInfo
			var cms = new SignedCms(new ContentInfo(KerberosV5_PK_INIT_SPECModule.id_pkinit_authData.ToOid(), authPackBytes));
			cms.ComputeSignature(new CmsSigner(cert));
			var signedBytes = cms.Encode();

			PA_PK_AS_REQ pkreq = new PA_PK_AS_REQ(
				signedBytes,
				null,
				null
				);
			return new PA_DATA((int)PadataType.PkASReq, Asn1DerEncoder.EncodeTlv(pkreq).ToArray());
		}

		protected override void BuildPadataList(KDC_REQ_BODY reqBody, List<PA_DATA> padataList)
		{
			if (this._pkinitType == 0)
			{
				padataList.Add(new PA_DATA((int)PadataType.AsFreshness, Array.Empty<byte>()));
			}
			else if (this._pkinitType == PadataType.PkASReq)
			{
				var pkreq = (this._pkinitResponse ??= this.DoPkinit(reqBody, this.Credential.Certificate));
				padataList.Add(pkreq);

				padataList.Add(new PA_DATA((int)PadataType.AsFreshness, Array.Empty<byte>()));
			}

			base.BuildPadataList(reqBody, padataList);
		}
	}
}
