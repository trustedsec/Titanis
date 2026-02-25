using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Cli.Test;

[TestClass]
public class WildcardTests
{
	[TestMethod]
	[DataRow("1234","1234", true)]
	[DataRow("*234*","12345", true)]
	[DataRow("*234*", "2345", true)]
	[DataRow("*234*", "1234", true)]
	[DataRow("*234*", "234", true)]
	[DataRow("1*45", "15", false)]
	[DataRow("1*5", "1", false)]
	[DataRow("1*45", "1245", true)]
	[DataRow("1*45", "12345", true)]
	public void TestWildcard(string pattern, string input, bool expected)
	{
		WildcardPattern pat = new WildcardPattern(pattern);
		bool actual = pat.Matches(input);
		Assert.AreEqual(expected, actual);
	}
}
