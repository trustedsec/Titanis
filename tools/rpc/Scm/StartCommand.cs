using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msscmr;
using Titanis.Winterop.Security;

namespace Titanis.Cli.ScmTool;

/// <task category="SCM;Lateral Movement">Start a service</task>
[Description("Starts a service")]
[Example("Start a service", "{0} LUMON-DC1 -UserName milchick -Password Br3@kr00m! -EncryptRpc myservice")]
[Example("Start a service with arguments", "{0} LUMON-DC1 -UserName milchick -Password Br3@kr00m! -EncryptRpc myservice arg1 arg2 arg3")]
internal class StartCommand : ServiceCommand
{
	protected sealed override ServiceAccess RequiredServiceAccess => ServiceAccess.Start;
	protected sealed override ScmAccess RequiredScmAccess => ScmAccess.None;

	[Parameter(20)]
	[Description("Optional arguments to pass to service")]
	public string[] ServiceArgs { get; set; }

	protected sealed override async Task<int> RunAsync(Scm scm, Service service, CancellationToken cancellationToken)
	{
		await service.StartAsync(this.ServiceArgs, cancellationToken);
		this.WriteMessage($"Started service '{this.ServiceName}'");
		return 0;
	}
}
