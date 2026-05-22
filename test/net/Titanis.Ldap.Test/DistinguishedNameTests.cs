using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap.Test;

[TestClass]
public class DistinguishedNameTests
{
	[TestMethod]
	[DataRow("", 0, null, 0, null, DisplayName = "Simple CN")]
	[DataRow("CN=", 1, "CN", 1, "", DisplayName = "Empty RDN")]
	[DataRow("CN=name", 1, "CN", 1, "name", DisplayName = "Simple CN")]
	[DataRow(@"CN=\ \ name", 1, "CN", 1, "  name", DisplayName = "RDN with leading spaces")]
	[DataRow(@"CN=name\ \ ", 1, "CN", 1, "name  ", DisplayName = "RDN with trailing spaces")]
	[DataRow(@"CN=na me", 1, "CN", 1, "na me", DisplayName = "RDN with embedded space")]
	[DataRow(@"CN=na\,me", 1, "CN", 1, "na,me", DisplayName = "RDN with embedded comma")]
	[DataRow(@"CN=na\00me", 1, "CN", 1, "na\0me", DisplayName = "RDN with embedded null")]
	[DataRow(@"CN=line\0Abreak", 1, "CN", 1, "line\nbreak", DisplayName = "RDN with line break")]
	[DataRow("CN=name,DC=lumon,DC=ind", 3, "CN", 1, "name", DisplayName = "Multiple RDNs")]
	[DataRow("CN=name1+name2", 1, "CN", 2, "name1", DisplayName = "Multi-valued CN")]
	public void ParseNames(string name, int partCount, string part1Type, int part1ValueCount, string part1Value1)
	{
		LdapDistinguishedName dn = new LdapDistinguishedName(name);

		Assert.AreEqual(partCount, dn.Rdns.Count);
		if (partCount > 0)
		{
			Assert.AreEqual(part1Type, dn.Rdns[0].Type);
			Assert.AreEqual(part1ValueCount, dn.Rdns[0].Values.Length);
			Assert.AreEqual(part1Value1, dn.Rdns[0].Values[0]);
		}

		Assert.AreEqual(name, dn.Text);
	}
}
