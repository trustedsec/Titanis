using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli.Kerb.Test;
using Titanis.Winterop;

namespace Titanis.Cli.ScmTool.Test;

[TestClass]
public class DeleteTests : CliCommandTest<DeleteCommand>
{
	[TestMethod]
	[CliTest("milchickNtlm_delete")]
	public async Task DeleteTest(Token[] args)
	{
		var ex = await Assert.ThrowsAsync<Win32Exception>(async () =>
		{
			var results = await TestCommand(args);
		});
		Assert.AreEqual(Win32ErrorCode.ERROR_SERVICE_DOES_NOT_EXIST, (Win32ErrorCode)ex.NativeErrorCode);
	}
}
