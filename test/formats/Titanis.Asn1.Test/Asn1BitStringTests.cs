using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Asn1.Test;
[TestClass]
public class Asn1BitStringTests
{
	[TestMethod]
	public void ExcessiveUnusedBits()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => new Asn1BitString([], 1));
	}
	[TestMethod]
	public void Empty()
	{
		var bits = new Asn1BitString([], 0);
		Assert.IsTrue(bits.IsEmpty);
		Assert.AreEqual(new Asn1Tag(Asn1PredefTag.BitString), bits.Tag);
	}
	[TestMethod]
	public void InitWithUInt32()
	{
		var bits = new Asn1BitString(0x01234567);
		Assert.AreEqual(0, bits.UnusedBits);
		CollectionAssert.AreEqual(new byte[] { 0x01, 0x23, 0x45, 0x67 }, bits.Octets);

		Assert.IsFalse(bits.IsEmpty);
		Assert.AreEqual(0x01234567U, bits.ToUInt32());
	}
	[TestMethod]
	public void InitWithUInt64()
	{
		var bits = new Asn1BitString(0x00000123456789ABUL);
		Assert.AreEqual(0, bits.UnusedBits);
		CollectionAssert.AreEqual(new byte[] { 0x00, 0x00, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB }, bits.Octets);

		Assert.IsFalse(bits.IsEmpty);
		Assert.Throws<OverflowException>(() => bits.ToUInt32());
		Assert.AreEqual(0x00000123456789ABUL, bits.ToUInt64());
	}
}
