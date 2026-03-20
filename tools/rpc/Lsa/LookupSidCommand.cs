using System.ComponentModel;
using Titanis.Msrpc.Mslsar;
using Titanis.Winterop.Security;

namespace Titanis.Cli.LsaTool;

/// <task category="LSA;Enumeration">Translate an a SID to its account name and domain</task>
[Command]
[OutputRecordType(typeof(LsaAccountMapping))]
[Description("Translates one or more SIDs to their account names")]
[DetailedHelpText("The command accepts multiple SIDs")]
[Example("Look up multiple names", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! S-1-5-21-1752138614-393460150-3098146133-1103 S-1-5-21-1752138614-393460150-3098146133-1107", "Titanis tries to connect via TCP but cannot find an enpoint, and falls back to connecting over SMB.", Tag ="milchickNtlm_LookupDomainSids")]
[Example("Look up multiple names on a DC", "{0} LUMON-DC1 -PreferSmb -UserName milchick -Password Br3@kr00m! S-1-5-21-1752138614-393460150-3098146133-1103 S-1-5-21-1752138614-393460150-3098146133-1107", "By default, Titanis checks for a TCP endpoint and tries to connect over IP first.  Specifying -PreferSmb forces it to skip the check for the TCP endpoint and uses named pipes instead.", Tag = "milchickNtlm_LookupDomainSidsOnDc")]
public class LookupSidCommand : LsaPolicyCommand
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