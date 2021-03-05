using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{

	public enum EType : ushort  // Underlying used by CCache
	{
		Aes128CtsHmacSha1_96 = 17,
		Aes256CtsHmacSha1_96 = 18,

		Rc4Hmac = 23,
		Rc4HmacExp = 24,
		DesCbcMd5 = 3,
		DesCbcCrc = 1,
	}
}
