using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msscmr;
using Titanis.Winterop.Security;

namespace Titanis.Cli.ScmTool;

[Command]
[Description("Queries the configuration of a service")]
[OutputRecordType(typeof(ServiceConfig), DefaultOutputStyle = OutputStyle.List)]
public class QueryConfigCommand : ServiceCommand
{
	protected override ServiceAccessRights RequiredServiceAccess => ServiceAccessRights.QueryConfig;

	protected override ScmAccessRights RequiredScmAccess => ScmAccessRights.Connect;

	protected override async Task<int> RunAsync(Scm scm, Service service, CancellationToken cancellationToken)
	{
		var config = await service.QueryConfigAsync(cancellationToken);
		this.WriteRecord(config);
		return 0;
	}
}
