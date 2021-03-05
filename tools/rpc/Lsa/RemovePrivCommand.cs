using System.ComponentModel;
using System.ComponentModel.Design;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;

namespace Lsa;

/// <task category="LSA">Revoke a privilege from an account</task>
[Command]
[Description("Removes one or more privileges from an account")]
[DetailedHelpText(@"Each privilege may be the symbolic name or the value, expressed as a 64-bit integer.  If the name is not a predefined privilege, {0} resolves the name with the remote LSA.  For predefined privilege names (those in the help text), you are not required to append `Privilege` to the name.

To remove all privileges, use `*`.  Note that you may have to escape this depending on which shell you are using.

This command cannot be used to remove a user right.

Note that the LSA tracks accounts separate from the SAM.  Even for local accounts, you make need to create the LSA account first.")]
[Example("Remove SeTcbPrivilege from Administrators", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrators SeTcb")]
[Example("Remove SeTcbPrivilege from S-1-5-32-646", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-646 SeTcb")]
[Example("Remove all privileges from S-1-5-32-646", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-646 *")]
internal class RemovePrivCommand : LsaPolicyCommand
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
		bool allPrivs = (this.Privileges[0] == "*");

		IList<PrivilegeInfo>? privs;
		if (allPrivs)
		{
			privs = null;
		}
		else
		{
			privs = await ResolvePrivileges(policy, this.Privileges, PrivilegeAttributes.None, cancellationToken);
			bool lookupFailure = privs.Count < this.Privileges.Count();
			if (lookupFailure)
			{
				this.WriteError("An error occurred resolving one or more privileges.  Operation aborting.");
				return 1;
			}
		}

		var sid = await this.Account.Resolve(policy, cancellationToken);
		using (var account = await policy.OpenAccount(sid, LsaAccountAccess.AdjustPrivileges, cancellationToken))
		{
			if (allPrivs)
			{
				await account.RemoveAllPrivileges(cancellationToken);
				this.WriteVerbose($"Removed ALL privileges to account");
			}
			else
			{
				await account.RemovePrivileges(privs, cancellationToken);
				this.WriteVerbose($"Removed {privs.Count} privileges to account");
			}
		}

		return 0;
	}
}