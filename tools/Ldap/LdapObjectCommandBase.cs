using System.ComponentModel;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;
internal abstract class LdapObjectCommandBase : LdapCommandBase
{
	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Names or DNs of objects to create")]
	public string[] ObjectName { get; set; }

	protected abstract Task<LdapDistinguishedName> ResolveObjectName(string simpleName, LdapClient ldap, CancellationToken cancellationToken);

	protected sealed override async Task<int> RunAsync(LdapClient ldap, CancellationToken cancellationToken)
	{
		foreach (var name in this.ObjectName)
		{
			LdapDistinguishedName dn;
			if (!name.Contains('='))
				// This is a simple namee
				dn = await this.ResolveObjectName(name, ldap, cancellationToken);
			else
			{
				var fullName = name;
				// This is relative to the domain root
				if (!name.Contains(",DC="))
					fullName += "," + ldap.DomainRoot;
				dn = new LdapDistinguishedName(fullName);
			}

			await RunAsync(ldap, dn, cancellationToken);
		}

		return 0;
	}

	protected abstract Task RunAsync(LdapClient ldap, LdapDistinguishedName objName, CancellationToken cancellationToken);
}
