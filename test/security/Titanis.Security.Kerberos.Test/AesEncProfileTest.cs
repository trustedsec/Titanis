using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security.Kerberos.Test
{
	[TestClass]
	public class AesEncProfileTest
	{
		private static void TestAesKey<TEnc>(byte[] expectedKey, int iterations, string password, string saltPhrase)
			where TEnc : EncProfile_AesCtsHmacSha1_96, new()
		{
			var salt = Encoding.UTF8.GetBytes(saltPhrase);
			TestAesKey<TEnc>(expectedKey, iterations, Encoding.UTF8.GetBytes(password), salt);
		}
		private static void TestAesKey<TEnc>(byte[] expectedKey, int iterations, string password, byte[] salt)
			where TEnc : EncProfile_AesCtsHmacSha1_96, new()
		{
			TestAesKey<TEnc>(expectedKey, iterations, Encoding.UTF8.GetBytes(password), salt);
		}
		private static void TestAesKey<TEnc>(byte[] expectedKey, int iterations, byte[] password, string saltPhrase)
			where TEnc : EncProfile_AesCtsHmacSha1_96, new()
		{
			var salt = Encoding.UTF8.GetBytes(saltPhrase);
			TestAesKey<TEnc>(expectedKey, iterations, password, salt);
		}

		private static void TestAesKey<TEnc>(byte[] expectedKey, int iterations, byte[] password, byte[] salt) where TEnc : EncProfile_AesCtsHmacSha1_96, new()
		{
			TEnc prof = new TEnc()
			{
				iterations = iterations
			};
			byte[] actual = prof.StringToKey(password, salt);
			CollectionAssert.AreEqual(expectedKey, actual);
		}

		[TestMethod("RFC 3962 AES 128 Key #1")]
		public void TestAes128Key_1()
		{
			byte[] expected = new byte[]
			{
				0x42, 0x26, 0x3c, 0x6e, 0x89, 0xf4, 0xfc, 0x28, 0xb8, 0xdf, 0x68, 0xee, 0x09, 0x79, 0x9f, 0x15,
			};
			TestAesKey<EncProfile_Aes128CtsHmacSha1_96>(expected, 1, "password", "ATHENA.MIT.EDUraeburn");
		}

		[TestMethod("RFC 3962 AES 256 Key #1")]
		public void TestAes256Key_1()
		{
			byte[] expected = new byte[]
			{
				0xfe, 0x69, 0x7b, 0x52, 0xbc, 0x0d, 0x3c, 0xe1, 0x44, 0x32, 0xba, 0x03, 0x6a, 0x92, 0xe6, 0x5b,
				0xbb, 0x52, 0x28, 0x09, 0x90, 0xa2, 0xfa, 0x27, 0x88, 0x39, 0x98, 0xd7, 0x2a, 0xf3, 0x01, 0x61,
			};
			TestAesKey<EncProfile_Aes256CtsHmacSha1_96>(expected, 1, "password", "ATHENA.MIT.EDUraeburn");
		}

		[TestMethod("RFC 3962 AES 128 Key #2")]
		public void TestAes128Key_2()
		{
			byte[] expected = new byte[]
			{
				0xc6, 0x51, 0xbf, 0x29, 0xe2, 0x30, 0x0a, 0xc2, 0x7f, 0xa4, 0x69, 0xd6, 0x93, 0xbd, 0xda, 0x13,
			};
			TestAesKey<EncProfile_Aes128CtsHmacSha1_96>(expected, 2, "password", "ATHENA.MIT.EDUraeburn");
		}

		[TestMethod("RFC 3962 AES 256 Key #2")]
		public void TestAes256Key_2()
		{
			byte[] expected = new byte[]
			{
				0xa2, 0xe1, 0x6d, 0x16, 0xb3, 0x60, 0x69, 0xc1, 0x35, 0xd5, 0xe9, 0xd2, 0xe2, 0x5f, 0x89, 0x61,
				0x02, 0x68, 0x56, 0x18, 0xb9, 0x59, 0x14, 0xb4, 0x67, 0xc6, 0x76, 0x22, 0x22, 0x58, 0x24, 0xff,
			};
			TestAesKey<EncProfile_Aes256CtsHmacSha1_96>(expected, 2, "password", "ATHENA.MIT.EDUraeburn");
		}

		[TestMethod("RFC 3962 AES 128 Key #3")]
		public void TestAes128Key_3()
		{
			byte[] expected = new byte[]
			{
				0x4c, 0x01, 0xcd, 0x46, 0xd6, 0x32, 0xd0, 0x1e, 0x6d, 0xbe, 0x23, 0x0a, 0x01, 0xed, 0x64, 0x2a,
			};
			TestAesKey<EncProfile_Aes128CtsHmacSha1_96>(expected, 1200, "password", "ATHENA.MIT.EDUraeburn");
		}

		[TestMethod("RFC 3962 AES 256 Key #3")]
		public void TestAes256Key_3()
		{
			byte[] expected = new byte[]
			{
			   0x55, 0xa6, 0xac, 0x74, 0x0a, 0xd1, 0x7b, 0x48, 0x46, 0x94, 0x10, 0x51, 0xe1, 0xe8, 0xb0, 0xa7,
			   0x54, 0x8d, 0x93, 0xb0, 0xab, 0x30, 0xa8, 0xbc, 0x3f, 0xf1, 0x62, 0x80, 0x38, 0x2b, 0x8c, 0x2a,
			};
			TestAesKey<EncProfile_Aes256CtsHmacSha1_96>(expected, 1200, "password", "ATHENA.MIT.EDUraeburn");
		}

		[TestMethod("RFC 3962 AES 128 Key #4")]
		public void TestAes128Key_4()
		{
			byte[] expected = new byte[]
			{
				0xe9, 0xb2, 0x3d, 0x52, 0x27, 0x37, 0x47, 0xdd, 0x5c, 0x35, 0xcb, 0x55, 0xbe, 0x61, 0x9d, 0x8e,
			};
			byte[] salt = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x78, 0x56, 0x34, 0x12, };
			TestAesKey<EncProfile_Aes128CtsHmacSha1_96>(expected, 5, "password", salt);
		}

		[TestMethod("RFC 3962 AES 256 Key #4")]
		public void TestAes256Key_4()
		{
			byte[] expected = new byte[]
			{
				0x97, 0xa4, 0xe7, 0x86, 0xbe, 0x20, 0xd8, 0x1a, 0x38, 0x2d, 0x5e, 0xbc, 0x96, 0xd5, 0x90, 0x9c,
				0xab, 0xcd, 0xad, 0xc8, 0x7c, 0xa4, 0x8f, 0x57, 0x45, 0x04, 0x15, 0x9f, 0x16, 0xc3, 0x6e, 0x31,
			};
			byte[] salt = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x78, 0x56, 0x34, 0x12, };
			TestAesKey<EncProfile_Aes256CtsHmacSha1_96>(expected, 5, "password", salt);
		}

		[TestMethod("RFC 3962 AES 128 Key #5")]
		public void TestAes128Key_5()
		{
			byte[] expected = new byte[]
			{
				0x59, 0xd1, 0xbb, 0x78, 0x9a, 0x82, 0x8b, 0x1a, 0xa5, 0x4e, 0xf9, 0xc2, 0x88, 0x3f, 0x69, 0xed,
			};
			TestAesKey<EncProfile_Aes128CtsHmacSha1_96>(expected, 1200, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", "pass phrase equals block size");
		}

		[TestMethod("RFC 3962 AES 256 Key #5")]
		public void TestAes256Key_5()
		{
			byte[] expected = new byte[]
			{
			   0x89, 0xad, 0xee, 0x36, 0x08, 0xdb, 0x8b, 0xc7, 0x1f, 0x1b, 0xfb, 0xfe, 0x45, 0x94, 0x86, 0xb0,
			   0x56, 0x18, 0xb7, 0x0c, 0xba, 0xe2, 0x20, 0x92, 0x53, 0x4e, 0x56, 0xc5, 0x53, 0xba, 0x4b, 0x34,
			};
			TestAesKey<EncProfile_Aes256CtsHmacSha1_96>(expected, 1200, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", "pass phrase equals block size");
		}

		[TestMethod("RFC 3962 AES 128 Key #6")]
		public void TestAes128Key_6()
		{
			byte[] expected = new byte[]
			{
			   0xcb, 0x80, 0x05, 0xdc, 0x5f, 0x90, 0x17, 0x9a, 0x7f, 0x02, 0x10, 0x4c, 0x00, 0x18, 0x75, 0x1d,
			};
			TestAesKey<EncProfile_Aes128CtsHmacSha1_96>(expected, 1200, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", "pass phrase exceeds block size");
		}

		[TestMethod("RFC 3962 AES 256 Key #6")]
		public void TestAes256Key_6()
		{
			byte[] expected = new byte[]
			{
			   0xd7, 0x8c, 0x5c, 0x9c, 0xb8, 0x72, 0xa8, 0xc9, 0xda, 0xd4, 0x69, 0x7f, 0x0b, 0xb5, 0xb2, 0xd2,
			   0x14, 0x96, 0xc8, 0x2b, 0xeb, 0x2c, 0xae, 0xda, 0x21, 0x12, 0xfc, 0xee, 0xa0, 0x57, 0x40, 0x1b,
			};
			TestAesKey<EncProfile_Aes256CtsHmacSha1_96>(expected, 1200, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", "pass phrase exceeds block size");
		}

		[TestMethod("RFC 3962 AES 128 Key #7")]
		public void TestAes128Key_7()
		{
			byte[] expected = new byte[]
			{
			   0xf1, 0x49, 0xc1, 0xf2, 0xe1, 0x54, 0xa7, 0x34, 0x52, 0xd4, 0x3e, 0x7f, 0xe6, 0x2a, 0x56, 0xe5,
			};
			byte[] password = new byte[] { 0xF0, 0x9D, 0x84, 0x9E };
			TestAesKey<EncProfile_Aes128CtsHmacSha1_96>(expected, 50, password, "EXAMPLE.COMpianist");
		}

		[TestMethod("RFC 3962 AES 256 Key #7")]
		public void TestAes256Key_7()
		{
			byte[] expected = new byte[]
			{
			   0x4b, 0x6d, 0x98, 0x39, 0xf8, 0x44, 0x06, 0xdf, 0x1f, 0x09, 0xcc, 0x16, 0x6d, 0xb4, 0xb8, 0x3c,
			   0x57, 0x18, 0x48, 0xb7, 0x84, 0xa3, 0xd6, 0xbd, 0xc3, 0x46, 0x58, 0x9a, 0x3e, 0x39, 0x3f, 0x9e,
			};
			byte[] password = new byte[] { 0xF0, 0x9D, 0x84, 0x9E };
			TestAesKey<EncProfile_Aes256CtsHmacSha1_96>(expected, 50, password, "EXAMPLE.COMpianist");
		}

		[TestMethod("AES 256 Seal/Unseal roundtrip")]
		public void TestSealRoundtrip()
		{
			EncProfile_Aes256CtsHmacSha1_96 encProf = new EncProfile_Aes256CtsHmacSha1_96();

			var key = AesCtsTest.Aes256TestKey;
			const int SeqNbr = 42;

			var message = new byte[42];
			var trailer = new byte[encProf.SealHeaderSize + encProf.SealTrailerSize];

			MessageSealParams sealParams = new(default, SecBufferList.Create(SecBuffer.PrivacyWithIntegrity(message)), trailer);
			encProf.SealMessage(key, KeyUsage.InitiatorSeal, SeqNbr, WrapFlags.Sealed, sealParams);

			encProf.UnsealMessage(key, KeyUsage.InitiatorSeal, SeqNbr, WrapFlags.Sealed, sealParams);
		}

		[TestMethod("AES 256 Seal/Unseal roundtrip w/ multiple buffers")]
		public void TestSealRoundtrip2()
		{
			EncProfile_Aes256CtsHmacSha1_96 encProf = new EncProfile_Aes256CtsHmacSha1_96();

			var key = AesCtsTest.Aes256TestKey;
			const int SeqNbr = 42;

			void FillArray(byte[] data)
			{
				for (int i = 0; i < data.Length; i++)
				{
					data[i] = (byte)((i << 4) | ((i + 1) & 0x0F));
				}
			}

			var message1 = new byte[24];
			var message2 = new byte[24];
			var message3 = new byte[8];
			FillArray(message1);
			FillArray(message2);
			FillArray(message3);

			var trailer = new byte[encProf.SealHeaderSize + encProf.SealTrailerSize];

			MessageSealParams sealParams = new(default, SecBufferList.Create(
				SecBuffer.Integrity(message1),
				SecBuffer.PrivacyWithIntegrity(message2),
				SecBuffer.Integrity(message3)
				), trailer);
			encProf.SealMessage(key, KeyUsage.InitiatorSeal, SeqNbr, WrapFlags.Sealed | WrapFlags.AcceptorSubkey, sealParams);

			encProf.UnsealMessage(key, KeyUsage.InitiatorSeal, SeqNbr, WrapFlags.Sealed | WrapFlags.AcceptorSubkey, sealParams);
		}

		[TestMethod("AES 256 Encrypt/Decrypt round trip (all zeroes)")]
		public void TestAes256RoundTrip()
		{
			var key = AesCtsTest.Aes256TestKey;
			byte[] data = new byte[257];

			EncProfile_Aes256CtsHmacSha1_96 encProf = new EncProfile_Aes256CtsHmacSha1_96();
			var cipher = encProf.Encrypt(key, KeyUsage.AsreqPaEncTimestamp, data);
			var decrypted = encProf.Decrypt(key, KeyUsage.AsreqPaEncTimestamp, cipher);

			CollectionAssert.AreEqual(data, decrypted.ToArray());
		}

		[TestMethod("AES 128 Encrypt/Decrypt round trip (all zeroes)")]
		public void TestAes128RoundTrip()
		{
			var key = AesCtsTest.Aes128TestKey;
			byte[] data = new byte[257];

			EncProfile_Aes256CtsHmacSha1_96 encProf = new EncProfile_Aes256CtsHmacSha1_96();
			var cipher = encProf.Encrypt(key, KeyUsage.AsreqPaEncTimestamp, data);
			var decrypted = encProf.Decrypt(key, KeyUsage.AsreqPaEncTimestamp, cipher);

			CollectionAssert.AreEqual(data, decrypted.ToArray());
		}
	}
}
