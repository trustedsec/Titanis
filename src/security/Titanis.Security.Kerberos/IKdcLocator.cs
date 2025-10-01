using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Titanis.Security.Kerberos
{
	[Flags]
	public enum LocateKdcOptions
	{
		None = 0,
		Home = 1,
	}

	/// <summary>
	/// Provides functionality for locating a KDC.
	/// </summary>
	/// <seealso cref="SimpleKdcLocator"/>
	public interface IKdcLocator
	{
		/// <summary>
		/// Determines the endpoint for a KDC for a specified realm.
		/// </summary>
		/// <param name="realm">Realm</param>
		/// <param name="options">Options</param>
		/// <returns>An <see cref="EndPoint"/> for communicating with the KDC.</returns>
		EndPoint LocateKdc(string realm, LocateKdcOptions options);
	}
}
