using System.ComponentModel;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;
using Titanis.Winterop.Security;

namespace Lsa;

/// <task category="LSA;Expanding Access">Set the system access rights for an account</task>
[Command]
[OutputRecordType(typeof(SystemAccessRights))]
[Description("Sets the system access rights granted to an account")]
[Example("Set system access rights by SID", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-544 SeBatchLogonRight")]
[Example("Set system access rights by account name", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrators SeBatchLogonRight")]
[DetailedHelpText(@"By default, the specified access rights are added to the rights already granted to the account.  Use -Reset to clear existing access rights and only grant the rights specified.  To reset all access rights currently granted, use -Reset and specify a single right of 0")]
internal class SetSystemAccessRightsCommand : LsaPolicyCommand
{
	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public LsaAccountParameters Account { get; set; }

	[Parameter]
	[Description("Clears any rights already set on the account")]
	public SwitchParam Reset { get; set; }

	[Parameter(10)]
	[Mandatory]
	[Description("Access rights to grant")]
	public SystemAccessRights[] Rights { get; set; }

	protected sealed override LsaPolicyAccess RequiredPolicyAccess => LsaPolicyAccess.LookupNames;

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);
		this.Account.Validate(context);
	}

	protected sealed override async Task<int> RunAsync(LsaPolicy policy, CancellationToken cancellationToken)
	{
		SecurityIdentifier sid = await this.Account.Resolve(policy, cancellationToken);
		using (var user = await policy.OpenAccount(sid, LsaAccountAccess.View | LsaAccountAccess.AdjustSystemAccess, cancellationToken))
		{
			SystemAccessRights rights = 0;
			if (!this.Reset.IsSet)
			{
				rights = await user.GetSystemAccess(cancellationToken);
			}

			foreach (var right in this.Rights)
			{
				rights |= right;
			}

			await user.SetSystemAccess(rights, cancellationToken);

			this.WriteVerbose($"Set system access to {rights}");
		}

		return 0;
	}
}