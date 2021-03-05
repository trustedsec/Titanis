using Titanis.Msrpc.Mslsar;
using Titanis.Winterop.Security;

namespace Lsa;

internal abstract class LsaPolicyCommand : LsaCommand
{
	/// <summary>
	/// Gets the <see cref="LsaPolicyAccess"/> required by the command.
	/// </summary>
	protected abstract LsaPolicyAccess RequiredPolicyAccess { get; }

	protected sealed override async Task<int> RunAsync(LsaClient client, CancellationToken cancellationToken)
	{
		using (var policy = await client.OpenPolicy(this.RequiredPolicyAccess, cancellationToken))
		{
			return await this.RunAsync(policy, cancellationToken);
		}
	}

	protected abstract Task<int> RunAsync(LsaPolicy policy, CancellationToken cancellationToken);

	protected async Task<LsaAccountInfo[]> SidsToAccountInfos(LsaPolicy policy, SecurityIdentifier[] sids, CancellationToken cancellationToken)
	{
		var accounts = Array.ConvertAll(sids, r => new LsaAccountInfo() { Sid = r });

		if (this.IsFieldInOutput(nameof(LsaAccountInfo.AccountName)) || this.IsFieldInOutput(nameof(LsaAccountInfo.DomainName)))
		{
			LsaAccountMapping[] mappings;
			try
			{
				mappings = await policy.ResolveSidsAsync(sids, cancellationToken);
			}
			catch (LsaAccountMappingException ex)
			{
				mappings = ex.Mappings;
				this.WriteWarning("Not all accounts could be mapped.");
			}

			for (int i = 0; i < mappings.Length; i++)
			{
				LsaAccountMapping? mapping = mappings[i];
				if (mapping is null)
					continue;

				var account = accounts[i];
				account.AccountName = mapping.AccountName;
				account.DomainName = mapping.DomainName;
			}
		}

		return accounts;
	}

	protected async Task<IList<PrivilegeInfo>> ResolvePrivileges(LsaPolicy policy, IList<string> privilegeSpecs, PrivilegeAttributes attributes, CancellationToken cancellationToken)
	{
		List<PrivilegeInfo> privs = new List<PrivilegeInfo>(privilegeSpecs.Count);
		PrivilegeAttributes attrs = PrivilegeAttributes.None;
		// UNDONE: Although the flags are tracked, they do not appear to affect the token created when the user logs in.
		//PrivilegeAttributes attrs = PrivilegeAttributes.Enabled | PrivilegeAttributes.EnabledByDefault;

		for (int i = 0; i < privilegeSpecs.Count; i++)
		{
			var privSpec = privilegeSpecs[i];
			if (Enum.TryParse(privSpec, out Privilege priv))
				privs.Add(new PrivilegeInfo(priv, attrs));
			else if (Enum.TryParse(privSpec + "Privilege", out priv))
				privs.Add(new PrivilegeInfo(priv, attrs));
			else if (long.TryParse(privSpec, out var privInt))
				privs.Add(new PrivilegeInfo((Privilege)privInt, attrs));
			else
			{
				try
				{
					var privInfo = await policy.LookupPrivilege(privSpec, cancellationToken);
					privs.Add(new PrivilegeInfo(privInfo, attrs));
				}
				catch (Exception ex)
				{
					this.WriteError($"Unable to resolve privilege {privSpec}: {ex.Message}");
				}
			}
		}

		return privs;
	}
}