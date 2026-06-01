using Titanis.Cli.Kerb.Test;
using Titanis.Winterop;

namespace Titanis.Cli.LsaTool.Test;

[TestClass]
public sealed class CreateAccountTests : CliCommandTest<CreateAccountCommand>
{
	[TestMethod]
	[CliTest("milchick")]
	public async Task CreateAccountTest(Token[] args)
	{
		try
		{
			var results = await TestCommand(args);
		}
		catch (NtstatusException ex) when (ex.StatusCode == Ntstatus.STATUS_OBJECT_NAME_COLLISION)
		{
			// Object already exists
		}
	}
}
