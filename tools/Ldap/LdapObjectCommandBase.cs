using System.ComponentModel;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

public abstract class LdapObjectCommandBase : LdapCommandBase
{
	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Names or DNs of objects to create")]
	public string[] ObjectName { get; set; }

	protected virtual async Task<LdapDistinguishedName?> ResolveObjectName(string simpleName, LdapClient ldap, CancellationToken cancellationToken)
	{
		var result = await ldap.SimpleSearch(simpleName, cancellationToken);
		if (result.EntryCount == 0)
			return null;
		else if (result.EntryCount == 1)
			return result.Entries[0].EntryName;
		else
		{
			this.WriteError($"The search for '{simpleName}' return multiple results:");
			foreach (var entry in result.Entries)
			{
				this.WriteMessage($"DN: {entry.EntryName}");
			}

			throw new InvalidOperationException($"The name '{simpleName}' resolved to multiple objects.  Either specify a more restrictive search string or specify the DN of the desired object.");
		}
	}

	protected sealed override async Task<int> RunAsync(LdapClient ldap, CancellationToken cancellationToken)
	{
		bool hasMatch = false;
		foreach (var name in this.ObjectName)
		{
			LdapDistinguishedName dn;
			if (!name.Contains('='))
			{
				// This is a simple namee
				dn = await this.ResolveObjectName(name, ldap, cancellationToken);
				if (dn is null)
				{
					this.WriteWarning($"No object found matching '{name}'");
					continue;
				}
			}
			else
			{
				var fullName = name;

				// This is relative to the domain root
				if (!name.Contains(",DC="))
					fullName += "," + ldap.DomainRoot;
				dn = new LdapDistinguishedName(fullName);
			}

			hasMatch = true;
			await RunAsync(ldap, dn, cancellationToken);
		}

		return 0;
	}

	protected abstract Task RunAsync(LdapClient ldap, LdapDistinguishedName objName, CancellationToken cancellationToken);
}
