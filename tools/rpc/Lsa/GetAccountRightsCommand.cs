using System.ComponentModel;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;
using Titanis.Winterop.Security;

namespace Lsa;

/// <task category="LSA;Enumeration">Enumerate the rights and privileges granted to an account</task>
[Command]
[Description("Gets the user rights and privileges granted to an account")]
[OutputRecordType(typeof(UserRightInfo), DefaultFields = new string[] { nameof(UserRightInfo.Name) })]
[Example("Get privileges and rights by SID", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-544")]
[Example("Get privileges and rights by name", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrators")]
internal class GetAccountRightsCommand : LsaPolicyCommand
{
	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public LsaAccountParameters Account { get; set; }

	protected sealed override LsaPolicyAccess RequiredPolicyAccess => LsaPolicyAccess.LookupNames;

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);
		this.Account.Validate(context);
	}

	protected sealed override async Task<int> RunAsync(LsaPolicy policy, CancellationToken cancellationToken)
	{
		SecurityIdentifier sid = await this.Account.Resolve(policy, cancellationToken);

		var rights = await policy.GetUserRights(sid, cancellationToken);
		this.WriteRecords(rights);

		this.WriteVerbose($"Retrieved {rights.Length} privileges");

		return 0;
	}
}