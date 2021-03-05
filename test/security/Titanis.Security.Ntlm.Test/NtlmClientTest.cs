using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Security.Cryptography;
using Titanis.Crypto;
using Titanis.IO;

namespace Titanis.Security.Ntlm.Test
{
	[TestClass]
	public class NtlmClientTest
	{

		[TestMethod]
		public void TestComputeResponseV1_Extended()
		{
			var actual = Ntlm.ComputeResponseV1(
				NegotiateFlags.P_NegotiateExtendedSessionSecurity,
				new NtlmHashCredential(
					TestInputValues.UserName,
					TestInputValues.Domain,
					new Buffer128(TestOutputValues.Lmowf1),
					new Buffer128(TestOutputValues.Ntowf1)
				),
				TestInputValues.ClientChallenge,
				TestInputValues.ServerChallenge
				);
			CollectionAssert.AreEqual(TestOutputValues.LMResponseV1_Extended, actual.LmChallengeResponse.AsSpan().ToArray());
			CollectionAssert.AreEqual(TestOutputValues.NTResponseV1_Extended, actual.NtChallengeResponse.Span.ToArray());
			CollectionAssert.AreEqual(TestOutputValues.SessionBaseKey_V1, actual.SessionBaseKey.AsSpan().ToArray());
		}

		[TestMethod]
		public void TestKxkeyV1_LM()
		{
			var actual = Ntlm.KxkeyV1(
				NegotiateFlags.G_NegotiateLMKey,
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
				TestInputValues.ServerChallenge);
			CollectionAssert.AreEqual(TestValues.Kxkey_LM, actual.AsSpan().ToArray());
		}

		[TestMethod]
		public void TestKxkeyV1_Extended()
		{
			var actual = Ntlm.KxkeyV1(
				NegotiateFlags.P_NegotiateExtendedSessionSecurity,
				new NtlmResponse
				{
					keys = new NtlmResponseKeys
					{
						ResponseKeyLM = new Buffer128(TestOutputValues.Lmowf1),
						ResponseKeyNT = new Buffer128(TestOutputValues.Ntowf1)
					},
					LmChallengeResponse = new Buffer192(TestOutputValues.LMResponseV1_Extended),
					NtChallengeResponse = TestOutputValues.NTResponseV1_Extended,
					SessionBaseKey = new Buffer128(TestOutputValues.SessionBaseKey_V1)
				},
				TestInputValues.ServerChallenge
				);
			CollectionAssert.AreEqual(TestValues.Kxkey_Extended, actual.AsSpan().ToArray());
		}


		[TestMethod]
		public void TestNtowf2()
		{
			var actual = Ntlm.NtowfV2(
				TestValues.Password,
				TestValues.UserName,
				TestValues.Domain
				).AsSpan().ToArray();
			CollectionAssert.AreEqual(TestValues.ResponseKeyNT_V2, actual);
		}

		[TestMethod]
		public void TestLMChallengeResponseV2()
		{
			var actual = Ntlm.ComputeLMChallengeResponseV2(
				new Buffer128(TestValues.ResponseKeyNT_V2),
				TestInputValues.ServerChallenge,
				TestValues.ClientChallenge);
			CollectionAssert.AreEqual(TestValues.LMChallengeResponse_V2, actual.AsSpan().ToArray());
		}

