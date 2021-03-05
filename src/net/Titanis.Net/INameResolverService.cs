using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Net
{
	/// <summary>
	/// Provides functionality to resolve a server name to an IP address.
	/// </summary>
	public interface INameResolverService
	{
		/// <summary>
		/// Resolves a host name to one or more IP addresses.
		/// </summary>
		/// <param name="hostName">Host name to resolve</param>
		/// <returns>An array of one or more <see cref="IPAddress"/> entries.</returns>
		/// <remarks>
		/// If the implementation cannot resolve <paramref name="hostName"/>, it must throw an exception.
		/// </remarks>
		Task<IPAddress[]> ResolveAsync(string hostName, CancellationToken cancellationToken);
	}
}
