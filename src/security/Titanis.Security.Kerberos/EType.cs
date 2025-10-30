using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{

	public enum EType : ushort  // Underlying used by CCache
	{
		// [RFC 3961] § 8
		DesCbcMd5 = 3,
		DesCbcCrc = 1,
		Rc4Hmac = 23,
		Rc4HmacExp = 24,

		// [RFC 3962] § 7
		Aes128CtsHmacSha1_96 = 17,
		Aes256CtsHmacSha1_96 = 18,

		// [RFC 4556] § 3.1
		DsaWithSha1 = 9,
		Md5WithRsa = 10,
		Sha1WithRsa = 11,
		Rc2Cbc = 12,
		Rsa = 13,
		RsaesOaep = 14,
		DesEde3Cbc = 15,
	}
}
