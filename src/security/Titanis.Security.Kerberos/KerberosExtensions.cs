
using ms_adtsclaims;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Security;
using Titanis.Security.Kerberos;
using Titanis.Winterop;

namespace KerberosV5Spec2
{
	public enum ErrorDataType
	{
		// [MS-KILE] § 2.2.2
		SkewRecovery = 2,
		Extended = 3,
	}

	public partial class KRB_ERROR_Tagged30
	{
		internal Exception GetException()
		{
			if (this.e_data != null)
			{
				// Try to determine error data type
				Asn1DerDecoder decoder = Asn1DerEncoding.CreateDerDecoder(this.e_data);
				if (decoder.CheckTag(new Asn1Tag(0x20000010)))
				{
					decoder.DecodeTlvStart(new Asn1Tag(0x20000010));

					if (decoder.CheckTag(new Asn1Tag(0x20000010)))
					{
						try
						{
							var padataList = decoder.DecodeValue<Asn1SequenceOf<PA_DATA>>().Values;

							return new KerberosPadataException((KerberosErrorCode)this.error_code, padataList);
						}
						catch (Asn1UnexpectedTagException ex) when (ex.ExpectedTag == new Asn1Tag(0xA0000001) && ex.ActualTag == new Asn1Tag(0xA0000000))
						{
							// This is probably a U2U
							try
							{
								var code = decoder.DecodeTaggedValue(new Asn1Tag(0xA0000000), d => decoder.DecodeIntegerTlvAsInt32());
								if (code is -128)
								{
									return CreateNtstatusKerberosException((KerberosErrorCode)this.error_code, Ntstatus.STATUS_USER2USER_REQUIRED);
								}
							}
							catch
							{
								// Fall through to general case
							}
						}
					}
					else if (decoder.CheckTag(new Asn1Tag(0xA0000001)))
					{
						// [MS-KILE] § 2.2.2
						var errorData = decoder.DecodeValue<KERB_ERROR_DATA>();

						if (errorData?.data_type == (int)ErrorDataType.Extended && errorData.data_value?.Length == 12)
						{
							Ntstatus ntstatus = (Ntstatus)BinaryPrimitives.ReadUInt32LittleEndian(errorData.data_value);
							return CreateNtstatusKerberosException((KerberosErrorCode)this.error_code, ntstatus);
						}
					}
				}
			}

			// TODO: Provide e-text, although it's usually empty
			return new KerberosException((KerberosErrorCode)this.error_code);
		}

		private static Exception CreateNtstatusKerberosException(KerberosErrorCode kerbErrorCode, Ntstatus ntstatus)
		{
			// It may actually be HRESULT
			Exception innerException;
			if (Enum.IsDefined((Hresult)ntstatus))
				innerException = ((Hresult)ntstatus).GetException();
			else
				innerException = ntstatus.GetException();

			return new KerberosException(kerbErrorCode, innerException);
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
