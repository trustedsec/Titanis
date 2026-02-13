using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Represents a Kerberos credential that uses an encryption key to authenticate.
	/// </summary>
	public abstract class KerberosKeyCredentialBase : KerberosCredential
	{
		protected KerberosKeyCredentialBase(UserPrincipalName userName) : base(userName)
		{
		}

		internal override PreauthContext CreatePreauthContext(KerberosClient client, IKerberosCallback? callback) => new PreauthKeyContext(client, this, callback);
	}
}
