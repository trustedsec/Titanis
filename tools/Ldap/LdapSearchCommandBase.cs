using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;


public abstract class LdapSearchCommandBase : LdapGenericSearchCommandBase
{

	[Parameter]
	[Description("DN of search root (default is domain root)")]
	public LdapDistinguishedName[]? SearchBase { get; set; }

	[Parameter]
	[Description("Scope of search")]
	public LdapSearchScope? Scope { get; set; }

	[Parameter]
	[Description("Includes delete items (but not recycled)")]
	public SwitchParam IncludeDeleted { get; set; }

	[Parameter]
	[Description("Includes deleted and recycled items")]
	public SwitchParam IncludeRecycled { get; set; }

	[Parameter]
	[Description("Includes links to deleted items")]
	public SwitchParam IncludeDeletedLinks { get; set; }

	[Parameter]
	[Description("Only return changes since [cookie]")]
	public HexString? DirSync { get; set; }

    protected override void SetQueryProperties(LdapQuery query)
    {
        base.SetQueryProperties(query);

		var searchBase = query.SearchBase;
		var isRootDse = ((searchBase != null) && (searchBase.Rdns.Count == 0));

		query.IncludeDeleted = this.IncludeDeleted.IsSet;
		query.IncludeRecycled = this.IncludeRecycled.IsSet;
		query.IncludeDeletedLinks = this.IncludeDeletedLinks.IsSet;
		query.Scope = this.Scope ?? (isRootDse ? LdapSearchScope.BaseObject : LdapSearchScope.WholeSubtree);
		query.DirSyncCookie = this.DirSync?.Bytes;
    }
}
