namespace Titanis.Ldap
{
	/// <summary>
	/// Receives notifications during a search.
	/// </summary>
	public interface ILdapClientSearchCallback
	{
		/// <summary>
		/// Called when the server sends an entry matching the search.
		/// </summary>
		/// <param name="entry"></param>
		void OnEntry(LdapEntry entry);
		/// <summary>
		/// Called when the server sends a reference.
		/// </summary>
		/// <param name="reference"></param>
		void OnReference(string reference);
	}
}
