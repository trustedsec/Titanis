using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Mswkst;

namespace Titanis.Cli.Wkst;

[Description("Gets a list of logged on users")]
[OutputRecordType(typeof(WorkstationInfo), DefaultOutputStyle = OutputStyle.List)]
public class EnumUsersCommand : WorkstationCommand
{
	protected override async Task<int> RunAsync(WorkstationClient client, CancellationToken cancellationToken)
	{
		await foreach (var user in client.GetLoggedOnUsers(this.CurrentServerName, cancellationToken).WithCancellation(cancellationToken))
		{
			this.WriteRecord(user);
		}
		return 0;
	}
}
