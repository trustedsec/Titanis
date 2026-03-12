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
			Asn1DerDecoder decoder = new Asn1DerDecoder(new ByteMemoryReader(data), Asn1DerDecoderOptions.None);
			var actual = decoderFunc(decoder);
			Assert.AreEqual(expected, actual);
		}

		private static void TestDecodeTlv<T>(
			byte[] data,
			Asn1Tag tag,
			Func<Asn1DerDecoder, T> decoderFunc,
			T expected
			)
		{
			Asn1DerDecoder decoder = new Asn1DerDecoder(new ByteMemoryReader(data), Asn1DerDecoderOptions.None);
			var actualTag = decoder.PeekTag();
			var actual = decoderFunc(decoder);
			Assert.AreEqual(expected, actual);
		}
		private static void TestDecodeTaggedValue<T>(
			byte[] data,
			Asn1Tag tag,
			Func<Asn1DerDecoder, T> decoderFunc,
			T expected
			)
		{
			Asn1DerDecoder decoder = new Asn1DerDecoder(new ByteMemoryReader(data), Asn1DerDecoderOptions.None);
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
			TestDecodeTlv(data, Asn1PredefTag.Boolean, r => r.DecodeBoolTlv(), false);
		}

		[TestMethod]
		public void TestTlv_BoolTrue()
		{
			byte[] data = new byte[]
			{
				0x01,0x01, 0xFF
			};
			TestDecodeTlv(data, Asn1PredefTag.Boolean, r => r.DecodeBoolTlv(), true);
		}

		[TestMethod]
		public void TestTlv_Byte1()
		{
			byte[] data = new byte[]
			{
				0x02, 0x01, 0x01
			};
			TestDecodeTlv(data, Asn1PredefTag.Integer, r => r.DecodeIntegerTlvAsByte(), 1);
		}

		[TestMethod]
		public void TestTlv_Byte1_Large()
		{
			byte[] data = new byte[]
			{
				0x02, 0x02, 0x00, 0x82
			};
			TestDecodeTlv(data, Asn1PredefTag.Integer, r => r.DecodeIntegerTlvAsByte(), 0x82);
		}

		[TestMethod]
		public void TestTlv_Byte1_TooLarge()
		{
			byte[] data = new byte[]
			{
				0x02, 0x02, 0x01, 0x02
			};
			Assert.Throws<OverflowException>(() =>
			{
				TestDecodeTlv(data, Asn1PredefTag.Integer, r => r.DecodeIntegerTlvAsByte(), 0x00);
			});
		}

		[TestMethod]
		public void TestTlv_SByte1()
		{
			byte[] data = new byte[]
			{
				0x02, 0x01, 0xFF
			};
			TestDecodeTlv(data, Asn1PredefTag.Integer, r => r.DecodeIntegerTlvAsSByte(), -1);
		}

		[TestMethod]
		public void TestTlv_Int16_Small()
		{
			byte[] data = new byte[]
			{
				0x02, 0x01, 0x01
			};
			TestDecodeTlv(data, Asn1PredefTag.Integer, r => r.DecodeIntegerTlvAsInt16(), 1);
		}

		[TestMethod]
		public void TestTlv_Int16_Large()
		{
			byte[] data = new byte[]
			{
				0x02, 0x02, 0x12, 0x34
			};
			TestDecodeTlv(data, Asn1PredefTag.Integer, r => r.DecodeIntegerTlvAsInt16(), 0x1234);
		}

		[TestMethod]
		public void TestTlv_Int16_M1()
		{
			byte[] data = new byte[]
			{
				0x02, 0x01, 0xFF
			};
			TestDecodeTlv(data, Asn1PredefTag.Integer, r => r.DecodeIntegerTlvAsInt16(), -1);
		}

		[TestMethod]
		public void TestTlv_Int32_Large()
		{
			byte[] data = new byte[]
			{
				0x02, 0x04, 0x12, 0x34, 0x56, 0x78
			};
			TestDecodeTlv(data, Asn1PredefTag.Integer, r => r.DecodeIntegerTlvAsInt32(), 0x12345678);
		}

		[TestMethod]
		public void TestTlv_Int64_Large()
		{
			byte[] data = new byte[]
			{
				0x02, 0x08, 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0
			};
			TestDecodeTlv(data, Asn1PredefTag.Integer, r => r.DecodeIntegerTlvAsInt64(), 0x123456789ABCDEF0);
		}

		[TestMethod]
		public void TestTlv_Bitstring()
		{
			byte[] data = new byte[]
			{
				0x03, 0x02, 0x02, 0x12
			};
			TestDecodeTaggedValue(data, Asn1PredefTag.BitString, r => r.DecodeBitStringValue(), new Asn1BitString(new byte[] { 0x12 }, 0x02));
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
			TestDecodeTaggedValue(data, Asn1PredefTag.BitString, r => r.DecodeBitStringValue(), new Asn1BitString(new byte[] { 0x12 }, 0x02));
		}

		[TestMethod]
		public void TestTlv_Bitstring_Const2()
		{
			byte[] data = new byte[]
			{
				0x23, 0x80,
					0x03, 0x03, 0x00, 0x0A, 0x3B,
					0x03, 0x05, 0x04, 0x5F, 0x29, 0x1C, 0xD0,
				0x00, 0x00
			};
			TestDecodeTlv(data, Asn1PredefTag.BitString, r => r.DecodeBitStringTlv(), new Asn1BitString(new byte[] { 0x0A, 0x3B, 0x5F, 0x29, 0x1C, 0xD0 }, 0x04));
		}

		[TestMethod]
		public void TestTlv_Bitstring_Const2_Nested()
		{
			byte[] data = new byte[]
			{
				0x23, 0x80,
					0x23, 0x80,
						0x03, 0x02, 0x00, 0x12,
						0x03, 0x02, 0x00, 0x34,
						0x00, 0x00,
					0x03, 0x02, 0x02, 0x56,
					0x00, 0x00
			};
			TestDecodeTlv(data, Asn1PredefTag.BitString, r => r.DecodeBitStringTlv(), new Asn1BitString(new byte[] { 0x12, 0x34, 0x56 }, 0x02));
		}
	}
}