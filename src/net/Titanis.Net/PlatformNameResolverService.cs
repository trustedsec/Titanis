using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Titanis.Net
{
	public enum NameResolverOptions
	{
		None = 0,
		Default = None,

		UseTcp4Only = 1,
		UseTcp6Only = 2,
	}

	/// <summary>
	/// Implements <see cref="INameResolverService"/> using the system DNS facilities.
	/// </summary>
	public class PlatformNameResolverService : INameResolverService
	{
		public PlatformNameResolverService(NameResolverOptions options = NameResolverOptions.None, ILog? log = null)
		{
			if ((options & (NameResolverOptions.UseTcp4Only | NameResolverOptions.UseTcp6Only)) == (NameResolverOptions.UseTcp4Only | NameResolverOptions.UseTcp6Only))
				throw new ArgumentException("UseTcp4Only and UseTcp6Only may not be used together.", nameof(options));

			this.Options = options;
			Log = log;
		}

		public NameResolverOptions Options { get; }
		public ILog? Log { get; }

		public bool UseTcp4Only => 0 != (this.Options & NameResolverOptions.UseTcp4Only);
		public bool UseTcp6Only => 0 != (this.Options & NameResolverOptions.UseTcp6Only);

		public async Task<IPAddress[]> ResolveAsync(string hostName, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(hostName)) throw new ArgumentException($"'{nameof(hostName)}' cannot be null or empty.", nameof(hostName));

			var hostAddress = hostName;
			var options = this.Options;

			return await ResolveAsync(hostAddress, options, this.Log, cancellationToken).ConfigureAwait(false);
		}

		public const string PlatformResolverSourceName = "PlatformNameResolver";

		public static async Task<IPAddress[]> ResolveAsync(
			string hostAddress,
			NameResolverOptions options,
			ILog? log,
			CancellationToken cancellationToken)
		{
			bool useTcp4Only = 0 != (options & NameResolverOptions.UseTcp4Only);
			bool useTcp6Only = 0 != (options & NameResolverOptions.UseTcp6Only);

			// Resolve the host address
			try
			{
				IPAddress[]? addrs;
				if (IPAddress.TryParse(hostAddress, out IPAddress ipaddr))
				{
					log?.WriteMessage(LogMessage.Diagnostic(PlatformResolverSourceName, $"Resolving address {hostAddress} as itself"));
					addrs = new IPAddress[] { ipaddr };
				}
				else
				{
					var entry = await Dns.GetHostEntryAsync(hostAddress, cancellationToken).ConfigureAwait(false);
					log?.WriteMessage(LogMessage.Diagnostic(PlatformResolverSourceName, $"System DNS resolved {hostAddress} as [ {string.Join(",", (object[])entry.AddressList)} ]"));
					addrs = entry.AddressList;
				}

				if (useTcp4Only || useTcp6Only)
				{
					addrs = Array.FindAll(addrs, r =>
					{
						bool include = ((useTcp4Only && r.AddressFamily == AddressFamily.InterNetwork))
							|| (useTcp6Only && r.AddressFamily == AddressFamily.InterNetworkV6);
						if (include)
						{
							log?.WriteMessage(LogMessage.Diagnostic(PlatformResolverSourceName, $"Removing {r} from result set because it doesn't match the address family filters."));
							return false;
						}
						else
							return true;
					});
				}

				if (addrs.Length == 0)
				{
					throw new InvalidOperationException("Server address resolved, but none of the addresses meets the address family requirements.");
				}

				log?.WriteMessage(LogMessage.Verbose(PlatformResolverSourceName, $"Resolved {hostAddress} with [ {string.Join(",", (object[])addrs)} ]"));
				return addrs;
			}
			catch (Exception ex)
			{
				log?.WriteMessage(LogMessage.Error(PlatformResolverSourceName, ex));
				throw;
			}
		}
	}
}
