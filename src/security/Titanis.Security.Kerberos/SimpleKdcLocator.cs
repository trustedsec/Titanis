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
		/// <param name="defaultEP">Default endpoint if no other endpoint specified.</param>
		public SimpleKdcLocator(EndPoint defaultEP)
		{
			this.DefaultEP = defaultEP;
		}

		/// <summary>
		/// Gets the endpoint returned for any realm not added with <see cref="AddEndpoint(string, EndPoint)"/>.
		/// </summary>
		public EndPoint DefaultEP { get; }

		private Dictionary<string, EndPoint> _endpoints = new Dictionary<string, EndPoint>(StringComparer.OrdinalIgnoreCase);
		/// <summary>
		/// Adds an endpoint.
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
		/// otherwise, <see cref="DefaultEP"/> is returned.
		/// </remarks>
		public EndPoint FindKdc(string realm)
		{
			if (string.IsNullOrEmpty(realm))
				throw new ArgumentNullException(nameof(realm));

			if (this._endpoints.TryGetValue(realm, out EndPoint ep))
				return ep;
			else
				return this.DefaultEP;
		}
	}
}
