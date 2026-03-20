using Titanis.Cli.DcomTool;
using Titanis.Cli.Kerb.Test;

namespace Titanis.Cli.Dcom.Test;

[TestClass]
public sealed class InvokeTests : CliCommandTest<InvokeCommand>
{
	[TestMethod]
	[CliTest("milchickNtlm_Mmc20Exec", "milchickKerb_Mmc20Exec_fqdn")]
	public async Task TestMethod1(Token[] args)
	{
		var results = await TestCommand(args);
	}
}
