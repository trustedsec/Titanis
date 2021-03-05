using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace Titanis.Crypto.Test
{
	[TestClass]
	public class Sha1Test
	{
		[TestMethod]
		public void TestHashBuffer_Initialize()
		{
			Sha1State buf = new Sha1State();
			buf.Initialize();

			Assert.AreEqual(Sha1State.InitialWord0, buf.a);
			Assert.AreEqual(Sha1State.InitialWord1, buf.b);
			Assert.AreEqual(Sha1State.InitialWord2, buf.c);
			Assert.AreEqual(Sha1State.InitialWord3, buf.d);
		}

		private static void TestHash(string source, string expectedHash, int repeatCount)
		{
			byte[] plaintext = Encoding.UTF8.GetBytes(source);
			Sha1Context ctx = new Sha1Context();
			ctx.Initialize();
			for (int i = 0; i < repeatCount; i++)
			{
				ctx.HashData(plaintext);
			}
			byte[] hash = new byte[ctx.DigestSizeBytes];
			ctx.HashFinal(hash);
			string hashstr = hash.ToHexString();

			Assert.AreEqual(expectedHash, hashstr);
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
			var actual = SlimHashAlgorithm.ComputeHmac<Sha1Context>(key, plaintext);
			string hashstr = actual.ToHexString();

			Assert.AreEqual(expected, hashstr);
		}

		[TestMethod]
		public void TestSha1_TestVectors()
		{
			TestHash("abc", "a9993e364706816aba3e25717850c26c9cd0d89d", 1);
			TestHash("abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq", "84983e441c3bd26ebaae4aa1f95129e5e54670f1", 1);
			TestHash("0123456701234567012345670123456701234567012345670123456701234567", "dea356a2cddd90c7a7ecedc5ebb563934f460452", 10);
			TestHash("a", "34aa973cd4c4daa4f61eeb2bdbad27316534016f", 1000000);
		}

		[TestMethod]
		public void TestHmacSha1_TestVector1()
		{
			TestHmac("0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b", "Hi There", "b617318655057264e28bc0b6fb378c8ef146be00");
			TestHmac(Encoding.UTF8.GetBytes("Jefe"), "what do ya want for nothing?", "effcdf6ae5eb2fa2d27416d5f184df9c259a7c79");
			TestHmac("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", MakeArray(0xdd, 50), "125d7342b9ac11cd91a39af48aa17b4f63f175d3");
			TestHmac("0102030405060708090a0b0c0d0e0f10111213141516171819", MakeArray(0xCD, 50), "4c9007f4026250c6bc8414f9bf50c86c2d7235da");
			TestHmac("0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c", "Test With Truncation", "4c1a03424b55e07fe7f27be1d58bb9324a9a5a04");
			TestHmac(MakeArray(0xaa, 80), "Test Using Larger Than Block-Size Key - Hash Key First", "aa4ae5e15272d00e95705637ce8a3b55ed402112");
			TestHmac(MakeArray(0xaa, 80), "Test Using Larger Than Block-Size Key and Larger Than One Block-Size Data", "e8e99d0f45237d786d6bbaa7965c7808bbff1a91");
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
