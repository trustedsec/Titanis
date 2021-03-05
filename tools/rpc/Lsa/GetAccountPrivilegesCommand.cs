using System.ComponentModel;
using Titanis;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;
using Titanis.Winterop.Security;

namespace Lsa;

/// <task category="LSA;Enumeration">Enumerate the privileges granted to an account</task>
[Command]
[OutputRecordType(typeof(PrivilegeInfo), DefaultFields = new string[] { nameof(PrivilegeInfo.Privilege) })]
[Description("Gets the privileges assigned to an account.")]
[DetailedHelpText(@"One of -BySid or -ByName is required to specify the account to get the privileges for.  The account may be a user or group.

By default, only the names of predefined privileges are resolved.  Te get the names of all privileges, use -OutputFields to specify PrivilegeName")]
[Example("Get privileges for account by SID", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-544")]
[Example("Get privileges for account by name", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrator")]
[Example("Get privileges for account by name, look up privilege names", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrator -OutputFields Privilege, PrivilegeName")]
internal class GetAccountPrivilegesCommand : LsaPolicyCommand
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
		Dictionary<Privilege, string> names = new Dictionary<Privilege, string>();

		SecurityIdentifier sid = await this.Account.Resolve(policy, cancellationToken);
		using (var user = await policy.OpenAccount(sid, LsaAccountAccess.View, cancellationToken))
		{
			var privs = await user.GetPrivileges(cancellationToken);

			if (this.IsFieldInOutput(nameof(PrivilegeInfo.PrivilegeName)))
			{
				for (int i = 0; i < privs.Length; i++)
				{
					PrivilegeInfo? priv = privs[i];
					if (names.TryGetValue(priv.Privilege, out var name))
						priv = priv.WithPrivilegeName(name);
					else
					{
						try
						{
							name = await policy.LookupPrivilege((long)priv.Privilege, cancellationToken);
							names.Add(priv.Privilege, name);
							priv = priv.WithPrivilegeName(name);
						}
						catch { }
					}

					privs[i] = priv;
				}
			}

			this.WriteRecords(privs);
			this.WriteVerbose($"Retrieved {privs.Length} privileges");
		}

		return 0;
	}
}