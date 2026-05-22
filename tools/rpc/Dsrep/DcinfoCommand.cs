using ms_drsr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Msdrsr;

namespace Dsrep;

/// <task category="RPC;Enumeration">Get info on domain controllers</task>
[Command]
[Description("Gets information on domain controllers")]
[OutputRecordType(typeof(DomainControllerInfo))]
internal class DcinfoCommand:DsbindCommand
{
	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		var dcinfos=await dsbind.GetDcInfo(this.RpcParameters.Authentication.UserDomain, cancellationToken);
		this.WriteRecords(dcinfos);

		return 0;
	}
}
