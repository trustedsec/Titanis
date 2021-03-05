using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msscmr;

namespace Titanis.Cli.ScmTool;
internal abstract class ServiceCommand : ScmCommand
{
	protected abstract ServiceAccess RequiredServiceAccess { get; }

	[Parameter(10)]
	[Mandatory]
	[Description("Name of the service")]
	public string ServiceName { get; set; }

	protected sealed override async Task<int> RunAsync(Scm scm, CancellationToken cancellationToken)
	{
		using (var service = await scm.OpenServiceAsync(this.ServiceName, this.RequiredServiceAccess, cancellationToken))
		{
			return await this.RunAsync(scm, service, cancellationToken);
		}
	}

	protected abstract Task<int> RunAsync(Scm scm, Service service, CancellationToken cancellationToken);
}
