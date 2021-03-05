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
	/// Implements <see cref="INameResolverService"/> as a simple dictionary lookup.
	/// </summary>
	/// <remarks>
	/// Call <see cref="SetAddress(string, IPAddress[])"/> host-to-address mappings.
	/// </remarks>
	public class DictionaryNameResolver : INameResolverService
	{
		/// <summary>
		/// Initializes a new <see cref="DictionaryNameResolver"/>.
		/// </summary>
		/// <param name="fallbackResolver"><see cref="INameResolverService"/> to fall back to.</param>
		public DictionaryNameResolver(INameResolverService? fallbackResolver = null)
		{
			this._fallbackResolver = fallbackResolver;
		}

		private readonly INameResolverService? _fallbackResolver;
		private Dictionary<string, IPAddress[]> _addresses = new Dictionary<string, IPAddress[]>();

		/// <summary>
		/// Sets a host-to-address mapping.
		/// </summary>
		/// <param name="hostName">Host name to map</param>
		/// <param name="addresses">Addresses to map to <paramref name="hostName"/></param>
		/// <exception cref="ArgumentException"><paramref name="hostName"/> is <see langword="null"/> or empty.</exception>
		/// <remarks>
		/// If <paramref name="hostName"/> is already mapped, the mapping is replaced and no error occurs.
		/// If <paramref name="addresses"/> is <see langword="null"/> or empty, then <paramref name="hostName"/>
		/// will not resolvne, and the request will not go to the fallback resolver.
		/// </remarks>
		public void SetAddress(string hostName, IPAddress[] addresses)
		{
			if (string.IsNullOrEmpty(hostName)) throw new ArgumentException($"'{nameof(hostName)}' cannot be null or empty.", nameof(hostName));

			this._addresses[hostName] = addresses;
		}

		/// <inheritdoc/>
		public Task<IPAddress[]> ResolveAsync(string hostName, CancellationToken cancellationToken)
		{
			if (this._addresses.TryGetValue(hostName, out var addrs))
			{
				if (addrs.IsNullOrEmpty())
					return Task.FromException<IPAddress[]>(new ArgumentException($"Unable to resolve host '{hostName}'.", nameof(hostName)));
				else
					return Task.FromResult(addrs);
			}
			else if (this._fallbackResolver != null)
				return this._fallbackResolver.ResolveAsync(hostName, cancellationToken);
			else
				return Task.FromException<IPAddress[]>(new ArgumentException($"Unable to resolve host '{hostName}'.", nameof(hostName)));
		}
	}
}