		[TestMethod]
		public void TestTempV2()
		{
			byte[] expected = new byte[] {
				0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00, 0xaa, 0xaa, 0xaa, 0xaa, 0xaa, 0xaa, 0xaa, 0xaa, 0x00, 0x00, 0x00, 0x00,
				0x02, 0x00, 0x0c, 0x00, 0x44, 0x00, 0x6f, 0x00, 0x6d, 0x00, 0x61, 0x00, 0x69, 0x00, 0x6e, 0x00,
				0x01, 0x00, 0x0c, 0x00, 0x53, 0x00, 0x65, 0x00, 0x72, 0x00, 0x76, 0x00, 0x65, 0x00, 0x72, 0x00,
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
			};
			ByteWriter tempWriter = new ByteWriter(expected.Length);
			Ntlm.BuildTempV2(
				tempWriter,
				TestValues.Time,
				TestValues.ClientChallenge,
				TestValues.TargetInfo.ToBytes().Span);
			byte[] actual = tempWriter.GetData().ToArray();
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestNtProofStr()
		{
			var actual = Ntlm.ComputeSessionBaseKeyV2(
				new Buffer128(TestValues.ResponseKeyNT_V2),
				new Buffer128(TestValues.NTProofStr_V2)
				).AsSpan().ToArray();
			CollectionAssert.AreEqual(TestValues.SessionBaseKey_V2, actual);
		}

		[TestMethod]
		public void TestSessionBaseKey()
		{
			var actual = Ntlm.ComputeSessionBaseKeyV2(
				new Buffer128(TestValues.ResponseKeyNT_V2),
				new Buffer128(TestValues.NTProofStr_V2)
				).AsSpan().ToArray();
			CollectionAssert.AreEqual(TestValues.SessionBaseKey_V2, actual);
		}

		[TestMethod]
		public void TestComputeResponseV2()
		{
			NtlmAuthContextState state = new NtlmAuthContextState
			{
				clientTime = TestValues.Time,
				challengeFromClient = TestValues.ClientChallenge,
				serverChallenge = TestInputValues.ServerChallenge
			};
			var actual = Ntlm.ComputeResponseV2(
				ref state,
				new Buffer128(TestValues.ResponseKeyNT_V2),
				TestValues.TargetInfo.ToBytes().Span
				);
			CollectionAssert.AreEqual(TestValues.NTProofStr_V2, actual.NTProofStr.AsSpan().ToArray());
			CollectionAssert.AreEqual(TestValues.LMChallengeResponse_V2, actual.LmChallengeResponse.AsSpan().ToArray());
		}

		[TestMethod]
		public void TestHandleChallenge()
		{
			NtlmCredential cred = new NtlmPasswordCredential(
				TestValues.UserName,
				TestValues.Domain,
				TestValues.Password
			);
			NtlmClientContext context = new NtlmClientContext(cred)
			{
				ClientConfigFlags = TestValues.ChallengeFlagsV2,
				Workstation = TestValues.Workstation,
				Version = TestValues.Version
			};

			context.Initialize();

			NtlmAuthContextState state = new NtlmAuthContextState
			{
				clientTime = TestValues.Time,
				challengeFromClient = TestValues.ClientChallenge
			};
			var auth = context.HandleChallengeV2(
				ref state,
				context.Options,
				cred,
				TestValues.AuthFlags,
				NtlmChallenge.Parse(TestValues.ChallengeMessage_V2),
				new Buffer128(TestInputValues.RandomSessionKey),
				null);
			CollectionAssert.AreEqual(TestValues.AuthMessage_V2, auth);
		}

		[TestMethod]
		public void TestBuildAuthMessage()
		{
			NtlmAuthContextState state = new NtlmAuthContextState
			{
				clientTime = TestValues.Time,
				challengeFromClient = TestValues.ClientChallenge,
				serverChallenge = TestInputValues.ServerChallenge
			};
			var ntChallengeResponse = Ntlm.ComputeResponseV2(
				ref state,
				new Buffer128(TestValues.ResponseKeyNT_V2),
				TestValues.TargetInfo.ToBytes().Span
				);
			var kxkey = Ntlm.KxkeyV2(ntChallengeResponse.SessionBaseKey);

			NtlmAuthInfo authInfo = new NtlmAuthInfo
			{
				negotiateFlags = TestValues.AuthFlags,
				version = TestValues.Version,
				workstationName = TestValues.Workstation,
				userName = TestValues.UserName,
				userDomain = TestValues.Domain,
				resp = ntChallengeResponse,
				kxkey = kxkey,
				exportedSessionKey = new Buffer128(TestInputValues.RandomSessionKey)
			};
			var actual = NtlmClientContext.BuildAuthMessage(
				ref authInfo,
				ref state
				);
			CollectionAssert.AreEqual(TestValues.AuthMessage_V2, actual.authMessage);
		}

		[TestMethod]
		public void TestDeriveKeysV1_Ext()
		{
			NtlmAuthResult auth = new NtlmAuthResult
			{
				negFlags = NegotiateFlags.P_NegotiateExtendedSessionSecurity | NegotiateFlags.W_Negotiate56,
				exportedSessionKey = new Buffer128(TestValues.Kxkey_Extended),
			};
			Ntlm.DeriveKeys(ref auth);

			CollectionAssert.AreEqual(TestValues.SealingKeyC2S_V1Ext, auth.sealKeyC2S.AsSpan().ToArray());
			CollectionAssert.AreEqual(TestValues.SigningKeyC2S_V1Ext, auth.signKeyC2S.AsSpan().ToArray());
		}

		[TestMethod]
		public void TestDeriveKeysV2()
		{
			NtlmAuthResult auth = new NtlmAuthResult
			{
				negFlags = NegotiateFlags.P_NegotiateExtendedSessionSecurity | NegotiateFlags.U_Negotiate128,
				exportedSessionKey = new Buffer128(TestInputValues.RandomSessionKey),
			};
			Ntlm.DeriveKeys(ref auth);

			CollectionAssert.AreEqual(TestValues.SealingKeyC2S_V2, auth.sealKeyC2S.AsSpan().ToArray());
			CollectionAssert.AreEqual(TestValues.SigningKeyC2S_V2, auth.signKeyC2S.AsSpan().ToArray());
		}

		[TestMethod]
		public void TestCrc32()
		{
			uint crc32 = Ntlm.Crc32(TestValues.Plaintext, new System.ReadOnlySpan<byte>(), new System.ReadOnlySpan<byte>());
			Assert.AreEqual(TestValues.PlaintextCrc, crc32);
		}

		[TestMethod]
		public void TestSealV1()
		{
			var sealingKey = new Rc4Context();
			sealingKey.Initialize(TestInputValues.RandomSessionKey);
			byte[] buffer = (byte[])TestValues.Plaintext.Clone();
			byte[] sigbuf = new byte[NtlmMessageSignatureV2.StructSize];
			Ntlm.SealV1(
				new MessageSealParams
				{
					bufs = new MessageSecBufferList
					{
						buf1 = new MessageSecBuffer(buffer, MessageSecBufferOptions.Privacy)
					},
					macBuffer = sigbuf
				},
				ref sealingKey,
				0,
				0
				);

			//Ntlm.MacV1(
			//	TestValues.Plaintext,
			//	ref sealingKey,
			//	0,
			//	0,
			//	out NtlmMessageSignatureV1 sig);

			CollectionAssert.AreEqual(TestValues.PlaintextSealedV1, buffer);
			CollectionAssert.AreEqual(TestValues.PlaintextMacV1, sigbuf);
		}

		[TestMethod]
		public void TestSealV1_Ext()
		{
			var sealingKey = new Rc4Context();
			sealingKey.Initialize(TestValues.SealingKeyC2S_V1Ext);
			byte[] buffer = (byte[])TestValues.Plaintext.Clone();
			byte[] sigbuf = new byte[NtlmMessageSignatureV2.StructSize];
			Ntlm.SealV2(
				new MessageSealParams
				{
					bufs = new MessageSecBufferList
					{
						buf1 = new MessageSecBuffer(buffer, MessageSecBufferOptions.Privacy),
					},
					macBuffer = sigbuf
				},
				ref sealingKey,
				new Buffer128(TestValues.SigningKeyC2S_V1Ext),
				0,
				false
				);

			//Ntlm.MacV2(
			//	TestValues.Plaintext,
			//	new Buffer128(TestValues.SigningKeyC2S_V1Ext),
			//	ref sealingKey,
			//	false,
			//	0,
			//	out NtlmMessageSignatureV2 sig);

			CollectionAssert.AreEqual(TestValues.PlaintextSealedV1_Ext, buffer);
			CollectionAssert.AreEqual(TestValues.PlaintextMacV1_Ext, sigbuf);
		}

		[TestMethod]
		public void TestSealV2()
		{
			var sealingKey = new Rc4Context();
			sealingKey.Initialize(TestValues.SealingKeyC2S_V2);
			byte[] buffer = (byte[])TestValues.Plaintext.Clone();
			byte[] sigbuf = new byte[NtlmMessageSignatureV2.StructSize];
			Ntlm.SealV2(
				new MessageSealParams
				{
					bufs = new MessageSecBufferList
					{
						buf1 = new MessageSecBuffer(buffer, MessageSecBufferOptions.Privacy)
					},
					macBuffer = sigbuf
				},
				ref sealingKey,
				new Buffer128(TestValues.SigningKeyC2S_V2),
				0,
				true
				);

			//Ntlm.MacV2(
			//	TestValues.Plaintext,
			//	new Buffer128(TestValues.SigningKeyC2S_V2),
			//	ref sealingKey,
			//	true,
			//	0,
			//	out NtlmMessageSignatureV2 sig);

			CollectionAssert.AreEqual(TestValues.PlaintextSealedV2, buffer);
			CollectionAssert.AreEqual(TestValues.PlaintextMacV2, sigbuf);
		}

		[TestMethod]
		public void TestMessageSecurity_V1()
		{
			var cred = new NtlmHashCredential(
				TestValues.UserName,
				TestValues.Domain,
				new Buffer128(TestOutputValues.Lmowf1),
				new Buffer128(TestOutputValues.Ntowf1)
				);
			NtlmClientContext context = new NtlmClientContext(cred);
			var resp = Ntlm.ComputeResponseV1(
				TestValues.ChallengeFlagsV1,
				cred,
				TestValues.ClientChallenge,
				TestInputValues.ServerChallenge
				);

			NtlmAuthInfo authInfo = new NtlmAuthInfo
			{
				negotiateFlags = TestValues.AuthFlags,
				version = TestValues.Version,
				workstationName = TestValues.Workstation,
				userName = TestValues.UserName,
				userDomain = TestValues.Domain,
				resp = resp,
				kxkey = new Buffer128(TestOutputValues.SessionBaseKey_V1),
				exportedSessionKey = new Buffer128(TestInputValues.RandomSessionKey)
			};
			NtlmAuthContextState state = new NtlmAuthContextState
			{

			};
			context.HandleAuthResult(NtlmClientContext.BuildAuthMessage(
				ref authInfo,
				ref state
				));

			byte[] seal = (byte[])TestValues.Plaintext.Clone();
			byte[] sig = new byte[16];
			context.SealMessage(new MessageSecBuffer(seal, MessageSecBufferOptions.Privacy), sig);
			CollectionAssert.AreEqual(TestValues.PlaintextSealedV1, seal);
			CollectionAssert.AreEqual(TestValues.PlaintextMacV1, sig);
		}

		[TestMethod]
		public void TestMessageSecurity_V1Ext()
		{
			var cred = new NtlmHashCredential(
				TestValues.UserName,
				TestValues.Domain,
				new Buffer128(TestOutputValues.Lmowf1),
				new Buffer128(TestOutputValues.Ntowf1)
				);
			NtlmClientContext context = new NtlmClientContext(cred);
			var resp = Ntlm.ComputeResponseV1(
				TestValues.ChallengeFlags_V1Ext,
				cred,
				TestValues.ClientChallenge,
				TestInputValues.ServerChallenge
				);

			NtlmAuthInfo authInfo = new NtlmAuthInfo
			{
				negotiateFlags = TestValues.AuthFlags,
				version = TestValues.Version,
				workstationName = TestValues.Workstation,
				userName = TestValues.UserName,
				userDomain = TestValues.Domain,
				resp = resp,
				kxkey = new Buffer128(TestValues.Kxkey_Extended),
				exportedSessionKey = new Buffer128(TestValues.Kxkey_Extended)
			};
			NtlmAuthContextState state = new NtlmAuthContextState
			{

			};
			context.HandleAuthResult(NtlmClientContext.BuildAuthMessage(
				ref authInfo,
				ref state
				));

			byte[] seal = (byte[])TestValues.Plaintext.Clone();
			byte[] sig = new byte[16];
			context.SealMessage(new MessageSecBuffer(seal, MessageSecBufferOptions.Privacy), sig);
			CollectionAssert.AreEqual(TestValues.PlaintextSealedV1_Ext, seal);
			CollectionAssert.AreEqual(TestValues.PlaintextMacV1_Ext, sig);
		}

		[TestMethod]
		public void TestMessageSecurity_V2()
		{
			var cred = new NtlmHashCredential(
				TestValues.UserName,
				TestValues.Domain,
				new Buffer128(TestValues.ResponseKeyNT_V2),
				new Buffer128(TestValues.ResponseKeyNT_V2)
				);
			NtlmClientContext context = new NtlmClientContext(cred);
			NtlmAuthContextState state = new NtlmAuthContextState
			{
				clientTime = TestValues.Time,
				challengeFromClient = TestValues.ClientChallenge,
				serverChallenge = TestInputValues.ServerChallenge
			};
			var resp = Ntlm.ComputeResponseV2(
				ref state,
				cred.GetResponseKeyNTv2(),
				TestValues.TargetInfo.ToBytes().Span
				);

			NtlmAuthInfo authInfo = new NtlmAuthInfo
			{
				negotiateFlags = TestValues.AuthFlags,
				version = TestValues.Version,
				workstationName = TestValues.Workstation,
				userName = TestValues.UserName,
				userDomain = TestValues.Domain,
				resp = resp,
				kxkey = new Buffer128(TestValues.Kxkey_Extended),
				exportedSessionKey = new Buffer128(TestInputValues.RandomSessionKey)
			};
			context.HandleAuthResult(NtlmClientContext.BuildAuthMessage(
				ref authInfo,
				ref state
				));

			byte[] seal = (byte[])TestValues.Plaintext.Clone();
			byte[] sig = new byte[16];
			context.SealMessage(new MessageSecBuffer(seal, MessageSecBufferOptions.Privacy), sig);
			CollectionAssert.AreEqual(TestValues.PlaintextSealedV2, seal);
			CollectionAssert.AreEqual(TestValues.PlaintextMacV2, sig);
		}
	}
}
