using System.ComponentModel;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;
using Titanis.Winterop.Security;

namespace Lsa;

/// <task category="LSA;Enumeration">Translate an a SID to its account name and domain</task>
[Command]
[OutputRecordType(typeof(LsaAccountMapping))]
[Description("Translates one or more SIDs to their account names")]
[DetailedHelpText("The command accepts multiple SIDs")]
[Example("Look up multiple names", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! S-1-5-21-1752138614-393460150-3098146133-1103 S-1-5-21-1752138614-393460150-3098146133-1107")]
internal class LookupSidCommand : LsaPolicyCommand
{
	protected sealed override LsaPolicyAccess RequiredPolicyAccess => LsaPolicyAccess.LookupNames;

	[Parameter(10)]
	[Mandatory]
	[Description("SIDs to look up")]
	public SecurityIdentifier[] Sids { get; set; }

	protected sealed override async Task<int> RunAsync(LsaPolicy policy, CancellationToken cancellationToken)
	{
		try
		{
			var mappings = await policy.ResolveSidsAsync(this.Sids, cancellationToken);
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