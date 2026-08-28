using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msraa;
using Titanis.Winterop.Security;

namespace Titanis.Cli.Raza;

[Command]
[Description("Checks remote authorization")]
[OutputRecordType(typeof(AccessCheckResult))]
[OutputFieldFormat(nameof(AccessCheckResult.GrantedAccessMask), "X8", null)]
public class CheckCommand : RemoteAuthzCommand
{
	[Parameter]
	[Mandatory]
	[Description("Security descriptor(s) to evaluate")]
	public SecurityDescriptor[] Sddl { get; set; }

	[Parameter]
	[Description("SID to use for SELF ACEs")]
	public SecurityIdentifier? PrincipalSelf { get; set; }

	protected override async Task RunAsync(RemoteAuthorizationContext context, RemoteAuthorizationClient client, CancellationToken cancellationToken)
	{
		var sd = SecurityDescriptor.ParseSddl("O:BAG:SYD:(A;;FA;;;AU)(A;;GR;;;AU)", null);
		this.WriteRecord(await context.AccessCheck(
			sd,
			(uint)FileAccessRights.MaxAllowed,
			this.PrincipalSelf,
			cancellationToken));
	}
}
