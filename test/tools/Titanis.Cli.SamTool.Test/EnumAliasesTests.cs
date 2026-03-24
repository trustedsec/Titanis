using Titanis.Cli.Kerb.Test;

namespace Titanis.Cli.SamTool.Test;

[TestClass]
public sealed class EnumAliasesTests : CliCommandTest<EnumAliasesCommand>
{
	[TestMethod]
	[CliTest("milchickNtlm_enum")]
	public async Task EnumAliasesTest(Token[] args)
	{
		var results = await TestCommand(args);
	}
}
