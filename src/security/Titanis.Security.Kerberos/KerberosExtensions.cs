
using System;
using System.Buffers.Binary;
using Titanis.Asn1.Serialization;
using Titanis.Security;
using Titanis.Security.Kerberos;
using Titanis.Winterop;

namespace KerberosV5Spec2
{
	public partial class KRB_ERROR_Tagged30
	{
		internal Exception GetException()
		{
			if (this.e_data != null)
			{
				// [MS-KILE] § 2.2.2
				var errorInfo = Asn1DerDecoder.DecodeTlv<KERB_ERROR_DATA>(this.e_data);
				if (errorInfo?.data_type == 3 && errorInfo.data_value?.Length == 12)
				{
					Ntstatus ntstatus = (Ntstatus)BinaryPrimitives.ReadUInt32LittleEndian(errorInfo.data_value);
					ntstatus.CheckAndThrow();
				}
			}
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
