using KerberosV5_PK_INIT_SPEC;
using KerberosV5Spec2;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Crypto.DiffieHellman;
using Titanis.Ldap;
using Titanis.Mocks;
using Titanis.Security;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb.Test;

[TestClass]
public class AsreqTests : CliCommandTest<AsreqCommand>
{
	private const string KdcName = "LUMON-DC1";
	private const string RealmFqdn = "LUMON.IND";
	private const string RealmName = "LUMON";
	private const string MilchickUpn = "milchick@LUMON.IND";
	private const string MilchickName = "milchick";
	private const string MilchickSalt = "LUMON.INDseth";
	private const string MilchickPassword = "Br3@kr00m!";

	private Mock<IKerberosTransport> kerbTransport;

	protected override void InitializeHostServices(ServiceContainer hostServices)
	{
		base.InitializeHostServices(hostServices);

		var kerbTransport = mocks.Create<IKerberosTransport>();
		this.kerbTransport = kerbTransport;
		hostServices.AddService(typeof(IKerberosTransport), kerbTransport.Object);
	}

	private static bool HasPreauth(KDC_REQ_CHOICE kdcreq)
	{
		bool isInitial =
			kdcreq.SelectedChoice == KDC_REQ_CHOICE.ChoiceIndex.Asreq
			&& kdcreq.Asreq.padata.Any(r => (PadataType)r.padata_type is PadataType.EncTimestamp or PadataType.PkASReq or PadataType.PkASreqOld);
		return isInitial;
	}

	private static readonly EType[] etypes = [EType.Aes256CtsHmacSha1_96, EType.Aes128CtsHmacSha1_96, EType.Rc4Hmac, EType.DesCbcMd5];

	[TestMethod]
	[CliTest("milchick_password")]
	public async Task TestMilchickPassword(Token[] tokens)
	{
		TicketInfo ticket = await this.DoAsreqTest(tokens, RealmName, Structs.PrincipalName(PrincipalNameType.Principal, MilchickName), Structs.PrincipalName(PrincipalNameType.ServiceInstance, ServiceClassNames.Krbtgt, RealmName), etypes);

		Assert.IsNotNull(ticket);
	}

	[TestMethod]
	[CliTest("milchickUpn_password")]
	public async Task TestMilchickUpnPassword(Token[] tokens)
	{
		TicketInfo ticket = await this.DoAsreqTest(tokens, RealmFqdn, Structs.PrincipalName(PrincipalNameType.Enterprise, MilchickUpn), Structs.PrincipalName(PrincipalNameType.ServiceInstance, ServiceClassNames.Krbtgt, RealmFqdn), etypes);

		Assert.IsNotNull(ticket);
	}

	[TestMethod]
	[CliTest("milchick_pkinit")]
	public async Task TestMilchickPkinit(Token[] tokens)
	{
		TicketInfo ticket = await this.DoAsreqTest(tokens, RealmFqdn, Structs.PrincipalName(PrincipalNameType.Enterprise, MilchickUpn), Structs.PrincipalName(PrincipalNameType.ServiceInstance, ServiceClassNames.Krbtgt, RealmFqdn), etypes);

		Assert.IsNotNull(ticket);
	}

