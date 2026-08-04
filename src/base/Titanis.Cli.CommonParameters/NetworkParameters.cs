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
		[Advanced]
		public SwitchParam UseTcp6Only { get; set; }

		[Parameter]
		[Alias("4")]
		[Description("Only use TCP over IPv4 endpoint")]
		[Category(ParameterCategories.Connection)]
		[Advanced]
		public SwitchParam UseTcp4Only { get; set; }

		[Parameter]
		[Description("End point of SOCKS 5 server to use")]
		[Category(ParameterCategories.Connection)]
		[Placeholder("host-or-ip:port")]
		public EndPoint Socks5 { get; set; }

		private INameResolverService? _hostResolver;
		private ISocketService? _hostSocketService;

		protected sealed override void Initialize(IServiceContainer services)
		{
			this._hostResolver = services.GetService<INameResolverService>();
			this._hostSocketService = services.GetService<ISocketService>();

			base.Initialize(services);
			services.AddService(typeof(ISocketService), this.CreateSocketService);
			services.AddService(typeof(INameResolverService), this);
		}

		private ISocketService? CreateSocketService(IServiceContainer container, Type serviceType)
		{
			var log = container.GetService<ILog>();
			if (log != null)
			{
				ISocketService socketService = this._hostSocketService ?? new PlatformSocketService(this, log);
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

		private INameResolverService GetHostResolver() => this._hostResolver ??= new PlatformNameResolverService(ResolverOptions, this.Log);
		private NameResolverOptions ResolverOptions =>
			UseTcp4Only.IsSet ? NameResolverOptions.UseTcp4Only
			: UseTcp6Only.IsSet ? NameResolverOptions.UseTcp6Only
			: NameResolverOptions.Default;

		protected string? TargetServerName => (this.Owner as IHaveServerName)?.ServerName;

		private async Task<IPAddress[]> ResolveStaticAsync(string hostName, CancellationToken cancellationToken)
		{
			var primaryTarget = this.TargetServerName;
			var log = this.Log;

			// Only override the address of the primary target
			string[]? hostNames = null;
			if (string.Equals(hostName, primaryTarget, StringComparison.OrdinalIgnoreCase))
				hostNames = this.HostAddress;

			// If this is not the primary target, or there are no overrides specified, use the caller-provided name.
			hostNames ??= [hostName];

			// Resolve the host address
			List<IPAddress> addrs = new List<IPAddress>();
			var hostResolver = this.GetHostResolver();
			try
			{
				foreach (var hostAddress in hostNames)
				{
					if (IPAddress.TryParse(hostAddress, out IPAddress ipaddr))
						addrs.Add(ipaddr);
					else
					{
						try
						{
							var entries = await hostResolver.ResolveAsync(hostAddress, cancellationToken).ConfigureAwait(false);
							addrs.AddRange(entries);
						}
						catch { }
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
					throw new InvalidOperationException($"Cannot resolve host '{hostName}'.");

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

			return ResolveStaticAsync(hostName, cancellationToken);
		}
	}
}
