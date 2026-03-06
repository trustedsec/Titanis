namespace Titanis.Ldap.Test;

[TestClass]
public sealed class LdapTests
{
	[TestMethod]
	[DataRow("(1.2.3)", "1.2.3", DisplayName = "OID only")]
	[DataRow("(1.2.3 NAME 'name')", "1.2.3", DisplayName = "OID and Name")]
	[DataRow("(1.2.3 NAME ('name1' 'name2'))", "1.2.3", DisplayName = "OID and Multi-name")]
	[DataRow("(1.2.3 NAME 'name1' DESC 'desc with \\27 and \\5C and \\5c')", "1.2.3", DisplayName = "OID, Name, Description")]
	[DataRow("(1.2.3 OBSOLETE)", "1.2.3", DisplayName = "OID, Obsolete")]
	[DataRow("(1.2.3 SUP 1)", "1.2.3", DisplayName = "OID, supertype OID")]
	[DataRow("(1.2.3 SUP 1.1)", "1.2.3", DisplayName = "OID, supertype OID2")]
	[DataRow("(1.2.3 SUP name)", "1.2.3", DisplayName = "OID, supertype OID-name")]
	[DataRow("(1.2.3 EQUALITY 1)", "1.2.3", DisplayName = "OID, equality OID")]
	[DataRow("(1.2.3 EQUALITY 1.1)", "1.2.3", DisplayName = "OID, equality OID2")]
	[DataRow("(1.2.3 EQUALITY name)", "1.2.3", DisplayName = "OID, equality OID-name")]
	[DataRow("(1.2.3 ORDERING 1)", "1.2.3", DisplayName = "OID, ORDERING OID")]
	[DataRow("(1.2.3 ORDERING 1.1)", "1.2.3", DisplayName = "OID, ORDERING OID2")]
	[DataRow("(1.2.3 ORDERING name)", "1.2.3", DisplayName = "OID, ORDERING OID-name")]
	[DataRow("(1.2.3 SUBSTR 1)", "1.2.3", DisplayName = "OID, SUBSTR OID")]
	[DataRow("(1.2.3 SUBSTR 1.1)", "1.2.3", DisplayName = "OID, SUBSTR OID2")]
	[DataRow("(1.2.3 SUBSTR name)", "1.2.3", DisplayName = "OID, SUBSTR OID-name")]
	[DataRow("(1.2.3 SYNTAX 1.1)", "1.2.3", DisplayName = "OID, Syntax")]
	[DataRow("(1.2.3 SYNTAX 1.1{32})", "1.2.3", DisplayName = "OID, Syntax, maxlen")]
	[DataRow("(1.2.3 SINGLE-VALUE)", "1.2.3", DisplayName = "OID, single-value")]
	[DataRow("(1.2.3 COLLECTIVE)", "1.2.3", DisplayName = "OID, collective")]
	[DataRow("(1.2.3 NO-USER-MODIFICATION)", "1.2.3", DisplayName = "OID, not-user-modifiable")]
	public void ParseAttrDescr(string str, string expectedOid)
	{
		var descr = AttributeTypeDescription.Parse(str);
		Assert.AreEqual(expectedOid, descr.Oid);
	}
}
