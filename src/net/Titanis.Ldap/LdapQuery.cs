using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop.Security;

namespace Titanis.Ldap
{
	/// <summary>
	/// Specifies flags that affect the operation of a query.
	/// </summary>
	[Flags]
	public enum LdapQueryOptions
	{
		None = 0,
		/// <summary>
		/// All attributes in <see cref="LdapQuery.Attributes"/> appear in every <see cref="LdapEntry"/>even if they have no values.
		/// </summary>
		/// <remarks>
		/// This is useful in data-binding scenarios when records must appear uniform.
		/// </remarks>
		IncludeMissingAttributes = 1,
		AllPages = 2,
	}

	public class LdapQuery
	{
		public LdapQuery() { }
		public LdapQuery(
			LdapDistinguishedName? searchBase,
			LdapSearchScope scope,
			LdapFilter? filter,
			AttributeSpec[]? attributes
			)
		{
			this.SearchBase = searchBase;
			this.Filter = filter;
			this.Scope = scope;
			this.Attributes = attributes;
		}

		/// <summary>
		/// Specifies options for the query.
		/// </summary>
		public LdapQueryOptions Options { get; set; }
		public bool IncludeMissingAttributes => 0 != (this.Options & LdapQueryOptions.IncludeMissingAttributes);
		/// <summary>
		/// Gets or sets the search base.
		/// </summary>
		/// <remarks>
		/// If none specified, <see cref="LdapClient.DomainRoot"/> is used.
		/// </remarks>
		public LdapDistinguishedName? SearchBase { get; set; }
		/// <summary>
		/// Gets or sets the search scope.
		/// </summary>
		public LdapSearchScope Scope { get; set; }
		/// <summary>
		/// Gets or sets the filter to apply.
		/// </summary>
		/// <remarks>
		/// If none specified, all results are returned.
		/// </remarks>
		public LdapFilter? Filter { get; set; }
		/// <summary>
		/// Gets or sets the attributes to retrieve.
		/// </summary>
		/// <remarks>
		/// If none specified, all attributes are returned.
		/// </remarks>
		public AttributeSpec[]? Attributes { get; set; }
		/// <summary>
		/// Gets or sets the page size.
		/// </summary>
		/// <remarks>
		/// If set, limits the amount of results returned with a bookmark returned in <see cref="LdapSearchResult.Bookmark"/>.
		/// </remarks>
		public int? PageSize { get; set; }
		/// <summary>
		/// Gets or sets the paging bookmark.
		/// </summary>
		/// <remarks>
		/// When set, returns the next page of results.
		/// </remarks>
		public byte[]? PagingBookmark { get; set; }
		/// <summary>
		/// Gets or sets a value determining whether to remain connected and monitor changes.
		/// </summary>
		/// <remarks>
		/// When set, after issuing the query, the LDAP connection stays open and any changes to the
		/// directory that match the query are returned.
		/// </remarks>
		public bool WatchForChanges { get; set; }
		/// <summary>
		/// Gets or sets a value determining whether to include deleted items in the results.
		/// </summary>
		/// <remarks>
		/// Setting this property to <see langword="true"/> does not return recycled items; for this, set <see cref="IncludeRecycled"/>.
		/// </remarks>
		public bool IncludeDeleted { get; set; }
		/// <summary>
		/// Gets or sets a value determining whether to include deleted and recycled items in the results.
		/// </summary>
		public bool IncludeRecycled { get; set; }
		public bool IncludeDeletedLinks { get; set; }
		public bool IncludeExtendedDNs { get; set; }
		public bool IncludeLinkTtl { get; set; }
		/// <summary>
		/// Gets or sets the dirsync cookie to retrieve changes after a certain point.
		/// </summary>
		/// <remarks>
		/// To get a dirsync cookie, set <see cref="DirSyncCookie"/> to an empty byte array (not <see langword="null"/>).
		/// The dirsync cookie is returned in <see cref="LdapSearchResult.DirsyncCookie"/>.
		/// </remarks>
		public byte[]? DirSyncCookie { get; set; }

		/// <summary>
		/// Gets or sets a value that specifies which sections of a security descriptor to return.
		/// </summary>
		public SecurityInfo? SdFlags { get; set; }
	}
}
