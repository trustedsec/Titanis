
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
		internal KerberosException GetException()
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
								var typedData = decoder.DecodeValue<TYPED_DATA_Element>();
								if (typedData.data_type is -128)
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
			return new KerberosException((KerberosErrorCode)this.error_code, null);
		}

		private static KerberosException CreateNtstatusKerberosException(KerberosErrorCode kerbErrorCode, Ntstatus ntstatus)
		{
			return new KerberosException(kerbErrorCode, ntstatus);
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

	public sealed partial class PrincipalName : IEquatable<PrincipalName>
	{
		public sealed override bool Equals(object? obj) => (obj is PrincipalName other) && this.Equals(other);

		public bool Equals(PrincipalName? other)
		{
			bool equals = (other != null)
				&& (this.name_type == other.name_type)
				&& (this.name_string.Length == other.name_string.Length);
			if (equals)
			{
				for (int i = 0; i < this.name_string.Length; i++)
				{
					if (!string.Equals(this.name_string[i].Value, other.name_string[i].Value, StringComparison.OrdinalIgnoreCase))
						return false;
				}
				return true;
			}
			return false;
		}

		public sealed override int GetHashCode()
		{
			int hash = this.name_type;
			foreach (var name in this.name_string)
			{
				hash = HashCode.Combine(hash, name.Value.GetHashCode(StringComparison.OrdinalIgnoreCase));
			}
			return hash;
		}

		public static bool operator ==(PrincipalName left, PrincipalName right) => object.ReferenceEquals(left, right) || (left is not null && left.Equals(right));
		public static bool operator !=(PrincipalName left, PrincipalName right) => !(left == right);

		internal bool? Matches(PrincipalNameType nameType, string name0)
		{
			return (PrincipalNameType)this.name_type == nameType
				&& (this.name_string.Length == 1)
				&& name0.Equals(this.name_string[0].Value, StringComparison.OrdinalIgnoreCase)
				;
		}

		internal bool? Matches(PrincipalNameType nameType, string name0, string name1)
		{
			return (PrincipalNameType)this.name_type == nameType
				&& (this.name_string.Length == 2)
				&& name0.Equals(this.name_string[0].Value, StringComparison.OrdinalIgnoreCase)
				&& name1.Equals(this.name_string[1].Value, StringComparison.OrdinalIgnoreCase)
				;
		}

		internal SecurityPrincipalName ToSecurityPrincipalName()
			=> SecurityPrincipalName.Create((PrincipalNameType)this.name_type, Array.ConvertAll(this.name_string, r => r.Value));

	}
}
