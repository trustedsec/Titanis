using Titanis.Cli.Kerb.Test;
using Titanis.Smb2.Cli;
using Titanis.Winterop;

namespace Titanis.Cli.Smb2Client.Test;

[TestClass]
public sealed class LsTests : CliCommandTest<Smb2LsCommand>
{
	[TestMethod]
	[CliTest("MilchickNtlm_Mdr")]
	public async Task MilchickNtlm_Mdr(Token[] tokens)
	{
		var results = await TestCommand(tokens);
	}

	[TestMethod]
	[CliTest("Anon_Pipes")]
	public async Task Anon_ListPipes(Token[] tokens)
	{
		var results = await TestCommand(tokens);
	}

	[TestMethod]
	[CliTest("Milchick_ListPipes", "Milchick_AltHostAddress", "MilchickNtlmHash", "MilchickNtlm_WorkstationVersion", "MilchickKerberos", "MilchickNtlm_AllFields")]
	public async Task MilchickTests(Token[] tokens)
	{
		try
		{
			var results = await TestCommand(tokens);
		}
		catch (NtstatusException ex) when (ex.StatusCode == Ntstatus.STATUS_ACCESS_DENIED)
		{
			// The lab is configured with strict SPN checking
		}
	}
}
