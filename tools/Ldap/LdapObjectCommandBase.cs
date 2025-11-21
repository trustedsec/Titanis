using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;

namespace Ldap;
internal abstract class LdapObjectCommandBase : LdapCommandBase
{
	[Parameter(20)]
	[Mandatory]
	[Description("Names or DNs of OUs to create")]
	public string[] Name { get; set; }

	protected abstract string RdnName { get; }
	protected sealed override async Task<int> RunAsync(LdapClient ldap, CancellationToken cancellationToken)
	{
		foreach (var name in this.Name)
		{
			var dn = name;
			if (!dn.Contains('='))
				// This is a simple namee
				dn = this.RdnName + "=" + LdapRelativeDistinguishedName.Escape(dn);

			if (!dn.Contains(",DC="))
				// This is relative to the domain root
				dn += "," + ldap.DomainRoot;

			var objName = new LdapDistinguishedName(dn);
			await RunAsync(ldap, objName, cancellationToken);
		}

		return 0;
	}

	protected abstract Task RunAsync(LdapClient ldap, LdapDistinguishedName objName, CancellationToken cancellationToken);
}
