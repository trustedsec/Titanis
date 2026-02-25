using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Core.Test;

[TestClass]
public class StringHelperTests
{
	[TestMethod]
	[DataRow(@"xxx\x44xxx", "xxx\x44xxx")]
	[DataRow(@"xxx\uAA55xxx", "xxx\uAA55xxx")]
	[DataRow(@"xxx\a\b\e\f\n\r\t\v\\\""\'\?xxx", "xxx\a\b\x1B\f\n\r\t\v\\\"\'?xxx")]
	// TODO: Test \U
	public void MyTestMethod(string escaped, string expected)
	{
		string actual = escaped.UnescapeCStyle();
		Assert.AreEqual(expected, actual);
	}
}
