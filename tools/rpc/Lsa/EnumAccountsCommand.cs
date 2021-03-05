using System.ComponentModel;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;
using Titanis.Winterop.Security;

namespace Lsa;

public class LsaAccountInfo
{
	public SecurityIdentifier Sid { get; set; }
	public string? AccountName { get; set; }
	public string? DomainName { get; set; }
}

/// <task category="LSA;Enumeration">Enumerate policy accounts</task>
[Command]
[OutputRecordType(typeof(LsaAccountInfo), DefaultFields = new string[] { nameof(LsaAccountInfo.Sid) })]
[Description("Enumerates accounts")]
[DetailedHelpText("By default, the output only includes the SIDs of the accounts.  Use -OutputFields if you want additional information such as the account or domain name.  The additional fields require another RPC call to the server.")]
[Example("Get account SIDs", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m!")]
[Example("Get account SIDs with account name and domain", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -OutputFields Sid, AccountName, DomainName")]
internal class EnumAccountsCommand : LsaPolicyCommand
{
	protected sealed override LsaPolicyAccess RequiredPolicyAccess =>
		LsaPolicyAccess.ViewLocalInfo
		| (this.IsFieldInOutput(nameof(LsaAccountInfo.AccountName)) ? LsaPolicyAccess.LookupNames : 0)
		;
	protected sealed override async Task<int> RunAsync(LsaPolicy policy, CancellationToken cancellationToken)
	{
		var sids = await policy.GetAccounts(cancellationToken);
		LsaAccountInfo[] accounts = await SidsToAccountInfos(policy, sids, cancellationToken);

		this.WriteRecords(accounts);
		this.WriteVerbose($"Retrieved {accounts.Length} accounts");
		return 0;
	}
}