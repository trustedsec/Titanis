using Titanis.Cli.Kerb.Test;

namespace Titanis.Cli.LsaTool.Test;

[TestClass]
public sealed class AddprivTests : CliCommandTest<AddPrivCommand>
{
	[TestMethod]
	[CliTest("milchickNtlm_ByName", "milchickNtlm_BySid")]
	public async Task AddPrivTest(Token[] args)
	{
		var results = await TestCommand(args);
	}
}
