using System.ComponentModel;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;

namespace Lsa;

/// <task category="LSA;Enumeration">Translate an account name to its SID and domain name</task>
[Command]
[OutputRecordType(typeof(LsaAccountMapping))]
[Description("Gets the SID for one or more account names")]
[DetailedHelpText("The command accepts multiple user names")]
[Example("Look up multiple names", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! marks milchick")]
internal class LookupNameCommand : LsaPolicyCommand
{
	protected sealed override LsaPolicyAccess RequiredPolicyAccess => LsaPolicyAccess.LookupNames;

	[Parameter(10)]
	[Description("Names of accounts to look up")]
	[Mandatory]
	public string[] AccountNames { get; set; }

	protected sealed override async Task<int> RunAsync(LsaPolicy policy, CancellationToken cancellationToken)
	{
		try
		{
			var mappings = await policy.ResolveAccountNames(this.AccountNames, cancellationToken);
			this.WriteRecords(mappings);
		}
		catch (LsaAccountMappingException ex)
		{
			this.WriteRecords(ex.Mappings);
			this.WriteError($"Failed to map one or more accounts");
		}
		return 0;
	}
}