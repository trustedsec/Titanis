using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Implements <see cref="IKdcLocator"/> as a dictionary.
	/// </summary>
	/// <remarks>
	/// Add endpoints by calling <see cref="AddEndpoint(string, EndPoint)"/>.
	/// </remarks>
	public class SimpleKdcLocator : IKdcLocator
	{
		/// <summary>
		/// Initializes a new <see cref="SimpleKdcLocator"/>.
		/// </summary>
		/// <param name="homeEP">Endpoint of KDC for user's home domain</param>
		public SimpleKdcLocator(EndPoint homeEP)
		{
			this.HomeEP = homeEP;
		}

		/// <summary>
		/// Gets the endpoint for the user's home domain.
		/// </summary>
		public EndPoint HomeEP { get; }

		private Dictionary<string, EndPoint> _endpoints = new Dictionary<string, EndPoint>(StringComparer.OrdinalIgnoreCase);
		/// <summary>
		/// Adds an endpoint for a realm.
		/// </summary>
		/// <param name="realm">Realm</param>
		/// <param name="ep">KDC endpoint</param>
		/// <exception cref="ArgumentException"><paramref name="realm"/> is <see langword="null"/> or empty.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="ep"/> is <see langword="null"/>.</exception>
		public void AddEndpoint(string realm, EndPoint ep)
		{
			if (string.IsNullOrEmpty(realm)) throw new ArgumentException($"'{nameof(realm)}' cannot be null or empty.", nameof(realm));
			ArgumentNullException.ThrowIfNull(ep);

			this._endpoints[realm] = ep;
		}

		/// <inheritdoc/>
		/// <remarks>
		/// If an endpoint has been provided for <paramref name="realm"/>, it is returned;
		/// otherwise, <see cref="HomeEP"/> is returned.
		/// </remarks>
		public EndPoint LocateKdc(string realm, LocateKdcOptions options)
		{
			if (0 != (options & LocateKdcOptions.Home))
				return this.HomeEP;

			if (string.IsNullOrEmpty(realm))
				throw new ArgumentNullException(nameof(realm));

			if (this._endpoints.TryGetValue(realm, out EndPoint ep))
				return ep;
			else
				return new DnsEndPoint(realm, KerberosClient.KdcTcpPort);
		}
	}
}
