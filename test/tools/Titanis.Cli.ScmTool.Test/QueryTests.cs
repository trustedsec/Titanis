using Titanis.Cli.Kerb.Test;

namespace Titanis.Cli.ScmTool.Test;

[TestClass]
public sealed class QueryTests : CliCommandTest<QueryCommand>
{
	[TestMethod]
	[CliTest("milchickNtlm_query", "milchickKerb_query")]
	public async Task TestMethod1(Token[] args)
	{
		var results = await TestCommand(args);
	}
}
