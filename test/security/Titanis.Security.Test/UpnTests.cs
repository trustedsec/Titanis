using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Test;

[TestClass]
public class UpnTests
{
	[TestMethod]
	[DataRow("username", "username", null, "username")]
	[DataRow("domain\\username", "username", "domain", "username")]
	[DataRow("username@domain", "username", "domain", "username")]
	[DataRow("username@domain.root", "username", "domain.root", "username@domain.root")]
	public void TestParseUpn(string text, string userName, string realm, string wireName)
	{
		var upn = UserPrincipalName.Parse(text);
		Assert.AreEqual(userName, upn.UserName);
		Assert.AreEqual(realm, upn.Realm);
		Assert.AreEqual(wireName, upn.WireName);
	}
}
