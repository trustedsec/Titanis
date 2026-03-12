using Titanis.Crypto.DiffieHellman;

namespace Titanis.Crypto.Test;

[TestClass]
public sealed class ModpTests
{
	[TestMethod]
	public void TestMethod1()
	{
		var q = (ModpGroups.Group14.P - 1) / 2;
		var str = q.ToString("X");
	}
}
