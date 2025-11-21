using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
	}

	public class LdapQuery
	{
		public LdapQuery() { }
		public LdapQuery(
			LdapDistinguishedName? searchBase,
			LdapSearchScope scope,
			LdapFilter? filter,
			AttributeSpec[] attributes
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
		public int? PageSize { get; set; }
		public byte[]? PagingBookmark { get; set; }
		public bool WatchForChanges { get; set; }
		public bool IncludeDeleted { get; set; }
		public bool IncludeRecycled { get; set; }
		public bool IncludeDeletedLinks { get; set; }
		public byte[]? DirSyncCookie { get; set; }
	}
}
