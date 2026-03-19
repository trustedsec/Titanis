using System.Threading.Tasks;
using Titanis.Cli.Kerb.Test;
using Titanis.Cli.LdapTool;

namespace Titanis.Cli.Ldap.Test;

[TestClass]
public sealed class QueryCommandTest : CliCommandTest<ListPartitionsCommand>
{
	[TestMethod]
	[CliTest("lspart")]
	public async Task ListPartitionsTest(Token[] args)
	{
		var results = await TestCommand(args);
	}
}
