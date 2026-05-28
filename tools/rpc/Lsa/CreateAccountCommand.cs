using System.ComponentModel;
using Titanis.Msrpc.Mslsar;
using Titanis.Winterop.Security;

namespace Titanis.Cli.LsaTool;

/// <task category="LSA;Expanding Access">Create an LSA policy account</task>
[Description("Creates an account")]
[Example("Create a policy account", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! S-1-5-32-646")]
[Example("Create a policy account for a domain SID", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! S-1-5-21-1752138614-393460150-3098146133-1103", Tag ="milchick")]
public class CreateAccountCommand : LsaPolicyCommand
{
	protected sealed override LsaPolicyAccess RequiredPolicyAccess => LsaPolicyAccess.CreateAccount;

	[Parameter(10)]
	[Mandatory]
	[Description("SID of account to create")]
	public SecurityIdentifier Sid { get; set; }

	protected sealed override async Task<int> RunAsync(LsaPolicy policy, CancellationToken cancellationToken)
	{
		await policy.CreateAccount(this.Sid, cancellationToken);

		this.WriteMessage($"Created account {this.Sid}");

		return 0;
	}
}
