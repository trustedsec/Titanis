using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Titanis.Crypto;

namespace Titanis.Security.Ntlm.Test
{
	[TestClass]
	public class DesTest
	{

		[TestMethod]
		public void TestEncrypt1()
		{
			ulong input = 0x9474B8E8C73BCA7D;
			input = BinaryPrimitives.ReverseEndianness(input);

			ulong[] expected = new ulong[]
			{
				0x8da744e0c94e5e17,
				0x0cdb25e3ba3c6d79,
				0x4784c4ba5006081f,
				0x1cf1fc126f2ef842,
				0xe4be250042098d13,
				0x7bfc5dc6adb5797c,
				0x1ab3b4d82082fb28,
				0xc1576a14de707097,
				0x739b68cd2e26782a,
				0x2a59f0c464506edb,
				0xa5c39d4251f0a81e,
				0x7239ac9a6107ddb1,
				0x070cac8590241233,
				0x78f87b6e3dfecf61,
				0x95ec2578c2c433f0,
				0x1b1a2ddb4c642438,
			};
			for (int i = 0; i < 16; i++)
			{
				bool decrypt = (0 != (i % 2));
				ulong result = Des.Process(input, input, decrypt);

				Assert.AreEqual(BinaryPrimitives.ReverseEndianness(expected[i]), result);

				input = result;
			}
		}

		[TestMethod]
		public void TestDes()
		{
			DES des = DES.Create();
			des.GenerateKey();
			des.Mode = CipherMode.ECB;

			byte[] plaintext = new byte[8];
			var cryptor = des.CreateEncryptor();
			cryptor.TransformBlock(plaintext, 0, plaintext.Length, plaintext, 0);

			ulong key = BitConverter.ToUInt64(des.Key);
			ulong expected = BitConverter.ToUInt64(plaintext);

			var actual = Des.Encrypt(key, 0);

			Assert.AreEqual(expected, actual);
		}
	}
}
