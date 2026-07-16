namespace Titanis.Security.Kerberos.Test;

[TestClass]
public class FastTests
{
	[TestMethod]
	[DataRow(EType.Aes128CtsHmacSha1_96, "97df97e4b798b29eb31ed7280287a92a", DisplayName = "AES 128")]
	[DataRow(EType.Aes256CtsHmacSha1_96, "4d6ca4e629785c1f01baf55e2e548566b9617ae3a96868c337cb93b5e72b1c7b", DisplayName = "AES 256")]
	public void TestMethod1(EType etype, string expected)
	{
		KerberosClient krb = new KerberosClient();
		var aes128 = krb.GetEncProfile(etype);
		var k1 = aes128.StringToKey("key1", "key1");
		var k2 = aes128.StringToKey("key2", "key2");
		

		var result = KerberosClient.KrbFxCf2(aes128, k1, k2, [(byte)'a'], [(byte)'b']);

		CollectionAssert.AreEqual(BinaryHelper.ParseHexString(expected), result.KeyBytes);
	}
}
