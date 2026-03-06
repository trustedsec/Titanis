
using System;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Provides functionality to get a key for a security principal for use as a Kerberos server.
	/// </summary>
	public interface IKerberosKeyStore
	{
		SessionKey? TryGetKeyFor(SecurityPrincipalName spn, EncProfile? encProf);
	}
}