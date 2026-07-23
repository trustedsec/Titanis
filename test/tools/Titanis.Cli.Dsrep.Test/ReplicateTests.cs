using System.Threading.Tasks;
using Titanis.Cli.Kerb.Test;

namespace Titanis.Cli.Dsrep.Test;

[TestClass]
public sealed class ReplicateTests : CliCommandTest<ReplicateObjectsCommand>
{
	[TestMethod]
	[CliTest("milchick_all", "milchick_LdapQuery", "milchick_name_dn")]
	public async Task TestReplicate(Token[] args)
	{
		var output = await TestCommand(args);
	}
}
