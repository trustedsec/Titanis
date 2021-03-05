using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Reflection;
using Titanis.Security.Ntlm;
using Titanis.Security.Spnego.Asn1.GSS_API;
using Titanis.Security.Spnego.Asn1.SPNEGOASNOneSpec;

namespace Titanis.Security.Spnego.Test
{
	[TestClass]
	public class SpnegoTest
	{
		private static void TestSerialize<T>(T obj, byte[] expected)
		{
			//Asn1Serializer ser = new Asn1Serializer(typeof(T), Asn1DerEncoding.Instance);
			//byte[] actual = ser.Write(obj);
			//CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestReadNegTokenInit()
		{
			string resourceName = "Titanis.Security.Spnego.Test.TestData.gssapi-spnego-NTLM_NEGOTIATE.bin";
			byte[] data = Assembly.GetExecutingAssembly().LoadResourceData(resourceName);

			var decoder = Asn1DerEncoding.CreateDerDecoder(data);

			InitialContextToken token = new InitialContextToken();
			token.DecodeTlv(decoder);

			var spnego = token.Value.innerContextToken.DecodeAs<NegotiationToken>();
			//var token = decoder.DecodeObjTlv<NegTokenInit>();

			//Asn1Serializer ser = new Asn1Serializer(typeof(NegTokenInit), Asn1Encodings.Der);
			//var token = ser.Read<NegTokenInit>(data);
			//var actualData = ser.Write(token);
			//CollectionAssert.AreEqual(data, actualData);
		}

		[TestMethod]
		public void TestReadNegTokenInit_opt()
		{
			string resourceName = "Titanis.Security.Spnego.Test.TestData.gssapi-spnego-NegTokenInit2.bin";
			byte[] data = Assembly.GetExecutingAssembly().LoadResourceData(resourceName);

			var decoder = Asn1DerEncoding.CreateDerDecoder(data);

			InitialContextToken token = new InitialContextToken();
			token.DecodeTlv(decoder);

			var spnego = token.Value.innerContextToken.DecodeAs<NegotiationToken2>();
			//var token = decoder.DecodeObjTlv<NegTokenInit>();

			//Asn1Serializer ser = new Asn1Serializer(typeof(NegTokenInit), Asn1Encodings.Der);
			//var token = ser.Read<NegTokenInit>(data);
			//var actualData = ser.Write(token);
			//CollectionAssert.AreEqual(data, actualData);
		}

		[TestMethod]
		public void TestInitialize_NegTokenInit2()
		{
			string resourceName = "Titanis.Security.Spnego.Test.TestData.gssapi-spnego-NegTokenInit2.bin";
			byte[] data = Assembly.GetExecutingAssembly().LoadResourceData(resourceName);

			SpnegoClientContext context = new SpnegoClientContext();
			NtlmPasswordCredential ntlmCred = new NtlmPasswordCredential("User", "password");
			NtlmClientContext ntlm = new NtlmClientContext(ntlmCred, true);
			context.Contexts.Add(ntlm);
			context.Initialize(data);
		}
	}
}
