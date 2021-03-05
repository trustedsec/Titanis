using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2
{
	public enum Smb2Priority : byte
	{
		Negotiate = 1,
		CreateDir = 3,
		OpenDir = 3,
		SessionSetup = 1,
		TreeConnect = 1,
		TreeDisconnect = 1,
		Close = 1,
		Logoff = 1,
		ChangeNotify = 3,
		Read = 3,

		/// <summary>
		/// Used for PDUs where the standard behavior is unknown
		/// </summary>
		Unknown = 0,

		Mask = 7,
	}
}
