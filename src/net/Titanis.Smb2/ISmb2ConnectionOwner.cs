using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2
{
	/// <summary>
	/// Receives notifications from a connection.
	/// </summary>
	public interface ISmb2ConnectionOwner
	{
		/// <summary>
		/// Called when the connection is aborted.
		/// </summary>
		/// <param name="connection">Connection aborted</param>
		/// <param name="exception">Exception indicating why</param>
		void OnConnectionAborted(Smb2Connection connection, Exception exception);
	}
}
