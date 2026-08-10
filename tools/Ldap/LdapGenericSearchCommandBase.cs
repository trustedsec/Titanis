using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.ComponentModel;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

/// <summary>
/// Base class for commands that run queries.
/// </summary>
/// <remarks>
/// This class has properties that allow the user to control query execution, such as page size and whether to follow references.
/// </remarks>
public abstract class LdapGenericSearchCommandBase : LdapCommandBase, ILdapClientSearchCallback
{
	[Parameter]
	[Advanced]
	[Description("Number of results to fetch per page")]
	[DefaultValue(100)]
	public int? PageSize { get; set; }

	[Parameter]
	[Description("Max number of records to return")]
	public int? RecordLimit { get; set; }

	[Parameter]
	[Advanced]
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

	/// <summary>
	/// Base class can further customize the query.
	/// </summary>
	/// <param name="query">Query to be run</param>
	protected virtual void SetQueryProperties(LdapQuery query)
	{

	}

	protected async Task BuildAndRunQuery(LdapClient ldap, LdapQuery query, CancellationToken cancellationToken)
	{

		if (this.PageSize.HasValue)
			query.PageSize = this.PageSize.Value;

		this.SetQueryProperties(query);


		if (!this.OutputFields.IsNullOrEmpty())
		{
			List<AttributeSpec> attrs = new List<AttributeSpec>(this.OutputFields.Length);
			//if (this.OutputFields is ["**"])
			//{
			//	var allAttrs = await ldap.Search(new LdapQuery(ldap.SchemaRoot, LdapSearchScope.SingleLevel, LdapFilter.Parse("(objectClass=attributeSchema)"), [LdapAttributeTypes.LDAPDisplayName]) { Options = LdapQueryOptions.AllPages, PageSize = 100 }, cancellationToken);
			//	foreach (var attrEntry in allAttrs.Entries)
			//	{
			//		var ldapName = attrEntry[LdapAttributeTypes.LDAPDisplayName]?.Value as string;
			//		if (ldapName != null)
			//		{
			//			attrs.Add(ldapName);
			//			fieldNames.Add(ldapName);
			//		}
			//	}
			//	this.OutputFields = fieldNames.ToArray();
			//}
			//else
			{
				List<string> fieldNames = new List<string>(1 + this.OutputFields.Length);
				foreach (var name in this.OutputFields)
				{
					//if (name.Equals("*constructed", StringComparison.OrdinalIgnoreCase))
					//{
					//	var allAttrs = await ldap.Search(new LdapQuery(ldap.SchemaRoot, LdapSearchScope.SingleLevel, LdapFilter.Parse("(&(objectClass=attributeSchema)(systemFlags&=Constructed))"), [LdapAttributeTypes.LDAPDisplayName]) { Options = LdapQueryOptions.AllPages, PageSize = 100 }, cancellationToken);
					//	foreach (var attrEntry in allAttrs.Entries)
					//	{
					//		var ldapName = attrEntry[LdapAttributeTypes.LDAPDisplayName]?.Value as string;
					//		if (ldapName != null)
					//		{
					//			attrs.Add(ldapName);
					//			fieldNames.Add(ldapName);
					//		}
					//	}

					//}
					//else
					{
						if (!name.Equals(nameof(LdapEntry.EntryName), StringComparison.OrdinalIgnoreCase))
						{
							attrs.Add(new AttributeSpec(name));
						}
						fieldNames.Add(name);
					}
				}
				this.OutputFields = fieldNames.ToArray();
			}

			if (attrs.Count == 0)
				// The user has not requested any attributes; put a known-bad value to prevent all attributes from being returned (Windows does this)
				attrs.Add("1.1");
			query.Attributes = Array.ConvertAll(this.OutputFields!, r => new AttributeSpec(r));
		}

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

	private bool _pageHasResult;
	void ILdapClientSearchCallback.OnEntry(LdapEntry entry)
	{
		this._pageHasResult = true;
		this.WriteRecord(entry);
	}

	protected readonly ConcurrentQueue<string> referralQueue = new ConcurrentQueue<string>();
	void ILdapClientSearchCallback.OnReference(string reference)
	{
		this.WriteVerbose($"Received reference to " + reference);
		this.referralQueue.Enqueue(reference);
	}

}
