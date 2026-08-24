using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis
{
	/// <summary>
	/// Wants a server name.
	/// </summary>
	public interface IWantServerName
	{
		/// <summary>
		/// Sets the target server name.
		/// </summary>
		public string? ServerName { get; set; }
	}
}
