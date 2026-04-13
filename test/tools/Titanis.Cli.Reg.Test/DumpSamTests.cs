using Titanis.Cli.Kerb.Test;
using Titanis.Msrpc.Msrrp.Cli;

namespace Titanis.Cli.Reg.Test;

[TestClass]
public sealed class DumpSamTests : CliCommandTest<DumpSamCommand>
{
	[TestMethod]
	[CliTest("marks_backup")]
	public async Task DumpSamTest(Token[] args)
	{
		var results = await TestCommand(args);
	}
}
