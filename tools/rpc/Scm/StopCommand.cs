using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msscmr;
using Titanis.Winterop.Security;

namespace Titanis.Cli.ScmTool;

/// <task category="SCM">Stop a service</task>
[Description("Stops a service")]
[Example("Stop a service", "{0} LUMON-DC1 -UserName milchick -Password Br3@kr00m! -EncryptRpc myservice")]
internal class StopCommand : ServiceCommand
{
	protected sealed override ServiceAccess RequiredServiceAccess => ServiceAccess.Stop;
	protected sealed override ScmAccess RequiredScmAccess => ScmAccess.None;

	protected sealed override async Task<int> RunAsync(Scm scm, Service service, CancellationToken cancellationToken)
	{
		await service.StopAsync(cancellationToken);
		this.WriteMessage($"Stopped service '{this.ServiceName}'");
		return 0;
	}
}
