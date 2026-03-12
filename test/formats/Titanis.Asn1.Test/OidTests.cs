using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Asn1.Test;

[TestClass]
public class OidTests
{
	[TestMethod]
	public void EmptyOid()
	{
		var empty = new Asn1Oid();
		Assert.AreEqual(string.Empty, empty.ToString());
		var arcs = empty.ToArray();
		CollectionAssert.AreEqual(new uint[0], arcs);

		var empty2 = new Asn1Oid(string.Empty);
		Assert.AreEqual(empty.GetHashCode(), empty2.GetHashCode());
	}

	[TestMethod]
	public void MyTestMethod()
	{

	}

	[TestMethod]
	public void EqualityTest()
	{
		var x = new Asn1Oid("1.2.3");
		var y = new Asn1Oid([1, 2, 3]);
		var z = new Asn1Oid();

		Assert.AreEqual(x, y);
		Assert.AreEqual(x.ToString(), y.ToString());
		Assert.AreNotEqual(x, z);
		Assert.AreNotEqual(z, x);
		Assert.AreEqual(x.GetHashCode(), y.GetHashCode());
		Assert.IsTrue(object.Equals(x, y));

		Assert.IsTrue(x == y);
		Assert.IsTrue(y == x.Text);
		Assert.IsTrue(y.Text == x);

		Assert.IsTrue(x == y);
		Assert.IsTrue(y != z);

		List<Asn1Oid> list = [x];
		Assert.Contains(y, list);
		CollectionAssert.AreEqual(new uint[] { 1, 2, 3 }, x.ToArray());
		CollectionAssert.AreEqual(new uint[] { 1, 2, 3 }, y.ToArray());
	}
}
