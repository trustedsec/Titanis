using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap.FilterExpressions;

namespace Titanis.Ldap.Test;

[TestClass]
public class FilterParserTests
{
	[TestMethod]
	[DataRow("(attr=value)", typeof(EqualsExpression), "attr", "value", DisplayName = "Simple attr=value")]
	[DataRow(@"(attr=value\29)", typeof(EqualsExpression), "attr", "value)", DisplayName = "Escaped )")]
	[DataRow("(attr~=value)", typeof(ApproxEqualExpression), "attr", "value", DisplayName = "Simple ~=")]
	[DataRow("(attr<=value)", typeof(LessOrEqualExpression), "attr", "value", DisplayName = "Simple <=")]
	[DataRow("(attr>=value)", typeof(GreaterOrEqualExpression), "attr", "value", DisplayName = "Simple >=")]
	[DataRow("(attr&=value)", typeof(ExtensibleMatchExpression), "attr", "value", DisplayName = "&=")]
	[DataRow("(attr|=value)", typeof(ExtensibleMatchExpression), "attr", "value", DisplayName = "|=")]
	[DataRow("(attr*=value)", typeof(ExtensibleMatchExpression), "attr", "value", DisplayName = "*=")]
	[DataRow("((attr=value))", typeof(EqualsExpression), "attr", "value", DisplayName = "Nested ()")]
	[DataRow("((attr=*))", typeof(PresentExpression), "attr", null, DisplayName = "Attr. present")]
	[DataRow("(&(attr=value)(attr=value))", typeof(AndExpression), null, null, DisplayName = "Simple &")]
	[DataRow("(|(attr=value)(attr=value))", typeof(OrExpression), null, null, DisplayName = "Simple |")]
	public void SimpleClauseTests(string text, Type expectedType, string expectedAttr, string expectedValue)
	{
		var filter = FilterExpression.Parse(text);
		Assert.IsInstanceOfType(filter, expectedType);
		if (filter is AssertionExpression assert)
		{
			Assert.AreEqual(expectedAttr, assert.AttributeDescription);
			Assert.AreEqual(expectedValue, assert.AssertionValue);
		}
	}
}
