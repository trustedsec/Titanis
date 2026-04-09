using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli.Kerb.Test;

namespace Titanis.Cli.LsaTool.Test;

[TestClass]
public class WhoamiTests : CliCommandTest<WhoamiCommand>
{
	[TestMethod]
	[CliTest("Ntlm_UserNamePassword", "Kerberos_UserNamePassword", "Kerberos_S4U", "Kerberos_Interrealm", "Kerberos_S4U_NtlmHash")]
	public async Task TestWhoami(Token[] args)
	{
		var result = (await TestCommand(args))[0];
	}
}
