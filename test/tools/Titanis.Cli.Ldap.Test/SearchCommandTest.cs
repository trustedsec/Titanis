using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli.Kerb.Test;
using Titanis.Cli.LdapTool;

namespace Titanis.Cli.Ldap.Test;

[TestClass]
public class SearchCommandTest : CliCommandTest<SearchCommand>
{
	[TestMethod]
	[CliTest("milchickKerb_ldaps_ChannelBinding", "milchickNtlm_ldaps_ChannelBinding")]
	public async Task ListPartitionsTest(Token[] args)
	{
		var results = await TestCommand(args);
	}
}
