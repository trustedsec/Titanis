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
	}
}
