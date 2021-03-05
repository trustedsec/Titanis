using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titanis.Msrpc.Msscmr;

namespace Titanis.Cli.ScmTool;
internal abstract class MultiServiceCommand : ScmCommand
{
	protected abstract ServiceAccess RequiredServiceAccess { get; }

	protected sealed override ScmAccess RequiredScmAccess => ScmAccess.EnumerateService;

	[Parameter(10)]
	[Mandatory]
	[Description("Names of services to query (* for all)")]
	public string[] ServiceName { get; set; }

	protected sealed override async Task<int> RunAsync(Scm scm, CancellationToken cancellationToken)
	{
		if (this.ServiceName[0] == "*")
		{
			var services = await scm.GetServicesAsync(cancellationToken);
			foreach (var serviceInfo in services)
			{
				try
				{
					using (var service = await scm.OpenServiceAsync(serviceInfo.ServiceName, this.RequiredServiceAccess, cancellationToken))
					{
						await RunAsync(service, cancellationToken);
					}
				}
				catch (Exception ex)
				{
					this.WriteError($"Failed to open service '{serviceInfo.ServiceName}' with error: {ex.Message}");
				}
			}
		}
		else
		{
			foreach (var name in this.ServiceName)
			{
				try
				{
					using (var service = await scm.OpenServiceAsync(name, this.RequiredServiceAccess, cancellationToken))
					{
						await RunAsync(service, cancellationToken);
					}
				}
				catch (Exception ex)
				{
					this.WriteError($"Failed to open service '{name}' with error: {ex.Message}");
				}
			}
		}

		return 0;
	}

	protected abstract Task RunAsync(Service service, CancellationToken cancellationToken);
}
