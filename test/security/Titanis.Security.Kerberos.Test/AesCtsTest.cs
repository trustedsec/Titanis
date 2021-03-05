using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Titanis.Security.Kerberos.Test
{
	[TestClass]
	public class AesCtsTest
	{
		internal static readonly byte[] Aes128TestKey = new byte[] {
			0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
			0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
		};
		internal static readonly byte[] Aes256TestKey = new byte[] {
			0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
			0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,

			0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
			0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
		};

		private static readonly byte[] TestConfounder = new byte[] {
			0x0F, 0x0E, 0x0D, 0x0C, 0x0B, 0x0A, 0x09, 0x08,
			0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x00
		};

		internal static void TestAesCbcCts(byte[] input, int blockSizeBits)
		{
			byte[] confounder = (byte[])TestConfounder.Clone();

			byte[] actual = (byte[])input.Clone();
			TestEncrypt(Aes128TestKey, confounder, actual, blockSizeBits);

			TestDecrypt(Aes128TestKey, confounder, actual, blockSizeBits);
			CollectionAssert.AreEqual(TestConfounder, confounder);
			CollectionAssert.AreEqual(input, actual);
		}
		internal static void TestEncrypt(byte[] key, Span<byte> confounder, Span<byte> input, int blockSizeBits)
		{
			Aes aes = Aes.Create();
			aes.BlockSize = blockSizeBits;
			aes.Padding = PaddingMode.None;
			aes.Mode = CipherMode.CBC;
			aes.Key = key;
			aes.IV = new byte[blockSizeBits / 8];

			Span<byte> cbcState = stackalloc byte[16];

			var encHandler = new EncryptCtsHandler(aes, cbcState);
			BasicEncProfile.EncryptCts(
				confounder,
				SecBufferList.Create(SecBuffer.PrivacyWithIntegrity(input)),
				ref encHandler
				);
		}

		internal static void TestDecrypt(byte[] key, Span<byte> confounder, Span<byte> input, int blockSizeBits)
		{
			Aes aes = Aes.Create();
			aes.BlockSize = blockSizeBits;
			aes.Padding = PaddingMode.None;
			aes.Key = key;
			aes.IV = new byte[blockSizeBits / 8];
			aes.Mode = CipherMode.ECB;

			var decHandler = new DecryptCtsHandler(aes);
			BasicEncProfile.DecryptCts(
				confounder,
				SecBufferList.Create(SecBuffer.PrivacyWithIntegrity(input)),
				ref decHandler
				);
		}

		[TestMethod("AES128 CTS with 1 byte")]
		public void TestAesCts_1()
		{
			byte[] input = new byte[]
			{
				0xFF
			};

			TestAesCbcCts(input, 128);
		}

		[TestMethod("AES128 CTS with 16 bytes")]
		public void TestAesCts_2()
		{
			byte[] input = new byte[]
			{
				0xFF, 0xEE, 0xDD, 0xCC, 0xBB, 0xAA, 0x99, 0x88,
				0x77, 0x66, 0x55, 0x44, 0x33, 0x22, 0x11, 0x00,
			};

			TestAesCbcCts(input, 128);
		}

		[TestMethod("AES128 CTS with 17 bytes")]
		public void TestAesCts_3()
		{
			byte[] input = new byte[]
			{
				0xFF, 0xEE, 0xDD, 0xCC, 0xBB, 0xAA, 0x99, 0x88,
				0x77, 0x66, 0x55, 0x44, 0x33, 0x22, 0x11, 0x00,

				0xFE,
			};

			TestAesCbcCts(input, 128);
		}

		[TestMethod("AES128 CTS with 32 bytes")]
		public void TestAesCts_4()
		{
			byte[] input = new byte[]
			{
				0xFF, 0xEE, 0xDD, 0xCC, 0xBB, 0xAA, 0x99, 0x88,
				0x77, 0x66, 0x55, 0x44, 0x33, 0x22, 0x11, 0x00,

				0xFE, 0xED, 0xDC, 0xCB, 0xBA, 0xA9, 0x98, 0x87,
				0x76, 0x65, 0x54, 0x43, 0x32, 0x21, 0x10, 0x0F,
			};

			TestAesCbcCts(input, 128);
		}

		[TestMethod("AES128 CTS with 33 bytes")]
		public void TestAesCts_5()
		{
			byte[] input = new byte[]
			{
				0xFF, 0xEE, 0xDD, 0xCC, 0xBB, 0xAA, 0x99, 0x88,
				0x77, 0x66, 0x55, 0x44, 0x33, 0x22, 0x11, 0x00,

				0xFE, 0xED, 0xDC, 0xCB, 0xBA, 0xA9, 0x98, 0x87,
				0x76, 0x65, 0x54, 0x43, 0x32, 0x21, 0x10, 0x0F,

				0x01,
			};

			TestAesCbcCts(input, 128);
		}



	}
}
