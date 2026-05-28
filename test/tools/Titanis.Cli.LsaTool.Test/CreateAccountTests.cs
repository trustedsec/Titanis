using Titanis.Cli.Kerb.Test;

namespace Titanis.Cli.LsaTool.Test;

[TestClass]
public sealed class CreateAccountTests : CliCommandTest<CreateAccountCommand>
{
	[TestMethod]
	[CliTest("milchick")]
	public async Task CreateAccountTest(Token[] args)
	{
		var results = await TestCommand(args);
	}
}
