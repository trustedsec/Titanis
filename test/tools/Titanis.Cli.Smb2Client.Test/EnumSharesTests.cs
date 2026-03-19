using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli.Kerb.Test;
using Titanis.Smb2.Cli;

namespace Titanis.Cli.Smb2Client.Test;

[TestClass]
public class EnumSharesTests : CliCommandTest<Smb2EnumSharesCommand>
{
	[TestMethod]
	[CliTest("marks_basic", "milchick_Level2", "milchick_SecDesc")]
	public async Task BasicListingTest(Token[] args)
	{
		// TODO: Assert the appropriate levels and fields are returned
		var results = await this.TestCommand(args);
	}
}
