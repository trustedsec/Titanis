using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Msscmr;
using Titanis.Winterop.Security;

namespace Titanis.Cli.ScmTool;

/// <task category="SCM;Enumeration">Query the triggers configured to start or stop a service</task>
[Command]
[Description("Queries the status of a service")]
[OutputRecordType(typeof(ServiceTrigger))]
internal class QueryTriggersCommand : MultiServiceCommand
{
	protected sealed override ServiceAccessRights RequiredServiceAccess => ServiceAccessRights.QueryConfig;

	protected sealed override async Task RunAsync(Service service, CancellationToken cancellationToken)
	{
		var triggers = await service.QueryTriggersAsync(cancellationToken);
		this.WriteRecords(triggers);
	}
}
