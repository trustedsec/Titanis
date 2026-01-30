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
	[DataRow("(attr=value)", nameof(EqualsExpression), "attr", "value", DisplayName = "Simple attr=value")]
	[DataRow(@"(attr=value\29)", nameof(EqualsExpression), "attr", "value)", DisplayName = "Escaped )")]
	[DataRow("(attr~=value)", nameof(ApproxEqualExpression), "attr", "value", DisplayName = "Simple ~=")]
	[DataRow("(attr<=value)", nameof(LessOrEqualExpression), "attr", "value", DisplayName = "Simple <=")]
	[DataRow("(attr>=value)", nameof(GreaterOrEqualExpression), "attr", "value", DisplayName = "Simple >=")]
	[DataRow("(attr&=value)", nameof(ExtensibleMatchExpression), "attr", "value", DisplayName = "&=")]
	[DataRow("(attr|=value)", nameof(ExtensibleMatchExpression), "attr", "value", DisplayName = "|=")]
	[DataRow("(attr*=value)", nameof(ExtensibleMatchExpression), "attr", "value", DisplayName = "*=")]
	[DataRow("((attr=value))", nameof(EqualsExpression), "attr", "value", DisplayName = "Nested ()")]
	[DataRow("((attr=*))", nameof(PresentExpression), "attr", null, DisplayName = "Attr. present")]
	[DataRow("(&(attr=value)(attr=value))", nameof(AndExpression), null, null, DisplayName = "Simple &")]
	[DataRow("(|(attr=value)(attr=value))", nameof(OrExpression), null, null, DisplayName = "Simple |")]
	public void SimpleClauseTests(string text, string expectedType, string expectedAttr, string expectedValue)
	{
		var filter = FilterExpression.Parse(text);
		Assert.AreEqual(expectedType, filter.RootClause.GetType().Name);
		if (filter.RootClause is AssertionExpression assert)
		{
			Assert.AreEqual(expectedAttr, assert.AttributeDescription);
			Assert.AreEqual(expectedValue, ((LiteralAssertionValue)assert.AssertionValue).LiteralValue);
		}
	}
}
