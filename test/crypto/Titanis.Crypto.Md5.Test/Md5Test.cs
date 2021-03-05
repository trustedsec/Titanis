using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Titanis.Crypto.Test
{
	[TestClass]
	public class Md5Test
	{
		[TestMethod]
		public void TestMd4Buffer_Initialize()
		{
			Md5State buf = new Md5State();
			buf.Initialize();

			Assert.AreEqual(Md5State.InitialWord0, buf.a);
			Assert.AreEqual(Md5State.InitialWord1, buf.b);
			Assert.AreEqual(Md5State.InitialWord2, buf.c);
			Assert.AreEqual(Md5State.InitialWord3, buf.d);
		}

		private static void TestHash(string sourcer, string expectedHash)
		{
			byte[] plaintext = Encoding.UTF8.GetBytes(sourcer);
			byte[] hash = SlimHashAlgorithm.ComputeHash<Md5Context>(plaintext);
			string hashstr = hash.ToHexString();

			Assert.AreEqual(expectedHash, hashstr);
		}

		[TestMethod]
		public void TestMd4_Empty()
		{
			TestHash("", "d41d8cd98f00b204e9800998ecf8427e");
			TestHash("a", "0cc175b9c0f1b6a831c399e269772661");
			TestHash("abc", "900150983cd24fb0d6963f7d28e17f72");
			TestHash("message digest", "f96b697d7cb7938d525a2f31aaf161d0");
			TestHash("abcdefghijklmnopqrstuvwxyz", "c3fcd3d76192e4007dfb496cca67e13b");
			TestHash("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", "d174ab98d277d9f5a5611c2c9f419d9f");
			TestHash("12345678901234567890123456789012345678901234567890123456789012345678901234567890", "57edf4a22be3c955ac49da2e2107b67a");
		}


		private static void TestHmac(string keystr, string input, string expected)
		{
			byte[] key = BinaryHelper.ParseHexString(keystr);
			byte[] plaintext = Encoding.Default.GetBytes(input);
			TestHmac(key, plaintext, expected);
		}

		private static void TestHmac(string keystr, byte[] plaintext, string expected)
		{
			byte[] key = BinaryHelper.ParseHexString(keystr);
			TestHmac(key, plaintext, expected);
		}

		private static void TestHmac(byte[] key, string input, string expected)
		{
			byte[] plaintext = Encoding.Default.GetBytes(input);
			TestHmac(key, plaintext, expected);
		}

		private static void TestHmac(byte[] key, byte[] plaintext, string expected)
		{
			var actual = SlimHashAlgorithm.ComputeHmac<Md5Context>(key, plaintext);
			string hashstr = actual.ToHexString();

			Assert.AreEqual(expected, hashstr);
		}

		[TestMethod]
		public void TestHmacMd5_TestVector1()
		{
			TestHmac("0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b", "Hi There", "9294727a3638bb1c13f48ef8158bfc9d");
			TestHmac(Encoding.UTF8.GetBytes("Jefe"), "what do ya want for nothing?", "750c783e6ab0b503eaa86e310a5db738");
			TestHmac("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", MakeArray(0xdd, 50), "56be34521d144c88dbb8c733f0e8b3f6");
			TestHmac("0102030405060708090a0b0c0d0e0f10111213141516171819", MakeArray(0xCD, 50), "697eaf0aca3a3aea3a75164746ffaa79");
			TestHmac("0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c", "Test With Truncation", "56461ef2342edc00f9bab995690efd4c");
			TestHmac(MakeArray(0xaa, 80), "Test Using Larger Than Block-Size Key - Hash Key First", "6b1ab7fe4bd7bf8f0b62e6ce61b9d0cd");
			TestHmac(MakeArray(0xaa, 80), "Test Using Larger Than Block-Size Key and Larger Than One Block-Size Data", "6f630fad67cda0ee1fb1f562db3aa53e");
		}

		private static byte[] MakeArray(byte n, int length)
		{
			byte[] arr = new byte[length];
			for (int i = 0; i < length; i++)
			{
				arr[i] = n;
			}
			return arr;
		}

	}
}
