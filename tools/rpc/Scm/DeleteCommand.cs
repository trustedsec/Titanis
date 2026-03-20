using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msscmr;
using Titanis.Winterop.Security;

namespace Titanis.Cli.ScmTool;

/// <task category="SCM">Delete a service</task>
[Description("Deletes a service")]
[Example("Delete a service", "{0} LUMON-DC1 -UserName milchick -Password Br3@kr00m! myservice", Tag = "milchickNtlm_delete")]
public class DeleteCommand : ServiceCommand
{
	protected sealed override ServiceAccessRights RequiredServiceAccess => (ServiceAccessRights)StandardAccessRights.Delete;
	protected sealed override ScmAccessRights RequiredScmAccess => ScmAccessRights.None;
	protected sealed override async Task<int> RunAsync(Scm scm, Service service, CancellationToken cancellationToken)
	{
		await service.DeleteAsync(cancellationToken);
		this.WriteMessage($"Deleted service '{this.ServiceName}'");
		return 0;
	}
}
