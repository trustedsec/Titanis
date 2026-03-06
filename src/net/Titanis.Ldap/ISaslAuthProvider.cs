using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Security;

namespace Titanis.Ldap
{
	/// <summary>
	/// Exposes functionality to provide <see cref="AuthServerContext"/> to a server implementation.
	/// </summary>
	public interface ISaslAuthProvider
	{
		/// <summary>
		/// Gets a <see cref="AuthServerContext"/> for a SASL mechanism.
		/// </summary>
		/// <param name="saslMechName">SASL mechanism</param>
		/// <returns><see cref="AuthServerContext"/> for <paramref name="saslMechName"/>, if available; otherwise, <see langword="null"/></returns>
		AuthServerContext? TryGetAuthContext(string saslMechName);
	}
}
