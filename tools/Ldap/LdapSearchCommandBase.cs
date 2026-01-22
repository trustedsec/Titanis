using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using Titanis.Cli;
using Titanis.Ldap;

namespace Ldap;
internal abstract class LdapSearchCommandBase : LdapCommandBase, ILdapClientSearchCallback
{

	[Parameter]
	[Description("DN of search root (default is domain root)")]
	public LdapDistinguishedName[]? SearchBase { get; set; }

	[Parameter]
	[Description("Scope of search")]
	public LdapSearchScope? Scope { get; set; }

	[Parameter]
	[Description("Number of results to fetch per page")]
	[DefaultValue(100)]
	public int? PageSize { get; set; }

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

	[Parameter]
	[Description("Max number of records to return")]
	public int? RecordLimit { get; set; }

	[Parameter]
	[Description("Follows referrals")]
	public SwitchParam FollowReferrals { get; set; }

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		if (this.RecordLimit.HasValue)
		{
			if (this.RecordLimit == 0)
			{
				this.WriteWarning($"-{nameof(RecordLimit)} is set to 0.  There will be no results.  To return all records, don't specify any limit.");
			}
			else if (this.RecordLimit < 0)
			{
				context.LogError(new ParameterValidationError(nameof(RecordLimit), $"-{nameof(RecordLimit)} must be a positive integer."));
			}
			else if (this.PageSize > this.RecordLimit)
			{
				this.WriteWarning($"-{nameof(RecordLimit)} is less than -{nameof(PageSize)}; -{nameof(PageSize)} will be ignored.");
			}
		}

		if (this.PageSize.HasValue)
		{
			if (this.PageSize == 0)
			{
				this.WriteWarning($"-{nameof(PageSize)} is set to 0.  There will be no results.  To search without using paging, don't specify -{nameof(PageSize)}.");
			}
		}
	}

	private bool _pageHasResult;
	public void OnEntry(LdapEntry entry)
	{
		this._pageHasResult = true;
		this.WriteRecord(entry);
	}

	protected readonly ConcurrentQueue<string> referralQueue = new ConcurrentQueue<string>();
	public void OnReference(string reference)
	{
		this.WriteMessage($"Received reference to " + reference);
		this.referralQueue.Enqueue(reference);
	}

	protected async Task BuildAndRunQuery(LdapClient ldap, LdapQuery query, CancellationToken cancellationToken)
	{
		var searchBase = query.SearchBase;
		var isRootDse = ((searchBase != null) && (searchBase.Rdns.Count == 0));

		if (this.PageSize.HasValue)
			query.PageSize = this.PageSize.Value;
		query.IncludeDeleted = this.IncludeDeleted.IsSet;
		query.IncludeRecycled = this.IncludeRecycled.IsSet;
		query.IncludeDeletedLinks = this.IncludeDeletedLinks.IsSet;
		query.Scope = this.Scope ?? (isRootDse ? LdapSearchScope.BaseObject : LdapSearchScope.WholeSubtree);
		query.DirSyncCookie = this.DirSync?.Bytes;

		int? recordLimit = this.RecordLimit;
		do
		{
			if (query.WatchForChanges)
				this.WriteMessage($"Watching changes; press Ctrl+C to quit.");

			if (recordLimit.HasValue)
			{
				query.PageSize = Math.Min(query.PageSize ?? recordLimit.Value, recordLimit.Value);
			}

			this._pageHasResult = false;
			var results = await ldap.Search(query, cancellationToken, this).ConfigureAwait(false);
			query.PagingBookmark = results.Bookmark;
			query.DirSyncCookie = results.DirsyncCookie;

			if (recordLimit.HasValue)
			{
				recordLimit = Math.Max(0, recordLimit.Value - results.EntryCount);
			}

			if (!results.DirsyncCookie.IsNullOrEmpty())
				this.WriteMessage($"Received dirsync cookie: {results.DirsyncCookie.ToHexString()}");
		} while ((!query.PagingBookmark.IsNullOrEmpty() || !query.DirSyncCookie.IsNullOrEmpty()) && this._pageHasResult && !cancellationToken.IsCancellationRequested);
	}
}
