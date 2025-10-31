
using System;
using Titanis.Security;
using Titanis.Security.Kerberos;

namespace KerberosV5Spec2
{
	public partial class KRB_ERROR_Tagged30
	{
		internal Exception GetException()
		{
			// TODO: Provite e-text, although it's usually empty
			return new KerberosException((KerberosErrorCode)this.error_code);
		}
	}
	public partial class EncryptionKey
	{

	}
	public partial class PA_DATA
	{

	}
	public partial class ETYPE_INFO_ENTRY
	{

	}
	public partial class ETYPE_INFO2_ENTRY
	{

	}

	public partial class PrincipalName
	{
		internal SecurityPrincipalName ToSecurityPrincipalName()
			=> SecurityPrincipalName.Create((PrincipalNameType)this.name_type, Array.ConvertAll(this.name_string, r => r.Value));

	}
}
