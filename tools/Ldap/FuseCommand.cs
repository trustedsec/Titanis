using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli.Fuse;
using Titanis.Ldap;
using Titanis.Ldap.Fusion;
using Titanis.Linterop.Fuse;

namespace Titanis.Cli.LdapTool;

/// <task category="LDAP;Enumeration">Mount an LDAP directory as a file system</task>
[Description("Mounts a directory as a file system")]
internal class FuseCommand : LdapCommandBase
{
	[ParameterGroup(ParameterGroupOptions.Required)]
	public FuseParameterGroup FuseParameters { get; set; }

	[Parameter]
	[Description("Name of root entry in directory to mount")]
	public LdapDistinguishedName? SearchBase { get; set; }

	protected override async Task<int> RunAsync(LdapClient ldap, CancellationToken cancellationToken)
	{
		var fuseParams = this.FuseParameters;

		var root = (await ldap.Search(new LdapQuery(this.SearchBase ?? ldap.DomainRoot, LdapSearchScope.Base, null, null) { Options = LdapQueryOptions.AllPages }, cancellationToken).ConfigureAwait(false)).Entries.ToArray().FirstOrDefault();
		await Task.Yield();
		var rootNode = new LdapEntryNode(new LdapMountInfo
		{
			ldapClient = ldap,
			uid = fuseParams.Uid ?? NativeMethods.geteuid(),
			gid = fuseParams.Gid ?? NativeMethods.getegid(),
		}, ".", root);

		FuseMount.Mount(fuseParams.Mountpoint, rootNode, this.Log, fuseParams.ReadWrite.IsSet, cancellationToken, ["LdapMount"]);

		return 0;
	}
}
