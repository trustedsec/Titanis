using Titanis.Asn1.Serialization;
using Titanis.IO;

namespace Titanis.Asn1.Test
{
	[TestClass]
	public class UnitTest1
	{
		private static void TestDecode<T>(
			byte[] data,
			Func<Asn1DerDecoder, T> decoderFunc,
			T expected
			)
		{
			Asn1DerDecoder decoder = new Asn1DerDecoder(new ByteMemoryReader(data));
			var actual = decoderFunc(decoder);
			Assert.AreEqual(expected, actual);
		}

		private static void TestDecodeTuple<T>(
			byte[] data,
			Asn1Tag tag,
			Func<Asn1DerDecoder, T> decoderFunc,
			T expected
			)
		{
			Asn1DerDecoder decoder = new Asn1DerDecoder(new ByteMemoryReader(data));
			var frame = decoder.DecodeTlvStart(tag);
			var actual = decoderFunc(decoder);
			decoder.CloseTlv(frame);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestDecodeTag_Simple()
		{
			byte[] data = new byte[]
			{
				0x01
			};
			TestDecode(data, r => r.DecodeTag(), Asn1PredefTag.Boolean);
		}

		[TestMethod]
		public void TestDecodeTag_Big1()
		{
			byte[] data = new byte[]
			{
				0x1F, 0x01
			};
			TestDecode(data, r => r.DecodeTag(), Asn1PredefTag.Boolean);
		}

		[TestMethod]
		public void TestDecodeTag_Big2()
		{
			byte[] data = new byte[]
			{
				0x3F, 0x01
			};
			TestDecode(data, r => r.DecodeTag(), new Asn1Tag(Asn1PredefTag.Boolean, Asn1TagFlags.Constructed));
		}

		[TestMethod]
		public void TestDecodeTag_Big3()
		{
			byte[] data = new byte[]
			{
				0x3F, 0x81, 0x02
			};
			TestDecode(data, r => r.DecodeTag(), new Asn1Tag((Asn1PredefTag)0b1_0000010, Asn1TagFlags.Constructed));
		}

		[TestMethod]
		public void TestDecodeLength_Small()
		{
			byte[] data = new byte[]
			{
				0x01
			};
			TestDecode(data, r => r.DecodeLength(), 1);
		}

		[TestMethod]
		public void TestDecodeLength_Indef()
		{
			byte[] data = new byte[]
			{
				0x80
			};
			TestDecode(data, r => r.DecodeLength(), -1);
		}

		[TestMethod]
		public void TestDecodeLength_Length2()
		{
			byte[] data = new byte[]
			{
				0x82, 0xAA, 0x55
			};
			TestDecode(data, r => r.DecodeLength(), 0xAA55);
		}

		[TestMethod]
		public void TestTlv_BoolFalse()
		{
			byte[] data = new byte[]
			{
				0x01,0x01, 0x00
			};
			TestDecodeTuple(data, Asn1PredefTag.Boolean, r => r.DecodeBool(), false);
		}

		[TestMethod]
		public void TestTlv_BoolTrue()
		{
			byte[] data = new byte[]
			{
				0x01,0x01, 0xFF
			};
			TestDecodeTuple(data, Asn1PredefTag.Boolean, r => r.DecodeBool(), true);
		}

		[TestMethod]
		public void TestTlv_Byte1()
		{
			byte[] data = new byte[]
			{
				0x02, 0x01, 0x01
			};
			TestDecodeTuple(data, Asn1PredefTag.Integer, r => r.DecodeByte(), 1);
		}

		[TestMethod]
		public void TestTlv_Byte1_Large()
		{
			byte[] data = new byte[]
			{
				0x02, 0x02, 0x00, 0x82
			};
			TestDecodeTuple(data, Asn1PredefTag.Integer, r => r.DecodeByte(), 0x82);
		}

		[TestMethod]
		[ExpectedException(typeof(OverflowException))]
		public void TestTlv_Byte1_TooLarge()
		{
			byte[] data = new byte[]
			{
				0x02, 0x02, 0x01, 0x02
			};
			TestDecodeTuple(data, Asn1PredefTag.Integer, r => r.DecodeByte(), 0x00);
		}

		[TestMethod]
		public void TestTlv_SByte1()
		{
			byte[] data = new byte[]
			{
				0x02, 0x01, 0xFF
			};
			TestDecodeTuple(data, Asn1PredefTag.Integer, r => r.DecodeSByte(), -1);
		}

		[TestMethod]
		public void TestTlv_Int16_Small()
		{
			byte[] data = new byte[]
			{
				0x02, 0x01, 0x01
			};
			TestDecodeTuple(data, Asn1PredefTag.Integer, r => r.DecodeInt16(), 1);
		}

		[TestMethod]
		public void TestTlv_Int16_Large()
		{
			byte[] data = new byte[]
			{
				0x02, 0x02, 0x12, 0x34
			};
			TestDecodeTuple(data, Asn1PredefTag.Integer, r => r.DecodeInt16(), 0x1234);
		}

		[TestMethod]
		public void TestTlv_Int16_M1()
		{
			byte[] data = new byte[]
			{
				0x02, 0x01, 0xFF
			};
			TestDecodeTuple(data, Asn1PredefTag.Integer, r => r.DecodeInt16(), -1);
		}

		[TestMethod]
		public void TestTlv_Int32_Large()
		{
			byte[] data = new byte[]
			{
				0x02, 0x04, 0x12, 0x34, 0x56, 0x78
			};
			TestDecodeTuple(data, Asn1PredefTag.Integer, r => r.DecodeInt32(), 0x12345678);
		}

		[TestMethod]
		public void TestTlv_Int64_Large()
		{
			byte[] data = new byte[]
			{
				0x02, 0x08, 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0
			};
			TestDecodeTuple(data, Asn1PredefTag.Integer, r => r.DecodeInt64(), 0x123456789ABCDEF0);
		}

		[TestMethod]
		public void TestTlv_Bitstring()
		{
			byte[] data = new byte[]
			{
				0x03, 0x02, 0x02, 0x12
			};
			TestDecodeTuple(data, Asn1PredefTag.BitString, r => r.DecodeBitString(), new Asn1BitString(new byte[] { 0x12 }, 0x02));
		}

		[TestMethod]
		public void TestTlv_Bitstring_Const()
		{
			byte[] data = new byte[]
			{
				0x23, 0x80,
					0x03, 0x02, 0x02, 0x12,
				0x00, 0x00
			};
			TestDecodeTuple(data, Asn1PredefTag.BitString, r => r.DecodeBitString(), new Asn1BitString(new byte[] { 0x12 }, 0x02));
		}

		[TestMethod]
		public void TestTlv_Bitstring_Const2()
		{
			byte[] data = new byte[]
			{
				0x23, 0x80,
					0x03, 0x02, 0x12, 0x34,
					0x03, 0x02, 0x02, 0x56,
				0x00, 0x00
			};
			TestDecodeTuple(data, Asn1PredefTag.BitString, r => r.DecodeBitString(), new Asn1BitString(new byte[] { 0x12, 0x34, 0x56 }, 0x02));
		}

		[TestMethod]
		public void TestTlv_Bitstring_Const2_Nested()
		{
			byte[] data = new byte[]
			{
				0x23, 0x80,
					0x23, 0x80,
						0x03, 0x01, 0x12,
						0x03, 0x01, 0x34,
						0x00, 0x00,
					0x03, 0x02, 0x02, 0x56,
					0x00, 0x00
			};
			TestDecodeTuple(data, Asn1PredefTag.BitString, r => r.DecodeBitString(), new Asn1BitString(new byte[] { 0x12, 0x34, 0x56 }, 0x02));
		}
	}
}