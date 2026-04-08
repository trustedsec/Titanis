using Titanis.Cli.Kerb.Test;

namespace Titanis.Cli.SamTool.Test;

[TestClass]
public sealed class MembersCommandTests : CliCommandTest<AliasMembersCommand>
{
	[TestMethod]
	[CliTest("milchickNtlm_544", "milchickNtlm_multi", "milchickNtlm_BadAlias")]
	public async Task AliasMemberTests(Token[] args)
	{
		var results = await TestCommand(args);
	}
}
