using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Cli
{
	/// <summary>
	/// Provides a server name.
	/// </summary>
	public interface IHaveServerName
	{
		/// <summary>
		/// Gets the target server name.
		/// </summary>
		public string? ServerName { get; }
	}
}
