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
	[DataRow("<SID=S-1-5-32-544>;CN=sid", 1, "CN", 1, "sid", DisplayName = "Extended DN with text SID")]
	[DataRow("<SID=01020000000000052000000020020000>;CN=sid", 1, "CN", 1, "sid", DisplayName = "Extended DN with binary SID")]
	[DataRow("<GUID=31b7eee1-e24f-664e-853a-3e1d37288f58>;CN=guid", 1, "CN", 1, "guid", DisplayName = "Extended DN with text GUID")]
	[DataRow("<GUID=31b7eee1e24f664e853a3e1d37288f58>;CN=guid", 1, "CN", 1, "guid", DisplayName = "Extended DN with binary GUID")]
	[DataRow("<WKGUID=31b7eee1-e24f-664e-853a-3e1d37288f58,CN=extDN>;CN=wkguid", 1, "CN", 1, "wkguid", DisplayName = "Extended DN with text WKGUID")]
	[DataRow("<TTL=42,CN=extDN>,CN=ttl", 1, "CN", 1, "ttl", DisplayName = "Extended DN with TTL")]
	[DataRow("<TTL=42>,CN=extDN", 1, "CN", 1, "extDN", DisplayName = "Extended DN with TTL 2")]
	[DataRow("<TTL=42,CN=Mark S.,OU=MDR,OU=Severed Floor,OU=Kier\\, PE,DC=lumon,DC=ind>;CN=ttl", 1, "CN", 1, "ttl", DisplayName = "Extended DN with TTL 2")]
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
