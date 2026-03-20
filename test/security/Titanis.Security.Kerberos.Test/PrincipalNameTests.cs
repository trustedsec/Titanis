using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos.Test;

[TestClass]
public class PrincipalNameTests
{
	[TestMethod]
	[DataRow(PrincipalNameType.Principal, new string[] { "krbtgt", "LUMON.IND" })]
	[DataRow(PrincipalNameType.ServiceInstance, new string[] { "krbtgt", "LUMON.IND" })]
	[DataRow(PrincipalNameType.Enterprise, new string[] { "milchick@LUMON.IND" })]
	public void MyTestMethod(PrincipalNameType nameType, string[] parts)
	{
		var actual = SecurityPrincipalName.Create(nameType, parts);
		Assert.AreEqual(nameType, actual.NameType);
		Assert.AreEqual(parts.Length, actual.NamePartCount);
		CollectionAssert.AreEqual(parts, actual.GetNameParts());
	}
}
