using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	public enum EncChecksumType
	{
		// [RFC 3961] § 8
		Crc32 = 1,
		RsaMd4 = 2,
		rsaMd4Des = 3,
		DesMac = 4,
		DesMacK = 5,
		RsaMd4DesK = 6,
		RsaMd5 = 7,
		RsaMd5Des = 8,
		Sha1 = -131,

		// [RFC 3962] § 7
		HmacSha1_96_Aes128 = 15,
		HmacSha1_96_Aes256 = 16,

		// [RFC 4757] 4.
		HmacMd5String = -138,
	}
}
