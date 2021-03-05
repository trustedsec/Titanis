using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Titanis.Core.Test
{
	[TestClass]
	public class HexTests
	{
		[TestMethod]
		public void TestParseHexChar_5()
		{
			Assert.AreEqual(5, BinaryHelper.ParseHexChar('5'));
		}

		[TestMethod]
		public void TestParseHexChar_c()
		{
			Assert.AreEqual(12, BinaryHelper.ParseHexChar('c'));
		}

		[TestMethod]
		public void TestParseHexChar_D()
		{
			Assert.AreEqual(13, BinaryHelper.ParseHexChar('D'));
		}

		[TestMethod]
		public void TestParseHexString()
		{
			byte[] expected = new byte[]
			{
				0x01,0x23,0x45,0x67,0x89,0xAB,0XCD,0XEF
			};
			byte[] actual = BinaryHelper.ParseHexString("0123456789ABCDEF");
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestToHexString()
		{
			byte[] source = new byte[]
			{
				0x01,0x23,0x45,0x67,0x89,0xAB,0XCD,0XEF
			};
			string expected = "0123456789abcdef";
			string actual = source.ToHexString();

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestToHexString_Uppercase()
		{
			byte[] source = new byte[]
			{
				0x01,0x23,0x45,0x67,0x89,0xAB,0XCD,0XEF
			};
			string expected = "0123456789ABCDEF";
			string actual = source.ToHexString(HexStringOptions.Uppercase);

			Assert.AreEqual(expected, actual);
		}
	}
}
