using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	enum EncChecksumType
	{
		Crc32 = 1,
		RsaMd4 = 2,
		rsaMd4Des = 3,
		DesMac = 4,
		DesMacK = 5,
		RsaMd4DesK = 6,
		RsaMd5 = 7,
		RsaMd5Des = 8,
		Sha1 = -131,
		HmacSha1_96_Aes128 = 15,
		HmacSha1_96_Aes256 = 16,
		HmacMd5String = -138,
	}
}
