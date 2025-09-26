using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;
using Titanis.Winterop.Security;

namespace Lsa;
internal class LsaAccountParameters : ParameterGroupBase
{
	[Parameter]
	[Description("SID of account")]
	public SecurityIdentifier? BySid { get; set; }

	[Parameter]
	[Description("Account name")]
	public string? ByName { get; set; }

	public void Validate(ParameterValidationContext context)
	{
		if (this.BySid == null && this.ByName == null)
			context.LogError("You must specify either -BySid or -ByName");
	}

	public async Task<SecurityIdentifier> Resolve(LsaPolicy policy, CancellationToken cancellationToken)
	{
		if (this.BySid != null)
			return this.BySid;
		else if (this.ByName != null)
			return (await policy.ResolveAccountName(this.ByName, cancellationToken)).AccountSid;
		else
			throw new ArgumentException("No account specified");

	}
}
