using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	// [MS-KILE] § 2.2.7 - Supported Encryption Types Bit Flags
	[Flags]
	public enum SupportedEncryptionTypes
	{
		None = 0,

		DesCbcCrc = 1,
		DesCbcMd5 = 2,
		Rc4Hmac = 4,
		Aes128CtsHmacSha1_96 = 8,
		Aes256CtsHmacSha1_96 = 0x10,
		Aes256CtsHmacSha1_96_SK = 0x20,

		Fast = (1 << 16),
		CompoundIdentitySupported = (1 << 17),
		ClaimsSupported = (1 << 18),
		ResourceSidCompressionDisabled = (1 << 19),
	}
}
