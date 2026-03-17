
namespace Titanis.Cli.Kerb.Test;

[TestClass]
public sealed class SelectTests : CliCommandTest<SelectCommand>
{
	[TestMethod]
	[CliTest("AllMilchickCcache")]
	public async Task TestReadAllMilchickTickets(Token[] args)
	{
		var results = await TestCommand(args);
		Assert.HasCount(4, results);
	}
	[TestMethod]
	[CliTest("CombineMilchickCache")]
	public async Task TestCombineAllMilchickTickets(Token[] args)
	{
		var results = await TestCommand(args);
		Assert.HasCount(4, results);

		// Verify written file

	}
}
