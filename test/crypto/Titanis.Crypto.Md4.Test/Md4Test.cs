using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Titanis.Crypto.Test
{
	[TestClass]
	public class Md4Test
	{
		[TestMethod]
		public void TestMd4Buffer_Initialize()
		{
			Md4State buf = new Md4State();
			buf.Initialize();

			Assert.AreEqual(Md4State.InitialWord0, buf.a);
			Assert.AreEqual(Md4State.InitialWord1, buf.b);
			Assert.AreEqual(Md4State.InitialWord2, buf.c);
			Assert.AreEqual(Md4State.InitialWord3, buf.d);
		}

		private static void TestHash(string sourcer, string expectedHash) => TestHash(sourcer, expectedHash, Encoding.UTF8);
		private static void TestHash(string sourcer, string expectedHash, Encoding encoding)
		{
			byte[] plaintext = encoding.GetBytes(sourcer);
			byte[] hash=SlimHashAlgorithm.ComputeHash<Md4Context>(plaintext);
			string hashstr = hash.ToHexString();

			Assert.AreEqual(expectedHash, hashstr);
		}

		[TestMethod]
		public void TestMd4_TestVectors()
		{
			TestHash("", "31d6cfe0d16ae931b73c59d7e0c089c0");
			TestHash("a", "bde52cb31de33e46245e05fbdbd6fb24");
			TestHash("abc", "a448017aaf21d8525fc10ae87aa6729d");
			TestHash("message digest", "d9130a8164549fe818874806e1c7014b");
			TestHash("abcdefghijklmnopqrstuvwxyz", "d79e1c308aa5bbcdeea8ed63df412da9");
			TestHash("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", "043f8582f241db351ce627e153e7f0e4");
			TestHash("12345678901234567890123456789012345678901234567890123456789012345678901234567890", "e33b4ddc9c38f2199c3e7b164fcc0536");
		}
	}
}
