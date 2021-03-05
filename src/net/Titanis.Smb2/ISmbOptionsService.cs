namespace Titanis.Smb2
{
	/// <summary>
	/// Provides functionality to get the options for a specific server.
	/// </summary>
	public interface ISmbOptionsService
	{
		/// <summary>
		/// Gets the options for a server.
		/// </summary>
		/// <param name="serverName">Name of the server</param>
		/// <returns><see cref="Smb2ConnectionOptions"/> matching the server, if found; otherwise, <see langword="null"/></returns>
		Smb2ConnectionOptions? GetConnectionOptionsFor(string serverName);
	}
}