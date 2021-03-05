using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Crypto;
using Titanis.Mocks;

namespace Titanis.Security.Ntlm.Test
{
	[TestClass]
	public class NtlmTests
	{

		// [MS-NLMP] § 4.2.2.1.1 - LMOWFv1()
		[TestMethod("[MS-NLMP] § 4.2.2.1.1 - LMOWFv1()")]
		public void TestLmowfV1()
		{
			byte[] actual = Ntlm.LmowfV1(TestInputValues.Password).AsSpan().ToArray();
			CollectionAssert.AreEqual(TestOutputValues.Lmowf1, actual);
		}

		// [MS-NLMP] § 4.2.2.1.2 - NTOWFv1()
		[TestMethod("[MS-NLMP] § 4.2.2.1.2 - NTOWFv1()")]
		public void TestNtowfV1()
		{
			byte[] actual = Ntlm.NtowfV1(TestInputValues.Password).AsSpan().ToArray();
			CollectionAssert.AreEqual(TestOutputValues.Ntowf1, actual);
		}

		// [MS-NLMP] § 4.2.2.1.3 - Session Base Key and Key Exchange Key
		[TestMethod("[MS-NLMP] § 4.2.2.1.3 - Session Base Key and Key Exchange Key")]
		public void TestSessionBaseKey()
		{
			var actual = Ntlm.ComputeResponseV1(
				0,
				new NtlmHashCredential(
					TestInputValues.UserName,
					TestInputValues.Domain,
					new Buffer128(TestOutputValues.Lmowf1),
					new Buffer128(TestOutputValues.Ntowf1)
				),
				TestInputValues.ClientChallenge,
				TestInputValues.ServerChallenge
				);

			// [MS-NLMP] § 4.2.2.1.3 - Session Base Key and Key Exchange Key
			CollectionAssert.AreEqual(TestOutputValues.SessionBaseKey_V1, actual.SessionBaseKey.AsSpan().ToArray());
		}

		// [MS-NLMP] § 4.2.2.2.1 - NTLMv1 Response
		[TestMethod("[MS-NLMP] § 4.2.2.2.1 - NTLMv1 Response")]
		public void TestNtlmv1Response()
		{
			var actual = Ntlm.ComputeResponseV1(
				0,
				new NtlmHashCredential(
					TestInputValues.UserName,
					TestInputValues.Domain,
					new Buffer128(TestOutputValues.Lmowf1),
					new Buffer128(TestOutputValues.Ntowf1)
				),
				TestInputValues.ClientChallenge,
				TestInputValues.ServerChallenge
				);

			// [MS-NLMP] § 4.2.2.2.1 - NTLMv1 Response
			CollectionAssert.AreEqual(TestOutputValues.LMResponseV1, actual.LmChallengeResponse.AsSpan().ToArray());
		}

		// [MS-NLMP] § 4.2.2.2.2 - LMv1 Response
		[TestMethod("[MS-NLMP] § 4.2.2.2.2 - LMv1 Response")]
		public void TestLmv1Response()
		{
			var actual = Ntlm.ComputeResponseV1(
				0,
				new NtlmHashCredential(
					TestInputValues.UserName,
					TestInputValues.Domain,
					new Buffer128(TestOutputValues.Lmowf1),
					new Buffer128(TestOutputValues.Ntowf1)
				),
				TestInputValues.ClientChallenge,
				TestInputValues.ServerChallenge
				);

			// [MS-NLMP] § 4.2.2.2.2 - LMv1 Response
			CollectionAssert.AreEqual(TestOutputValues.NTResponseV1, actual.NtChallengeResponse.Span.ToArray());
		}




		// [MS-NLMP] § 4.2.2.2.3 - Encrypted Session Key
		static void TestEncryptedSessionKey_v1(NegotiateFlags negFlags, byte[] expected)
		{
			byte[] kxkey = Ntlm.KxkeyV1(
				negFlags,
				new NtlmResponse
				{
					keys = new NtlmResponseKeys
					{
						ResponseKeyLM = new Buffer128(TestOutputValues.Lmowf1),
						ResponseKeyNT = new Buffer128(TestOutputValues.Ntowf1)
					},
					LmChallengeResponse = new Buffer192(TestOutputValues.LMResponseV1),
					NtChallengeResponse = TestOutputValues.NTResponseV1,
					SessionBaseKey = new Buffer128(TestOutputValues.SessionBaseKey_V1)
				},
				TestInputValues.ServerChallenge
				).AsSpan().ToArray();
			var rc4 = new Rc4().CreateEncryptor(kxkey, null);
			byte[] encrypted = rc4.TransformFinalBlock(
				TestInputValues.RandomSessionKey,
				0,
				TestInputValues.RandomSessionKey.Length
				);

			CollectionAssert.AreEqual(expected, encrypted);
		}

		// [MS-NLMP] § 4.2.2.2.3 - Encrypted Session Key
		[TestMethod("[MS-NLMP] § 4.2.2.2.3 - Encrypted Session Key (RC4 encryption)")]
		public void TestEncryptedSessionKey_V1()
		{
			TestEncryptedSessionKey_v1(0, TestOutputValues.EncryptedSessionKey_V1);
		}

		// [MS-NLMP] § 4.2.2.2.3 - Encrypted Session Key
		[TestMethod("[MS-NLMP] § 4.2.2.2.3 - Encrypted Session Key (NON_NT)")]
		public void TestEncryptedSessionKey_NonNT()
		{
			TestEncryptedSessionKey_v1(NegotiateFlags.R_RequestNonNTSessionKey, TestOutputValues.EncryptedSessionKey_V1_NonNT);
		}

