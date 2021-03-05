using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2
{
	/// <summary>
	/// Receives callback notifications from <see cref="Smb2Client"/>.
	/// </summary>
	public interface ISmb2TraceCallback
	{
		/// <summary>
		/// Called before connecting to a server.
		/// </summary>
		/// <param name="serverEP">Remote server endpoint</param>
		/// <param name="serverName">Name of server</param>
		/// <param name="options">Server connection options</param>
		void OnConnecting(EndPoint serverEP, string serverName, Smb2ConnectionOptions options);
		/// <summary>
		/// Called after the SMB client establishes a connection.
		/// </summary>
		/// <param name="serverEP">Remote server endpoint</param>
		/// <param name="connection">Established connection</param>
		void OnConnected(EndPoint serverEP, Smb2Connection connection);
		/// <summary>
		/// Called after authenticating a session.
		/// </summary>
		/// <param name="session">The new session</param>
		void OnSessionAuthenticated(Smb2Session session);
		/// <summary>
		/// Called after connecting to a share.
		/// </summary>
		/// <param name="uncPath">UNC path of share</param>
		/// <param name="share">Connected share</param>
		void OnShareConnected(UncPath uncPath, Smb2TreeConnect share);
		void OnDfsReferralReceived(UncPath uncPath, DfsReferral referral);
		void OnDfsReferralConnectFailed(UncPath uncPath, DfsReferral referral, DfsReferralEntry entry, Exception ex);
		void OnDfsReferralFollowed(UncPath originalPath, Smb2TreeConnect referredShare, UncPath referredPath);
	}
}
