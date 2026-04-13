using Titanis.Cli.Kerb.Test;
using Titanis.Msrpc.Msrrp.Cli;

namespace Titanis.Cli.Reg.Test;

[TestClass]
public sealed class DumpLsaSecretsTests : CliCommandTest<DumpLsaSecretsCommand>
{
	[TestMethod]
	[CliTest("marks_backup")]
	public async Task DumpLsaSecretsTest(Token[] args)
	{
		var results = await TestCommand(args);
	}
}
