using Titanis.Cli.Kerb.Test;

namespace Titanis.Cli.SamTool.Test;

[TestClass]
public sealed class EnumGroupsTests : CliCommandTest<EnumGroupsCommand>
{
	[TestMethod]
	[CliTest("milchickNtlm_enum")]
	public async Task EnumGroupsTest(Token[] args)
	{
		var results = await TestCommand(args);
	}
}
