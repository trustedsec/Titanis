using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msscmr;
using Titanis.Winterop.Security;

namespace Titanis.Cli.ScmTool;

public abstract class ServiceCommand : ScmCommand
{
	protected abstract ServiceAccessRights RequiredServiceAccess { get; }

	[Parameter(10)]
	[Mandatory]
	[Description("Name of the service")]
	public string[] ServiceName { get; set; }

	protected sealed override async Task<int> RunAsync(Scm scm, CancellationToken cancellationToken)
	{
		foreach (var serviceName in this.ServiceName)
		{
			try
			{
				using (var service = await scm.OpenServiceAsync(serviceName, this.RequiredServiceAccess, cancellationToken))
				{
					return await this.RunAsync(scm, service, cancellationToken);
				}
			}
			catch (Exception ex)
			{
				this.WriteError($"Failed for service {serviceName}: {ex.ToString()}");
				if (!this.ContinueOnError.IsSet)
					throw;
			}
		}

		return 0;
	}

	protected abstract Task<int> RunAsync(Scm scm, Service service, CancellationToken cancellationToken);
}