	private async Task<TicketInfo> DoAsreqTest(
		Token[] tokens,
		string expectedRealm,
		PrincipalName expectedCname,
		PrincipalName expectedSname,
		EType[] expectedETypes
		)
	{
		var signerCert = new X509Certificate2(this.fileAccess.ReadAllBytesFrom(new FileSpec("milchick.pfx")), "password");

		// Initial AS-REQ (no preauth)
		kerbTransport.Expect(r => r.TransceiveKdcAsync(
			expectedRealm,
			Arg.Matches<DnsEndPoint>(r => r.Host == KdcName && r.Port == KerberosClient.KdcTcpPort),
			Arg.Matches<KDC_REQ_CHOICE>(r => !HasPreauth(r)),
			this.TestContext.CancellationToken)
		).ReturnAsync((object[] args) =>
		{
			var kdcreq = (KDC_REQ_CHOICE)args[2];
			var asreq = kdcreq.Asreq;

			Assert.AreEqual(expectedSname, asreq.req_body.sname);
			Assert.AreEqual(expectedCname, asreq.req_body.cname);
			Assert.AreEqual(expectedRealm, asreq.req_body.realm.Value);

			foreach (var etype in expectedETypes)
			{
				Assert.Contains((int)etype, asreq.req_body.etype);
			}

			return Task.FromResult(new KDC_REP_CHOICE
			{
				Error = new KRB_ERROR_Tagged30(
					5,
					(byte)KrbMessageType.Error,
					new GeneralizedTime(DateTime.UtcNow),
					42,
					(int)KerberosErrorCode.KDC_ERR_PREAUTH_REQUIRED,
					new Asn1.GeneralString(RealmFqdn),
					Structs.PrincipalName(PrincipalNameType.ServiceInstance, ServiceClassNames.Krbtgt, RealmFqdn),

					e_data: Asn1DerEncoder.EncodeTlv(new Asn1SequenceOf<PA_DATA>([
						Structs.PAData_ETypeInfo2(
								Structs.PAData_ETypeInfo2Entry(EType.Aes256CtsHmacSha1_96, MilchickSalt),
								Structs.PAData_ETypeInfo2Entry(EType.Aes128CtsHmacSha1_96, MilchickSalt)
							),
							new PA_DATA((int)PadataType.EncTimestamp,[]),
							new PA_DATA((int)PadataType.PkASReq,null),
							new PA_DATA((int)PadataType.PkASreqOld,null)
						])).ToArray()
					)
			});
		});
		// AS-REQ with preauth
		kerbTransport.Expect(r => r.TransceiveKdcAsync(
			expectedRealm,
			Arg.Matches<DnsEndPoint>(r => r.Host == KdcName && r.Port == KerberosClient.KdcTcpPort),
			Arg.Matches<KDC_REQ_CHOICE>(r => HasPreauth(r)),
			this.TestContext.CancellationToken)
		).ReturnAsync((object[] args) =>
		{
			var kdcreq = (KDC_REQ_CHOICE)args[2];
			var asreq = kdcreq.Asreq;

			var ts = asreq.padata.FirstOrDefault(r => (PadataType)r.padata_type == PadataType.EncTimestamp);
			var pkasreq = asreq.padata.FirstOrDefault(r => (PadataType)r.padata_type == PadataType.PkASReq);
			KerberosClient krb = new KerberosClient();
			EncProfile? encProfile;
			SessionKey? clientKey;
			PA_DATA[]? padata = null;
			if (ts != null)
			{
				var tsCipher = Asn1DerDecoder.DecodeTlv<EncryptedData>(ts.padata_value);
				encProfile = krb.GetEncProfile((EType)tsCipher.etype);
				clientKey = encProfile.StringToKey(MilchickPassword, MilchickSalt);
				var tsPlain = clientKey.Decrypt(KeyUsage.AsreqPaEncTimestamp, tsCipher.cipher).ToArray();
				var timestamp = Asn1DerDecoder.DecodeTlv<PA_ENC_TS_ENC>(tsPlain);
				// TODO: Verify time
			}
			else if (pkasreq != null)
			{
				var pkreq = Asn1DerDecoder.DecodeTlv<PA_PK_AS_REQ>(pkasreq.padata_value);
				var cms = new SignedCms();
				cms.Decode(pkreq.signedAuthPack);
				var authPack = Asn1DerDecoder.DecodeTlv<AuthPack>(cms.ContentInfo.Content);
				var pkAuth = authPack.pkAuthenticator;

				var clientExponent = Asn1DerDecoder.DecodeTlv<Asn1Integer>(authPack.clientPublicValue.subjectPublicKey.Octets).Value;

				// TODO: Verify freshness token
				// TODO: Verify checksum
				// TODO: Verify algorithm is DH
				// TODO: Verify client DH nonce length

				var modpGroup = new ModpGroup(authPack.clientPublicValue.algorithm.parameters);
				var dhkey = ModpKeyPair.Generate(modpGroup, modpGroup.BitLength - 1);

				var zz = dhkey.GenerateSessionKey(authPack.clientPublicValue.subjectPublicKey);

				byte[] serverNonce = new byte[32];

				// Just pick the first etype
				encProfile = krb.GetEncProfile((EType)asreq.req_body.etype[0]);
				clientKey = PreauthPkinitContext.DeriveProtocolKey(encProfile, zz, authPack.clientDHNonce, serverNonce);

				SignedCms signedReply = new SignedCms(new ContentInfo(new Oid(KerberosV5_PK_INIT_SPECModule.id_pkinit_DHKeyData.ToOid()), Asn1DerEncoder.EncodeTlv(new KDCDHKeyInfo(new Asn1BitString(dhkey.EncodePublicExponent(), 0), pkAuth.nonce
					)).ToArray()));
				signedReply.ComputeSignature(new CmsSigner(signerCert));

				padata = [new PA_DATA((int)PadataType.PkASRep, Asn1DerEncoder.EncodeTlv(new PA_PK_AS_REP() {
					DhInfo = new DHRepInfo(signedReply.Encode(), serverNonce)
				}).ToArray())];
			}
			else
			{
				Assert.Fail("No preauthentication");
				throw null;
			}

			PrincipalName sname = new((int)PrincipalNameType.ServiceInstance, [new GeneralString(ServiceClassNames.Krbtgt), new GeneralString(RealmFqdn)]);
			EncryptionKey sessionKey = encProfile.GenerateSubkey().key;

			return Task.FromResult(new KDC_REP_CHOICE
			{
				Asrep = new KDC_REP(
					5,
					(byte)KrbMessageType.Asrep,
					new GeneralString(RealmFqdn),
					Structs.PrincipalName(PrincipalNameType.Principal, MilchickName),
					new Ticket_Tagged1(5, new GeneralString(RealmFqdn), sname, new EncryptedData(0, [])),
					clientKey.EncryptAndWrap(KeyUsage.AsrepEncPart, Asn1DerEncoder.EncodeTlv(new EncASRepPart(new EncKDCRepPart(
						sessionKey,
						[],
						asreq.req_body.nonce,
						new Asn1BitString(0),
						new GeneralizedTime(),
						new GeneralizedTime(),
						new GeneralString(RealmFqdn),
						sname
						))).ToArray()),
					padata)
			});
		});

		var result = await TestCommand(tokens);
		Assert.HasCount(1, result);
		TicketInfo ticket = (TicketInfo)result[0];
		return ticket;
	}
}
