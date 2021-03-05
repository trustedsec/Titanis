using System.ComponentModel;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;

namespace Lsa;

/// <task category="LSA;Expanding Access">Grant a privilege to an account</task>
[Command]
[Description("Adds one or more privileges to an account")]
[DetailedHelpText(@"Each privilege may be the symbolic name or the value, expressed as a 64-bit integer.  If the name is not a predefined privilege, {0} resolves the name with the remote LSA.

This command cannot be used to grant a user right.

Note that the LSA tracks accounts separate from the SAM.  Even for local accounts, you make need to create the LSA account first.")]
[Example("Add SeLoadDriverPrivilege and SeTcbPrivilege to Administrators", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrators SeLoadDriver SeTcb")]
[Example("Add SeLoadDriverPrivilege and SeTcbPrivilege to S-1-5-32-646", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-646 SeLoadDriver SeTcb")]
internal class AddPrivCommand : LsaPolicyCommand
{
	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public LsaAccountParameters Account { get; set; }

	[Parameter(10)]
	[Mandatory]
	[Description("Names or values of the privileges to add")]
	[EnumNameList(typeof(Privilege))]
	public string[] Privileges { get; set; }

	protected sealed override LsaPolicyAccess RequiredPolicyAccess => LsaPolicyAccess.LookupNames;

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);
		this.Account.Validate(context);
	}

	protected sealed override async Task<int> RunAsync(LsaPolicy policy, CancellationToken cancellationToken)
	{
		// UNDONE: Although the flags are tracked, they do not appear to affect the token created when the user logs in.
		//PrivilegeAttributes attrs = PrivilegeAttributes.Enabled | PrivilegeAttributes.EnabledByDefault;
		var privs = await ResolvePrivileges(policy, this.Privileges, PrivilegeAttributes.None, cancellationToken);
		bool lookupFailure = privs.Count < this.Privileges.Count();
		if (lookupFailure)
		{
			this.WriteError("An error occurred resolving one or more privileges.  Operation aborting.");
			return 1;
		}

		var sid = await this.Account.Resolve(policy, cancellationToken);
		using (var account = await policy.OpenAccount(sid, LsaAccountAccess.AdjustPrivileges, cancellationToken))
		{
			await account.AddPrivileges(privs, cancellationToken);
		}

		this.WriteVerbose($"Added {privs.Count} privileges to account");

		return 0;
	}
}