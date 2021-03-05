using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Msscmr;

namespace Titanis.Cli.ScmTool;

/// <task category="SCM;Enumeration">Query the status of a service</task>
[Command]
[Description("Queries the status of a service")]
[OutputRecordType(typeof(EnumServiceStatusInfo))]
internal class QueryCommand : ScmCommand
{
	/// <inheritdoc/>
	protected sealed override ScmAccess RequiredScmAccess => ScmAccess.EnumerateService;

	[Parameter]
	[Description("Filter by service type")]
	public ServiceTypes[]? Types { get; set; }

	[Parameter]
	[Description("Filter by service state")]
	public ServiceStates[]? States { get; set; }

	protected sealed override async Task<int> RunAsync(Scm scm, CancellationToken cancellationToken)
	{
		ServiceTypes types = (this.Types == null) ? ServiceTypes.All : this.Types.Aggregate(ServiceTypes.None, (x, y) => x | y);
		ServiceStates states = (this.States == null) ? ServiceStates.All : this.States.Aggregate(ServiceStates.None, (x, y) => x | y);

		var records = await scm.GetServicesAsync(types, states, cancellationToken);
		this.WriteRecords(records);

		return 0;
	}
}
