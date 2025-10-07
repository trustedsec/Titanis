using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Titanis.Net;
using Titanis.Socks;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies parameters for network connections.
	/// </summary>
	public class NetworkParameters : ParameterGroupBase, INameResolverService
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

		[Parameter]
		[Description("End point of SOCKS 5 server to use")]
		[TypeConverter(typeof(EndPointConverter))]
		[Placeholder("host-or-ip:port")]
		public EndPoint Socks5 { get; set; }


		private ILog? _log;
		protected sealed override void Initialize(Command owner, IServiceContainer services)
		{
			base.Initialize(owner, services);
			services.AddService(typeof(ISocketService), this.CreateSocketService);
			this._log = services.GetService<ILog>();
		}

		private ISocketService? CreateSocketService(IServiceContainer container, Type serviceType)
		{
			var log = container.GetService<ILog>();
			if (log != null)
			{
				ISocketService socketService = new PlatformSocketService(this.GetPlatformResolver(), log);
				if (this.Socks5 != null)
				{
					socketService = new Socks5Client(this.Socks5, socketService, new Socks5Logger(log));
				}

				return socketService;
			}
			else
				return null;
		}

		public void ValidateParameters(ParameterValidationContext context)
		{
			// -4 and -6 are mutually exclusive
			if (UseTcp6Only.IsSet && UseTcp4Only.IsSet)
				context.LogError("Both -4 and -6 were specified.  You may choose only one.");
		}

		private INameResolverService? _resolver;
		private INameResolverService GetPlatformResolver() => _resolver ??= new PlatformNameResolverService(ResolverOptions, this._log);
		private NameResolverOptions ResolverOptions =>
			UseTcp4Only.IsSet ? NameResolverOptions.UseTcp4Only
			: UseTcp6Only.IsSet ? NameResolverOptions.UseTcp6Only
			: NameResolverOptions.Default;

		private async Task<IPAddress[]> ResolveStaticAsync(string hostName, CancellationToken cancellationToken)
		{
			if (hostName != null && HostAddress == null)
				HostAddress = new string[] { hostName };

			var log = this._log;

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
