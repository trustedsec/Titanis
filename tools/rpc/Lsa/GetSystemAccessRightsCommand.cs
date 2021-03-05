using System.ComponentModel;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;
using Titanis.Winterop.Security;

namespace Lsa;

/// <task category="LSA;Enumeration">Enumerate the system access rights granted to an account</task>
[Command]
[OutputRecordType(typeof(SystemAccessRights))]
[Description("Gets the system access rights granted to an account")]
[Example("Get system access rights by SID", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-544")]
[Example("Get system access rights by account name", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrators")]
internal class GetSystemAccessRightsCommand : LsaPolicyCommand
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
		using (var user = await policy.OpenAccount(sid, LsaAccountAccess.View, cancellationToken))
		{
			var rights = await user.GetSystemAccess(cancellationToken);
			var allRights = Enum.GetValues<SystemAccessRights>();
			foreach (var right in allRights)
			{
				if (0 != (rights & right))
					this.WriteRecord(right);
			}
		}

		return 0;
	}
}