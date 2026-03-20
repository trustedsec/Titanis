using System.ComponentModel;
using Titanis.Msrpc.Mslsar;
using Titanis.Winterop.Security;

namespace Titanis.Cli.LsaTool;
public class LsaAccountParameters : ParameterGroupBase
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
