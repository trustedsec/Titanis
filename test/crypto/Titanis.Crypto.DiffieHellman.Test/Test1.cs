namespace Titanis.Crypto.DiffieHellman.Test;

[TestClass]
public sealed class Test1
{
	[TestMethod]
	public void TestMethod1()
	{
		var q = (ModpGroups.Group14.P - 1) / 2;
		var str = q.ToString("X");
	}
}
