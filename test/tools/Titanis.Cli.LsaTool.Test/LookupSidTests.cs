using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli.Kerb.Test;

namespace Titanis.Cli.LsaTool.Test;

[TestClass]
public class LookupSidTests : CliCommandTest<LookupSidCommand>
{
	[TestMethod]
	[CliTest("milchickNtlm_LookupDomainSids", "milchickNtlm_LookupDomainSidsOnDc")]
	public async Task LookupSidTest(Token[] args)
	{
		var results = await TestCommand(args);
	}
}
