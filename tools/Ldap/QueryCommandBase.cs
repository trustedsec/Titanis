using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

/// <summary>
/// Base class for commands that 
/// </summary>
public abstract class QueryCommandBase : LdapSearchCommandBase
{

	protected sealed override async Task<int> RunAsync(LdapClient ldap, CancellationToken cancellationToken)
	{
		if (!this.OutputFieldsSpecified)
			this.OutputFields = [nameof(LdapEntry.EntryName)];

		if (this.SearchBase.IsNullOrEmpty())
			this.SearchBase = [ldap.DomainRoot];

		foreach (var searchBase in this.SearchBase)
		{
			LdapQuery query = CreateQuery(searchBase);
			await BuildAndRunQuery(ldap, query, cancellationToken).ConfigureAwait(false);

			if (this.FollowReferrals.IsSet)
			{
				while (this.referralQueue.TryDequeue(out var referral))
				{
					this.WriteMessage($"Following referral {referral}");
					Uri refuri;
					try
					{
						refuri = new Uri(referral);
					}
					catch (Exception ex)
					{
						this.WriteError($"Referral '{referral}' is not valid: {ex.Message}");
						continue;
					}

					try
					{
						var refClient = await base.ConnectLdap(refuri.Host, cancellationToken);

						var referredBase = new LdapDistinguishedName(refuri.PathAndQuery.Substring(1));
						query = CreateQuery(referredBase);
						await BuildAndRunQuery(refClient, query, cancellationToken).ConfigureAwait(false);
					}
					catch (Exception ex)
					{
						this.WriteError($"Search on referral '{referral}' failed: {ex.Message}");
						continue;
					}
				}
			}
		}

		return 0;
	}

	protected abstract LdapQuery CreateQuery(LdapDistinguishedName searchBase);
}
