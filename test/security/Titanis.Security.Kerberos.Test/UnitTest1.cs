using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using Titanis.Asn1.Serialization;
using Titanis.Reflection;
using Titanis.Security.Kerberos.Asn1.KerberosV5Spec2;

namespace Titanis.Security.Kerberos.Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestEncProfileRoundtrip()
		{
			EncProfile_Aes256CtsHmacSha1_96 encProfile = new EncProfile_Aes256CtsHmacSha1_96();
			byte[] sessionKey = new byte[]
			{
				0,1,2,3,4,5,6,7,
				0,1,2,3,4,5,6,7,
				0,1,2,3,4,5,6,7,
				0,1,2,3,4,5,6,7,
			};
			byte[] plaintext = new byte[]
			{
				0,1,2,3,4,5,6,7,8,
				0,1,2,3,4,5,6,7,8,
				0,1,2,3,4,5,6,7,8,
				0,1,2,3,4,5,6,7,8,
			};

			var cipher = encProfile.Encrypt(
				sessionKey,
				KeyUsage.TgsreqPatgsreqPadataApreqAuthChecksum_TgsSessionKey_IncludesAuthSubkey,
				plaintext);

			var result = encProfile.Decrypt(
				sessionKey,
				KeyUsage.TgsreqPatgsreqPadataApreqAuthChecksum_TgsSessionKey_IncludesAuthSubkey,
				cipher.Span);

		}

		//[TestMethod]
		public void TestLive_Asreq_NoPreauth()
		{
			KerberosClient client = new KerberosClient(TestData_.locator);
			client.GetTicketAsync("cifs", "DC1", TestData_.Realm, TestData_.Cred_NoPreauth, CancellationToken.None).Wait();
		}

		//[TestMethod]
		public void TestLive_Asreq_WithPreauth()
		{
			KerberosClient client = new KerberosClient(TestData_.locator);
			client.GetTicketAsync("cifs", "DC1", TestData_.Realm, TestData_.Cred_TestUser, CancellationToken.None).Wait();
		}



		[TestMethod]
		public void TestRoundtrip_Asreq()
		{
			var req = TestReq<Kdc_req_choice>("asreq-testuser");
			KdcOptions options = (KdcOptions)req.asreq.req_body.kdc_options.ToUInt32();
		}
		[TestMethod]
		public void TestRoundtrip_Asrep()
		{
			TestReq<Kdc_rep_choice>("asrep-testuser");
		}

		[TestMethod]
		public void TestRoundtrip_Tgsreq()
		{
			TestReq<Kdc_req_choice>("tgsreq-testuser-cifs-dc1");
		}

		[TestMethod]
		public void TestRoundtrip_Tgsrep()
		{
			TestReq<Kdc_rep_choice>("tgsrep-testuser-cifs-dc1");
		}

		private static T TestReq<T>(string name)
			where T : IAsn1DerEncodableTlv, new()
		{
			byte[] asreqBytes = Assembly.GetExecutingAssembly().LoadResourceData("Titanis.Security.Kerberos.Test.TestData." + name + ".bin");
			asreqBytes = asreqBytes.Slice(4).ToArray();
			var req = Asn1DerDecoder.Decode<T>(new Memory<byte>(asreqBytes));

			byte[] encoded = Asn1DerEncoder.EncodeTlv(req).ToArray();

			CollectionAssert.AreEqual(asreqBytes, encoded);
			return req;
		}
	}
}
