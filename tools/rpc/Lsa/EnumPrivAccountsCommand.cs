using System.ComponentModel;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;

namespace Lsa;

[Command]
[OutputRecordType(typeof(LsaAccountInfo))]
[Description("Enumerates accounts that have a specific privilege or user right")]
[Example("Enumerate accounts with SeInteractiveLogonRight", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -Privilege SeInteractiveLogonRight")]
internal class EnumPrivAccountsCommand : LsaPolicyCommand
{
	/// <task category="LSA;Enumeration">Enumerate accounts that are granted a privilege</task>
	[Parameter]
	[Mandatory]
	[Description("Name of privilege or user right to check for")]
	[EnumNameList(typeof(Privilege), typeof(SystemAccessRights))]
	public string Privilege { get; set; }

	protected sealed override LsaPolicyAccess RequiredPolicyAccess => LsaPolicyAccess.LookupNames | LsaPolicyAccess.ViewLocalInfo;

	protected sealed override async Task<int> RunAsync(LsaPolicy policy, CancellationToken cancellationToken)
	{
		var sids = await policy.GetAccountsWithPrivilege(this.Privilege, cancellationToken);

		var accounts = await this.SidsToAccountInfos(policy, sids, cancellationToken);
		this.WriteRecords(accounts);

		this.WriteVerbose($"Retrieved {accounts.Length} accounts");

		return 0;
	}
}
