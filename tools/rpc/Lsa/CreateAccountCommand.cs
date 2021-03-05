using System.ComponentModel;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;
using Titanis.Winterop.Security;

namespace Lsa;

/// <task category="LSA;Expanding Access">Create an LSA policy account</task>
[Description("Creates an account")]
[Example("Create an account", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! S-1-5-32-646")]
internal class CreateAccountCommand : LsaPolicyCommand
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
