using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Titanis.Net;

namespace Titanis.Cli
{
	public class NetworkParameters : INameResolverService
	{
		[Parameter]
		[Alias("ha")]
		[Description("Network address(es) of the server")]
		[Category(ParameterCategories.Connection)]
		public string[]? HostAddress { get; set; }

		[Parameter]
		[Alias("6")]
		[Description("Only use TCP over IPv6 endpoint")]
		[Category(ParameterCategories.Connection)]
		public SwitchParam UseTcp6Only { get; set; }

		[Parameter]
		[Alias("4")]
		[Description("Only use TCP over IPv4 endpoint")]
		[Category(ParameterCategories.Connection)]
		public SwitchParam UseTcp4Only { get; set; }

		public void ValidateParameters(ParameterValidationContext context)
		{
			// -4 and -6 are mutually exclusive
			if (UseTcp6Only.IsSet && UseTcp4Only.IsSet)
				context.LogError("Both -4 and -6 were specified.  You may choose only one.");
		}

		private INameResolverService? _resolver;
		private INameResolverService GetPlatformResolver() => _resolver ??= new PlatformNameResolverService(ResolverOptions, Log);
		private NameResolverOptions ResolverOptions =>
			UseTcp4Only.IsSet ? NameResolverOptions.UseTcp4Only
			: UseTcp6Only.IsSet ? NameResolverOptions.UseTcp6Only
			: NameResolverOptions.Default;

		/// <summary>
		/// Gets or sets the <see cref="ILog"/> to log to.
		/// </summary>
		public ILog? Log { get; set; }

		private async Task<IPAddress[]> ResolveStaticAsync(string hostName, CancellationToken cancellationToken)
		{
			if (hostName != null && HostAddress == null)
				HostAddress = new string[] { hostName };

			var log = Log;

			// Resolve the host address
			List<IPAddress> addrs = new List<IPAddress>();
			try
			{
				foreach (var hostAddress in HostAddress)
				{
					if (IPAddress.TryParse(hostAddress, out IPAddress ipaddr))
						addrs.Add(ipaddr);
					else
					{
						var entry = await Dns.GetHostEntryAsync(hostAddress, cancellationToken).ConfigureAwait(false);
						addrs.AddRange(entry.AddressList);
					}

					if (UseTcp4Only.IsSet || UseTcp6Only.IsSet)
					{
						addrs.RemoveAll(r =>
						{
							bool include = UseTcp4Only.IsSet && r.AddressFamily == AddressFamily.InterNetwork
								|| UseTcp6Only.IsSet && r.AddressFamily == AddressFamily.InterNetworkV6;
							if (include)
							{
								log?.WriteVerbose($"Address {addrs} skipped due to address family requirements.");
								return true;
							}
							else
								return false;
						});
					}
				}

				if (addrs.Count == 0)
					throw new InvalidOperationException("Server address resolved, but none of the addresses meets the address family requirements.");

				return addrs.ToArray();
			}
			catch (Exception ex)
			{
				log?.WriteError($"Encountered an error in GetHostEntryAsync: {ex.Message}");
				throw;
			}
		}
		public Task<IPAddress[]> ResolveAsync(string hostName, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(hostName);

			if (HostAddress == null)
				return ResolveStaticAsync(hostName, cancellationToken);
			else
			{
				var resolver = GetPlatformResolver();
				return resolver.ResolveAsync(hostName, cancellationToken);
			}
		}
	}
}
