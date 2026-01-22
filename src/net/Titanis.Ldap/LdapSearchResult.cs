namespace Titanis.Ldap
{
	/// <summary>
	/// Describes a search result.
	/// </summary>
	public class LdapSearchResult
	{
		internal LdapSearchResult(int entryCount, LdapEntry[] entries, string[] referrals)
		{
			this.EntryCount = entryCount;

			Entries = entries;
			Referrals = referrals;
		}

		/// <summary>
		/// Gets the number of entries returned.
		/// </summary>
		public int EntryCount { get; }

		/// <summary>
		/// Gets a list of entries that matched the query.
		/// </summary>
		public LdapEntry[] Entries { get; }
		/// <summary>
		/// Gets a lits of referrals returned by the server.
		/// </summary>
		public string[] Referrals { get; }
		/// <summary>
		/// Gets the bookmark returned by the server when paging is used.
		/// </summary>
		public byte[]? Bookmark { get; internal set; }
		/// <summary>
		/// Gets the dirsync cookie returned by the server.
		/// </summary>
		/// <seealso cref="LdapQuery.DirSyncCookie"/>
		public byte[]? DirsyncCookie { get; internal set; }
	}
}