		// [MS-NLMP] § 4.2.2.2.3 - Encrypted Session Key
		[TestMethod("[MS-NLMP] § 4.2.2.2.3 - Encrypted Session Key (LM_KEY)")]
		public void TestEncryptedSessionKey_LM()
		{
			TestEncryptedSessionKey_v1(NegotiateFlags.G_NegotiateLMKey, TestOutputValues.EncryptedSessionKey_V1_LM);
		}

		// [MS-NLMP] § 4.2.2.4 - GSS_WrapEx Examples
		[TestMethod("[MS-NLMP] § 4.2.2.4 - GSS_WrapEx Examples (CRC32)")]
		public void TestCrc32()
		{
			uint crc32 = Ntlm.Crc32(SecBufferList.Create(SecBuffer.Integrity(TestInputValues.PlaintextBytes)));
			Assert.AreEqual(TestOutputValues.ExpectedCrc32, crc32);

		}
		// [MS-NLMP] § 4.2.2.4 - GSS_WrapEx Examples
		[TestMethod("[MS-NLMP] § 4.2.2.4 - GSS_WrapEx Examples (Seal Output)")]
		public void TestSeal_V1()
		{
			byte[] message = (byte[])TestInputValues.PlaintextBytes.Clone();


			NtlmAuthResult authResult = new NtlmAuthResult
			{
				negFlags = TestInputValues.ChallengeFlags_422,
				sealKeyC2S = new Buffer128(TestInputValues.RandomSessionKey),
			};
			NtlmCryptoContext crypto = new NtlmCryptoContext();
			crypto.SetCryptoContext(authResult);
			byte[] mac = new byte[NtlmMessageSignatureV1.StructSize];
			Ntlm.SealV1(
				new MessageSealParams(default, SecBufferList.Create(
						SecBuffer.PrivacyWithIntegrity(message)
					), mac),
				ref crypto.sealKeyC2S,
				TestInputValues.SeqNbr,
				TestInputValues.RandomPad
				);

			CollectionAssert.AreEqual(TestOutputValues.EncryptedMessage_422, message);

			ref NtlmMessageSignatureV1 sig = ref MemoryMarshal.AsRef<NtlmMessageSignatureV1>(mac);

			Assert.AreEqual(TestOutputValues.ExpectedRandomPadCipher_422, sig.randomPad);
			Assert.AreEqual(TestOutputValues.EncryptedChecksum_422, sig.checksum);
			Assert.AreEqual(TestOutputValues.EncryptedSeqnbr_422, sig.seqnbr);
		}

		[TestMethod("[MS-NLMP] § 4.2.2 NTLM v1 Authentication")]
		public void TestContext_422()
		{
			NtlmMessageSignatureV1 sig = new NtlmMessageSignatureV1();
			var message = ContexCryptoTestHelper(
				TestInputValues.ChallengeFlags_422,
				sig.AsSpan(), false
				);

			CollectionAssert.AreEqual(TestOutputValues.EncryptedMessage_422, message);

			Assert.AreEqual(TestOutputValues.ExpectedRandomPadCipher_422, sig.randomPad);
			Assert.AreEqual(TestOutputValues.EncryptedChecksum_422, sig.checksum);
			Assert.AreEqual(TestOutputValues.EncryptedSeqnbr_422, sig.seqnbr);
		}

		[TestMethod("[MS-NLMP] § 4.2.3 - NTLM v1 with Client Challenge")]
		public void TestContext_423()
		{
			NtlmMessageSignatureV2 sig = new NtlmMessageSignatureV2();
			var message = ContexCryptoTestHelper(
				TestInputValues.ChallengeFlags_423,
				sig.AsSpan(), false
				);

			CollectionAssert.AreEqual(TestOutputValues.EncryptedMessage_423, message);

			Assert.AreEqual(TestOutputValues.EncryptedChecksum_423, sig.checksum);
			Assert.AreEqual(TestInputValues.SeqNbr, sig.seqnbr);
		}

		[TestMethod("[MS-NLMP] § 4.2.4 - NTLMv2 Authentication")]
		public void TestContext_424()
		{
			NtlmMessageSignatureV2 sig = new NtlmMessageSignatureV2();
			var message = ContexCryptoTestHelper(
				TestInputValues.ChallengeFlags_424,
				sig.AsSpan(),
				true
				);

			CollectionAssert.AreEqual(TestOutputValues.EncryptedMessage_424, message);

			Assert.AreEqual(TestOutputValues.EncryptedChecksum_424, sig.checksum);
			Assert.AreEqual(TestInputValues.SeqNbr, sig.seqnbr);
		}

		private static byte[] ContexCryptoTestHelper(
			NegotiateFlags challengeFlags,
			Span<byte> sigbuf,
			bool ntlmv2
			)
		{
			byte[] message = (byte[])TestInputValues.PlaintextBytes.Clone();

			NtlmPasswordCredential cred = new NtlmPasswordCredential(TestInputValues.UserName, TestInputValues.Password);

			NtlmClientContext context = new NtlmClientContext(cred, ntlmv2);

			ref var state = ref context.GetState();
			state = new NtlmAuthContextState
			{
				negotiateFlags = challengeFlags,
				challengeFlags = challengeFlags,
				negAuthFlags = challengeFlags,
				challengeFromClient = TestInputValues.ClientChallenge,
				serverChallenge = TestInputValues.ServerChallenge,
				serverName = TestInputValues.ServerName,
				randomKey = new Buffer128(TestInputValues.RandomSessionKey),
			};

			context.BuildAuthToken(new NtlmAvInfo
			{
				NbComputerName = TestInputValues.ServerName,
				NbDomainName = TestInputValues.Domain
			});

			MessageSealParams sealParams = new(default, SecBufferList.Create(SecBuffer.PrivacyWithIntegrity(message)), sigbuf);
			context.SealMessage(ref sealParams);

			return message;
		}
	}
}
