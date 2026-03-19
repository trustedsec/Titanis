using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Core.Test;

[TestClass]
public class DurationTests
{
	[TestMethod]
	[DataRow("5ms", 5)]
	[DataRow("5s", 5 * 1000)]
	[DataRow("5m", 5 * 60 * 1000)]
	[DataRow("5h", 5 * 60 * 60 * 1000)]
	[DataRow("5d", 5 * 24 * 60 * 60 * 1000)]
	[DataRow("5d4h3m2s1ms", (((5 * 24 + 4) * 60 + 3) * 60 + 2) * 1000 + 1)]
	public void DurationTest(string text, int expectedMs)
	{
		var duration = Duration.Parse(text);
		Assert.AreEqual(expectedMs, (int)duration.TimeSpan.TotalMilliseconds);
		Assert.AreEqual(text, duration.ToString());
	}
}
